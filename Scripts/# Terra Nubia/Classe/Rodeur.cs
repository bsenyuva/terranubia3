using System;
using System.Collections;
using System.Reflection;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server
{
    public class ClasseRodeur : Classe
    {
        public override NubiaArmorType ArmorAllow { get { return NubiaArmorType.Legere; } }
        public override int GetDV { get { return 8; } }
        public override int PtComp { get { return 6; } }
        public ClasseRodeur() { }

        public override ClasseType CType { get { return ClasseType.Rodeur; } }

        public override DonEnum[][] DonClasse
        {
            get
            {
                return new DonEnum[][]{
                    new DonEnum[0], //0
                    new DonEnum[]{DonEnum.EmpathieSauvage, DonEnum.Pistage, DonEnum.DonSupClasse}, // 1
                    new DonEnum[0], //2
                   new DonEnum[0], //3
                    new DonEnum[0], //4
                    new DonEnum[0], //5
                    new DonEnum[0], //6
                    new DonEnum[0], //7
                    new DonEnum[0], //8
                    new DonEnum[0],//9
                    new DonEnum[0], //10
                   new DonEnum[0], //11
                   new DonEnum[0], //12
                    new DonEnum[0], //13
                   new DonEnum[0],//13
                   new DonEnum[0], //15
                   new DonEnum[0],//16
                    new DonEnum[0], //17
                    new DonEnum[0], //18
                    new DonEnum[0], //19
                   new DonEnum[0], //20
                };
            }
        }

        public override DonEnum[] getCustomDon(NubiaPlayer p, int niveau)
        {
            List<DonEnum> list = new List<DonEnum>();

            if (niveau == 1)
            {
                list.Add(DonEnum.EnemiJureAberration);
                list.Add(DonEnum.EnemiJureAnimal);
                list.Add(DonEnum.EnemiJureCreatureArticifielle);
                list.Add(DonEnum.EnemiJureDragon);
                list.Add(DonEnum.EnemiJureElementaire);
                list.Add(DonEnum.EnemiJureGeant);
                list.Add(DonEnum.EnemiJureHumanoide);
                list.Add(DonEnum.EnemiJureMagique);
                list.Add(DonEnum.EnemiJureMortVivant);
                list.Add(DonEnum.EnemiJurePlante);
                list.Add(DonEnum.EnemiJureVermine);
            }
          
            return list.ToArray();
        }

        public override CompType[] ClasseCompetences
        {
            get
            {
                /* Artisanat (Int), Concentration (Con),
                 * Connaissances (exploration souterraine) (Int), 
                 * Connaissances (g�ographie) (Int),
                 * Connaissances (nature) (Int),
                 * D�placement silencieux (Dex),
                 * D�tection (Sag), Discr�tion (Dex),
                 * Dressage (Cha), �quitation (Dex), 
                 * Escalade (For), Fouille (Int),
                 * Ma�trise des cordes (Dex), 
                 * Natation (For), Perception auditive (Sag), 
                 * Premiers secours (Sag), Profession (Sag), 
                 * Saut (For) et Survie (Sag).*/
                return new CompType[]{
                    CompType.Concentration,
                    CompType.Erudition,
                    CompType.DeplacementSilencieux,
                    CompType.Detection,
                    CompType.Discretion,
                    CompType.Dressage,
                    CompType.Equitation,
                    CompType.Escalade,
                    CompType.Fouille,
              //      CompType.MaitriseCordes,
                    CompType.PerceptionAuditive,
                    CompType.PremiersSecours,
                    CompType.Saut,
                    CompType.Survie
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
                    new int[] { 1 }, //1
                    new int[] { 2 }, //2
                    new int[] { 3 }, //3
                    new int[] { 4 }, //4
                    new int[] { 5 }, //5
                    new int[] { 6,1 }, //6
                    new int[] { 7,2 }, //7
                    new int[] { 8,3 }, //8
                    new int[] { 9,4 }, //9
                    new int[] { 10,5 }, //10
                    new int[] { 11,6,1 }, //11
                    new int[] { 12,7,2 }, //12
                    new int[] { 13,8,3 }, //13
                    new int[] { 14,9,4 }, //14
                    new int[] { 15,10,5 }, //15
                    new int[] { 16,11,6,1 }, //16
                    new int[] { 17,12,7,2 }, //17
                    new int[] { 18,13,8,3 }, //18
                    new int[] { 19,14,9,4 }, //19
                    new int[] { 20,15,10,5 } //20
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
                    0, //1
                    0, //2
                    1, //3
                    1, //4
                    1, //5
                    2, //6
                    2, //7
                    2, //8
                    3, //9
                    3, //10
                    3, //11
                    4, //12
                    4, //13
                    4, //14
                    5, //15
                    5, //16
                    5, //17
                    6, //18
                    6, //19
                    6, //20
                };
            }
        }
    }
}