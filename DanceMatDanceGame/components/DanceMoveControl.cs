using DanceMatClassLibrary;
using DanceMatMazeGame.components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using drawing = System.Drawing;

namespace DanceMatDanceGame.components
{
    public class DanceMoveControl
    {
        public Vector2 TopLeft { get; set; }
        public drawing.Size Size { get; set; }
        public Texture2D ArrowTexture { get; }
        public DanceMove DanceMove { get; set; }

        public DanceMoveControl(Vector2 location, int arrowSymbolSize, Texture2D arrowTexture, DanceMove danceMove)
        {
            TopLeft = location;
            Size = new drawing.Size(arrowSymbolSize * 4, arrowSymbolSize);
            ArrowTexture = arrowTexture;
            DanceMove = danceMove;
        }

        public void Draw(GameTime gameTime)
        {
            Rectangle destinationRectangle = new Rectangle((int)TopLeft.X, (int)TopLeft.Y, Size.Height, Size.Height);
            Rectangle sourceRectangle = new Rectangle(0, 0, ArrowTexture.Width / 4, ArrowTexture.Height / 2); ;

            var moves = DanceMove.GetAllButtons();

            for (int i = 0; i < 4; i++)
            {
                if (moves[i]) { DanceGame.SpriteBatch.Draw(ArrowTexture, destinationRectangle, sourceRectangle, Color.White); }
                destinationRectangle.Offset(Size.Width / 4, 0);
                sourceRectangle.Offset(ArrowTexture.Width / 4, 0);
            }
        }
    }
}