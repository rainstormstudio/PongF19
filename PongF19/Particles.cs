using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;
using System;

namespace PongF19
{
    public class Particles {
        private ParticleEffect _effect;
        private Texture2D _texture;
        private Vector2 _position;

        public Particles(GraphicsDevice gd, Vector2 position) {
            _texture = new Texture2D(gd, 1, 1);
            _texture.SetData(new[] {Color.White});
            _position = position;

            TextureRegion2D textureRegion = new TextureRegion2D(_texture);
            _effect = new ParticleEffect(autoTrigger: false) {
                Position = _position,
                Emitters = new List<ParticleEmitter> {
                    new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(0.5), Profile.Point()) {
                        Parameters = new ParticleReleaseParameters {
                            Speed = new Range<float>(0f, 50f),
                            Quantity = 3,
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(3.0f, 4.0f)
                        },
                        Modifiers = {
                            new VelocityColorModifier {
                                StationaryColor = Color.White.ToHsl(),
                                VelocityColor = Color.Yellow.ToHsl(),
                                VelocityThreshold = 40
                            },
                            new RotationModifier {RotationRate = 5f},
                        }
                    }
                }
            };
        }

        public void Update(float deltaTime, Vector2 position) {
            _position = position;
            _effect.Position = _position;
            _effect.Update(deltaTime);
        }

        public void Draw(float deltaTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(_effect);
        }

        ~Particles() {
            _texture.Dispose();
            _effect.Dispose();
        }
    }
}