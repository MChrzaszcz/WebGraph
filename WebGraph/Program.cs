using System;


namespace WebGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphGenerator graphGenerator = new GraphGenerator();
            graphGenerator.starGenerateGraph();
            Console.Read();
        }

    }
}
