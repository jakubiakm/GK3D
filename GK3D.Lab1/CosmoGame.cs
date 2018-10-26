using GK3D.Lab1.Prymitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GK3D.Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CosmoGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<SceneObject> sceneObjects = new List<SceneObject>();

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800 / 480f, 0.1f, 100f);

        public CosmoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth *= 2;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight *= 2;  // set this value to the desired height of your window            
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var satellite = new Satellite();
            var satellite2 = new Satellite();
            var planetoid = new Sphere(2, 25);
            var iglo = new Hemisphere(4f, 33);

            satellite.Initialize(Color.BlanchedAlmond, 0, -5, 5, 1);
            satellite2.Initialize(Color.BurlyWood, 0, 10, -5, 1);
            planetoid.Initialize(graphics.GraphicsDevice, new Color(188, 143, 143), 0, 3, 0, 0);
            iglo.Initialize(graphics.GraphicsDevice, new Color(188, 143, 143), 0, 0, 0, 0);

            sceneObjects.Add(satellite);
            sceneObjects.Add(satellite2);
            sceneObjects.Add(planetoid);
            sceneObjects.Add(iglo);

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

            sceneObjects.ForEach(sceneObject => sceneObject.LoadModel(Content));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
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

            sceneObjects.ForEach(sceneObject => sceneObject.Update(gameTime));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneObjects.ForEach(sceneObject => sceneObject.Draw(world, view, projection));

            base.Draw(gameTime);
        }
    }
}