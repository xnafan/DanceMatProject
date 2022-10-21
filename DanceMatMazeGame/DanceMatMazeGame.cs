using DanceMatClassLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DanceMatMazeGame
{
    public class DanceMatMazeGame : Game
    {
        #region Variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tileset;
        private bool[,] _maze = new bool[40, 32];
        private Vector2 _playerPosition;
        private int _tileSize = 32;
        private DanceMat _danceMat;
        #endregion

        #region Constructor and initialization
        public DanceMatMazeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _danceMat = new DanceMat();
            _danceMat.ButtonStateChanged += _danceMat_ButtonStateChanged;
        }


        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1024;

            ToggleFullScreen();
            _graphics.ApplyChanges();
        }

        private void ToggleFullScreen()
        {
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileset = Content.Load<Texture2D>("gfx/fantasy-tileset");
            NewGame();
        }

        private void NewGame()
        {
            Random rnd = new Random();
            for (int x = 0; x < _maze.GetLength(0); x++)
            {
                for (int y = 0; y < _maze.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == _maze.GetLength(0) - 1 || y == _maze.GetLength(1) - 1) { _maze[x, y] = true; }
                    else { _maze[x, y] = rnd.Next(5) == 0; }

                }
            }

            _maze[1, 1] = false;
            _maze[1, 2] = false;
            _maze[2, 1] = false;
            _maze[2, 2] = false;
            _playerPosition = Vector2.One;
        }

        #endregion

        #region Update and related
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
        }

        private void _danceMat_ButtonStateChanged(object sender, DanceMatEventArgs e)
        {
            if (e.Action != DanceMat.DanceMatButtonAction.Pressed) { return; }

            switch (e.Button)
            {
                case DanceMat.DanceMatButton.Start:
                    NewGame();
                    break;
                case DanceMat.DanceMatButton.Select:
                    //ToggleFullScreen();
                    break;
                case DanceMat.DanceMatButton.Circle:
                    TryMove(new Vector2(1, -1));
                    break;
                case DanceMat.DanceMatButton.Cross:
                    TryMove(new Vector2(-1, -1));
                    break;
                case DanceMat.DanceMatButton.Square:
                    TryMove(new Vector2(1, 1));
                    break;
                case DanceMat.DanceMatButton.Triangle:
                    TryMove(new Vector2(-1, 1));
                    break;
                case DanceMat.DanceMatButton.Right:
                    TryMove(Vector2.UnitX);
                    break;
                case DanceMat.DanceMatButton.Up:
                    TryMove(-Vector2.UnitY);
                    break;
                case DanceMat.DanceMatButton.Down:
                    TryMove(Vector2.UnitY);
                    break;
                case DanceMat.DanceMatButton.Left:
                    TryMove(-Vector2.UnitX);
                    break;
                default:
                    break;
            }

        }

        private void TryMove(Vector2 movement)
        {
            var newPosition = _playerPosition + movement;
            if (newPosition.X >= 0 && newPosition.X < _maze.GetLength(0) &&
                newPosition.Y >= 0 && newPosition.Y < _maze.GetLength(1) &&
                !_maze[(int)newPosition.X, (int)newPosition.Y])
            {
                _playerPosition = newPosition;
            }
        }
        #endregion

        #region Draw and related
        protected override void Draw(GameTime gameTime)
        {
            Rectangle floor = new Rectangle(4 * _tileSize, 3 * _tileSize, _tileSize, _tileSize);
            Rectangle wall = new Rectangle(2 * _tileSize, 2 * _tileSize, _tileSize, _tileSize);
            Rectangle player = new Rectangle(2 * _tileSize, 18 * _tileSize, _tileSize, _tileSize);

            GraphicsDevice.Clear(Color.Black);
            Color tile = Color.Red;
            Color empty = Color.Gray;
            _spriteBatch.Begin();
            for (int x = 0; x < _maze.GetLength(0); x++)
            {
                for (int y = 0; y < _maze.GetLength(1); y++)
                {
                    _spriteBatch.Draw(_tileset, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), _maze[x, y] ? wall : floor, _maze[x, y] ? tile : empty);

                }
            }
            Color colorToUse = gameTime.TotalGameTime.Milliseconds / 250 % 2 == 0 ? Color.White : Color.CornflowerBlue;
            _spriteBatch.Draw(_tileset, new Rectangle((int)_playerPosition.X * _tileSize - 2, (int)_playerPosition.Y * _tileSize - 2, _tileSize + 4, _tileSize + 4), player, Color.Black);
            _spriteBatch.Draw(_tileset, new Rectangle((int)_playerPosition.X * _tileSize, (int)_playerPosition.Y * _tileSize, _tileSize, _tileSize), player, colorToUse);

            _spriteBatch.End();
        } 
        #endregion
    }
}