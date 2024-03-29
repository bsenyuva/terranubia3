﻿using System;
using System.Collections.Generic;
using Server.Mobiles;
using System.Text;
using Server.Spells;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles.Dons
{
    public class DonScDuCritiqueBaton : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>"+
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>"+
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>"+
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueBaton()
            : base(DonEnum.ScienceDuCritiqueBaton, "Science du critique: Baton", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Baton) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueMasse : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueMasse()
            : base(DonEnum.ScienceDuCritiqueMasse, "Science du critique: Masse", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Masse) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueEpee : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueEpee()
            : base(DonEnum.ScienceDuCritiqueEpee, "Science du critique: Epée", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Masse) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueHache : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueHache()
            : base(DonEnum.ScienceDuCritiqueHache, "Science du critique: Hache", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hache) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueLance : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueLance()
            : base(DonEnum.ScienceDuCritiqueLance, "Science du critique: Lance", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Lance) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueHast : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueHast()
            : base(DonEnum.ScienceDuCritiqueHast, "Science du critique: Hast", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hast) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueDague : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueDague()
            : base(DonEnum.ScienceDuCritiqueDague, "Science du critique: Dague", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hast) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueArc : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueArc()
            : base(DonEnum.ScienceDuCritiqueArc, "Science du critique: Arc", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hast) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueArbalete : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueArbalete()
            : base(DonEnum.ScienceDuCritiqueArbalete, "Science du critique: Arbalète", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hast) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiqueJet : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiqueJet()
            : base(DonEnum.ScienceDuCritiqueJet, "Science du critique: Arme de jet", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Hast) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
    public class DonScDuCritiquePoing : BaseDon
    {
        public override int Icone { get { return 20993; } }
        public override string Description
        {
            get
            {
                return "Conditions. Maniement de l’arme choisie, bonus de base à l’attaque de +8.<br>" +
"Avantage. La zone de critique possible de l’arme choisie est doublée. Par exemple, une épée longue peut normalement obtenir une possibilité de coup critique sur un résultat de 19–20 au dé (2 nombres). Avec ce don, la zone de critique possible de cette arme passe à 17–20 (4 nombres).<br>" +
"Spécial. Ce don peut être choisi plusieurs fois, mais ses effets ne se cumulent pas. Il s’applique à chaque fois à une nouvelle arme.<br>" +
"Les effets de ce don ne se cumulent pas avec tout autre effet étendant la zone de critique possible d’une arme (tel que le sort affûtage).<br>" +
"Un guerrier peut choisir Science du critique en tant que don supplémentaire.";
            }
        }
        public override bool WarriorDon { get { return true; } }
        public DonScDuCritiquePoing()
            : base(DonEnum.ScienceDuCritiquePoing, "Science du critique: Poings", false)
        {
            mAchatMax = 1;
            mLimiteDayUse = false;
        }
        public override bool hasConditions(NubiaPlayer mob)
        {
            return (mob.getMaitrise(ArmeTemplate.Poing) > 0 && mob.BonusAttaque[0] >= 8);
        }
    }
}
