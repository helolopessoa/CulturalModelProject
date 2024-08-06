//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Enemy : MonoBehaviour
//{

//    public Weapon currentGun;
//    public PlayerController playerCtrl;
//    public ProxemicsBehavior proxemicsBehavior;
//    public Image healthBar;
//    public Image trustLevelBar;
//    public float maxHealth = 100f;
//    public float maxTrustLevel = 100f;
//    public Vector2 movementSpeed = new Vector2(0.2f, 0.2f);
//    public Text currentEmotionText;

//    [HideInInspector]
//    public bool hasBeenShot = false;
//    [HideInInspector]
//    public float maxWalkingTime = 5f;

//    string lastMentalState = "Neutral";
//    float neutralStateTimer = 0;
//    float stoppedStateTimer = 0;
//    //float hasBeenShotTimer = 0;
//    Vector2 direction;
//    string currentState = "neutral";
//    //string currentPlayerState = "";
//    public bool engagedInAction = false;
//    Rigidbody2D enemyBody;
//    float prejudiceLevel;
//    Emotion emotion;
//    Culture culture;
//    Personality personality;
//    Dictionary<string, float> cultureAttrs = new Dictionary<string, float>() {
//        { "dignity", 0 },
//        { "collectivism", 0 },
//        { "wealth", 0 },
//        { "politeness", 0 },
//        { "rationatity", 0 },
//        { "trust_level", 0 },
//    };
//    float[] emotionBands = new float[4] { 0, 0.2f, 0.5f, 0.7f };

//    // Use this for initialization
//    void Start()
//    {
//        GenerateInitialEmotion();
//        GenerateInitialPersonality();
//        GenerateInitialCulture();
//        prejudiceLevel = Random.Range(0f, 1f);
//        trustLevelBar.fillAmount = 0.5f;
//        SetRandomDirection();

//        float time = culture.GetTime();
//        movementSpeed = movementSpeed * time;

//        culture.LoadCultureDict(cultureAttrs);

//        currentGun.SetShotBy("Enemy");
//        enemyBody = GetComponent<Rigidbody2D>();

//        currentEmotionText.text = emotion.GetName();
//    }

//    /// <summary>
//    /// Generates the initial RANDOM personality.
//    /// </summary>
//    void GenerateInitialPersonality()
//    {
//        float[] newPersonality = new float[5];

//        for (int i = 0; i < newPersonality.Length; i++)
//        {
//            float rand = Random.Range(0f, 1f) * 10f;
//            newPersonality[i] = Mathf.Round(rand) * 0.1f;
//        }
//        personality = new Personality(newPersonality);
//    }

//    /// <summary>
//    /// Generates RANDOM bios emotion.
//    /// </summary>
//    void GenerateInitialEmotion()
//    {
//        float[] randomEmotion = Emotion.GetRandomEmotion();
//        float[] newEmotion = new float[4];

//        for (int i = 0; i < newEmotion.Length; i++)
//        {
//            //newEmotion[i] = randomEmotion[i];
//            newEmotion[i] = 0;
//        }
//        emotion = new Emotion(newEmotion);
//    }

//    // Generating RANDOM culture
//    /// <summary>
//    /// Generates the RANDOM initial culture.
//    /// </summary>
//    void GenerateInitialCulture()
//    {
//        float[] newCulture = new float[6];

//        for (int i = 0; i < newCulture.Length; i++)
//        {
//            float rand = Random.Range(0f, 1f) * 10f;
//            newCulture[i] = Mathf.Round(rand) * 0.1f;
//        }
//        culture = new Culture(newCulture);
//    }

//    /// <summary>
//    /// Sets a random direction.
//    /// </summary>
//    void SetRandomDirection()
//    {
//        int dirX = Random.Range(0, 2);
//        int dirY = (dirX == 1) ? 0 : 1;
//        direction = new Vector2(dirX, dirY);
//    }

//    /// <summary>
//    /// Updates the neutral movement.
//    /// </summary>
//    /// <param name="dt">Delta time.</param>
//    void UpdateNeutralMovement(float dt)
//    {
//        if (neutralStateTimer > maxWalkingTime)
//        {
//            currentState = "stopped";
//            neutralStateTimer = 0;
//        }

//        Vector2 position = enemyBody.position;

//        position.x += direction.x * movementSpeed.x * dt;
//        position.y += direction.y * movementSpeed.y * dt;

//        enemyBody.position = position;

//        if (!engagedInAction)
//        {
//            neutralStateTimer += dt;
//        }
//    }

//    /// <summary>
//    /// Updates the stop action.
//    /// </summary>
//    /// <param name="dt">Delta time</param>
//    void UpdateStopAction(float dt)
//    {
//        if (stoppedStateTimer > 7.0f)
//        {
//            SetRandomDirection();
//            currentState = "neutral";
//            stoppedStateTimer = 0;
//        }

//        if (!engagedInAction)
//        {
//            stoppedStateTimer += dt;
//        }
//    }

//    /// <summary>
//    /// Updates the shoot player.
//    /// </summary>
//    void UpdateShootPlayer()
//    {
//        //Vector2 transformPos = transform.position;
//        //PlayerController player = proxemicsBehavior.currentPlayer;
//        //if (player && playerCtrl) {
//        //    Vector2 playerPosition = playerCtrl.GetCurrentPosition();
//        //    Vector2 shotDirection = playerPosition - transformPos;
//        //    shotDirection.Normalize();

//        //    currentGun.SetDirection(shotDirection.x, shotDirection.y);
//        //    currentGun.SetTrigger(true);
//        //}
//        //else {
//        //    currentState = "neutral";
//        //    currentGun.SetTrigger(false);
//        //}
//    }

//    /// <summary>
//    /// Updates the run away.
//    /// </summary>
//    void UpdateRunAway(float dt)
//    {
//        //Vector2 position = enemyBody.position;
//        //PlayerController player = proxemicsBehavior.currentPlayer;
//        //Debug.Log(player);
//        //Debug.Log(playerCtrl);
//        //if (player && playerCtrl) {
//        //    Vector2 target = playerCtrl.GetCurrentPosition();
//        //    target = position + (position - target) * 100;
//        //    enemyBody.position = Vector2.MoveTowards(position, target, dt);
//        //    Debug.Log("qualquer coisa 2");
//        //}
//        //else {
//        //    currentState = "neutral";
//        //}
//    }

//    /// <summary>
//    /// Updates the follow.
//    /// </summary>
//    void UpdateFollow(float dt)
//    {
//        //Vector2 position = enemyBody.position;
//        //Vector2 position = transform.position;
//        //PlayerController player = proxemicsBehavior.currentPlayer;
//        //Debug.Log(player);
//        //Debug.Log(playerCtrl);
//        //if (player && playerCtrl) {
//        //    Vector2 target = playerCtrl.GetCurrentPosition();
//        //    Debug.Log(target);
//        //    enemyBody.position = Vector2.MoveTowards(position, target, dt);
//        //}
//        //else {
//        //    currentState = "neutral";
//        //}
//    }

//    /// <summary>
//    /// Updates enemy behavior.
//    /// </summary>
//    /// <param name="dt">Delta time.</param>
//    void UpdateBehavior(float dt)
//    {
//        if (currentState == "neutral")
//        {
//            UpdateNeutralMovement(dt);
//        }
//        else if (currentState == "stopped")
//        {
//            UpdateStopAction(dt);
//        }
//        else if (currentState == "anger")
//        {
//            UpdateShootPlayer();
//        }
//        else if (currentState == "fear" || currentState == "disgust" || currentState == "sadness")
//        {
//            Debug.Log("entrei 1");
//            UpdateRunAway(dt);
//        }
//        else if (currentState == "joy" || currentState == "trust")
//        {
//            Debug.Log("entrei 2");
//            UpdateFollow(dt);
//        }
//    }

//    /// <summary>
//    /// Updates the trust level value.
//    /// </summary>
//    void UpdateTrustLevel()
//    {
//        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
//        float trustLvl = trustLevelBar.fillAmount;
//        string mentalStateName = emotion.GetMentalStateName();
//        //Debug.Log("Mental State: " + mentalStateName);
//        int infValue = trustInf[mentalStateName];
//        //Debug.Log(infValue);

//        trustLvl = trustLvl + infValue * prejudiceLevel * (1 / maxTrustLevel);
//        trustLevelBar.fillAmount = trustLvl;
//    }

//    void UpdateCurrentState()
//    {
//        Dictionary<string, int> trustInf = AllEmotions.GetTrustInfluence();
//        string mentalStateName = emotion.GetMentalStateName();

//        //if (mentalStateName != lastMentalState) {
//        //    emotion.ResetCurrentEmotion();
//        //    lastMentalState = mentalStateName;
//        //}

//        currentState = mentalStateName.ToLower();
//    }

//    /// <summary>
//    /// Dispatchs the state of the player.
//    /// </summary>
//    /// <param name="playerState">Player current action state.</param>
//    public void DispatchPlayerState(string playerState)
//    {
//        Dictionary<string, string[]> stateEmo = ActionEmotions.GetDict();
//        Dictionary<string, string> stateAttrs = ActionEmotions.GetCultureAttributes();
//        Dictionary<string, float[]> allEmo = AllEmotions.GetDict();

//        if (playerState != "")
//        {
//            string[] emotionsArray = stateEmo[playerState];
//            string attrName = stateAttrs[playerState];
//            float rat = 1 - culture.GetRationality();
//            float attrValue = cultureAttrs[attrName];
//            float result = Mathf.Sqrt(attrValue * rat);
//            string resEmotion = emotionsArray[0];

//            for (int i = 1; i < emotionBands.Length; i++)
//            {
//                if (result > emotionBands[i])
//                {
//                    resEmotion = emotionsArray[i];
//                }
//            }
//            //Debug.Log("Result Emotion: " + resEmotion);

//            UpdateEmotionByEvent(allEmo[resEmotion]);
//            //string infEmoStr = "Influent Emotion: ";
//            //float[] infEmo = emotion.GetMostInfluentEmotion();

//            //for (int i = 0; i < infEmo.Length; i++)
//            //{
//            //    infEmoStr += infEmo[i] + ",";
//            //}
//            //Debug.Log(infEmoStr);
//            //Debug.Log("Influent Emotion Name: " + emotion.GetMostInfluentName());
//            UpdateTrustLevel();
//            UpdateCurrentState();
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float dt = Time.deltaTime;

//        emotion.Update(dt);

//        UpdateBehavior(dt);

//        cultureAttrs["trust_level"] = trustLevelBar.fillAmount;

//        if (healthBar.fillAmount <= 0)
//        {
//            Destroy(gameObject);
//        }

//        currentEmotionText.text = emotion.GetMostInfluentName();
//    }

//    /// <summary>
//    /// Updates the emotion by event.
//    /// </summary>
//    /// <param name="eventEmotion">Event emotion.</param>
//    void UpdateEmotionByEvent(float[] eventEmotion)
//    {
//        float[] newEmotion = new float[4];
//        float[] p = personality.GetPersonality();
//        float[,] pFactors = Personality.PositiveFactors;
//        float[,] nFactors = Personality.NegativeFactors;

//        // Generate new emotion based on Personality Traits and Factors
//        for (int i = 0; i < 4; i++)
//        {
//            for (int j = 0; j < 5; j++)
//            {
//                if (eventEmotion[i] > 0)
//                    newEmotion[i] += eventEmotion[i] * p[j] * pFactors[j, i];
//                else
//                    newEmotion[i] += eventEmotion[i] * p[j] * nFactors[j, i];
//            }
//            newEmotion[i] = newEmotion[i] / 5;
//        }

//        // Add new generated emotion
//        emotion.AddEmotion(newEmotion);
//        // Clamp after emotion add
//        emotion.ClampCurrentEmotion();
//        // Set the most influent emotion
//        emotion.SetMostInfluentEmotion();
//    }
//}