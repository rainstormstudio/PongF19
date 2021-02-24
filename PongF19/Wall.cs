using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace PongF19
{
    public class Wall : Collider {
        Rectangle _rect;

        public IShapeF Bounds {get;}
        
        public Wall(Rectangle rect) {
            _rect = rect;

            Bounds = new RectangleF(_rect.X, _rect.Y, rect.Width, rect.Height);
        }

        public void Draw(float deltaTime, SpriteBatch spriteBatch) {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public void Update(float deltaTime) {}

        public void OnCollision(CollisionEventArgs collisionInfo) {

        }
    }
}