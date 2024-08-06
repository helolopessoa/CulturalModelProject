// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GameController : MonoBehaviour {

//     public int totalEnemies;
//     public GameObject enemyPrefab;
//     public GameObject playerObj;
//     public MenuController menuCtrl;
//     public Sprite[] enemySprites;

//     List<GameObject> spawnedEnemies;

//     /// <summary>
//     /// Spawns enemies in random positions.
//     /// Instantiates "Enemy" prefab
//     /// </summary>
//     void SpawnEnemies() {
//         for (int i=0; i < totalEnemies; i++) {
//             GameObject newEnemy = Instantiate(enemyPrefab, null);
//             Vector2 pos = newEnemy.transform.position;
//             pos.x = Random.Range(-7.5f, 7.5f);
//             pos.y = Random.Range(-4.5f, 4.5f);
//             newEnemy.transform.position = pos;

//             int randPos = Random.Range(0, enemySprites.Length);
//             newEnemy.GetComponent<SpriteRenderer>().sprite = enemySprites[randPos];
//             newEnemy.GetComponent<Enemy>().healthBar.fillAmount = 0.5f;
//             spawnedEnemies.Add(newEnemy);
//         }
//     }

//     // Why I needed to put the spawn here?
//     private void Awake() {
//         spawnedEnemies = new List<GameObject>();
//         SpawnEnemies();
//     }

//     // Use this for initialization
//     void Start () {
//         //spawnedEnemies = new List<GameObject>();
//         //SpawnEnemies();
//     }

//     // Update is called once per frame
//     void Update() {
//         float totalTrustLevel = 0;

//         PlayerController currPlayer = playerObj.GetComponent<PlayerController>();

//         for (int i = spawnedEnemies.Count - 1; i >= 0; i--) {
//             Enemy enemy = spawnedEnemies[i].GetComponent<Enemy>();

//             enemy.playerCtrl = currPlayer;

//             if (enemy.healthBar.fillAmount <= 0) {
//                 Destroy(enemy.gameObject);
//                 spawnedEnemies.RemoveAt(i);
//             }
//             else {
//                 totalTrustLevel += enemy.trustLevelBar.fillAmount;
//             }
//         }

//         totalTrustLevel = 100 * totalTrustLevel / spawnedEnemies.Count;

//         if (totalTrustLevel > 80) {
//             menuCtrl.SetVictory();
//         }

//         if (currPlayer.healthBar.fillAmount <= 0) {
//             menuCtrl.SetGameOver();
//         }

//         if (Input.GetKeyDown(KeyCode.Return)) {
//             menuCtrl.ToggleGamePause();
//         }
//     }

//     void DestroyAllEnemies() {
//         for (int i = spawnedEnemies.Count - 1; i >= 0; i--) {
//             GameObject enemy = spawnedEnemies[i];
//             Destroy(enemy);
//         }
//     }
// }
