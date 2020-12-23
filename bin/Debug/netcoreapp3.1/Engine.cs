using Raylib_cs;
using System;
namespace Assessment
{
    public class Engine
    {
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        public static int CurrentSceneIndex
        {
            get
            {
                return _currentSceneIndex;
            }
        }

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

        public static int AddScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return -1;

            //Create a new temporary array that one size larger than the original
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy values from old array into new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Store the current index
            int index = _scenes.Length;

            //Sets the scene at the new index to be the scene passed in
            tempArray[index] = scene;

            //Set the old array to the tmeporary array
            _scenes = tempArray;

            return index;
        }

        public static bool RemoveScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            //Create a new temporary array that is one less than our original array
            Scene[] tempArray = new Scene[_scenes.Length - 1];

            //Copy all scenes except the scene we don't want into the new array
            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            //If the scene was successfully removed set the old array to be the new array
            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        }

        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        public Engine()
        {
            _scenes = new Scene[0];
        }

        public void Start()
        {
            //Creates a new window for raylib
            Raylib.InitWindow(1024, 760, "Space Flight");
            Raylib.SetTargetFPS(60);

            //Set up console window
            Console.CursorVisible = false;
            Console.Title = "Space Flight";

            //Create a new scene for our actors to exist in
            Scene scene1 = new Scene();
            Scene scene2 = new Scene();

            //Create the actors to add to our scene
            Goal actor = new Goal(10, 20, Color.GREEN, '■', ConsoleColor.Green);
            Enemy enemy = new Enemy(10, 10, Color.GREEN, '*', ConsoleColor.Green);
            Player player = new Player(0, 1, Color.BLUE, '@', ConsoleColor.Red);

            //Sets actors starting values
            enemy.Target = player;
            enemy.SetTranslation(new Vector2(5, 0));
            player.Speed = 5;

            //Add actors to the scenes
            scene1.AddActor(player);
            scene1.AddActor(actor);
            scene1.AddActor(enemy);
            scene2.AddActor(player);

            //Sets the starting scene index and adds the scenes to the scenes array
            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);
            AddScene(scene2);

            //Sets the current scene to be the starting scene index
            SetCurrentScene(startingSceneIndex);
        }

        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }

        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }

        public void Run()
        {
            //Call start for all objects in game
            Start();


            //Loops the game until either the game is set to be over or the window closes
            while (!GameManager.GameOver && !Raylib.WindowShouldClose())
            {
                //Stores the current time between frames
                float deltaTime = Raylib.GetFrameTime();
                //Call update for all objects in game
                Update(deltaTime);
                //Call draw for all objects in game
                Draw();
                //Clear the input stream for the console window
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }
            End();
        }
    }
}
        
    