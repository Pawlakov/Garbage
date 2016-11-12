//obiekt przechowuje kombinacje budynków regionu (DONE)
//class RegionCombination
//{
//	List<Building> buildings;
//	Random random;

//	public RegionCombination(RegionData data)
//	{
//		buildings = new List<Building>();
//		bool useResource = Convert.ToBoolean(random.Next(1));

//		if (data.coast == true)
//			buildings.Add(Generator.generateBuilding(BuildingType.COAST));


//		if (data.big == true)
//		{
//			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
//			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
//			buildings.Add(Generator.generateBuilding(BuildingType.CITY));
//			buildings.Add(Generator.generateBuilding(BuildingType.CITY));

//			if ((data.resource != Resource.NONE) && (useResource == true))
//			{
//				buildings.Add(Generator.generateBuilding(BuildingType.RESOURCE, data.resource));
//			}
//			else
//			{
//				buildings.Add(Generator.generateBuilding(BuildingType.CITY));
//			}
//		}
//		else
//		{
//			buildings.Add(Generator.generateBuilding(BuildingType.TOWN));
//			buildings.Add(Generator.generateBuilding(BuildingType.TOWN));

//			if ((data.resource != Resource.NONE) && (useResource == true))
//			{
//				buildings.Add(Generator.generateBuilding(BuildingType.RESOURCE, data.resource));
//			}
//			else
//			{
//				buildings.Add(Generator.generateBuilding(BuildingType.TOWN));
//			}
//		}
//	}

//}
//class ProvinceCombination
//{
//	List<Building>[] buildings;
//	Random random;

//	int food;
//	int order;
//	int[] sanitation;
//	ProvinceWealth wealth;

//	ProvinceCombination(ProvinceData data)
//	{
//		buildings = new List<Building>[3];
//		sanitation = new int[3];
//		wealth = new Wealth[3];

//		for (int i = 0; i < 3; i++)
//		{
//			sanitation[i] = 0;
//			wealth[i] = new Wealth();
//		}

//		//

//		for (int i = 0; i < 3; i++)
//		{
//			bool useResource = Convert.ToBoolean(random.Next(1));

//			if (data.regionsData[i].coast == true)
//				buildings[i].Add(Generator.generateBuilding(BuildingType.COAST));


//			if (data.regionsData[i].big == true)
//			{
//				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
//				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
//				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
//				buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));

//				if ((data.regionsData[i].resource != Resource.NONE) && (useResource == true))
//				{
//					buildings[i].Add(Generator.generateBuilding(BuildingType.RESOURCE, data.regionsData[i].resource));
//				}
//				else
//				{
//					buildings[i].Add(Generator.generateBuilding(BuildingType.CITY));
//				}
//			}
//			else
//			{
//				buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));
//				buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));

//				if ((data.regionsData[i].resource != Resource.NONE) && (useResource == true))
//				{
//					buildings[i].Add(Generator.generateBuilding(BuildingType.RESOURCE, data.regionsData[i].resource));
//				}
//				else
//				{
//					buildings[i].Add(Generator.generateBuilding(BuildingType.TOWN));
//				}
//			}
//		}
//	}

//	public void makeCombination()
//	{

//	}

//	void calculate()
//	{

//	}

//	bool checkConditions()
//	{

//	}
//}