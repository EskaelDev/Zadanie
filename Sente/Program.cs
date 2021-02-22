using Sente.Models;
using Sente.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Sente
{
    class Program
    {
        private static string DefaultInputPath => AppDomain.CurrentDomain.BaseDirectory + "/input/";

        static void Main(string[] args)
        {
            string inputPath = DefaultInputPath;
            if (args.Length > 0)
                inputPath = args[0];

            inputPath = Path.GetFullPath(inputPath);

            Deserializer deserializer = new Deserializer();

            var pyramid = deserializer.Deserialize<Pyramid>(inputPath + "piramida.xml");
            var bankTransfers = deserializer.Deserialize<TransferList>(inputPath + "przelewy.xml");


            if (pyramid.NoPyramid() || bankTransfers.NoTransfers())
                return;

            var transferSum = new Dictionary<int, List<decimal>>();
            foreach (var transfer in bankTransfers.Transfers)
            {
                if (transferSum.ContainsKey(transfer.From))
                    transferSum[transfer.From].Add(transfer.Amount);
                else
                    transferSum.Add(transfer.From, new List<decimal> { transfer.Amount });
            }
            pyramid.Init(transferSum);
            pyramid.Print();
            Console.ReadKey();
        }

    }
}
