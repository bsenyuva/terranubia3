﻿using System;
using System.Collections.Generic;
using Server.Mobiles;
using System.Text;
using Server.Spells;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles.Dons
{
    public class DonInterceptionProjectile : BaseDon
    {
        public DonInterceptionProjectile()
            : base(DonEnum.InterceptionDeProjectile, "Interception de projectile", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.Sag >= 15 && mob.hasDon(DonEnum.ParadeDeProjectiles) && mob.hasDon(DonEnum.ScienceDuCombatAMainsNues) );
        }

    }

}
