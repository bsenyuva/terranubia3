﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Spells;


namespace Server.Commands
{
    public class CommandesJoueur
    {
        public static void Initialize()
        {

            CommandSystem.Register("tissage", AccessLevel.GameMaster,
              new CommandEventHandler(tissage_OnCommand));

            CommandSystem.Register("reputations", AccessLevel.Player,
              new CommandEventHandler(reputation_OnCommand));


       /*     CommandSystem.Register("maitrise", AccessLevel.Player,
              new CommandEventHandler(maitrise_OnCommand));*/

            CommandSystem.Register("magie", AccessLevel.GameMaster,
              new CommandEventHandler(magie_OnCommand));
            CommandSystem.Register("magiegm", AccessLevel.GameMaster,
              new CommandEventHandler(magiegm_OnCommand));

            CommandSystem.Register("deguisement", AccessLevel.Player,
              new CommandEventHandler(deguisement_OnCommand));
            CommandSystem.Register("cache", AccessLevel.Player,
              new CommandEventHandler(cache_OnCommand));
            CommandSystem.Register("acrobatie", AccessLevel.Player,
               new CommandEventHandler(acrobatie_OnCommand));
            CommandSystem.Register("fiche", AccessLevel.Player, 
                new CommandEventHandler(fiche_OnCommand));
            CommandSystem.Register("comp", AccessLevel.Player,
               new CommandEventHandler(comp_OnCommand));
            CommandSystem.Register("anim", AccessLevel.Player,
                new CommandEventHandler(anim_OnCommand));
            CommandSystem.Register("blessure", AccessLevel.Player,
               new CommandEventHandler(blessure_OnCommand));
        }
        public static void reputation_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            p.CloseGump(typeof(GumpFactions));
            p.SendGump(new GumpFactions(p));
        }

        public static void maitrise_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            p.CloseGump(typeof(SortMaitriseGump));
            p.SendGump(new SortMaitriseGump(p));
        }
        private static bool checkAffinite(NubiaPlayer p)
        {
            if (p.Domaine == SortDomaine.All || p.Energie == SortEnergie.All)
            {
                p.SendGump(new SortMaitriseGump(p));
                p.SendMessage("Vous devez choisir vos affinités avant tout !");
                return false;
            }
            else
                return true;
        }
        public static void tissage_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (checkAffinite(p))
            {
                SortNubia newJutsu = new SortNubiaDestruction();
                p.CloseGump(typeof(SortCreationGump));
                p.SendGump(new SortCreationGump(p, newJutsu));
            }
        }
        public static void magiegm_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (checkAffinite(p))
            {
                p.CloseGump(typeof(SortListGump));
                p.SendGump(new SortListGump(p, true));
            }
        }
        public static void magie_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (checkAffinite(p))
            {
                p.CloseGump(typeof(NewSortListGump));
                p.SendGump(new NewSortListGump(p));
            }
       /*     if (e.Arguments.Length <= 0)
            {
                p.SendMessage("Utilisation: .magie [nom_de_classe] ( ex: .magie magicien )");
                return;
            }
            else
            {
                string clstr = e.Arguments[0];
                Console.WriteLine(" Magie: " + clstr);
                try
                {
                    ClasseType cl = ClasseType.None;
                    for (int c = 0; c < (int)ClasseType.Maximum; c++)
                    {
                        if (clstr.ToLower() == ((ClasseType)c).ToString().ToLower())
                        {
                            cl = (ClasseType)c;
                            break;
                        }
                    }

                    if (cl != ClasseType.None)
                        p.SendGump(new GumpLivreMagie(p, cl));
                    else
                        p.SendMessage("Mauvais nom de classe");

                }
                catch (Exception ex) { }
            }*/
        }
        public static void deguisement_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (p.Competences[CompType.Deguisement].getPureMaitrise() >= 2)
            {
                p.RemoveDeguisement();
                p.CloseGump(typeof(GumpDeguisement) );
                p.SendGump(new GumpDeguisement(p));
            }
            else
                p.SendMessage("Vous devez avoir au moin 2 en déguisement");
        }
        public static void blessure_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;

            p.Target = new MedecineTarget(p);
        }
        public static void cache_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            int DD = 10;
            int NuitModus = 0;
            if (p.Competences.mustWait())
                return;
            if (LightCycle.ComputeLevelFor(p) > p.LightLevel)
            {
                if (LightCycle.ComputeLevelFor(p) > 10)
                    NuitModus -= 5;
                else if (LightCycle.ComputeLevelFor(p) < 10)
                    NuitModus += 5;

                p.SendMessage("Bonus de nuit: -" + NuitModus);
            }
            else
            {
                if (p.LightLevel > 10)
                    NuitModus -= 5;
                else if (p.LightLevel < 10)
                    NuitModus += 5;
                p.SendMessage("Bonus de nuit: -" + NuitModus);
            }
            DD += NuitModus;
            DD += (int)p.Taille * 2;
            bool mustBluff = false;
            int DDBluff = 10;
            foreach (NubiaMobile mob in p.GetMobilesInRange(8))
            {
                if (mob == p)
                    continue;
                if (mob.CanSee(p))
                {
                    int mobPsy = mob.Competences[CompType.Psychologie].pureRoll(0) - NuitModus;
                    int bluff = p.Competences[CompType.Bluff].pureRoll(0) + NuitModus;
                    if (mobPsy < bluff)
                    {
                        mob.SendMessage("Quelques choses vous attire l'oeil plus loin");
                    }
                    DDBluff += mobPsy - bluff;
                    mustBluff = true;
                }
            }

            if (mustBluff)
            {
                if (p.Competences[CompType.Bluff].check(DDBluff,1))
                {
                    p.SendMessage("Vous réussissez à distraitre les observateurs pour vous cacher");
                }
                else
                {
                    p.SendMessage("Trop de monde vous observer, impossible de vous cacher");
                    return;
                }
            }
            if (p.Competences[CompType.Discretion].check(DD,1))
            {
                p.SendMessage("Vous vous dissimulez avec discretion");
                p.Hidden = true;
            }
            else
            {
                p.SendMessage("Vous n'arrivez pas a vous dissimuler");
            }
        }
        public static void acrobatie_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (p.Competences.mustWait())
                return;

            if (p.InCombat)
            {
                p.SendMessage("Vous ne pouvez pas faire d'acrobatie pour l'instant");
                return;
            }

            bool can = p.Competences[CompType.Acrobaties].check(1);
            if (can)
            {
                p.Emote("*execute une acrobatie*");
                foreach (Mobile m in p.GetMobilesInRange(8))
                {
                    if (m is NubiaPlayer)
                    {
                        NubiaPlayer player = m as NubiaPlayer;
                        if (player.CanSee(p))
                        {
                            player.changeMoral(Utility.Random(1, 8));
                        }
                    }
                }
            }
            else
            {
                p.Emote("*rate son acrobatie*");
                p.Damage(Utility.Random(1, 4));
            }
        }

        public static void anim_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            if (e.Arguments.Length > 0)
            {
                try
                {
                    int a = Convert.ToInt32( e.Arguments[0] );
                    
                    p.Animate(a, 7, 1, true, false, 0 );
                }
                catch (Exception ex) { }
            }
        }

        public static void comp_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            p.CloseGump(typeof(GumpCompetences));
            p.SendGump(new GumpCompetences(p, p));
        }

        public static void fiche_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            p.CloseGump(typeof(GumpFichePerso));
            p.SendGump(new GumpFichePerso(p, p));
        }
    }
}
