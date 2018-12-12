using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Helpers
{
    public static class DrawRectangleHelper
    {
        public static void DrawRoundedRectangle(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, int width, int height, Color color)
        {
            var rect = new Texture2D(graphicsDevice, width, height);

            var data = new Color[height * width];
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
                    data[i + width * j] = color;
                }
            }
            rect.SetData(data);

            var coor = new Vector2(x, y);
            spriteBatch.Draw(rect, coor, Color.White);
        }
    }
}
