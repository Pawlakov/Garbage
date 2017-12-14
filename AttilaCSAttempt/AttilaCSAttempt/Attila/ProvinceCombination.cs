using System;
namespace TWAssistant
{
	namespace Attila
	{
		class ProvinceCombination
		{
			readonly ProvinceData province;
			readonly Faction faction;
			readonly BuildingSlot[][] slots;
			bool isCurrent;
			readonly uint originalFertility;
			//
			uint fertility;
			int food;
			int order;
			readonly int[] sanitations;
			int religiousInfluence;
			float wealth;
			//
			public ProvinceCombination(ProvinceData iniProvince, Faction iniFaction, uint iniFertility, bool useResource)
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
						slots[whichRegion][whichSlot] = new BuildingSlot(province[whichRegion], faction.Buildings, whichSlot, useResource);
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
				slots = new BuildingSlot[3][];
				slots[0] = new BuildingSlot[6];
				slots[1] = new BuildingSlot[4];
				slots[2] = new BuildingSlot[4];
				//
				for (uint whichRegion = 0; whichRegion < source.slots.Length; ++whichRegion)
					for (uint whichSlot = 0; whichSlot < source.slots[whichRegion].Length; ++whichSlot)
						slots[whichRegion][whichSlot] = new BuildingSlot(source.slots[whichRegion][whichSlot]);
				//
				sanitations = new int[3];
				//
				isCurrent = false;
			}
			//
			public void Fill(Random random)
			{
				for (uint whichRegion = 0; whichRegion < 3; ++whichRegion)
				{
					BuildingLibrary buildings = new BuildingLibrary(faction.Buildings);
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
						if (slots[whichRegion][whichSlot].Building != null)
							buildings.Remove(slots[whichRegion][whichSlot].Building);
					for (uint whichBuilding = 0; whichBuilding < slots[whichRegion].Length; ++whichBuilding)
						slots[whichRegion][whichBuilding].Fill(random, buildings, province[whichRegion]);
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
				Console.WriteLine("W:{0} F:{1} O:{2} S:{3}/{4}/{5} R:{6} I:{7} ", Wealth, Food, Order, getSanitation(0), getSanitation(1), getSanitation(2), ReligiousInfluence, Fertility);
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
			public void ForceBuilding()
			{
				uint whichRegion;
				uint whichSlot;
				int choice;
				Console.Write("Region: ");
				whichRegion = Convert.ToUInt32(Console.ReadLine());
				Console.Write("Slot: ");
				whichSlot = Convert.ToUInt32(Console.ReadLine());
				Console.Write("0-Level / 1-Building: ");
				choice = Convert.ToInt32(Console.ReadLine());
				if (choice == 0)
				{
					Console.Write("Level: ");
					choice = Convert.ToInt32(Console.ReadLine());
					slots[whichRegion][whichSlot].Level = (uint)choice;
				}
				else
				{
					faction.Buildings.ShowListOneType(slots[whichRegion][whichSlot].Type, province[whichRegion].Resource);
					Console.Write("Building: ");
					choice = Convert.ToInt32(Console.ReadLine());
					slots[whichRegion][whichSlot].Building = faction.Buildings.GetExactBuilding(slots[whichRegion][whichSlot].Type, province[whichRegion].Resource, choice);
				}
				isCurrent = false;
			}
			//
			public BuildingSlot this[uint whichRegion, uint whichSlot]
			{
				get { return slots[whichRegion][whichSlot]; }
			}
			public int Food
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return food;
				}
			}
			public int Order
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return order;
				}
			}
			public int getSanitation(uint whichRegion)
			{
				if (!isCurrent)
					HarvestBuildings();
				return sanitations[whichRegion];
			}
			public int ReligiousInfluence
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return religiousInfluence;
				}
			}
			public uint Fertility
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return fertility;
				}
			}
			public float Wealth
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return wealth;
				}
			}
			//
			void HarvestBuildings()
			{
				food = faction.Food;
				order = faction.Order;
				sanitations[0] = faction.Sanitation;
				sanitations[1] = faction.Sanitation;
				sanitations[2] = faction.Sanitation;
				religiousInfluence = faction.ReligiousInfluence;
				fertility = faction.Fertility + originalFertility;
				for (uint whichRegion = 0; whichRegion < 3; ++whichRegion)
				{
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						fertility += slots[whichRegion][whichSlot].Fertility;
					}
				}
				if (fertility > 6)
					fertility = 6;
				ProvinceWealth wealthCalculator = new ProvinceWealth(fertility);
				if (faction.WealthBonuses != null)
					for (uint whichBonus = 0; whichBonus < faction.WealthBonuses.Length; ++whichBonus)
						wealthCalculator.AddBonus(faction.WealthBonuses[whichBonus]);
				//
				for (uint whichRegion = 0; whichRegion < 3; ++whichRegion)
				{
					for (uint whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						BuildingSlot slot = slots[whichRegion][whichSlot];
						food += slot.getFood(fertility);
						order += slot.Order;
						sanitations[0] += slot.ProvincionalSanitation;
						sanitations[1] += slot.ProvincionalSanitation;
						sanitations[2] += slot.ProvincionalSanitation;
						sanitations[whichRegion] += slot.RegionalSanitation;
						religiousInfluence += slot.ReligiousInfluence;
						for (uint whichBonus = 0; whichBonus < slots[whichRegion][whichSlot].WealthBonusCount; ++whichBonus)
							wealthCalculator.AddBonus(slots[whichRegion][whichSlot].WealthBonuses[whichBonus]);
					}
				}
				wealth = wealthCalculator.Wealth;
				isCurrent = true;
			}
		}
	}
}