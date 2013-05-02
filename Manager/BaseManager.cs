namespace monoDoodler.Manager
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using monoDoodler.Entity;
    using monoDoodler.Properties;

    public abstract class BaseManager<T> where T : PositibleObject
    {
        protected BaseManager()
        {
            List = new List<T>();
        }

        protected internal List<T> List { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            List.ForEach(x => x.Draw(spriteBatch));
        }

        public void WindowMooveY(float doodleSpeed, ContentManager content)
        {
            List.ForEach(x => x.Position.Y += doodleSpeed);
        }

        public void ReInitIfOutMonitor(ContentManager content)
        {
            foreach (var item in this.List)
            {
                if (item.Position.Y > Settings.MonitorHeight)
                {
                    item.Initialize(content);
                }
            }
        }

        /// <summary>
        /// Убирает планформы с экрана.
        /// </summary>
        /// <returns>
        /// Убрана ли последняя платформа
        /// </returns>
        public bool HideComplided()
        {
            const int HideSpeed = 40;
            var disposeCancel = true;
            foreach (var item in List)
            {
                item.Position.Y -= HideSpeed;
                if (item.Position.Y > 0)
                {
                    disposeCancel = false;
                }
            }

            return disposeCancel;
        }

        public void AddItem(ContentManager content)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), new object[] { });
            instance.Initialize(content);
            List.Add(instance);
        }

        public virtual void Initialize(ContentManager content)
        {
            List.ForEach(x => x.Initialize(content));
        }
    }
}
