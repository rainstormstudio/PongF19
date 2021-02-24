using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace PongF19
{
    public class GameBoard {
        Texture2D _texture;

        Ball _ball;

        Score _score1;
        Score _score2;

        public GameBoard(Texture2D texture, Ball ball) {
            _texture = texture;
            _ball = ball;
            _score1 = null;
            _score2 = null;
        }

        public void setScore1(Score score) {
            _score1 = score;
        }

        public void setScore2(Score score) {
            _score2 = score;
        }

        public void update() {
            switch (_ball.win()) {
                case 1: {
                    _score1.inc();
                    break;
                }
                case 2: {
                    _score2.inc();
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            _score1.Draw(spriteBatch);
            _score2.Draw(spriteBatch);
        }
    }
}