using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13F8, 0x13F9 )]
	public class GnarledStaff : BaseStaff
	{
		

		[Constructable]
		public GnarledStaff() : base( 0x13F8 )
		{
			Weight = 3.0;
		}

		public GnarledStaff( Serial serial ) : base( serial )
		{
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