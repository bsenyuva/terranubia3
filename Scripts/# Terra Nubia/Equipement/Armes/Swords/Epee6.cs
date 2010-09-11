using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Epee6 : BaseSword
	{
		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		[Constructable]
		public Epee6() : base( 0x2BF0 )
        {
            Name = "Ep�e";
			Weight = 7.0;
		}

		public Epee6( Serial serial ) : base( serial )
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