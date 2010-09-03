﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
namespace Server.Items
{
    public enum PiegeDeclencheur
    {
        Espace,
        Proximite,
        Sonore,
        Visuel,
        Contact,
        Minute
    }
    public class BasePiege : Item
    {
        public override bool HandlesOnMovement
        {
            get
            {
                return true;
            }
        }
        public override bool HandlesOnSpeech
        {
            get
            {
                return true;
            }
        }
        private bool mMecanique = true;
        private PiegeDeclencheur mDeclencheur = PiegeDeclencheur.Espace;
        private int mUtilisation = -1; //-1 = infini, 0 = cassé;
        private TimeSpan mRetardement = TimeSpan.FromSeconds(0);
        private DateTime mLastUtilisation = DateTime.Now- TimeSpan.FromMinutes(2);
        private int mDD = 20;
        private int mRange = 3;
        private int mTourRechargement = 3;
        private bool mAmorce = true;
        private De mDegat = De.douze;
        private int mNbrLancer = 1;
        private bool mPoisonning = false;
        private Poison mPoison = Poison.Lesser;
        private int mEffectID = 0x36B0;

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison PPoison
        {
            get { return mPoison; }
            set { mPoison = value; }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Poisonning
        {
            get { return mPoisonning; }
            set { mPoisonning = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int NbrLancer
        {
            get { return mNbrLancer; }
            set { mNbrLancer = value; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public De Degat
        {
            get { return mDegat; }
            set { mDegat = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Amorce
        {
            get { return mAmorce; }
            set { mAmorce = value; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public int TourRechargement
        {
            get { return mTourRechargement; }
            set { mTourRechargement = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Range
        {
            get { return mRange; }
            set { mRange = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DD
        {
            get { return mDD; }
            set { mDD = value; }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastUtilisation
        {
            get { return mLastUtilisation; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Retardement
        {
            get { return mRetardement; }
            set { mRetardement = value; }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int Utilisation
        {
            get { return mUtilisation; }
            set { mUtilisation = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public PiegeDeclencheur Declencheur
        {
            get { return mDeclencheur; }
            set { mDeclencheur = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Mecanique
        {
            get { return mMecanique; }
            set { mMecanique = value; }
        }

        [Constructable]
        public BasePiege()
            : base(0x1B7A)
        {
            Visible = false;
            Movable = false;
            Name = "Piege";
        }

        [Constructable]
        public BasePiege(Serial s)
            : base(s)
        {
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (!checkReflexe(m) 
                && canActive() )
            {
                BeginDeclenchement(m);
            }
            return true;
        }
        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            //Detection
            if( m.InRange(m.Location, Math.Max( mRange + 4, 8 ) ) && m is NubiaPlayer )
            {
                NubiaPlayer player = m as NubiaPlayer;
                if (mDD <= 20 || player.hasDon(DonEnum.RecherchePiege))
                {
                    if (player.Competences[CompType.Fouille].check(mDD,0,true))
                    {
                        player.PrivateOverheadMessage(MessageType.Regular, Utility.RandomRedHue(), true, "Piege proche !", player.NetState);
                        SendInfoTo(player.NetState);
                        //this.LabelTo(player, "Piège !");
                    }
                }
            }
            if (m.InRange(this.Location, mRange))
            {
                bool declenche = false;
                if (mDeclencheur == PiegeDeclencheur.Proximite)
                    declenche = true;
                else if (mDeclencheur == PiegeDeclencheur.Sonore && canHear(m))
                    declenche = true;
                else if (mDeclencheur == PiegeDeclencheur.Visuel && m.CanSee(this))
                    declenche = true;

                if( declenche)
                    OnMoveOver(m);
            }
        }
        public override void OnSpeech(SpeechEventArgs e)
        {
            if (e.Type == MessageType.Regular || e.Type == MessageType.Yell)
            {
                OnMoveOver(e.Mobile);
            }
        }

        private bool canHear(Mobile m)
        {
            if (m is NubiaMobile)
            {
                NubiaMobile mob = m as NubiaMobile;
                int roll = mob.Competences[CompType.DeplacementSilencieux].pureRoll(0);
                if ( !mob.Hidden)
                    roll -= 5;
                if (roll >= mDD)
                    return false;
            }
            return true;
        }

        private bool checkReflexe(Mobile m)
        {
            if (m is NubiaMobile)
            {
                NubiaMobile mob = m as NubiaMobile;
                int roll = DndHelper.rollDe(De.vingt);
                roll += mob.getBonusReflexe(SortEnergie.Piege);
                if (m is NubiaPlayer)
                {
                    if (((NubiaPlayer)mob).hasDon(DonEnum.SensPieges))
                    {
                        roll += ((NubiaPlayer)mob).getDonNiveau(DonEnum.SensPieges);
                    }
                }
                if (roll >= mDD)
                {
                    m.Emote("*Evite le piège*");
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool canActive()
        {
            return (DateTime.Now > mLastUtilisation + TimeSpan.FromSeconds(8*mTourRechargement)) && mUtilisation != 0 && mAmorce;
        }
        public void consumeUtilisation()
        {
            if (mUtilisation > 0)
                mUtilisation--;
            mLastUtilisation = DateTime.Now;
        }
        public void BeginDeclenchement(Mobile m)
        {
            consumeUtilisation();
            if (mRetardement.TotalSeconds == 0)
                OnDeclenchement(m as NubiaMobile);
            else
                new RetardementTimer(this, m, mRetardement);
            m.Emote("*click*");
            
        }
        protected virtual void OnDeclenchement(NubiaMobile m)
        {
            int degats = DndHelper.rollDe(mDegat, mNbrLancer);
            foreach (NubiaMobile mob in this.GetMobilesInRange(mRange))
            {
                if (DndHelper.rollDe(De.vingt) + m.getBonusReflexe(SortEnergie.Piege) < mDD)
                {
                    mob.ActionRevelation();
                    mob.FixedParticles(mEffectID, 7, 5, 0, EffectLayer.Head);
                    mob.Damage(degats);
                    if (mPoisonning && DndHelper.rollDe(De.vingt) + m.getBonusVigueur(SortEnergie.Piege) < mDD)
                        mob.Poison = mPoison;
                }
                else
                    mob.Emote("*évite le piège*");
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (from is NubiaMobile)
            {
                from.Emote("*tente de désamorcer un piège*");
                if (!tryDesarmorcer(from as NubiaMobile))
                    OnMoveOver(from);
                else{
                    from.SendMessage("vous avez désamorçé le piège");
                    this.mAmorce = false;
                    new RechargeTimer(this, from, TimeSpan.FromMinutes(5) ); 
                }
            }
        }
        public virtual bool tryDesarmorcer(NubiaMobile m)
        {
            if (m.Competences[CompType.Desamorcage].check(mDD))
            {
                return true;
            }
            return false;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            // list.Add(String.Format("Rang: {0}\n",));
            string infos = "";

            if (mUtilisation == 0)
                infos += "Cassé\n";

            infos += String.Format("Déclenchement: {0}\nDégats: {1}\nPortée: {2}m",
                mDeclencheur.ToString(),
                DndHelper.nomDe(mDegat, mNbrLancer),
                (double)(mRange*1.5) );
            if (mPoisonning)
                infos += "\nEmpoisonné";
            infos += "\nAmorcé: ";
            if (mAmorce)
                infos += "oui";
            else
                infos += "non";
            list.Add(infos);
        }

        private class RetardementTimer : Timer
        {
            private BasePiege piege;
            private Mobile mobile;
            public RetardementTimer(BasePiege p, Mobile m, TimeSpan time)
                : base(time)
            {
                piege = p;
                mobile = m;
            }
            protected override void OnTick()
            {
                if (piege != null && mobile != null)
                {
                    piege.OnDeclenchement(mobile as NubiaMobile);
                }
            }
        }

        private class RechargeTimer : Timer
        {
            private BasePiege piege;
            private Mobile mobile;
            public RechargeTimer(BasePiege p, Mobile m, TimeSpan time)
                : base(time)
            {
                piege = p;
                mobile = m;
            }
            protected override void OnTick()
            {
                if (piege != null && mobile != null)
                {
                    piege.mAmorce = true;
                }
            }
        }
      

        public override void Serialize(GenericWriter writer)
        {
            /* bool mMecanique = true;
        private PiegeDeclencheur mDeclencheur = PiegeDeclencheur.Espace;
        private int mUtilisation = -1; //-1 = infini, 0 = cassé;
        private TimeSpan mRetardement = TimeSpan.FromSeconds(0);
        private DateTime mLastUtilisation = DateTime.Now- TimeSpan.FromMinutes(2);
        private int mDD = 20;
        private int mRange = 3;
        private int mTourRechargement = 3;
        private bool mAmorce = true;
             * 
             * rivate De mDegat = De.douze;
        private int mNbrLancer = 1;
        private bool mPoisonning = false;
        private Poison mPoison = Poison.Lesser;
        private int mEffectID = 0x36B0;
*/
            base.Serialize(writer);
            writer.Write((int)0);//version
            writer.Write((bool)mMecanique);
            writer.Write((int)mDeclencheur);
            writer.Write((int)mUtilisation);
            writer.Write((double)mRetardement.TotalSeconds);
            writer.Write((DateTime)mLastUtilisation);
            writer.Write((int)mDD);
            writer.Write((int)mRange);
            writer.Write((int)mTourRechargement);
            writer.Write((bool)mAmorce);
            writer.Write((int)mDegat);
            writer.Write((int)NbrLancer);
            writer.Write((bool)mPoisonning);
            writer.Write((int)PPoison.Level);
            writer.Write((int)mEffectID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            mMecanique = reader.ReadBool();
            mDeclencheur = (PiegeDeclencheur)reader.ReadInt();
            mUtilisation = reader.ReadInt();
            mRetardement = TimeSpan.FromSeconds(reader.ReadDouble());
            mLastUtilisation = reader.ReadDateTime();
            mDD = reader.ReadInt();
            mRange = reader.ReadInt();
            mTourRechargement = reader.ReadInt();
            mAmorce = reader.ReadBool();
            mDegat = (De)reader.ReadInt();
            mNbrLancer = reader.ReadInt();
            mPoisonning = reader.ReadBool();
            mPoison = Poison.GetPoison(reader.ReadInt());
            mEffectID = reader.ReadInt();
        }
    }
}
