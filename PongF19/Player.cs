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

        int _score;
        
        public Player(Texture2D texture, Rectangle srcRect, Vector2 position) {
            _texture = texture;
            _srcRect = srcRect;

            _position = position;
            _velocity = Vector2.Zero;

            _score = 0;
        }

        public int score() {
            return _score;
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
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, _srcRect, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }
    }
}