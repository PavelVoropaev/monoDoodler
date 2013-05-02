namespace monoDoodler.Manager
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using monoDoodler.Entity;

    public class BonusManager : BaseManager<Bonus>
    {
        private readonly List<Bonus> activeBonusList;

        public BonusManager()
        {
            this.activeBonusList = new List<Bonus>();
        }

        /// <summary>
        /// Взял ли дудл бонус?
        /// </summary>
        /// <param name="doodle">Дудл</param>
        /// <returns>Взял ли дудл бонус?</returns>
        public bool IsTakenBonus(Doodle doodle)
        {
            foreach (var bonus in List.Where(bonus => !Rectangle.Intersect(doodle.Rectangle, bonus.Rectangle).IsEmpty))
            {
                this.activeBonusList.Add(bonus);
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
        public void TimeRefresh()
        {
            activeBonusList.ForEach(x => x.Duration--);
            activeBonusList.RemoveAll(x => x.Duration <= 0);
        }
    }
}
