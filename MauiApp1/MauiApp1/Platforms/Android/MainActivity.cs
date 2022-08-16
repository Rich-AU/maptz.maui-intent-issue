using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MauiApp1;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}


[Activity(Name = "mauiapp1.some.myshareactivity", Exported = true)]
[IntentFilter(new[] { "android.intent.action.SEND" }, Categories = new[] { "android.intent.category.DEFAULT" }, DataMimeType = "text/plain")]
public class ShareActivity : MauiAppCompatActivity
{

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		//https://github.com/dotnet/maui/issues/9090
		//Platform.Init(this, savedInstanceState);
		base.OnCreate(savedInstanceState);
		if (Intent.Type == "text/plain")
		{
			handleSendUrl();
		}
	}




	private void handleSendUrl()
	{
		var view = new LinearLayout(this) { Orientation = Orientation.Vertical };
		var es = Intent.Extras;
		var url = Intent.GetStringExtra("");

		var urlTextView = new TextView(this) { Gravity = GravityFlags.Center };
		urlTextView.Text = url;

		view.AddView(urlTextView);
		var description = new EditText(this) { Gravity = GravityFlags.Top };
		view.AddView(description);

		new AlertDialog.Builder(this)
				 .SetTitle("Save a URL Link")
				 .SetMessage("Type a description for your link")
				 .SetView(view)
				 .SetPositiveButton("Add", (dialog, whichButton) =>
				 {
					 var desc = description.Text;
					 //Save off the url and description here
					 //Remove dialog and navigate back to app or browser that shared                 
					 //the link
					 FinishAndRemoveTask();
					 FinishAffinity();
				 })
				 .Show();
	}
}
