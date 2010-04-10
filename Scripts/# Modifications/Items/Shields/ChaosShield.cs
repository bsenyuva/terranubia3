using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class ChaosShield : BaseShield
	{
		[Constructable]
		public ChaosShield() : base( 0x1BC3 )
		{
            BType = BouclierType.Ecu;

			if ( !Core.AOS )
				LootType = LootType.Newbied;

			Weight = 5.0;
		}

		public ChaosShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public virtual bool Validate( Mobile m )
		{
			if ( m == null || !m.Player || m.AccessLevel != AccessLevel.Player || Core.AOS )
				return true;

			Guild g = m.Guild as Guild;

			if ( g == null || g.Type != GuildType.Chaos )
			{
				m.FixedEffect( 0x3728, 10, 13 );
				Delete();

				return false;
			}

			return true;
		}
	}
}