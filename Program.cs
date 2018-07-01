using FlitBit.Copy;
using System;

namespace quadient_test
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Write object id, id should be a numeric");
                string row = Console.ReadLine();
                var request = SampleObject.InitObject(int.Parse(row));

                var watch = new System.Diagnostics.Stopwatch();

                #region targetFramework net452
                //watch.Start();
                //var newObj = new SampleObject().CopyFrom(request);
                //watch.Stop();
                //Console.WriteLine($"FlitBit.Copy ticks: {watch.ElapsedTicks}");

                //watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                //var newObjInt = new SampleObject().CopyFromFlitBitImplementation(request);
                //watch.Stop();
                //Console.WriteLine($"CopyFromFlitBitImplementation ticks: {watch.ElapsedTicks}");

                //watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                //var newObjClone = new SampleObject().CopyFromReflection(request);
                //watch.Stop();
                //Console.WriteLine($"CopyFromReflection ticks: {watch.ElapsedTicks}");
                #endregion

                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var newObjSerializable = new SampleObject().CopyFromSerializable(request);
                watch.Stop();
                Console.WriteLine($"CopyFromSerializable ticks: {watch.ElapsedTicks}");

                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var newObjJson = new SampleObject().CopyFromJson(request);
                watch.Stop();
                Console.WriteLine($"CopyFromJson ticks: {watch.ElapsedTicks}");

                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var newObjJObject = new SampleObject().CopyFromJObject(request);
                watch = System.Diagnostics.Stopwatch.StartNew();
                Console.WriteLine($"CopyFromJObject ticks: {watch.ElapsedTicks}");
            }
        }
    }
}
