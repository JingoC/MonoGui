using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.System
{
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Input.Touch;

    public class InputSingleton
    {
        static Input input;

        static public Input GetInstance()
        {
            return input != null ? InputSingleton.input : (input = new Input());
        }
    }

    public delegate void DeviceEventHandler(object sender, DeviceEventArgs e);

    public class DeviceEventArgs : EventArgs
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float X2 { get; set; }
        public float Y2 { get; set; }

        public DeviceEventArgs()
        {

        }
    }

    public class Input
    {
        public event DeviceEventHandler ClickTouch;
        public event DeviceEventHandler PressedTouch;
        public event DeviceEventHandler UnPressedTouch;
        public event DeviceEventHandler MoveTouch;

        public event DeviceEventHandler ClickMouse;
        public event DeviceEventHandler PressedMouse;
        public event DeviceEventHandler UnPressedMouse;
        public event DeviceEventHandler PositionChangedMouse;

        public event DeviceEventHandler BackKeyboard;

        private bool mouseIsPressed;
        private bool mouseIsPressedEvent;
        private bool keyboardIsPressed;
        private bool touchIsPressed;
        private int lastX;
        private int lastY;

        public bool Enable { get; set; } = true;
        public bool MouseEnable { get; set; } = true;
        public bool TouchEnable { get; set; } = true;
        public bool KeyboardEnable { get; set; } = true;

        public Input()
        {
            this.mouseIsPressed = false;
            this.mouseIsPressedEvent = false;

            this.touchIsPressed = false;
            this.keyboardIsPressed = false;
        }
        
        public void Update()
        {
            if (this.Enable)
            {
                if (this.MouseEnable)
                    this.UpdateMouse();

                if (this.TouchEnable)
                    this.UpdateTouch();

                if (this.KeyboardEnable)
                    this.UpdateKeyboard();
            }
        }

        void UpdateMouse()
        {
            switch (Mouse.GetState().LeftButton)
            {
                case ButtonState.Pressed:
                    {
                        this.mouseIsPressed = true;
                        if (!this.mouseIsPressedEvent)
                        {
                            this.mouseIsPressedEvent = true;
                            if (this.PressedMouse != null)
                            {
                                DeviceEventArgs e = new DeviceEventArgs();
                                e.X = Mouse.GetState().X;
                                e.Y = Mouse.GetState().Y;
                                this.PressedMouse(this, e);
                            }
                        }
                    } break;
                case ButtonState.Released:
                    {
                        if (this.mouseIsPressed)
                        {
                            this.mouseIsPressed = false;
                            this.mouseIsPressedEvent = false;       
                            if (this.ClickMouse != null)
                            {
                                DeviceEventArgs e = new DeviceEventArgs();
                                e.X = Mouse.GetState().X;
                                e.Y = Mouse.GetState().Y;
                                this.ClickMouse(this, e);
                            }
                        }
                    }
                    break;
                default: break;
            }

            int mx = Mouse.GetState().X;
            int my = Mouse.GetState().Y;
            if ((lastX != mx) || (lastY != my))
            {
                lastX = mx;
                lastY = my;
                DeviceEventArgs e = new DeviceEventArgs();
                e.X = lastX;
                e.Y = lastY;
                if (this.PositionChangedMouse != null)
                    this.PositionChangedMouse(this, e);
            }
        }

        void UpdateTouch()
        {
            var touch = TouchPanel.GetState().FirstOrDefault();
            if (touch != null)
            {

                switch (touch.State)
                {
                    case TouchLocationState.Pressed:
                        {
                            if (!this.touchIsPressed)
                            {
                                this.touchIsPressed = true;
                                if (this.PressedMouse != null)
                                {
                                    DeviceEventArgs e = new DeviceEventArgs();

                                    e.X = touch.Position.X;
                                    e.Y = touch.Position.Y;
                                    this.PressedTouch(this, e);
                                }
                            }
                        } break;
                    case TouchLocationState.Released:
                        {
                            if (this.touchIsPressed)
                            {
                                this.touchIsPressed = false;
                                
                                if (this.ClickTouch != null)
                                {
                                    DeviceEventArgs e = new DeviceEventArgs();
                                    e.X = touch.Position.X;
                                    e.Y = touch.Position.Y;
                                    this.ClickTouch(this, e);
                                }
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        void UpdateKeyboard()
        {
            var state = Keyboard.GetState();

            if (!this.keyboardIsPressed && state.IsKeyDown(Keys.Back))
            {
                this.keyboardIsPressed = true;
            }

            if (this.keyboardIsPressed && state.IsKeyUp(Keys.Back))
            {
                this.keyboardIsPressed = false;
                if (this.BackKeyboard != null)
                    this.BackKeyboard(this, new DeviceEventArgs());
            }
        }
    }
}
