using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_game_model
{
    internal class Game
    {
        public List<Card> cards;
        public List<Player> gamer;

        private Random rnd = new Random();
        private int Cards = 36;
        public Game(int playersCount = 2)
        {       
            gamer = new List<Player>();
            for (int i = 0; i < playersCount; i++)
            {
                gamer.Add(new Player());
            }
           
            cards = DeckCard();
            Shuffle(cards);      
            DealCards(gamer, cards);
        }

        public List<Card> DeckCard()
        {
            cards = new List<Card>();
            int  number = Cards / 4;

            for (int i = 0; i < number; i++)
            {
                cards.Add(new Card((Rank)i, (Suit)0));
                cards.Add(new Card((Rank)i, (Suit)1));
                cards.Add(new Card((Rank)i, (Suit)2));
                cards.Add(new Card((Rank)i, (Suit)3));
            }
            return cards;
        }

        public void Shuffle(List<Card> cards)
        {
            cards.Sort((one, two) => rnd.Next(-2, 2));
        }

        public void DealCards(List<Player> gamer, List<Card> cards)
        {
            int player = 0;
            
            for (int i = 0; i < cards.Count; i++)
            {
                gamer[player].cards.Add(cards[i]);

                player++;
                player = player% + gamer.Count;
            }
        }

        public bool Show()
        {
            Console.WriteLine("Роздача карт:");
                    
            int maxValue = -1;
            Player MaxRank = null;
            Stack<Card> Stack = new Stack<Card>();

            for (int i = 0; i < gamer.Count; i++)
            {
                Player player = gamer[i];

                if (player.cards.Count > 0)
                {
                    Card card = player.cards[rnd.Next(player.cards.Count)];

                    Console.WriteLine($"Iгрок {i}\nкiлькiсть карт {player.cards.Count}\nхiд {card}\n");
                    player.cards.Remove(card);

                    if ((int)card.rank > maxValue)
                    {
                        maxValue = (int)card.rank;
                        MaxRank = player;
                    }

                    Stack.Push(card);

                }
            }

            MaxRank.cards.AddRange(Stack);
            Console.WriteLine($"Карти отримує iгрок {gamer.IndexOf(MaxRank)}.\n");
          
            if (MaxRank.cards.Count == Cards)
            {
                Console.WriteLine($"Перемога за iгроком {gamer.IndexOf(MaxRank)}\n\n");
                return false;
            }
            return true;
        }
    }

    public class Player
    {
        public List<Card> cards = new List<Card>();
    }

    public enum Rank
    {
        Шiсть, Сiм, Вiсiм, Дев_ять, Десять, Король, Валет, Дама, Туз
    }
   
    public enum Suit
    {
        Черва, Пiка, Трефа, Бубна 
    }
    public class Card
    {
        public Rank rank;
        public Suit suit;

        public Card(Rank rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public override string ToString()
        {
            return $"{rank} {suit}";
        }

        class Program
        {
            static void Main()
            {
                Game game = new Game(6);
                //game.Show();
                while (game.Show())
                {
                    Console.WriteLine();
                }
            }
        }
    }
}


