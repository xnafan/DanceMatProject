using DanceMatClassLibrary;
using DanceMatDanceGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using drawing = System.Drawing;
using System.Linq;
using System;
using DanceMatDanceGame.components;

namespace DanceMatMazeGame.components
{
    public class DanceMovesListControl
    {

        #region Variables and properties

        private Random _random = new Random();
        public Vector2 TopLeft { get; set; }
        public Texture2D ArrowTexture { get; }
        public int ArrowSymbolSize { get; private set; }
        public List<DanceMoveControl> DanceMoveControls { get; set; } = new();
        public DanceMat DanceMat { get; set; }
        private bool IsReadyForInput { get; set; } = false;

        public float Speed { get; set; } = 4;

        #endregion

        #region Eventhandler and related
        public event EventHandler<DanceMoveSuccesRate> SuccessfulDanceMove; 
        #endregion

        #region Constructor

        public DanceMovesListControl(Texture2D arrowTexture, Vector2 topLeft, int arrowSymbolSize, DanceMat danceMat)
        {
            ArrowTexture = arrowTexture;
            TopLeft = topLeft;
            ArrowSymbolSize = arrowSymbolSize;
            DanceMat = danceMat;
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            DanceMoveControls.ForEach(control => control.TopLeft += Vector2.UnitY * (float)gameTime.ElapsedGameTime.TotalMilliseconds * -Speed * .05f);
            CheckForDanceMatStateMatchAndRemoveIfMatch();
            var topDanceControl = DanceMoveControls.FirstOrDefault();
            if(topDanceControl == null) { return; }
            if (Math.Abs(topDanceControl.TopLeft.Y - TopLeft.Y) < 2)
            {
                DanceGame.Drums.Play();
            }
            if (topDanceControl.TopLeft.Y < TopLeft.Y - ArrowSymbolSize / 2)
            {
                SuccessfulDanceMove?.Invoke(this, new DanceMoveSuccesRate(DanceMoveControls.First().DanceMove, DanceMoveSuccesRate.Precision.Lost));
                DanceMoveControls.RemoveAt(0);
                AddRandomMove();
            }
        }

        private bool CheckForDanceMatStateMatchAndRemoveIfMatch()
        {
            bool match = false;
            DanceMatState currentState = DanceMat.GetCurrentState();
            if (currentState.NothingPressed) { IsReadyForInput = true; }
            if (!IsReadyForInput) { return false; }
            if (DanceMoveControls.Count == 0) { return false; }
            if (DanceMoveControls.First().DanceMove.TestForDanceMatStateMatch(currentState))
            {
                var firstDanceMoveControl = DanceMoveControls[0];
                var move = firstDanceMoveControl.DanceMove;
                DanceMoveSuccesRate.Precision timingPrecision = DanceMoveSuccesRate.Precision.Bad;
                var pixelsFromPerfection = firstDanceMoveControl.TopLeft.Y - TopLeft.Y;
                if (pixelsFromPerfection <= ArrowSymbolSize/16) { timingPrecision = DanceMoveSuccesRate.Precision.Perfect; }
                else if (pixelsFromPerfection <= ArrowSymbolSize /12) { timingPrecision = DanceMoveSuccesRate.Precision.Close; }
                else if (pixelsFromPerfection <= ArrowSymbolSize / 8) { timingPrecision = DanceMoveSuccesRate.Precision.Acceptable; }
                DanceMoveSuccesRate successRate = new DanceMoveSuccesRate(move,timingPrecision);
                SuccessfulDanceMove?.Invoke(this, successRate);
                DanceMoveControls.RemoveAt(0);
                AddRandomMove();
                match = true;
                IsReadyForInput = false;
            }
            return match;
        }

        public void AddRandomMove()
        {
            DanceMove danceMove = new ();
            while(danceMove.GetAllButtons().Count(button => button) != 2)
            {
                 danceMove = new DanceMove()
                {
                    Up = _random.Next(2) == 0,
                    Down = _random.Next(2) == 0,
                    Left = _random.Next(2) == 0,
                    Right = _random.Next(2) == 0,
                };
            }
            AddNewDanceMoveControl(danceMove);
        }

        private void AddNewDanceMoveControl(DanceMove danceMove)
        {
            var newDanceMoveControl = new DanceMoveControl(new Vector2(), ArrowSymbolSize, ArrowTexture, danceMove);
            if (DanceMoveControls.Count == 0)
            {
                newDanceMoveControl.TopLeft = new Vector2(TopLeft.X, TopLeft.Y + ((DanceMoveControls.Count + 1) * ArrowSymbolSize * 2.5f));
            }
            else
            {
                var lastDanceControl = DanceMoveControls.Last();
                newDanceMoveControl.TopLeft = new Vector2(lastDanceControl.TopLeft.X, lastDanceControl.TopLeft.Y + (lastDanceControl.Size.Height)  * 1.5f);
            }
            
            DanceMoveControls.Add(newDanceMoveControl);
        }

        public void Draw(GameTime gameTime)
        {
            DanceGame.SpriteBatch.Draw(ArrowTexture, new Rectangle((int)TopLeft.X, (int)TopLeft.Y, ArrowSymbolSize * 4, ArrowSymbolSize),
                new Rectangle(0,0, ArrowTexture.Width, ArrowTexture.Height/2),
                Color.LightGreen);
            float opacity = 1;
            DanceMoveControls.ForEach(control => control.Draw(gameTime, opacity-=.15f));
        }

        #endregion
    }
}