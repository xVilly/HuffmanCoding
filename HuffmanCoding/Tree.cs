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
        
        // Konstruktor budujący drzewo
        public Tree(string input){
            var CharacterOccurences = new Dictionary<char, int>();
            // Zliczenie wystąpień znaków w tekście wejściowym
            foreach (char c in input){
                if (!CharacterOccurences.ContainsKey(c))
                    CharacterOccurences.Add(c, 0);
                CharacterOccurences[c]++;
            }
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

        // Funkcja kodująca tekst na podstawie drzewa 
        public BitArray EncodeText (string raw){
            var output = new List<bool>();
            foreach (char c in raw){
                // Każdy znak jest zastąpiony przez odpowiadający mu ciąg binarny
                var encodedChar = Root?.ReadBinaryCode(c, new List<bool>());
                output.AddRange(encodedChar ?? Enumerable.Empty<bool>());
            }
            return new BitArray(output.ToArray());
        }

        // Odkodowanie tekstu
        public string DecodeText (BitArray encoded){
            var curr = Root;
            string output = String.Empty;
            foreach (bool val in encoded){
                // Wybieramy liść (będący znakiem) poprzez ciąg binarny
                curr = val ? curr?.Right : curr?.Left;
                if (curr != null && curr.IsLeaf){
                    output += curr.Character;
                    curr = Root;
                }
            }
            return output;
        }
    }
}
