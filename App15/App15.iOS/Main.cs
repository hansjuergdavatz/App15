using UIKit;

namespace App15.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}

    //TODO hjd diese stehr hier dami man es da einbauen kann wo es verwendet wird.

    //public void ShowToast(String message, UIView view)
    //{
    //  UIView residualView = view.ViewWithTag(1989);
    //  if (residualView != null)
    //    residualView.RemoveFromSuperview();

    //  var viewBack = new UIView(new CoreGraphics.CGRect(83, 0, 300, 100));
    //  viewBack.BackgroundColor = UIColor.Black;
    //  viewBack.Tag = 1989;
    //  UILabel lblMsg = new UILabel(new CoreGraphics.CGRect(0, 20, 300, 60));
    //  lblMsg.Lines = 2;
    //  lblMsg.Text = message;
    //  lblMsg.TextColor = UIColor.White;
    //  lblMsg.TextAlignment = UITextAlignment.Center;
    //  viewBack.Center = view.Center;
    //  viewBack.AddSubview(lblMsg);
    //  view.AddSubview(viewBack);
    //  roundtheCorner(viewBack);
    //  UIView.BeginAnimations("Toast");
    //  UIView.SetAnimationDuration(3.0f);
    //  viewBack.Alpha = 0.0f;
    //  UIView.CommitAnimations();
    //}

  }
}
