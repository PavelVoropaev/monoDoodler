namespace monoDoodler.Entity
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using monoDoodler.Properties;

    public sealed class Enemy : PositibleObject
    {
        public int SpeedX { get; set; }

        public int SpeedY { get; set; }

        public override void Initialize(ContentManager content)
        {
            Image = content.Load<Texture2D>("Graphics\\Enemy");
            this.SpeedX = this.Rnd.Next(-3, 3);
            this.SpeedY = this.Rnd.Next(1, 3);
            this.Position = new Vector2(this.Rnd.Next(10, Settings.MonitorWigth - 30), 0);
        }
    }
}