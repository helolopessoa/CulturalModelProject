using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRNPC : MonoBehaviour {
    [SerializeField]
    private ActionHUDVR ActionHUD;
    [SerializeField]
    private GameObject NPCHUD;
    [SerializeField]
    private GameObject nameHUD;
    [SerializeField]
    private GameObject humorHUD;
    //[SerializeField]
    //private GameObject cultureHUD;
    [SerializeField]
    private ElementBar healthBar;
    [SerializeField]
    private ElementBar trustBar;

    Emotion emotion;
    Culture culture;
    Personality personality;
    Rigidbody2D enemyBody; // IS THIS CORRECT??
    Vector3 direction;
    public Animator animComp;
    //public Weapon currentGun;
    //public PlayerController playerCtrl;
    //public ProxemicsBehavior proxemicsBehavior;

    private float maxHealth = 100;
    private float maxTrust = 100;
    private float currentHealth = 100;
    private float currentTrust = 0.5f;
    public Vector3 movementSpeed = new Vector3(0f, 0f, 0f);

    [HideInInspector]
    public bool hasBeenShot = false;
    [HideInInspector]
    public float maxWalkingTime = 5f;

    private string humorState = "joy";
    private string lastMentalState = "neutral";
    private float neutralStateTimer = 0;
    private float stoppedStateTimer = 0;

    //private string cultureString;
    [SerializeField]
    private string nameString;

    //public static currentNPC

    //public bool engagedInAction = false;
    float prejudiceLevel;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI humorText;
    //[SerializeField]
    //private TextMeshProUGUI cultureText;


    Dictionary<string, float> cultureAttrs = new Dictionary<string, float>() {
        { "dignity", 0 },
        { "collectivism", 0 },
        { "wealth", 0 },
        { "politeness", 0 },
        { "rationatity", 0 },
        { "trust_level", 0 },
    };
    float[] emotionBands = new float[4] { 0, 0.2f, 0.5f, 0.7f };


    void Start() {
        GenerateInitialEmotion();
        GenerateInitialPersonality();
        GenerateInitialCulture();
        SetRandomDirection();
        NPCHUD.SetActive(true);
        prejudiceLevel = Random.Range(0f, 1f);
        this.healthBar.SetMaxValue(maxHealth);
        this.trustBar.SetMaxValue(maxTrust);
        this.healthBar.SetValue(maxHealth);
        this.trustBar.SetValue(currentTrust);

        float time = culture.GetTime();
        movementSpeed = movementSpeed * time;
        culture.LoadCultureDict(cultureAttrs);
        //currentGun.SetShotBy("NPC");
        enemyBody = GetComponent<Rigidbody2D>();

        humorState = emotion.GetName();

        nameText = nameText.GetComponent<TextMeshProUGUI>();
        humorText = humorText.GetComponent<TextMeshProUGUI>();
        //cultureText = cultureText.GetComponent<TextMeshProUGUI>();
        nameText.text = nameString;
        //cultureText.text = cultureString;
    }


    void Update() {
        float dt = Time.deltaTime;

        this.emotion.UpdateEmotion(dt);

        UpdateBehavior(dt);

        cultureAttrs["trust_level"] = currentTrust;
        this.healthBar.SetValue(currentHealth);

        humorState = emotion.GetName();
        humorText.text = humorState;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates the trust level value.
    /// </summary>
    void UpdateTrustLevel()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetFuzzyEmotion();
        int infValue = trustInf[mentalStateName];

        currentTrust = currentTrust + infValue * prejudiceLevel * (1 / maxTrust);
        this.trustBar.SetValue(currentTrust);

    }

    private void OnCollisionEnter()
    {
            this.DispatchPlayerState("is_attacking");
            this.currentHealth -= 10;

    }

    public void StealItem()
    {
        this.DispatchPlayerState("is_stealing_item");
    }

    public void GiveItem()
    {
        this.DispatchPlayerState("is_giving_item");
    }

    public void StealMoney()
    {
        this.DispatchPlayerState("is_stealing_money");
    }

    public void GiveMoney()
    {
        this.DispatchPlayerState("is_giving_money");
    }

    /// <summary>
    /// Generates the initial RANDOM personality.
    /// </summary>
    void GenerateInitialPersonality()
    {
        float[] newPersonality = new float[5];

        for (int i = 0; i < newPersonality.Length; i++)
        {
            float rand = Random.Range(0f, 1f) * 10f;
            newPersonality[i] = Mathf.Round(rand) * 0.1f;
        }
        personality = new Personality(newPersonality);
    }

    /// <summary>
    /// Generates RANDOM bios emotion.
    /// </summary>
    void GenerateInitialEmotion()
    {
        float[] randomEmotion = Emotion.GetRandomEmotion();
        float[] newEmotion = new float[4];

        for (int i = 0; i < newEmotion.Length; i++)
        {
            //newEmotion[i] = randomEmotion[i];
            newEmotion[i] = 0;
        }
        emotion = new Emotion(newEmotion);
    }

    // Generating RANDOM culture
    /// <summary>
    /// Generates the RANDOM initial culture.
    /// </summary>
    void GenerateInitialCulture()
    {
        float[] newCulture = new float[6];

        for (int i = 0; i < newCulture.Length; i++)
        {
            float rand = Random.Range(0f, 1f) * 10f;
            newCulture[i] = Mathf.Round(rand) * 0.1f;
        }
        culture = new Culture(newCulture);
    }

    /// <summary>
    /// Sets a random direction.
    /// </summary>
    void SetRandomDirection()
    {
        int dirX = Random.Range(0, 2);
        int dirY = (dirX == 1) ? 0 : 1;
        int dirZ = (dirY == 1) ? 0 : 1;
        direction = new Vector3(dirX, dirY, dirZ);
    }

    /// <summary>
    /// Updates NPC animation and behaviour.
    /// </summary>
    /// <param name="dt">Delta time.</param>
    void UpdateBehavior(float dt)
    {
        if (humorState == "neutral" || humorState == "stopped")
        {
            animComp.SetTrigger("triggerNeutral");
        }
        else if (humorState == "anger")
        {
            animComp.SetTrigger("triggerAnger");
        }
        else if (humorState == "fear")
        {
            animComp.SetTrigger("triggerFear");
        }
        else if (humorState == "joy")
        {
            animComp.SetTrigger("triggerJoy");
        }
        else if (humorState == "trust")
        {
            animComp.SetTrigger("triggerTrust");
        }
        else if (humorState == "sadness")
        {
            animComp.SetTrigger("triggerSadness");
        }
        else if (humorState == "disgust")
        {
            animComp.SetTrigger("triggerDisgust");
        }
    }

    /// <summary>
    /// Updates NPC humor (aka their current state)
    /// </summary>
    void UpdateCurrentState()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetFuzzyEmotion();

        //if (mentalStateName != lastMentalState) {
        //    emotion.ResetCurrentEmotion();
        //    lastMentalState = mentalStateName;
        //}

        humorState = mentalStateName.ToLower();
        Debug.Log(humorState);
    }

    /// <summary>
    /// Dispatchs the state of the player.
    /// </summary>
    /// <param name="playerState">Player current action state.</param>
    public void DispatchPlayerState(string playerState)
    {
        Debug.Log(playerState);
        Dictionary<string, string[]> stateEmo = ActionEmotions.GetDict();
        Dictionary<string, string> stateAttrs = ActionEmotions.GetCultureAttributes();
        Dictionary<string, float[]> allEmo = AllEmotions.GetDict();

        if (playerState != "")
        {
            string[] emotionsArray = stateEmo[playerState];
            string attrName = stateAttrs[playerState];
            float rat = 1 - culture.GetRationality();
            float attrValue = cultureAttrs[attrName];
            float result = Mathf.Sqrt(attrValue * rat);
            string resEmotion = emotionsArray[0];

            for (int i = 1; i < emotionBands.Length; i++)
            {
                if (result > emotionBands[i])
                {
                    resEmotion = emotionsArray[i];
                }
            }

            UpdateEmotionByEvent(allEmo[resEmotion]);
            UpdateTrustLevel();
            UpdateCurrentState();
        }
    }

    /// <summary>
    /// Updates the emotion by event.
    /// </summary>
    /// <param name="eventEmotion">Event emotion.</param>
    void UpdateEmotionByEvent(float[] eventEmotion)
    {
        float[] newEmotion = new float[4];
        float[] p = personality.GetPersonality();
        float[,] pFactors = Personality.PositiveFactors;
        float[,] nFactors = Personality.NegativeFactors;

        // Generate new emotion based on Personality Traits and Factors
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (eventEmotion[i] > 0)
                    newEmotion[i] += eventEmotion[i] * p[j] * pFactors[j, i];
                else
                    newEmotion[i] += eventEmotion[i] * p[j] * nFactors[j, i];
            }
            newEmotion[i] = newEmotion[i] / 5;
        }

        //// Add new generated emotion
        emotion.AddEmotion(newEmotion);
        //emotion.calculateEmotion();
        //// Clamp after emotion add
        //emotion.ClampCurrentEmotion();
        //// Set the most influent emotion
        //emotion.SetMostInfluentEmotion();
    }

    // THIS COULD BE USED IN CASE OF DIFFERENT ACTIONS FOR DIFFERENT NPCS
    //public string[] getActions() {
    //    string[] actions = { "Give", "Steal", "Talk" };
    //    return actions;
    //}

}