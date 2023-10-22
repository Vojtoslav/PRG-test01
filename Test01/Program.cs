namespace Test01
{
    class Program
    {

        private static int _jump = 0;
        private static int _crouch = 0;
        private static int _sceneWidth = 80;
        private static int _sceneHeight = 60;
        private static Random _random = new Random();

        static void ProcessInput()
        {
            if (_jump > 0)
            {
                _jump--;
            }
            if (_crouch > 0)
            {
                _crouch--;
            }

            while (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar.Equals('w'))
                {
                    _jump = 5;
                }
                else
                {
                    if (key.KeyChar.Equals('s'))
                    {
                        _crouch = 5;
                    }
                }
            }
        }

        static GameObject RandomObstacle(Vector minBounds, Vector maxBounds, float X)
        {
            if (_random.NextDouble() > 0.5)
            {
                Meteor meteor = new Meteor(new Vector(X, _sceneHeight - 15));
                meteor.MinBounds = minBounds;
                meteor.MaxBounds = maxBounds;

                return meteor;
            }
            else
            {
                Cactus cactus = new Cactus(new Vector(X, _sceneHeight - 6));
                cactus.MinBounds = minBounds;
                cactus.MaxBounds = maxBounds;

                return cactus;
            }
        }

        static void Create()
        {

        }

        static void Main(string[] args)
        {
            bool again = true;
            while (again)
            {
                Vector minBounds = new Vector(0, 0);
                Vector maxBounds = new Vector(_sceneWidth, _sceneHeight);

                Scene scene = new Scene(_sceneWidth, _sceneHeight);
                Player player = new Player(new Vector(3, 0));
                player.MinBounds = minBounds;
                player.MaxBounds = maxBounds;
                GameObject obstacle1 = RandomObstacle(minBounds, maxBounds, _sceneWidth);
                Action onObstacle1Destroy = null;
                onObstacle1Destroy = () =>
                {
                    obstacle1 = RandomObstacle(minBounds, maxBounds, _sceneWidth);
                    obstacle1.OnDestroy = onObstacle1Destroy;
                };
                obstacle1.OnDestroy = onObstacle1Destroy;
                GameObject obstacle2 = RandomObstacle(minBounds, maxBounds, (int)(_sceneWidth * 1.5));
                Action onObstacle2Destroy = null;
                onObstacle2Destroy = () =>
                {
                    obstacle2 = RandomObstacle(minBounds, maxBounds, _sceneWidth);
                    obstacle2.OnDestroy = onObstacle2Destroy;
                };
                obstacle2.OnDestroy = onObstacle2Destroy;
                int score = 0;

                while (true)
                {
                    ProcessInput();
                    if (_jump > 0)
                    {
                        player.Jump();
                    }
                    else if (_crouch > 0)
                    {
                        player.Crouch();
                    }
                    else
                    {
                        player.StandUp();
                    }
                    obstacle1.Move();
                    obstacle2.Move();
                    player.Move();

                    scene.Draw(player, obstacle1, obstacle2);

                    if (player.CollideWith(obstacle1) || player.CollideWith(obstacle2))
                    {
                        Console.WriteLine("Would you like to go again (yes/no)?");
                        string response = Console.ReadLine();
                        if (response.ToLower() == "yes")
                        {
                            break;
                        }
                        else {
                            again = false;
                            break;
                        }
                    }

                    score++;
                    Console.WriteLine("score: " + score);
                    Thread.Sleep(100);
                }
            }
        }
    }
}