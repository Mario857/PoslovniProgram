using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poslovni.Klase
{
    public delegate bool ExtendRoule(string name, decimal price );
    public class CSharpInDepth
    {
        public static string DontPassNull(string input) // Cant use without plugin
        {
            Contract.Requires(input != null);
            Contract.Ensures(Contract.Result<string>() != null);
            return input;
        }

        public static object LetsTryLambda(string Text) {
            Func<string, int> returnLength;
            //  returnLength = delegate (string text) { return text.Length;  };

            //returnLength = (string text) => text.Length;
            returnLength = text => text.Length;


            return returnLength(Text);
        }
        public static object LetsTryExpressionTrees1() {
            Expression num1 = Expression.Constant(2);
            Expression num2 = Expression.Constant(3);
            Expression sum = Expression.Add(num1, num2);
            return sum.ToString(); // Prints( 2 + 3)
        }
        public static object LetsTryExpressionTrees2() {
            Expression num1 = Expression.Constant(2);
            Expression num2 = Expression.Constant(3);
            Expression sum = Expression.Add(num1, num2);
            Func<int> compiled = Expression.Lambda<Func<int>>(sum).Compile(); //Prints 5

            return compiled();
        }

        public void SampleProductCall() {
            Product.GetSampleProducts()[0].ToString();
        }
        public void DelegatesAreFun() {
            ExtendRoule extendRoule = new ExtendRoule(ExtendR);
            extendRoule("",1);
            
        }
        public bool ExtendR(string name, decimal price) {
            if(name != "" && price != 0)
            return true;

            return false;
           
        }
        
    }

    public class Product {
        public string Name { get; private set; }
        public decimal price { get; private set; }

        public Product() { }

        public Product(string name, decimal Price,ExtendRoule ex) {
            if (ex(name, Price))
            {
                this.Name = name;
                this.price = Price;
            }
        }

        public static List<Product> GetSampleProducts() {
            return new List<Product>
            {
                new Product { Name = "Assasins", price = 9.99m},
                new Product { Name = "Toalet", price = 14.99m}
            };

        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Name, this.price);
        }
    }
    class ProductNameComparer : IComparer {
        public int Compare(object x, object y) {
            Product first = (Product)x;
            Product second = (Product)y;
            return first.Name.CompareTo(second.Name);
        }

    }
    class IsFun {
        void forFun() {
            ArrayList products = new ArrayList(Product.GetSampleProducts());
            products.Sort(new ProductNameComparer());
        }
    }

   public class GlavnaKlasa
    {
       
        public static void Glavni() {
            object[] values = { "a", "b", "c", "d" };
            IterationSample collecion = new IterationSample(values, 3);

            object someobj;
            foreach (object x in collecion)
            {
                someobj = x;
            }
              
        }
    }
    public class IterationSample : IEnumerable {
        public object[] values;
        public int startingPoint;

        public IterationSample(object[] values, int startingPoint) {
            this.values = values;
            this.startingPoint = startingPoint;
        }
        public IEnumerator GetEnumerator()
        {

            return new IterationSampleIterator(this);
        }
    }
    class IterationSampleIterator : IEnumerator {
        IterationSample parent;
        int position;

        internal IterationSampleIterator(IterationSample parent) {
            this.parent = parent;
            position = - 1;
        }


        public bool MoveNext() {
            if (position != parent.values.Length) {
                position++;
            }
            return position < parent.values.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current {

            get {
                if (position == -1 ||
                    position == parent.values.Length)

                {
                    throw new InvalidOperationException();
                }
                int index = position + parent.startingPoint;
                index = index % parent.values.Length;
                return parent.values[index];
                     
            }
        }
    }
    
}
