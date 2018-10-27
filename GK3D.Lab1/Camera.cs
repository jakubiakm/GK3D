using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GK3D.Lab1
{
    public class Camera
    {
        // We need this to calculate the aspectRatio
        // in the ProjectionMatrix property.
        GraphicsDevice graphicsDevice;

        public Vector3 position = new Vector3(0, 20, 10);

        public Vector3 rotation = new Vector3(0, 0, 0);

        float _moveSpeed;

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = new Vector3(0, -1f, -.5f);
                // We'll create a rotation matrix using our angle
                var rotationMatrix =
                    Matrix.CreateRotationX(rotation.X) *
                    Matrix.CreateRotationX(rotation.Y) *
                    Matrix.CreateRotationZ(rotation.Z);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;
                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(
                    position, lookAtVector, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice, float moveSpeed = 5)
        {
            _moveSpeed = moveSpeed;
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            var rotationMatrix =
                    Matrix.CreateRotationX(rotation.X) *
                    Matrix.CreateRotationX(rotation.Y) *
                    Matrix.CreateRotationZ(rotation.Z);
            var prevPosition = position;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                rotation.X += 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                rotation.X -= 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation.Z += 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation.Z -= 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                var forwardVector = new Vector3(0, -1, 0);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);
                
                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                var forwardVector = new Vector3(0, 1, 0);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);
                
                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                var forwardVector = new Vector3(1, 0, 0);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                var forwardVector = new Vector3(-1, 0, 0);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                var forwardVector = new Vector3(0, 0, 1);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                var forwardVector = new Vector3(0, 0, -1);

                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);
                
                this.position += forwardVector * _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
