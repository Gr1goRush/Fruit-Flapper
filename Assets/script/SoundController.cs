using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Text musicText;
    public Text soundText;
    public Text vibeText;

    private bool musicEnabled = true;
    private bool soundEnabled = true;
    private bool vibeEnabled = true;

    UI uiscript;
    [SerializeField] GameObject uiScriptGm;
    private void Start()
    {
        uiscript = uiScriptGm.GetComponent<UI>();
        // Загрузка сохраненных настроек звука
        //if (PlayerPrefs.HasKey("MusicEnabled"))
        //{
        //    musicEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1;
        //}
        //if (PlayerPrefs.HasKey("SoundEnabled"))
        //{
        //    soundEnabled = PlayerPrefs.GetInt("SoundEnabled") == 1;
        //}
        //if (PlayerPrefs.HasKey("VibeEnabled"))
        //{
        //    vibeEnabled = PlayerPrefs.GetInt("VibeEnabled") == 1;
        //}

        // Установка текста на кнопках в соответствии с текущими настройками звука
        UpdateButtonText();
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            PlayerPrefs.SetInt("MusicEnabled", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MusicEnabled", 1);
        }

        UpdateButtonText();
        // Здесь добавьте код для включения/выключения музыки
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            PlayerPrefs.SetInt("SoundEnabled", 0);
        }
        else
        {
            PlayerPrefs.SetInt("SoundEnabled", 1);
        }

        UpdateButtonText();
        // Здесь добавьте код для включения/выключения звуков
    }

    public void ToggleVibe()
    {
        vibeEnabled = !vibeEnabled;
        PlayerPrefs.SetInt("VibeEnabled", vibeEnabled ? 1 : 0);
        UpdateButtonText();
        // Здесь добавьте код для включения/выключения вибрации
    }

    private void UpdateButtonText()
    {
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            musicText.text = "On";
        }
        else
        {
            musicText.text = "Off";
        }
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            soundText.text = "On";
        }
        else
        {
            soundText.text = "Off";
        }
        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            vibeText.text = "On";
        }
        else
        {
            vibeText.text = "Off";
        }


        //if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        //{
        //    uiscript.OnMusic();
        //}
        //else
        //{
        //    uiscript.OffMusic();
        //}
    }
}
