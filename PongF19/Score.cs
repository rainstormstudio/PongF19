using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongF19
{
    public class Score {
        const int FONT_WIDTH = 3;
        const int FONT_HEIGHT = 5;
        const int SCALE = 5;

        Texture2D _texture;
        Vector2 _position;

        Rectangle _srcRect1;
        Rectangle _srcRect2;

        private int _value;
        
        public Score(Texture2D texture, Vector2 position) {
            _texture = texture;
            _position = position;
            _position.X -= (FONT_WIDTH * SCALE + 20) / 2;
            _srcRect1 = new Rectangle(0, 0, FONT_WIDTH, FONT_HEIGHT);
            _srcRect2 = new Rectangle(0, 0, FONT_WIDTH, FONT_HEIGHT);
            _value = 0;
        }

        public void inc() {
            _value ++;
            _srcRect1.X = (_value / 10 % 10) % 5 * FONT_WIDTH;
            _srcRect1.Y = (_value / 10 % 10) / 5 * FONT_HEIGHT;
            _srcRect2.X = (_value % 10) % 5 * FONT_WIDTH;
            _srcRect2.Y = (_value % 10) / 5 * FONT_HEIGHT;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Vector2 delta = new Vector2(20, 0);
            spriteBatch.Draw(_texture, _position, _srcRect1, Color.White, 0f, Vector2.Zero, new Vector2(SCALE, SCALE), SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _position + delta, _srcRect2, Color.White, 0f, Vector2.Zero, new Vector2(SCALE, SCALE), SpriteEffects.None, 0f);
        }
    }
}