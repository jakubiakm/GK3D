using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.SceneObjects
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
            Filter = TextureFilter.Linear;
            MipMapLevelOfDetailBias = 0;
            Msaa = true;
            MipmapFilter = false;
            LinearMagnifierFilter = false;
            ResolutionWidth = 1600;
            ResolutionHeight = 960;
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

        public Menu(List<SceneObject> sceneObjects, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            _sceneObjects = sceneObjects;
            Options = new Options
            {
                Filter = TextureFilter.Linear,
                MipMapLevelOfDetailBias = 0f
            };
            _graphicsDeviceManager = graphicsDeviceManager;
            InitializeMenu(content);
        }

        private void InitializeMenu(ContentManager content)
        {
            UserInterface.Initialize(content, BuiltinThemes.hd);

            _filterPanel = new Panel(new Vector2(400, 300), PanelSkin.Default, Anchor.TopLeft);
            _antiAliasingPanel = new Panel(new Vector2(400, 300), PanelSkin.Default, Anchor.TopLeft);
            _resolutionPanel = new Panel(new Vector2(400, 300), PanelSkin.Default, Anchor.TopLeft);

            UserInterface.Active.AddEntity(_filterPanel);
            UserInterface.Active.AddEntity(_antiAliasingPanel);
            UserInterface.Active.AddEntity(_resolutionPanel);

            _filterLinearMagnify = new CheckBox("Magnifier linear filter");
            _filterMinmap = new CheckBox("Mipmap filter");
            _filterMinmapBias = new Slider(0, 100);
            _filterMinmapBias.Value = 0;

            _filterPanel.AddChild(new Header("Filters"));
            _filterPanel.AddChild(new HorizontalLine());
            _filterPanel.AddChild(_filterLinearMagnify);
            _filterPanel.AddChild(_filterMinmap);
            _filterPanel.AddChild(new Label("Level of details bias"));
            _filterPanel.AddChild(_filterMinmapBias);

            _antiAliasingMsaa = new CheckBox("MSAA");

            _antiAliasingPanel.AddChild(new Header("Anti aliasing"));
            _antiAliasingPanel.AddChild(new HorizontalLine());
            _antiAliasingPanel.AddChild(_antiAliasingMsaa);

            _resolution = new DropDown();
            _resolution.AddItem("1440x900");
            _resolution.AddItem("1900x1080");
            _resolution.AddItem("1600x960");

            _resolutionPanel.AddChild(new Header("Resolution"));
            _resolutionPanel.AddChild(new HorizontalLine());
            _resolutionPanel.AddChild(_resolution);

            _filterLinearMagnify.OnValueChange = (Entity e) => UpdateOptions();
            _filterMinmap.OnValueChange = (Entity e) => UpdateOptions();
            _filterMinmapBias.OnValueChange = (Entity e) => UpdateOptions();
            _resolution.OnValueChange = (Entity e) => UpdateOptions();
            _antiAliasingMsaa.OnValueChange = (Entity e) => UpdateOptions();
            UpdateOptions();
        }

        private void UpdateOptions()
        {
            TextureFilter filter = TextureFilter.Linear;
            float mipMapLevelOfDetailBias = _filterMinmapBias.Value / 25f;
            bool msaa = _antiAliasingMsaa.Checked;
            bool mipmapFilter = false;
            bool linearMagnifierFilter = false;
            int resolutionWidth = 1600;
            int resolutionHeight = 960;
            if(Options.Msaa != msaa)
            {
                _graphicsDeviceManager.PreferMultiSampling = msaa;
                var rasterizerState = new RasterizerState
                {
                    MultiSampleAntiAlias = msaa
                };
                _graphicsDeviceManager.GraphicsDevice.RasterizerState = rasterizerState;
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
                filter = TextureFilter.MinLinearMagPointMipLinear;
            }
            if (_filterLinearMagnify.Checked && !_filterMinmap.Checked)
            {
                filter = TextureFilter.MinPointMagLinearMipPoint;
            }
            if (!_filterLinearMagnify.Checked && !_filterMinmap.Checked)
            {
                filter = TextureFilter.MinLinearMagPointMipPoint;
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
            spriteBatch.Begin();
            int height = (int)(graphicsDevice.Viewport.Height * mHeightFilter);
            int width = (int)(graphicsDevice.Viewport.Width * mWidthFilter);
            int x = (int)(graphicsDevice.Viewport.Width * xPositionFilter);
            int y = (int)(graphicsDevice.Viewport.Height * yPositionFilter);
            _filterPanel.Size = new Vector2(width / 1.5f, height / 1.5f);
            _filterPanel.Offset = new Vector2(x + width / 6, y + height / 6);

            Texture2D rect = new Texture2D(graphicsDevice, width, height);

            Color[] data = new Color[height * width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int size = 20;
                    if (i < size && j < size)
                    {
                        if (Math.Sqrt((size - i) * (size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    if (i > width - size && j > height - size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if (i < size && j > height - size)
                    {
                        if (Math.Sqrt((size - i) * (size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if (i > width - size && j < size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    data[i + width * j] = new Color(Color.BurlyWood, 0.5f);
                }
            }
            rect.SetData(data);

            Vector2 coor = new Vector2(x, y);
            spriteBatch.Draw(rect, coor, Color.White);

            height = (int)(graphicsDevice.Viewport.Height * mHeightAntiAliasing);
            width = (int)(graphicsDevice.Viewport.Width * mWidthAntiAliasing);
            x = (int)(graphicsDevice.Viewport.Width * xPositionAntiAliasing);
            y = (int)(graphicsDevice.Viewport.Height * yPositionAntiAliasing);
            _antiAliasingPanel.Size = new Vector2(width / 1.5f, height / 1.5f);
            _antiAliasingPanel.Offset = new Vector2(x + width / 6, y + height / 6);

            rect = new Texture2D(graphicsDevice, width, height);

            data = new Color[height * width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int size = 20;
                    if (i < size && j < size)
                    {
                        if (Math.Sqrt((size - i) * (size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    if (i > width - size && j > height - size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if (i < size && j > height - size)
                    {
                        if (Math.Sqrt((size - i) * (size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if (i > width - size && j < size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    data[i + width * j] = new Color(Color.BurlyWood, 0.5f);
                }
            }
            rect.SetData(data);

            coor = new Vector2(x, y);
            spriteBatch.Draw(rect, coor, Color.White);

            height = (int)(graphicsDevice.Viewport.Height * mHeightResolution);
            width = (int)(graphicsDevice.Viewport.Width * mWidthResolution);
            x = (int)(graphicsDevice.Viewport.Width * xPositionResolution);
            y = (int)(graphicsDevice.Viewport.Height * yPositionResolution);
            _resolutionPanel.Size = new Vector2(width / 1.5f, height / 1.5f);
            _resolutionPanel.Offset = new Vector2(x + width / 6, y + height / 6);

            rect = new Texture2D(graphicsDevice, width, height);

            data = new Color[height * width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int size = 20;
                    if (i < size && j < size)
                    {
                        if(Math.Sqrt((size - i) * (size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    if(i > width - size && j > height - size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if(i < size && j > height - size)
                    {
                        if (Math.Sqrt((size - i) * (size - i) + (height - size - j) * (height - size - j)) > size)
                            continue;
                    }
                    if(i > width - size && j < size)
                    {
                        if (Math.Sqrt((width - size - i) * (width - size - i) + (size - j) * (size - j)) > size)
                            continue;
                    }
                    data[i + width * j] = new Color(Color.BurlyWood, 0.5f);
                }
            }
            rect.SetData(data);

            coor = new Vector2(x, y);
            spriteBatch.Draw(rect, coor, Color.White);
            spriteBatch.End();

            UserInterface.Active.Draw(spriteBatch);
        }
    }
}
