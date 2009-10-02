using System;
using System.Collections;
using System.Reflection;
using Server.Items;
using Server.Mobiles;

namespace Server
{
    public class ClasseMoine : Classe
    {
        public override NubiaArmorType ArmorAllow { get { return NubiaArmorType.None; } }
        public override int GetDV { get { return 8; } }
        public override int PtComp { get { return 4; } }
        public ClasseMoine() { }

        public override ClasseType CType { get { return ClasseType.Moine; } }
       
        public override CompType[] ClasseCompetences
        {
            get
            {
                /*Acrobaties (Dex), Artisanat (Int), 
                 * Concentration (Con), Connaissances (myst�res) (Int), 
                 * Connaissances (religion) (Int), D�placement silencieux (Dex), 
                 * D�tection (Sag), Diplomatie (Cha), Discr�tion (Dex),
                 * �quilibre (Dex), Escalade (For), �vasion (Dex),
                 * Natation (For), Perception auditive (Sag), 
                 * Profession (Sag), Psychologie (Sag), 
                 * Repr�sentation (Cha) et Saut (For).*/
                return new CompType[]{
                    CompType.Acrobaties,
                    CompType.Concentration,
                    CompType.Erudition,
                    CompType.DeplacementSilencieux,
                    CompType.Detection,
                    CompType.Diplomatie,
                    CompType.Discretion,
                    CompType.Equilibre,
                    CompType.Escalade,
                    CompType.Evasion,
                    CompType.PerceptionAuditive,
                    CompType.Psychologie,
                    CompType.Representation,
                    CompType.Saut
               };
            }
        }
        public override int[][] BonusAttaque
        {
            get
            {
                return new int[][] 
                { 
                    new int[] { 0 }, //0
                    new int[] { 0 }, //1
                    new int[] { 1 }, //2
                    new int[] { 2 }, //3
                    new int[] { 3 }, //4
                    new int[] { 3 }, //5
                    new int[] { 4 }, //6
                    new int[] { 5 }, //7
                    new int[] { 6,1 }, //8
                    new int[] { 6,1 }, //9
                    new int[] { 7,2 }, //10
                    new int[] { 8,3 }, //11
                    new int[] { 9,4 }, //12
                    new int[] { 9,4 }, //13
                    new int[] { 10,5 }, //14
                    new int[] { 11,6,1 }, //15
                    new int[] { 12,7,2 }, //16
                    new int[] { 12,7,2 }, //17
                    new int[] { 13,8,3 }, //18
                    new int[] { 14,9,4 }, //19
                    new int[] { 15,10,5 } //20
                };
            }
        }

        public override int[] BonusReflexe
        {
            get
            {
                return new int[]
                {
                    0, //0
                    2, //1
                    3, //2
                    3, //3
                    4, //4
                    4, //5
                    5, //6
                    5, //7
                    6, //8
                    6, //9
                    7, //10
                    7, //11
                    8, //12
                    8, //13
                    9, //14
                    9, //15
                    10, //16
                    10, //17
                    11, //18
                    11, //19
                    12, //20
                };
            }
        }

        public override int[] BonusVigueur
        {
            get
            {
                return new int[]
                {
                    0, //0
                    2, //1
                    3, //2
                    3, //3
                    4, //4
                    4, //5
                    5, //6
                    5, //7
                    6, //8
                    6, //9
                    7, //10
                    7, //11
                    8, //12
                    8, //13
                    9, //14
                    9, //15
                    10, //16
                    10, //17
                    11, //18
                    11, //19
                    12 //20
                };
            }
        }

        public override int[] BonusVolonte
        {
            get
            {
                return new int[]
                {
                    0, //0
                    2, //1
                    3, //2
                    3, //3
                    4, //4
                    4, //5
                    5, //6
                    5, //7
                    6, //8
                    6, //9
                    7, //10
                    7, //11
                    8, //12
                    8, //13
                    9, //14
                    9, //15
                    10, //16
                    10, //17
                    11, //18
                    11, //19
                    12, //20
                };
            }
        }
    }
}