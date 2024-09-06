using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject MainMenu, Settings, Shop, GameOver, GameUI,HowToPlay,Generator,PauseUI,blestki;
    [SerializeField] Text ExtraLiveText, ExtraLiveShopText, MainScoreText,TotalScore,YouEarn,MoneyText;
    [SerializeField] GameObject[] Player;
    float score;
    bool play=false;
    public string[] tagsToRemove;
    [SerializeField] Sprite[] Fons;
    [SerializeField] Image Fon;
    [SerializeField] Sprite Off,On;
    [SerializeField] Button MusicB, SoundB, VibrB;
    AudioSource audio;
    [SerializeField] AudioClip buttonSound, gameMusic, gameOverSound, MenuMusic, FruitSound, MoneyMusic, BuySound, DropSound;
    public string[] tagsToDeactivate;
    [SerializeField] GameObject[] joins;
    Player _player;
    
    void Start()
    {
        
        audio = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("sound",1);
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.SetInt("vibr", 1);
        _player = Player[0].GetComponent<Player>();
        _player = Player[1].GetComponent<Player>();
        _player = Player[2].GetComponent<Player>();
        if (PlayerPrefs.GetInt("music") == 1)
        {
            audio.clip = MenuMusic;
            audio.Play();
        }
        else
        {
            audio.clip = null ;
            audio.Play();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        ExtraLiveText.text = PlayerPrefs.GetInt("ExtraLive")+"";
        ExtraLiveShopText.text = PlayerPrefs.GetInt("ExtraLive") + "";
        if (PlayerPrefs.GetInt("ExtraLive") <= 0&&play)
        {
            RemoveObjectsWithTags();
            Player[0].SetActive(false);
            Player[1].SetActive(false);
            Player[2].SetActive(false);
            Generator.SetActive(false);

           
            GameOver.SetActive(true);
           // pauseButton.SetActive(false);
            play = false;
            TotalScore.text = (int)score+"";
            YouEarn.text = (int)score + "";
           // PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+(int)score);
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(gameOverSound);
            }
            if (PlayerPrefs.GetInt("music") == 1)
            {
                audio.clip = MenuMusic;
                audio.Play();
            }
            else
            {
                audio.clip = null;
                audio.Play();
            }
        }
        if (play)
        {
            score += Time.deltaTime /2;
            MainScoreText.text = "" + (int)score;
        }
        MoneyText.text = PlayerPrefs.GetInt("money")+"";
        switch (PlayerPrefs.GetInt("ActiveBackground", 0))
        {
            case 0:
                Fon.GetComponent<Image>().sprite = Fons[0];
                break;
            case 1:
                Fon.GetComponent<Image>().sprite = Fons[1];
                break;
            case 2:
                Fon.GetComponent<Image>().sprite = Fons[2];
                break;
            default:
                break;
        }
        //Debug.Log(PlayerPrefs.GetInt("sound")+" "+ PlayerPrefs.GetInt("music")+" "+ PlayerPrefs.GetInt("vibr"));
    
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        Player[PlayerPrefs.GetInt("ActiveCharacter", 0)].SetActive(false);
       joins = GameObject.FindGameObjectsWithTag("join");
        for (int i = 0; i < joins.Length; i++)
        {
            joins[i].SetActive(false);
        }
        Generator.SetActive(false);
        foreach (string tag in tagsToDeactivate)
        {
            // Получаем массив всех игровых объектов с данным тегом
           GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            // Деактивируем каждый объект
            foreach (GameObject obj in objectsWithTag)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
        }



    }
    public void Prodol()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        Generator.SetActive(true);
        Player[PlayerPrefs.GetInt("ActiveCharacter")].SetActive(true);
        //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < joins.Length; i++)
        {
            joins[i].SetActive(true);
        }
        joins = null;
        foreach (string tag in tagsToDeactivate)
        {
            // Получаем массив всех игровых объектов с данным тегом
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            // Деактивируем каждый объект
            foreach (GameObject obj in objectsWithTag)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }

    }
    public void ToMenuAfterPause()
    {
        blestki.SetActive(true);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
        RemoveObjectsWithTags();
        Player[0].SetActive(false);
        Player[1].SetActive(false);
        Player[2].SetActive(false);
        Generator.SetActive(false);
        BackToMenu();
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }
    public void Play()
    {
        blestki.SetActive(false);
        Player[0].transform.position = new Vector2(0,0);
        Player[1].transform.position = new Vector2(0, 0);
        Player[2].transform.position = new Vector2(0, 0);
       // pauseButton.SetActive(true);
        if (PlayerPrefs.GetInt("music") == 1)
        {
            audio.clip = gameMusic ;
            audio.Play();
        }
        else
        {
            audio.clip = null;
            audio.Play();
        }
        PlayerPrefs.SetFloat("acceleration", 0);
        play = true;
        score = 0;
        Ochist();
        GameUI.SetActive(true);
        Player[PlayerPrefs.GetInt("ActiveCharacter", 0)].SetActive(true);
        Generator.SetActive(true);
        PlayerPrefs.SetInt("spwnLocate",1);
       
            if (PlayerPrefs.GetInt("ExtraLive")<=0)
        {
            PlayerPrefs.SetInt("ExtraLive", 1);
        }

        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }
    public void PlayAgain()
    {
        blestki.SetActive(false);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
        BackToMenu();
        Play();
        Time.timeScale = 1;
        Player[PlayerPrefs.GetInt("ActiveCharacter")].GetComponent<Player>().OchistLotsWithOutVol();
        
    }
    public void Ochist()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(false);
        Shop.SetActive(false);
        GameOver.SetActive(false);
        GameUI.SetActive(false);
        HowToPlay.SetActive(false);
        Player[0].SetActive(false);
        Player[1].SetActive(false);
        Player[2].SetActive(false);
        Generator.SetActive(false);
        Generator.SetActive(false);
        PauseUI.SetActive(false);
    }
    public void BackToMenu()
    {
        blestki.SetActive(true);
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        joins = GameObject.FindGameObjectsWithTag("join");
        for (int i = 0; i < joins.Length; i++)
        {
            if (joins[i]!=null)
            {
                Destroy(joins[i]);
            }
            
        }
        if (PlayerPrefs.GetInt("ExtraLive") <= 0)
        {
            PlayerPrefs.SetInt("ExtraLive", 1);
        }
        Ochist();
        MainMenu.SetActive(true);
        RemoveObjectsWithTags();
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }
    public void BackToMenuPause()
    {
        blestki.SetActive(true);
        if (PlayerPrefs.GetInt("music") == 1)
        {
            audio.clip = MenuMusic;
            audio.Play();
        }
        else
        {
            audio.clip = null;
            audio.Play();
        }
        Player[PlayerPrefs.GetInt("ActiveCharacter")].GetComponent<Player>().OchistLots();
        Time.timeScale = 1;
        PauseUI.SetActive(false);

        for (int i = 0; i < joins.Length; i++)
        {
            Destroy(joins[i]);
        }
        if (PlayerPrefs.GetInt("ExtraLive") <= 0)
        {
            PlayerPrefs.SetInt("ExtraLive", 1);
        }
        Ochist();
        MainMenu.SetActive(true);
        RemoveObjectsWithTags();
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }
    public void ToShop()
    {
        Ochist();
        Shop.SetActive(true);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }
    public void ToSettings()
    {
        Ochist();
        Settings.SetActive(true);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }
    public void ToHowToPlay()
    {
        Ochist();
        HowToPlay.SetActive(true);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(buttonSound);
        }
    }

 
    public void RemoveObjectsWithTags()
    {
        // Проходим по каждому тегу в массиве
        foreach (string tagToRemove in tagsToRemove)
        {
            // Находим все объекты на сцене с заданным тегом
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToRemove);

            // Проходим по каждому найденному объекту и удаляем его
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
    }
    public void SoundButton()
    {
        if (PlayerPrefs.GetInt("sound")==1)
        {
            PlayerPrefs.SetInt("sound", 0);
            SoundB.GetComponent<Image>().sprite = Off;
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            SoundB.GetComponent<Image>().sprite = On;
        }
    }
    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            MusicB.GetComponent<Image>().sprite = Off;
            audio.clip = null;
            audio.Play();
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            MusicB.GetComponent<Image>().sprite = On;
            audio.clip = MenuMusic;
            audio.Play();
        }
    }
    public void VibrButton()
    {
        if (PlayerPrefs.GetInt("vibr") == 1)
        {
            PlayerPrefs.SetInt("vibr", 0);
            VibrB.GetComponent<Image>().sprite = Off;
        }
        else
        {
            PlayerPrefs.SetInt("vibr", 1);
           VibrB.GetComponent<Image>().sprite = On;
        }
       
    }
}
