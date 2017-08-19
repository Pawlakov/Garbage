using System;
using System.Xml;
/// <summary>
/// Struct respresenting a single branch of buildings with multiple levels.
/// </summary>
partial struct BuildingBranch
{
	private string name;
	private BuildingType type;
	private Resource resource;
	private BuildingLevel[] levels;
	//
    /// <summary>
    /// Creates new instance of BuildingBranch struct.
    /// </summary>
    /// <param name="branchNode">
    /// XMLNode required to create new instance.
    /// </param>
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
    /// <summary>
    /// Gets level's public order bonus (or penalty).
    /// </summary>
    /// <param name="index">
    /// Zero-based index of building level.
    /// </param>
    /// <returns>
    /// Level's public order bonus (or penalty).
    /// </returns>
    public short GetFood(byte index)
    {
        return levels[index].Food;
    }
    /// <summary>
    /// Gets level's food bonus (or penalty).
    /// </summary>
    /// <param name="index">
    /// Zero-based index of building level.
    /// </param>
    /// <returns>
    /// Level's food bonus (or penalty).
    /// </returns>
    public short GetOrder(byte index)
    {
        return levels[index].Order;
    }
    /// <summary>
    /// Gets level's wealth bonuses.
    /// </summary>
    /// <param name="index">
    /// Zero-based index of building level.
    /// </param>
    /// <returns>
    /// Array of level's wealth bonuses.
    /// </returns>
    public WealthBonus[] GetWealthBonuses(byte index)
    {
        return levels[index].WealthBonuses;
    }
    /// <summary>
    /// Name of the branch.
    /// </summary>
    public string Name
    {
        get { return name; }
    }
    public int NumberOfLevels
    {
        get { return levels.Length; }
    }
}