using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MyFirstGame.Manager;

namespace MyFirstGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;


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

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
