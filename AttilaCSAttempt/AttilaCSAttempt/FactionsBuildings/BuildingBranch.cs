using System;
using System.Xml;
struct BuildingBranch
{
	private string name;
	private BuildingType type;
	private Resource resource;
	private BuildingLevel[] levels;
	//
	public BuildingBranch(XmlNode branchNode)
	{
		XmlNodeList levelNodeList = branchNode.ChildNodes;
		name = branchNode.Attributes.GetNamedItem("name").InnerText;
		Enum.TryParse(branchNode.Attributes.GetNamedItem("type").InnerText, out type);
		Enum.TryParse(branchNode.Attributes.GetNamedItem("resource").InnerText, out resource);
		levels = new BuildingLevel[levelNodeList.Count];
		for(byte whichLevel = 0; whichLevel < levels.Length; whichLevel++)
		{
			levels[whichLevel] = new BuildingLevel(levelNodeList.Item(whichLevel));
		}
	}
}