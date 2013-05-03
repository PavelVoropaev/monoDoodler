namespace monoDoodler.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework.Content;

    using monoDoodler.Entity;
    using monoDoodler.Properties;

    public class PlatformManager : BaseManager<Platform>
    {
        public override void Initialize(ContentManager content)
        {
            base.Initialize(content);
            for (var tempPosY = 0; tempPosY < Settings.MonitorHeight; tempPosY += 7)
            {
                var platform = new Platform();
                platform.Initialize(content);
                platform.Position.Y = tempPosY;
                List.Add(platform);
            }
        }

        /// <summary>
        /// Стоит ли дудл на платформе?
        /// </summary>
        /// <param name="doodle">Дудл</param>
        /// <param name="strenge">Сила толчка платформы </param>
        /// <returns>Стоит ли дудл на платформе?</returns>
        public bool StendToPlatfotm(Doodle doodle, out float strenge)
        {
            foreach (var platform in List.Where(platform =>
                doodle.AccelerationY < 0 &&
                Math.Abs(doodle.Position.Y + doodle.Height - platform.Position.Y - platform.Height / 2F) < platform.Height &&
                doodle.Position.X + doodle.Width + doodle.AccelerationX > platform.Position.X &&
                doodle.Position.X < platform.Position.X + platform.Width))
            {
                strenge = platform.Strange;
                return true;
            }

            strenge = 0;
            return false;
        }

        public void Moove()
        {
            foreach (var platform in List)
            {
                if (platform.GoToLeft)
                {
                    platform.Position.X -= platform.SpeedX;
                }
                else if (platform.GoToRight)
                {
                    platform.Position.X += platform.SpeedX;
                }

                if (platform.Position.X + platform.Width > Settings.MonitorWigth)
                {
                    platform.GoToLeft = true;
                    platform.GoToRight = false;
                }
                else if (platform.Position.X < 0)
                {
                    platform.GoToLeft = false;
                    platform.GoToRight = true;
                }
            }
        }
    }
}
