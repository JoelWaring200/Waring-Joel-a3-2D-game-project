using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace MohawkGame2D
{
    public class Player
    {
        public float x;
        public float y; 
        float width;
        float height;
        public int maxHealth;
        public int currentHealth;
        public float movementSpeed;

        private Vector2 velocity;
        private Vector2 gravity;
        bool isGrounded;

        // simple ground height (for testing)
        private float groundY = 550f;


        public Player(int x, int y, float width, float height, int maxHealth, float movementSpeed)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.movementSpeed = movementSpeed;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.velocity = Vector2.Zero;
            this.gravity = new Vector2(0, 500f);
            this.isGrounded = false;
        }
        //draws player
        public void drawPlayer()
        {
            //eyes that track mouse
            float eyeRadius = width / 4.0f;
            float pupilRadius = eyeRadius / 2.0f;

            Vector2 eyeCenterLeft = new Vector2(x + width / 2 - width / 4, y + width / 2 - width / 4 + 2);
            Vector2 mouseDirectionLeft = Input.GetMousePosition() - eyeCenterLeft;

            Vector2 eyeCenterRight = new Vector2(x + width - width / 4, y + width / 2 - width / 4 + 2);
            Vector2 mouseDirectionRight = Input.GetMousePosition() - eyeCenterRight;

            // Body
            Draw.FillColor = Color.Blue;
            Draw.Rectangle(x, y, width, height);

            // Eyes
            Draw.FillColor = Color.White;
            Draw.Circle(eyeCenterLeft.X, eyeCenterLeft.Y, eyeRadius);
            Draw.Circle(eyeCenterRight.X, eyeCenterRight.Y, eyeRadius);

            // Pupils
            Draw.FillColor = Color.Black;

            Vector2 leftPupilOffset = Vector2.Normalize(mouseDirectionLeft) * MathF.Min(mouseDirectionLeft.Length(), eyeRadius - pupilRadius);
            Vector2 rightPupilOffset = Vector2.Normalize(mouseDirectionRight) * MathF.Min(mouseDirectionRight.Length(), eyeRadius - pupilRadius);

            Draw.Circle(eyeCenterLeft.X + leftPupilOffset.X, eyeCenterLeft.Y + leftPupilOffset.Y, pupilRadius);
            Draw.Circle(eyeCenterRight.X + rightPupilOffset.X, eyeCenterRight.Y + rightPupilOffset.Y, pupilRadius);
        }
        public void movement()
        {
            //player movement
            if (Input.IsKeyboardKeyPressed(KeyboardInput.W) && isGrounded || Input.IsKeyboardKeyPressed(KeyboardInput.Space) && isGrounded)
            {
                velocity.Y -= movementSpeed * 150;
                isGrounded = false;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.A) || Input.IsKeyboardKeyDown(KeyboardInput.Left))
            {
                x -= movementSpeed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.D) || Input.IsKeyboardKeyDown(KeyboardInput.Right))
            {
                x += movementSpeed;
            }
        }

        public void simGravity()
        {
            // Apply gravity
            velocity += gravity * Time.DeltaTime;

            // Update position
            y += velocity.Y * Time.DeltaTime;
        }
        public void Collision(List<Platforms> playerPlatforms, List<Platforms> attackPlatforms)
        {
            
            isGrounded = false;

            float playerLeft = x;
            float playerRight = x + width;
            float playerTop = y;
            float playerBottom = y + height;

            // Combine both lists if you like:
            List<Platforms> allPlatforms = new List<Platforms>();
            allPlatforms.AddRange(playerPlatforms);
            allPlatforms.AddRange(attackPlatforms);

            foreach (Platforms platform in allPlatforms)
            {
                float platLeft = platform.x;
                float platRight = platform.x + platform.width;
                float platTop = platform.y;
                float platBottom = platform.y + platform.height;

                bool overlap = playerRight > platLeft && playerLeft < platRight &&
                               playerBottom > platTop && playerTop < platBottom;
                if (!overlap) continue;

                float fromTop = playerBottom - platTop;
                float fromBottom = platBottom - playerTop;
                float fromLeft = playerRight - platLeft;
                float fromRight = platRight - playerLeft;
                bool isVertical = (fromTop < fromLeft && fromTop < fromRight) ||
                                  (fromBottom < fromLeft && fromBottom < fromRight);

                if (fromTop < fromLeft && fromTop < fromRight && velocity.Y >= 0)
                {
                    y -= fromTop;
                    velocity.Y = 0;
                    isGrounded = true;
                }
                else if (fromBottom < fromLeft && fromBottom < fromRight && velocity.Y < 0)
                {
                    y += fromBottom;
                    velocity.Y = 0;
                }
                else if (!isVertical)
                {
                    if (fromLeft < fromRight)
                        x -= fromLeft;
                    else
                        x += fromRight;
                    velocity.X = 0;
                }
            }

            
            // walls / player area
            if (playerTop <= 300 || playerBottom >= 550 || playerRight >= 600 || playerLeft <= 200)
            {
                x = 390;
                y = 420;
                currentHealth -= 1;
                velocity = Vector2.Zero;
            }
        }
        //draws health
        public void health()
        {
            for(int i = 0; i < maxHealth; i++)
            {
                Draw.FillColor = Color.Gray;
                Draw.Rectangle(i * 40 + 20, 80, 30, 30);
            }
            for(int i = 0; i < currentHealth; i++)
            {
                Draw.FillColor = new Color(120, 6, 6, 255);
                Draw.Rectangle(i * 40 + 20, 80, 30, 30);
            }
            
        }
        public void update(List<Platforms> playerPlatforms, List<Platforms> attackPlatforms)
        {
            health();
            movement();
            simGravity();
            Collision(playerPlatforms, attackPlatforms);
            drawPlayer();
        }
    }
}
