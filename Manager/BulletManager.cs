using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using monoDoodler.Entity;

namespace monoDoodler.Manager
{
    public class BulletManager : BaseManager<Bullet>
    {
        public void Fire(Doodle doodle, bool multiFire, ContentManager content)
        {
            var bullet = new Bullet
                {
                    Position = new Vector2(doodle.Position.X + doodle.Width/2F, doodle.Position.Y)
                };
            bullet.Initialize(content);
            List.Add(bullet);
            if (multiFire)
            {
                bullet.SpeedX = 3;
                List.Add(bullet);
                bullet.SpeedX = -3;

                List.Add(bullet);
            }
        }

        public void Moove()
        {
            List.ForEach(x => x.Moove());
            List.RemoveAll(x => x.Position.Y < 0);
        }
    }
}
