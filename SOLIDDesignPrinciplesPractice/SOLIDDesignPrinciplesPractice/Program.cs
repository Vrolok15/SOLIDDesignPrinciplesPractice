using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciplesPractice
{
    public enum Color
    {
        Red, Green, Blue, Yellow
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    //
    // !!! WRONG WAY TO IMPLEMENT FILTERING:
    //

    public class ProductFilter
    {
        //First filter added

        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                {
                    yield return p;
                }
            }
        }

        //
        //!!! By adding more filters, I am violating the Open/closed principle
        //    Thus there needs to be another solution (Inheritance!)
        //

        //Second filter added

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                {
                    yield return p;
                }
            }
        }

        //Third filter added

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color)
                {
                    yield return p;
                }
            }
        }


    }


    //
    // +++ CORRECT WAY TO IMPLEMENT FILTERING:
    //


    public interface ISpecification<T>
    {
        //Checks if product meets specifications
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        //Filter interface that works with any type T
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    //
    // Specifications instead of new filters -> can add as many as you like!
    //

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    // Check for two specifications:

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(paramName: nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(paramName: nameof(second));
            }
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    // New Filter:

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                {
                    yield return i;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var lemon = new Product("Lemon", Color.Yellow, Size.Small);
            var watermelon = new Product("Watermelon", Color.Green, Size.Large);

            Product[] products = {apple, lemon, watermelon};


            //Old method
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old): ");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($"- {p.Name} is Green;");
            }
            Console.WriteLine("");

            //New method
            var bf = new BetterFilter();
            Console.WriteLine("Green products (new): ");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"- {p.Name} is Green;");
            }
            Console.WriteLine("");

            Console.WriteLine("Large Green products (new): ");
            foreach (var p in bf.Filter(
                products, 
                new AndSpecification<Product>(
                new ColorSpecification(Color.Green), 
                new SizeSpecification(Size.Large)
                )))
            {
                Console.WriteLine($"- {p.Name} is Large and Green;");
            }

            Console.ReadLine();
        }
    }
}
