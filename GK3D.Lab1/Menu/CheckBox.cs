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
    class CheckBox : IMenuObject
    {
        public string Content { get; set; }

        public bool Checked { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

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
            if (Checked)
            {
                DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x, y, height, height, new Color(0, 0, 0, 22));
                DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x + 5, y + 5, height - 10, height - 10, new Color(Color.Green, 0.5f));
            }
            else
            {
                DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x, y, height, height, new Color(0, 0, 0, 22));
            }
            spriteBatch.DrawString(_font, $"{Content}", new Vector2(x + height + 20, y + height / 4), Color.Red);
        }

        public void Update()
        {
        }

        public CheckBox(string content, SpriteFont font)
        {
            _font = font;
            Content = content;
            Checked = false;
        }
    }
}
