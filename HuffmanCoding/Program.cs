using System.Collections;
using System.IO;
using HuffmanCoding;



Console.WriteLine("Text file path:");
string path = Console.ReadLine() ?? "";
//string text = "AAAAAABCCCCCCDDEEEEE"; //
//string text = FileManager.LoadTextFile(path);
//long lengthBefore = new System.IO.FileInfo(path).Length;
//Tree huffmanTree = new Tree(text);
//string name = new System.IO.FileInfo(path).Name;

//BitArray encoded = huffmanTree.EncodeText(text);
BitArray encoded = FileManager.LoadBinaryFile("textfile.huff");

foreach (bool b in encoded)
    Console.Write(b ? '1' : '0');
Console.WriteLine();
//FileManager.SaveBinaryFile($"{name}.huff", encoded);

// Decode
//BitArray binary = FileManager.LoadBinaryFile(path);
//Tree huffmanTree = new Tree(binary);
//string name = new System.IO.FileInfo(path).Name;
//string decoded = huffmanTree.DecodeText(binary);
//FileManager.SaveTextFile($"{name}.dec", decoded);