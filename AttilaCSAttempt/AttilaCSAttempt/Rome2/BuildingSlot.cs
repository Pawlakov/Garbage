using System;
namespace TWAssistant
{
	namespace Rome2
	{
		class BuildingSlot
		{
			private BuildingType type;
			private BuildingBranch building;
			private byte? level;
			//
			public BuildingSlot(BuildingSlot source)
			{
				type = source.type;
				building = source.building;
				level = source.level;
			}
			public BuildingSlot(RegionData region, byte index)
			{
				level = null;
				building = null;
				switch (index)
				{
					case 0:
						if (region.Resource != Resource.NONE)
							type = BuildingType.RESOURCE;
						else if (region.IsBig)
							type = BuildingType.CENTER_CITY;
						else
							type = BuildingType.CENTER_TOWN;
						break;
					case 1:
						if (region.IsCoastal)
							type = BuildingType.COAST;
						else if (region.IsBig)
							type = BuildingType.CITY;
						else
							type = BuildingType.TOWN;
						break;
					default:
						if (region.IsBig)
							type = BuildingType.CITY;
						else
							type = BuildingType.TOWN;
						break;
				}
			}
			//
			public BuildingType Type
			{
				get { return type; }
			}
			public BuildingBranch BuildingBranch
			{
				get { return building; }
				set { building = value; }
			}
			public byte? Level
			{
				get { return level; }
				set { level = value; }
			}
			public short Food
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building.GetFood(level.Value);
					}
					else
						return 0;
				}
			}
			public short Order
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building.GetOrder(level.Value);
					}
					else
						return 0;
				}
			}
			public WealthBonus[] WealthBonuses
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building.GetWealthBonuses(level.Value);
					}
					else
						return null;
				}
			}
			public int WealthBonusCount
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building.GetWealthBonuses(level.Value).Length;
					}
					else
						return 0;
				}
			}
			//
			public void ShowContent()
			{
				Console.WriteLine("{0} {1} {2}", type, BuildingToString, LevelToString);
			}
			public void Fill(Random random, BuildingLibrary buildings, RegionData region)
			{
				if (BuildingBranch == null)
				{
					if (type == BuildingType.RESOURCE)
					{
						byte whichResourceBuilding = (byte)region.Resource;
						for (whichResourceBuilding = 0; region.Resource != buildings[BuildingType.RESOURCE][whichResourceBuilding].Resource; whichResourceBuilding++) { }
						BuildingBranch = buildings[BuildingType.RESOURCE][whichResourceBuilding];
					}
					else
					{
						int randomPick = random.Next(0, buildings[type].Count);
						BuildingBranch = buildings[type][randomPick];
						buildings[type].RemoveAt(randomPick);
					}
				}
				if (Level == null)
				{
					level = (byte)random.Next(0, BuildingBranch.NumberOfLevels);
				}
			}
			//
			private string LevelToString
			{
				get
				{
					if (level == null)
						return "???";
					else
						return level.Value.ToString();
				}
			}
			private string BuildingToString
			{
				get
				{
					if (building == null)
						return "???";
					else
						return building.Name;
				}
			}
		}
	}
}