using System.Collections;
using System.IO;
using HuffmanCoding;

Console.WriteLine("Text file path:");
string path = Console.ReadLine() ?? "";
string text = FileManager.LoadTextFile(path);
long lengthBefore = new System.IO.FileInfo(path).Length;
Tree huffmanTree = new Tree(text);
string name = new System.IO.FileInfo(path).Name;

// Encode
BitArray encoded = huffmanTree.EncodeText(text);
FileManager.SaveBinaryFile($"{name}.huff", encoded);

// Decode