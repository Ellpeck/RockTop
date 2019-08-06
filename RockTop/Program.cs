namespace RockTop {
    internal static class Program {

        public static void Main(string[] args) {
            using (var game = new GameImpl())
                game.Run();
        }

    }
}