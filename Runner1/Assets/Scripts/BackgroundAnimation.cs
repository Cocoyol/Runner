
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour {

    public float speed;

    private Material material;

    private void Start () {
        material = GetComponent<Renderer>().material;
	}

	private void Update () {
        Vector2 offset = material.mainTextureOffset;
        offset.x = (offset.x + Time.deltaTime * speed) % 1;
        material.mainTextureOffset = offset;
	}
}
