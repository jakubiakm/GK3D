using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Menu
{
    public interface IMenuObject
    {
        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, int width, int height);

        void Update();
    }
}
