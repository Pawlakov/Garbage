using System;
namespace TWAssistant
{
	namespace Attila
	{
		public class ProvinceCombination
		{
			readonly ProvinceData province;
			readonly Faction faction;
			readonly int originalFertility;
			readonly Religion religion;
			readonly int maxFertility;
			readonly int incomingOsmosis;
			//
			readonly BuildingSlot[][] slots;
			//
			int fertility;
			int food;
			int order;
			int influence;
			int counterinfluence;
			int osmosis;
			readonly int[] sanitations;
			float wealth;
			bool isCurrent;
			//
			public ProvinceCombination(ProvinceData iniProvince, Faction iniFaction, int iniFertility, int iniOsmosis, int whichMod, Religion iniReligion)
			{
				province = iniProvince;
				faction = iniFaction;
				originalFertility = iniFertility;
				incomingOsmosis = iniOsmosis;
				religion = iniReligion;
				if (whichMod == 2)
					maxFertility = 6;
				else
					maxFertility = 5;
				//
				slots = new BuildingSlot[province.RegionCount][];
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					slots[whichRegion] = new BuildingSlot[province[whichRegion].SlotsCount];
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
						slots[whichRegion][whichSlot] = new BuildingSlot(province[whichRegion], whichSlot);
				}
				//
				sanitations = new int[slots.Length];
				isCurrent = false;
			}
			public ProvinceCombination(ProvinceCombination source)
			{
				province = source.province;
				faction = source.faction;
				originalFertility = source.originalFertility;
				religion = source.religion;
				maxFertility = source.maxFertility;
				//
				slots = new BuildingSlot[source.slots.Length][];
				for (int whichRegion = 0; whichRegion < source.slots.Length; ++whichRegion)
				{
					slots[whichRegion] = new BuildingSlot[source.slots[whichRegion].Length];
					for (int whichSlot = 0; whichSlot < source.slots[whichRegion].Length; ++whichSlot)
						slots[whichRegion][whichSlot] = new BuildingSlot(source.slots[whichRegion][whichSlot]);
				}
				//
				sanitations = new int[slots.Length];
				isCurrent = false;
			}
			//
			public void Fill(Random random)
			{
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					BuildingLibrary buildings = new BuildingLibrary(faction.Buildings);
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
						if (slots[whichRegion][whichSlot].Building != null)
							buildings.Remove(slots[whichRegion][whichSlot].BuildingBranch);
					for (int whichBuilding = 0; whichBuilding < slots[whichRegion].Length; ++whichBuilding)
						slots[whichRegion][whichBuilding].Fill(random, buildings, province[whichRegion]);
				}
				isCurrent = false;
			}
			public void ShowContent()
			{
				Console.WriteLine("{0}", province.Name);
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					Console.WriteLine("{0}", province[whichRegion].Name);
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						Console.Write("{0}. ", whichSlot);
						slots[whichRegion][whichSlot].ShowContent();
					}
				}
				Console.WriteLine("Wea:{0} Foo:{1} Ord:{2} San:{3}/{4}/{5} Rel:{6}/{7} Osm:{8} Fer:{9} ", Wealth, Food, Order, getSanitation(0), getSanitation(1), getSanitation(2), Influence, Counterinfluence, ReligiousOsmosis, Fertility);
			}
			public void RewardBuildings()
			{
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
						slots[whichRegion][whichSlot].Reward();
			}
			/*
			public void ForceBuilding()
			{
				int whichRegion;
				int whichSlot;
				int choice;
				Console.Write("Region: ");
				whichRegion = Convert.ToInt32(Console.ReadLine());
				Console.Write("Slot: ");
				whichSlot = Convert.ToInt32(Console.ReadLine());
				Console.Write("0-Level / 1-Building: ");
				choice = Convert.ToInt32(Console.ReadLine());
				if (choice == 0)
				{
					Console.Write("Level: ");
					choice = Convert.ToInt32(Console.ReadLine());
					slots[whichRegion][whichSlot].Level = choice;
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
			*/
			//
			public BuildingSlot this[int whichRegion, int whichSlot]
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
			public int ReligiousOsmosis
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return osmosis;
				}
			}
			public int Influence
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return influence;
				}
			}
			public int Counterinfluence
			{
				get
				{
					if (!isCurrent)
						HarvestBuildings();
					return counterinfluence;
				}
			}
			public int Fertility
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
				fertility = faction.Fertility + originalFertility;
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						fertility += slots[whichRegion][whichSlot].BuildingLevel.Irigation;
					}
				}
				if (fertility > maxFertility)
					fertility = maxFertility;
				if (fertility < 0)
					fertility = 0;
				//
				food = 0;
				order = 0;
				for (int whichRegion = 0; whichRegion < sanitations.Length; ++whichRegion)
					sanitations[whichRegion] = faction.Sanitation;
				osmosis = 0;
				//
				ProvinceWealth wealthCalculator = new ProvinceWealth(fertility);
				if (faction.WealthBonuses != null)
					for (int whichBonus = 0; whichBonus < faction.WealthBonuses.Length; ++whichBonus)
						wealthCalculator.AddBonus(faction.WealthBonuses[whichBonus]);
				ProvinceReligion religionCalculator = new ProvinceReligion(province.Traditions, religion);
				religionCalculator.AddInfluence(incomingOsmosis, religion);
				//
				for (int whichRegion = 0; whichRegion < slots.Length; ++whichRegion)
				{
					for (int whichSlot = 0; whichSlot < slots[whichRegion].Length; ++whichSlot)
					{
						BuildingLevel level = slots[whichRegion][whichSlot].BuildingLevel;
						food += level.GetFood(fertility);
						order += level.Order;
						for (int whichSanitation = 0; whichSanitation < sanitations.Length; ++whichSanitation)
							sanitations[whichSanitation] += level.ProvincionalSanitation;
						sanitations[whichRegion] += level.RegionalSanitation;
						osmosis += level.ReligiousOsmosis;
						//
						Religion? potentialReligion = slots[whichRegion][whichSlot].BuildingBranch.Religion;
						if (potentialReligion.HasValue)
							religionCalculator.AddInfluence(level.ReligiousInfluence, potentialReligion.Value);
						for (int whichBonus = 0; whichBonus < level.WealthBonuses.Length; ++whichBonus)
							wealthCalculator.AddBonus(level.WealthBonuses[whichBonus]);
					}
				}
				order += religionCalculator.Order;
				influence = religionCalculator.Influence;
				counterinfluence = religionCalculator.Counterinfluence;
				wealth = wealthCalculator.Wealth;
				isCurrent = true;
			}
		}
	}
}