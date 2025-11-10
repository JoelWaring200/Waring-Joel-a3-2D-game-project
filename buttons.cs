using Raylib_cs;
using System;
using System.Numerics;



namespace MohawkGame2D
{
    public class Button
    {
        private int width;
        private int height;
        private int x;
        private int y;
        private string text;
        private int fontSize;
        private Color color;
        private Color hoverColor;
        private bool interactable;

        public Button( int x, int y, int width, int height, string text, int fontSize, Color color, bool interactable)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.text = text;
            this.color = color;
            this.fontSize = fontSize;
            this.interactable = interactable;

            hoverColor = new Color(
                (int)((255 + 2 * color.R) / 3),
                (int) ((255 + 2 * color.G) / 3),
                (int) ((255 + 2 * color.B) / 3),
                255
            );
        }   

        public void drawButton(Vector2 mousePos)
        {
            bool hovering = false;
            //checks if the button is actualy a button
            if (interactable)
            {
                //check if players hovering over button
                if (mousePos.X > x && mousePos.X < x + width && mousePos.Y > y && mousePos.Y < y + height)
                {
                    hovering = true;
                }
            }
           
           
            //draws the buttons rectangle
            if (hovering)
            {
                Draw.FillColor = hoverColor;
                Draw.Rectangle(x, y, width, height);
            }
            else
            {
                Draw.FillColor = color;
                Draw.Rectangle(x, y, width, height);
            }
            //draws the text centered
            Text.Size = fontSize;
            int textWidth = Raylib.MeasureText(text, fontSize);
            int textX = x + (width / 2 - textWidth / 2);
            int textY = y + (height / 2 - fontSize / 2);
            Text.Color = Color.White;

            Text.Draw(text, new Vector2(textX, textY));
        }
        //checks if button is clicked
        public bool clicked(Vector2 mousePos)
        {
            bool hovering = false;
            if (mousePos.X > x && mousePos.X < x + width && mousePos.Y > y && mousePos.Y < y + height)
            {
                hovering = true;
            }
            bool isMousePressed = Input.IsMouseButtonPressed(0);
            return hovering && isMousePressed;
        }
    }
}
