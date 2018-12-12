using GK3D.Lab1.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Menu
{
    public class Panel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public List<IMenuObject> Objects { get; set; }

        public Panel(int x, int y, int width, int height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            Objects = new List<IMenuObject>();
        }

        public void AddChild(IMenuObject item)
        {
            Objects.Add(item);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            DrawRectangleHelper.DrawRoundedRectangle(spriteBatch, graphicsDevice, X, Y, Width, Height, Color);
        }
    }
}
