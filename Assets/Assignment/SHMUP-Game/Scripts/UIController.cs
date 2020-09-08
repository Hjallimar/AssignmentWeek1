using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance { get { return instance; } }

    [Header("The diferent main panels")]
    [SerializeField] private GameObject gameUIPanel = null;
    [SerializeField] private GameObject menuUIPanel = null;

    [Header("Upper part of Game UI")]
    [SerializeField] private Text playerHealth = null;
    [SerializeField] private Text playerScore = null;
    [SerializeField] private Text bossName = null;
    [SerializeField] private Slider bossSlider = null;

    [Header("Lower part of Game UI")]
    [SerializeField] private Image shieldCooldown = null;
    [SerializeField] private Image[] weaponSlots = new Image[3];

    bool shieldOnCD = false;
    float durationCD = 0f;

    float currentHealth;
    float currentScore;
    float bossHealth;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Instance.bossSlider.gameObject.SetActive(false);
        Instance.bossName.text = "";
    }

    public static void ChangeWeapon(int i)
    {
        Sprite spriteHolder;
        if( i > 0)
        {
            int lastSprite = Instance.weaponSlots.Length - 1;
            spriteHolder = Instance.weaponSlots[0].sprite;

            Debug.Log("SpriteHolder is  " + 0);
            for (int j = 0; j <= lastSprite; j++)
            {
                if (j + 1 >= lastSprite)
                {
                    Debug.Log(j + " is now " + (j+1));
                    Instance.weaponSlots[j].sprite = Instance.weaponSlots[j + 1].sprite;
                }
                else
                {
                    Debug.Log("Swapping " + j + " and " + 0);
                    Instance.weaponSlots[j].sprite = spriteHolder;
                }
            }
        }
        else
        {
            int lastSprite = Instance.weaponSlots.Length - 1;
            spriteHolder = Instance.weaponSlots[lastSprite].sprite;
            Debug.Log("SpriteHolder is  " + lastSprite);
            for (int j = lastSprite; j >= 0; j--)
            {
                if (j - 1 >= 0)
                {
                    Debug.Log("Swapping " + j + " and " + (j - 1));
                    Instance.weaponSlots[j].sprite = Instance.weaponSlots[j - 1].sprite;
                }
                else
                {
                    Debug.Log("Swapping " + j + " and " + lastSprite);
                    Instance.weaponSlots[j].sprite = spriteHolder;
                }
            }
        }
    }

    public static void ShieldActivated(float duration)
    {
        if (!Instance.shieldOnCD)
        {
            Instance.durationCD = duration;
            Instance.StartCoroutine(StartShieldCooldown());
        }
    }

    private static IEnumerator StartShieldCooldown()
    {

        Instance.shieldOnCD = true;
        float fillAmount = 1;
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime / Instance.durationCD;
            Instance.shieldCooldown.fillAmount = fillAmount;
            yield return new WaitForEndOfFrame();
        }
        Instance.shieldOnCD = false;
    }

    public static void UpdatePlayerHealth(float damage)
    {
        Instance.currentHealth -= damage;
        Instance.playerHealth.text = Instance.currentHealth.ToString();
    }

    public static void UpdatePlayerScore(float score)
    {
        Instance.currentScore += score;
        Instance.playerScore.text = Instance.currentScore.ToString();
    }

    public static void UpdateBossHealth(float damage)
    {
        Instance.bossHealth -= damage;
        Instance.bossSlider.value = Instance.bossHealth;
        if (Instance.bossSlider.value == 0)
        {
            Instance.bossSlider.gameObject.SetActive(false);
            Instance.bossName.text = "";
        }
    }

    public static void AssignPlayer(float maxHP)
    {
        Instance.currentHealth = maxHP;
        Instance.playerHealth.text = maxHP.ToString();
        Instance.currentScore = 0;
        Instance.playerScore.text = "" + 0;
    }

    public static void AssignBoss(string name, float maxHp)
    {
        Instance.bossHealth = maxHp;
        Instance.bossName.text = name;
        Instance.bossSlider.gameObject.SetActive(true);
        Instance.bossSlider.maxValue = maxHp;
        Instance.bossSlider.value = maxHp;
        Instance.bossSlider.minValue = 0;
    }
}
