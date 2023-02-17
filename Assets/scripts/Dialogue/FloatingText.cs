// Floating Text
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Dialogue 
{
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

        public void UpdateFloatingText(float speed = 1, bool destroyOnFinish = false) {
            if (!active) {
                return;
            }

            if (active) {
                this.text.color = new Color(this.color.r, this.color.g, this.color.b, Mathf.Lerp(1, 0, (Time.time - lastShown) / duration));
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
    }
}