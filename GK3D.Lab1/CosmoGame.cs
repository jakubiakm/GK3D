using GK3D.Lab1.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GK3D.Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CosmoGame : Game
    {
        bool ShowMenu { get; set; }

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _exercise1Texture;
        Texture2D _shipTexture;
        KeyboardState _currentKeyboardState;
        KeyboardState _previousKeyboardState;

        List<SceneObject> _sceneObjects = new List<SceneObject>();
        Satellite _satellite;

        Matrix _world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        Camera _camera;

        Options Options
        {
            get
            {
                return _options;
            }
            set
            {
                foreach (var scebeObject in _sceneObjects)
                    scebeObject.Options = value;
                _options = value;
            }
        }

        Options _options;

        public CosmoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth *= 2;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight *= 2;  // set this value to the desired height of your window            
            _graphics.IsFullScreen = false;
            IsMouseVisible = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferMultiSampling = false;

        }

        public Texture2D LoadPicture(string Filename)
        {
            FileStream setStream = File.Open(Filename, FileMode.Open);
            Texture2D NewTexture = Texture2D.FromStream(_graphics.GraphicsDevice, setStream);
            setStream.Dispose();
            return NewTexture;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            _graphics.ApplyChanges();
            _camera = new Camera(_graphics.GraphicsDevice);

            _satellite = new Satellite();
            var tree = new Tree();
            var bison = new Bison();
            var airboat = new Airboat();
            _exercise1Texture = LoadPicture(@"D:\Users\syntaximus\OneDrive\Studia\Semestr 9\GK\Projekty\GK3D.Lab1\GK3D.Lab1\Content\White-Floor-Concrete-Texture.jpg");
            _shipTexture = LoadPicture(@"D:\Users\syntaximus\OneDrive\Studia\Semestr 9\GK\Projekty\GK3D.Lab1\GK3D.Lab1\Content\Ship-texture.jpg");
            var scene = new Cuboid(1, 20, 40);
            var cube1 = new Cuboid(1, 1, 1);
            var sun = new Sphere(100f, 50);
            var planetoid = new Sphere(5.5f, 100);
            var researchStationHemisphere = new Hemisphere(1, 100);
            var researchStationHemicylinder = new Hemicylinder(0.5f, 100, 0.25f);

            _satellite.Initialize(new Color(86, 125, 155), 0,
                new Vector3(-5, 5, 3), new Vector3(0, 0, -MathHelper.PiOver4),
                new Vector3(0.1f, 0.1f, 0.1f), _exercise1Texture);
            _satellite.Initialize(new Color(155, 123, 86), 0,
                new Vector3(10, -5, 1), new Vector3(0, MathHelper.Pi, 0),
                new Vector3(0.1f, 0.1f, 0.1f), _exercise1Texture);
            planetoid.Initialize(_graphics.GraphicsDevice, new Color(0, 0.21f, 0), 0,
                new Vector3(0, 0, 0), new Vector3(0, 0, 0), _exercise1Texture);
            researchStationHemisphere.Initialize(_graphics.GraphicsDevice, new Color(179, 204, 255), 0,
                new Vector3(0, 2.65f, 0), new Vector3(-MathHelper.PiOver2, 0, 0));
            researchStationHemicylinder.Initialize(_graphics.GraphicsDevice, new Color(179, 204, 255), 0,
                new Vector3(-0.4f, 2.65f, 0), new Vector3(-0.25f, MathHelper.PiOver2, MathHelper.PiOver2),
                new Vector3(1, 1, 1));
            tree.Initialize(Color.DarkGreen, 0,
                new Vector3(1, 2.5f, 0), new Vector3(0, 0, -MathHelper.PiOver4 / 2),
                new Vector3(0.007f, 0.007f, 0.007f));
            bison.Initialize(new Color(115, 77, 38), 0,
                new Vector3(-1, 2.328f, 1.5f), new Vector3(-0.35f, 2.6f, MathHelper.PiOver4 / 2),
                new Vector3(0.65f, 0.65f, 0.65f),
                _exercise1Texture);
            sun.Initialize(_graphics.GraphicsDevice, Color.Yellow, 0,
                new Vector3(0, 1000, 0), new Vector3(0, 0, 0), null);
            scene.Initialize(_graphics.GraphicsDevice, Color.SteelBlue, 0,
                new Vector3(20, 45, 10), new Vector3(0, 0, 0), _exercise1Texture);
            cube1.Initialize(_graphics.GraphicsDevice, Color.BlanchedAlmond, 0,
                new Vector3(20, 47, 10), new Vector3(0, 0, 0), _exercise1Texture);
            airboat.Initialize(Color.Wheat, 0,
                new Vector3(0, 48.5F, -10), new Vector3(0, 2, 0),
                new Vector3(0.005f, 0.005f, 0.005f), _shipTexture);
            _sceneObjects.Add(_satellite);
            _sceneObjects.Add(planetoid);
            _sceneObjects.Add(researchStationHemisphere);
            _sceneObjects.Add(researchStationHemicylinder);
            _sceneObjects.Add(tree);
            _sceneObjects.Add(bison);
            _sceneObjects.Add(sun);
            _sceneObjects.Add(scene);
            _sceneObjects.Add(cube1);
            _sceneObjects.Add(airboat);
            //AddStarsToScene();
            GraphicsDevice.PresentationParameters.MultiSampleCount = 16;
            _graphics.ApplyChanges();
            base.Initialize();
            Options = new Options
            {
                Filter = TextureFilter.Linear,
                MipMapLevelOfDetailBias = 0f
            };
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            if (_currentKeyboardState.IsKeyUp(Keys.Space) && _previousKeyboardState.IsKeyDown(Keys.Space))
            {
                ShowMenu = !ShowMenu;
            }
                if (_currentKeyboardState.IsKeyUp(Keys.Z) && _previousKeyboardState.IsKeyDown(Keys.Z))
            {
                _graphics.PreferMultiSampling = !_graphics.PreferMultiSampling;
                _graphics.ApplyChanges();
                //_graphics.ToggleFullScreen();

            }
            if (_currentKeyboardState.IsKeyUp(Keys.F) && _previousKeyboardState.IsKeyDown(Keys.F))
            {
                switch (Options.MipMapLevelOfDetailBias)
                {
                    case 0:
                        Options = new Options
                        {
                            MipMapLevelOfDetailBias = 0.25f,
                            Filter = TextureFilter.MinPointMagLinearMipLinear
                        };
                        break;
                    case 0.25f:
                        Options = new Options
                        {
                            MipMapLevelOfDetailBias = 0.5f,
                            Filter = TextureFilter.MinLinearMagPointMipLinear
                        };
                        break;
                    case 0.5f:
                        Options = new Options
                        {
                            MipMapLevelOfDetailBias = 0.75f,
                            Filter = TextureFilter.MinPointMagLinearMipLinear
                        };
                        break;
                    case 0.75f:
                        Options = new Options
                        {
                            MipMapLevelOfDetailBias = 1,
                            Filter = TextureFilter.MinLinearMagPointMipPoint
                        };
                        break;
                    case 1:
                        Options = new Options
                        {
                            MipMapLevelOfDetailBias = 0,
                            Filter = TextureFilter.MinPointMagLinearMipPoint
                        };
                        break;

                }
                _graphics.PreferMultiSampling = !_graphics.PreferMultiSampling;
                _graphics.ApplyChanges();

            }
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

            _sceneObjects.ForEach(sceneObject => sceneObject.Draw(gameTime, _world, _camera,
                _satellite.PositionVectors[0], _satellite.PositionVectors[1]));
            DrawDebugInformation();
            if (ShowMenu)
                DrawMenu();
            
            base.Draw(gameTime);
        }

        private void DrawDebugInformation()
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, $"Camera position:{_camera.Position}", new Vector2(20, 5), Color.Red);
            _spriteBatch.DrawString(_font, $"Camera direction:{_camera.Direction}", new Vector2(20, 25), Color.Red);
            _spriteBatch.DrawString(_font, $"Camera up:{_camera.Up}", new Vector2(20, 45), Color.Red);

            _spriteBatch.End();
        }

        private void DrawMenu()
        {
            float xPosition = 0.1f; //0..1
            float yPosition = 0.1f; //0..1
            float mWidth = 0.8f; //0..1
            float mHeight = 0.8f; //0..1

            int height = (int)(GraphicsDevice.Viewport.Height * mHeight);
            int width = (int)(GraphicsDevice.Viewport.Width * mWidth);

            int x = (int)(GraphicsDevice.Viewport.Width * xPosition);
            int y = (int)(GraphicsDevice.Viewport.Height * yPosition);

            _spriteBatch.Begin();
            Texture2D rect = new Texture2D(GraphicsDevice, width, height);

            Color[] data = new Color[height * width];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = new Color(Color.BurlyWood, 0.5f);
            }
            rect.SetData(data);

            Vector2 coor = new Vector2(x, y);
            _spriteBatch.Draw(rect, coor, Color.White);
            _spriteBatch.End();

        }

        void AddStarsToScene()
        {
            Random rand = new Random();
            for (int i = 0; i != 2000; i++)
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
                star.Initialize(_graphics.GraphicsDevice, Color.White, 0,
                    starPosition, new Vector3(0, 0, 0), null);

                _sceneObjects.Add(star);
            }
        }
    }
}