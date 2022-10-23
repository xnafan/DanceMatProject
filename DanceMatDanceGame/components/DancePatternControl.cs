using DanceMatClassLibrary;
using DanceMatDanceGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using drawing = System.Drawing;
using System.Linq;
using System;

namespace DanceMatMazeGame.components
{
    public class DancePatternControl
    {
        public struct DanceMove : IEquatable<DanceMove>
        {
            public bool Up { get; set; } = false;
            public bool Down { get; set; } = false;
            public bool Left { get; set; } = false;
            public bool Right{ get; set; } = false;

            public DanceMove(bool up, bool down, bool left, bool right)
            {
                Up = up;
                Down = down;
                Left = left;
                Right = right;
            }

            public bool TestForDanceMatStateMatch(DanceMatState danceMatState)
            {
                return danceMatState.Up == Up &&
                    danceMatState.Down == Down &&
                    danceMatState.Left == Left &&
                    danceMatState.Right == Right;
            }

            public List<bool> GetAllButtons() => new List<bool>() {Left, Down, Up, Right };

            public bool Equals(DanceMove other)
            {
                return Up == other.Up && Down == other.Down && Left == other.Left && Right == other.Right;
            }
            public bool NothingPressed { get { return !(Up || Down || Right || Left); } }
        }

        #region Variables and properties

        private Random _random = new Random();
        public Vector2 BottomCenter { get; set; }
        public drawing.Size Size { get; set; }
        public Texture2D ArrowTexture { get; }
        public List<DanceMove> DanceMoves { get; set; } = new();
        public DanceMat DanceMat { get; set; }
        private bool IsReadyForInput { get; set; } = false;

        #endregion

        #region Constructor

        public DancePatternControl(Texture2D arrowTexture, Vector2 bottomCenter, drawing.Size size, DanceMat danceMat)
        {
            ArrowTexture = arrowTexture;
            BottomCenter = bottomCenter;
            Size = size;
            DanceMat = danceMat;
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            CheckForDanceMatStateMatchAndRemoveIfMatch();
        }

        private bool CheckForDanceMatStateMatchAndRemoveIfMatch()
        {
            bool match = false;
            DanceMatState currentState = DanceMat.GetCurrentState();
            if (currentState.NothingPressed) { IsReadyForInput = true; }
            if (!IsReadyForInput) { return false; }
            if (DanceMoves.Count == 0) { return false; }
            if (DanceMoves.First().TestForDanceMatStateMatch(currentState))
            {
                DanceMoves.RemoveAt(0);
                AddRandomMove();
                match = true;
                IsReadyForInput = false;
            }
            return match;
        }

        public void AddRandomMove()
        {
            DanceMove danceMove = new ();
            while(danceMove.GetAllButtons().Count(button => button) > 2 || danceMove.GetAllButtons().Count(button => button) == 0)
            {
                 danceMove = new DanceMove()
                {
                    Up = _random.Next(2) == 0,
                    Down = _random.Next(2) == 0,
                    Left = _random.Next(2) == 0,
                    Right = _random.Next(2) == 0,
                };
            }
            DanceMoves.Add(danceMove);
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < DanceMoves.Count; i++)
            {
                Vector2 topLeft = new Vector2(BottomCenter.X - Size.Width / 2 , BottomCenter.Y - Size.Height * (i + 1));
                DrawSingleDanceMove(topLeft, DanceMoves[i]);
            }
        }

        private void DrawSingleDanceMove(Vector2 topLeft, DanceMove danceMove)
        {
            Rectangle destinationRectangle = new Rectangle((int)topLeft.X, (int)topLeft.Y,Size.Width/4, Size.Height);
            Rectangle sourceRectangle = new Rectangle(0, 0, ArrowTexture.Width / 4, ArrowTexture.Height / 2); ;
            destinationRectangle.X = (int)topLeft.X;
            destinationRectangle.Y = (int)topLeft.Y;

            var moves = danceMove.GetAllButtons();
            for (int i = 0; i < 4; i++)
            {
                if (moves[i]) { DanceGame.SpriteBatch.Draw(ArrowTexture, destinationRectangle, sourceRectangle, Color.White); }
                destinationRectangle.Offset(Size.Width / 4, 0);
                sourceRectangle.Offset(ArrowTexture.Width / 4, 0);
            }
        }
        #endregion
    }
}