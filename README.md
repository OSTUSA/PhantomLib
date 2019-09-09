# PhantomLib
Collection of Xamarin additions that are ready to consume.

# Effects included in this library
* Kerning Effect - Allows you to specify the letter spacing of your labels.
* Spinner Effect - Allows you to have a custom activity indicator. Just supply an image and duration (how long it takes for one rotation) and the effect will rotate it.

# Custom controls in this library
* RoundedFrame - Allows you to specify which corners of a frame are rounded.
* UltimateEntry - Entry control jam packed with functionality. You can set an icon on the right that will clear the contents when tapped. You can use it as a password entry and set an icon to show and hide the password by tapping it. You can have it validate input and show an error when validation fails. And you can even round the corners!

# Behaviors included in this library
* Tap Command Behavior - Allows you to bind an `ICommand` to be executed when a control is tapped.

# Converters included in this library
* CharacterCountConverter - Counts the characters in a string
* InverseBoolConverter - Inverses Boolean values
* IsMinimumCharacterCountConverter - Counts the characters in a string and determines if it meets a minimum count
* IsMinimumValueConverter - Determines if a number meets a minimum count
* IsNullConverter - Determines if a value is null
* IsNullOrWhitespace - Determines if a string value is null or whitespace
* IsNotNullConverter - Determines if a value is not null (inverse of IsNullConverter)
* IsNotNullOrWhitespace - Determines if a string value is not null or whitespace (inverse of IsNullOrWhitespaceConverter)
* StringFormatConverter - Safely formats strings (handles case where format string is not a valid .NET format string)
* ToUpperConverter - Uppercases each character in a string (culture-specific)

# Attached Properties in this library
* Labels.Kerning - Helper to easily add Kerning Effect
* Views.TapBackgroundColor - Set the temporary background color of a view when it is tapped
* Pages.FloatingActionButton - Add a Button to the page as a floating action button.

# Other helpers included in this library
* BaseAttachable - Acts as a base class for view-models, with helpers to easily raise `IPropertyChanged` events for properties.

# Installation
You must make a call to initialize after `Forms.Init` and before `LoadApplication` in order to use the effects from this library in `AppDelegate.cs` (iOS) and/or `MainActivity.cs` (Android).

**Android:**
  ```
global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
PhantomLib.Android.Effects.Effects.Init();
LoadApplication(new App());
```
**iOS:**
```
LoadApplication(new  App());  
PhantomLib.iOS.Effects.Effects.Init();  
return base.FinishedLaunching(app,  options);
```

# Sample
![Sample Image](Images/sample4.gif)

