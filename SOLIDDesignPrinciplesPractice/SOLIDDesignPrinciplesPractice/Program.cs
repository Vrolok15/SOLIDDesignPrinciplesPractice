using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDDesignPrinciplesPractice
{
    public class Document
    {
        
    }

    // WRONG way to build interfaces!
    // One BIG inteface for every type of printer and scanner...
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Copy(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //Print
        }
        public void Scan(Document d)
        {
            //Scan
        }
        public void Copy(Document d)
        {
            //Copy
        }
        public void Fax(Document d)
        {
            //Fax
        }
    }

    // Have to implement useless methods!
    public class ClassicPrinter : IMachine
    {
        public void Print(Document d)
        {
            //Print
        }
        public void Scan(Document d)
        {
            //CAN'T Scan!
        }
        public void Copy(Document d)
        {
            //CAN'T Copy!
        }
        public void Fax(Document d)
        {
            //CAN'T Fax!
        }

    }

    // Correct way to build interfaces :D
    // Many intefaces for every function!
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface ICopier
    {
        void Copy(Document d);
    }

    public interface IFaxMachine
    {
        void Fax(Document d);
    }

    //Another old school printer
    public class AnotherClassicPrinter : IPrinter
    {
        public void Print(Document d)
        {
            //Print
        }
    }

    //Printer + Fax
    public class FaxAndPrinter : IPrinter, IFaxMachine
    {
        public void Print(Document d)
        {
            //Print
        }
        public void Fax(Document d)
        {
            //Fax
        }
    }

    //Can also implement new interfaces from smaller interfaces! 
    public interface IMultiFunctionMachine : IPrinter, IScanner, ICopier, IFaxMachine
    {

    }

    //Another cool modern printer
    public class AnotherMultiFunctionPrinter : IMultiFunctionMachine
    {
        public void Print(Document d)
        {
            //Print
        }
        public void Scan(Document d)
        {
            //Scan
        }
        public void Copy(Document d)
        {
            //Copy
        }
        public void Fax(Document d)
        {
            //Fax
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
