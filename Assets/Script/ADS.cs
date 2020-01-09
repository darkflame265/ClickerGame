////////////RewardedAdsManger///////////
using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.Advertisements; 

public class ADS : MonoBehaviour 
{
    public Text coinText;
    private int coin = 0; 

    // 광고 보여주기
    public void ShowAD()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
            DataController.Instance.diamond += 500;
            CharacterStateController.Instance.change_skill_in_shop();
        }
    }

    //보상형 광고 보여주기
    public void ShowReward()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    //광고 실행 후 결과
    void ResultAds(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Failed:
                Debug.Log("failed");
                break;
            case ShowResult.Skipped:
                //DataController.Instance.diamond += 2000;
                break;
            case ShowResult.Finished:
                DataController.Instance.diamond += 2000;
                SummonSystem.Instance.epic_summon();
                Debug.Log("success");
                break;
        }
    }
}