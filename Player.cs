using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace Assessment
{
    public class Player : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;
        private bool _canMove = true;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/playerShip1_green.png");
        }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/playerShip1_green.png");
        }

        public void DisableControls()
        {
            _canMove = false;
        }

        public override void Start()
        {
            GameManager.onWin += DisableControls;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //If the player can't move, don't ask for input.
            if (!_canMove)
                return;

            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Engine.GetKeyDown((int)KeyboardKey.KEY_S));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Acceleration = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }
    }
}
