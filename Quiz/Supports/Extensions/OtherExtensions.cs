using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Supports.PropertyAnimationHandlers;

namespace Quiz.Supports.Extensions
{
    static class OtherExtensions
    {
        /// <summary>
        /// Reverse <see cref="AnimationTriggerValue"/>
        /// If value set to Start it changes to StartReverce
        /// If value set to StartReverce it changes to Start
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AnimationTriggerValue ReverceAnimationTriggerValue(AnimationTriggerValue value) =>
            value == AnimationTriggerValue.Start ? AnimationTriggerValue.StartReverce : AnimationTriggerValue.Start;

    }
}
