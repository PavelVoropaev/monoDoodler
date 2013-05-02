namespace monoDoodler.Entity
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using monoDoodler.Properties;

    public sealed class Platform : PositibleObject
    {
        public int Strange { get; set; }

        public bool GoToRight { get; set; }

        public bool GoToLeft { get; set; }

        public int SpeedX { get; set; }

        public int SpeedY { get; set; }

        public override void Initialize(ContentManager content)
        {
            Strange = 14;
            Position.X = Rnd.Next(10, Settings.MonitorWigth - 70);
            GoToRight = Rnd.Next(1, 15) % 5 == 0;
            GoToLeft = Rnd.Next(1, 15) % 5 == 0;
            SpeedX = Rnd.Next(1, 3);
            Position.Y = 0;
            SpeedY = Rnd.Next(1, 3);
            Position.X = Rnd.Next(10, Settings.MonitorWigth - 70);

            if (Rnd.Next(0, 20) == 6)
            {
                Image = content.Load<Texture2D>("Graphics\\GreenPlatform");
                Strange = 20;
            }
            else if (Rnd.Next(0, 60) == 12)
            {
                Image = content.Load<Texture2D>("Graphics\\RedPlatform");
                Strange = 30;
            }
            else
            {
                Image = content.Load<Texture2D>("Graphics\\YellowPlatform");
                Strange = 14;
            }
        }
    }
}
