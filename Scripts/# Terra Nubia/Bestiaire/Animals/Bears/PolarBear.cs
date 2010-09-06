using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a polar bear corpse" )]
	[TypeAlias( "Server.Mobiles.Polarbear" )]
	public class PolarBear : NubiaCreature
	{
		[Constructable]
        public PolarBear()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.1, 0.4)
		{
			Name = "a polar bear";
			Body = 213;
			BaseSoundID = 0xA3;

            CreatureType = MobileType.Animal;
            this.VirtualArmor = 3;
            Faction = FactionEnum.Nature;

            NiveauCreature = 7;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public PolarBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}