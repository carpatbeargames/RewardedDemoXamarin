using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
