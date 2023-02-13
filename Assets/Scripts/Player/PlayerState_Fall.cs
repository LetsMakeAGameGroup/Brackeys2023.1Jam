using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerState_Fall : PlayerState {
    public PlayerState_Fall(PlayerSystem system) : base(system) { }

    public override IEnumerator Start() {
        _system.ePlayerState = EPlayerState.Player_Fall;
        yield break;
    }

    public override IEnumerator Process() {
        // Check if still falling
        if (!_system.characterController.isGrounded) yield break;

        // PlayerState_Walk condition & transition
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            _system.SetState(new PlayerState_Walk(_system));
            yield break;
        }

        // If not falling or walking, must be idle.
        _system.SetState(new PlayerState_Idle(_system));
        yield break;
    }
}
