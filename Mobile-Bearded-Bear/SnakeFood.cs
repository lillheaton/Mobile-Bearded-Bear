using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Mobile_Bearded_Bear
{
    public class SnakeFood
    {
        public List<Vector2> Food { get; private set; } 

        private TimeSpan lastUpdateTime;
        private TimeSpan updatesPerMilliseconds;
        private Random random;
        private Viewport viewport;
        private Texture2D foodTexture;

        public SnakeFood(Texture2D foodTexture, Viewport viewport)
        {
            this.foodTexture = foodTexture;
            this.viewport = viewport;
            updatesPerMilliseconds = TimeSpan.FromSeconds(6);
            Food = new List<Vector2>();
            random = new Random(DateTime.Now.Millisecond);
        }

        public void Update(GameTime gameTime)
        {
            lastUpdateTime += gameTime.ElapsedGameTime;
            if (lastUpdateTime > updatesPerMilliseconds)
            {
                lastUpdateTime -= updatesPerMilliseconds;
                Food.Add(new Vector2(random.Next(0, viewport.Width / Snake.SnakeBodySize), random.Next(0, viewport.Height / Snake.SnakeBodySize)));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var food in Food)
            {
                var foodViewVector = Vector2.Multiply(food, Snake.SnakeBodySize);

                spriteBatch.Draw(
                    this.foodTexture,
                    new Rectangle((int)foodViewVector.X, (int)foodViewVector.Y, Snake.SnakeBodySize, Snake.SnakeBodySize),
                    Color.Black);
            }
        }
    }
}