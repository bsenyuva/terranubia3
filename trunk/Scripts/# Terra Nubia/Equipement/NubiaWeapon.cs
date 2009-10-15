﻿using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Factions;
using Server.Engines.Craft;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Items
{
    public class NubiaWeapon : Item, INubiaCraftable,  IWeapon
    {
        public static string getTemplateString(ArmeTemplate t)
        {
            switch (t)
            {
                case ArmeTemplate.Arbalete: return "Arbalères";
                case ArmeTemplate.Arc: return "Arcs";
                case ArmeTemplate.Baton: return "Batons";
                case ArmeTemplate.Dague: return "Dagues";
                case ArmeTemplate.Epee: return "Epées";
                case ArmeTemplate.Hache: return "Haches";
                case ArmeTemplate.Hast: return "Armes d'Hast";
                case ArmeTemplate.Lance: return "Lances";
                case ArmeTemplate.Masse: return "Masses";
                case ArmeTemplate.Poing: return "Poings";
                case ArmeTemplate.Jet: return "Armes de jet";
            }
            return "none";
        }

        private De mDe = De.trois;
        private int mNbrLance = 1;
        private int mMiniCritique = 20; // mMiniCritique ou supérieur sur 1d20 pour un coup critique
        private int mCritiqueMulti = 2;
        private int mMaxRange = 1;
        private ArmeCategorie mArmeCategorie = ArmeCategorie.Courante;
        private ArmeTemplate mTemplate = ArmeTemplate.Arbalete;

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmeCategorie ArmeCategorie
        {
            get { return mArmeCategorie; }
            set { mArmeCategorie = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxRange
        {
            get { return mMaxRange; }
            set { mMaxRange = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public De De
        {
            get { return mDe; }
            set { mDe = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int NbrLance
        {
            get { return mNbrLance; }
            set { mNbrLance = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int MiniForCritique
        {
            get { return mMiniCritique; }
            set { mMiniCritique = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int CritiqueMulti
        {
            get { return mCritiqueMulti; }
            set { mCritiqueMulti = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int DefHitSound { get { return 0; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int DefMissSound { get { return 0; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual WeaponAnimation DefAnimation { get { return WeaponAnimation.Wrestle; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual ArmeTemplate Template { get { return mTemplate; } }


        private bool mOpportunite = false; // en cas d'attaque d'opportunité, on divise par deux le délai entre deux attaque

        #region Craft Variable & Functions
        private int mHitsMax = 50;
        private int mHits = 50;
        private int mBonusAttaque = 0;
        private int mBonusDegat = 0;
        private Mobile mArtisan = null;
        private NubiaQualityEnum mNubiaQuality = NubiaQualityEnum.Mauvaise;
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Artisan { get { return mArtisan; } set { mArtisan = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public NubiaQualityEnum NQuality { get { return mNubiaQuality; } set { AfterCraft(value); } }

        private List<NubiaRessource> mTRessourceList = new List<NubiaRessource>();
        public List<NubiaRessource> TRessourceList { get { return mTRessourceList; } }

        public void AfterCraft(NubiaQualityEnum quality)
        {
            mNubiaQuality = quality;
            for (int i = 0; i < mTRessourceList.Count; i++)
            {
                NubiaRessource res = mTRessourceList[i];
                NubiaInfoRessource infos =
                    NubiaInfoRessource.GetInfoRessource(res);
                mBonusAttaque = Math.Max(mBonusAttaque, infos.BonusAttaque);
                mBonusDegat = Math.Max(mBonusDegat, infos.BonusDegat);
                mHitsMax = Math.Max((int)(50.0 * infos.Durabilite), mHitsMax);
                mHits = mHitsMax;
            }
            switch (mNubiaQuality)
            {
                case NubiaQualityEnum.Mauvaise: mBonusAttaque--; break;
                case NubiaQualityEnum.Bonne: mHitsMax += 10; break;
                case NubiaQualityEnum.Excellente: mBonusAttaque++; mHitsMax += 20; break;
                case NubiaQualityEnum.Maitre: mBonusAttaque++; mBonusDegat++; mHitsMax += 30; break;
                    
            }
        }

        #endregion


        public NubiaWeapon(ArmeTemplate template, int ItemId)
            : base(ItemId)
        {
            mTemplate = template;
            Layer = Layer.OneHanded;
            if (template == ArmeTemplate.Arbalete)
            {
                mDe = De.huit;
                mMiniCritique = 19;
                mMaxRange = 15;
                mArmeCategorie = ArmeCategorie.Exotique;
                Layer = Layer.TwoHanded;
            }
            else if (template == ArmeTemplate.Arc)
            {
                mDe = De.huit;
                mMiniCritique = 19;
                mMaxRange = 15;
                mArmeCategorie = ArmeCategorie.Guerre;
                Layer = Layer.TwoHanded;
            }
            else if (template == ArmeTemplate.Baton)
            {
                mDe = De.six;
                mMiniCritique = 20;
                Layer = Layer.TwoHanded;
            }
            else if (template == ArmeTemplate.Dague)
            {
                mDe = De.quatre;
                mMiniCritique = 19;                
            }
            else if (template == ArmeTemplate.Epee)
            {
                mDe = De.huit;
                mMiniCritique = 19;
                mArmeCategorie = ArmeCategorie.Guerre;
            }
            else if (template == ArmeTemplate.Hache)
            {
                mDe = De.dix;
                mMiniCritique = 20;
                mCritiqueMulti = 3;
                Layer = Layer.TwoHanded;
                mArmeCategorie = ArmeCategorie.Guerre;
            }
            else if (template == ArmeTemplate.Hast)
            {
                mDe = De.dix;
                mMiniCritique = 20;
                mCritiqueMulti = 3;
                mMaxRange = 2;
                Layer = Layer.TwoHanded;
                mArmeCategorie = ArmeCategorie.Guerre;
            }
            else if (template == ArmeTemplate.Lance)
            {
                mDe = De.huit;
                mMiniCritique = 20;
                mCritiqueMulti = 3;
                mMaxRange = 2;
                Layer = Layer.TwoHanded;
                mArmeCategorie = ArmeCategorie.Courante;
            }
            else if (template == ArmeTemplate.Masse)
            {
                mDe = De.huit;
                mMiniCritique = 20;
                mCritiqueMulti = 3;
                mMaxRange = 1;
                Layer = Layer.OneHanded;
                mArmeCategorie = ArmeCategorie.Guerre;
            }
            else if (template == ArmeTemplate.Jet)
            {
                mDe = De.huit;
                mMiniCritique = 19;
                mCritiqueMulti = 2;
                mMaxRange = 15;
                Layer = Layer.OneHanded;
                mArmeCategorie = ArmeCategorie.Guerre;
            }

            Weight = 10.0;
           // Name = "Nubia Weapon";
        }
        public NubiaWeapon(Serial serial) : base(serial) { }

        public virtual void PlaySwingAnimation(Mobile from)
        {
            int action;

            switch (from.Body.Type)
            {
                case BodyType.Sea:
                case BodyType.Animal:
                    {
                        action = Utility.Random(5, 2);
                        break;
                    }
                case BodyType.Monster:
                    {
                        switch (DefAnimation)
                        {
                            default:
                            case WeaponAnimation.Wrestle:
                            case WeaponAnimation.Bash1H:
                            case WeaponAnimation.Pierce1H:
                            case WeaponAnimation.Slash1H:
                            case WeaponAnimation.Bash2H:
                            case WeaponAnimation.Pierce2H:
                            case WeaponAnimation.Slash2H: action = Utility.Random(4, 3); break;
                            case WeaponAnimation.ShootBow: return; // 7
                            case WeaponAnimation.ShootXBow: return; // 8
                        }

                        break;
                    }
                case BodyType.Human:
                    {
                        if (!from.Mounted)
                        {
                            action = (int)DefAnimation;
                        }
                        else
                        {
                            switch (DefAnimation)
                            {
                                default:
                                case WeaponAnimation.Wrestle:
                                case WeaponAnimation.Bash1H:
                                case WeaponAnimation.Pierce1H:
                                case WeaponAnimation.Slash1H: action = 26; break;
                                case WeaponAnimation.Bash2H:
                                case WeaponAnimation.Pierce2H:
                                case WeaponAnimation.Slash2H: action = 29; break;
                                case WeaponAnimation.ShootBow: action = 27; break;
                                case WeaponAnimation.ShootXBow: action = 28; break;
                            }
                        }

                        break;
                    }
                default: return;
            }

            from.Animate(action, 7, 1, true, false, 0);
        }

        public virtual TimeSpan OnSwing(Mobile attacker, Mobile defender)
        {
            return OnSwing(attacker, defender, 1.0);
        }

        public virtual TimeSpan OnSwing(Mobile attacker, Mobile defender, double damageBonus)
        {
            bool canSwing = true;

            if (attacker == null || defender == null)
                return WorldData.TimeTour();

            canSwing = (!attacker.Paralyzed && !attacker.Frozen);


            NubiaMobile AttMob = attacker as NubiaMobile;
            NubiaMobile DefMob = defender as NubiaMobile;

          
            //Bouclier total
            if (AttMob.GetActionCombat() == ActionCombat.DefenseTotale)
                canSwing = false;

           
            //Etourdi
            if (DefMob.IsEtourdi)
            {
                canSwing = false;
            }


            //Attaque à vue
            if (!NubiaHelper.LookAt(attacker, defender) && canSwing)
            {

                attacker.SendMessage("Vous devez êtres tourner vers {0} pour l'attaquer", defender.Name);
                PlaySwingAnimation(attacker);
                canSwing = false;
            }

            if (canSwing)
            {
                //Spell sp = attacker.Spell as Spell;

              //  canSwing = (sp == null || !sp.IsCasting || !sp.BlocksMovement);
            }

            if (canSwing && attacker.HarmfulCheck(defender))
            {
                attacker.DisruptiveAction();

                PlaySwingAnimation(attacker);

                if (attacker.NetState != null)
                    attacker.Send(new Swing(0, attacker, defender));

               /* if (attacker is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)attacker;
                    WeaponAbility ab = bc.GetWeaponAbility();

                    if (ab != null)
                    {
                        if (bc.WeaponAbilityChance > Utility.RandomDouble())
                            WeaponAbility.SetCurrentAbility(bc, ab);
                        else
                            WeaponAbility.ClearCurrentAbility(bc);
                    }
                }
            */

                CheckHit(attacker, defender);
                /* if (CheckHit(attacker, defender))
                     OnHit(attacker, defender);
                 else
                     OnMiss(attacker, defender);*/
            }

            return GetDelay(attacker);
        }

        public virtual bool CheckHit(Mobile attacker, Mobile defender)
        {

            /**
             * ANIMATION
             * PARER = 
             * ESQUIVER = 30 // 32 (se baisser)
             * COUP RECU = 20
             * TOMBER = 21
             * */
            NubiaMobile AttMob = attacker as NubiaMobile;
            NubiaMobile DefMob = defender as NubiaMobile;

            //Attaque d'opportunité
            if (mOpportunite)
            {
                mOpportunite = false;
                AttMob.PrivateOverheadMessage(MessageType.Regular, 2119, false, "Attaque d'opportunité!", AttMob.NetState);
            }

            int roll = Utility.RandomMinMax(1, 20); //Jet de base du 1d20


            bool coupCritique = (roll >= mMiniCritique);

            double bonii = 0;

            //Bonus d'attaque du perso            
            bonii += AttMob.BonusAttaque[AttMob.GetAttackTurn()]; //On chope le bon bonus de classe(s) suivant le tour d'attaque 
            AttMob.DoAttackTurn(); //Hop, un tour d'attaque pour passer au bonus suivant

            //Modificateur de charactéristique en fonction de l'arme Distance ou contact
            if ( this.MaxRange > 3 )
                bonii += (double)DndHelper.GetCaracMod(AttMob, DndStat.Dexterite);
            else
                bonii += (double)DndHelper.GetCaracMod(AttMob, DndStat.Force);
          //  Console.WriteLine(AttMob.Name + " :: Bonus d'attaque (Classe+Mod Carac): +" + bonii);

           
            //Malus quand on attaque en courant
            // ???

            //Malus de -2 a l'attaque quand on porte un pavois
            Item ibouc = AttMob.FindItemOnLayer(Layer.TwoHanded);
            if (ibouc != null && ibouc is NubiaShield)
            {
                NubiaShield bouclier = ibouc as NubiaShield;
                if (bouclier.BType == BouclierType.Pavois)
                    bonii -= 2;
                else if (bouclier.BType == BouclierType.GrandPavois)
                    bonii -= 4;
            }

            //Malus au combat contre adversaires multiples. -2 par adversaire supplémentaires
            int malusAd = 0;
            malusAd = (AttMob.Aggressed.Count + AttMob.Aggressors.Count - 1) * (-2);
            bonii -= malusAd;

            //Malus / Bonus (Defense & Defense totale comprise)
            bonii += AttMob.getBonusRoll();

            /*
             *  MALUS/BONUS DES ARMES
             *  ---------------------
            */

            bonii += mBonusAttaque;

            //LANCE
            if (mTemplate == ArmeTemplate.Lance)
            {
                if (AttMob.InRange(DefMob.Location, 1))
                    bonii -= 6;
            } //POLEARMS
            else if (mTemplate == ArmeTemplate.Hast)
            {
                if (AttMob.InRange(DefMob.Location, 1))
                    bonii -= 7;
            } //MASSEs
            else if (mTemplate == ArmeTemplate.Masse)
            {
                bonii -= 6;
            }

            //Pris au dépourvu
            bool prisDepourvu = false;
            if ((AttMob.Hidden || !DefMob.Warmode) && !DefMob.InCombat)
            {
                if (!DefMob.hasDon(DonEnum.EsquiveInstinctive))
                {
                    prisDepourvu = true;
                    DefMob.Emote("*Prit au dépourvu*");

                }
                else
                {
                    DefMob.SendMessage("Votre don Esquive Instinctive vous évite d'être pris au dépourvu");
                }
            }

            double DD = (DefMob.CA) + 1.0;
            if (prisDepourvu && DndHelper.GetCaracMod(DefMob, DndStat.Dexterite) > 0 )
                DD -= DndHelper.GetCaracMod(DefMob, DndStat.Dexterite);
            //Bonus avec un jet d'acrobatie réussi
            if (DefMob.getActionCombat() == ActionCombat.Defense 
                && DefMob.Competences[CompType.Acrobaties].getPureMaitrise() >= 5)
                DD += 1;
            else if (DefMob.getActionCombat() == ActionCombat.DefenseTotale 
                && DefMob.Competences[CompType.Acrobaties].getPureMaitrise() >= 5)
                DD += 2;
            else if (DefMob.Competences[CompType.Acrobaties].check())
                DD += 1;

            //Test de bluff
            if (AttMob.Competences[CompType.Bluff].getPureMaitrise() > 0)
            {
                int bluffDD = DefMob.Competences[CompType.Psychologie].pureRoll();
                bluffDD += DefMob.BonusAttaque[DefMob.GetAttackTurn()];
                if (DefMob.CreatureType != MobileType.Humanoide)
                    bluffDD += 4;
                if (AttMob.Competences[CompType.Bluff].check(bluffDD))
                {
                    AttMob.Emote("*feinte*");
                    DD -= (DefMob.CA);
                }
            }

            if (DefMob is NubiaPlayer && DefMob.Weapon is NubiaWeapon)
            {
                int maitrise = ((NubiaPlayer)DefMob).getMaitrise( ((NubiaWeapon)DefMob.Weapon).Template );
                DD += maitrise;
            }

   

            double finalRoll = roll + bonii;

           // Console.WriteLine("{0} Frappe {1}. Jet d'attaque: {2}({4}+{5})(DD{3})", AttMob.Name, DefMob.Name, finalRoll.ToString(), DD.ToString(), roll.ToString(), bonii.ToString());

            //   attacker.SendMessage("{0}: Jet de {1}({2}) contre CA {3} de {4}", AttMob.Name, valMod, val, DefMob.CA, DefMob.Name);
            //  defender.SendMessage("{0}: Jet de {1}({2}) contre CA {3} de {4}", AttMob.Name, valMod, val, DefMob.CA, DefMob.Name);
            if ((finalRoll > DD || roll >= 20) && roll > 1)
            {

                //POLEARMS
                if (mTemplate == ArmeTemplate.Hast)
                {
                    //Attaque non voulue
                    if (Utility.Random(100) <= 5)
                    {
                        foreach (NubiaMobile mob in AttMob.GetMobilesInRange(2))
                        {
                            if (mob == AttMob || mob == DefMob)
                                continue;
                            if (NubiaHelper.LookAt(AttMob, mob))
                            {
                                AttMob.Emote("*Frappe par inadvertance {0}*", mob.Name);
                                DefMob = mob;
                                break;
                            }
                        }
                    }
                } //MASSEs
                else if ( mTemplate == ArmeTemplate.Masse)
                {
                    //Etourdir
                    if (Utility.Random(100) <= 5) //5% mais j'ai de force contre vigueur
                    {
                        AttMob.Emote("*Frappe violement {0}*", DefMob.Name);
                        bool result = DefMob.Etourdir(AttMob);
                        if (result)
                            DefMob.Emote("*Etourdit*");
                    }
                }
                //Attaque d'opportunité
                int opDiff = DefMob.CheckForOpportuniteResult(AttMob);



                double DamageBonus = 0.0;
                DamageBonus += DndHelper.GetCaracMod(AttMob, DndStat.Force); //Bonus du modificateur de Str aux dégats.
           //     DamageBonus *= 5; // on a multiplié vie & Dégats par 5
                if (Layer.TwoHanded == Layer && DamageBonus > 0)
                    DamageBonus *= 1.5;

             //   Console.WriteLine("Damage bonus après Carac & Arme 2M : " + DamageBonus.ToString());
            

                //Attaque d'opportunité
                mOpportunite = (opDiff > 0 && AttMob.CanDoOpportuniteAttack);

                int damage = 0;
                if (AttMob.hasClasse(ClasseType.Moine) && Template == ArmeTemplate.Poing)
                {
                    damage = ClasseMoine.getDamage(AttMob.getNiveauClasse(ClasseType.Moine));
                }
                else
                    damage = DndHelper.rollDe(this.mDe, this.mNbrLance);

                damage += (int)DamageBonus + mBonusDegat;

                if (coupCritique) //Critique !
                {
                    
                    AttMob.PrivateOverheadMessage(MessageType.Regular, 2119, false, "Critique!", AttMob.NetState);
                  
                    //Poison des dagues
                  
                    damage *= mCritiqueMulti;

                    //BLESSURES
                    if ( !(AttMob.Weapon is Fists) ||  AttMob.hasDon(DonEnum.ScienceDuCombatAMainsNues) )
                    {
                        if (Utility.RandomDouble() < 0.01)
                            DefMob.AddBlessure(NubiaBlessure.getRandomBlessure());
                    }
                }


               

                OnHit(AttMob, DefMob, damage);
            }
            else
                OnMiss(AttMob, DefMob);
            
            return true;
        }

        public virtual void OnHit(Mobile attacker, Mobile defender, double Damage)
        {
           // PlayHurtAnimation(defender);

            defender.Animate(20, 7, 1, true, false, 0);

            attacker.PlaySound(DefHitSound);
            defender.PlaySound(DefHitSound);

            NubiaMobile AttMob = attacker as NubiaMobile;
            NubiaMobile DefMob = defender as NubiaMobile;



            //XP
            AttMob.GiveXP(10);

            if (DefMob.GetActionCombat() == ActionCombat.DefenseTotale)
                Damage /= 2;

            

            //Elite attaque
            if (attacker is NubiaCreature)
            {
                NubiaCreature creatAtt = attacker as NubiaCreature;
                if (creatAtt.IsElite)
                    Damage *= 1.5;
            }

            defender.Damage((int)Damage, attacker);
        }

        public virtual void OnMiss(Mobile attacker, Mobile defender)
        {
            NubiaMobile AttMob = attacker as NubiaMobile;
            NubiaMobile DefMob = defender as NubiaMobile;

            DefMob.GiveXP(10);

            if (Utility.RandomDouble() > 0.1 || DndHelper.GetArmorCA(defender as NubiaMobile) < 2)
            {
                attacker.PlaySound(DefMissSound);
                defender.PlaySound(DefMissSound);
                defender.Animate((Utility.RandomDouble() > 0.9 ? 32 : 30), 7, 1, true, false, 0);
            }
            else
            {
                defender.FixedParticles(0x37B9, 1, 4, 0x251D, 0, 0, EffectLayer.Waist);
                attacker.PlaySound(DefHitSound);
                defender.PlaySound(DefHitSound);
                defender.Animate(20, 7, 1, true, false, 0);
            }
        }

        public virtual void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }
        public virtual void GetStatusDamage(Mobile from, out int min, out int max)
        {
            min = 1;
            max = (int)mDe;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            // list.Add(String.Format("Rang: {0}\n",));
            string infos = "";

            if (Layer == Layer.TwoHanded)
                infos += "Arme à deux mains\n";

            if (mNubiaQuality != NubiaQualityEnum.Normale)
            {
                infos += NubiaQuality.getQualityName(mNubiaQuality) + "\n";
            }
            if (mTRessourceList.Count > 0)
            {
                for (int i = 0; i < mTRessourceList.Count; i++)
                {
                    infos += NubiaInfoRessource.GetInfoRessource(mTRessourceList[i]).Name;
                    if (i < mTRessourceList.Count - 1)
                        infos += ", ";
                }
                infos += "\n";
            }

            if (mBonusAttaque != 0)
                infos += "Attaque +" + mBonusAttaque +"\n";


            

            infos += String.Format("Catégorie: {0}\nDégats: {1}\nPortée: {2}m\nCritique: {3}",
                mArmeCategorie,
                DndHelper.nomDe(De, NbrLance) + (mBonusDegat>0? "+"+mBonusDegat.ToString(): ""),
                ((double)mMaxRange*1.5).ToString(),
                getStringCritique() );
            list.Add(infos);
        }
        public string getStringCritique(){
            string crit =  "";
            crit += mMiniCritique.ToString();
            if( mMiniCritique < 20 )
               crit += "-20";
            crit += "/'";
            crit += mCritiqueMulti.ToString();
            return crit;
            
        }

        public virtual TimeSpan GetDelay(Mobile m)
        {
            if (m == null)
                return WorldData.TimeTour();
            NubiaMobile mob = m as NubiaMobile;
            double baseSec = WorldData.TimeTour().Seconds;
            /*if (mob is NubiaPlayer)
            {
                int maitrise = ((NubiaPlayer)mob).getMaitrise(this.Template);
                if( maitrise > 3)
                    baseSec -= 0.1 * (maitrise - 3);
            }*/
            if (mOpportunite)
            {
                baseSec /= 2;
            }
            baseSec /= mob.BonusAttaque.Length + 1;



            return TimeSpan.FromSeconds( baseSec - (Utility.RandomDouble())  );
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); //version
            writer.Write((int)mDe);
            writer.Write((int)mNbrLance);
            writer.Write((int)mMiniCritique);
            writer.Write((int)mCritiqueMulti);
            writer.Write((int)mMaxRange);
            writer.Write((int)mArmeCategorie);
            writer.Write((int)mTemplate);

            writer.Write((Mobile)mArtisan);

            writer.Write((int)mTRessourceList.Count);
            writer.Write((int)mNubiaQuality);
            for (int i = 0; i < mTRessourceList.Count; i++)
                writer.Write((int)mTRessourceList[i]);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            mDe = (De)reader.ReadInt();
            mNbrLance = reader.ReadInt();
            mMiniCritique = reader.ReadInt();
            mCritiqueMulti = reader.ReadInt();
            mMaxRange = reader.ReadInt();
            mArmeCategorie = (ArmeCategorie)reader.ReadInt();
            mTemplate = (ArmeTemplate)reader.ReadInt();

            if (version >= 1)
            {
                mArtisan = reader.ReadMobile();

                int rescount = reader.ReadInt();
                mNubiaQuality = (NubiaQualityEnum)reader.ReadInt();
                mTRessourceList = new List<NubiaRessource>();
                for (int i = 0; i < rescount; i++)
                    mTRessourceList.Add((NubiaRessource)reader.ReadInt());

            }
        }
    }
}
