using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance { get { return instance; } }

    [Header("Reference to the player")]
    [SerializeField] private PlayerBehaviour player = null;

    [Header("The diferent main panels")]
    [SerializeField] private GameObject menuUIPanel = null;

    [Header("Main Menu components")]
    [SerializeField] private GameObject settingMenu = null;
    [SerializeField] private Toggle mouseController = null;
    [SerializeField] private Toggle waveBased = null;

    [SerializeField] private GameObject howToPlayMenu = null;
    [SerializeField] private GameObject howToPlayIntro = null;

    [Header("Upper part of Game UI")]
    [SerializeField] private Text playerHealth = null;
    [SerializeField] private Text playerScore = null;
    [SerializeField] private Text bossName = null;
    [SerializeField] private Slider bossSlider = null;
    [SerializeField] private Text announcecment = null;

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

        Time.timeScale = 0;
        EventCoordinator.RegisterEventListener<ResetGameEventInfo>(ResetGame);
        Instance.bossSlider.gameObject.SetActive(false);
        Instance.bossName.text = "";
    }

    public void ToggleHowToPlay(bool status)
    {
        howToPlayMenu.SetActive(status);
    }

    public void ToggleSettings(bool status)
    {
        settingMenu.SetActive(status);
    }

    public static void ChangeWeapon(int i)
    {
        Sprite spriteHolder;
        if( i > 0)
        {
            int lastSprite = Instance.weaponSlots.Length - 1;
            spriteHolder = Instance.weaponSlots[0].sprite;

            for (int j = 0; j <= lastSprite; j++)
            {
                if (j + 1 <= lastSprite)
                {
                    Instance.weaponSlots[j].sprite = Instance.weaponSlots[j + 1].sprite;
                }
                else
                {
                    Instance.weaponSlots[j].sprite = spriteHolder;
                }
            }
        }
        else
        {
            int lastSprite = Instance.weaponSlots.Length - 1;
            spriteHolder = Instance.weaponSlots[lastSprite].sprite;
            for (int j = lastSprite; j >= 0; j--)
            {
                if (j - 1 >= 0)
                {
                    Instance.weaponSlots[j].sprite = Instance.weaponSlots[j - 1].sprite;
                }
                else
                {
                    Instance.weaponSlots[j].sprite = spriteHolder;
                }
            }
        }
    }

    public void StartGame()
    {
        ToggleHowToPlay(false);
        player.MouseController(mouseController.isOn);
        menuUIPanel.SetActive(false);
        StartCoroutine(ReminderHowToPlay());
    }

    IEnumerator ReminderHowToPlay()
    {
        Time.timeScale = 1;
        howToPlayIntro.SetActive(true);
        float timer = 0;
        while(timer < 5)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        howToPlayIntro.SetActive(false);
        Spawner.ActivateSpawner(waveBased.isOn);
    }

    public static void ShieldActivated(float duration)
    {
        if (!Instance.shieldOnCD)
        {
            Instance.durationCD = duration;
            Instance.StartCoroutine(StartShieldCooldown());
        }
    }

    public static void UpdateWaveInfo(string text)
    {
        instance.announcecment.text = text;
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

    public static void ResetGame(EventInfo ei)
    {
        Instance.bossSlider.gameObject.SetActive(false);
        Instance.bossName.text = "";
        Instance.menuUIPanel.SetActive(true);
    }
}
