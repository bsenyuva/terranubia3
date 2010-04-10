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
	public class SortCreationChoixRender : Gump
	{
		private NubiaPlayer m_owner;
		private SortNubia m_SortNubia;

        public SortCreationChoixRender(NubiaPlayer _owner, SortNubia _SortNubia)
            : base(50, 50)
		{
			m_owner = _owner;
			m_SortNubia = _SortNubia;

			int largeur = 350;
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
			//AddButton( col, (line*scale), 0xFAB, 0xFAC, 998, GumpButtonType.Reply, 0 );
			AddLabel( col, line*scale, 2116, "Actuelle: "+m_SortNubia.render.ToString() );
			line++;
			line++;

			AddLabel( col, (line*scale), 2224, "Choix d'un rendu:");
			line++;
			
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 100, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Normal" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 101, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Assombri" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 102, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Eclairci" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 103, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Transparence (Sombre converti en transparence)" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 104, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Transparence (Haute)" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 105, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Transparence (Sur la couleur primaire)" );
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 106, GumpButtonType.Reply, 0 ); 
			AddLabel( col+40, line*scale, 0, "Negatif" );
			line++;
			
			line++;
			AddButton( col, (line*scale), 0xFAB, 0xFAC, 999, GumpButtonType.Reply, 0 );
			AddLabel( col+40, line*scale, 0, "Annuler" );

			
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile f = sender.Mobile;
            NubiaPlayer from = f as NubiaPlayer;

			if(info.ButtonID >= 100 && info.ButtonID < 200)
			{
				m_SortNubia.render = (MagieRender)(info.ButtonID-100);
			}

			from.CloseGump(typeof(SortCreationChoixRender));
			from.SendGump(new SortCreationGump(m_owner, m_SortNubia) );
		}

	}
}