using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame.Manager
{
    public static class InputManager
    {
        private static KeyboardState _previousKeyboard;
        private static MouseState _previousMouse;
        private static GamePadState _previousGamePad;

        public static void Update()
        {
            #region Touch Input Handling
            var touchCollections = TouchPanel.GetState();
            foreach (var touch in touchCollections)
            {
                switch (touch.State)
                {
                    case TouchLocationState.Pressed:
                        // Handle touch press event
                        // StartTouch(touch.id, touch.Position);
                        break;
                    case TouchLocationState.Moved:
                        // Handle touch move event
                        // MoveTouch(touch.id, touch.Position);
                        break;
                    case TouchLocationState.Released:
                        // Handle touch release event
                        // EndTouch(touch.id, touch.Position);
                        break;
                }
            }

            while(TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                        // Handle tap gesture
                        // HandleTap(gesture.Position);
                        break;
                    case GestureType.FreeDrag:
                        // Handle free drag gesture
                        // PanCamera(gesture.Delta);
                        break;
                    case GestureType.Pinch:
                        // Handle pinch gesture
                        // ZoomCamera(gesture.Delta.X);
                        break;
                }
            }
            #endregion

            #region keyboard mouse gamepad input handling
            // TODO: Add your update logic here
            var currentKeyboard = Keyboard.GetState();
            var currentMouse = Mouse.GetState();
            var currentGamePad = GamePad.GetState(PlayerIndex.One);

            // store the current state for the next frame
            var previousKeyboard = _previousKeyboard;
            var previousMouse = _previousMouse;
            var previousGamePad = _previousGamePad;

            bool IsKeyPressed(Keys key) => currentKeyboard.IsKeyDown(key) && previousKeyboard.IsKeyDown(key);
            bool IsKeyDown(Keys key) => currentKeyboard.IsKeyDown(key);

            // get position of mouse
            var mousePosition = new Point(currentMouse.X, currentMouse.Y);
            // get left mouse button state
            bool isLeftMouseButtonPressed = currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
            if (isLeftMouseButtonPressed)
            {
                // Handle left mouse button press event
                // HandleTap(new Vector2(currentMouse.X, currentMouse.Y));
            }

            // read scroll wheel value
            int scrollWheelValue = currentMouse.ScrollWheelValue - previousMouse.ScrollWheelValue;
            if (scrollWheelValue != 0)
            {
                // Handle scroll wheel event
            }

            if (currentGamePad.IsConnected)
            {
                bool IsButtonPressed(Buttons button) => currentGamePad.IsButtonDown(button) && previousGamePad.IsButtonUp(button);

                // check if A button was just pressed
                if (currentGamePad.Buttons.A == ButtonState.Pressed && previousGamePad.Buttons.A == ButtonState.Released)
                {
                    // Handle A button press event
                }
                // analog stick reading (-1.0 to 1.0)
                var leftThumbstick = currentGamePad.ThumbSticks.Left;
                // invert y if needed: leftThumbstick.Y *= -1;

                // trigger reading (0.0 to 1.0)
                float rightTrigger = currentGamePad.Triggers.Right;

                // rumble feedback
                GamePad.SetVibration(PlayerIndex.One, 0.5f * rightTrigger, 0.5f * rightTrigger); // left motor, right motor
            }
            #endregion
        }
    }
}
