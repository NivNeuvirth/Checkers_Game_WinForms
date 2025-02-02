namespace UI
{
    public class Program
    {
        private static void Main()
        {
            startGame();
        }

        private static void startGame()
        {
            GameUI checkersGame = new GameUI();
            checkersGame.Play();
        }
    }
}