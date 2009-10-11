using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Targeting;
using Server.Engines.Harvest;
using Server.Misc;
using Server.Engines.Craft;
namespace Server.Engines
{
    public class CraftSystemNubia
    {
        protected CraftList mList = new ListForge();
        protected CompType mComp = CompType.Forge;
        protected string mName = "Craft System";
        protected static CraftSystemNubia mSingleton = null;

        


        public CraftList List
        {
            get
            {
                return mList;
            }
        }


        public CompType Comp { get { return mComp; } }
        public string Name { get { return mName; } }
        public CraftSystemNubia()
        {
        }
        public void TryCraft(NubiaPlayer crafter, BaseToolNubia tool, CraftEntry entry)
        {
            if (entry == null || crafter == null || tool == null)
            {
                Console.WriteLine("WARNING: Null dans TryCraft ou index invalid");
                return;
            }
            Item newitem = null;

            if (entry.MinValue > crafter.Competences[mComp].getPureMaitrise() )
            {
                crafter.SendMessage("Vous n'�tes pas encore assez dou� pour �a...");
                return;
            }
           

            if (crafter.Backpack == null)
            {
                crafter.SendMessage("vous n'avez pas de sac � dos");
                return;
            }

            

            bool allressource = false;
            ArrayList ressourceslist = new ArrayList();
            for (int r = 0; r < entry.Ressource.Length; r++)
            {
                Type type = entry.Ressource[r].RType;
                int nbr = entry.Ressource[r].Number;
                int nbrTrouve = 0;
                int nbrConsume = 0;

                bool badselect = false;
                if (type == typeof(BaseMetal) && nbr > 0)
                {
                    if (tool.Metal != null)
                        type = tool.Metal.GetType();
                    else
                    {
                        crafter.SendMessage(2118, "Vous devez selectionner le M�tal");
                        badselect = true;
                    }
                }
                else if (type == typeof(BaseCuir) && nbr > 0)
                {
                    if (tool.Cuir != null)
                        type = tool.Cuir.GetType();
                    else
                    {
                        crafter.SendMessage(2118, "Vous devez selectionner le Cuir");
                        badselect = true;
                    }
                }
                else if (type == typeof(BaseBois) && nbr > 0)
                {
                    if (tool.Bois != null)
                        type = tool.Bois.GetType();
                    else
                    {
                        crafter.SendMessage(2118, "Vous devez selectionner le Bois");
                        badselect = true;
                    }
                }
                
                if (badselect)
                    return;

                
                BaseRessource itAdd = null;
                foreach (Item ritem in crafter.Backpack.Items)
                {
                    //Console.WriteLine("Ritem="+ritem+" :: type="+type);
                    if (ritem.GetType().IsSubclassOf(type) || ritem.GetType() == type)
                    {
                        ConstructorInfo rtor = ritem.GetType().GetConstructor(Type.EmptyTypes);
                        Object robj = rtor.Invoke(null);
                        //itAdd = robj as Item;
                        //Console.WriteLine("Same Type");
                        if (ritem is BaseRessource)
                        {
                            Console.WriteLine(((BaseRessource)ritem).isRaffine);
                            if (!((BaseRessource)ritem).isRaffine)
                            {
                                crafter.SendMessage(2118, "Vous devez raffiner votre {0} avant tout", ritem.Name);
                                break;
                            }
                            else
                                itAdd = ritem as BaseRessource;
                        }
                        if (ritem.Amount == 1)
                        {
                            //ritem.Delete();
                            nbrTrouve++;
                        }
                        else if (ritem.Amount < nbr - nbrTrouve)
                        {
                            nbrTrouve += ritem.Amount;
                            //ritem.Delete();
                        }
                        else
                        {
                            nbrTrouve = nbr;
                            //ritem.Amount -= nbr-nbrTrouve;
                        }
                    }
                    if (nbr - nbrTrouve <= 0)
                        break;
                }
                if (itAdd != null)
                    ressourceslist.Add(itAdd);
                if (nbrTrouve < nbr)
                {
                    allressource = false;
                    crafter.SendMessage("Il vous manque des mat�riaux...");
                }
                else
                {
                    allressource = true;
                    /*Type[] types = new Type[ressourceslist.ToArray().Length];
                    for(int t = 0; t < types.Length; t++)
                        types[t] = ((Item)ressourceslist[t]).GetType();
                    int[] quatitys = new int[entry.Ressource.Length];
                    for(int q = 0; q < quatitys.Length; q++)
                        quatitys[q] = entry.Ressource[q].Number;*/


                }
                if (!allressource)
                    return;
            }
            

            if ( crafter.Competences[mComp].check(entry.Diff) )
            {
                
                newitem = null;

                try
                {
                    ConstructorInfo constructor = entry.ToCraft.GetConstructor(Type.EmptyTypes);
                    Object obj = constructor.Invoke(null);
                    newitem = obj as Item;
                }
                catch (Exception ex) { Console.WriteLine("TryCraft: " + ex.Message); }

                if (newitem == null)
                    return;
                crafter.SendMessage("Vous reussissez votre cr�ation!!");
                if (newitem is INubiaCraftable)
                {
                    INubiaCraftable nubitem = newitem as INubiaCraftable;

                    NubiaQualityEnum quality = NubiaQualityEnum.Normale;

                    int delta = crafter.Competences[mComp].pureRoll() - entry.Diff;
                    if (delta < 0)
                        quality = NubiaQualityEnum.Mauvaise;
                    else if (delta >= 15)
                        quality = NubiaQualityEnum.Maitre;
                    else if (delta >= 10)
                        quality = NubiaQualityEnum.Excellente;
                    else if (delta >= 5)
                        quality = NubiaQualityEnum.Bonne;

                    nubitem.Artisan = crafter;
                    nubitem.TRessourceList.Clear();
                    bool colored = false;
                    for (int i = 0; i < ressourceslist.Count; i++)
                    {
                        Console.WriteLine("Ressource: " + ressourceslist[i].ToString());
                        if (ressourceslist[i] is BaseRessource)
                        {
                            BaseRessource res = ressourceslist[i] as BaseRessource;
                            if (!colored && res.Infos != null)
                            {
                                newitem.Hue = res.Infos.Hue;
                                colored = true;
                            }
                            Console.WriteLine("Ressource: " + res.Ressource.ToString());
                           // nubitem.AddRessource((NubiaRessource)res.Ressource);                   
                            nubitem.TRessourceList.Add(res.Ressource);
                        }
                    }
                    nubitem.AfterCraft(quality);
                 //   nubitem.ComputeRessourceBonus();
                }
                if (newitem != null && crafter.Backpack != null)
                    crafter.Backpack.AddItem(newitem);
                else if (newitem != null)
                    newitem.MoveToWorld(crafter.Location, crafter.Map);
            }
            else
                crafter.SendMessage("Vous n'arrivez a rien, de la mati�re premi�re � �t� perdue");

            Console.WriteLine("Consume Quantity:  ");
            for (int r2 = 0; r2 < entry.Ressource.Length; r2++)
            {
                Type type2 = entry.Ressource[r2].RType;
                int nbr2 = entry.Ressource[r2].Number;
                CraftSystemNubia.ConsumeQuantity(crafter.Backpack, type2, nbr2, tool);
            }
            Console.Write("Ok !");
        }
        public static void ConsumeQuantity(Container cont, Type types, int amounts, BaseToolNubia tool)
        {
            Type type;
            if (types == typeof(BaseMetal))
                types = tool.Metal.GetType();
            else if (types == typeof(BaseCuir))
                types = tool.Cuir.GetType();
            else if (types == typeof(BaseBois))
                types = tool.Bois.GetType();

            Item itAdd = null;

            ArrayList targets = new ArrayList();

            foreach (Item ritem in cont.Items)
            {
                //Console.WriteLine("Ritem="+ritem+" :: type="+type);
                if (ritem.GetType().IsSubclassOf(types) || ritem.GetType() == types)
                {
                    targets.Add(ritem);
                }
            }

            for (int i = 0; i < targets.Count; ++i)
            {
                Item ritem = (Item)targets[i];
                ConstructorInfo rtor = ritem.GetType().GetConstructor(Type.EmptyTypes);
                Object robj = rtor.Invoke(null);
                itAdd = robj as Item;
                //Console.WriteLine("Same Type");
                if (ritem is BaseRessource)
                {
                    //	Console.WriteLine("BaseRessource tourv�e");
                    //	Console.WriteLine(((BaseRessource)ritem).isRaffine);
                    if (!((BaseRessource)ritem).isRaffine)
                        break;
                }
                if ((ritem.Amount > amounts) && amounts > 0)
                {
                    ritem.Amount -= amounts;
                    amounts = 0;
                }
                else if ((ritem.Amount == amounts) && amounts > 0)
                {
                    ritem.Delete();
                    amounts = 0;
                }
                else if ((ritem.Amount < amounts) && amounts > 0)
                {
                    amounts -= ritem.Amount;
                    ritem.Delete();
                }
            }


            /*if ( types.Length != amounts.Length )
                throw new ArgumentException();*/

            /*	for ( int i = 0; i < types.Length; ++i )
                {
				
                    int deleted= 0;
                    foreach(Item itemb in cont.Items)
                    {
                        if(itemb.GetType() == types[i] )
                        {
                            if(itemb.Amount <= amounts[i]-deleted){
                                deleted = itemb.Amount;
                                //itemb.Consume();
                            }
                            else{
                                itemb.Amount -= amounts[i]-deleted;
                                deleted = amounts[i];
                            }
                        }
                        if(deleted >= amounts[i])
                            break;
                    }
                }*/
        }

    }
    public class CraftForgeSystem : CraftSystemNubia
    {

        public CraftForgeSystem()
        {
            mList = new ListForge();
            mComp = CompType.Forge;
            mName = "Forge d'armure";
        }
        public static CraftSystemNubia Singleton
        {
            get
            {
                if( mSingleton == null )
                    mSingleton = new CraftForgeSystem();
                return mSingleton;
             }
         }
    }

    public class CraftForgeArmeSystem : CraftSystemNubia
    {
        public CraftForgeArmeSystem()
        {
            mList = new ListForge();
            mComp = CompType.Forge;
            mName = "Forge d'arme";
        }
        public static CraftSystemNubia Singleton
        {
            get
            {
                if (mSingleton == null)
                    mSingleton = new CraftForgeArmeSystem();
                return mSingleton;
            }
        }
    }

    public class CraftEruditionSystem : CraftSystemNubia
    {
        public CraftEruditionSystem()
        {
            mList = new ListErudition();
            mComp = CompType.Erudition;
            mName = "Ecriture";
        }
        public static CraftSystemNubia Singleton
        {
            get
            {
                if (mSingleton == null)
                    mSingleton = new CraftEruditionSystem();
                return mSingleton;
            }
        }
    }

}
