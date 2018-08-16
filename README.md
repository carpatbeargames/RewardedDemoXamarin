1.	In Android Project add Xamarin.GooglePlayServices.Ads(Add it using NuGet)

2.	In Forms project add a new interface:

  public interface IViewAd
    {
         void ShowAd(Demo demoPage);
    } 

3.	In Android project create a new class:

[assembly: Xamarin.Forms.Dependency(typeof(RewardedAd))]
namespace RewardedDemo.Droid
{
    
    public class RewardedAd:AppCompatActivity, IViewAd, IRewardedVideoAdListener
    {
        //create new ad
        IRewardedVideoAd videoAd;
        //This is the page in Forms project where you have methods for view ad, reward user, display error message, etc...
        Demo originalPage = new Demo();
        public RewardedAd()
        {
            //Set the ad
            videoAd = MobileAds.GetRewardedVideoAdInstance(Android.App.Application.Context);
        }

       

        public void ShowAd(Demo demoPage)
        {
            //set the page as the parsed page from Forms
            originalPage = demoPage;
            LoadAd();
            if (videoAd.IsLoaded)
                videoAd.Show();

        }
        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            //Replace the TestAdId with your ID
            videoAd.LoadAd("ca-app-pub-3940256099942544/5224354917", requestbuilder.Build());
            //Set the listener to know when the ad is completed, closed, loaded, etc...
            videoAd.RewardedVideoAdListener = this;
        }
        public void OnRewarded(IRewardItem reward)
        {
            // Actions when the user shoud receive the reward
            //Call a method from originalPage(the page from Forms project)
            originalPage.RewardUser(reward.Amount);
        }

        public void OnRewardedVideoAdClosed()
        {
            //Actions when the ad is closed
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            //Actions when the ad failed to load. I.E. display an error message
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            //Actions when the app is left
        }

        public void OnRewardedVideoAdLoaded()
        {
            //Actions when the ad is loaded
            videoAd.Show();
        }

        public void OnRewardedVideoAdOpened()
        {
            //Actions when the ad is opened
        }

        public void OnRewardedVideoStarted()
        {
            //Actions when the ad is started
        }
    }
}


4.	In Forms project create a class with methods for view ad, reward user, display error message, etc:

namespace RewardedDemo
{
    public partial class Demo : ContentPage
    {
       
        int views = 0;
        IViewAd rewardedAd;
        public Demo()
        {
            InitializeComponent();
           

            labelViews.Text = "Rewards: " + views.ToString();

           
        }
      
        public void RewardUser(int rewardAmount)
        {
            views = views + rewardAmount;   
            labelViews.Text = "Rewards: " + views.ToString();
        }
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (rewardedAd == null)
            {
                //Set the interfase as dependency
                rewardedAd = DependencyService.Get<IViewAd>();
            }
            rewardedAd.ShowAd(this);
        }


    }
}


Tips:
-	Always update the nugget packages
-	Viewing ads working better with a real app Id

Now you have rewarded ads.

If you want to say thanks, play my games:
https://play.google.com/store/apps/developer?id=CarpatBearGames



