using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public class BuildingBranch
		{
			readonly string name;
			readonly BuildingType type;
			//
			readonly BuildingLevel[] levels;
			//
			readonly Resource resource;
			readonly Religion? religion;
			readonly bool isReligionExclusive;
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
				
				temporary = branchNode.Attributes.GetNamedItem("rel");
				if (temporary != null)
				{
					Religion temporaryReligion;
					Enum.TryParse(temporary.InnerText, out temporaryReligion);
					religion = temporaryReligion;
					temporary = branchNode.Attributes.GetNamedItem("rex");
					isReligionExclusive = Convert.ToBoolean(temporary.InnerText);
				}
				if (isReligionExclusive && stateReligion != religion.Value)
				{
					for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
					{
						levels[whichLevel].ForceVoid();
					}
				}
				for (int whichLevel = 0; whichLevel<levels.Length; ++whichLevel)
				{
					if (levels[whichLevel].IsLegacy == !useLegacy)
						levels[whichLevel].ForceVoid();
				}
			}
			//
			public string Name
			{
				get { return name; }
			}
			public BuildingType Type
			{
				get { return type; }
			}
			//
			public int NumberOfLevels
			{
				get { return levels.Length; }
			}
			public int nonVoidCount
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
			public Resource Resource
			{
				get { return resource; }
			}
			public Religion? Religion
			{
				get { return religion; }
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
				} while (levels[result].IsVoid == true || levels[result].Level != desiredLevel);
				return result;
			}
		}
	}
}