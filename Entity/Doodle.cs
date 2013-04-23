using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monoDoodler.Entity
{
    using Properties;

    public sealed class Doodle : PositibleObject
    {
        public float AccelerationY { get; set; }

        public float AccelerationX { get; set; }

        /// <summary>
        /// Прыжок
        /// </summary>
        /// <param name="strange">Сила прыжка</param>
        public void Jamp(float strange)
        {
            AccelerationY = strange;
        }

        public void MooveY()
        {
            Position.Y -= AccelerationY;
        }

        public void MooveX(int speed)
        {
            AccelerationX = speed;
            Position.X += AccelerationX;
            AccelerationX = AccelerationX / 8 * 5;
            if (Position.X + Width > Settings.MonitorWigth)
            {
                Position.X = 0;
            }
            else if (Position.X < 0)
            {
                Position.X = Settings.MonitorWigth - Width;
            }
        }

        public override void Initialize(ContentManager content)
        {
            Image = content.Load<Texture2D>("Graphics\\Doodle");
            Position.Y = Settings.MonitorHeight / 3f;
            Position.X = Settings.MonitorWigth / 3f;
            AccelerationY = -2;
        }
    }
}
