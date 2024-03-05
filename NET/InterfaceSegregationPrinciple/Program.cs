using System;

namespace InterfaceSegregationPrinciple
{
    public class Document
    {

    }
    public interface IMachine
    {
        void Print(Document doc);
        void Scan(Document doc);
        void Fax(Document doc);
    }

    public class MultiFunctionPrinter: IMachine
    {
        public void Fax(Document doc)
        {
           
        }

        public void Print(Document doc)
        {
            
        }

        public void Scan(Document doc)
        {
           
        }
    }

    public class OldFashion : IMachine
    {
        public void Fax(Document doc)
        {
            throw new NotImplementedException();
        }

        public void Print(Document doc)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document doc)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner
    {

    }

    public class MultiFunctionMachine: IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
