using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DisplayNameAttribute = DevExpress.Xpo.DisplayNameAttribute;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{
	[Appearance("Outstanding", Criteria = "Outstanding > 0", TargetItems = "*", FontColor = "Red")]
	[Appearance("OutstandingPaid", Criteria = "Outstanding = 0", TargetItems = "*", FontColor = "Green")]
	[Appearance("Paid Rent", Criteria = "IsPaid = true", TargetItems = "*", FontStyle = System.Drawing.FontStyle.Bold)]
	//[NavigationItem("Rental")]
	//[DefaultClassOptions]
	
	public class Rent : BaseObject
	{ 
		public Rent(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			
			
		}

		protected override void OnSaving()
		{
			base.OnSaving();

			if(this.Tenant != null && Session.IsNewObject(this))
			{
				this.rentAmount = this.Tenant.Apartment.RentAmount;
				this.expiryDate = this.StartDate.AddYears(1);
				//this.Outstanding = this.rentAmount;
			}
		}


		Tenant tenant;
		bool isPaid;
		decimal rentAmount;
		DateTime expiryDate;
		DateTime startDate;

		public DateTime StartDate
		{
			get => startDate;
			set => SetPropertyValue(nameof(StartDate), ref startDate, value);
		}

		//[PersistentAlias("StartDate.AddYears(1)")]
		public DateTime ExpiryDate
		{
			get => expiryDate;
			set => SetPropertyValue(nameof(ExpiryDate), ref expiryDate, value);
		}

		public decimal RentAmount
		{
			get => rentAmount;
			set => SetPropertyValue(nameof(RentAmount), ref rentAmount, value);
		}

		[DisplayName("Paid")]
		[ImmediatePostData]
		public bool IsPaid
		{
			get => isPaid;
			set => SetPropertyValue(nameof(IsPaid), ref isPaid, value);
		}



		[Association("Rent-Payments")]
		public XPCollection<Payment> Payments
		{
			get
			{
				return GetCollection<Payment>(nameof(Payments));
			}
		}


		[Association("Tenant-Rents")]
		[RuleRequiredField]
		[RuleUniqueValue]
		public Tenant Tenant
		{
			get => tenant;
			set => SetPropertyValue(nameof(Tenant), ref tenant, value);
		}

		[PersistentAlias("RentAmount - TotalPaid")]
		public decimal Outstanding
		{
			get { return (decimal)EvaluateAlias("Outstanding"); }
			//set => SetPropertyValue(nameof(Outstanding), ref outstanding, value);
		}

		private decimal TotalPaid => Payments.Sum(x => x.Amount);

	}
}