namespace monoDoodler.Manager
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    using monoDoodler.Entity;

    public class BulletManager : BaseManager<Bullet>
    {
        public void Fire(Doodle doodle, bool multiFire, ContentManager content)
        {
            var bullet1 = new Bullet { Position = new Vector2(doodle.Position.X + doodle.Width / 2F, doodle.Position.Y) };

            bullet1.Initialize(content);
           
            List.Add(bullet1);
            if (multiFire)
            {
                var bullet2 = new Bullet { Position = new Vector2(doodle.Position.X + doodle.Width / 2F, doodle.Position.Y) };
                var bullet3 = new Bullet { Position = new Vector2(doodle.Position.X + doodle.Width / 2F, doodle.Position.Y) };
                bullet2.Initialize(content);
                bullet3.Initialize(content);
                bullet2.SpeedX = 3;
                List.Add(bullet2);
                bullet3.SpeedX = -3;
                List.Add(bullet3);
            }
        }

        public void Moove()
        {
            List.ForEach(x => x.Moove());
            List.RemoveAll(x => x.Position.Y < 0);
        }
    }
}
