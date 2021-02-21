using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongF19
{
    public abstract class Collider {
        public Vector2 collisionNormal;
        public Vector2 collisionPosition;
        public float collisionTime;
        public bool rigid;

        public Collider collided;
        
        public abstract void Draw(GraphicsDevice gd, SpriteBatch spriteBatch);

        public abstract void check(float deltaTime, Collider other);

        public abstract void Update(Vector2 position, Vector2 velocity);
    }
}