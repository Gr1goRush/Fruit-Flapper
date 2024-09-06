using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Sprite spritePrice;
    public Sprite spriteAvailable;
    public Sprite spriteActive;

    public Button[] characterButtons;
    public Button[] backgroundButtons;

    private int activeCharacterIndex = 0;
    private int activeBackgroundIndex = 0;

    public Text[] characterPrices;
    public Text[] backgroundPrices;

    AudioSource audio;
    [SerializeField] AudioClip BuySound,NoBuy;
    private void Start()
    {
        audio = GetComponent<AudioSource>();


        PlayerPrefs.SetInt("Character_0", 1);
        PlayerPrefs.SetInt("Background_0", 1);
        // ��������� ������ �� PlayerPrefs ��� ������ ����
        activeCharacterIndex = PlayerPrefs.GetInt("ActiveCharacter", 0);
        activeBackgroundIndex = PlayerPrefs.GetInt("ActiveBackground", 0);

        // ������������� ��������� ������� ��� ������
        UpdateButtonSprites();
    }
    public void Update()
    {
       // Debug.Log(PlayerPrefs.GetInt("ActiveBackground", 0));
    }

    private void UpdateButtonSprites()
    {
        // ������������� ������� � ���������� ������� ��� ������ ����������
        for (int i = 0; i < characterButtons.Length; i++)
        {
            if (i == activeCharacterIndex)
                characterButtons[i].image.sprite = spriteActive;
            else if (PlayerPrefs.GetInt("Character_" + i, 0) == 1)
            {
                characterButtons[i].image.sprite = spriteAvailable;
                characterPrices[i].gameObject.SetActive(false);
            }
            else
            {
                characterButtons[i].image.sprite = spritePrice;
                characterPrices[i].text = GetCharacterPrice(i).ToString();
                characterPrices[i].gameObject.SetActive(true);
            }
        }

        // ������������� ������� � ���������� ������� ��� ������ �����
        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            if (i == activeBackgroundIndex)
                backgroundButtons[i].image.sprite = spriteActive;
            else if (PlayerPrefs.GetInt("Background_" + i, 0) == 1)
            {
                backgroundButtons[i].image.sprite = spriteAvailable;
                backgroundPrices[i].gameObject.SetActive(false);
            }
            else
            {
                backgroundButtons[i].image.sprite = spritePrice;
                backgroundPrices[i].text = GetBackgroundPrice(i).ToString();
                backgroundPrices[i].gameObject.SetActive(true);
            }
        }
    }

    public void OnCharacterButtonClick(int index)
    {
        if (index == activeCharacterIndex)
            return; // ���� �������� ��� �������, ������ �� ������

        if (PlayerPrefs.GetInt("Character_" + index, 0) == 0)
        {
            // �������� ��������, ���� ��� ��� ���
          
            int characterPrice = GetCharacterPrice(index);
            if (PlayerPrefs.GetInt("money", 0) >= characterPrice)
            {
                if (PlayerPrefs.GetInt("sound") == 1)
                {
                    audio.PlayOneShot(BuySound);
                }
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) - characterPrice);
                PlayerPrefs.SetInt("Character_" + index, 1);
                activeCharacterIndex = index;
                PlayerPrefs.SetInt("ActiveCharacter", activeCharacterIndex);
                characterPrices[index].gameObject.SetActive(false); // �������� ����� ���� ����� �������
                UpdateButtonSprites();
            }
            else
            {
                Debug.Log("Not enough money to buy character!");
                if (PlayerPrefs.GetInt("sound") == 1)
                {
                    audio.PlayOneShot(NoBuy);
                }
            }
        }
        else
        {
            // ������������� �������� ��������� ��������
            activeCharacterIndex = index;
            PlayerPrefs.SetInt("ActiveCharacter", activeCharacterIndex);
            UpdateButtonSprites();
        }
    }

    public void OnBackgroundButtonClick(int index)
    {
        if (index == activeBackgroundIndex)
            return; // ���� ��� ��� �������, ������ �� ������

        if (PlayerPrefs.GetInt("Background_" + index, 0) == 0)
        {
            // �������� ���, ���� ��� ��� ���
            int backgroundPrice = GetBackgroundPrice(index);
            if (PlayerPrefs.GetInt("money", 0) >= backgroundPrice)
            {
                if (PlayerPrefs.GetInt("sound") == 1)
                {
                    audio.PlayOneShot(BuySound);
                }
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) - backgroundPrice);
                PlayerPrefs.SetInt("Background_" + index, 1);
                activeBackgroundIndex = index;
                PlayerPrefs.SetInt("ActiveBackground", activeBackgroundIndex);
                backgroundPrices[index].gameObject.SetActive(false); // �������� ����� ���� ����� �������
                UpdateButtonSprites();
            }
            else
            {
                Debug.Log("Not enough money to buy background!");
                if (PlayerPrefs.GetInt("sound") == 1)
                {
                    audio.PlayOneShot(NoBuy);
                }
            }
        }
        else
        {
            // ������������� �������� ��������� ���
            activeBackgroundIndex = index;
            PlayerPrefs.SetInt("ActiveBackground", activeBackgroundIndex);
            UpdateButtonSprites();
        }
    }

    private int GetCharacterPrice(int index)
    {
        switch (index)
        {
            case 0:
                return 0; // ����������� ���� ���������
            case 1:
                return 500; // ���� ��� ������� �����
            case 2:
                return 500; // ���� ��� �������� �����
            default:
                return 500; // �� ���������
        }
    }

    private int GetBackgroundPrice(int index)
    {
        switch (index)
        {
            case 0:
                return 0; // ����������� ��� ���������
            case 1:
                return 50; // ���� ��� ������� ����
            case 2:
                return 50; // ���� ��� �������� ����
            default:
                return 50; // �� ���������
        }
    }
    public void BuyExtraLive()
    {
        if (PlayerPrefs.GetInt("money", 0) >= 10)
        {
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) - 10);
            PlayerPrefs.SetInt("ExtraLive", PlayerPrefs.GetInt("ExtraLive") + 1);
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(BuySound);
            }
        }
        
    }
}
