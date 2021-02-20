using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongF19
{
    public class GameBoard {
        Texture2D _texture;

        Player _player1;
        Player _player2;
        Score _score1;
        Score _score2;

        public GameBoard(Texture2D texture, Player player1, Player player2) {
            _texture = texture;
            _player1 = player1;
            _player2 = player2;
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
            _score1.update(_player1.score());
            _score2.update(_player2.score());
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            _score1.Draw(spriteBatch);
            _score2.Draw(spriteBatch);
        }
    }
}