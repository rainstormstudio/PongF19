using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace PongF19
{
    public class Ball : Collider {
        Texture2D _texture;
        Vector2 _position;
        Rectangle _srcRect;
        Vector2 _velocity;
        const float VC = 200;

        private Particles _particles;

        private int _win;

        Random _random;

        public IShapeF Bounds {get;}
        
        public Ball(GraphicsDevice gd, Texture2D texture, Rectangle srcRect) {
            _particles = new Particles(gd, _position);
            _texture = texture;
            _srcRect = srcRect;
            Bounds = new RectangleF(_position.X, _position.Y, srcRect.Width, srcRect.Height);
            _random = new Random();

            _velocity = Vector2.Zero;
            reset();
            _win = 0;
        }

        ~Ball() {
            _particles = null;
        }

        public Vector2 position() {
            return _position;
        }

        public void reset() {
            _position = new Vector2(200 - 4, 170 - 4);
            _velocity.X = 0;
            _velocity.Y = 1;
            if (_random.Next(0, 2) == 0) {
                _velocity.Y = -1;
            }
            _velocity = _velocity.Rotate(_random.NextSingle(0.3333f, 0.6667f) * (float)Math.PI);
            _velocity = VC * _velocity / _velocity.Length();
            Bounds.Position = _position;
        }

        public void Draw(float deltaTime, SpriteBatch spriteBatch) {
            _particles.Draw(deltaTime, spriteBatch);
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public void Update(float deltaTime) {
            _win = 0;
            _position += deltaTime * _velocity;
            if (_position.X < 5) {
                reset();
                _win = 2;
            } else if (_position.X > 387) {
                reset();
                _win = 1;
            }
            Bounds.Position = _position;
            _particles.Update(deltaTime, _position + new Vector2(4, 4));
        }

        public int win() {
            return _win;
        }

        private float clamp(float value, float min, float max) {
            if (value < min) {
                return min;
            }
            if (value > max) {
                return max;
            }
            return value;
        }

        public void OnCollision(CollisionEventArgs collisionInfo) {
            _position -= collisionInfo.PenetrationVector;
            Vector2 normal = -collisionInfo.PenetrationVector;
            normal = normal / normal.Length();
            if (collisionInfo.Other is Player && (normal.Y <= 0.001f)) {
                Player p = (Player)collisionInfo.Other;
                float range = (float)p.Rect.Height;
                float value = _position.Y + _srcRect.Height / 2 - p.Position.Y;
                value = clamp(value, 0.0f, range);
                if (normal == new Vector2(1, 0)) {
                    _velocity.Y = -1;
                } else {
                    _velocity.Y = 1;
                    value = range - value;
                }
                _velocity.X = 0;
                float angle = (value / range) * 150f + 15;
                _velocity = _velocity.Rotate(angle * (float)Math.PI / 180f);
                _velocity = _velocity / _velocity.Length();
                _velocity *= VC;
            } else {
                _velocity = _velocity - 2 * (Vector2.Dot(_velocity, normal)) * normal;
                _velocity = _velocity / _velocity.Length();
                _velocity *= VC;
            }
        }
    }
}