// Include the namespaces (code libraries) you need below.
using System;
using System.Collections.Generic;
using System.Numerics;

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
                Text.Draw("Rules", new Vector2(150, 150));
                //buttons
                backButton.drawButton(mousePos);
                
                //tracking button clicks
                if (backButton.clicked(mousePos))
                {
                    titleScreen = true;
                    rulesScreen = false;
                }
                
            }
            if (levelsScreen)
            {
                //Title
                Text.Size = 70;
                Text.Color = Color.White;
                Text.Draw("Levels", new Vector2(150, 150));
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
                // make the levels
            }
            if (levelTwo)
            {

            }
            if (levelThree)
            {

            }


        }
    }
    
}
    