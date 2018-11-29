using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK3D.Lab1.Helpers
{
    public static class BasicEffectHelper
    {
        public static void AddLightToBasicEffect(BasicEffect basicEffect, Vector3 light1Position, Vector3 light2Position)
        {
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.Direction = new Vector3(0.0f, -1000.0f, 0);
            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.0005f, 0.0005f, 0.0005f);

            basicEffect.DirectionalLight1.Enabled = true;
            basicEffect.DirectionalLight1.SpecularColor = Color.White.ToVector3();
            basicEffect.DirectionalLight1.Direction = -light1Position;
            basicEffect.DirectionalLight1.DiffuseColor = new Vector3(0.0f, 0.025f, 0.0f);

            basicEffect.DirectionalLight2.Enabled = true;
            basicEffect.DirectionalLight2.SpecularColor = Color.White.ToVector3();
            basicEffect.DirectionalLight2.Direction = -light2Position;
            basicEffect.DirectionalLight2.DiffuseColor = new Vector3(0.0f, 0.015f, 0.0f);

            basicEffect.SpecularPower = 500f;
            basicEffect.SpecularColor = Color.White.ToVector3();
            basicEffect.AmbientLightColor = new Vector3(0.0f, 0.02f, 0.0f);
            basicEffect.LightingEnabled = true;
            //basicEffect.EnableDefaultLighting();
        }
    }
}
