To use any of the effects, you must include a call to init in your AppDelegate(iOS) and MainActivity(Android)
in order for them to work.

Android:

global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
PhantomLib.Android.Effects.Effects.Init();
LoadApplication(new App());

iOS:

LoadApplication(new  App());  
PhantomLib.iOS.Effects.Effects.Init();  
return base.FinishedLaunching(app,  options);
