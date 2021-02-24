using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace PongF19
{
    public class Ball : Collider {
        Texture2D _texture;
        Vector2 _position;
        Rectangle _srcRect;
        Vector2 _velocity;
        const float VC = 200;

        private Particles _particles;

        public IShapeF Bounds {get;}
        
        public Ball(GraphicsDevice gd, Texture2D texture, Rectangle srcRect) {
            _particles = new Particles(gd, _position);
            _texture = texture;
            _srcRect = srcRect;
            Bounds = new RectangleF(_position.X, _position.Y, srcRect.Width, srcRect.Height);

            reset();
            _velocity = VC * Vector2.Normalize(new Vector2(5, 2));
        }

        ~Ball() {
            _particles = null;
        }

        public Vector2 position() {
            return _position;
        }

        public void reset() {
            _position = new Vector2(200 - 4, 170 - 4);
            Bounds.Position = _position;
        }

        public void Draw(float deltaTime, SpriteBatch spriteBatch) {
            _particles.Draw(deltaTime, spriteBatch);
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public void Update(float deltaTime) {
            _position += deltaTime * _velocity;
            Bounds.Position = _position;
            _particles.Update(deltaTime, _position + new Vector2(4, 4));
        }

        public void OnCollision(CollisionEventArgs collisionInfo) {
            Vector2 normal = collisionInfo.PenetrationVector;
            normal = normal / normal.Length();
            _velocity = _velocity - 2 * (Vector2.Dot(_velocity, normal)) * normal;
            _velocity = _velocity / _velocity.Length();
            _velocity *= VC;
            _position -= collisionInfo.PenetrationVector;
            Debug.WriteLine("collided");
        }
    }
}