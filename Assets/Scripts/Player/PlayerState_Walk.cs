using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerState_Walk : PlayerState {
    public PlayerState_Walk(PlayerSystem system) : base(system) { }

    public override IEnumerator Start() {
        _system.ePlayerState = EPlayerState.Player_Walk;
        yield break;
    }

    public override IEnumerator Process() {
        // PlayerState_Jump condition & transition
        if (Input.GetButtonDown("Jump")) {
            _system.SetState(new PlayerState_Jump(_system));
            yield break;
        }

        // PlayerState_Fall condition & transition
        if (!_system.characterController.isGrounded) {
            _system.SetState(new PlayerState_Fall(_system));
            yield break;
        }

        // PlayerState_Idle condition & transition
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
            _system.SetState(new PlayerState_Idle(_system));
            yield break;
        }

        yield break;
    }
}
