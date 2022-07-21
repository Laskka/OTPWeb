using OneTwoPlay.Web.Ads;
using UnityEngine;

public class StartPreRoll : MonoBehaviour
{
    public void PlayPreRoll()
    {
        ADSManager.Instance.ShowPreRoll();
    }
}
