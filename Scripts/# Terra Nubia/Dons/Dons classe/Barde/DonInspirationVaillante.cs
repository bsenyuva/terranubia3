﻿using System;
using System.Collections.Generic;
using Server.Mobiles;
using System.Text;
using Server.Spells;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles.Dons
{
    public class DonInspirationVaillante : BaseDon
    {
        public DonInspirationVaillante()
            : base(DonEnum.InspirationVaillante, "Inspiration Vaillante", true)
        {
            mAchatMax = 5;
            mLimiteDayUse = false;
        }

        public override void OnUse(NubiaPlayer p)
        {
            if (p.Competences[CompType.Representation].getMaitrise() < 3)
            {
                p.SendMessage("Vous n'êtes pas assez doué en représentation pour ce don");
                return;
            }
            
            p.ActionRevelation();

            if (p.FindItemOnLayer(Layer.TwoHanded) is BaseInstrument)
            {
                p.Target = new InternalTarget(p, p.FindItemOnLayer(Layer.TwoHanded) as BaseInstrument);
            }
            else
                BaseInstrument.PickInstrument(p, new InstrumentPickedCallback(OnPickedInstrument));

        }
        public static void OnPickedInstrument(Mobile f, BaseInstrument instrument)
        {
            NubiaPlayer from = f as NubiaPlayer;
           

            f.SendMessage("Qui voulez vous inspirer ?");
            
            f.Target = new InternalTarget(from, instrument);
        }

        public override bool hasConditions(NubiaPlayer mob)
        {
            return true;
        }
        private class InternalTarget : Target
        {
            NubiaPlayer mOwner = null;
            BaseInstrument mInstrument = null;
            public InternalTarget(NubiaPlayer f, BaseInstrument instrument)
                : base(12, false, TargetFlags.Beneficial)
            {
                mOwner = f;
                mInstrument = instrument;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is NubiaMobile)
                {
                    NubiaMobile cible = targeted as NubiaMobile;
                    mInstrument.PlayInstrumentWell(mOwner);
                    new InspirationVaillante(mOwner, cible, mOwner.getDonNiveau(DonEnum.InspirationVaillante) );
                }
            }
        }
    }
    public class InspirationVaillante : BaseBuff
    {
        public InspirationVaillante(NubiaMobile caster, NubiaMobile cible, int val)
            : base(caster, cible, 23001,
                50, "Inspiration Vaillante")
        {
            Sauvegardes.Add(new SauvegardeMod(val, SauvegardeEnum.Attaque));
            Sauvegardes.Add(new SauvegardeMod(val, SauvegardeEnum.Volonte, new MagieEcole[]{MagieEcole.Enchantement, MagieEcole.Necromancie} ) );
            mDegatBonus = val;
            m_descrip = "La musique et la présence du Barde vous inspire !";
        }
        public override bool OnTurn()
        {
            if (base.OnTurn())
            {
                if ( (!m_cible.CanSee(m_caster) || !m_cible.InRange(m_caster.Location, 12)) && m_turn > 5 )
                {
                    m_cible.SendMessage("Le barde est trop loin pour vous inspirer");
                    m_turn = 5;
                }
                return true;
            }
            else
                return false;
        }
    }
}
