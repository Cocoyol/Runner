
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject[] platforms;
    public GameObject[] commonItems;
    public LayerMask platformMask;

    public float minPlatDistance = 1f;
    public float maxPlatDistance = 2f;

    private GameObject platform;
    private BoxCollider2D platCollider;

    private int platformsArraylenght;
    private float posXNewPlatform;

    private int commonItemsArrayLenght;

    private void Awake() {

        UpdateLevelParameters();

        platformsArraylenght = platforms.Length;
        posXNewPlatform = transform.position.x;

        commonItemsArrayLenght = commonItems.Length;

        // Crea la primera plataforma
        platform = platforms[Random.Range(0, platformsArraylenght)];
        platCollider = platform.GetComponent<BoxCollider2D>();
    }

    public void UpdateLevelParameters() {
        minPlatDistance += (GameParameters.level - 1) * 0.1f;
        maxPlatDistance += (GameParameters.level - 1) * 0.05f;
        Debug.Log("Plat. Distances: " + minPlatDistance + ", " + maxPlatDistance);
    }

    private void Update() {
        if (posXNewPlatform < transform.position.x) {
            CreatePlatform(0);
        }
    }

    public void CreatePlatform(float offsetY) {
        Vector3 newPosition = transform.position;
        newPosition.y += offsetY + Random.value - 0.5f;

        // ** <ITEM **
        // Crea el primer Item con una probabilidad
        if(Random.value>0.75)
            CreateItem(GetItemPosition(newPosition, 1f, 3f));
        // ** ITEM> **

        // Crea la plataforma planeada
        //GameObject go = Instantiate(platform, newPosition, Quaternion.identity);
        Instantiate(platform, newPosition, Quaternion.identity);

        // Agrega la mitad del largo de plat. anterior
        posXNewPlatform += platCollider.size.x / 2;
        
        // Selecciona aleatoriamente una nueva siguiente plataforma
        platform = platforms[Random.Range(0, platformsArraylenght)];
        platCollider = platform.GetComponent<BoxCollider2D>();

        // Calcula aleatoriamente (min, max) la distancia
        float distance = Random.Range(minPlatDistance, maxPlatDistance);

        // ** <ITEM **
        Vector3 tmpPos = newPosition;
        tmpPos.x = posXNewPlatform + distance / 2;

        // Crea el segundo Item con una probabilidad
        if (Random.value > 0.75)
            CreateItem(GetItemPosition(tmpPos, 2f, 3f));
        // ** ITEM> **

        // Suma la nueva posición de la siguiente plataforma
        posXNewPlatform += distance + platCollider.size.x / 2;
    }

    // Sumar el offset a la posición de la plataforma para obtener la posición del item
    public Vector3 GetItemPosition(Vector3 initialPos, float minOffsetY, float maxOffsetY) {
        initialPos.y += Random.Range(minOffsetY, maxOffsetY);
        return initialPos;
    }

    // Instanciar el item
    public void CreateItem(Vector3 position) {
        // Selecciona aleatoriamente un nuevo item
        GameObject item = commonItems[Random.Range(0, commonItemsArrayLenght)];
        //GameObject item = commonItems[0];
        Instantiate(item, position, Quaternion.identity);
    }

    private bool CheckCollisionPlatforms(Vector3 position, Vector2 size, LayerMask layer) {      // layerMask (1<<layr)
        bool coll = Physics2D.OverlapBox(position, size, 0, layer);
        return coll;
    }
}
