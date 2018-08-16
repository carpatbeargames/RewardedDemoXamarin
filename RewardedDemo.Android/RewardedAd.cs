using System;
using Android.App;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Android.Support.V7.App;
using RewardedDemo.Droid;

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
