using System.Collections.Generic;

public class GameRules
{
    private readonly Dictionary<int, List<int>> winningMoves;

    public GameRules(int numberOfMoves)
    {
        winningMoves = new Dictionary<int, List<int>>();
        GenerateWinningMoves(numberOfMoves);
    }

    private void GenerateWinningMoves(int numberOfMoves)
    {
        for (int i = 1; i <= numberOfMoves; i++)
        {
            List<int> movesThatWin = new List<int>();
            for (int j = 1; j <= numberOfMoves; j++)
            {
                if (i != j && (j - i + numberOfMoves) % numberOfMoves <= numberOfMoves / 2)
                {
                    movesThatWin.Add(j);
                }
            }
            winningMoves[i] = movesThatWin;
        }
    }

    public bool IsPlayerWin(int playerMove, int computerMove)
    {
        return winningMoves[playerMove].Contains(computerMove);
    }
}
