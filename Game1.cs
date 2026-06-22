using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended.Graphics;
using MyFirstGame.Manager;
using System.Collections.Generic;

namespace MyFirstGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;
    private readonly Dictionary<string, Texture2D> _textures = new ();
    private Texture2DAtlas _atlas;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        // let gestures fall back to mouse if desired
        TouchPanel.EnableMouseGestures = true;

        // ensure touch coordinates match your viewport
        TouchPanel.DisplayWidth = GraphicsDevice.Viewport.Width;
        TouchPanel.DisplayHeight = GraphicsDevice.Viewport.Height;

        // enable desired gestures
        TouchPanel.EnabledGestures =
              GestureType.Hold 
            | GestureType.Tap 
            | GestureType.DoubleTap
            | GestureType.FreeDrag
            | GestureType.Pinch;

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // loads content/textures/player.png into gpu memory and assigns it to _texture
        _texture = Content.Load<Texture2D>("textures/player");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        // load the packed image containing 32x32 frame
        var texture = Content.Load<Texture2D>("Textures/cards");

        // create regions for every 32x32 cell in row-major order
        _atlas = Texture2DAtlas.Create("CardsAtlas", texture, 32, 32);

        // bulk load textures
        string[] assetNames = { "Textures/player", "Textures/enemy", "Textures/background" };
        foreach (string assetName in assetNames)
        {
            _textures[assetName] = Content.Load<Texture2D>(assetName);
        }
    }

    protected override void Update(GameTime gameTime)
    {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        InputManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        // draw the 13th region (zero-based) at (100, 100)
        _spriteBatch.Draw(_atlas[12], new Vector2(100, 100), Color.White);

        _spriteBatch.Begin(
            sortMode: SpriteSortMode.Deferred,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp
            );

        // draws the player texture at (100, 150) with no rotation, no scaling
        _spriteBatch.Draw(
            texture: _texture,
            position: new Vector2(100, 150),
            sourceRectangle: null,
            color: Color.White
            //rotation: 0f,
            //origin: Vector2.Zero,
            //scale: 1f,
            //effects: SpriteEffects.None,
            //layerDepth: 0f
        );

        _spriteBatch.Draw(
            texture: _textures["Textures/enemy"],
            position: new Vector2(300, 150),
            color: Color.White
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        foreach (var texture in _textures.Values)
        {
            texture.Dispose();
        }

        _spriteBatch.Dispose();
    }
}
