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
    [SerializeField] private Transform[] arrowSlot = new Transform[3];
    [SerializeField] private GameObject arrow = null;
    [SerializeField] private Sprite defaultSprite = null;

    bool shieldOnCD = false;
    float durationCD = 0f;

    float currentHealth;
    float currentScore;
    float bossHealth;
    int activeWeapons = -1;
    int currentWeapon = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            ResetGameEventInfo Rgei = new ResetGameEventInfo(null, "Escape has been pressed");
            EventCoordinator.ActivateEvent(Rgei);
        }
    }

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
        instance.arrow.transform.position = instance.arrowSlot[0].position;
    }

    public static void AddNewWeaponSprite(Sprite weaponSprite)
    {
        instance.activeWeapons++;
        instance.weaponSlots[instance.activeWeapons].sprite = weaponSprite;
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
        if (instance.activeWeapons < 0)
            return;

        instance.currentWeapon += i;


        if (instance.currentWeapon > instance.activeWeapons)
        {
            instance.currentWeapon = 0;
        }
        else if (instance.currentWeapon < 0)
        {
            instance.currentWeapon = instance.activeWeapons;
        }

        instance.arrow.transform.position = instance.arrowSlot[instance.currentWeapon].position;
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
        instance.activeWeapons = -1;
        instance.currentWeapon = 0;
        Time.timeScale = 0;
        foreach (Image img in instance.weaponSlots)
        {
            img.sprite = instance.defaultSprite;
        }
        instance.arrow.transform.position = instance.arrowSlot[0].position;
    }
}
