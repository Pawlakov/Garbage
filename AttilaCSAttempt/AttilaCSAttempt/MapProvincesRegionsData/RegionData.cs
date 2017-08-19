using System;
using System.Xml;
/// <summary>
/// Contain all region's conditions.
/// </summary>
struct RegionData
{
    private string name;
    private Resource resource;
    private bool isCoastal;
    private bool isBig;
    //
    /// <summary>
    /// Creates instance of region data.
    /// </summary>
    /// <param name="regionNode">
    /// XMLNode containing necessary inforamtions.
    /// </param>
    /// <param name="iniIsBig">
    /// Information if region's settlement is big.
    /// </param>
    public RegionData(XmlNode regionNode, bool iniIsBig)
    {
        isBig = iniIsBig;
        name = regionNode.Attributes.GetNamedItem("name").InnerText;
        isCoastal = Convert.ToBoolean(regionNode.Attributes.GetNamedItem("is_coastal").InnerText);
        Enum.TryParse(regionNode.Attributes.GetNamedItem("resource").InnerText, out resource);
    }
    /// <summary>
    /// Return region's name.
    /// </summary>
    public string Name
    {
        get
        {
            return name;
        }
    }
    /// <summary>
    /// Returns region's resource.
    /// </summary>
    public Resource Resource
    {
        get
        {
            return resource;
        }
    }
    /// <summary>
    /// Returns information if region is coastal.
    /// </summary>
    public bool IsCoastal
    {
        get { return isCoastal; }
    }
    /// <summary>
    /// Returns information if region's settlement is big.
    /// </summary>
    public bool IsBig
    {
        get { return isBig; }
    }
    /// <summary>
    /// Number of building slots in this region.
    /// </summary>
    public byte NumberOfSlots
    {
        get
        {
            if(isBig)
            {
                if (isCoastal)
                    return 6;
                else
                    return 5;
            }
            else
            {
                if (isCoastal)
                    return 4;
                else
                    return 3;
            }
        }
    }
}