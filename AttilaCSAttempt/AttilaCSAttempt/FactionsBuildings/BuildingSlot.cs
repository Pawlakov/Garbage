using System;
/// <summary>
/// Class respresenting a single building slot: its type an content.
/// </summary>
class BuildingSlot
{
    private BuildingType type;
    private BuildingBranch building;
    private byte? level;
    //
    /// <summary>
    /// Creates a copy of existing instance of BuildingSlot class.
    /// </summary>
    /// <param name="templateSlot">
    /// Object to copy.
    /// </param>
    public BuildingSlot(BuildingSlot templateSlot)
    {
        type = templateSlot.type;
        building = templateSlot.building;
        level = templateSlot.level;
    }
    /// <summary>
    /// Creates new instance of BuildingSlot class.
    /// </summary>
    /// <param name="region">
    /// Informations about region.
    /// </param>
    /// <param name="index">
    /// Index of this building slot in its region.
    /// </param>
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
    /// <summary>
    /// Type of the possible building.
    /// </summary>
    public BuildingType Type
    {
        get { return type; }
    }
    /// <summary>
    /// Building placed in this slot.
    /// </summary>
    public BuildingBranch BuildingBranch
    {
        get { return building; }
        set { building = value; }
    }
    /// <summary>
    /// Level of building in this slot.
    /// </summary>
    public byte? Level
    {
        get { return level; }
        set { level = value; }
    }
    /// <summary>
    /// Shows content of this slot in console.
    /// </summary>
    public void ShowContent()
    {
        if (building != null && level != null)
            Console.WriteLine("Type: {0} Building: {1} Level: {2}",type , building.Name, level.Value);
        else if (building == null && level != null)
            Console.WriteLine("Type: {0} Building: ??? Level: {1}", type, level.Value);
        else if (building != null && level == null)
            Console.WriteLine("Type: {0} Building: {1} Level: ???", type, building.Name);
        else if (building == null && level == null)
            Console.WriteLine("Type: {0} Building: ??? Level: ???", type);
    }
}