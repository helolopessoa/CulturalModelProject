using UnityEngine;
using TMPro;

public class NPCHUD : MonoBehaviour {

    // [SerializeField]
    // private GameObject nameHUD;
    // [SerializeField]
    // private GameObject humorHUD;
    // [SerializeField]
    // private GameObject cultureHUD;
    [SerializeField]
    private ElementBar healthBar;
    [SerializeField]
    private ElementBar trustBar;


    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI humorText;
    [SerializeField]
    private TextMeshProUGUI cultureText;

    public float currentHealth = 100;
    public float currentTrust = 0.5f;
    public string currentName;
    public string currentCulture;
    public string currentHumor;


    void UpdateNPC()
    {

        healthBar.SetMaxValue(currentHealth);
        trustBar.SetMaxValue(currentTrust);
        nameText = nameText.GetComponent<TextMeshProUGUI>();
        humorText = humorText.GetComponent<TextMeshProUGUI>();
        cultureText = cultureText.GetComponent<TextMeshProUGUI>();
        nameText.text = currentName;
        cultureText.text = currentCulture;
        humorText.text = currentHumor;

    }

    public void onNPCSelected(NPC npc){
        // Debug.Log(npc.nameString);
        currentHealth = npc.currentHealth;
        currentTrust = npc.currentTrust;
        currentName =  npc.nameString;
        currentCulture = npc.cultureString;
        currentHumor = npc.humorState;
        UpdateNPC();
    }


}