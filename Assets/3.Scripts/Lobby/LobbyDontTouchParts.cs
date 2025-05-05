using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LobbyDontTouchParts : LobbyParts
{
    private static readonly int FORCED_EXIT = Animator.StringToHash("ForcedExit");

    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject ForcedExitUI;
    [SerializeField] private Transform cam;
    
    protected override IEnumerator OnInteractionCoroutine()
    {
        isInteractionEnabled = true;
        anim.SetTrigger(FORCED_EXIT);
        yield return new WaitForSeconds(1f);
        audioManager.SetSfxClip(audioManager.AudioObject.playerClips.DontTouchClips[
            Random.Range(0, audioManager.AudioObject.playerClips.DontTouchClips.Length)]);
        yield return new WaitForSeconds(1.3f);
        lobbyUI.SetActive(false);
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.2f);
        ForcedExitUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        cam.DORotate(new Vector3(-90f, 10f, -90f), 2f).OnComplete(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        });
    }
}
