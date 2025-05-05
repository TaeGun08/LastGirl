using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyChestParts : LobbyParts
{
    protected override IEnumerator OnInteractionCoroutine()
    {
        isInteractionEnabled = true;
        audioManager.SetSfxClip(audioManager.AudioObject.playerClips.ChestClips[
            Random.Range(0, audioManager.AudioObject.playerClips.ChestClips.Length)]);
        yield return new WaitForSeconds(2f);
        isInteractionEnabled = false;
    }
}
