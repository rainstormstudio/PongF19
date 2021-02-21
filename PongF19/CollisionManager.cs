using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace PongF19
{
    public class CollisionManager {
        List<Collider> colliders;

        public CollisionManager() {
            colliders = new List<Collider>();
        }

        public Collider createRectCollider(Vector2 position, Vector2 dimensions, bool isRigid) {
            RectCollider collider = new RectCollider(position, dimensions, isRigid);
            colliders.Add(collider);
            return collider;
        }

        public void resetAll() {
            foreach (var collider in colliders) {
                collider.collided = null;
                collider.collisionNormal = Vector2.Zero;
                collider.collisionPosition = Vector2.Zero;
                collider.collisionTime = -1;
            }
        }

        public void Update(float deltaTime, Collider collider) {
            foreach (var obj in colliders) {
                if (obj == collider) continue;
                collider.check(deltaTime, obj);
            }
        }
        
        public void Draw(GraphicsDevice gd, SpriteBatch spriteBatch) {
            foreach (var collider in colliders) {
                collider.Draw(gd, spriteBatch);
            }
        }
    }
}