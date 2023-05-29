using System.Collections;
using System.IO;
using HuffmanCoding;

while (true){
    Console.Clear();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("[E] Encode text file");
    Console.WriteLine("[D] Decode binary file");
    Console.WriteLine("[X] Exit application");
    string opt = Console.ReadLine() ?? "";
    switch(opt){
        case "E":
            Console.Write("Text file path: ");
            string path = Console.ReadLine() ?? "";
            if (!File.Exists(path)){
                Log.Error("File not found. Click any key to try again");
                Console.ReadKey();
                break;
            }
            long lengthBefore = new System.IO.FileInfo(path).Length;
            string name = new System.IO.FileInfo(path).FullName;
            string text = FileManager.LoadTextFile(path);
            Tree huffmanTree = new Tree(text);
            BitArray encoded = huffmanTree.EncodeText(text);
            FileManager.SaveBinaryFile($"{name}.huff", encoded);
            long lengthAfter = new System.IO.FileInfo($"{name}.huff").Length;
            double percentage = Math.Round(((double)lengthAfter / (double)lengthBefore) * 100.0 - 100, 2);
            Log.Info($"File size before: {lengthBefore}B, after: {lengthAfter}B ({(percentage>=0?"+":"")}{percentage}%)");
            Console.ReadKey();
            break;
        case "D":
            Console.Write("Binary file path: ");
            path = Console.ReadLine() ?? "";
            if (!File.Exists(path)){
                Log.Error("File not found. Click any key to try again");
                Console.ReadKey();
                break;
            }
            lengthBefore = new System.IO.FileInfo(path).Length;
            name = new System.IO.FileInfo(path).FullName;
            BitArray binary = FileManager.LoadBinaryFile(path);
            huffmanTree = new Tree(binary);
            string decoded = huffmanTree.DecodeText(binary);
            FileManager.SaveTextFile($"{name}.d", decoded);
            lengthAfter = new System.IO.FileInfo($"{name}.d").Length;
            percentage = Math.Round(((double)lengthAfter / (double)lengthBefore) * 100.0 - 100, 2);
            Log.Info($"File size before: {lengthBefore}B, after: {lengthAfter}B ({(percentage >= 0 ? "+" : "")}{percentage}%)");
            Console.ReadKey();
            break;
        case "X":
            return 0;
        default:
            Console.WriteLine("Option not found. Click any key to try again.");
            Console.ReadKey();
            break;
    }
}
