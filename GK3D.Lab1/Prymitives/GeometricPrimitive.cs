﻿#region File Description
//-----------------------------------------------------------------------------
// GeometricPrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using GK3D.Lab1.Helpers;
using GK3D.Lab1.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GK3D.Lab1.Prymitives
{
    /// <summary>
    /// Base class for simple geometric primitive models. This provides a vertex
    /// buffer, an index buffer, plus methods for drawing the model. Classes for
    /// specific types of primitive (CubePrimitive, SpherePrimitive, etc.) are
    /// derived from this common base, and use the AddVertex and AddIndex methods
    /// to specify their geometry.
    /// 
    /// This class is borrowed from the GK3D.Lab1.Prymitives sample.
    /// </summary>
    public abstract class GeometricPrimitive : IDisposable
    {
        #region Fields
        // During the procvess of constructing a primitive model, vertex
        // and index data is stored on the CPU in these managed lists.
        List<VertexPositionNormal> vertices = new List<VertexPositionNormal>();
        List<ushort> indices = new List<ushort>();
        public bool perlinMode = false;

        // Once all the geometry has been specified, the InitializePrimitive
        // method copies the vertex and index data into these buffers, which
        // store it on the GPU ready for efficient rendering.
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        BasicEffect basicEffect;


        #endregion

        #region Initialization


        /// <summary>
        /// Adds a new vertex to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddVertex(Vector3 position, Vector3 normal)
        {
            vertices.Add(new VertexPositionNormal(position, normal));
        }


        /// <summary>
        /// Adds a new index to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddIndex(int index)
        {
            if (index > 100 * ushort.MaxValue)
                throw new ArgumentOutOfRangeException("index");

            indices.Add((ushort)index);
        }


        /// <summary>
        /// Queries the index of the current vertex. This starts at
        /// zero, and increments every time AddVertex is called.
        /// </summary>
        protected int CurrentVertex
        {
            get { return vertices.Count; }
        }


        /// <summary>
        /// Once all the geometry has been specified by calling AddVertex and AddIndex,
        /// this method copies the vertex and index data into GPU format buffers, ready
        /// for efficient rendering.
        protected void InitializePrimitive(GraphicsDevice graphicsDevice)
        {
            var rand = new Random();

            //for (int i = 0; i != vertices.Count; i++)
            //    vertices[i] = new VertexPositionNormal(new Vector3((rand.Next(-10, 10) * 0.02f) + vertices[i].Position.X, (rand.Next(-10, 10) * 0.02f) + vertices[i].Position.Y, (rand.Next(-10, 10) * 0.02f) + vertices[i].Position.Z), vertices[i].Normal);
            NoiseMaker maker = new NoiseMaker();

            if (perlinMode)
                for (int i = 0; i != vertices.Count; i++)
                {
                    float noise = NoiseMaker.Noise(vertices[i].Position.X, vertices[i].Position.Y, vertices[i].Position.Z);

                    //dodanie do pozycji
                    //vertices[i] = new VertexPositionNormal(vertices[i].Position * (1 + noise * 0.25f * (rand.NextDouble() > 0.5 ? 1 : -1)), vertices[i].Normal);

                    //wzdłóż wektora normalnego o losowym kierunku
                    Vector3 scaledVec = 0.5f * (rand.NextDouble() > 0.5 ? 1 : -1) * noise * vertices[i].Normal;
                    vertices[i] = new VertexPositionNormal(vertices[i].Position + scaledVec, vertices[i].Normal);

                    //wzdłóż wektora normalnego o losowych kierunkach
                    //Vector3 scaledVec = noise * vertices[i].Normal;
                    //vertices[i] = new VertexPositionNormal(new Vector3(vertices[i].Position.X + scaledVec.X * (rand.NextDouble() > 0.5 ? 1 : -1), vertices[i].Position.Y + scaledVec.Y * (rand.NextDouble() > 0.5 ? 1 : -1), vertices[i].Position.Z + scaledVec.Z * (rand.NextDouble() > 0.5 ? 1 : -1)), vertices[i].Normal);
                }

            // Create a vertex declaration, describing the format of our vertex data.

            // Create a vertex buffer, and copy our vertex data into it.
            vertexBuffer = new VertexBuffer(graphicsDevice,
                                            typeof(VertexPositionNormal),
                                            vertices.Count, BufferUsage.None);

            vertexBuffer.SetData(vertices.ToArray());

            // Create an index buffer, and copy our index data into it.
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort),
                                          indices.Count, BufferUsage.None);

            indexBuffer.SetData(indices.ToArray());

            // Create a basicEffect, which will be used to render the primitive.
            basicEffect = new BasicEffect(graphicsDevice);

            //basicEffect.EnableDefaultLighting();
            basicEffect.PreferPerPixelLighting = false;
        }


        /// <summary>
        /// Finalizer.
        /// </summary>
        ~GeometricPrimitive()
        {
            Dispose(false);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (vertexBuffer != null)
                    vertexBuffer.Dispose();

                if (indexBuffer != null)
                    indexBuffer.Dispose();

                if (basicEffect != null)
                    basicEffect.Dispose();
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the primitive model, using the specified basicEffect. Unlike the other
        /// Draw overload where you just specify the world/view/projection matrices
        /// and color, this method does not set any renderstates, so you must make
        /// sure all states are set to sensible values before you call it.
        /// </summary>
        public void Draw(BasicEffect basicEffect)
        {
            GraphicsDevice graphicsDevice = basicEffect.GraphicsDevice;

            // Set our vertex declaration, vertex buffer, and index buffer.
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;
            foreach (EffectPass basicEffectPass in basicEffect.CurrentTechnique.Passes)
            {
                basicEffectPass.Apply();

                int primitiveCount = indices.Count / 3;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, primitiveCount);

            }
        }


        /// <summary>
        /// Draws the primitive model, using a basicEffect shader with default
        /// lighting. Unlike the other Draw overload where you specify a custom
        /// basicEffect, this method sets important renderstates to sensible values
        /// for 3D model rendering, so you do not need to set these states before
        /// you call it.
        /// </summary>
        public void Draw(Matrix world, Color color, Camera camera, Vector3 light1Position, Vector3 light2Position, Texture2D texture, Options options, float colorIntensity = 1f)
        {
            basicEffect.World = world;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.Projection = camera.ProjectionMatrix;

            BasicEffectHelper.SetNormalBasicEffect(basicEffect, light1Position, light2Position, color, texture, options);

            // Draw the model, using basicEffect.
            Draw(basicEffect);
        }
        #endregion
    }
}