using System;
using System.Xml;
struct FactionsList
{
    private Faction[] factions;
    //
    public Faction this[int whichFaction]
    {
        get
        {
            return factions[whichFaction];
        }
    }
    public FactionsList(string filename)
    {
        XmlDocument sourceFile = new XmlDocument();
        sourceFile.Load(filename);
        XmlNodeList factionNodesList = sourceFile.GetElementsByTagName("faction");
        factions = new Faction[factionNodesList.Count];
        for (byte whichFaction = 0; whichFaction < factions.Length; whichFaction++)
        {
            factions[whichFaction] = new Faction(factionNodesList[whichFaction]);
        }
    }
    public void ShowList()
    {
        for (byte whichFaction = 0; whichFaction < factions.Length; whichFaction++)
            Console.WriteLine("{0}. {1}", whichFaction, factions[whichFaction].Name);
    }
}