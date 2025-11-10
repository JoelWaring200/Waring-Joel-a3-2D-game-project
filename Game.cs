// Include the namespaces (code libraries) you need below.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        //setup
        int Width = 800;
        int Height = 600;

        //game states
        bool titleScreen;
        bool rulesScreen;
        bool levelsScreen;
        bool levelOne;
        bool levelTwo;
        bool levelThree;
        bool endScreen;

        //checking level locks
        bool levelOneWon;
        bool levelTwoWon;

        //checking player actions to decide what ending
        int enemysSpared = 0;
        int enemysKilled = 0;

        bool lastOption = false;

        //buttons
        Button playButton = new Button(300, 400, 200, 50, "Play", 36, new Color(70, 130, 180, 255), true);
        Button rulesButton = new Button(350, 500, 100, 50, "rules", 36, new Color(70, 130, 180, 255), true);
        Button backButton = new Button(50, 50, 100, 50, "back", 38, new Color(120, 6, 6, 255), true);
        Button levelOneButton = new Button(50, 150, 200, 200, "ONE", 50, new Color(70, 130, 180, 255), true);
        Button levelTwoButton = new Button(300, 150, 200, 200, "TWO", 50, new Color(70, 130, 180, 255), true);
        Button levelThreeButton = new Button(550, 150, 200, 200, "THREE", 50, new Color(70, 130, 180, 255), true);

        //fake buttons for rules
        Button[] fakeButtons =
                {   //          x y width height text font sise color interactable
                    new Button(150, 150, 50, 50, "W", 40, new Color(70, 130, 180, 255), false),
                    new Button(100, 200, 50, 50, "A", 40, new Color(70, 130, 180, 255), false),
                    new Button(200, 200, 50, 50, "D", 40, new Color(70, 130, 180, 255), false),
                    new Button(100, 300, 50, 50, "W", 40, new Color(70, 130, 180, 255), false),
                    new Button(100, 400, 50, 50, "A", 40, new Color(70, 130, 180, 255), false),
                    new Button(100, 500, 50, 50, "D", 40, new Color(70, 130, 180, 255), false),
                    new Button(350, 150, 50, 50, "<-", 40, new Color(70, 130, 180, 255), false),
                    new Button(450, 150, 50, 50, "->", 40, new Color(70, 130, 180, 255), false),
                    new Button(350, 200, 150, 50, "Space", 40, new Color(70, 130, 180, 255), false),
                    new Button(350, 500, 50, 50, "->", 40, new Color(70, 130, 180, 255), false),
                    new Button(350, 400, 50, 50, "<-", 40, new Color(70, 130, 180, 255), false),
                    new Button(250, 300, 150, 50, "Space", 40, new Color(70, 130, 180, 255), false)
                };

        //character creation
        Player Character = new Player(390, 420, 20, 20, 5, 2.0f);

        // enemy Creation
        Enemy blandEnemy = new Enemy(300, 50, 200, 200, 3, Color.Green, false, true, false);
        Enemy happyEnemy = new Enemy(300, 50, 200, 200, 3, Color.Yellow, false, false, true);
        Enemy sadEnemy = new Enemy(300, 50, 200, 200, 3, Color.Red, true, false, false);
        //characters "stand"
        List<Platforms> playerPlatforms = new List<Platforms>
            {
                new Platforms(390, 450, 20, 30, new Color(70, 130, 180, 255), new Vector2(390, 450), new Vector2(0, 0), new Vector2(0, 0), false),
                new Platforms(390, 520, 20, 30, new Color(70, 130, 180, 255), new Vector2(390, 520), new Vector2(0, 0), new Vector2(0, 0), false),
            };
        //attack handling
        List<Platforms> attackPlatforms = new List<Platforms>
        {

        };
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(Width, Height);
            titleScreen = true;
            rulesScreen = false;
            levelsScreen = false;
            levelOne = false;
            levelTwo = false;
            levelThree = false;
            endScreen = false;

            //checking level locks
            levelOneWon = false;
            levelTwoWon = false;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            if (Character.currentHealth > 0)
            {
                //mouse pos
                Vector2 mousePos = Input.GetMousePosition();
                //drawing the screen
                Window.ClearBackground(Color.Black);
                TitleScreen(mousePos);
                RulesScreen(mousePos);
                LevelScreen(mousePos);
                LevelOne(mousePos);
                LevelTwo(mousePos);
                LevelThree(mousePos);
                EndScreen();
            }
            if (Character.currentHealth <= 0)
            {
                Window.ClearBackground(Color.Red);
                Text.Color = Color.Black;
                Text.Size = 50;
                Text.Draw("you lose", 300, 250);
            }
        }
        void TitleScreen(Vector2 mousePos)
        {
            if (titleScreen)
            {
                //Title
                Text.Size = 70;
                Text.Color = Color.White;
                Text.Draw("Square Fighter", new Vector2(150, 150));

                //buttons
                playButton.drawButton(mousePos);
                rulesButton.drawButton(mousePos);

                //tracking button clicks
                if (playButton.clicked(mousePos))
                {
                    levelsScreen = true;
                    titleScreen = false;
                }
                if (rulesButton.clicked(mousePos))
                {
                    rulesScreen = true;
                    titleScreen = false;
                }
            }
        }
        //level handling
        void RulesScreen(Vector2 mousePos)
        {
            if (rulesScreen)
            {
                //Title
                Text.Size = 70;
                Text.Color = Color.White;
                Text.Draw("Rules", new Vector2(305, 50));

                //buttons
                backButton.drawButton(mousePos);

                //tracking button clicks
                if (backButton.clicked(mousePos))
                {
                    titleScreen = true;
                    rulesScreen = false;
                }
                //writing out rules

                //movement
                Text.Size = 30;
                Text.Draw("movement", new Vector2(100, 120));
                Draw.LineColor = Color.White;
                Draw.Line(new Vector2(70, 130), new Vector2(70, 570));
                Draw.Line(new Vector2(70, 570), new Vector2(520, 570));
                Draw.Line(new Vector2(520, 570), new Vector2(520, 130));
                Draw.Line(new Vector2(250, 130), new Vector2(520, 130));
                Draw.Line(new Vector2(70, 130), new Vector2(90, 130));
                for (int button = 0; button < fakeButtons.Length; button++)
                {
                    fakeButtons[button].drawButton(mousePos);
                }
                Text.Draw("UP", new Vector2(160, 315));
                Text.Draw("LEFT", new Vector2(160, 415));
                Text.Draw("RIGHT", new Vector2(160, 515));
                Text.Draw("LEFT", new Vector2(410, 415));
                Text.Draw("RIGHT", new Vector2(410, 515));
                Text.Draw("UP", new Vector2(410, 315));

                //Goal
                Text.Draw("Goal", new Vector2(600, 120));
                Text.Size = 20;
                Text.Draw("Dodge attacks and wait", new Vector2(550, 180));
                Text.Draw("for an opportunity to ", new Vector2(550, 205));
                Text.Draw("attack or heal", new Vector2(550, 230));
                Draw.Line(new Vector2(530, 130), new Vector2(530, 260));
                Draw.Line(new Vector2(530, 260), new Vector2(790, 260));
                Draw.Line(new Vector2(790, 260), new Vector2(790, 130));
                Draw.Line(new Vector2(790, 130), new Vector2(700, 130));
                Draw.Line(new Vector2(530, 130), new Vector2(590, 130));

                //story
                Text.Size = 30;
                Text.Draw("Story", new Vector2(600, 270));
                Text.Size = 20;
                Text.Draw("Three warriors stand", new Vector2(550, 325));
                Text.Draw("guarding the gate", new Vector2(550, 350));
                Text.Draw("First fights by habit", new Vector2(550, 375));
                Text.Draw("yields if shown mercy", new Vector2(550, 400));
                Text.Draw("Second seeks honor", new Vector2(550, 425));
                Text.Draw("spared with kindness", new Vector2(550, 450));
                Text.Draw("Third acts from fear", new Vector2(550, 475));
                Text.Draw("surrenders if trusted", new Vector2(550, 500));
                Text.Draw("All can be spared", new Vector2(550, 525));
                Text.Draw("All can be killed", new Vector2(550, 550));
                Draw.Line(new Vector2(530, 280), new Vector2(530, 570));
                Draw.Line(new Vector2(530, 570), new Vector2(790, 570));
                Draw.Line(new Vector2(790, 570), new Vector2(790, 280));
                Draw.Line(new Vector2(790, 280), new Vector2(700, 280));
                Draw.Line(new Vector2(530, 280), new Vector2(590, 280));

            }
        }
        void LevelScreen(Vector2 mousePos)
        {
            if (levelsScreen)
            {
                Draw.LineSize = 1;
                Draw.LineColor = Color.Black;
                //Title
                Text.Size = 70;
                Text.Color = Color.White;
                Text.Draw("Levels", new Vector2(305, 50));
                //buttons
                backButton.drawButton(mousePos);
                levelOneButton.drawButton(mousePos);
                levelTwoButton.drawButton(mousePos);
                levelThreeButton.drawButton(mousePos);
                if (levelOneButton.clicked(mousePos) && levelOneWon == false)
                {
                    levelsScreen = false;
                    levelOne = true;
                }
                if (levelTwoButton.clicked(mousePos) && levelOneWon == true && levelTwoWon == false)
                {
                    levelsScreen = false;
                    levelTwo = true;
                    Character.x = 390;
                    Character.y = 420;
                }
                if (levelThreeButton.clicked(mousePos) && levelTwoWon == true)
                {
                    levelsScreen = false;
                    levelThree = true;
                    Character.x = 390;
                    Character.y = 420;
                }
                //tracking button clicks
                if (backButton.clicked(mousePos))
                {
                    titleScreen = true;
                    levelsScreen = false;
                }

            }
        }
        void LevelOne(Vector2 mousePos)
        {
            if (levelOne)
            {
                //player area
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
                //set players platforms
                foreach (Platforms platform in playerPlatforms)
                {
                    platform.Update();
                };

                //attack logic (empty till enemy)
                foreach (Platforms platform in attackPlatforms)
                {
                    platform.Update();
                };

                //player
                Character.update(playerPlatforms, attackPlatforms);
                //enemy type
                blandEnemy.update(attackPlatforms, mousePos, Character);
                //removes attacks once they leave the play area
                for (int a = 0; a < attackPlatforms.Count; a++)
                {
                    Platforms p = attackPlatforms[a];
                    if (p.x < 200 && p.isforward == false || p.x > 600 && p.isforward == true || p.y < 300 && p.isforward == false || p.y > 550 && p.isforward == true)
                    {
                        attackPlatforms.RemoveAt(a);
                    }
                }
                if (blandEnemy.currentEnemyHealth <= 0)
                {
                    // level two unlocks
                    levelOneWon = true;
                    lastOption = true;

                    //player options
                    Button playerOptionSpare = new Button(50, 200, 100, 100, "SPARE", 20, new Color(0, 100, 0), true);
                    Button playerOptionKill = new Button(650, 200, 100, 100, "KILL", 20, new Color(100, 0, 0), true);

                    //buttons
                    playerOptionSpare.drawButton(mousePos);
                    playerOptionKill.drawButton(mousePos);

                    //tracking button clicks
                    if (playerOptionSpare.clicked(mousePos))
                    {
                        titleScreen = true;
                        enemysSpared += 1;
                        levelOne = false;
                    }
                    if (playerOptionKill.clicked(mousePos))
                    {
                        titleScreen = true;
                        enemysKilled += 1;
                        levelOne = false;
                    }
                }
            }
        }
        void LevelTwo(Vector2 mousePos)
        {
            if (levelTwo)
            {

                //player area
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
                //set players platforms
                foreach (Platforms platform in playerPlatforms)
                {
                    platform.Update();
                }
                ;
                //attack logic (empty till enemy)
                foreach (Platforms platform in attackPlatforms)
                {
                    platform.Update();
                }
                ;

                //player
                Character.update(playerPlatforms, attackPlatforms);
                //enemy type
                happyEnemy.update(attackPlatforms, mousePos, Character);
                //removes attacks once theyre outside play area
                for (int a = 0; a < attackPlatforms.Count; a++)
                {
                    Platforms p = attackPlatforms[a];
                    if (p.x < 200 && p.isforward == false || p.x > 600 && p.isforward == true || p.y < 300 && p.isforward == false || p.y > 550 && p.isforward == true)
                    {
                        attackPlatforms.RemoveAt(a);
                    }
                }
                if (happyEnemy.currentEnemyHealth <= 0)
                {
                    // level two unlocks
                    levelTwoWon = true;

                    //player options
                    Button playerOptionSpare = new Button(50, 200, 100, 100, "SPARE", 20, new Color(0, 100, 0), true);
                    Button playerOptionKill = new Button(650, 200, 100, 100, "KILL", 20, new Color(100, 0, 0), true);

                    //buttons
                    playerOptionSpare.drawButton(mousePos);
                    playerOptionKill.drawButton(mousePos);

                    //tracking button clicks
                    if (playerOptionSpare.clicked(mousePos))
                    {
                        titleScreen = true;
                        enemysSpared += 1;
                        levelTwo = false;
                    }
                    if (playerOptionKill.clicked(mousePos))
                    {
                        titleScreen = true;
                        enemysKilled += 1;
                        levelTwo = false;
                    }
                }
            }
        }
        //fix this
        void LevelThree(Vector2 mousePos)
        {
            if (levelThree)
            {
                //player area
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
                //set players platforms
                foreach (Platforms platform in playerPlatforms)
                {
                    platform.Update();
                }
                ;

                //attack logic (empty till enemy)
                foreach (Platforms platform in attackPlatforms)
                {
                    platform.Update();
                }
                ;

                //player
                Character.update(playerPlatforms, attackPlatforms);
                //enemy type
                sadEnemy.update(attackPlatforms, mousePos, Character);
                // removes attacks once outside play area
                for (int a = 0; a < attackPlatforms.Count; a++)
                {
                    Platforms p = attackPlatforms[a];
                    if (p.x < 200 && p.isforward == false || p.x > 600 && p.isforward == true || p.y < 300 && p.isforward == false || p.y > 550 && p.isforward == true)
                    {
                        attackPlatforms.RemoveAt(a);
                    }
                }
                if (sadEnemy.currentEnemyHealth <= 0)
                {
                    // level two unlocks
                    levelOneWon = true;
                    lastOption = true;

                    //player options
                    Button playerOptionSpare = new Button(50, 200, 100, 100, "SPARE", 20, new Color(0, 100, 0), true);
                    Button playerOptionKill = new Button(650, 200, 100, 100, "KILL", 20, new Color(100, 0, 0), true);

                    //buttons
                    playerOptionSpare.drawButton(mousePos);
                    playerOptionKill.drawButton(mousePos);

                    //tracking button clicks
                    if (playerOptionSpare.clicked(mousePos))
                    {
                        endScreen = true;
                        enemysSpared += 1;
                        levelThree = false;
                    }
                    if (playerOptionKill.clicked(mousePos))
                    {
                        endScreen = true;
                        enemysKilled += 1;
                        levelThree = false;
                    }
                }
            }
        }
        void EndScreen()
        {
            if (endScreen)
            {
                if(enemysSpared >= 3)
                {
                    Window.ClearBackground(Color.Green);
                    Text.Color = Color.Black;
                    Text.Size = 50;
                    Text.Draw("you were", 300, 250);
                    Text.Draw("merciful", 300, 250);

                }
                else if(enemysKilled >= 3)
                {
                    Window.ClearBackground(Color.Red);
                    Text.Color = Color.Black;
                    Text.Size = 50;
                    Text.Draw("you were", 300, 250);
                    Text.Draw("a Monster", 300, 250);
                }
                else
                {
                    Window.ClearBackground(Color.Gray);
                    Text.Color = Color.Black;
                    Text.Size = 50;
                    Text.Draw("you couldnt", 300, 250);
                    Text.Draw("decide", 300, 250);
                }
            }
        }
    }

}

