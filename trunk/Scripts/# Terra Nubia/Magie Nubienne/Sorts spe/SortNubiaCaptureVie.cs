using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells
{
	public class SortNubiaCaptureVie : SortNubia
	{
		public override int GetCercle()
		{
				return (int)1;
		}

        public override SortDomaine Domaine { get { return SortDomaine.All; } }
		public override bool mustConsume{ get{return true;}} //Pour consommer un Mobile/Item de condition;
		public override bool playEffect{ get	{	return true;	}} //Calibrer les effets dans la cr�ation
		public override bool canBePere{ get{return false;}}

		public override bool isUnique{ get{return true;}}

		public override SortEnergie[] allowCompetence 
		{ 
			get
			{
                return new SortEnergie[] 
				{
					SortEnergie.Mental
				};
			}
		}

		//DESTRUCTION
		private int m_minDegat = 5;
		private int m_maxDegat = 10;
		private int m_number = 1;
		private bool m_canCible = true;
	
		[Constructable]
		public SortNubiaCaptureVie() : base ("Capture de vie")
		{
			distance = 10;
			Delay = 30.0;
			TimeToCast = 4.0;
			//forceCondition(SortNubiaCondition.Objet);
		}
		[Constructable]
		public SortNubiaCaptureVie( Serial serial ) : base (serial)
		{
		}

		public override void StartCast()
		{
			base.StartCast();
		}

		public override bool canCast(NubiaPlayer from)
		{
			return base.canCast(from);
		}
		public override void EndSortNubia()
		{
			base.EndSortNubia();
		}
		public override bool Cast()
		{
			if(!base.Cast())
				return false;
			
			if(m_canCible)
				Owner.Target = new InternalTarget( this );
			else
				FinishSequence(Owner);
			return true;
		}
		public void FinishSequence(Mobile cible)
		{
			Owner.Animate( 17, 7, 1, true, false, 0 );

			//SortNubiaHelper.MakeEffect( Owner, cible, this, true, mustExplose );
            if (!(cible is NubiaCreature))
				return;

            NubiaCreature creat = cible as NubiaCreature;

			if(creat.Hits < Math.Max( 8, (creat.HitsMax/5) ) )
			{
				SortNubiaHelper.makeBigSmoke(creat.Location, creat.Map);
				RuneVie rune = new RuneVie();
				rune.creature = NubiaHelper.CopyCreature(creat);
				rune.MoveToWorld(creat.Location, creat.Map);
				creat.Delete();
			}
			else
				Owner.SendMessage("La cr�ature n'est pas assez affaibli");
			
			EndSortNubia(); //important ;)
		}

		private class InternalTarget : Target
		{
			private SortNubiaCaptureVie m_Owner;

			public InternalTarget( SortNubiaCaptureVie owner ) : base( owner.distance, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.FinishSequence( (Mobile)o );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);
			writer.Write( (int) 0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
		
	}

}