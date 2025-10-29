using Raylib_cs;
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
        
        
        public Button( int x, int y, int width, int height, string text, int fontSize, Color color)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.text = text;
            this.color = color;
            this.fontSize = fontSize;

            hoverColor = new Color(
                (int)((255 + 2 * color.R) / 3),
                (int) ((255 + 2 * color.G) / 3),
                (int) ((255 + 2 * color.B) / 3),
                255
            );
        }   

        public void drawButton(Vector2 mousePos)
        {
            //check if players hovering over button
            bool hovering = false;
            if (mousePos.X > x && mousePos.X < x + width && mousePos.Y > y && mousePos.Y < y + height)
            {
                hovering = true;
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
            int textX = x + (width - textWidth) / 2;
            int textY = y + (height - fontSize) / 2;
            Text.Color = Color.White;

            Text.Draw(text, new Vector2(textX, textY));
        }
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
