
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof(Character))]
public class InputController : MonoBehaviour {

    private Character character;
    private bool jump;

    private void Awake() {
        character = GetComponent<Character>();
    }

    private void Update() {
        if (!jump) {
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate() {

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        character.Move(horizontal, jump);
        jump = false;
    }
}
