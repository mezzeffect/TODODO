using System;
using Xamarin.UITest.Queries;

namespace TodoSampleMobile.UITest
{
	public interface IHomeScreen
	{
		Func<AppQuery, AppQuery> addButton {get;}
	}	
}