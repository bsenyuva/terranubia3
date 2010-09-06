using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	[TypeAlias( "Server.Mobiles.Bear" )]
	public class BlackBear : NubiaCreature
	{
		[Constructable]
        public BlackBear()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.1, 0.4)
		{
			Name = "Ours noir";
			Body = 211;
            BaseSoundID = 0xA3;

            this.VirtualArmor = 1;

            CreatureType = MobileType.Animal;

            Faction = FactionEnum.Nature;

            NiveauCreature = 5;

            AddCompetence(CompType.Detection, 4);
            AddCompetence(CompType.PerceptionAuditive, 4);
            AddCompetence(CompType.Escalade, 4);

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 35.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public BlackBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}