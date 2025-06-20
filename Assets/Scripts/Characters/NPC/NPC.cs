using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject NPCHUD;
    public GameObject LLMChatHUD;

    [HideInInspector]
    public Emotion emotion;

    [HideInInspector]
    public Culture culture;

    [HideInInspector]
    public Personality personality;
    Rigidbody npcBody;
    Vector3 direction;
    float prejudiceLevel;

    public Animator animComp;
    // public ProxemicsBehavior proxemicsBehavior;

    public float maxHealth = 100;
    private float maxTrust = 100;
    [HideInInspector]
    public float currentHealth = 100;
    [HideInInspector]
    public float currentTrust = 0.5f;
    [HideInInspector]
    public Vector3 movementSpeed = new Vector3(0f, 0f, 0f);

    [HideInInspector]
    public bool hasBeenShot = false;
    [HideInInspector]
    public float maxWalkingTime = 5f;

    private string lastMentalState = "Neutral";
    private float neutralStateTimer = 0;
    private float stoppedStateTimer = 0;

    [HideInInspector]
    public string cultureString;
    [HideInInspector]
    public string humorState = "neutral";
    public string nameString;


    public bool engagedInAction = false;


    Dictionary<string, float> cultureAttrs = new Dictionary<string, float>() {
        { "dignity", 0 },
        { "collectivism", 0 },
        { "wealth", 0 },
        { "politeness", 0 },
        { "rationatity", 0 },
        { "trust_level", 0 },
    };

    float[] emotionBands = new float[4] { 0, 0.2f, 0.5f, 0.7f };

    void Start()
    {
        GenerateInitialEmotion();
        GenerateInitialPersonality();
        GenerateInitialCulture();
        SetRandomDirection();
        prejudiceLevel = Random.Range(0f, 1f);


        float time = culture.GetTime();
        movementSpeed = movementSpeed * time;
        culture.LoadCultureDict(cultureAttrs);
        npcBody = GetComponent<Rigidbody>();
        humorState = emotion.GetName();


        UpdateCurrentState();

    }


    void Update()
    {

        float dt = Time.deltaTime;

        emotion?.UpdateEmotion(dt);

        UpdateBehavior(dt);
        // UpdateCurrentState();

        cultureAttrs["trust_level"] = currentTrust;

        humorState = emotion?.GetName();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            NPCHUD.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// Updates the trust level value.
    /// </summary>
    void UpdateTrustLevel()
    {
        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
        string mentalStateName = emotion.GetMentalStateName();
        int infValue = trustInf[mentalStateName];

        currentTrust = currentTrust + infValue * prejudiceLevel * (1 / maxTrust);

    }

    private void OnMouseDown()
    {
            DispatchPlayerState("is_attacking");
            currentHealth -= 1;

    }

    public void OnMouseAimEnter()
    {
        NPCHUD.SetActive(true);
        NPCHUD npcHud = NPCHUD.GetComponent<NPCHUD>();
        npcHud.onNPCSelected(this);
    }

    public void OnMouseAimExit()
    {
        NPCHUD.SetActive(false);
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
        Dictionary<string, float[]> cultures = Culture.GetCulturesValueDict();
        int rand = Random.Range(0, 5);
        cultureString = Culture.Cultures[rand];
        // Debug.Log(cultureString);
        for (int i = 0; i < newCulture.Length; i++)
        {
            newCulture[i] = cultures[cultureString][i];
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
        direction = new Vector3(dirX, dirY, 0f);
    }

    /// <summary>
    /// Updates NPC animation and behaviour.
    /// </summary>
    /// <param name="dt">Delta time.</param>
    void UpdateBehavior(float dt)
    {
        if (humorState == "neutral" || humorState == "stopped")
        {
            //UpdateNeutralMovement(dt);
            //animComp.SetTrigger("triggerWalking");
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
        string mentalStateName = emotion.GetMentalStateName();

        //if (mentalStateName != lastMentalState) {
        //    emotion.ResetCurrentEmotion();
        //    lastMentalState = mentalStateName;
        //}

        humorState = mentalStateName.ToLower();
        // Debug.Log(humorState);
    }

    /// <summary>
    /// Dispatchs the state of the player.
    /// </summary>
    /// <param name="playerState">Player current action state.</param>
    public void DispatchPlayerState(string playerState)
    {
        // Debug.Log(playerState);
        if (playerState != "")
        {
            if(playerState == "is_talking")
            {
                LLMChatHUD.SetActive(true);
                // GameObject.Find("LLMChat")?.SetActive(true);
                // LLMChatBox llmChat = GameObject.Find("LLMChat")?.GetComponent<LLMChatBox>();
                LLMChatBox llmChat = LLMChatHUD.GetComponent<LLMChatBox>();
                llmChat.onNPCSelected(this);
                return;
            }

            Dictionary<string, string[]> stateEmo = ActionEmotions.GetDict();
            Dictionary<string, string> stateAttrs = ActionEmotions.GetCultureAttributes();
            Dictionary<string, float[]> allEmo = AllEmotions.GetDict();

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
        emotion.ClampCurrentEmotion();
        //// Set the most influent emotion
        //emotion.SetMostInfluentEmotion();
    }

    //    /// <summary>
    //    /// Updates the neutral movement.
    //    /// </summary>
    //    /// <param name="dt">Delta time.</param>
    //void UpdateNeutralMovement(float dt)
    //{
    //    //if (neutralStateTimer > maxWalkingTime)
    //    //{
    //    //    currentState = "stopped";
    //    //    neutralStateTimer = 0;
    //    //}

    //    Vector3 position = npcBody.position;

    //    position.x += direction.x * movementSpeed.x * dt;
    //    position.y += direction.y * movementSpeed.y * dt;
    //    position.z = 0f;

    //    npcBody.position = position;

    //    //if (!engagedInAction)
    //    //{
    //    //    neutralStateTimer += dt;
    //    //}
    //}


    // THIS COULD BE USED IN CASE OF DIFFERENT ACTIONS FOR DIFFERENT NPCS
    //public string[] getActions() {
    //    string[] actions = { "Give", "Steal", "Talk" };
    //    return actions;
    //}

}