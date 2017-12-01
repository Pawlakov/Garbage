using System;
namespace TWAssistant
{
	namespace Rome2
	{
		class ProvinceCombination
		{
			private ProvinceData province;
			private Faction faction;
			private BuildingSlot[][] slots;
			private System.Collections.Generic.List<string> deletedBuildings;
			private bool isCurrent;
			//
			private short food;
			private short order;
			private double wealth;
			//
			public ProvinceCombination(ProvinceData iniProvince, Faction iniFaction)
			{
				province = iniProvince;
				faction = iniFaction;
				deletedBuildings = null;
				//
				slots = new BuildingSlot[province.NumberOfRegions][];
				for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
				{
					slots[whichRegion] = new BuildingSlot[province[whichRegion].NumberOfSlots];
					for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
					{
						slots[whichRegion][whichSlot] = new BuildingSlot(province[whichRegion], whichSlot);
					}
				}
				isCurrent = false;
			}
			public ProvinceCombination(ProvinceCombination source)
			{
				province = source.province;
				faction = source.faction;
				deletedBuildings = null;
				//
				slots = new BuildingSlot[source.slots.Length][];
				for (byte whichRegion = 0; whichRegion < source.slots.Length; whichRegion++)
				{
					slots[whichRegion] = new BuildingSlot[source.slots[whichRegion].Length];
					for (byte whichSlot = 0; whichSlot < source.slots[whichRegion].Length; whichSlot++)
						slots[whichRegion][whichSlot] = new BuildingSlot(source.slots[whichRegion][whichSlot]);
				}
				isCurrent = false;
			}
			//
			public void Fill()
			{
				Random random = new Random();
				for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
				{
					BuildingLibrary buildings = new BuildingLibrary(faction.Buildings);
					for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
					{
						if (slots[whichRegion][whichSlot].BuildingBranch != null)
						{
							buildings[slots[whichRegion][whichSlot].Type].Remove(slots[whichRegion][whichSlot].BuildingBranch);
						}
					}
					for (byte whichBuilding = 0; whichBuilding < slots[whichRegion].Length; whichBuilding++)
					{
						slots[whichRegion][whichBuilding].Fill(random, buildings, province[whichRegion]);
					}
				}
				isCurrent = false;
			}
			public void CurbUselessBuildings()
			{
				deletedBuildings = faction.CurbUselessBuildings();
			}
			public void ShowContent()
			{
				Console.WriteLine("{0}", province.Name);
				for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
				{
					Console.WriteLine("	{0}: {1}", whichRegion, province[whichRegion].Name);
					for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
					{
						Console.Write("		{0}: ", whichSlot);
						slots[whichRegion][whichSlot].ShowContent();
					}
				}
				Console.WriteLine("W: {0} F: {1} O: {2}", Wealth, Food, Order);
				if (deletedBuildings != null)
				{
					Console.WriteLine("Recently excluded buildings:");
					for (byte whichBuilding = 0; whichBuilding < deletedBuildings.Count; whichBuilding++)
					{
						Console.WriteLine("	{0}: {1}", whichBuilding, deletedBuildings[whichBuilding]);
					}
				}
			}
			public void RewardUsefulBuildings()
			{
				for (byte whichRegion = 0; whichRegion < slots.Length; whichRegion++)
				{
					for (byte whichSlot = 0; whichSlot < slots[whichRegion].Length; whichSlot++)
					{
						slots[whichRegion][whichSlot].BuildingBranch.Usefuliness += 1;
					}
				}
			}
			public void ForceConstraint()
			{
				byte whichRegion;
				byte whichSlot;
				byte choice;
				Console.WriteLine("Which region?");
				whichRegion = Convert.ToByte(Console.ReadLine());
				Console.WriteLine("Which slot?");
				whichSlot = Convert.ToByte(Console.ReadLine());
				Console.WriteLine("which coinstraint type?");
				Console.WriteLine("0. Level.");
				Console.WriteLine("1. Building.");
				choice = Convert.ToByte(Console.ReadLine());
				if (choice == 0)
				{
					Console.WriteLine("Which level?");
					choice = Convert.ToByte(Console.ReadLine());
					slots[whichRegion][whichSlot].Level = choice;
				}
				else
				{
					faction.Buildings.ShowListOneType(slots[whichRegion][whichSlot].Type);
					Console.WriteLine("Which building?");
					choice = Convert.ToByte(Console.ReadLine());
					slots[whichRegion][whichSlot].BuildingBranch = faction.Buildings[slots[whichRegion][whichSlot].Type, choice];
				}
				isCurrent = false;
			}
			//
			public BuildingSlot this[byte whichRegion, byte whichSlot]
			{
				get { return this.slots[whichRegion][whichSlot]; }
			}
			public short Food
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.food;
				}
			}
			public short Order
			{
				get
				{
					if (!this.isCurrent)
						HarvestBuildings();
					return this.order;
				}
			}
			public double Wealth
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
				ProvinceWealth wealthCalculator = new ProvinceWealth();
				if (this.faction.WealthBonuses != null)
					for (byte whichBonus = 0; whichBonus < this.faction.WealthBonuses.Length; whichBonus++)
						wealthCalculator.AddBonus(this.faction.WealthBonuses[whichBonus]);
				for (byte whichRegion = 0; whichRegion < this.slots.Length; whichRegion++)
				{
					for (byte whichSlot = 0; whichSlot < this.slots[whichRegion].Length; whichSlot++)
					{
						this.food += this.slots[whichRegion][whichSlot].Food;
						this.order += this.slots[whichRegion][whichSlot].Order;
						for (byte whichBonus = 0; whichBonus < this.slots[whichRegion][whichSlot].WealthBonusCount; whichBonus++)
							wealthCalculator.AddBonus(this.slots[whichRegion][whichSlot].WealthBonuses[whichBonus]);
					}
				}
				this.wealth = wealthCalculator.Wealth;
				this.isCurrent = true;
			}
		}
	}
}