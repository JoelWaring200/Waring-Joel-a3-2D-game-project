using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    internal class Enemy
    {
        public int maxEnemyHealth;
        public int currentEnemyHealth;
        float x;
        float y;
        float width;
        float height;
        Color color;
        bool sad;
        bool bland;
        bool happy;
        bool start = false;
        bool isAttacking = false;
        public Enemy(float x, float y, float width, float height, int health, Color color, bool sad, bool bland, bool happy)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.maxEnemyHealth = health;
            this.currentEnemyHealth = health;
            this.color = color;
            this.sad = sad;
            this.bland = bland;
            this.happy = happy;
        }

        public void drawenemy()
        {
            Draw.FillColor = color;
            Draw.Rectangle(x, y, width, height);
            if (this.sad)
            {
                //eye
                Draw.FillColor = Color.White;
                Draw.LineSize = 1;
                Draw.Circle(x + width / 4, y + height / 3, width / 6);
                Draw.Circle(x + width * 3 / 4, y + height / 3, width / 6);
                //pupil
                Draw.FillColor = Color.Black;
                Draw.Circle(x + width / 4, y + height / 3, width / 12);
                Draw.Circle(x + width * 3 / 4, y + height / 3, width / 12);
                //tears
                Draw.FillColor = Color.Blue;
                Draw.Rectangle(x + width / 4 - width / 6, y + height / 3, width / 3, 40);
                Draw.Rectangle(x + width * 3 / 4 - width / 6, y + height / 3, width / 3, 40);
                //mouth
                Draw.FillColor = Color.Black;
                Draw.Circle(Window.Width / 2, y + height * 2.5f / 3, width / 4);
                Draw.FillColor = color;
                Draw.LineSize = 0;
                Draw.Rectangle(x, y + height * 2.5f / 3, width, 33);
            }
            
            
            if (this.happy)
            {
                //eye
                Draw.FillColor = Color.White;
                Draw.LineSize = 1;
                Draw.Circle(x + width / 4, y + height / 3, width / 6);
                Draw.Circle(x + width * 3 / 4, y + height / 3, width / 6);
                //pupil
                Draw.FillColor = Color.Black;
                Draw.Circle(x + width / 4, y + height / 3, width / 12);
                Draw.Circle(x + width * 3 / 4, y + height / 3, width / 12);
                //mouth
                Draw.FillColor = Color.Black;
                Draw.Circle(Window.Width / 2, y + height * 2.5f / 3 - 45, width / 4);
                //happy eyes/mouth block
                Draw.FillColor = color;
                Draw.LineSize = 0;
                Draw.Rectangle(x, y + height / 3, width, 60);
            }
            if(this.bland)
            {
                Draw.FillColor = Color.White;
                Draw.LineSize = 1;
                Draw.Rectangle(x + width / 8, y + height / 6, width / 3, height / 6);
                Draw.Rectangle(x + width * 4.5f / 8, y + height / 6, width / 3, height / 6);

                Draw.FillColor = Color.Black;
                Draw.Rectangle(x + width / 8 + width / 6 - width / 12, y + height / 6, width / 6, height / 6);
                Draw.Rectangle(x + width * 4.5f / 8 + width / 6 - width / 12, y + height / 6, width / 6, height / 6);
                Draw.Rectangle(x + width / 8, y + height * 2 / 3, width - 40, 20);
            }
        }
        public void attackhaddling()
        {
            if (Input.IsKeyboardKeyPressed(KeyboardInput.W) && start == true || Input.IsKeyboardKeyPressed(KeyboardInput.Space) && start == true)
            {
                isAttacking = true;
                start = false;
            }

            if (isAttacking == true)
            {
                
            }
        }
        public void health()
        {
            for (int i = 0; i < maxEnemyHealth; i++)
            {
                Draw.FillColor = Color.Gray;
                Draw.Rectangle(Window.Width - i * 40 - 50, 80, 30, 30);
            }
            for (int i = 0; i < currentEnemyHealth; i++)
            {
                Draw.FillColor = new Color(138, 43, 226);
                Draw.Rectangle(Window.Width - i * 40 - 50, 80, 30, 30);
            }
        }
        public void update()
        {
            drawenemy();
            health();
        }
    }
}
