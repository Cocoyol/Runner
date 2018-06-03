
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

    public float BasePointsPerItem = 5;
    public float alivePointsFactor = 0.075f;

    public static bool alive = true;
    private int _level = 1;
    private float _alivePoints = 0;
    private float _itemPoints = 0;
    private int _itemsTaken = 0;
    private float _points = 0;

    private int _maxLevel = 1;
    private float _maxAlivePoints = 0;
    private float _maxItemPoints = 0;
    private int _maxItemsTaken = 0;
    private float _maxPoints = 0;

    [SerializeField]
    private Text scoreText;
    private Transform camPosition;

    //private float levelGameItemBonus;
    private float cacheAlivePoints = 0;

    // <<< Getters - Setters

    public int level {
        get {
            return _level;
        }

        set {
            _level = value;
        }
    }

    public float alivePoints {
        get {
            return _alivePoints;
        }

        set {
            _alivePoints = value;
        }
    }

    public float itemPoints {
        get {
            return _itemPoints;
        }

        set {
            _itemPoints = value;
        }
    }

    public int itemsTaken {
        get {
            return _itemsTaken;
        }

        set {
            _itemsTaken = value;
        }
    }

    public float points {
        get {
            return _points;
        }

        set {
            _points = value;
        }
    }

    public int maxLevel {
        get {
            return _maxLevel;
        }

        set {
            _maxLevel = value;
        }
    }

    public float maxAlivePoints {
        get {
            return _maxAlivePoints;
        }

        set {
            _maxAlivePoints = value;
        }
    }

    public float maxItemPoints {
        get {
            return _maxItemPoints;
        }

        set {
            _maxItemPoints = value;
        }
    }

    public int maxItemsTaken {
        get {
            return _maxItemsTaken;
        }

        set {
            _maxItemsTaken = value;
        }
    }

    public float maxPoints {
        get {
            return _maxPoints;
        }

        set {
            _maxPoints = value;
        }
    }
    // >>>

    private void Awake() {
        alive = true;
        camPosition = Camera.main.transform;
        //levelGameItemBonus = levelGameItemBonus / 4;

        SetTextToScreen();

    }

    // <-- No es obligatorio, pero es una BUENA PRÁCTICA poner el EventHandler en "OnEnable"
    private void OnEnable() {
        Item.OnTake += IncrementsItemPoints;
    }

    private void OnDisable() {
        Item.OnTake -= IncrementsItemPoints;
    }
    // -->

    private void Update() {
        if (alive) {
            IncrementsAlivePoints();
        }
        _points = _alivePoints + _itemPoints;
        SetTextToScreen();
    }

    public void IncrementsItemPoints() {
        if (alive) {
            _itemsTaken++;
            _itemPoints = BasePointsPerItem + GameParameters.level - 1;
        }
        _points = _alivePoints + _itemPoints;
        SetTextToScreen();
    }

    // Incrementar puntuación cuanto más lejos llega el personaje
    public void IncrementsAlivePoints() {

        float tmpAlivePoints = (alivePointsFactor) * camPosition.position.x;

        if (!IsSameInteger(tmpAlivePoints, cacheAlivePoints)) {
            _alivePoints = tmpAlivePoints;
        }
        cacheAlivePoints = tmpAlivePoints;
    }

    private void SetTextToScreen() {
        scoreText.text = Mathf.Round(_points).ToString();
    }

    private bool IsSameInteger(float a, float b) {
        return ((int)a == (int)b);
    }

    public void InitScores(int level, float mp, float map, float mip, int mit) {
        // Highscores
        _maxLevel = level;
        _maxPoints = mp;
        _maxAlivePoints = map;
        _maxItemPoints = mip;
        _maxItemsTaken = mit;
    }

}
