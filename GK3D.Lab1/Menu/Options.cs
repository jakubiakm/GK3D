using Microsoft.Xna.Framework.Graphics;
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

        public Options()
        {
            Filter = TextureFilter.Linear;
            MipMapLevelOfDetailBias = 0;
        }

    }
}
