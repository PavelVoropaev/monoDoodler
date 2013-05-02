namespace monoDoodler.Entity
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using monoDoodler.Properties;

    public sealed class Bonus : PositibleObject
    {
        public int Duration { get; set; }

        public BonusType Type { get; set; }

        public override void Initialize(ContentManager content)
        {
            Duration = 200;
            if (Rnd.Next(2) % 2 == 0)
            {
                Type = BonusType.DoobleJump;
                Image = content.Load<Texture2D>("Graphics\\doubleJump");
            }
            else
            {
                Type = BonusType.MultFire;
                Image = content.Load<Texture2D>("Graphics\\SBonus");
            }

            Position.Y = 0;
            Position.X = Rnd.Next(10, Settings.MonitorWigth - 30);
        }
    }
}
