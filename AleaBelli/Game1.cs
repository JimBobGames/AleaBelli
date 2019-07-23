using AleaBelli.Core.Network;
using AleaBelli.Core.Objects;
using AleaBelli.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AleaBelli
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont HeaderFont;
        SpriteFont CounterFont;
        RenderTarget2D renderTarget;
        SinglePlayerAleaBelliGame abgame = new SinglePlayerAleaBelliGame();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 768;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            /*
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            */


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                200,
                200,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            HeaderFont = Content.Load<SpriteFont>("Header"); // Use the name of your sprite font file here instead of 'Score'.
            CounterFont = Content.Load<SpriteFont>("Counter"); // Use the name of your sprite font file here instead of 'Score'.

            // load game data
            Battalion r1 = new Battalion()
            {
                BattalionId = 1,
                ShortName = "1st NY",
                MapX = 100,
                MapY = 100,
                FacingInDegrees = 45,
                BattalionType = BattalionType.LineInfantry,
                BattalionFormation = BattalionFormation.Line2,
            };
            Battalion r2 = new Battalion()
            {
                BattalionId = 2,
                ShortName = "2nd NY",
                MapX = 300,
                MapY = 150,
                FacingInDegrees = 0,
                BattalionType = BattalionType.LineInfantry,
                BattalionFormation = BattalionFormation.Line2,
            };
            Battalion r3 = new Battalion()
            {
                BattalionId = 3,
                ShortName = "1st Cav",
                MapX = 485,
                MapY = 270,
                FacingInDegrees = 300,
                BattalionType = BattalionType.LightCavalry,
                BattalionFormation = BattalionFormation.Line2,
            };
            abgame.AddBattalion(r1);
            abgame.AddBattalion(r2);
            abgame.AddBattalion(r3);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int bWidth = graphics.PreferredBackBufferWidth;
            int bHeight = graphics.PreferredBackBufferHeight;

            // draw each battalion to its texture if required
            GameRenderer.UpdateAllBattalionTextures(abgame, GraphicsDevice, spriteBatch, CounterFont);

            // Draw the scene to the screen
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, DepthStencilState.Default,
                RasterizerState.CullNone);

            GameRenderer.DrawAllBattalions(abgame, GraphicsDevice, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
