using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class adMob : MonoBehaviour {

    private BannerView bannerView;

    public void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-";
#elif UNITY_IPHONE
                        string appId = "ca-app-pub-";
#else
                        string appId = "unexpected_platform";
#endif

        MobileAds.Initialize(appId);

        this.RequestBanner();

    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-";
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            //.AddTestDevice("")
            .Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    void OnDestroy()
    {
        bannerView.Destroy();
    }
}
