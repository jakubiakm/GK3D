using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
