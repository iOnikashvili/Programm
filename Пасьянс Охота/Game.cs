using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Пасьянс_Охота
{

    public class Game
    {
        private List<Stack<Card>> Kolodi;
        private Stack<Tuple<Card, Card>> DumpDeck;
        public delegate void GameChanged(bool isWin);
        public event GameChanged ChangeNotification = win => { };

        public void Start() => RefreshGame();
        public void RefreshGame()
        {
            Kolodi = Kolody();
            DumpDeck = new Stack<Tuple<Card, Card>>();
            ChangeNotification(false);
        }

        public void TryDumpCards(int firstDeckIndex, int secondDeckIndex)
        {
            if (Kolodi[firstDeckIndex].Count * Kolodi[secondDeckIndex].Count == 0 || firstDeckIndex == secondDeckIndex)
                return;
            if (Kolodi[firstDeckIndex].Peek().EqualsByValue(Kolodi[secondDeckIndex].Peek()))
            {
                DumpDeck.Push(Tuple.Create(Kolodi[firstDeckIndex].Pop().WithChangedStackNumber(firstDeckIndex),
                                    Kolodi[secondDeckIndex].Pop().WithChangedStackNumber(secondDeckIndex)));
            }
            ChangeNotification(Kolodi.Sum(s => s.Count) == 0);
        }

        public void Undo()
        {
            if (DumpDeck.Count == 0)
                return;
            var tuple = DumpDeck.Pop();
            Kolodi[tuple.Item1.stackNumber].Push(tuple.Item1);
            Kolodi[tuple.Item2.stackNumber].Push(tuple.Item2);
            ChangeNotification(false);
        }

        private List<Stack<Card>> Kolody()
        {
            var Koloda = new List<Card>();
            for (int value = 0; value < 9; value++)
                for (int suit = 0; suit < 4; suit++)
                    Koloda.Add(new Card(value, suit));
            //Sam ty kukold
            var randomSeed = new Random();
            var random = new Random(randomSeed.Next());
            for (int i = 0; i < 200; i++)
            {
                var firstIndex = random.Next() % 36;
                var secondIndex = random.Next() % 36;
                var card = Koloda[firstIndex];
                Koloda[firstIndex] = Koloda[secondIndex];
                Koloda[secondIndex] = card;
            }

            var Kolodi = new List<Stack<Card>>();
            for(int i = 0; i < 9; i++)
                Kolodi.Add(new Stack<Card>(Koloda.GetRange(i * 4, 4)));
            return Kolodi;
        }

        public IEnumerable<Card?> Datb_Topovye_kolody()
        {
            foreach (var Koloda in Kolodi)
            {
                if (!Koloda.TryPeek(out var result))
                    yield return null;
                else yield return result;
            }

            if (!DumpDeck.TryPeek(out var res))
            {
                yield return null;
                yield break;
            }
            yield return res.Item2;
        }
    }
}
