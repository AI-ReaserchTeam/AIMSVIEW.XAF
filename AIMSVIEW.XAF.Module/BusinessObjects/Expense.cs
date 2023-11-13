using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;

namespace AIMSVIEW.XAF.Module.BusinessObjects
{

	[NavigationItem("Projects")]
	[DefaultClassOptions]
	public class Expense : BaseObject
	{

		DateTime createdOn;
		ApplicationUser createdBy;
		Project project;
		decimal? amount;
		PaymentMethod paymentMethod;
		string payedBy;
		string payedTo;
		double quantity;
		double rate;
		string description;
		string item;


		[RuleRequiredField]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Item
		{
			get => item;
			set => SetPropertyValue(nameof(Item), ref item, value);
		}
		[VisibleInListView(false)]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Description
		{
			get => description;
			set => SetPropertyValue(nameof(Description), ref description, value);
		}
		[VisibleInListView(false)]
		public double Rate
		{
			get => rate;
			set => SetPropertyValue(nameof(Rate), ref rate, value);
		}
		[VisibleInListView(false)]
		public double Quantity
		{
			get => quantity;
			set => SetPropertyValue(nameof(Quantity), ref quantity, value);
		}

		[VisibleInListView(false)]
		public decimal TotalCost => (decimal)Quantity * (decimal)Rate;

		[RuleRequiredField]
		[XafDisplayName("PayedTo")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string PayedTo
		{
			get => payedTo;
			set => SetPropertyValue(nameof(PayedTo), ref payedTo, value);
		}
		[RuleRequiredField]
		[XafDisplayName("PayedBy")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string PayedBy
		{
			get => payedBy;
			set => SetPropertyValue(nameof(PayedBy), ref payedBy, value);
		}
		
		[VisibleInListView(false)]
		public PaymentMethod PaymentMethod
		{
			get => paymentMethod;
			set => SetPropertyValue(nameof(PaymentMethod), ref paymentMethod, value);
		}
		[RuleRequiredField]
		public decimal? Amount
		{
			get => amount;
			set => SetPropertyValue(nameof(Amount), ref amount, value);
		}

		[Association("Project-Expences")]
		public Project Project
		{
			get => project;
			set => SetPropertyValue(nameof(Project), ref project, value);
		}

		[ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
		public ApplicationUser CreatedBy
		{
			get => createdBy;
			set => SetPropertyValue(nameof(CreatedBy), ref createdBy, value);
		}

		[ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
		[ModelDefault(nameof(IModelCommonMemberViewItem.DisplayFormat), "G")]
		public DateTime CreatedOn
		{
			get => createdOn;
			set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
		}



		public Expense(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			createdOn = DateTime.Now;
			createdBy = GetCurrentUser();
		}
		protected override void OnSaving()
		{
			base.OnSaving();
			if (Session.IsNewObject(this)) {
				createdOn = DateTime.Now;
				createdBy = GetCurrentUser();
			}
		}
		ApplicationUser GetCurrentUser()
		{
			return Session.GetObjectByKey<ApplicationUser>(Session.ServiceProvider.GetRequiredService<ISecurityStrategyBase>().UserId);
		} 

		


	}

	public enum PaymentMethod
	{
		Cash,
		Cheque,
		Online,
		Proxy
	}
}