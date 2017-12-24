using System;
namespace TWAssistant
{
	namespace Attila
	{
		public class BuildingSlot
		{
			BuildingType type;
			BuildingBranch buildingBranch;
			BuildingLevel buildingLevel;
			int? level;
			//
			public BuildingSlot(BuildingSlot source)
			{
				type = source.type;
				buildingBranch = source.buildingBranch;
				buildingLevel = source.buildingLevel;
				level = source.level;
			}
			public BuildingSlot(RegionData region, int index)
			{
				level = null;
				buildingBranch = null;
				buildingLevel = null;
				switch (index)
				{
					case 0:
						if (region.IsBig)
							type = BuildingType.CENTERCITY;
						else
							type = BuildingType.CENTERTOWN;
						level = 4;
						break;
					case 1:
						if (region.IsCoastal)
						{
							type = BuildingType.COAST;
							level = 4;
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
							level = 4;
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
			public BuildingBranch BuildingBranch
			{
				get { return buildingBranch; }
				set { buildingBranch = value; }
			}
			public BuildingLevel BuildingLevel
			{
				get { return buildingLevel; }
				set { buildingLevel = value; }
			}
			public int? Level
			{
				get { return level; }
				set { level = value; }
			}
			public BuildingLevel Building
			{
				get { return buildingLevel; }
			}
			//
			public void ShowContent()
			{
				Console.WriteLine("{0} {1}", BuildingToString, LevelToString);
			}
			public void Fill(Random random, BuildingLibrary library, RegionData region)
			{
				if (buildingLevel == null)
				{
					if (buildingBranch == null)
					{
						if (type == BuildingType.RESOURCE)
						{
							buildingBranch = library.GetBuilding(region.Resource);
						}
						else
						{
							buildingBranch = library.GetBuilding(random, type);
						}
					}
					if (level == null)
					{
						buildingLevel = buildingBranch[buildingBranch.GetLevel(random)];
						level = buildingLevel.Level;
					}
					else
					{
						buildingLevel = buildingBranch[buildingBranch.GetLevel(random, level.Value)];
					}
				}
			}
			public void Reward()
			{
				buildingLevel.Reward();
			}
			//
			string LevelToString
			{
				get
				{
					if (buildingLevel != null)
						return buildingLevel.Level.ToString();
					if (level != null)
						return level.Value.ToString();
					return "?";
				}
			}
			string BuildingToString
			{
				get
				{
					if (buildingLevel != null)
						return buildingLevel.Name;
					if (buildingBranch != null)
						return buildingBranch.Name;
					return type.ToString();
				}
			}
		}
	}
}