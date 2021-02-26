using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace PongF19
{
    public class Player : Collider {
        Texture2D _texture;
        Vector2 _position;
        public Vector2 Position {
            get { return _position; }
        }
        Rectangle _srcRect;
        public Rectangle Rect {
            get { return _srcRect; }
        }
        Vector2 _velocity;

        public IShapeF Bounds {get;}
        
        public Player(Texture2D texture, Rectangle srcRect, Vector2 position) {
            _texture = texture;
            _srcRect = srcRect;

            _position = position;
            _velocity = Vector2.Zero;

            Bounds = new RectangleF(_position.X, _position.Y, _srcRect.Width, _srcRect.Height);
        }

        public Vector2 position() {
            return _position;
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

        public void updateControl(float deltaTime, Keys upkey, Keys downkey) {
            var kstate = Keyboard.GetState();
            _velocity = Vector2.Zero;
            if (kstate.IsKeyDown(upkey)) {
                _velocity.Y = -200;
            }
            if (kstate.IsKeyDown(downkey)) {
                _velocity.Y = 200;
            }
            _position += deltaTime * _velocity;
            _position.Y = clamp(_position.Y, 60, 264);
            Bounds.Position = _position;
        }

        public void Draw(float deltaTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }

        public void Update(float deltaTime) {}

        public void OnCollision(CollisionEventArgs collisionInfo) {

        }
    }
}