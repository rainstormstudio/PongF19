using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PongF19
{
    public class Ball {
        Texture2D _texture;
        Vector2 _position;
        Rectangle _srcRect;
        Vector2 _velocity;
        const float VC = 200;

        private Collider _collider;
        
        public Ball(Texture2D texture, Rectangle srcRect) {
            _texture = texture;
            _srcRect = srcRect;

            reset();
            _velocity = VC * Vector2.Normalize(new Vector2(5, 2));

            _collider = null;
        }

        public void getCollider(Collider collider) {
            _collider = collider;
        }

        public Vector2 position() {
            return _position;
        }

        public Collider collider() {
            return _collider;
        }

        public void reset() {
            _position = new Vector2(200 - 4, 170 - 4);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public void update(float deltaTime) {
            if (_collider.collided != null) {
                if (_collider.rigid && _collider.collided.rigid) {
                    Vector2 normal = _collider.collisionNormal;
                    normal = normal / normal.Length();
                    _velocity = _velocity - 2 * (Vector2.Dot(_velocity, normal)) * normal;
                    _velocity = _velocity / _velocity.Length();
                    _velocity *= VC;
                }
            }
            _position += deltaTime * _velocity;
            _collider.Update(_position, _velocity);
        }
    }
}