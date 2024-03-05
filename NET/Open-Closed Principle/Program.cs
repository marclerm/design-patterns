using System;
using System.Collections.Generic;

namespace Open_Closed_Principle
{
    class Program
    {
        public enum Color
        {
            Red, Green, Blue
        }
        public enum Size
        {
            Small, Medium, Large
        }

        public class Product
        {
            public string Name;
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(paramName: nameof(name));
                }
                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach(var p in products)
                {
                    if (p.Size == size)
                        yield return p;
                }
            }
            public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Color == color)
                        yield return p;
                }
            }

            public static IEnumerable<Product> FilterBySizeColor(IEnumerable<Product> products,Size size, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Color == color && p.Size == size)
                        yield return p;
                }
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisifed(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            Color color;
            public ColorSpecification(Color color)
            {
                this.color = color;
            }
            public bool IsSatisifed(Product t)
            {
                return t.Color == this.color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }
            public bool IsSatisifed(Product t)
            {
                return t.Size == this.size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first, second;
            
            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
                this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            }
            public bool IsSatisifed(T t)
            {
                return first.IsSatisifed(t) && second.IsSatisifed(t);
            }
        }
        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
               foreach(var i in items)
                {
                    if (spec.IsSatisifed(i))
                        yield return i;
                }
            }
        }
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Medium);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };         
            Console.WriteLine("Green products (old):");          
            foreach(var p in ProductFilter.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($"-{p.Name}");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (better):");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"-{p.Name}");
            }

            Console.WriteLine("Large blue items");
            foreach(var p in bf.Filter(products, 
                new AndSpecification<Product>( new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))
                ))
            {
                Console.WriteLine($"-{p.Name}");
            }
            Console.ReadKey();
        }
    }
}
