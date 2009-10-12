﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.Commands;

namespace Server.Mobiles
{
    public abstract class BaseDon
    {
        public static Dictionary<string, BaseDon> DonBank = new Dictionary<string, BaseDon>();
        public static void Initialize()
        {
            string space = "Server.Mobiles.Dons";
            List<string> donsClasses = NubiaHelper.getAllClasses(space);
            foreach (string clstr in donsClasses)
            {
                Type cltype = Type.GetType(space + "." + clstr);
                // Console.WriteLine("Type: " + cltype);
                if (cltype != null)
                {
                    if (cltype.IsSubclassOf(typeof(BaseDon)) && !cltype.IsAbstract )
                    {
                        try
                        {
                          BaseDon don = (BaseDon)cltype.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                          Console.WriteLine("- Don: " + don.Name + " (DonEnum." + don.DType.ToString() + ")");
                          DonBank.Add(don.DType.ToString().ToLower(), don);
                          don = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            CommandSystem.Register("don", AccessLevel.Player,
                new CommandEventHandler(donUse_OnCommand));
        }
        [Usage(".don [nom brut du don]")]
        [Description("Utilise un don particulier")]
        public static void donUse_OnCommand(CommandEventArgs e)
        {
            NubiaPlayer p = e.Mobile as NubiaPlayer;
            string[] args = e.Arguments;
            if (args.Length > 0)
            {
                string rawName = args[0];
                rawName = rawName.ToLower();
                if (DonBank.ContainsKey(rawName))
                {
                    BaseDon don = DonBank[rawName];
                    if (!p.hasDon(don.DType))
                    {
                        p.SendMessage("Vous ne possèdez pas ce don");
                        return;
                    }
                    if( don.CanUse )
                    {
                        p.SendMessage("Vous utilisez {0}", don.Name);
                        don.OnUse(p);
                    }
                    else
                        p.SendMessage("{0} n'est pas un don actif", don.Name);
                }
                else
                    p.SendMessage("Le don de nom brut '{0}' n'existe pas", rawName);
            }
            else
                p.SendMessage("Utilisation: .don [nom brut du don]");
        }
        private DonEnum mDType = DonEnum.AffiniteMagique;
        private string mName = "noname";
        private bool mCanUse = false;
        protected int mAchatMax = 1;
        protected bool mLimiteDayUse = false;

        public string Name { get { return mName; } }
        public DonEnum DType { get { return mDType; } }
        public bool CanUse { get { return mCanUse; } }
        

        public BaseDon(DonEnum type, string name, bool canUse)
        {
            mDType = type;
            mName = name;
            mCanUse = canUse;
        }
        public virtual TimeSpan getDelay(NubiaPlayer p)
        {
            if (mLimiteDayUse && p.getDonNiveau(DType) > 0 )
            {
                return TimeSpan.FromMinutes(WorldData.SpellDay().TotalMinutes / p.getDonNiveau(DType));
            }
            return WorldData.TimeTour();
        }
        public abstract void OnUse(NubiaPlayer p);

      

        public abstract bool hasConditions(NubiaPlayer mob);
    }
}
