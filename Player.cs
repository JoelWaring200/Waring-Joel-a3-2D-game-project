using System;
using System.Numerics;

namespace MohawkGame2D
{
    public class Player
    {
        float x; // changed to float
        float y; // changed to float
        float width;
        float height;
        int maxHealth;
        int currentHealth;
        float movementSpeed;

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

        public void drawPlayer()
        {
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

        public void simGravity()
        {
            // Apply gravity
            velocity += gravity * Time.DeltaTime;

            // Update position
            y += velocity.Y * Time.DeltaTime;
            x += velocity.X * Time.DeltaTime;

            //place holder will change to interact with platform class
            if (y + height > groundY)
            {
                y = groundY - height;
                velocity.Y = 0;
                isGrounded = true;
            }
            if (x + width >= 600)
            {
                x = 600 - width;
                velocity.X = 0;
            }
            if (x <= 200)
            {
                x = 200;
                velocity.X = 0;
            }
            //player movement
            if (Input.IsKeyboardKeyPressed(KeyboardInput.W) && isGrounded || Input.IsKeyboardKeyPressed(KeyboardInput.Space) && isGrounded)
            {
                velocity.Y -= movementSpeed * 200;
                isGrounded = false;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.A) || Input.IsKeyboardKeyDown(KeyboardInput.Left))
            {
                velocity.X -= movementSpeed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.D) || Input.IsKeyboardKeyDown(KeyboardInput.Right))
            {
                velocity.X += movementSpeed;
            }
            
        }
        public void health()
        {
            if(x + width >= 600 || x <= 200)
            {
                currentHealth -= 1;
                x = 390;
                y = 420;
                velocity.Y = 0;
                velocity.X = 0;

            }
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
            Console.WriteLine(currentHealth);
        }
        public void update()
        {
            health();
            simGravity();
            drawPlayer();
        }
    }
}
