using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment
{
    public class Goal : Actor
    {
        public Goal(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) : base(x, y, icon, color)
        {
            _collisionRadius = 5;
        }

        public Goal(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White): base(x,y, rayColor, icon, color)
        {
            _collisionRadius = 5;
        }

        public override void Draw()
        {
            Raylib.DrawCircle((int)(WorldPosition.X * 50), (int)(WorldPosition.Y * 30), _collisionRadius * 30, _rayColor);
            Raylib.DrawText("Base", ((int)(WorldPosition.X * 50) - 100), ((int)(WorldPosition.Y * 30) - 75), 100, Color.BLUE);
            base.Draw();
        }

        public override void End()
        {
            base.End();
        }

        public override void OnCollision(Actor other)
        {
            if (other is Player)
                GameManager.onWin?.Invoke();

            base.OnCollision(other);
        }

        public override void Start()
        {
            GameManager.onWin += DrawWinText;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        private void DrawWinText()
        {
            Raylib.DrawText("You Win!!\nPress Esc to quit", 100, 100, 100, Color.BLUE);
        }
    }
}
