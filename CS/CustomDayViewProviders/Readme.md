# Day View - Native Platform Views for Date Headers and Cells
This example demonstrates how to specify custom native Android and iOS views for date headers and cells of the [Day View](https://docs.devexpress.com/MobileControls/400677/xamarin-forms/scheduler/views/views#day-view). To do this, implement *view providers* and register these implementations in each platform project.

## Android 
1. Create the [CustomDateHeaderViewProvider.cs](./CustomDayViewProviders.Android/CustomViewProviders/CustomDateHeaderViewProvider.cs) and [CustomDayCellViewProvider.cs](./CustomDayViewProviders.Android/CustomViewProviders/CustomDayCellViewProvider.cs) classes that define custom view providers for date headers and cells. A view provider class should implement the **IViewProvider** or **ICachedViewProvider** (for views with many elements) interface. 
    - **CustomDateHeaderViewProvider** - implements the **IViewProvider** interface.  
    The **CreateNewView** method creates a [View](https://docs.microsoft.com/en-us/dotnet/api/android.views.view?view=xamarin-android-sdk-9) object, and the **BindView** method binds this view to data.
    - **CustomDayCellViewProvider** - implements the **ICachedViewProvider** interface.  
    This class contains methods that reduce a view's creation and load time: 
        - **RequestViewFromCache** - before creating a view, checks whether the cache already contains it. If it does, it loads the existing view. If a view cannot be returned from the cache, the **CreateNewView** method creates a new view (an instance of the [CustomCell.cs](./CustomDayViewProviders.Android/CustomViews/CustomCell.cs) class that inherits [View](https://docs.microsoft.com/en-us/dotnet/api/android.views.view?view=xamarin-android-sdk-9)).
        - **RecycleView** - adds a view to the cache.
        - **Recycle** - clears the cache.

2. In the [MainActivity.cs](./CustomDayViewProviders.Android/MainActivity.cs) file, register a custom service that inherits the **DayViewProviderService** class and override the **CreateCellViewProvider** and **CreateDateHeaderViewProvider** methods to use custom view providers for date headers and cells.

## iOS
1. Create the [CustomDateHeaderViewProvider.cs](./CustomDayViewProviders.iOS/CustomViewProviders/CustomDateHeaderViewProvider.cs) and [CustomCellViewProvider.cs](./CustomDayViewProviders.iOS/CustomViewProviders/CustomCellViewProvider.cs) classes that implement the **IViewProvider** interface and define custom view providers for date headers and cells. 
    - **CustomDateHeaderViewProvider**  
    The **RequestView** method returns a new view (an instance of the [CustomDateHeader.cs](./CustomDayViewProviders.iOS/CustomViews/CustomDateHeader.cs) class that inherits [UIView](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiview?view=xamarin-ios-sdk-12)), and the **BindView** method binds this view to data. 
    - **CustomDayCellViewProvider**  
    This class has the following methods:
        - **RequestView** - returns a view from the cache, if any, otherwise creates a new view (an instance of the [CustomCell.cs](./CustomDayViewProviders.iOS/CustomViews/CustomCell.cs) class that inherits  [UIView](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiview?view=xamarin-ios-sdk-12)).
        - **RecycleView** - adds a view to the cache.

2. In the [AppDelegate.cs](./CustomDayViewProviders.iOS/AppDelegate.cs) file, register a custom service that inherits the **DayViewProviderService** class and override the **CreateCellViewProvider** and **CreateDateHeaderViewProvider** methods to use custom view providers for date headers and cells.

Note that the **DayViewProviderService** class has also methods that allow you to specify custom view providers for other Day View elements: 
- **CreateAppointmentViewProvider**
- **CreateAllDayAppointmentViewProvider**
- **CreateAllDayCellViewProvider** 
- **CreateTimeRulerCellViewProvider**
- **CreateTimeRulerHeaderViewProvider** 


To run the application:
1. [Obtain your NuGet feed URL](http://docs.devexpress.com/GeneralInformation/116042/installation/install-devexpress-controls-using-nuget-packages/obtain-your-nuget-feed-url).
2. Register the DevExpress NuGet feed as a package source.
3. Restore all NuGet packages for the solution.
