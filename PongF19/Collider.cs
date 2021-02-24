using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;

namespace PongF19
{
    public interface Collider : ICollisionActor {
        public void Update(float deltaTime);
        public void Draw(float deltaTime, SpriteBatch spriteBatch);        
    }
}