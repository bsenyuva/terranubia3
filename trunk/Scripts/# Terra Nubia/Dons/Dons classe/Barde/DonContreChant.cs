﻿using System;
using System.Collections.Generic;
using Server.Mobiles;
using System.Text;

namespace Server.Mobiles.Dons
{
    public class DonContreChant : BaseDon
    {
        public DonContreChant()
            : base(DonEnum.ContreChant, "Contre-Chant", true)
        {
            mAchatMax = 1;
            mLimiteDayUse = true;            
        }

        public override void OnUse(NubiaPlayer p)
        {
            if (!hasConditions(p))
            {
                p.SendMessage("Vous n'avez pas assez de représentation pour cela");
                return;
            }
            p.Emote("*Chante en opposition*");
            IPooledEnumerable eable = p.GetMobilesInRange(8);
            foreach (NubiaMobile m in eable)
            {
                if (m.Competences[CompType.Representation].pureRoll(0) < p.Competences[CompType.Representation].pureRoll(0))
                {
                    m.SendMessage("Oublie: Todo interrupt cast. Repportez l'oubli sur le forum");
                  //  m.InterruptCast(p, ClasseType.Barde);
                }
            }
            eable.Free();
        }

        public override bool hasConditions(NubiaPlayer mob)
        {
            return mob.Competences[CompType.Representation].getMaitrise() >= 3;
        }
    }

}
