using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.System
{
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Input.Touch;

    public enum TypeInputDevice
    {
        None = 0,
        Mouse = 1,
        Touch = 2,
        Keyboard = 3
    }

    public enum TypeSwype
    {
        None = 0,
        Left = 1,
        Right = 2,
        Down = 3,
        Up = 4
    }

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
        public float X { get; set; } = -1;
        public float Y { get; set; } = -1;

        public float X2 { get; set; } = -1;
        public float Y2 { get; set; } = -1;

        public int ScrollValue { get; set; } = 1;

        public TypeInputDevice Device { get; set; } = TypeInputDevice.None;
        public TypeSwype Swype { get; set; } = TypeSwype.None;

        public DeviceEventArgs()
        {

        }
    }

    public class Input
    {
        public static int TouchScatter { get; set; } = 10;

        public event DeviceEventHandler ClickTouch;
        public event DeviceEventHandler PressedTouch;
        public event DeviceEventHandler UnPressedTouch;
        public event DeviceEventHandler MoveTouch;
        public event DeviceEventHandler ScrollingMouse;

        public event DeviceEventHandler ClickMouse;
        public event DeviceEventHandler PressedMouse;
        public event DeviceEventHandler UnPressedMouse;
        public event DeviceEventHandler MoveMouse;
        public event DeviceEventHandler ClickMoveMouse;
        public event DeviceEventHandler PositionChangedMouse;

        public event DeviceEventHandler Swype;

        public event DeviceEventHandler BackKeyboard;

        private bool mouseIsPressed;
        private bool mouseIsPressedEvent;
        private bool keyboardIsPressed;
        private bool touchIsPressed;
        private int mouseXPressed = 0;
        private int mouseYPressed = 0;
        private int mouseXClickMove = 0;
        private int mouseYClickMove = 0;
        private int lastX = 0;
        private int lastY = 0;
        private int lastScrollMouse = 0;

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

                            this.mouseXPressed = (int)e.X;
                            this.mouseYPressed = (int)e.Y;
                        
                            this.PressedMouse(this, e);
                        }
                    }
                    else
                    {
                        if (this.mouseIsPressed)
                        {
                            if (this.ClickMoveMouse != null)
                            {
                                DeviceEventArgs e = new DeviceEventArgs();
                                e.X2 = Mouse.GetState().X;
                                e.Y2 = Mouse.GetState().Y;

                                if ((e.X2 != this.mouseXClickMove) && (e.Y2 != this.mouseYClickMove))
                                {
                                    e.X = this.mouseXPressed;
                                    e.Y = this.mouseYPressed;

                                    this.mouseXClickMove = (int)e.X2;
                                    this.mouseYClickMove = (int)e.Y2;

                                    this.ClickMoveMouse.Invoke(this, e);
                                }
                            }
                        }
                    }
                }
                break;
                case ButtonState.Released:
                {
                    if (this.mouseIsPressed)
                    {
                        this.mouseIsPressed = false;
                        this.mouseIsPressedEvent = false;

                        int x = Mouse.GetState().X;
                        int y = Mouse.GetState().Y;

                        DeviceEventArgs e = new DeviceEventArgs();
                        e.Device = TypeInputDevice.Mouse;

                        if ((Math.Abs(this.mouseXPressed - x) > Input.TouchScatter) ||
                            (Math.Abs(this.mouseYPressed - y) > Input.TouchScatter))
                        {
                            if (this.Swype != null)
                            {
                                e.X = this.mouseXPressed;
                                e.Y = this.mouseYPressed;
                                e.X2 = x;
                                e.Y2 = y;

                                if (this.MoveMouse != null)
                                    this.MoveMouse(this, e);

                                var dX = e.X2 - e.X;
                                var dY = e.Y2 - e.Y;

                                bool isRight = dX > 0;
                                bool isDown = dY > 0;
                                bool isHorizontal = Math.Abs(dX) > Math.Abs(dY);

                                e.Swype = isHorizontal ? (isRight ? TypeSwype.Right : TypeSwype.Left) : (isDown ? TypeSwype.Down : TypeSwype.Up);

                                this.Swype(this, e);
                            }
                        }
                        else if (this.ClickMouse != null)
                        {
                            e.X = x;
                            e.Y = y;
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
                if (this.PositionChangedMouse != null)
                {
                    lastX = mx;
                    lastY = my;
                    DeviceEventArgs e = new DeviceEventArgs();
                    e.X = lastX;
                    e.Y = lastY;

                    this.PositionChangedMouse(this, e);
                }
            }

            var scroll = Mouse.GetState().ScrollWheelValue;
            if (lastScrollMouse != scroll)
            {
                if (this.ScrollingMouse != null)
                {
                    DeviceEventArgs e = new DeviceEventArgs();
                    e.ScrollValue = scroll - lastScrollMouse;
                    lastScrollMouse = scroll;

                    this.ScrollingMouse(this, e);
                }
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
