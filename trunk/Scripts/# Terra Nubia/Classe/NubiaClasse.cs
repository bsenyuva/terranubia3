﻿using System;
using System.Collections;
using System.Reflection;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server
{
    public enum ClasseType
    {
        Barbare,
        Barde,
        Druide,
        Ensorceleur,
        Guerrier,
        Magicien,
        Moine,
        Paladin,
        Pretre,
        Rodeur,
        Roublard,
        //Artisana
        ArtisanCouturier,
        ArtisanForgeron,
        ArtisanIngenieur,
        ArtisanEbeniste,
        ArtisanAlchimiste,
        ArtisanPaysan,

        Maximum //pour comptage
    }
    public abstract class Classe
    {
        public virtual int GetDV { get { return 4; } }
        public virtual int PtComp { get { return 2; } } //x pts par niveau (d'achat)
        public virtual NubiaArmorType ArmorAllow { get { return NubiaArmorType.None; } }
        public virtual BouclierType BouclierAllow { get { return BouclierType.None; } }
        public virtual ArmeCategorie ArmeAllow { get { return ArmeCategorie.Courante; } }

        #region Magie !
        public virtual Type[][] SortAllow
        {
            get
            {
                return new Type[][]{ new Type[0]};
            }
        }
        public virtual int[][] MagieAllow
        {
            get
            {
                return new int[][] { new int[0] };
            }
        }
        #endregion

        public virtual DonEnum[][] DonClasse
        {
            get
            {
                return new DonEnum[][]{ new DonEnum[0] };
            }
        }

        public static string GetNameClasse(ClasseType ct)
        {
            switch (ct)
            {
                case ClasseType.Barbare: return "Barbare";
                case ClasseType.Barde: return "Barde";
                case ClasseType.Druide: return "Druide";
                case ClasseType.Ensorceleur: return "Ensorceleur";
                case ClasseType.Guerrier: return "Guerrier";
                case ClasseType.Magicien: return "Magicien";
                case ClasseType.Moine: return "Moine";
                case ClasseType.Paladin: return "Paladin";
                case ClasseType.Pretre: return "Prêtre";
                case ClasseType.Rodeur: return "Rôdeur";
                case ClasseType.Roublard: return "Roublard";
                case ClasseType.ArtisanAlchimiste: return "Artisan: Alchimiste";
                case ClasseType.ArtisanCouturier: return "Artisan: Couturier";
                case ClasseType.ArtisanEbeniste: return "Artisan: Ebeniste";
                case ClasseType.ArtisanForgeron: return "Artisan: Forgeron";
                case ClasseType.ArtisanIngenieur: return "Artisan: Ingenieur";
                case ClasseType.ArtisanPaysan: return "Artisan: Paysan";
            }
            return "Classe: Inconnu";
        }

        public static Type GetClasse(ClasseType ct)
        {
            //Console.WriteLine("GetClasse dans Classe: "+ct);
            Type type = typeof(ClasseGuerrier);
            switch (ct)
            {
                case ClasseType.Barbare: type = typeof(ClasseBarbare); break;
                case ClasseType.Barde: type = typeof(ClasseBarde); break;
                case ClasseType.Druide: type = typeof(ClasseDruide); break;
                case ClasseType.Ensorceleur: type = typeof(ClasseEnsorceleur); break;
                case ClasseType.Guerrier: type = typeof(ClasseGuerrier); break;
                case ClasseType.Magicien: type = typeof(ClasseMagicien); break;
                case ClasseType.Moine: type = typeof(ClasseMoine); break;
                case ClasseType.Paladin: type = typeof(ClassePaladin); break;
                case ClasseType.Pretre: type = typeof(ClassePretre); break;
                case ClasseType.Rodeur: type = typeof(ClasseRodeur); break;
                case ClasseType.Roublard: type = typeof(ClasseRoublard); break;
                case ClasseType.ArtisanAlchimiste: type = typeof(ArtisanAlchimiste); break;
                case ClasseType.ArtisanCouturier: type = typeof(ArtisanCouturier); break;
                case ClasseType.ArtisanEbeniste: type = typeof(ArtisanEbeniste); break;
                case ClasseType.ArtisanForgeron: type = typeof(ArtisanForgeron); break;
                case ClasseType.ArtisanIngenieur: type = typeof(ArtisanIngenieur); break;
                case ClasseType.ArtisanPaysan: type = typeof(ArtisanPaysan); break;
            }
            return type;
        }
        public virtual ClasseType CType { get { return ClasseType.Guerrier; } }

        private int m_niveau = 0;
        public int Niveau { get { return m_niveau; } set { m_niveau = value; } }

        public virtual CompType[] ClasseCompetences
        {
            get
            {
                return new CompType[] { CompType.Acrobaties };
            }
        }

        public bool isComptenceClasse(CompType c)
        {
            bool isit = false;
            for (int i = 0; i < ClasseCompetences.Length; i++)
            {
                if (c == ClasseCompetences[i])
                {
                    isit = true;
                    break;
                }
            }
            return isit;
        }

        public virtual int[][] BonusAttaque
        {
            get
            {
                return new int[][] { new int[] { 0 } };
            }
        }

        public virtual int[] BonusReflexe
        {
            get
            {
                return new int[] { 0 };
            }
        }

        public virtual int[] BonusVigueur
        {
            get
            {
                return new int[] { 0 };
            }
        }

        public virtual int[] BonusVolonte
        {
            get
            {
                return new int[] { 0 };
            }
        }

        public Classe()
        {
        }
    }
}