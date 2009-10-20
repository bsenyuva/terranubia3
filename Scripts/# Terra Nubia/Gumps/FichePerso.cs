using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Multis;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.Spells;

namespace Server.Gumps
{
    public class GumpFichePerso : GumpNubia
    {
        private const double Width = 97;

        private NubiaPlayer mOwner;
        private NubiaPlayer mViewer;
        private Classe[] classes = new Classe[0];
        private bool canMultiClasse(NubiaMobile mob)
        {
            bool ok = true;
            for (int i = 0; i < classes.Length; i++)
            {
                if (classes[i].Niveau < 4 && !(classes[i] is ClasseArtisan))
                    ok = false;
            }
            return ok;
        }
        private bool canUpClasse(NubiaMobile mob, Classe c)
        {
            int nivClasseMini = 4;
            if (c.Niveau < nivClasseMini)
                return true;
            else if (c is ClasseArtisan && c.Niveau < 5)
                return true;
            else if (c is ClasseArtisan && c.Niveau >= 5)
                return false;
            else
            {
                for (int i = 0; i < classes.Length; i++)
                {
                    if (classes[i].CType != c.CType)
                        if (classes[i].Niveau < nivClasseMini)
                            return false;
                }
            }
            return true;
        }
        public GumpFichePerso(NubiaPlayer _owner, NubiaPlayer _viewer)
            : base("Fiche de personnage", 520, 400, 250)
        {
            Closable = true;
            mOwner = _owner;
            mViewer = _viewer;

            int y = YBase;
            int x = XBase;
            int line = 0;
            int scale = 22;
            int decal = 5;
            x += 5;

            AddBackground(x - 5, y, 240, 150 + (scale * mOwner.GetClasses().Count), 3000);
            AddLabel(x, y + line * scale, ColorText, "Nom: ");
            AddLabel(x + 50, y + line * scale, ColorTextLight, mOwner.Name);
            line++;
            AddLabel(x, y + line * scale, ColorText, "Race: ");
            if (mOwner.Race != null)
                AddLabel(x + 50, y + line * scale, ColorTextLight, mOwner.Race.Name);
            line++;


            AddLabel(x, y + line * scale, ColorText, "Moral: ");
            int colMoral = ColorText;
            if (mOwner.CurrentMoral > Moral.Normal)
                colMoral = ColorTextGreen;
            else if (mOwner.CurrentMoral < Moral.Normal)
                colMoral = ColorTextRed;
            AddLabel(x + 50, y + line * scale, colMoral, mOwner.MoralString);
            line++;
            /* Barre d'XP */
            AddLabel(x, y + (line * scale), ColorText, "Experience: ");

            int XpTot = 0;
            for (int n = 0; n < mOwner.Niveau; n++)
                XpTot += XPHelper.GetXpForLevel(n);
            int XpActuel = mOwner.XP - XpTot;
            // Console.WriteLine("Experience : {0} / {1}", XpActuel, XPHelper.GetXpForLevel(mOwner.Niveau + 1));
            double ratio = ((double)XpActuel / (double)XPHelper.GetXpForLevel(mOwner.Niveau + 1));
            // Console.WriteLine("Ratio: "+ratio);
            int largeurXp = (int)(ratio * Width);
            AddImage(x + 70, y + (line * scale) + 5, 0x809);
            AddImageTiled(x + 70, y + (line * scale) + 5, largeurXp, 8, 0x808);
            line++;

            //CLASSES
            bool canUp = false;
            if (mOwner.NivClassDispo > 0)
            {
                AddLabel(x, y + (line * scale), ColorTextGreen, "Repartissez vos points de classes!");
                canUp = true;
                line++;
            }

            classes = new Classe[mOwner.GetClasses().Count];
            mOwner.GetClasses().CopyTo(classes, 0);
            for (int i = 0; i < classes.Length; i++) //50+i baisser, 75+i augmenter
            {

                //AddButton(x, y + (line * scale), 0x1467, 0x1467, 50 + i, GumpButtonType.Reply, 0);
                if (canUp && canUpClasse(mOwner, classes[i]))
                    AddButton(x + 15, y + (line * scale), 0x1468, 0x1468, 75 + i, GumpButtonType.Reply, 0);
                AddLabel(x + 20 + (canUp ? 15 : 0), y + (line * scale), ColorTextYellow, classes[i].CType.ToString() + " niveau " + classes[i].Niveau.ToString());
                line++;
            }
            if (classes.Length < 2 && canUp && canMultiClasse(mOwner)) //Nouvelle classe 100
            {
                AddButton(x, y + (line * scale), 0x1468, 0x1468, 100, GumpButtonType.Reply, 0);
                AddLabel(x + 20, y + (line * scale), ColorTextLight, "Choisir une nouvelle classe");
                line++;
            }


            //ARMURES
            AddImage(x, y + (line * scale), 0x5200);
            AddLabel(x + 50, y + (line * scale), ColorText, "Classe d'armure: ");
            AddLabel(x + 150, y + (line * scale), (mOwner.CA > 0 ? ColorTextYellow : ColorTextRed), (mOwner.CA > 0 ? "+" : "") + mOwner.CA);
            line++;
            // AddLabel(XCol + 50, y + (line * scale), ColorText, "Armures: ");
            string armorList = "Aucune armure";
            if (mOwner.ArmorAllow >= NubiaArmorType.Legere)
                armorList = "L�g�res";
            if (mOwner.ArmorAllow >= NubiaArmorType.Intermediaire)
                armorList += ", Interm�diaires";
            if (mOwner.ArmorAllow >= NubiaArmorType.Lourde)
                armorList += ", Lourdes";
            AddLabel(x + 50, y + (line * scale), ColorTextLight, armorList);

            line++;



            AddBackground(x - 5, y + line * scale + 20, 240, 180, 3000);


            line++;
            int xCharac = x + 100;
            int xMod = xCharac + 20;

            bool canUpCharac = (mOwner.RawStr + mOwner.RawDex + mOwner.RawCons + mOwner.RawInt + mOwner.RawSag + mOwner.RawCha) < DndHelper.getTotalStat(mOwner.Niveau);

            if (canUpCharac)
            {
                AddLabel(x, y + line * scale, ColorTextGreen, "Point de charact�ristique dispo: " + (DndHelper.getTotalStat(mOwner.Niveau) - (mOwner.RawStr + mOwner.RawDex + mOwner.RawCons + mOwner.RawInt + mOwner.RawSag + mOwner.RawCha)).ToString());
                line++;
            }

            //Force
            int colorCharac = ColorTextGray;
            double modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Force);
            bool isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Force);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawStr < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Force, "Force: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Force: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.RawStr.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;
            //Dexterite
            colorCharac = ColorTextGray;
            modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Dexterite);
            isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Dexterite);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawDex < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Dexterite, "Dext�rit�: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Dext�rit�: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.RawDex.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;
            //Consitution
            colorCharac = ColorTextGray;
            modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Constitution);
            isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Constitution);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawCons < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Constitution, "Constitution: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Constitution: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.Cons.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;
            //Intelligence
            colorCharac = ColorTextGray;
            modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Intelligence);
            isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Intelligence);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawInt < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Intelligence, "Intelligence: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Intelligence: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.Int.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;
            //Sagesme
            colorCharac = ColorTextGray;
            modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Sagesse);
            isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Sagesse);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawSag < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Sagesse, "Sagesse: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Sagesse: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.Sag.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;
            //Charisme
            colorCharac = ColorTextGray;
            modCharac = DndHelper.GetCaracMod(mOwner, DndStat.Charisme);
            isCharacLimited = DndHelper.CaracModIsLimited(mOwner, DndStat.Charisme);
            if (modCharac < 0 || isCharacLimited)
                colorCharac = ColorTextRed;
            else if (modCharac > 0)
                colorCharac = ColorTextLight;
            if (canUpCharac && mOwner.RawCha < mOwner.Niveau + 17)
                AddSimpleButton(x, y + line * scale, 200 + (int)DndStat.Charisme, "Charisme: ");
            else
                AddLabel(x, y + (line * scale), ColorText, "Charisme: ");
            AddLabel(xCharac, y + (line * scale), ColorText, mOwner.Cha.ToString());
            AddLabel(xMod, y + (line * scale), colorCharac, "/ " + (modCharac >= 0 ? "+" : "") + modCharac.ToString());
            line++;


            /**
             * 
             * 2ND COLONNE
             * 
             * Previous = 2322, 2323
             * 
             * PrevFleche = 4014, 4015
             * NextFleche = 4005, 4006
             * 
             * */


            /*
             * 
             * */

            int xprev = XCol;
            int xnext = XCol + 200;
            int yprevnext = y;
            line = 1;
            // PAGE 1
            AddPage(1);
            AddButton(xprev, yprevnext, 4014, 4015, 5, GumpButtonType.Page, 1);
            AddButton(xnext, yprevnext, 4005, 4006, 6, GumpButtonType.Page, 2);
            AddLabel(XCol, y + line * scale, ColorTextYellow, "Bonus & Maitrises d'armes");
            line++;

            AddLabel(XCol, y + line * scale, ColorTextLight, "Bonus aux jets:");
            line++;
            AddLabel(XCol, y + line * scale, ColorText, "Attaque:");
            string attaquebonii = "";
            for (int i = 0; i < mOwner.BonusAttaque.Length; i++)
                attaquebonii += "+" + mOwner.BonusAttaque[i] + (i < mOwner.BonusAttaque.Length - 1 ? " / " : "");
            AddLabel(XCol + 60, y + line * scale, ColorTextYellow, attaquebonii);

            line++;
            AddLabel(XCol, y + line * scale, ColorText, "Reflexe:");
            AddLabel(XCol + 60, y + line * scale, ColorTextYellow, "+" + mOwner.getBonusReflexe().ToString());

            line++;
            AddLabel(XCol, y + line * scale, ColorText, "Volont�:");
            AddLabel(XCol + 60, y + line * scale, ColorTextYellow, "+" + mOwner.getBonusVolonte().ToString());

            line++;
            AddLabel(XCol, y + line * scale, ColorText, "Vigueur:");
            AddLabel(XCol + 60, y + line * scale, ColorTextYellow, "+" + mOwner.getBonusVigueur().ToString());

            // MAITRISES d'ARMES

            line++;
            AddLabel(XCol, y + line * scale, ColorTextLight, "Maitrises d'armes");
            int maitrise_depense = mOwner.PtsMaitriseTotal();
            int maitrise_max = DndHelper.getMaitriseMax(mOwner.Niveau);
            int maitrise_total = DndHelper.getTotalMaitrise(mOwner.Niveau);
            int maitrise_restant = maitrise_total - maitrise_depense;
            if (maitrise_restant > 0)
                AddLabel(XCol + 150, y + line * scale, ColorTextGreen, "Disponible(s): " + (maitrise_restant).ToString());
            int maitrise_line = 1;
            int maitriseX = 0;
            scale -= 3;
            for (int i = 0; i < (int)ArmeTemplate.Maximum; i++)
            {
                if ((int)ArmeTemplate.Maximum == i)
                    continue;
                bool canIncrease = false;
                canIncrease = (maitrise_restant > 0) && mOwner.getMaitrise((ArmeTemplate)i) < maitrise_max;
                string maitriseName = NubiaWeapon.getTemplateString((ArmeTemplate)i);
                int mcol = ColorText;
                int mvalue = mOwner.getMaitrise((ArmeTemplate)i);
                if (mvalue > 0)
                    mcol = ColorTextLight;
                int buttonDecal = 0;
                if (canIncrease)
                {
                    buttonDecal = 15;
                    AddSimpleButton(XCol + maitriseX, 15 + y + 5 + (maitrise_line + line) * scale, 250 + i, maitriseName, mcol);
                }
                else
                    AddLabel(XCol + maitriseX, 15 + y + 5 + (maitrise_line + line) * scale, mcol, maitriseName);

                for (int v = 0; v < mvalue; v++)
                    AddImage(XCol + 90 + maitriseX + buttonDecal + (13 * v), 17 + y + 5 + (maitrise_line + line) * scale, 9022);

                maitrise_line++;

            }
            scale += 3;

            // DONS
            // PAGE 2
            AddPage(2);
            line = 1;
            AddButton(xprev, yprevnext, 4014, 4015, 5, GumpButtonType.Page, 1);
            AddButton(xnext, yprevnext, 4005, 4006, 6, GumpButtonType.Page, 2);
            AddLabel(XCol, y + line * scale, ColorTextYellow, "Dons du personnage");
            line++;

            foreach (DonEnum don in mOwner.Dons)
            {
                int niv = mOwner.getDonNiveau(don);
                if (niv > 0)
                {
                    string dname = "";
                    if (BaseDon.DonBank.ContainsKey(don.ToString().ToLower()))
                        dname = BaseDon.DonBank[don.ToString().ToLower()].Name;
                    else
                        dname = don.ToString();

                    AddLabel(XCol, y + line * scale, ColorText, dname + (niv > 1 ? " (Rang:" + niv.ToString() + ")" : ""));

                    BaseDon rdon = null;
                    if( BaseDon.DonBank.ContainsKey( don.ToString().ToLower() )){
                        rdon = BaseDon.DonBank[don.ToString().ToLower()];
                        if( rdon.CanUse )
                            AddLabel(XCol+120, y + line * scale,ColorTextGray, "[.don "+don.ToString()+"]" );
                    }


                    line++;
                }
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile f = sender.Mobile;
            NubiaPlayer from = f as NubiaPlayer;


            if (info.ButtonID == 100) //Nouvelle classe
            {
                from.SendGump(new GumpChoixClasse(from, false));
            }
            else if (info.ButtonID >= 50 && info.ButtonID < 75) //Baisser classe
            {
                Classe c = null;
                try { c = classes[info.ButtonID - 50]; }
                catch (Exception ex) { }
                if (c != null)
                    from.MakeClasse(c.GetType(), c.Niveau - 1);
                from.SendGump(new GumpFichePerso(mOwner, mViewer));
            }
            else if (info.ButtonID >= 75 && info.ButtonID < 100) //Augmenter classe
            {
                Classe c = null;
                try { c = classes[info.ButtonID - 75]; }
                catch (Exception ex) { }
                if (c != null)
                    from.MakeClasse(c.GetType(), c.Niveau + 1);
                from.SendGump(new GumpFichePerso(mOwner, mViewer));
            }
            else if (info.ButtonID >= 200 && info.ButtonID < 250) //Augmenter stat
            {
                DndStat stat = (DndStat)(info.ButtonID - 200);
                switch (stat)
                {
                    case DndStat.Force: from.RawStr++; break;
                    case DndStat.Dexterite: from.RawDex++; break;
                    case DndStat.Constitution: from.RawCons++; break;
                    case DndStat.Intelligence: from.RawInt++; break;
                    case DndStat.Sagesse: from.RawSag++; break;
                    case DndStat.Charisme: from.RawCha++; break;
                }
                from.SendGump(new GumpFichePerso(mOwner, mViewer));
            }
            else if (info.ButtonID >= 250 && info.ButtonID < 300) //maitrises
            {
                ArmeTemplate t = (ArmeTemplate)(info.ButtonID - 250);
                mOwner.increaseMaitrise(t);
                from.SendGump(new GumpFichePerso(mOwner, mViewer));
            }
        }

    }
}
