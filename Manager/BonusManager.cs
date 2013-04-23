using System;
using Microsoft.Xna.Framework;

namespace monoDoodler.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using Entity;

    public class BonusManager : BaseManager<Bonus>
    {
        private readonly Dictionary<Bonus, int> _activeBonusList;

        public BonusManager()
        {
            _activeBonusList = new Dictionary<Bonus, int>();
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
               _activeBonusList.Add(bonus, 200);
               List.Remove(bonus);
               return true;
           }

            return false;
        }

        public bool DoobleJumpIsActive()
        {
            return _activeBonusList.Any(bonuse => bonuse.Key.DoobleJump);
        }

        public bool MultiFireIsActive()
        {
            return _activeBonusList.Any(bonuse => bonuse.Key.MultFire);
        }

        /// <summary>
        /// Удалить бонусы из активных если их действие закончилось
        /// </summary>
        public void TimeRefresh()
        {

       //    try
       //    {
       //        foreach (var bonusTime in _activeBonusList)
       //        {
       //            _activeBonusList[bonusTime.Key] = bonusTime.Value - 1;
       //            if (bonusTime.Value >= 0) continue;
       //            _activeBonusList.Remove(bonusTime.Key);
       //        }
       //    }
       //    catch (Exception e)
       //    {
       //    }

        }
    }
}
