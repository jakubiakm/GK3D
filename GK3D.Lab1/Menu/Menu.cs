using GK3D.Lab1.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Menu
{
    public class Options
    {
        public float MipMapLevelOfDetailBias { get; set; }

        public TextureFilter Filter { get; set; }

        public bool Msaa { get; set; }

        public bool LinearMagnifierFilter { get; set; }

        public bool MipmapFilter { get; set; }

        public int ResolutionWidth { get; set; }

        public int ResolutionHeight { get; set; }

        public Options()
        {
            Filter = TextureFilter.Point;
            MipMapLevelOfDetailBias = 0;
            Msaa = true;
            MipmapFilter = false;
            LinearMagnifierFilter = false;
            ResolutionWidth = 1000;
            ResolutionHeight = 600;
        }

    }

    public class Menu : SceneObject
    {
        public bool ShowMenu { get; set; } = false;

        public new Options Options
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

        GraphicsDeviceManager _graphicsDeviceManager;
        Options _options = new Options();
        KeyboardState _currentKeyboardState;
        KeyboardState _previousKeyboardState;
        List<SceneObject> _sceneObjects;


        //KONFIGURACJA PROSTOKĄTA FILTRÓW
        float xPositionFilter = 0.05f;
        float yPositionFilter = 0.05f;
        float mWidthFilter = 0.4f;
        float mHeightFilter = 0.4f;

        //KONFIGURACJA PROSTOKĄTA ANTYALIASINGU
        float xPositionAntiAliasing = 0.70f;
        float yPositionAntiAliasing = 0.05f;
        float mWidthAntiAliasing = 0.25f;
        float mHeightAntiAliasing = 0.3f;

        //KONFIGURACJA PROSTOKĄTA ROZDZIELCZOŚCI
        float xPositionResolution = 0.55f;
        float yPositionResolution = 0.55f;
        float mWidthResolution = 0.4f;
        float mHeightResolution = 0.4f;

        Panel _filterPanel;
        Panel _antiAliasingPanel;
        Panel _resolutionPanel;

        CheckBox _antiAliasingMsaa;
        DropDown _resolution;
        CheckBox _filterLinearMagnify;
        CheckBox _filterMinmap;
        Slider _filterMinmapBias;

        public Menu(List<SceneObject> sceneObjects, ContentManager content, GraphicsDeviceManager graphicsDeviceManager, SpriteFont font)
        {
            _sceneObjects = sceneObjects;
            Options = new Options
            {
                Filter = TextureFilter.Point,
                MipMapLevelOfDetailBias = 0f
            };
            _graphicsDeviceManager = graphicsDeviceManager;
            InitializeMenu(content, font);
        }

        private void InitializeMenu(ContentManager content, SpriteFont font)
        {
            _filterPanel = new Panel(0, 0, 400, 300, new Color(Color.BurlyWood, 0.5f));
            _antiAliasingPanel = new Panel(0, 0, 400, 300, new Color(Color.BurlyWood, 0.5f));
            _resolutionPanel = new Panel(0, 0, 400, 300, new Color(Color.BurlyWood, 0.5f));

            _filterLinearMagnify = new CheckBox("Magnifier linear filter", font);
            _filterMinmap = new CheckBox("Mipmap filter", font);
            _filterMinmapBias = new Slider("Level of details bias", 0, 0, 100, font);

            _filterPanel.AddChild(_filterLinearMagnify);
            _filterPanel.AddChild(_filterMinmap);
            _filterPanel.AddChild(_filterMinmapBias);

            _antiAliasingMsaa = new CheckBox("MSAA", font);
            _antiAliasingMsaa.Checked = true;

            _antiAliasingPanel.AddChild(_antiAliasingMsaa);

            _resolution = new DropDown("Resolution", font);
            _resolution.AddItem("1440x900");
            _resolution.AddItem("1900x1080");
            _resolution.AddItem("1600x960");

            _resolutionPanel.AddChild(_resolution);

            _filterLinearMagnify.OnValueChanged += UpdateOptions;
            _filterMinmap.OnValueChanged += UpdateOptions;
            _filterMinmapBias.OnValueChanged += UpdateOptions;
            _resolution.OnValueChanged += UpdateOptions;
            _antiAliasingMsaa.OnValueChanged += UpdateOptions;
            UpdateOptions(null, null);
        }

        private void UpdateOptions(object sender, EventArgs e)
        {
            TextureFilter filter = TextureFilter.Point;
            float mipMapLevelOfDetailBias = _filterMinmapBias.Value / 25f;
            bool msaa = _antiAliasingMsaa.Checked;
            bool mipmapFilter = false;
            bool linearMagnifierFilter = false;
            int resolutionWidth = 1000;
            int resolutionHeight = 600;
            if (Options.Msaa != msaa)
            {
                _graphicsDeviceManager.PreferMultiSampling = msaa;
                _graphicsDeviceManager.ApplyChanges();
            }

            switch (_resolution.SelectedIndex)
            {
                case 0:
                    resolutionHeight = 900;
                    resolutionWidth = 1440;
                    break;
                case 1:
                    resolutionHeight = 1080;
                    resolutionWidth = 1900;
                    break;
                case 2:
                    resolutionHeight = 960;
                    resolutionWidth = 1600;
                    break;
            }

            if (_graphicsDeviceManager.PreferredBackBufferWidth != resolutionWidth
               || _graphicsDeviceManager.PreferredBackBufferHeight != resolutionHeight)
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = resolutionWidth;
                _graphicsDeviceManager.PreferredBackBufferHeight = resolutionHeight;
                _graphicsDeviceManager.ApplyChanges();
            }
            if (_filterLinearMagnify.Checked && _filterMinmap.Checked)
            {
                filter = TextureFilter.MinPointMagLinearMipLinear;
            }
            if (!_filterLinearMagnify.Checked && _filterMinmap.Checked)
            {
                filter = TextureFilter.PointMipLinear;
            }
            if (_filterLinearMagnify.Checked && !_filterMinmap.Checked)
            {
                filter = TextureFilter.MinPointMagLinearMipPoint;
            }
            if (!_filterLinearMagnify.Checked && !_filterMinmap.Checked)
            {
                filter = TextureFilter.Point;
            }
            Options = new Options()
            {
                Filter = filter,
                LinearMagnifierFilter = linearMagnifierFilter,
                MipmapFilter = mipmapFilter,
                MipMapLevelOfDetailBias = mipMapLevelOfDetailBias,
                Msaa = msaa,
                ResolutionHeight = resolutionHeight,
                ResolutionWidth = resolutionWidth
            };
        }

        public override void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();
            if (_currentKeyboardState.IsKeyUp(Keys.Space) && _previousKeyboardState.IsKeyDown(Keys.Space))
            {
                ShowMenu = !ShowMenu;
            }
            _antiAliasingPanel.Update();
            _filterPanel.Update();
            _resolutionPanel.Update();
            _previousKeyboardState = _currentKeyboardState;
            base.Update(gameTime);
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            if (ShowMenu)
            {
                DrawMenu(graphicsDevice, spriteBatch);
            }
        }

        private void DrawMenu(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _filterPanel.Height = (int)(graphicsDevice.Viewport.Height * mHeightFilter);
            _filterPanel.Width = (int)(graphicsDevice.Viewport.Width * mWidthFilter);
            _filterPanel.X = (int)(graphicsDevice.Viewport.Width * xPositionFilter);
            _filterPanel.Y = (int)(graphicsDevice.Viewport.Height * yPositionFilter);
            _filterPanel.Draw(spriteBatch, graphicsDevice);

            _antiAliasingPanel.Height = (int)(graphicsDevice.Viewport.Height * mHeightAntiAliasing);
            _antiAliasingPanel.Width = (int)(graphicsDevice.Viewport.Width * mWidthAntiAliasing);
            _antiAliasingPanel.X = (int)(graphicsDevice.Viewport.Width * xPositionAntiAliasing);
            _antiAliasingPanel.Y = (int)(graphicsDevice.Viewport.Height * yPositionAntiAliasing);
            _antiAliasingPanel.Draw(spriteBatch, graphicsDevice);

            _resolutionPanel.Height = (int)(graphicsDevice.Viewport.Height * mHeightResolution);
            _resolutionPanel.Width = (int)(graphicsDevice.Viewport.Width * mWidthResolution);
            _resolutionPanel.X = (int)(graphicsDevice.Viewport.Width * xPositionResolution);
            _resolutionPanel.Y = (int)(graphicsDevice.Viewport.Height * yPositionResolution);
            _resolutionPanel.Draw(spriteBatch, graphicsDevice);
            spriteBatch.End();
        }
    }
}
