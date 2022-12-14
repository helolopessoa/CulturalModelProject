using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0.5f;
    public GameObject bulletObj;

    string shotBy;
    float fireTime = 0;
    int totalClip = 200;
    int bulletsPerShot = 1;
    float delayBetweenBulletsPerShot = 0.1f;
    bool hasInfiniteClip = true;
    bool triggerPulled = false;
    Vector2 direction = new Vector2(1, 0);

    // Use this for initialization
    void Start () {
        fireTime = fireRate;
    }

    // Update is called once per frame
    private void Update() {
        float dt = Time.deltaTime;

        fireTime += dt;
        if (triggerPulled) {

            if (fireTime > fireRate && totalClip > 0) {
                for (int i = 0; i < bulletsPerShot; i++) {
                    Invoke("Shoot", delayBetweenBulletsPerShot*i);
                }
                triggerPulled = false;
                fireTime = 0;
            }

        }
    }

    /// <summary>
    /// Sets the direction.
    /// </summary>
    /// <param name="dirX">X direction.</param>
    /// <param name="dirY">Y direction.</param>
    public void SetDirection(float dirX, float dirY) {
        direction.x = dirX;
        direction.y = dirY;
    }

    /// <summary>
    /// Sets the shot by.
    /// </summary>
    /// <param name="someone">Someone.</param>
    public void SetShotBy(string someone) {
        shotBy = someone;
    }

    /// <summary>
    /// Gets the shot by.
    /// </summary>
    /// <returns>The shot by.</returns>
    public string GetShotBy() {
        return shotBy;
    }

    /// <summary>
    /// Sets the triggerPulled attribute.
    /// </summary>
    /// <param name="value"> Is true or false.</param>
    public void SetTrigger(bool value) {
        triggerPulled = value;
    }

    /// <summary>
    /// Instantiate bullet object.
    /// </summary>
    void Shoot() {
        GameObject newBullet = Instantiate(bulletObj, null);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        newBullet.transform.position = transform.position;
        bulletScript.SetDirection(direction.x, direction.y);
        bulletScript.SetShotBy(shotBy);

        if (!hasInfiniteClip) {
            totalClip -= bulletsPerShot;
        }
    }
}
