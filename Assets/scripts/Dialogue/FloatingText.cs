// Floating Text
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FloatingText {
    public bool active;
    public GameObject gObj;

    public Text text;
    public Vector3 position;
    public float duration;
    public float lastShown;

    public Color color;
    public float size;
    public float speed;
    public float rotation;
    public float rotationSpeed;
    public bool destroyOnFinish;
    public bool useWorldSpace;

    public void Show() {
        active = true;
        lastShown = Time.time;
        gObj.SetActive(active);
    }

    public void Hide() {
        active = false;
        gObj.SetActive(active);
    }

    public void UpdateFloatingText() {
        if (!active) {
            return;
        }

        if (active) {
            if (Time.time - lastShown > duration) {
                Hide();
                if (destroyOnFinish) {
                    Object.Destroy(gObj);
                }
            }

            gObj.transform.position += position * speed * Time.deltaTime;
            
            // else {
            //     position.y += speed * Time.deltaTime;
            //     rotation += rotationSpeed * Time.deltaTime;
            //     gObj.transform.position = position;
            //     gObj.transform.rotation = Quaternion.Euler(0, 0, rotation);
            // }
        }
    }
    // constructor
    public FloatingText(GameObject gObj, Text text, Vector3 position, float duration, Color color, float size, float speed, float rotation, float rotationSpeed, bool destroyOnFinish, bool useWorldSpace) {
        this.gObj = gObj;
        this.text = text;
        this.position = position;
        this.duration = duration;
        this.color = color;
        this.size = size;
        this.speed = speed;
        this.rotation = rotation;
        this.rotationSpeed = rotationSpeed;
        this.destroyOnFinish = destroyOnFinish;
        this.useWorldSpace = useWorldSpace;
    }
}