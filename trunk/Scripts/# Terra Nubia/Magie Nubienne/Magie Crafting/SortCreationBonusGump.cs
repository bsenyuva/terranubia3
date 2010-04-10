using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.Gumps
{
	public class SortCreationBonusGump : Gump
	{
		private NubiaPlayer m_owner;
		private SortNubia m_SortNubia;
		private int m_bonusIndex = 1;

        public SortCreationBonusGump(NubiaPlayer _owner, SortNubia _SortNubia, int bonusIndex)
            : base(50, 50)
		{
			m_owner = _owner;
			m_SortNubia = _SortNubia;
			m_bonusIndex = bonusIndex;

			int largeur = 300;
			int hauteur = 300;
			int _x = 50;
			int _y = 50;
			AddBackground( _x, _y, _x+largeur,_y+hauteur, 0x13BE );
			AddBackground( _x+10, _y+10, _x+largeur-20,_y+hauteur-15, 0x0DAC );
			AddImage( 0,0, 0x28C8 );
			AddImage( largeur+70,0, 0x28C9 );
			AddImage( (largeur/2)-10,30, 0x28D4 );
			

			int y = 45+_y;
			int line = 4;
			int scale = 22;
			int col = 35+_y;
			//18lignes max ;)
			AddLabel( col, (line*scale), 2224, "Cr�ation d'un nouveau SortNubia");
			line++;
			//AddButton( col, (line*scale), 0xFAB, 0xFAC, 998, GumpButtonType.Reply, 0 ); //Voir
			AddLabel( col, line*scale, 2116, "Bonus "+m_bonusIndex );
			line++;
			line++;

			AddLabel( col, (line*scale), 2224, "Choix d'un composant:");
			line++;
		
			/*for(int i = 0; i < m_SortNubia.Owner.Backpack.Items.Count; i++)
			{
				if(m_SortNubia.Owner.Backpack.Items[i] is BaseComposant)
				{
					BaseComposant compo = m_SortNubia.Owner.Backpack.Items[i] as BaseComposant;
					AddButton( col, (line*scale), 0xFAB, 0xFAC, 100+i, GumpButtonType.Reply, 0 ); //Voir
					AddLabel( col+40, line*scale, 0, compo.Name );
					line++;
				}
			}*/

			AddButton( col, (line*scale), 0xFAB, 0xFAC, 999, GumpButtonType.Reply, 0 ); //Voir
			AddLabel( col+40, line*scale, 0, "Annuler" );
			line++;
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile f = sender.Mobile;
            NubiaPlayer from = f as NubiaPlayer;


			from.CloseGump(typeof(SortCreationChoixSort));
			from.CloseGump(typeof(SortCreationBonusGump));
			from.SendGump(new SortCreationGump(m_owner, m_SortNubia) );
		}

	}
}