using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHeadParts : LobbyParts
{
    protected override IEnumerator OnInteractionCoroutine()
    {
        isInteractionEnabled = true;
        audioManager.SetSfxClip(audioManager.AudioObject.playerClips.HeadClips[
            Random.Range(0, audioManager.AudioObject.playerClips.HeadClips.Length)]);
        yield return new WaitForSeconds(2f);
        isInteractionEnabled = false;
    }
}
