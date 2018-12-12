using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Lab1.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Lab1.Menu
{
    class Slider : IMenuObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Content { get; set; }

        public int Value { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        SpriteFont _font;

        public event EventHandler OnValueChanged;

        protected virtual void ValueChanged(EventArgs e)
        {
            EventHandler handler = OnValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            spriteBatch.DrawString(_font, $"{Content}", new Vector2(x, y), Color.Red);
            DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x, y + 20 + height / 2, width, height / 10, new Color(0, 0, 0, 244));
            float percent = (float)(Value - Min) / (Max - Min);
            int sliderDragXPosition = x + 10 + (int)(percent * (Width));
            DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, sliderDragXPosition, y + 20, 10, height, new Color(0, 0, 0, 244));

        }

        public void Update()
        {
        }

        public Slider(string content, int value, int min, int max, SpriteFont font)
        {
            _font = font;
            Content = content;
            Value = value;
            Min = min;
            Max = max;
        }
    }
}
