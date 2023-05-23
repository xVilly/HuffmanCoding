using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace HuffmanCoding
{
    internal class Log
    {
        public static void Error(string t){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[❌]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {t}");
        }
        public static void Warning(string t){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[⚠️]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {t}");
        }
        public static void Info(string t){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[✔️]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {t}");
        }
        public static void Data(string t){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(t);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    internal class FileManager
    {
        private static byte[] ConvertToByte(BitArray bits)
        {
            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
        public static string LoadTextFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                Log.Error($"File \"{FileName}\" does not exist.");
                return String.Empty;
            }
            return File.ReadAllText(FileName);
        }

        public static BitArray LoadBinaryFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                Log.Error($"File \"{FileName}\" does not exist.");
                return null;
            }
            return new BitArray(File.ReadAllBytes(FileName));
        }

        public static void SaveTextFile(string FileName, string Text)
        {
            if (!File.Exists(FileName))
            {
                Log.Error($"File \"{FileName}\" does not exist.");
                return;
            }
            File.WriteAllText(FileName, Text);
            Log.Info($"Saved text output to \"{FileName}\"");
        }

        public static void SaveBinaryFile(string FileName, BitArray Bits)
        {
            if (!File.Exists(FileName))
            {
                Log.Error($"File \"{FileName}\" does not exist.");
                return;
            }
            File.WriteAllBytes(FileName, ConvertToByte(Bits));
            Log.Info($"Saved binary output to \"{FileName}\"");
        }
    }
}
