namespace monoDoodler.Manager
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    using monoDoodler.Entity;

    public class BonusManager : BaseManager<Bonus>
    {
        private List<Bonus> activeBonusList;

        public override void Initialize(ContentManager content)
        {
            activeBonusList = new List<Bonus>();
            base.Initialize(content);
        }

        /// <summary>
        /// Взял ли дудл бонус?
        /// </summary>
        /// <param name="doodle">Дудл</param>
        /// <returns>Взял ли дудл бонус?</returns>
        public bool IsTakenBonus(Doodle doodle, ContentManager content)
        {
            foreach (var bonus in List.Where(bonus => !Rectangle.Intersect(doodle.Rectangle, bonus.Rectangle).IsEmpty))
            {
                if (activeBonusList.Exists(x => x.Type == bonus.Type))
                {
                    this.activeBonusList.FirstOrDefault(x => x.Type == bonus.Type).Duration += bonus.Duration;
                }
                else
                {
                    this.activeBonusList.Add(bonus);
                }
                
                if (bonus.Type == BonusType.FlyDoodler)
                {
                    doodle.SetFlySkyn(true, content);
                }

                List.Remove(bonus);
                return true;
            }

            return false;
        }

        public bool BonusIsActive(BonusType bonusType)
        {
            return this.activeBonusList.Any(bonuse => bonuse.Type == bonusType);
        }

        /// <summary>
        /// Удалить бонусы из активных если их действие закончилось
        /// </summary>
        /// <returns>
        /// Удаленные бонусы
        /// </returns>
        public List<Bonus> TimeRefresh()
        {
            activeBonusList.ForEach(x => x.Duration--);
            var removedBonuses = activeBonusList.Where(x => x.Duration <= 0).ToList();
            activeBonusList.RemoveAll(x => x.Duration <= 0);
            return removedBonuses;
        }
    }
}
