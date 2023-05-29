using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    internal class Tree
    {
        // Korzeń drzewa (i lista pomocnicza z węzłami)
        public Node? Root;
        private List<Node> nodes = new List<Node>();
        private int decodeOffset = 0;
        private const int charSize = 16;
        private int charCount = 0;
        
        // Konstruktor budujący drzewo z tekstu
        public Tree(string input){
            var CharacterOccurences = new Dictionary<char, int>();
            // Zliczenie wystąpień znaków w tekście wejściowym
            foreach (char c in input){
                if (!CharacterOccurences.ContainsKey(c))
                    CharacterOccurences.Add(c, 0);
                CharacterOccurences[c]++;
            }
            charCount = CharacterOccurences.Count;
            // Utworzenie pierwszych węzłów
            foreach (var c in CharacterOccurences)
                nodes.Add(new Node() { Character = c.Key, Count = c.Value });
            // Budowa drzewa na podstawie pierwszych węzłów
            while (nodes.Count > 1){
                // Posortowana lista wystąpień
                var pN = nodes.OrderBy(n => n.Count).ToList(); 
                if (pN.Count >= 2){
                    // Scalanie dwóch węzłów o najmniejszej ilości
                    var p = pN.Take(2).ToList();
                    Node fused = new Node() {Count = p[0].Count + p[1].Count, Left = p[0], Right = p[1] };
                    // Przekształcenie drzewa (zastąpienie 2 węzłów jednym)
                    nodes.Remove(p[0]);
                    nodes.Remove(p[1]);
                    nodes.Add(fused);
                }
                Root = nodes.FirstOrDefault();
            }
        }

        private Node ReadNode(ref BitArray input, ref int offset){
            if (input[offset]){
                offset++;
                string _charBits = String.Empty;
                for (int i=0; i<charSize; i++)
                    _charBits+=input[offset+i]?'1':'0';
                char c = (char)Convert.ToInt16(_charBits, 2);
                offset+=charSize;
                return new Node(){Character=c, Left=null, Right=null};
            } else {
                offset++;
                Node leftChild = ReadNode(ref input, ref offset);
                Node rightChild = ReadNode(ref input, ref offset);
                return new Node(){Left=leftChild, Right=rightChild};
            }
        }

        // Konstruktor budujący drzewo z kodu binarnego
        public Tree(BitArray input){
            int offset = 0;
            Root = ReadNode(ref input, ref offset);
            Log.Info($"Node reading complete, huffman tree rebuilt with decode offset {offset} bits.");
            decodeOffset = offset;
        }

        // Funkcja kodująca drzewo huffmana
        private void EncodeNode(Node node, ref List<bool> output){
            if (node.IsLeaf){
                output.Add(true);
                string _cbs = Convert.ToString(node.Character, 2).PadLeft(charSize, '0');
                foreach(char _c in _cbs)
                    output.Add(_c=='1');
            } else {
                output.Add(false);
                EncodeNode(node.Left, ref output);
                EncodeNode(node.Right, ref output);
            }
        }

        // Funkcja kodująca tekst na podstawie drzewa 
        public BitArray EncodeText (string raw){
            var output = new List<bool>();
            EncodeNode(Root, ref output);
            Log.Info($"Saved huffman tree of total length {output.Count()} bits ({charCount} unique chars).");
            // Zapis binarny tekstu
            foreach (char c in raw){
                // Każdy znak jest zastąpiony przez odpowiadający mu ciąg binarny
                var encodedChar = Root?.ReadBinaryCode(c, new List<bool>());
                output.AddRange(encodedChar ?? Enumerable.Empty<bool>());
            }
            Log.Info($"Saved encoded text of total length {output.Count()} bits ({raw.Length} total chars).");
            return new BitArray(output.ToArray());
        }

        // Odkodowanie tekstu
        public string DecodeText (BitArray encoded){
            
            var curr = Root;
            string output = String.Empty;
            const int _trailingBits = 0;
            for (int i=decodeOffset; i < encoded.Length; i++){
                bool val = encoded[i];
                // Wybieramy liść (będący znakiem) poprzez ciąg binarny
                curr = val ? curr?.Right : curr?.Left;
                if (curr != null && curr.IsLeaf){
                    if (i > encoded.Length - _trailingBits)
                        break;
                    output += curr.Character;
                    curr = Root;
                }
            }
            Log.Info($"Decoded text of total bit length {encoded.Length-decodeOffset}, offset {decodeOffset}, characters {output.Length}");
            return output;
        }
    }
}
