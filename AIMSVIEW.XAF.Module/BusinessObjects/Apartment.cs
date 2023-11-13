using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Utils;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{
	[NavigationItem("Rental")]
	[DefaultProperty("ApartmentAlias")]
	[DefaultClassOptions]
	public class Apartment : BaseObject
	{
		public Apartment(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			this.isVacant = true;
			Condition = Condition.Ok;
		}




		Tenant currentTenant;
		Condition condition;
		ApartmentSize size;
		Consultant consultant;
		string name;
		ApartmentType apartmentType;
		bool isVacant;
		decimal rentAmount;
		Property property;


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Name
		{
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}

		public decimal RentAmount
		{
			get => rentAmount;
			set => SetPropertyValue(nameof(RentAmount), ref rentAmount, value);
		}

		[XafDisplayName("Vacant")]
		public bool IsVacant
		{
			get => isVacant;
			set => SetPropertyValue(nameof(IsVacant), ref isVacant, value);
		}


		public ApartmentType ApartmentType
		{
			get => apartmentType;
			set => SetPropertyValue(nameof(ApartmentType), ref apartmentType, value);
		}


		[Association("Property-Apartments")]
		public Property Property
		{
			get => property;
			set => SetPropertyValue(nameof(Property), ref property, value);
		}

		public Consultant Consultant
		{
			get => consultant;
			set => SetPropertyValue(nameof(Consultant), ref consultant, value);
		}


		public ApartmentSize Size
		{
			get => size;
			set => SetPropertyValue(nameof(Size), ref size, value);
		}

		[VisibleInListView(false)]
		[XafDisplayName("Apartment")]
		public string ApartmentAlias { get => ObjectFormatter.Format("{Property.Name} {Name}", this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty); }

		public Condition Condition
		{
			get => condition;
			set => SetPropertyValue(nameof(Condition), ref condition, value);
		}

		//[DataSourceProperty("Tenant", DataSourcePropertyIsNullMode.SelectAll)]
		//[DataSourceCriteria("Position.Title = 'Manager' AND Oid != '@This.Oid'")]
		public Tenant CurrentTenant
		{
			get => currentTenant;
			set => SetPropertyValue(nameof(CurrentTenant), ref currentTenant, value);
		}


	}

	public enum ApartmentType
	{
		Bungalow,
		Penthouse,
		StaffRoom,
		MiniFlat,
		MiniSelcontained,
		SelfContained,
		SelfContainedExecutive,
		Duplex,
		Duplex_Penthouse,
		AreaSpace,
		BoysQuarters,
		Shop
	}

	public enum ApartmentSize
	{
		OneBedroom,
		TwoBedroom,
		ThreeBedroom,
		ThreeBedroom_Materoom,
		FourBedroom,
		FourBedrroom_Materomm,
		FiveBedroom,
		FiveBedrroom_Materomm,
		MateRoom
	}

	public enum Consultant
	{
		AIMSVIEW,
		ICITY,
		AIMSANDICITY
	}

	public enum Condition
	{
		[ImageName("State_Validation_Valid")]
		Ok,
		[ImageName("State_Validation_Invalid")]
		UnderMaintenance,
		[ImageName("State_Validation_Invalid")]
		UnderRenovation,
	}

}