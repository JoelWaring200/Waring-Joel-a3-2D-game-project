// Include the namespaces (code libraries) you need below.
using System;
using System.Collections.Generic;
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
        bool titleScreen = true;
        bool rulesScreen = false;
        bool levelsScreen = false;
        bool levelOne = false;
        bool levelTwo = false;
        bool levelThree = false;
        //checking level locks
        bool levelOneWon = false;
        bool levelTwoWon = false;
        //buttons
        Button playButton = new Button(300, 400, 200, 50, "Play", 36, new Color(70, 130, 180, 255));
        Button rulesButton = new Button(350, 500, 100, 50, "rules", 36, new Color(70, 130, 180, 255));
        Button backButton = new Button(50, 50, 100, 50, "back", 38, new Color(120, 6, 6, 255));
        Button levelOneButton = new Button(50, 250, 200, 200, "ONE", 50, new Color(70, 130, 180, 255));
        Button levelTwoButton = new Button(300, 250, 200, 200, "TWO", 50, new Color(70, 130, 180, 255));
        Button levelThreeButton = new Button(550, 250, 200, 200, "THREE", 50, new Color(70, 130, 180, 255));
        //fake buttons
        Button rulesW = new Button(150, 150, 50, 50, "W", 40, new Color(70, 130, 180, 255));
        Button rulesA = new Button(100, 200, 50, 50, "A", 40, new Color(70, 130, 180, 255));
        Button rulesD = new Button(200, 200, 50, 50, "D", 40, new Color(70, 130, 180, 255));
        Button rulesWB = new Button(100, 300, 50, 50, "W", 40, new Color(70, 130, 180, 255));
        Button rulesAB = new Button(100, 400, 50, 50, "A", 40, new Color(70, 130, 180, 255));
        Button rulesDB = new Button(100, 500, 50, 50, "D", 40, new Color(70, 130, 180, 255));

        Button rulesRightArrow = new Button(350, 150, 50, 50, "<-", 40, new Color(70, 130, 180, 255));
        Button rulesLeftArrow = new Button(450, 150, 50, 50, "->", 40, new Color(70, 130, 180, 255));
        Button rulesSpace = new Button(350, 200, 150, 50, "Space", 40, new Color(70, 130, 180, 255));
        Button rulesRightArrowB = new Button(350, 500, 50, 50, "<-", 40, new Color(70, 130, 180, 255));
        Button rulesLeftArrowB = new Button(350, 400, 50, 50, "->", 40, new Color(70, 130, 180, 255));
        Button rulesSpaceB = new Button(250, 300, 150, 50, "Space", 40, new Color(70, 130, 180, 255));
        Player Character = new Player(390, 420, 20, 20, 3, 2.0f);
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(Width, Height);
           
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            
            //mouse pos
            Vector2 mousePos = Input.GetMousePosition();
            //drawing the screen
            Window.ClearBackground(Color.Black);
            GameStateControl();
            
        }
        void GameStateControl()
        {
            Vector2 mousePos = Input.GetMousePosition();
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
                rulesW.drawButton(mousePos);
                rulesA.drawButton(mousePos);
                rulesD.drawButton(mousePos);
                rulesRightArrow.drawButton(mousePos);
                rulesLeftArrow.drawButton(mousePos);
                rulesSpace.drawButton(mousePos);
                rulesWB.drawButton(mousePos);
                Text.Draw("UP", new Vector2(160, 315));
                rulesAB.drawButton(mousePos);
                Text.Draw("LEFT", new Vector2(160, 415));
                rulesDB.drawButton(mousePos);
                Text.Draw("RIGHT", new Vector2(160, 515));
                rulesRightArrowB.drawButton(mousePos);
                Text.Draw("RIGHT", new Vector2(410, 415));
                rulesLeftArrowB.drawButton(mousePos);
                Text.Draw("LEFT", new Vector2(410, 515));
                rulesSpaceB.drawButton(mousePos);
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
                if (levelOneButton.clicked(mousePos))
                {
                    levelsScreen = false;
                    levelOne = true;
                }
                if (levelTwoButton.clicked(mousePos) && levelOneWon == true)
                {
                    levelsScreen = false;
                    levelTwo = true;
                }
                if (levelThreeButton.clicked(mousePos) && levelTwoWon == true)
                {
                    levelsScreen = false;
                    levelThree = true;
                }
                //tracking button clicks
                if (backButton.clicked(mousePos))
                {
                    titleScreen = true;
                    levelsScreen = false;
                }

            }
            if (levelOne)
            {
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
                Character.drawPlayer();
                Character.SimulateGravity();
            }
            if (levelTwo)
            {
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
            }
            if (levelThree)
            {
                Draw.FillColor = Color.White;
                Draw.Rectangle(new Vector2(200, 300), new Vector2(400, 250));
            }
        }
    }
    
}
    