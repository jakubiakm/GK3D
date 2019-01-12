using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GK3D.Lab1.Helpers;



namespace GK3D.Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CosmoGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteFont _font;
        SpriteBatch _spriteBatch;
        KeyboardState _currentKeyboardState;
        KeyboardState _previousKeyboardState;

        List<SceneObject> _sceneObjects = new List<SceneObject>();

        Matrix _world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        Camera _camera;


        public CosmoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 800; 
            _graphics.IsFullScreen = false;
            IsMouseVisible = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferMultiSampling = true;
            _graphics.ApplyChanges();
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _camera = new Camera(_graphics.GraphicsDevice);

            var bison = new Bison();
            var cuboid = new Cuboid();

            cuboid.Initialize(Color.SkyBlue, 0,
                new Vector3(-1, 2.328f, 1.5f), new Vector3(-0.35f, 2.6f, MathHelper.PiOver4 / 2),
                new Vector3(3.65f, 0.65f, 2.65f), null);

            bison.Initialize(Color.Red, 0,
                new Vector3(1, 2f, 1.5f), new Vector3(-0.35f, 2.6f, MathHelper.PiOver4 / 2),
                new Vector3(0.65f, 0.65f, 0.65f), null);


            _sceneObjects.Add(bison);
            _sceneObjects.Add(cuboid);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Position");
            _sceneObjects.ForEach(sceneObject => sceneObject.LoadModel(Content));
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
            _currentKeyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _sceneObjects.ForEach(sceneObject => sceneObject.Update(gameTime));
            _camera.Update(gameTime);
            _previousKeyboardState = _currentKeyboardState;
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            GraphicsDevice.Clear(Color.Black);
            _sceneObjects.ForEach(sceneObject =>
            {
                sceneObject.Draw(gameTime, _world, _camera);
                var prev = GraphicsDevice.RasterizerState;
                var state = new RasterizerState()
                {
                    FillMode = FillMode.WireFrame
                };
                sceneObject.Draw(gameTime, _world, _camera);
            });

            base.Draw(gameTime);
        }
        private void DrawDebugInformation()
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, $"Camera position:{_camera.Position}", new Vector2(20, 5), Color.Red);
            _spriteBatch.DrawString(_font, $"Camera direction:{_camera.Direction}", new Vector2(20, 25), Color.Red);
            _spriteBatch.DrawString(_font, $"Ca mera up:{_camera.Up}", new Vector2(20, 45), Color.Red);

            _spriteBatch.End();
        }
    }
}