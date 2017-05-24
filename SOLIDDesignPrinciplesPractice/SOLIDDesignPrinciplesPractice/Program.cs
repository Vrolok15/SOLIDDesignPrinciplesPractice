using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciplesPractice
{
    // Liskov substitution principle

    public class Rectangle
    {
        //Use virtual properties!
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }


    public class Square : Rectangle
    {
        //Use override!
        public override int Width
        {
            set { base.Width = base.Height = value;  }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    class Program
    {
        public static int Area(Rectangle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(4, 6);
            Console.WriteLine($"{rc} has area of {Area(rc)}");

            // Objects in a program should be replaceable with instances of their subtypes 
            // without altering the correctness of that program!
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area of {Area(rc)}");

            Console.ReadLine();
        }
    }
}
