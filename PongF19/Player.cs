using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongF19
{
    public class Player {
        Texture2D _texture;
        Vector2 _position;
        Rectangle _srcRect;
        Vector2 _velocity;
        private Collider _collider;

        int _score;
        
        public Player(Texture2D texture, Rectangle srcRect, Vector2 position) {
            _texture = texture;
            _srcRect = srcRect;

            _position = position;
            _velocity = Vector2.Zero;

            _collider = null;
            _score = 0;
        }

        public void getCollider(Collider collider) {
            _collider = collider;
        }

        public int score() {
            return _score;
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
            _collider.Update(_position, _velocity);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }
    }
}