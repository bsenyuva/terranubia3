using System;
using Server;
using Server.Nubia;
using Server.Targeting;

namespace Server.Items
{
	public class PlanteCoton : BasePlante
	{
		public override EnumPlanteTerrain Terrain { get{return EnumPlanteTerrain.Ferme; }}
		public override bool CanEat { get{return false; }} //Savoir si on peu directement manger la r�colte
		public override int DD { get{return 70; }}

		[Constructable]
		public PlanteCoton() : base( EnumPlante.Coton )
		{
			Hue = 2120;
		}
		[Constructable]
		public PlanteCoton( Serial serial ) : base( serial )
		{
			Hue = 2120;
		}

		

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
