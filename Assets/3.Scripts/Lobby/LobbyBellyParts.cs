using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBellyParts : LobbyParts
{
    protected override IEnumerator OnInteractionCoroutine()
    {
        isInteractionEnabled = true;
        audioManager.SetSfxClip(audioManager.AudioObject.playerClips.BellyClips[
            Random.Range(0, audioManager.AudioObject.playerClips.BellyClips.Length)]);
        yield return new WaitForSeconds(2f);
        isInteractionEnabled = false;
    }
}
