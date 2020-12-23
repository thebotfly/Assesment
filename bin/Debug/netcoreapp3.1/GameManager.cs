using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment
{
    delegate void GameEvent();
    static class GameManager
    {
        private static bool _gameOver = false;

        public static GameEvent onWin;

        public static bool GameOver { get => _gameOver; set => _gameOver = value; }
    }
}
