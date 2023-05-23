using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    internal class Node
    {
        public char Character;
        public int Count;
        public Node? Left;
        public Node? Right;

        // Parametr sprawdzający czy węzeł jest liściem
        public bool IsLeaf { get { return Left == null && Right == null; } }

        // Funkcja rekurencyjna wyszukująca zakodowany kod binarny danego znaku
        public List<bool>? ReadBinaryCode(char character, List<bool> prevPath){
            if (IsLeaf){
                if (character.Equals(Character))
                    return prevPath;
                else
                    return null;
            }
            List<bool>? left = null, right = null;
            if (Left != null){
                List<bool> totalPath = new List<bool>();
                totalPath.AddRange(prevPath);
                totalPath.Add(false); // Dodaje '0' na koniec ścieżki (lewo = 0)
                left = Left.ReadBinaryCode(character, totalPath);
            }
            if (Right != null){
                List<bool> totalPath = new List<bool>();
                totalPath.AddRange(prevPath);
                totalPath.Add(true); // Dodaje '1' na koniec ścieżki (prawo = 1)
                right = Right.ReadBinaryCode(character, totalPath);
            }
            return left ?? right;
        }
    }
}
