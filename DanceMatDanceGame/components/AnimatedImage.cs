using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceMatDanceGame.components
{
    public class AnimatedImage
    {
        #region Properties and variables
        public Vector2 TopLeft { get; set; }
        public Texture2D Texture { get; set; }
        public float FrameDelayInMs { get; set; }
        public int NumberOfImagesInTexture { get; set; }

        private double _msSinceLastImageSwap = 0;
        public int CurrentImageIndex { get; set; }

        public float ScaleFactor { get; set; }

        #endregion

        #region Constructor
        public AnimatedImage(Vector2 topLeft, Texture2D texture, float frameDelayInMs, int numberOfImagesInTexture, float scaleFactor = 1)
        {
            TopLeft = topLeft;
            Texture = texture;
            FrameDelayInMs = frameDelayInMs;
            NumberOfImagesInTexture = numberOfImagesInTexture;
            ScaleFactor = scaleFactor;
        }
        #endregion

        #region Update, Draw and related
        public void Update(GameTime gameTime)
        {
            _msSinceLastImageSwap += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_msSinceLastImageSwap >= FrameDelayInMs) { NextImage(); }
        }

        private void NextImage()
        {
            CurrentImageIndex++;
            CurrentImageIndex %= NumberOfImagesInTexture;
            _msSinceLastImageSwap = 0;
        }

        public void Draw(GameTime gameTime)
        {
            DanceGame.SpriteBatch.Draw(Texture, new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)(Texture.Width / NumberOfImagesInTexture * ScaleFactor), (int)(Texture.Height * ScaleFactor)),
                new Rectangle((Texture.Width / NumberOfImagesInTexture) * CurrentImageIndex, 0, Texture.Width / NumberOfImagesInTexture, Texture.Height), Color.White);
        } 
        #endregion
    }
}
