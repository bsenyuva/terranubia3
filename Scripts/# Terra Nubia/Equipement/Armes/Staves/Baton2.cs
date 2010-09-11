using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Baton2 : BaseStaff
	{
		[Constructable]
		public Baton2() : base( 0x2BF1 )
        {
            Name = "Baton";
			Weight = 6.0;
		}

		public Baton2( Serial serial ) : base( serial )
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