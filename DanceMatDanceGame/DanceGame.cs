using DanceMatClassLibrary;
using DanceMatMazeGame.components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using drawing = System.Drawing;

namespace DanceMatDanceGame;

public class DanceGame : Game
{
    private GraphicsDeviceManager _graphics;
    public static SpriteBatch SpriteBatch { get; set; }
    private Texture2D _arrowTexture;
    private DancePatternControl _dancePatternControl;
    public DanceMat DanceMat { get; set; }


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
        
        //ToggleFullScreen();
        _graphics.ApplyChanges();
        _dancePatternControl = new DancePatternControl(_arrowTexture, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 100), new drawing.Size(1024, 256), DanceMat);
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();
        _dancePatternControl.AddRandomMove();
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
       
        
    }



    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
        base.Update(gameTime);
        _dancePatternControl.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        SpriteBatch.Begin();
        GraphicsDevice.Clear(Color.Navy);
        // SpriteBatch.Draw(_arrowTexture, Vector2.One * 300, Color.White);
        _dancePatternControl.Draw(gameTime);
        SpriteBatch.End();
    }
}
