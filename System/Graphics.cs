using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGuiFramework.System
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GraphicsSingleton
    {
        static Graphics graphics;

        public static Graphics GetInstance()
        {
            return (graphics != null) ? graphics : (graphics = new Graphics());
        }
    }
    
    /// <summary>
    /// MonoGame wrapper
    /// </summary>
    public class Graphics : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Graphics()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
        }

        public SpriteBatch GetSpriteBatch() { return this.spriteBatch; }
        public GraphicsDeviceManager GetGraphics() { return this.graphics; }
        public int Width { get => this.graphics.PreferredBackBufferWidth; }
        public int Height { get => this.graphics.PreferredBackBufferHeight; }

        public event EventHandler LoadContentEvent;
        public event EventHandler UpdateEvent;
        public event EventHandler DrawEvent;
        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            base.LoadContent();

            if (this.LoadContentEvent != null)
                this.LoadContentEvent(this, EventArgs.Empty);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.UpdateEvent != null)
                this.UpdateEvent(gameTime, EventArgs.Empty);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (this.DrawEvent != null)
                this.DrawEvent(gameTime, EventArgs.Empty);
        }
    }
}
