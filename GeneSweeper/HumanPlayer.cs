using System;

namespace GeneSweeper
{
    public class HumanPlayer:Player
    {
        public HumanPlayer(Board.Difficulty difficulty) : base(difficulty)
        {
        }

        public HumanPlayer(Board board) : base(board)
        {
        }

        public override void Play()
        {
            do
            {
                Console.Clear();
                Console.WriteLine(Board);
                string[] input = (Console.ReadLine() ?? "").Split(' ');
                if (input[0][0] == 'r')
                    Board.Reveal(byte.Parse(input[1]), byte.Parse(input[2]));
                if (input[0][0] == 'f')
                    Board.Flag(int.Parse(input[1]), int.Parse(input[2]));
            } while (Board.CurrentState == Board.State.Playing);
            Console.WriteLine(Board.CurrentState);
        }
    }
}
