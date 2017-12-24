using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		struct FactionsList
		{
			readonly Faction[] factions;
			//
			public FactionsList(string filename, Religion stateReligion, bool useLegacyTechs)
			{
				XmlDocument sourceFile = new XmlDocument();
				sourceFile.Load(filename);
				XmlNodeList factionNodesList = sourceFile.GetElementsByTagName("faction");
				factions = new Faction[factionNodesList.Count];
				for (int whichFaction = 0; whichFaction < factions.Length; ++whichFaction)
				{
					factions[whichFaction] = new Faction(factionNodesList[whichFaction], stateReligion, useLegacyTechs);
				}

			}
			//
			public Faction this[int whichFaction]
			{
				get { return factions[whichFaction]; }
			}
			//
			public void ShowList()
			{
				for (int whichFaction = 0; whichFaction < factions.Length; ++whichFaction)
					Console.WriteLine("{0}. {1}", whichFaction, factions[whichFaction].Name);
			}
		}
	}
}