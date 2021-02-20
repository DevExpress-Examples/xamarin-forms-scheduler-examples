# Month View - Native Platform Views for Cells and Appointments
This example demonstrates how to specify custom native Android and iOS views for cells and appointments of the [Month View](https://docs.devexpress.com/MobileControls/400677/xamarin-forms/scheduler/views/views#month-view). To do this, implement *view providers* and register these implementations in each platform project.

## Android 
1. Create the [CustomCellViewProvider.cs](./CustomMonthViewProviders.Android/CustomViewProviders/CustomCellViewProvider.cs) and [CustomAppointmentViewProvider.cs](./CustomMonthViewProviders.Android/CustomViewProviders/CustomAppointmentViewProvider.cs) classes that implement the **ICachedViewProvider** interface and define custom view providers for cells and appointments. These classes contain methods that reduce a view's creation and load time: 
	- **RequestViewFromCache** - before creating a view, checks whether the cache already contains it. If it does, it loads the existing view. If a view cannot be returned from the cache, the **CreateNewView** method creates a new view (an instance of the [CustomCellView.cs](./CustomMonthViewProviders.Android/CustomViews/CustomCellView.cs) or [CustomAppointmentView.cs](./CustomMonthViewProviders.Android/CustomViews/CustomAppointmentView.cs) class that inherits [ViewGroup](https://docs.microsoft.com/en-us/dotnet/api/android.views.viewgroup?view=xamarin-android-sdk-9)).
	- **BindView** - binds a view to data.
        - **RecycleView** - adds a view to the cache.
        - **Recycle** - clears the cache.

2. In the [MainActivity.cs](./CustomMonthViewProviders.Android/MainActivity.cs) file, register a custom service that inherits the **MonthViewProviderService** class and override the **CreateCellViewProvider** and **CreateAppointmentViewProvider** methods to use custom view providers for cells and appointments.

## iOS
1. Create the [CustomCellViewProvider.cs](./CustomMonthViewProviders.iOS/CustomViewProviders/CustomCellViewProvider.cs) and [CustomAppointmentViewProvider.cs](./CustomMonthViewProviders.iOS/CustomViewProviders/CustomAppointmentViewProvider.cs) classes that implement the **IViewProvider** interface and define custom view providers for cells and appointments. These classes have the following methods:
	- **RequestView** - returns a view from the cache, if any, otherwise creates a new view (an instance of the [CustomCellView.cs](./CustomMonthViewProviders.iOS/CustomViews/CustomCellView.cs) or [CustomAppointmentView.cs](./CustomMonthViewProviders.iOS/CustomViews/CustomAppointmentView.cs) class that inherits [UIView](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiview?view=xamarin-ios-sdk-12)).
	- **BindView** - binds a view to data.
        - **RecycleView** - adds a view to the cache.

2. In the [AppDelegate.cs](./CustomMonthViewProviders.iOS/AppDelegate.cs) file, register a custom service that inherits the **MonthViewProviderService** class and override the **CreateCellViewProvide** and **CreateAppointmentViewProvider** methods to use custom view providers for cells and appointments.


To run the application:
1. [Obtain your NuGet feed URL](http://docs.devexpress.com/GeneralInformation/116042/installation/install-devexpress-controls-using-nuget-packages/obtain-your-nuget-feed-url).
2. Register the DevExpress NuGet feed as a package source.
3. Restore all NuGet packages for the solution.
