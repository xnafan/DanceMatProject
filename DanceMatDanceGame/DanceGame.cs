using DanceMatClassLibrary;
using DanceMatDanceGame.components;
using DanceMatMazeGame.components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DanceMatDanceGame;

public class DanceGame : Game
{
    #region Properties and variables
    private GraphicsDeviceManager _graphics;
    public static SpriteBatch SpriteBatch { get; set; }
    private Texture2D _arrowTexture;
    private AnimatedImage _dancingCow;
    private DanceMovesListControl _dancePatternControl;
    public DanceMat DanceMat { get; set; }
    public static SpriteFont RegularFont { get; set; }
    public static SpriteFont BigFont { get; set; }
    public int Points { get; set; }
    private string _lastPrecision;

    #endregion
    
    #region Constructor and initialization
    public DanceGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        DanceMat = new DanceMat();
    }

    protected override void Initialize()
    {

        base.Initialize();
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1024;

        // ToggleFullScreen();
        _graphics.ApplyChanges();
        _dancePatternControl = new DanceMovesListControl(_arrowTexture, new Vector2(200, 100), 128, DanceMat);
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();

        _dancePatternControl.SuccessfulDanceMove += _dancePatternControl_SuccessfulDanceMove;
    }

  
    private void ToggleFullScreen()
    {
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }
    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        _arrowTexture = Content.Load<Texture2D>("gfx/arrows");
        RegularFont = Content.Load<SpriteFont>("fonts/font");
        BigFont = Content.Load<SpriteFont>("fonts/bigfont");
        _dancingCow = new AnimatedImage(new Vector2(1300, 400), Content.Load<Texture2D>("gfx/dancecowanimation"), 200, 4, 2);
    }
    #endregion

    #region Update, Draw and related

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
        base.Update(gameTime);
        _dancePatternControl.Update(gameTime);
        _dancingCow.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        SpriteBatch.Begin();
        GraphicsDevice.Clear(Color.ForestGreen);
        // SpriteBatch.Draw(_arrowTexture, Vector2.One * 300, Color.White);
        _dancePatternControl.Draw(gameTime);
        SpriteBatch.DrawString(BigFont, $"{Points} points", new Vector2(_graphics.PreferredBackBufferWidth - 400, 50), Color.White);
        SpriteBatch.DrawString(BigFont, $"{_lastPrecision}!", new Vector2(_graphics.PreferredBackBufferWidth - 600, 150), Color.White);
        _dancingCow.Draw(gameTime);
        SpriteBatch.End();
    }

    private void _dancePatternControl_SuccessfulDanceMove(object sender, DanceMoveSuccesRate e)
    {
        Points += (int)e.TimingPrecision;
        if (Points < 0) { Points = 0; }
        _lastPrecision = e.TimingPrecision.ToString();
    }

    #endregion
}