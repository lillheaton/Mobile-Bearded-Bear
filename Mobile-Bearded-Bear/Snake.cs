using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Mobile_Bearded_Bear
{
    public class Snake
    {
        // Settings
        private const int SnakeBodySize = 100;
        private const float RespawnMs = 1000.0f;

        // Booleans
        private bool hasMoved = false;
        public bool Dead { get { return this.deathCounter.Ticks > 0; } }

        // Variables
        private List<Vector2> bodyParts;
        private Direction currentDirection;
        private Direction nextDirection;
        private Texture2D snakeTexture;
        private TimeSpan lastUpdateTime;
        private TimeSpan updatesPerMilliseconds;
        private TimeSpan deathCounter;

        public Snake(Texture2D texture)
        {
            this.snakeTexture = texture;
            this.updatesPerMilliseconds = TimeSpan.FromMilliseconds(300);

            this.Init(new Vector2(0, 0), Direction.East);
        }

        private void Init(Vector2 start, Direction direction)
        {
            this.bodyParts = new List<Vector2>();
            this.currentDirection = direction;
            this.nextDirection = direction;

            this.CreateBody(start, direction);
        }

        private void CreateBody(Vector2 start, Direction direction)
        {
            var currentVec = start;
            for (int i = 0; i < 10; i++)
            {
                this.bodyParts.Add(currentVec);
                currentVec += direction.GetVector();
            }

            this.bodyParts.Reverse();
        }

        public void Update(GameTime gameTime)
        {
            UpdateInput();
            CheckCollisions();

            if (Dead)
            {
                this.deathCounter -= gameTime.ElapsedGameTime;
                if (deathCounter.Ticks <= 0)
                {
                    this.Init(new Vector2(0, 4), Direction.East);
                }
            }
            else
            {
                lastUpdateTime += gameTime.ElapsedGameTime;
                if (lastUpdateTime > updatesPerMilliseconds)
                {
                    this.UpdatePosition();
                    lastUpdateTime -= updatesPerMilliseconds;
                }    
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int margin = 1;
            var rect = new Rectangle();
            rect.Width = SnakeBodySize - (margin * 2);
            rect.Height = SnakeBodySize - (margin * 2);
            
            foreach (var snakePart in this.bodyParts)
            {
                rect.X = (int)snakePart.X * SnakeBodySize + margin;
                rect.Y = (int)snakePart.Y * SnakeBodySize + margin;

                spriteBatch.Draw(snakeTexture, rect, Color.Red);
            }
        }

        private void UpdatePosition()
        {
            // Calculate next position
            for (int i = this.bodyParts.Count - 1; i > 0; i--)
            {
                this.bodyParts[i] = this.bodyParts[i - 1];
            }
            this.bodyParts[0] += currentDirection.GetVector();
            currentDirection = nextDirection;

            hasMoved = true;
        }

        private void CheckCollisions()
        {
            if(!hasMoved || Dead) return;

            var snakeHead = this.bodyParts[0];

            if (bodyParts.GetRange(1, bodyParts.Count - 1).Any(part => part == snakeHead))
            {
                this.SetDead();
            }
        }

        private void UpdateInput()
        {
            if (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.VerticalDrag)
                {
                    // Up
                    if (gesture.Delta.Y < 0)
                    {
                        TrySetNextDirection(Direction.North);
                    }

                    // Down
                    if (gesture.Delta.Y > 0)
                    {
                        TrySetNextDirection(Direction.South);
                    }
                }

                if (gesture.GestureType == GestureType.HorizontalDrag)
                {
                    // Left
                    if (gesture.Delta.X < 0)
                    {
                        TrySetNextDirection(Direction.West);
                    }

                    // Right
                    if (gesture.Delta.X > 0)
                    {
                        TrySetNextDirection(Direction.East);
                    }
                }
            }
        }

        private void TrySetNextDirection(Direction direction)
        {
            if (!this.currentDirection.IsOppositeOf(direction))
            {
                this.nextDirection = direction;
            }
        }

        private void SetDead()
        {
            deathCounter = TimeSpan.FromMilliseconds(RespawnMs);
        }
    }
}
