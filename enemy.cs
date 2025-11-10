using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        bool start = true;
        bool isAttacking = false;
        int attackAmount;
        int attackAllowedAmount;
        Color atkColor;
        int atkSpeed;
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
                //states the amount of attacks the player has to survive before being able to act
                attackAllowedAmount = 7;
                //sets attack speeds and color
                atkColor = Color.Red;
                atkSpeed = 8;
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
                //states the amount of attacks the player has to survive before being able to act
                attackAllowedAmount = 5;
                //sets attack speeds and color
                atkColor = Color.Yellow;
                atkSpeed = 6;

            }
            if(this.bland)
            {
                //eyes
                Draw.FillColor = Color.White;
                Draw.LineSize = 1;
                Draw.Rectangle(x + width / 8, y + height / 6, width / 3, height / 6);
                Draw.Rectangle(x + width * 4.5f / 8, y + height / 6, width / 3, height / 6);
                //mouth
                Draw.FillColor = Color.Black;
                Draw.Rectangle(x + width / 8 + width / 6 - width / 12, y + height / 6, width / 6, height / 6);
                Draw.Rectangle(x + width * 4.5f / 8 + width / 6 - width / 12, y + height / 6, width / 6, height / 6);
                Draw.Rectangle(x + width / 8, y + height * 2 / 3, width - 40, 20);
                //states the amount of attacks the player has to survive before being able to act
                attackAllowedAmount = 3;
                //sets attack speeds and color
                atkColor = Color.Green;
                atkSpeed = 4;
            }
        }
        public void attackhaddling(List<Platforms> attackPlatforms, Vector2 mousePos, Player Character)
        {
            //lets player start the level once its loaded
            if ((Input.IsKeyboardKeyPressed(KeyboardInput.W) && start) || (Input.IsKeyboardKeyPressed(KeyboardInput.Space) && start))
            {

                isAttacking = true;
                start = false;
            }
            if (start && !isAttacking)
            {
                Text.Size = 50;
                Text.Draw("press w or space to start", 20, 0);
            }

            if (isAttacking && currentEnemyHealth > 0)
            {   //enemy attacks
                System.Random rand = new System.Random();
                if (attackAmount < attackAllowedAmount)
                {
                    if (attackPlatforms.Count <= 0)
                    {
                        //decide attack type
                        int attackType = rand.Next(1, 4);
                        //what number of attacks the player has deltwith
                        attackAmount++;
                        
                        //attack types
                        if (attackType == 1)
                        {
                            attackPlatforms.Add(new Platforms(
                            201, 301, 20, 200, atkColor,
                            new Vector2(150, 301), new Vector2(601, 301),
                            new Vector2(atkSpeed, 0), true
                            ));
                        }
                        else if (attackType == 2)
                        {
                            attackPlatforms.Add(new Platforms(
                            201, 301, 20, 200, atkColor,
                            new Vector2(150, 301), new Vector2(601, 301),
                            new Vector2(atkSpeed, 0), true
                            ));
                        }
                        else if (attackType == 3)
                        {
                            attackPlatforms.Add(new Platforms(
                            201, 301, 20, 200, atkColor,
                            new Vector2(150, 301), new Vector2(601, 301),
                            new Vector2(atkSpeed, 0), true
                            ));
                        }
                        else if (attackType == 4)
                        {
                            attackPlatforms.Add(new Platforms(
                            201, 301, 20, 200, atkColor,
                            new Vector2(150, 301), new Vector2(601, 301),
                            new Vector2(atkSpeed, 0), true
                            ));
                        }
                    }
                }
                //after you survive the amount of enemy attacks (attackAllowedAmount) state change
                if (attackAmount >= attackAllowedAmount)
                {
                    attackAmount = 0;
                    isAttacking = false;
                }
            }
            //player chooses to heal or attack
            if (!isAttacking && !start && currentEnemyHealth > 0 && attackPlatforms.Count <= 0)
            {
                //player options
                Button playerOptionHeal = new Button(50, 300, 100, 100, "HEAL", 20, new Color(0, 100, 0), true);
                Button playerOptionAttack = new Button(650, 300, 100, 100, "ATTACK", 20, new Color(100, 0, 0), true);

                //buttons
                playerOptionHeal.drawButton(mousePos);
                playerOptionAttack.drawButton(mousePos);

                //tracking button clicks
                if (playerOptionHeal.clicked(mousePos))
                {
                    if (Character.currentHealth < Character.maxHealth)
                    {
                        Character.currentHealth += 1;
                    }
                    isAttacking = true;
                }
                if (playerOptionAttack.clicked(mousePos))
                {
                    currentEnemyHealth -= 1;
                    isAttacking = true;
                }
            }
        }
        public void health()
        {
            //draws health
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
        public void update(List<Platforms> attackPlatforms, Vector2 mousePos, Player Character)
        {
            drawenemy();
            health();
            attackhaddling(attackPlatforms, mousePos, Character);
        }
    }
}
