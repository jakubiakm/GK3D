using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK3D.Lab1.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GK3D.Lab1.Menu
{
    class DropDown : IMenuObject
    {
        ButtonState LastLeftMouseButtonState = ButtonState.Released;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        SpriteFont _font;

        public List<string> Items { get; set; }

        public string Content { get; set; }

        public bool IsDropped { get; set; } = false;

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
            X = x;
            Y = y;
            Width = width;
            Height = height;
            spriteBatch.DrawString(_font, $"{Content}", new Vector2(x, y), Color.Red);
            DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x, y + 20, width, height, new Color(0, 0, 0, 22));
            spriteBatch.DrawString(_font, $"{Items[SelectedIndex]}", new Vector2(x + 10, y + 30), Color.Red);
            if (IsDropped)
            {
                for(int i = 0; i != Items.Count; i++)
                {
                    DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, x, y + 20 + height * (i + 1), width, height, new Color(255, 255, 255, 222));
                    spriteBatch.DrawString(_font, $"{Items[i]}", new Vector2(x + 10, y + 30 + height * (i + 1)), Color.Black);
                }
            }
        }

        public void Update()
        {
            if (LastLeftMouseButtonState == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Point pos = Mouse.GetState().Position;
                if(IsDropped)
                {
                    for (int i = 0; i != Items.Count; i++)
                    {
                        if (pos.X >= X && pos.X <= X + Width && pos.Y >= Y + 20 + Height * (i + 1) && pos.Y <= Y + 20 + Height * (i + 2))
                        {
                            IsDropped = false;
                            SelectedIndex = i;
                            ValueChanged(null);
                        }
                    }
                }
                if (pos.X >= X && pos.X <= X + Width && pos.Y >= Y + 20 && pos.Y <= Y + 20 + Height)
                {
                    IsDropped = !IsDropped;
                    ValueChanged(null);
                }
            }
            LastLeftMouseButtonState = Mouse.GetState().LeftButton;
        }
    }
}
