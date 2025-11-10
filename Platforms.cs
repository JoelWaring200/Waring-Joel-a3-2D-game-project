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
        public Vector2 speed;
        public bool isforward;
        Color color;
        Vector2 Start;
        Vector2 End;
        
        
        public Platforms(float x, float y, int width, int height, Color color, Vector2 Start, Vector2 End, Vector2 speed, bool isforward)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            this.Start = Start;
            this.End = End;
            this.speed = speed;
            this.isforward = isforward;
        }
        //draws the platforms
        public void DrawRec()
        {
            Draw.FillColor = color;
            Draw.Rectangle(x, y, width, height);
        }
        public void MoveRec()
        {
            // X movement
            if (isforward)
            {
                if (x < End.X) x += speed.X;
                else x = End.X;
            }
            else
            {
                if (x > Start.X) x -= speed.X;
                else x = Start.X;
            }

            // Y movement
            if (isforward)
            {
                if (y < End.Y) y += speed.Y;
                else y = End.Y;
            }
            else
            {
                if (y > Start.Y) y -= speed.Y;
                else y = Start.Y;
            }

        }
        public void Update()
        {
            DrawRec();
            MoveRec();
        }
    }
}