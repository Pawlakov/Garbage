using System;
using System.Xml;

partial class BuildingBranch
{
    /// <summary>
    /// Struct containing information about bonuses from single building level.
    /// </summary>
    struct BuildingLevel
    {
        private short food;
        private short order;
        private WealthBonus[] wealthBonuses;
        //
        /// <summary>
        /// Creates instance of BuildingLevel struct.
        /// </summary>
        /// <param name="levelNode">
        /// XMLNode ceontaining required information.
        /// </param>
        public BuildingLevel(XmlNode levelNode)
        {
            XmlNodeList bonusNodeList = levelNode.ChildNodes;
            food = Convert.ToInt16(levelNode.Attributes.GetNamedItem("food").InnerText);
            order = Convert.ToInt16(levelNode.Attributes.GetNamedItem("order").InnerText);
            wealthBonuses = new WealthBonus[bonusNodeList.Count];
            for (byte whichBonus = 0; whichBonus < wealthBonuses.Length; whichBonus++)
            {
                wealthBonuses[whichBonus] = new WealthBonus(bonusNodeList.Item(whichBonus));
            }
        }
        /// <summary>
        /// Food bonus.
        /// </summary>
        public short Food
        {
            get { return food; }
        }
        /// <summary>
        /// Public order bonus.
        /// </summary>
        public short Order
        {
            get { return order; }
        }
        /// <summary>
        /// Array of wealth bonuses.
        /// </summary>
        public WealthBonus[] WealthBonuses
        {
            get { return wealthBonuses; }
        }
    }
}