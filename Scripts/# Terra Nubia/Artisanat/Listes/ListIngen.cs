﻿using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Targeting;
using Server.Engines.Harvest;

namespace Server.Engines
{
    public class ListIngen : CraftList
    {
        public override string[] Categorie
        {
            get
            {
                return new string[]{
                    "Outils", //0
                    "Lumières", //1
                    "Jeux", // 2
                    "Contenants", // 3
                };
            }
        }

        public override void ConstructList()
        {
            // O U T I L S
            AddEntry("Parchemin d'ecriture", 0, typeof(NubiaParchemin), 5, 15,
                new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseBois), 1) });
            AddEntry("Nécessaire d'écriture", 0, typeof(EruditionTool), 5, 10,
                new RessourceNeed[]{ 
                    new RessourceNeed(typeof(Feather), 2),
                    new RessourceNeed(typeof(BaseMetal), 1) });

            AddEntry("Marteau de forgeron", 0, typeof(ForgeronTool), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseBois), 1),
                    new RessourceNeed(typeof(BaseMetal), 1) });

            AddEntry("Nécessaire de couture", 0, typeof(CoutureTool), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseBois), 2),
                    new RessourceNeed(typeof(BaseMetal), 1) });

            AddEntry("Ciseaux", 0, typeof(Scissors), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 2) });

            AddEntry("Mortier & Pilon", 0, typeof(MortarPestle), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 3) });

            AddEntry("Hachette", 0, typeof(Hatchet), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 4) ,
                    new RessourceNeed (typeof(BaseBois), 1) });

            AddEntry("Pioche", 0, typeof(Pickaxe), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 4) ,
                    new RessourceNeed (typeof(BaseBois), 1) });

            AddEntry("Crochet à serrure", 0, typeof(Lockpick), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 1)});

            // L U M I E R E S
            AddEntry("Lanterne", 1, typeof(Lantern), 5, 10,
               new RessourceNeed[]{ 
                    new RessourceNeed(typeof(BaseMetal), 3)});
            
            AddEntry("Chandelle", 1, typeof(Candle), 5, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseMetal), 2)});

            // J E U X
            AddEntry("Jeu de backgammon", 2, typeof(Backgammon), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 5)});
            AddEntry("Jeu d'échec", 2, typeof(Chessboard), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 5)});
            AddEntry("Jeu de dames", 2, typeof(CheckerBoard), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 5)});
            AddEntry("Dés", 2, typeof(Dices), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 1)});

            // Contenants
            AddEntry("Petite caisse", 3, typeof(SmallCrate), 2, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 3)});

            AddEntry("Caisse", 3, typeof(MediumCrate), 2, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 5)});

            AddEntry("Grosse caisse", 3, typeof(MediumCrate), 2, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseBois), 8)});

            AddEntry("Boîte de bois", 3, typeof(WoodenBox), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseMetal), 1),
                    new RessourceNeed(typeof(BaseBois), 5)});

            AddEntry("Boîte de métal", 3, typeof(MetalBox), 3, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseMetal), 6)});

            AddEntry("Coffre de bois", 3, typeof(WoodenChest), 5, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseMetal), 2),
                    new RessourceNeed(typeof(BaseBois), 8)});

            AddEntry("Coffre de métal", 3, typeof(MetalChest), 5, 10,
                new RessourceNeed[]{
                    new RessourceNeed(typeof(BaseMetal), 10)});

            
           

        }

        public ListIngen()
            : base(typeof(CraftIngenSystem))
        {
        }
    }
}