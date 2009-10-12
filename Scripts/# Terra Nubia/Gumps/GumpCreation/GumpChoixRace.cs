﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
    public class GumpChoixRace : GumpNubia
    {
        private NubiaPlayer m_owner;
        private int choix = 0;
        public GumpChoixRace(NubiaPlayer _owner, int _choix)
            : base("Choix de la race...", 400, 420)
        {
            m_owner = _owner;
            choix = _choix;
            //Closable = true;

            int y = YBase;
            int x = XBase;
            int line = 0;
            int scale = 200;
            int decal = 125;
            int col = 0;
            //Humain
            addRaceChoix(x + col * decal, y + line * scale, 126, "Humain", 50+(int)RaceType.Humain);
            col++;
            //Demi-Elfe
            addRaceChoix(x + col * decal, y + line * scale, 119, "Demi-elfe", 50 + (int)RaceType.DemiElf);
            col++;
            //Demi-Orc
            addRaceChoix(x + col * decal, y + line * scale, 124, "Demi-orc", 50 + (int)RaceType.DemiOrc);
            col++;

            line++;
            col = 0;

            //Halfelin
            addRaceChoix(x + col * decal, y + line * scale, 122, "Halfelin", 50 + (int)RaceType.Halfelin);
            col++;
            //Elfe lune
            addRaceChoix(x + col * decal, y + line * scale, 128, "Elfe lunaire", 50 + (int)RaceType.ElfLune);
            col++;
            //Githzerai
            addRaceChoix(x + col * decal, y + line * scale, 118, "Githzerai", 50 + (int)RaceType.Githzerai);
            col++;

         /*   //Elfe
            addRaceChoix(x + col * decal, y + line * scale, 125, "Elfe", 14);
            col++;

            line++;
            col = 0;

            //ASAMIR
            addRaceChoix(x + col *decal, y + line *scale, 114, "Aasimar", 16);
            col++;
            //Changelin
            addRaceChoix(x + col * decal, y + line * scale, 115, "Changelin", 17);
            col++;
            //Drakeide
            addRaceChoix(x + col * decal, y + line * scale, 116, "Drakéide", 18);
            col++;
            //Drow
            addRaceChoix(x + col * decal, y + line * scale, 117, "Elfe noir", 19);
            col++;
           
            */

            if (choix > 0)
            {
                AddSimpleButton(x + 5, y + line * scale + 175, 100, "Continuer");
            }

           
        }
        private void addRaceChoix(int x, int y, int image, string race, int ID)
        {
            AddImage(x, y, image);
            if (ID == choix)
                AddImage(x + 10, y + 145, 2153);
            else
                AddButton(x + 10, y + 145, 2151, 2152, ID, GumpButtonType.Reply, 0);
            AddLabel(x+ 45, y + 147, ColorText, race);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile f = sender.Mobile;
            NubiaPlayer from = f as NubiaPlayer;

            if (info.ButtonID > 0 && info.ButtonID < 100 || choix <= 0)
            {
                choix = info.ButtonID;
                from.SendGump(new GumpChoixRace(from, choix));
            }
            else if (info.ButtonID == 100)
            {
                from.changeRace((RaceType)choix - 50);
                from.SendGump(new GumpMenuCreation(from));
            }
        }
    }
}
