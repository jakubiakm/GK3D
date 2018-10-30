using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GK3D.Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CosmoGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<SceneObject> sceneObjects = new List<SceneObject>();

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        Camera camera;

        public CosmoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth *= 2;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight *= 2;  // set this value to the desired height of your window            
            graphics.IsFullScreen = false;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var satellites = new Satellite();
            var tree = new Tree();
            var bison = new Bison();

            var sun = new Sphere(100f, 50);
            var planetoid = new Sphere(5.5f, 100);
            var researchStationHemisphere = new Hemisphere(1, 100);
            var researchStationHemicylinder = new Hemicylinder(0.5f, 100, 0.25f);
            
            satellites.Initialize(new Color(86, 125, 155), 0,
                new Vector3(-5, 5, 1), new Vector3(0, 0, -MathHelper.PiOver4),
                new Vector3(0.1f, 0.1f, 0.1f));
            satellites.Initialize(new Color(155, 123, 86), 0,
                new Vector3(10, -5, 1), new Vector3(0, MathHelper.Pi, 0),
                new Vector3(0.1f, 0.1f, 0.1f));
            planetoid.Initialize(graphics.GraphicsDevice, new Color(0, 0.21f, 0), 0,
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            researchStationHemisphere.Initialize(graphics.GraphicsDevice, new Color(179, 204, 255), 0,
                new Vector3(0, 2.65f, 0), new Vector3(-MathHelper.PiOver2, 0, 0));
            researchStationHemicylinder.Initialize(graphics.GraphicsDevice, new Color(179, 204, 255), 0,
                new Vector3(-0.4f, 2.65f, 0), new Vector3(-0.25f, MathHelper.PiOver2, MathHelper.PiOver2),
                new Vector3(1, 1, 1));
            tree.Initialize(Color.DarkGreen, 0,
                new Vector3(1, 2.5f, 0), new Vector3(0, 0, -MathHelper.PiOver4 / 2),
                new Vector3(0.007f, 0.007f, 0.007f));
            bison.Initialize(new Color(115, 77, 38), 0,
                new Vector3(-1, 2.328f, 1.5f), new Vector3(-0.35f, 2.6f, MathHelper.PiOver4 / 2),
                new Vector3(0.65f, 0.65f, 0.65f));
            sun.Initialize(graphics.GraphicsDevice, Color.Yellow, 0,
                new Vector3(0, 1000, 0), new Vector3(0, 0, 0));

            sceneObjects.Add(satellites);
            sceneObjects.Add(planetoid);
            sceneObjects.Add(researchStationHemisphere);
            sceneObjects.Add(researchStationHemicylinder);
            sceneObjects.Add(tree);
            sceneObjects.Add(bison);
            sceneObjects.Add(sun);

            AddStarsToScene();

            camera = new Camera(graphics.GraphicsDevice);

            base.Initialize();
        }

        void AddStarsToScene()
        {
            Random rand = new Random();
            for(int i = 0; i != 2000; i++)
            {
                var xRand = rand.NextDouble() > 0.5 ? 1 : -1;
                var yRand = rand.NextDouble() > 0.5 ? 1 : -1;
                var zRand = rand.NextDouble() > 0.5 ? 1 : -1;

                var starPosition = new Vector3(xRand * (40 * (float)rand.NextDouble()), yRand * (40 * (float)rand.NextDouble()), zRand * (40 * (float)rand.NextDouble()));
                var changed = false;
                while (!changed)
                {
                    if (rand.NextDouble() > 0.5)
                    {
                        starPosition.X += 50 * xRand;
                        changed = true;
                    }
                    if (rand.NextDouble() > 0.5)
                    {
                        starPosition.Y += 50 * yRand;
                        changed = true;
                    }
                    if (rand.NextDouble() > 0.5)
                    {
                        starPosition.Z += 50 * zRand;
                        changed = true;
                    }
                }
                var star = new Sphere(0.1f, 3);
                star.Initialize(graphics.GraphicsDevice, Color.White, 0,
                    starPosition, new Vector3(0, 0, 0));
               
                sceneObjects.Add(star);
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Position");
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
            camera.Update(gameTime);
          
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

            var satelliter = (Satellite)sceneObjects.First(o => o.GetType() == typeof(Satellite));

            sceneObjects.ForEach(sceneObject => sceneObject.Draw(world, camera,
                satelliter.PositionVectors[0], satelliter.PositionVectors[1]));

            DrawDebugInformation();

            base.Draw(gameTime);
        }

        private void DrawDebugInformation()
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, $"Camera position:{camera.Position}", new Vector2(20, 5), Color.Red);
            spriteBatch.DrawString(font, $"Camera direction:{camera.Direction}", new Vector2(20, 25), Color.Red);
            spriteBatch.DrawString(font, $"Camera up:{camera.Up}", new Vector2(20, 45), Color.Red);

            spriteBatch.End();
        }
    }
}