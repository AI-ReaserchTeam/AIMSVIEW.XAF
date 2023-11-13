using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{
	[NavigationItem("Rental")]
	[DefaultClassOptions]
	
	public class Property : BaseObject
	{ 
	 
		public Property(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
		}


		PropertyType propertyType;
		BuildingStructureType structureType;
		string address;
		string name;

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Name
		{
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Address
		{
			get => address;
			set => SetPropertyValue(nameof(Address), ref address, value);
		}


		public BuildingStructureType StructureType
		{
			get => structureType;
			set => SetPropertyValue(nameof(StructureType), ref structureType, value);
		}

		[Association("Property-Apartments")]
		public XPCollection<Apartment> Apartments
		{
			get
			{
				return GetCollection<Apartment>(nameof(Apartments));
			}
		}

		
		public PropertyType PropertyType
		{
			get => propertyType;
			set => SetPropertyValue(nameof(PropertyType), ref propertyType, value);
		}



	}


	public enum PropertyType
	{
		Residential,
		Commercial
	}

	public enum BuildingStructureType
	{
		Skyscraper,
		HighRise,
		MidRise,
		LowRise,
		Townhouse,
		SingleFamilyHome,
		Duplex,
		Triplex,
		ApartmentBuilding,
		Condominium
	}


}