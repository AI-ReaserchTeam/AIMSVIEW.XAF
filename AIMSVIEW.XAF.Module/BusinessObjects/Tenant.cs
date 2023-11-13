using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{
	[NavigationItem("Rental")]
	[DefaultClassOptions]
	public class Tenant : BaseObject
	{
		public Tenant(Session session)
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
			if (this.Apartment is not null && Session.IsNewObject(this))
			{
				this.Apartment.IsVacant = false;
				this.Apartment.CurrentTenant = this;
			}
				

		}



		Apartment apartment;
		string phoneContact;
		string name;

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Name
		{
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}


		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string PhoneContact
		{
			get => phoneContact;
			set => SetPropertyValue(nameof(PhoneContact), ref phoneContact, value);
		}


		//[DataSourceProperty("Apartment", DataSourcePropertyIsNullMode.SelectAll)]
		//[DataSourceCriteria("IsVacant = 'True' AND Condition == 'Condition.OK'")]
		public Apartment Apartment
		{
			get => apartment;
			set => SetPropertyValue(nameof(Apartment), ref apartment, value);
		}

		[Association("Tenant-Rents")]
		public XPCollection<Rent> Rents
		{
			get
			{
				return GetCollection<Rent>(nameof(Rents));
			}
		}

		public decimal OutstandingRent
		{
			get => GetOutstanding();
		}

		[Action(Caption = "Pay Rent", ConfirmationMessage = "Are you sure?", ImageName = "BO_Sale_Item", AutoCommit = true)]
		public void PayRent()
		{
			// Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
			var rent = Rents.Where(r => r.IsPaid == false).FirstOrDefault();
			if (rent != null)
			{
				var payment = new Payment(this.Session);
				payment.Amount = rent.Outstanding;
				payment.PaymentDate = DateTime.Now;
				payment.Rent = rent;
				payment.Save();
				rent.IsPaid = true;
				//var newrent = new Rent(this.Session);
				//newrent.StartDate = rent.ExpiryDate;
				//newrent.Tenant = this;
				rent.Save();
				this.Save();
			}

		}

		[Action(Caption = "Vacate", ConfirmationMessage = "Are you sure you want to vacate this Tenant?", ImageName = "Action_Delete", AutoCommit = true)]
		public void Vacate()
		{
			//code to remove the tenant from the apartment
			this.Apartment.IsVacant = true; //set the apartment to vacant
			this.Apartment.Save();
			this.Apartment = null; //remove the tenant from the apartment
			this.Save();
		}
		decimal GetOutstanding()
		{
			return Rents.Where(r => r.IsPaid == false).Sum(r => r.Outstanding);
		}
	}
}