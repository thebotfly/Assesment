using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace Assessment
{
    public class Enemy : Actor
    {
        private Actor _target;
        private Sprite _sprite;

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/tankBlack");
        }

        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/tankBlack");
            
        }

        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            //Checks if the target has a value before continuing
            if (Target == null)
                return false;

            //Find the vector representing the distance between the actor and its target
            Vector2 direction = Target.LocalPosition - LocalPosition;
            //Get the magnitude of the distance vector
            float distance = direction.Magnitude;
            //Use the inverse cosine to find the angle of the dot product in radians
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            //Return true if the angle and distance are in range
            if (angle <= maxAngle && distance <= maxDistance)
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            //If the target can be seen change the color to red
            //If the target can't be seen change the color to blue
            if (CheckTargetInSight(1.5f, 5))
            {
                _rayColor = Color.RED;
            }
            else
            {
                _rayColor = Color.BLUE;
                base.Update(deltaTime);
            }
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }
    }
}