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

        public Vector3 position = new Vector3(0f, 15f, 7f);

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
                                Matrix.CreateRotationY(rotation.Y) *
                                Matrix.CreateRotationZ(rotation.Z);
                                //Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), rotation.X) *
                                //Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), rotation.Y) *
                                //Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), rotation.Z);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;
                //var upVector = Vector3.UnitZ;

                Matrix ypr = Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);
                Vector3 up = Vector3.Transform(Vector3.Up, ypr);

                return Matrix.CreateLookAt(
                    position, lookAtVector, up);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, graphicsDevice.Viewport.AspectRatio, nearClipPlane, farClipPlane);
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
                Matrix.CreateRotationY(rotation.Y) *
                Matrix.CreateRotationZ(rotation.Z);
                //Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), rotation.X) *
                //Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), rotation.Y) *
                //Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), rotation.Z);
            var prevPosition = position;
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                rotation.X -= _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                rotation.X += _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                rotation.Z -= _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                rotation.Z += _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                rotation.Y -= _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                rotation.Y += _moveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds / 8;
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
