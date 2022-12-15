﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Emotion {

    // These four positions represent the 
    // 4 axis of Plutchik's Emotion Model
    // [0] AngerXFear [Range(-1f, 1f)]
    // [1] DisgustXTrust [Range(-1f, 1f)]
    // [2] SadnessXJoy [Range(-1f, 1f)]
    // [3] AntecipationXSurprise [Range(-1f, 1f)]
    float[] currentEmotion = new float[4];

    // Represents the bios emotion
    float[] bios = new float[4];

    // Represents the linear velocity that makes
    // the current emotion go back to the bios emotion
    float bioTendency = 0.01f;

    // The maximum time to start changing the 
    float bioTendencyTime = 1.0f;

    // Represents the current computed time (based on DeltaTime)
    float currentTime = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Emotion"/> class.
    /// </summary>
    /// <param name="newEmotion">New emotion.</param>
    public Emotion(float[] newEmotion) {
        InitializeEmotion(newEmotion);
        LoadBios();
        GetFuzzyEmotion();
    }

    /// <summary>
    /// Gets the emotion.
    /// </summary>
    /// <returns>The emotion.</returns>
    public float[] GetEmotion() {
        return currentEmotion;
    }

    /// <summary>
    /// Gets the most influent emotion.
    /// </summary>
    /// <returns>The most influent emotion.</returns>
    //public float[] GetMostInfluentEmotion() {
    //    return influentEmotion;
    //}

    /// <summary>
    /// Apply the initial emotion values to the bios.
    /// </summary>
    void LoadBios () {
        for (int i=0; i< currentEmotion.Length; i++) {
            bios[i] = currentEmotion[i];
        }
    }

    /// <summary>
    /// // Initialize the current emotion values.
    /// </summary>
    /// <param name="newEmotion">New emotion.</param>
    void InitializeEmotion(float[] newEmotion) {
        for (int i = 0; i < newEmotion.Length; i++) {
            currentEmotion[i] = newEmotion[i];
        }
    }

    /// <summary>
    /// Adds the emotion.
    /// </summary>
    /// <param name="otherEmotion">Other emotion.</param>
    public void AddEmotion(float[] otherEmotion)
    {
        for (int i = 0; i < otherEmotion.Length; i++) {
            currentEmotion[i] += otherEmotion[i];
        }
    }

    /// <summary>
    /// Verify if the current emotion is equals to bios.
    /// </summary>
    /// <returns><c>true</c>, if bios was ised, <c>false</c> otherwise.</returns>
    bool IsBios() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            if (currentEmotion[i] != bios[i]) {
                return false;
            }
        }
        return true;
    }
	
	// Update is called once per frame
	public void UpdateEmotion (float dt) {
        currentTime += dt;

        // If time passes the biotendency max time,
        // make the current emotion linearly go back
        // to the bios
        if (currentTime > bioTendencyTime && !IsBios()) {

            for (int i = 0; i < currentEmotion.Length; i++) {
                if (currentEmotion[i] < bios[i])
                {
                    if (currentEmotion[i] + bioTendency >= bios[i]) {
                        currentEmotion[i] = bios[i];
                    }
                    else {
                        currentEmotion[i] += bioTendency;
                    }
                }
                else if (currentEmotion[i] > bios[i])
                {
                    if (currentEmotion[i] - bioTendency <= bios[i]) {
                        currentEmotion[i] = bios[i];
                    }
                    else {
                        currentEmotion[i] -= bioTendency;
                    }
                }
            }
            currentTime = 0;
        }

        // Clamp after update
        ClampCurrentEmotion();
        PostFuzzyEmotion();


        //Debug.Log("InfEmo: ");
        //for (int i=0; i < influentEmotion.Length; i++) {
        //    Debug.Log(influentEmotion[i]);
        //}
        //Debug.Log("CurrEmo: ");
        //for (int i = 0; i < currentEmotion.Length; i++) {
        //    Debug.Log(currentEmotion[i]);
        //}
    }
    /// <summary>
    /// Gets the current emotion name.
    /// </summary>
    /// <returns>The current emotion name.</returns>
    public string GetName() {
        Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();
        currentEmotion = GetEmotion();
        return allEmotions.FirstOrDefault(x => x.Value.SequenceEqual(currentEmotion)).Key;
    }

    /// <summary>
    /// Gets the name of the most influent emotion.
    /// </summary>
    /// <returns>The most influent emotion name.</returns>
    //public string GetMostInfluentName() {
    //    Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();

    //    return allEmotions.FirstOrDefault(x => x.Value.SequenceEqual(influentEmotion)).Key;
    //}

    /// <summary>
    /// Gets the name of the mental state.
    /// </summary>
    /// <returns>The mental state name.</returns>

    public string GetMentalStateName()
    {
        Dictionary<string, string> mentalStates = AllEmotions.GetMentalState();
        string name = GetFuzzyEmotion();
        return mentalStates[name];
    }

    /// <summary>
    /// Gets the random emotion.
    /// </summary>
    /// <returns>The random emotion.</returns>
    public static float[] GetRandomEmotion() {
        Dictionary<string, float[]> allEmotions = AllEmotions.GetDict();
        int dictSize = allEmotions.Count;
        int randomIndex = Random.Range(0, dictSize);

        return allEmotions.ElementAt(randomIndex).Value;
    }

    /// <summary>
    /// Clamps the current emotion.
    /// </summary>
    public void ClampCurrentEmotion() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            currentEmotion[i] = Mathf.Clamp(currentEmotion[i], -1.0f, 1.0f);
        }
    }

    /// <summary>
    /// Resets the current emotion.
    /// </summary>
    public void ResetCurrentEmotion() {
        for (int i = 0; i < currentEmotion.Length; i++) {
            currentEmotion[i] = 0;
        }
    }

    public string GetFuzzyEmotion()
    {
        FuzzyResponse fr = FuzzyAPI.getFuzzyEmotionalResponse();
        return fr.emotion;
    }
    public void PostFuzzyEmotion()
    {
        FuzzyAPI.postFuzzyEmotionalInput(currentEmotion);
    }
}
