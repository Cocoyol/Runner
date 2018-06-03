
using UnityEngine;

public class Item : MonoBehaviour {

    private bool touched = false;

    public delegate void Taking();
    public static event Taking OnTake;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (!touched) {
                touched = true;

                if (OnTake != null) {
                    OnTake();
                }

                Destroy(gameObject);
            }
        }
    }
}
