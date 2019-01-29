using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    private string gameID = "2997851";
    [SerializeField] GameObject errorDot;

    private void Start()
    {
        Advertisement.Initialize(gameID, false);
    }
    private void Update()
    {
        errorDot.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Advertisement.IsReady("banner"))
            {
                Advertisement.Show("banner");
            }   else { errorDot.SetActive(true); }
        } 
    }
}
