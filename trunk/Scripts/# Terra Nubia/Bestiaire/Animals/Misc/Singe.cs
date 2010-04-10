using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("un corps de singe")]
    public class Singe : BaseBestiole
    {
        [Constructable]
        public Singe()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Singe";
            Body = 0x1D;
            BaseSoundID = 0x9E;
            Hue = Utility.RandomAnimalHue();

            this.VirtualArmor = 1;

            CreatureType = MobileType.Animal;

            mMonsterHits = DndHelper.rollDe(De.huit, 1) + 0;
            mMonsterCA = 10;
            Server.Items.Fists griffes = new Server.Items.Fists();
            griffes.De = De.trois;
            griffes.NbrLance = 1;
            griffes.BonusDegatStatic = 3;
            griffes.Movable = false;
            EquipItem(griffes);
            mMonsterAttaques = new int[] { 4 };

            mMonsterReflexe = 4;
            mMonsterVigueur = 2;
            mMonsterVolonte = 1;
            RawStr = 3;
            RawDex = 15;
            RawCons = 10;
            RawInt = 2;
            RawSag = 12;
            RawCha = 5;
            mMonsterNiveau = 2;
            AddCompetence(CompType.Detection, 3);
            AddCompetence(CompType.PerceptionAuditive, 3);
            AddCompetence(CompType.Discretion, 10);
            AddCompetence(CompType.Equilibre, 10);
            AddCompetence(CompType.Escalade, 10);
            Tamable = true;
            ControlSlots = 1;
        }

        public override int Meat { get { return 1; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Canine; } }

        public Singe(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}