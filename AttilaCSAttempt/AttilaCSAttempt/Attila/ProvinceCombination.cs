using System;
namespace TWAssistant
{
	namespace Attila
	{
		class ProvinceCombination
		{
			private ProvinceData province;
			private Faction faction;
			private BuildingSlot[][] slots;
			private bool isCurrent;
			private uint originalFertility;
			//
			private uint fertility;
			private int food;
			private int order;
			private int[] sanitations;
			private int religiousInfluence;
			private float wealth;
			//
			public ProvinceCombination(ProvinceData iniProvince, Faction iniFaction, uint iniFertility)
			{
				province = iniProvince;
				//
				faction = iniFaction;
				//
				originalFertility = iniFertility;
				//
				slots = new BuildingSlot[3][];
				for (uint whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					slots[whichRegion] = new BuildingSlot[province[whichRegion].SlotsCount];
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						slots[whichRegion][whichSlot] = new BuildingSlot(province[whichRegion], faction.Buildings, whichSlot);
					}
				}
				//
				sanitations = new int[3];
				//
				isCurrent = false;
			}
			public ProvinceCombination(ProvinceCombination source)
			{
				province = source.province;
				//
				faction = source.faction;
				//
				originalFertility = source.originalFertility;
				//
				slots = new BuildingSlot[source.slots.Length][];
				//
				for (uint whichRegion = 0; whichRegion < source.slots.Length; ++whichRegion)
				{
					slots[whichRegion] = new BuildingSlot[source.slots[whichRegion].Length];
					for (uint whichSlot = 0; whichSlot < source.slots[whichRegion].Length; ++whichSlot)
						slots[whichRegion][whichSlot] = new BuildingSlot(source.slots[whichRegion][whichSlot]);
				}
				//
				sanitations = new int[3];
				//
				isCurrent = false;
			}
			//
			public void Fill(Random random)
			{
				for (uint whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					BuildingLibrary buildings = new BuildingLibrary(faction.Buildings);
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						if (slots[whichRegion][whichSlot].Building != null)
						{
							buildings.Remove(slots[whichRegion][whichSlot].Building);
						}
					}
					for (uint whichBuilding = 0; whichBuilding < slots[whichRegion].Length; ++whichBuilding)
					{
						slots[whichRegion][whichBuilding].Fill(random, buildings, province[whichRegion]);
					}
				}
				isCurrent = false;
			}
			public void ShowContent()
			{
				Console.WriteLine("{0}", province.Name);
				for (uint whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					Console.WriteLine("	{0}: {1}", whichRegion, province[whichRegion].Name);
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						Console.Write("		{0}: ", whichSlot);
						slots[whichRegion][whichSlot].ShowContent();
					}
				}
				Console.WriteLine("W: {0} F: {1} O: {2} S: {3}/{4}/{5} I:{6} ", Wealth, Food, Order, getSanitation(0), getSanitation(1), getSanitation(2), Fertility);
			}
			public void RewardUsefulBuildings()
			{
				for (uint whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						slots[whichRegion][whichSlot].Reward();
					}
				}
			}
			//public void ForceConstraint()
			//{
			//	byte whichRegion;
			//	byte whichSlot;
			//	byte choice;
			//	Console.WriteLine("Which region?");
			//	whichRegion = Convert.ToByte(Console.ReadLine());
			//	Console.WriteLine("Which slot?");
			//	whichSlot = Convert.ToByte(Console.ReadLine());
			//	Console.WriteLine("which coinstraint type?");
			//	Console.WriteLine("0. Level.");
			//	Console.WriteLine("1. Building.");
			//	choice = Convert.ToByte(Console.ReadLine());
			//	if (choice == 0)
			//	{
			//		Console.WriteLine("Which level?");
			//		choice = Convert.ToByte(Console.ReadLine());
			//		slots[whichRegion][whichSlot].Level = choice;
			//	}
			//	else
			//	{
			//		faction.Buildings.ShowListOneType(slots[whichRegion][whichSlot].Type);
			//		Console.WriteLine("Which building?");
			//		choice = Convert.ToByte(Console.ReadLine());
			//		slots[whichRegion][whichSlot].BuildingBranch = faction.Buildings[slots[whichRegion][whichSlot].Type, choice];
			//	}
			//	isCurrent = false;
			//}
			//
			public BuildingSlot this[uint whichRegion, uint whichSlot]
			{
				get { return this.slots[whichRegion][whichSlot]; }
			}
			public int Food
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.food;
				}
			}
			public int Order
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.order;
				}
			}
			public int getSanitation(uint whichRegion)
			{
				if (!this.isCurrent)
					HarvestBuildings();
				return this.sanitations[whichRegion];
			}
			public int ReligiousInfluence
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.religiousInfluence;
				}
			}
			public uint Fertility
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.fertility;
				}
			}
			public float Wealth
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.wealth;
				}
			}
			//
			private void HarvestBuildings()
			{
				this.food = faction.Food;
				this.order = faction.Order;
				this.sanitations[0] = faction.Sanitation;
				this.sanitations[1] = faction.Sanitation;
				this.sanitations[2] = faction.Sanitation;
				this.religiousInfluence = faction.ReligiousInfluence;
				this.fertility = faction.Fertility + this.originalFertility;
				for (uint whichRegion = 0; whichRegion < this.slots.Length; ++whichRegion)
				{
					for (uint whichSlot = 0; whichSlot < this.slots[whichRegion].Length; ++whichSlot)
					{
						this.fertility += this.slots[whichRegion][whichSlot].Fertility;
					}
				}
				if (fertility > 5)
					fertility = 5;
				ProvinceWealth wealthCalculator = new ProvinceWealth(fertility);
				if (this.faction.WealthBonuses != null)
					for (uint whichBonus = 0; whichBonus < this.faction.WealthBonuses.Length; ++whichBonus)
						wealthCalculator.AddBonus(this.faction.WealthBonuses[whichBonus]);
				for (uint whichRegion = 0; whichRegion < this.slots.Length; ++whichRegion)
				{
					for (uint whichSlot = 0; whichSlot < this.slots[whichRegion].Length; ++whichSlot)
					{
						BuildingSlot slot = this.slots[whichRegion][whichSlot];
						this.food += slot.getFood(fertility);
						this.order += slot.Order;
						this.sanitations[0] += slot.ProvincionalSanitation;
						this.sanitations[1] += slot.ProvincionalSanitation;
						this.sanitations[2] += slot.ProvincionalSanitation;
						this.sanitations[whichRegion] += slot.RegionalSanitation;
						this.religiousInfluence += slot.ReligiousInfluence;
						for (uint whichBonus = 0; whichBonus < this.slots[whichRegion][whichSlot].WealthBonusCount; ++whichBonus)
							wealthCalculator.AddBonus(this.slots[whichRegion][whichSlot].WealthBonuses[whichBonus]);
					}
				}
				this.wealth = wealthCalculator.Wealth;
				this.isCurrent = true;
			}
		}
	}
}