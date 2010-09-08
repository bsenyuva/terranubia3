using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Arc5 : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public Arc5() : base( ArmeTemplate.Arc, 0x2C82 )
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
		}

		public Arc5( Serial serial ) : base( serial )
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

			if ( Weight == 7.0 )
				Weight = 6.0;
		}
	}
}