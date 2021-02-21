using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace PongF19
{
    public class RectCollider : Collider {
        private Vector2 _position;
        private Vector2 _dimensions;
        private Vector2 _velocity;
        
        public RectCollider(Vector2 position, Vector2 dimensions, bool isRigid) {
            _position = position;
            _dimensions = dimensions;

            collisionNormal = Vector2.Zero;
            collisionPosition = Vector2.Zero;
            collisionTime = -1;
            rigid = isRigid;
            collided = null;
        }
        
        public override void Draw(GraphicsDevice gd, SpriteBatch spriteBatch) {
            Texture2D texture = new Texture2D(gd, 1, 1);
            texture.SetData(new Color[] {Color.White});
            Rectangle rect = new Rectangle(
                (int)Math.Round(_position.X),
                (int)Math.Round(_position.Y),
                (int)Math.Round(_dimensions.X),
                (int)Math.Round(_dimensions.Y)
            );
            if (collided == null) {
                spriteBatch.Draw(texture, rect, Color.Red);
            } else {
                spriteBatch.Draw(texture, rect, Color.Green);
            }
        }

        public override void check(float deltaTime, Collider other) {
            Vector2 hitPosition = Vector2.Zero, hitNormal = Vector2.Zero;
            float hitTime = 0.0f;
            if (rectHit(this, (RectCollider)other, ref hitPosition, ref hitNormal, ref hitTime, deltaTime)) {
                if (collisionTime < 0 || collisionTime > hitTime) {
                    collisionNormal = hitNormal;
                    collisionPosition = hitPosition;
                    collisionTime = hitTime;
                    collided = other;
                }
                other.collided = this;
            }
        }

        public static void swap<T>(ref T a, ref T b) {
            T temp = a;
            a = b;
            b = temp;
        }

        public static bool rayHit(Vector2 rayOrigin, Vector2 rayDir, ref RectCollider other, ref Vector2 hitPosition, ref Vector2 hitNormal, ref float tNearHit) {
            Vector2 tNear = (other._position - rayOrigin) / rayDir;
            Vector2 tFar = (other._position + other._dimensions - rayOrigin) / rayDir;
            if (tNear.X > tFar.X) swap<float>(ref tNear.X, ref tFar.X);
            if (tNear.Y > tFar.Y) swap<float>(ref tNear.Y, ref tFar.Y);
            if (tNear.X > tFar.Y || tNear.Y > tFar.X) return false;
            tNearHit = Math.Max(tNear.X, tNear.Y);
            float tFarHit = Math.Min(tFar.X, tFar.Y);
            if (tFarHit < 0) return false;

            hitPosition = rayOrigin + tNearHit * rayDir;

            if (tNear.X > tNear.Y) {
                if (rayDir.X < 0) {
                    hitNormal.X = 1;
                    hitNormal.Y = 0;
                } else {
                    hitNormal.X = -1;
                    hitNormal.Y = 0;
                }
            } else if (tNear.X < tNear.Y) {
                if (rayDir.Y < 0) {
                    hitNormal.X = 0;
                    hitNormal.Y = 1;
                } else {
                    hitNormal.X = 0;
                    hitNormal.Y = -1;
                }
            }

            return true;
        }

        public static bool rectHit(RectCollider collider1, RectCollider collider2, ref Vector2 hitPosition, ref Vector2 hitNormal, ref float hitTime, float deltaTime) {
            if (collider1._velocity == Vector2.Zero) return false;
            RectCollider temp = new RectCollider(
                (collider2._position - collider1._dimensions / 2) + collider2._velocity * deltaTime,
                collider2._dimensions + collider1._dimensions,
                false
            );
            if (rayHit(
                    collider1._position + collider1._dimensions / 2,
                    collider1._velocity * deltaTime,
                    ref temp,
                    ref hitPosition,
                    ref hitNormal,
                    ref hitTime)
                ) 
            {
                if (hitTime <= 1.0f) return true;
            }
            return false;
        }

        public override void Update(Vector2 position, Vector2 velocity)
        {
            _position = position;
            _velocity = velocity;
        }
    }
}