using System;
using Server.Items;

namespace Server.Items
{
	//[FlipableAttribute( 13803 )]
	public class OrneGorgerin : NubiaArmor
	{
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public OrneGorgerin() : base( 13817 )
		{
			Weight = 6.0;
			//m_capaNeed = 10;
			Name = "Gorgerin d'Orne";
            ModelType = ArmorModelType.Plaque;
		}

		public OrneGorgerin( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			if ( Weight == 1.0 )
				Weight = 6.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}