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
			public BuildingBranch(XmlNode branchNode)
			{
				name = branchNode.Attributes.GetNamedItem("n").InnerText;
				try
				{
					type = (BuildingType)Enum.Parse(typeof(BuildingType), branchNode.Attributes.GetNamedItem("t").InnerText);
					XmlNodeList levelNodeList = branchNode.ChildNodes;
					levels = new BuildingLevel[levelNodeList.Count];
					for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
						levels[whichLevel] = new BuildingLevel(levelNodeList.Item(whichLevel));
					//
					resource = Resource.NONE;
					religion = null;
					isReligionExclusive = false;
					XmlNode temporary = branchNode.Attributes.GetNamedItem("r");
					if (type == BuildingType.RESOURCE)
						resource = (Resource)Enum.Parse(typeof(Resource), temporary.InnerText);
					if (type == BuildingType.RESOURCE && resource == Resource.NONE)
						throw new Exception("Resource building with NONE resource(" + name + ").");
					//
					temporary = branchNode.Attributes.GetNamedItem("rel");
					if (temporary != null)
					{
						Religion temporaryReligion;
						temporaryReligion = (Religion)Enum.Parse(typeof(Religion), temporary.InnerText);
						religion = temporaryReligion;
						temporary = branchNode.Attributes.GetNamedItem("rex");
						isReligionExclusive = Convert.ToBoolean(temporary.InnerText);
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine("Building fell off a bike. Catched in building.");
					Console.WriteLine(exception.Message);
					Console.WriteLine(exception.StackTrace);
					Console.ReadKey();
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
			public void ApplyLegacy()
			{
				for (int whichLevel = 0; whichLevel < levels.Length; ++whichLevel)
				{
					if (levels[whichLevel].isLegacy == !Globals.useLegacy)
						levels[whichLevel].ForceVoid();
				}
			}
			public int GetLevel(XorShift random)
			{
				int result;
				do
				{
					result = (int)random.Next(0, (uint)levels.Length);
				} while (levels[result].IsVoid == true);
				return result;
			}
			public int GetLevel(XorShift random, int desiredLevel)
			{
				int result;
				do
				{
					result = (int)random.Next(0, (uint)levels.Length);
				} while (levels[result].IsVoid == true || levels[result].level != desiredLevel);
				return result;
			}
		}
	}
}