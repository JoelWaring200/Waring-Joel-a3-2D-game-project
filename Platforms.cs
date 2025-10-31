using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Platforms
    {
        public float x;
        public float y;
        public int width;
        public int height;
        Color color;
        Vector2 Start;
        Vector2 End;
        public Vector2 speed;
        bool forwardX;
        bool forwardY;
        public Platforms(float x, float y, int width, int height, Color color, Vector2 Start, Vector2 End, Vector2 speed) 
        { 
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            this.Start = Start;
            this.End = End;
            this.speed = speed;
            forwardX = true;
            forwardY = true;
        }
        public void DrawRec()
        {
            Draw.FillColor = color;
            Draw.Rectangle(x, y, width, height);
        }
        public void MoveRec()
        {
            
            if (forwardX)
            {
                if (x + width < End.X)
                {
                    x += speed.X;
                }
                if (x + width >= End.X)
                {
                    forwardX = false;
                    x = End.X - width; 
                }
            }
            if (forwardY)
            {
                if (y + height < End.Y)
                {
                    y += speed.Y;
                }
                if (y + height >= End.Y)
                {
                    forwardY = false;
                    y = End.Y - height;
                }
            }
            if (!forwardX)
            {
                if (x > Start.X)
                {
                    x -= speed.X;
                }
                if (x <= Start.X)
                {
                    forwardX = true;
                    x = Start.X;
                }
            }
            if (!forwardY)
            {
                if (y > Start.Y)
                {
                    y -= speed.Y;
                }
                if (y <= Start.Y)
                {
                    forwardY = true;
                    y = Start.Y;
                }
            }

        }
        public void Update()
        {
            DrawRec();
            MoveRec();
        }
    }
}
