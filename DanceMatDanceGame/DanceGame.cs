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
    private Texture2D _arrowTexture, _squareTexture;
    private AnimatedImage _dancingCow;
    private DanceMovesListControl _dancePatternControl;
    public DanceMat DanceMat { get; set; }
    public static SpriteFont RegularFont { get; set; }
    public static SpriteFont BigFont { get; set; }
    public static SpriteFont GiantFont { get; set; }

    public int Points { get; set; }
    private string _lastPrecision = "";

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
        _graphics.PreferredBackBufferHeight = 1080;

         ToggleFullScreen();
        _graphics.ApplyChanges();
        _dancePatternControl = new DanceMovesListControl(_arrowTexture, new Vector2(200, 100), 192, DanceMat);
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
        GiantFont = Content.Load<SpriteFont>("fonts/giantfont");
        _squareTexture = Content.Load<Texture2D>("gfx/tile_16x16");
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
        SpriteBatch.Draw(_squareTexture, new Rectangle(180, 0, 830, _graphics.PreferredBackBufferHeight), Color.White * .1f);
        _dancePatternControl.Draw(gameTime);
        SpriteBatch.DrawString(BigFont, $"{Points} points", new Vector2(_graphics.PreferredBackBufferWidth - 575, _graphics.PreferredBackBufferHeight -150), Color.White);

        Vector2 textSize = MeasureFont(_lastPrecision.ToUpper(), GiantFont);
        SpriteBatch.DrawString(GiantFont,_lastPrecision, new Vector2(_graphics.PreferredBackBufferWidth - 370, 200) - textSize/2, Color.White);
        _dancingCow.Draw(gameTime);
        SpriteBatch.End();
    }

    private void _dancePatternControl_SuccessfulDanceMove(object sender, DanceMoveSuccesRate e)
    {
        Points += (int)e.TimingPrecision;
        if (Points < 0) { Points = 0; }
        _lastPrecision = e.TimingPrecision.ToString();
    }

    private Vector2 MeasureFont(string text, SpriteFont font)
    {
        return font.MeasureString(text);
    }

    #endregion
}