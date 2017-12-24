using System;
using System.Xml;
namespace TWAssistant
{
	namespace Attila
	{
		public struct RegionData
		{
			readonly string name;
			readonly Resource resource;
			readonly bool isCoastal;
			readonly bool isBig;
			readonly int slotsCountOffset;
			//
			public RegionData(XmlNode regionNode, bool iniIsBig)
			{
				XmlNode temporary;
				resource = Resource.NONE;
				slotsCountOffset = 0;
				//
				name = regionNode.Attributes.GetNamedItem("n").InnerText;
				//
				temporary = regionNode.Attributes.GetNamedItem("r");
				if (temporary != null)
					if (!Enum.TryParse(temporary.InnerText, out resource))
						throw (new Exception("Couldn't figure out resource type."));
				//
				isCoastal = Convert.ToBoolean(regionNode.Attributes.GetNamedItem("c").InnerText);
				//
				isBig = iniIsBig;
				//
				temporary = regionNode.Attributes.GetNamedItem("o");
				if (temporary != null)
					slotsCountOffset = Convert.ToInt32(temporary.InnerText);
			}
			//
			public string Name
			{
				get { return name; }
			}
			public Resource Resource
			{
				get { return resource; }
			}
			public bool IsCoastal
			{
				get { return isCoastal; }
			}
			public bool IsBig
			{
				get { return isBig; }
			}
			public int SlotsCount
			{
				get
				{
					int result;
					if (isBig)
						result = 6;
					else
						result = 4;
					return result += (slotsCountOffset);
				}
			}
		}
	}
}