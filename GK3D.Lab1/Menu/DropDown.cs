using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GK3D.Lab1.Menu
{
    class DropDown : IMenuObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        SpriteFont _font;

        public List<string> Items { get; set; }

        public string Content { get; set; }

        public event EventHandler OnValueChanged;

        public int SelectedIndex { get; set; }

        protected virtual void ValueChanged(EventArgs e)
        {
            EventHandler handler = OnValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public DropDown(string content, SpriteFont font)
        {
            _font = font;
            Content = content;
            Items = new List<string>();
            SelectedIndex = 0;
        }

        public void AddItem(string item)
        {
            Items.Add(item);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, int width, int height)
        {
        }

        public void Update()
        {
        }
    }
}
