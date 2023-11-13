using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
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
	
	public class Payment : BaseObject
	{ 
		public Payment(Session session)
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
		}



		Rent rent;
		decimal amount;
		DateTime paymentDate;

		public DateTime PaymentDate
		{
			get => paymentDate;
			set => SetPropertyValue(nameof(PaymentDate), ref paymentDate, value);
		}


		public decimal Amount
		{
			get => amount;
			set => SetPropertyValue(nameof(Amount), ref amount, value);
		}

		
		[Association("Rent-Payments")]
		public Rent Rent
		{
			get => rent;
			set => SetPropertyValue(nameof(Rent), ref rent, value);
		}






	}
}