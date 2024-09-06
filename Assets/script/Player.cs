using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;


public class Player : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    Animator animator;
    [SerializeField] Sprite[] FruitIcon;
    [SerializeField] GameObject[] lots;
    [SerializeField] GameObject ToAdd;
    AudioSource audio;
    [SerializeField] AudioClip  FruitSound, MoneyMusic, DropSound,sbor,herat;
    public string[] tagsToRemove;
    public string targetTag = "join"; // Тэг целевого предмета
    public float distanceThreshold = 0.2f; // Пороговое расстояние
    public float resizeValue = 0.6183925f; // Значение изменения размера персонажа
    private Transform target;
    [SerializeField] float accelerationFruit;
    public float borderPadding = 0.1f; // Расстояние от края экрана, на котором игрок может находиться

    private float minX, maxX, minY, maxY;
    private void Start()
    {
        // Определяем границы экрана в мировых координатах
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Учитываем borderPadding
        minX = minScreenBounds.x + borderPadding;
        maxX = maxScreenBounds.x - borderPadding;
        minY = minScreenBounds.y + borderPadding;
        maxY = maxScreenBounds.y - borderPadding;
        audio = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        
        animator = GetComponent<Animator>();
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        Rotate();
        if (isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;
            transform.position = targetPosition;
        }
       // Debug.Log(PlayerPrefs.GetInt("spwnLocate"));
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    void LateUpdate()
    {
        // Получаем текущую позицию игрока
        Vector3 clampedPosition = transform.position;

        // Ограничиваем позицию игрока внутри границ экрана
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX+0.2f, maxX-0.2f );
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY+0.2f , maxY-0.2f );

        // Устанавливаем новую позицию игрока
        transform.position = clampedPosition;
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "banan")
        {
            SwitchLotSprite(0);
            Destroy(collision.gameObject);

            animator.SetBool("fruit", true);

        }
        if (collision.gameObject.tag == "apple")
        {
            if (collision != null)
            {
                Destroy(collision.gameObject);
                SwitchLotSprite(1);

                animator.SetBool("fruit", true);
                collision.gameObject.SetActive(false);
            }



        }
        if (collision.gameObject.tag == "sliv")
        {
            SwitchLotSprite(2);
            Destroy(collision.gameObject);
            animator.SetBool("fruit", true);
        }
        if (collision.gameObject.tag == "klub")
        {
            SwitchLotSprite(3);
            Destroy(collision.gameObject);
            animator.SetBool("fruit", true);
        }
        if (collision.gameObject.tag == "heart")
        {
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(herat);
            }
            Destroy(collision.gameObject);
            animator.SetBool("fruit", true);
            PlayerPrefs.SetInt("ExtraLive", PlayerPrefs.GetInt("ExtraLive") + 1);
        }
        if (collision.gameObject.tag == "gnil")
        {
            Destroy(collision.gameObject);
            animator.SetBool("damage", true);
            OchistLots();
            MinusHp();

        }
        if (collision.gameObject.tag == "money")
        {
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 1);
            Destroy(collision.gameObject);
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(MoneyMusic);
            }

        }

    }
    public void MinusHp()
    {
        if (PlayerPrefs.GetInt("ExtraLive")<0)
        {
            PlayerPrefs.SetInt("ExtraLive", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ExtraLive", PlayerPrefs.GetInt("ExtraLive") - 1);
        }
      
    }
    public void SwitchLotSprite(int name)
    {
     
        if (lots[0].GetComponent<SpriteRenderer>().sprite == null)
        {
            lots[0].GetComponent<SpriteRenderer>().sprite = FruitIcon[name];
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(FruitSound);
            }

        }
        else if (lots[0].GetComponent<SpriteRenderer>().sprite == FruitIcon[name])
        {
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(FruitSound);
            }
            AddFruy(name);
        }
        //else if (lots[2].GetComponent<SpriteRenderer>().sprite == null && lots[1].GetComponent<SpriteRenderer>().sprite == FruitIcon[name])
        //{
        //    lots[2].GetComponent<SpriteRenderer>().sprite = FruitIcon[name];
        //    OchistLots();
        //    PlayerPrefs.SetInt("ExtraLive", PlayerPrefs.GetInt("ExtraLive") + 1);
        //    animator.SetBool("eat", true);
        //    AddFruy();
        //}
        else
        {
           
            OchistLots();
            MinusHp();
        }
    }
    public void OchistLots()
    {
        PlayerPrefs.SetInt("spwnLocate", 1);
        lots[0].GetComponent<SpriteRenderer>().sprite = null;
        Transform parentTransform = transform;
        if (PlayerPrefs.GetInt("vibr") == 1)
        {
            Handheld.Vibrate();
        }
        // Перебираем всех дочерних объектов текущего объекта
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            // Получаем ссылку на очередного дочернего объекта
            Transform child = parentTransform.GetChild(i);

            // Уничтожаем дочерний объект
            if (child.gameObject.tag!="lot")
            {
                Destroy(child.gameObject);
            }
          
        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("join");

        // Проходим по массиву найденных объектов и удаляем их
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            audio.PlayOneShot(DropSound);
        }
    }
    public void OchistLotsWithOutVol()
    {
        PlayerPrefs.SetInt("spwnLocate", 1);
        lots[0].GetComponent<SpriteRenderer>().sprite = null;
        Transform parentTransform = transform;
        if (PlayerPrefs.GetInt("vibr") == 1)
        {
            Handheld.Vibrate();
        }
        // Перебираем всех дочерних объектов текущего объекта
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            // Получаем ссылку на очередного дочернего объекта
            Transform child = parentTransform.GetChild(i);

            // Уничтожаем дочерний объект
            if (child.gameObject.tag != "lot")
            {
                Destroy(child.gameObject);
            }

        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("join");

        // Проходим по массиву найденных объектов и удаляем их
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
      
    }

    public void StopFruitAnimation()
    {
        animator.SetBool("fruit", false);
    }
    public void StopDamageAnimation()
    {
        animator.SetBool("damage", false);
    }
    public void StopEatAnimation()
    {
        animator.SetBool("eat", false);
    }
    public void AddFruy(int name)
    {

        if (PlayerPrefs.GetInt("spwnLocate")>=10)
        {
            PlayerPrefs.SetFloat("acceleration", PlayerPrefs.GetFloat("acceleration")+accelerationFruit);
            animator.SetBool("eat", true);
            PlayerPrefs.SetInt("spwnLocate",1);
            PlayerPrefs.SetInt("spwnLocate", 1);
            lots[0].GetComponent<SpriteRenderer>().sprite = null;
            Transform parentTransform = transform;
            // Перебираем всех дочерних объектов текущего объекта
            for (int i = parentTransform.childCount - 1; i >= 0; i--)
            {
                // Получаем ссылку на очередного дочернего объекта
                Transform child = parentTransform.GetChild(i);

                // Уничтожаем дочерний объект
                if (child.gameObject.tag != "lot")
                {
                    Destroy(child.gameObject);
                }

            }
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("join");

            // Проходим по массиву найденных объектов и удаляем их
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                audio.PlayOneShot(sbor);
            }
        }
        else
        {
            GameObject childObject = Instantiate(ToAdd, new Vector3(transform.position.x, transform.position.y - PlayerPrefs.GetInt("spwnLocate") * 0.6f, transform.position.z), Quaternion.identity);
            PlayerPrefs.SetInt("spwnLocate", PlayerPrefs.GetInt("spwnLocate") + 1);
            // Делаем созданный объект дочерним по отношению к текущему объекту
            // childObject.transform.parent = transform;
            childObject.GetComponent<SpriteRenderer>().sprite = FruitIcon[name];
            childObject.tag = "join";
            childObject.AddComponent<fruitList>();
        }
           
        
    }

    public void Rotate()
    {
        FindNearestTarget();
        if (target != null)
        {
            // Вычисляем расстояние между игроком и целевым объектом
            float distance = target.position.x - transform.position.x;

            // Проверяем, находится ли целевой объект левее
            if (distance < 0)
            {
                // Изменяем размер персонажа
                ResizeCharacter(-0.6183925f);
            }
            // Проверяем, находится ли целевой объект правее
            else if (distance > 0)
            {
                // Изменяем размер персонажа
                ResizeCharacter(0.6183925f);
            }
        }
    }
    void ResizeCharacter(float scale)
    {
        // Изменяем масштаб по оси X персонажа
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    // Метод для поиска ближайшего объекта с заданным тэгом
    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = obj.transform;
            }
        }
    }
}


