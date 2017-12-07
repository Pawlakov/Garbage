using System;
namespace TWAssistant
{
	namespace Attila
	{
		class BuildingSlot
		{
			private BuildingType type;
			private BuildingBranch building;
			private uint? level;
			//
			public BuildingSlot(BuildingSlot source)
			{
				type = source.type;
				building = source.building;
				level = source.level;
			}
			public BuildingSlot(RegionData region, BuildingLibrary library, uint index)
			{
				level = null;
				building = null;
				switch (index)
				{
					case 0:
						if (region.IsBig)
							type = BuildingType.CENTERCITY;
						else
							type = BuildingType.CENTERTOWN;
						building = library.GetBuilding(null, type);
						level = 3;
						break;
					case 1:
						if (region.IsCoastal)
						{
							type = BuildingType.COAST;
							//level = 3;
						}
						else if (region.IsBig)
							type = BuildingType.CITY;
						else
							type = BuildingType.TOWN;
						break;
					case 2:
						if (region.Resource != Resource.NONE)
						{
							type = BuildingType.RESOURCE;
							//level = 3;
						}
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
			public BuildingBranch Building
			{
				get { return building; }
				set { building = value; }
			}
			public uint? Level
			{
				get { return level; }
				set { level = value; }
			}
			public int getFood(uint fertility)
			{
				if (level.HasValue && building != null)
				{
					return building[level.Value].GetFood(fertility);
				}
				else
					return 0;
			}
			public int Order
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building[level.Value].Order;
					}
					else
						return 0;
				}
			}
			public int ProvincionalSanitation
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building[level.Value].ProvincionalSanitation;
					}
					else
						return 0;
				}
			}
			public int RegionalSanitation
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building[level.Value].RegionalSanitation;
					}
					else
						return 0;
				}
			}
			public int ReligiousInfluence
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building[level.Value].ReligiousInfluence;
					}
					else
						return 0;
				}
			}
			public uint Fertility
			{
				get
				{
					if (level.HasValue && building != null)
					{
						return building[level.Value].Fertility;
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
						return building[level.Value].WealthBonuses;
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
						return building[level.Value].WealthBonuses.Length;
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
			public void Fill(Random random, BuildingLibrary library, RegionData region)
			{
				if (building == null)
				{
					if (type == BuildingType.RESOURCE)
					{
						building = library.GetBuilding(random, region.Resource);
					}
					else
					{
						building = library.GetBuilding(random, type);
					}
				}
				if (level == null)
				{
					level = building.GetLevel(random);
				}
			}
			public void Reward()
			{
				building.RewardLevel(level.Value);
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