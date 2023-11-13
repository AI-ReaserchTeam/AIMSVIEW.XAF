using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{
	[NavigationItem("Projects")]
	[DefaultClassOptions]
	public class Project : BaseObject
	{


		string location;
		DateTime expectedStartDate;
		decimal budget;
		string customerContact;
		string customerName;
		string description;
		string name;

		public Project(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			
			 
		}
		public decimal? GetTotalExpenditure()
		{
			decimal? total = 0;
			foreach (Expense exp in Expences)
			{
				total += exp.Amount;
			}
			return total;
		}




		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Name
		{
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}

		[VisibleInListView(false)]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Description
		{
			get => description;
			set => SetPropertyValue(nameof(Description), ref description, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string CustomerName
		{
			get => customerName;
			set => SetPropertyValue(nameof(CustomerName), ref customerName, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string CustomerContact
		{
			get => customerContact;
			set => SetPropertyValue(nameof(CustomerContact), ref customerContact, value);
		}


		public decimal Budget
		{
			get => budget;
			set => SetPropertyValue(nameof(Budget), ref budget, value);
		}



		public DateTime ExpectedStartDate
		{
			get => expectedStartDate;
			set => SetPropertyValue(nameof(ExpectedStartDate), ref expectedStartDate, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Location
		{
			get => location;
			set => SetPropertyValue(nameof(Location), ref location, value);
		}


		[Association("Project-Expences")]
		public XPCollection<Expense> Expences
		{
			get
			{
				return GetCollection<Expense>(nameof(Expences));
			}
		}


		public decimal? TotalExpenditure => GetTotalExpenditure();


	}


}