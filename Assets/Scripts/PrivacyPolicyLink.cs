using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicyLink : MonoBehaviour
{
    string link = "https://github.com/Komstuch/Laser-Defender/blob/PrivacyPolicy/privacy_policy.md";

    public void OpenPolicy()
    {
        Application.OpenURL(link);
    }
}
