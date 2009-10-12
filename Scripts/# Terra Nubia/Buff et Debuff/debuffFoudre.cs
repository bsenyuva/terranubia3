using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Nubia;
using Server.Items;

namespace Server.Nubia
{

		public class FoudreDebuff : BaseDebuff
		{
			//## CONSTRUCTEUR
			public FoudreDebuff( NubiaMobile _caster ) : base(_caster, _caster, 0x8E9, 450 /*Une heure*/,"Foudroy� !")
			{
				//Name = "Crash!";
			/*	m_diffModus = -10;
				m_hitsModus = -10;
				m_stamModus = -10;*/
				m_descrip = "Vous avez re�u la foudre!";// Vous sentez encore la puissante d�charge vous parcourir<br> <i> Malus de "+m_diffModus+" � tout les jets; Vie et stam r�duite de "+m_hitsModus+" points";
			}

			public override bool OnTurn()
			{
				return base.OnTurn();
			}

			public override void OnAdd()
			{
				base.OnAdd();
			}

			public override void End()
			{
				base.End();
			}

			public FoudreDebuff( Serial serial ) : base(serial){}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 0 ); // version
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadInt();
			}
	}
}