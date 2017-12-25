using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public class BuildingBranch
		{
			readonly public string name;
			readonly public BuildingType type;
			//
			readonly public BuildingLevel[] levels;
			//
			readonly public Resource resource;
			readonly public Religion? religion;
			readonly public bool isReligionExclusive;
			//
			public BuildingBranch(XmlNode branchNode, Religion stateReligion, bool useLegacy)
			{
				
				name = branchNode.Attributes.GetNamedItem("n").InnerText;
				Enum.TryParse(branchNode.Attributes.GetNamedItem("t").InnerText, out type);
				XmlNodeList levelNodeList = branchNode.ChildNodes;
				levels = new BuildingLevel[levelNodeList.Count];
				for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
				{
					try
					{
						levels[whichLevel] = new BuildingLevel(levelNodeList.Item(whichLevel));
					}
					catch (Exception exception)
					{
						Console.WriteLine("{0} fell off a bike.", name);
						Console.WriteLine(exception.Message);
						Console.WriteLine(exception.StackTrace);
						Console.ReadKey();
					}
				}
				//
				resource = Resource.NONE;
				religion = null;
				isReligionExclusive = false;
				XmlNode temporary = branchNode.Attributes.GetNamedItem("r");
				if (type == BuildingType.RESOURCE)
					Enum.TryParse(temporary.InnerText, out resource);
				if (type == BuildingType.RESOURCE && resource == Resource.NONE)
					throw new Exception("Resource building with NONE resource(" + name + ").");
				//
				temporary = branchNode.Attributes.GetNamedItem("rel");
				if (temporary != null)
				{
					Religion temporaryReligion;
					Enum.TryParse(temporary.InnerText, out temporaryReligion);
					religion = temporaryReligion;
					temporary = branchNode.Attributes.GetNamedItem("rex");
					isReligionExclusive = Convert.ToBoolean(temporary.InnerText);
				}
				for (int whichLevel = 0; whichLevel<levels.Length; ++whichLevel)
				{
					if (levels[whichLevel].isLegacy == !useLegacy)
						levels[whichLevel].ForceVoid();
				}
			}
			//
			public int NumberOfLevels
			{
				get { return levels.Length; }
			}
			public int NonVoidCount
			{
				get
				{
					int result = 0;
					foreach (BuildingLevel level in levels)
					{
						if (!level.IsVoid)
							++result;
					}
					return result;
				}
			}
			public BuildingLevel this[int whichLevel]
			{
				get { return levels[whichLevel]; }
			}
			//
			public void EvalueateLevels()
			{
				for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
					levels[whichLevel].Evaluate();
			}
			public int GetLevel(Random random)
			{
				int result;
				do
				{
					result = random.Next(0, levels.Length);
				} while (levels[result].IsVoid == true);
				return result;
			}
			public int GetLevel(Random random, int desiredLevel)
			{
				int result;
				do
				{
					result = random.Next(0, levels.Length);
				} while (levels[result].IsVoid == true || levels[result].level != desiredLevel);
				return result;
			}
		}
	}
}