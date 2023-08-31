using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PingPongGame
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Ping Pong Game");
            window.SetFramerateLimit(60);

            int player1Score = 0;
            int player2Score = 0;

            Font font = new Font("C:\\Users\\Роман\\source\\repos\\Ping-pong\\ArialRegular.ttf"); // Почему-то требует полный адрес, хотя файл находится в папке проекта (」°ロ°)」

            Text player1ScoreText = new Text("Player 1: " + player1Score, font, 30);
            player1ScoreText.Position = new Vector2f(20, 20);
            player1ScoreText.FillColor = Color.White;

            Text player2ScoreText = new Text("Player 2: " + player2Score, font, 30);
            player2ScoreText.Position = new Vector2f(window.Size.X - 200, 20);
            player2ScoreText.FillColor = Color.White;

            RectangleShape player1 = new RectangleShape(new Vector2f(15, 100));
            player1.Position = new Vector2f(50, window.Size.Y / 2 - player1.Size.Y / 2);
            player1.FillColor = Color.White;

            RectangleShape player2 = new RectangleShape(new Vector2f(15, 100));
            player2.Position = new Vector2f(window.Size.X - 50 - player2.Size.X, window.Size.Y / 2 - player2.Size.Y / 2);
            player2.FillColor = Color.Red;

            CircleShape ball = new CircleShape(10);
            ball.Position = new Vector2f(window.Size.X / 2 - ball.Radius, window.Size.Y / 2 - ball.Radius);
            ball.FillColor = Color.Blue;

            Vector2f ballVelocity = new Vector2f(5, 3);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                // Движение игроков
                if (Keyboard.IsKeyPressed(Keyboard.Key.W) && player1.Position.Y > 0)
                    player1.Position -= new Vector2f(0, 5);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S) && player1.Position.Y < window.Size.Y - player1.Size.Y)
                    player1.Position += new Vector2f(0, 5);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && player2.Position.Y > 0)
                    player2.Position -= new Vector2f(0, 5);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && player2.Position.Y < window.Size.Y - player2.Size.Y)
                    player2.Position += new Vector2f(0, 5);

                // Движение шарика
                ball.Position += ballVelocity;

                // Столкновение мяа со стенками
                if (ball.Position.Y < 0 || ball.Position.Y > window.Size.Y - ball.Radius * 2)
                    ballVelocity.Y = -ballVelocity.Y;

                // Столкновение мяча с игроками
                if (ball.GetGlobalBounds().Intersects(player1.GetGlobalBounds()) || ball.GetGlobalBounds().Intersects(player2.GetGlobalBounds()))
                    ballVelocity.X = -ballVelocity.X;

                // Когда мяч за пределами поля
                if (ball.Position.X < 0)
                {
                    ball.Position = new Vector2f(window.Size.X / 2 - ball.Radius, window.Size.Y / 2 - ball.Radius);
                    ballVelocity = new Vector2f(-ballVelocity.X, ballVelocity.Y);
                    player2Score++;
                    player2ScoreText.DisplayedString = "Player 2: " + player2Score;
                }
                else if (ball.Position.X > window.Size.X)
                {
                    ball.Position = new Vector2f(window.Size.X / 2 - ball.Radius, window.Size.Y / 2 - ball.Radius);
                    ballVelocity = new Vector2f(-ballVelocity.X, ballVelocity.Y);
                    player1Score++;
                    player1ScoreText.DisplayedString = "Player 1: " + player1Score;
                }

                window.Clear();

                window.Draw(player1);
                window.Draw(player2);
                window.Draw(ball);
                window.Draw(player1ScoreText);
                window.Draw(player2ScoreText);

                window.Display();
            }
        }
    }
}
