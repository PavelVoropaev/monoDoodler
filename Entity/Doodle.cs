namespace monoDoodler.Entity
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using monoDoodler.Properties;

    public sealed class Doodle : PositibleObject
    {
        public float AccelerationY { get; set; }

        public float AccelerationX { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="strange">���� ������</param>
        public void Jamp(float strange)
        {
            AccelerationY = strange;
        }

        public void MooveY(int speed)
        {
            Position.Y -= speed;
        }

        public void MoveGravity()
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

        public void SetFlySkyn(bool fly, ContentManager content)
        {
            Image = content.Load<Texture2D>(fly ? "Graphics\\FlyDoodler" : "Graphics\\Doodle");
        }
    }
}
