namespace monoDoodler.Manager
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using monoDoodler.Entity;
    using monoDoodler.Properties;

    public class EnemyManager : BaseManager<Enemy>
    {
        /// <summary>
        /// Просчитывает попадания пуль в противника
        /// </summary>
        /// <param name="bulletList">Все пули</param>
        /// <returns>Были ли попадания?</returns>
        public bool KillEnemy(List<Bullet> bulletList)
        {
            foreach (var bullet in bulletList)
            {
                foreach (var enemy in List.Where(enemy => !Rectangle.Intersect(bullet.Rectangle, enemy.Rectangle).IsEmpty))
                {
                    List.Remove(enemy);
                    bulletList.Remove(bullet);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Были ли попадания в Дудла 
        /// </summary>
        /// <param name="doodle">Дудл</param>
        /// <returns>Были ли попадания в Дудла?</returns>
        public bool KillDoodle(Doodle doodle)
        {
            return this.List.Any(enemy => !Rectangle.Intersect(doodle.Rectangle, enemy.Rectangle).IsEmpty);
        }

        public void Moove()
        {
            foreach (var enemy in List)
            {
                enemy.Position.X += enemy.SpeedX;

                if (enemy.Position.X + enemy.Width > Settings.MonitorWigth || enemy.Position.X < 0)
                {
                    enemy.SpeedX = -enemy.SpeedX;
                }

                enemy.Position.Y += enemy.SpeedY;
            }
        }
    }
}
