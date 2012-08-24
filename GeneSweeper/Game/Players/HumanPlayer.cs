using System;

namespace GeneSweeper.Game.Players
{
    public class HumanPlayer:Player
    {
        public HumanPlayer(Board.Difficulty difficulty) : base(difficulty)
        {
        }

        public HumanPlayer(Board board)
            : base(board)
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
                    Board.Reveal(new GeneSweeper.Game.Board.Position(byte.Parse(input[1]), byte.Parse(input[2])));
                if (input[0][0] == 'f')
                    Board.Flag(new GeneSweeper.Game.Board.Position(byte.Parse(input[1]), byte.Parse(input[2])));
            } while (Board.CurrentState == Board.State.Playing);
            Console.WriteLine(Board.CurrentState+" "+Board.Score());
        }
    }
}
