using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    private static readonly int IS_GAME_START = Animator.StringToHash("isGameStart");

    private MainManager mainManager;
    private AudioManager audioManager;

    private Camera mainCam;

    [Header("InLobby Settings")] [SerializeField]
    private GameObject player;
    [SerializeField] private Transform lobbyCameraTrs;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform[] arrowScales;

    [Header("InTitle Settings")] [SerializeField]
    private Animator anim;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject[] cams;

    [Header("InGameReady Settings")]
    [SerializeField] private GameObject readyUI;
    
    private void Awake()
    {
        buttons[0].onClick.AddListener(() => { StartCoroutine(nameof(TitleBackCoroutine)); });

        buttons[1].onClick.AddListener(() =>
        {
            StartCoroutine(nameof(GameReadyCoroutine));
        });
    }

    private void Start()
    {
        mainManager = MainManager.Instance;
        audioManager = AudioManager.Instance;
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (mainManager.IsGameStart == false
            || mainManager.IsGameReady) return;

        InputCharacterRotate();
        InputMouseWheelZoom();
        InputPartsInteraction();
    }

    private void InputCharacterRotate()
    {
        if (Input.GetMouseButton(0))
        {
            float mousePosX = (Input.mousePosition.x / Screen.width) * 2f - 1f;

            if (mousePosX > 0.35f || mousePosX < -0.35f) return;
            player.transform.eulerAngles += new Vector3(0f, 360f * -mousePosX * 3f * Time.deltaTime, 0f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.transform.DORotate(new Vector3(0f, 180f, 0f), 0.5f);
        }
    }

    private void InputMouseWheelZoom()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        Vector3 camPos = lobbyCameraTrs.position;

        Vector3 leftArrow = arrowScales[0].localScale;
        Vector3 rightArrow = arrowScales[1].localScale;

        float t = mouseWheel * 2f;

        if (mouseWheel > 0)
        {
            camPos.z = Mathf.Lerp(camPos.z, targetPoint.position.z, t);
            camPos.y = Mathf.Lerp(camPos.y, targetPoint.position.y, t);
            leftArrow = Vector3.Lerp(leftArrow, Vector3.one * 0.5f, t);
            rightArrow = Vector3.Lerp(rightArrow, Vector3.one * 0.5f, t);
        }
        else if (mouseWheel < 0)
        {
            camPos.z = Mathf.Lerp(camPos.z, -2.0f, -t);
            camPos.y = Mathf.Lerp(camPos.y, 1.5f, -t);
            leftArrow = Vector3.Lerp(leftArrow, Vector3.one, -t);
            rightArrow = Vector3.Lerp(rightArrow, Vector3.one, -t);
        }

        lobbyCameraTrs.position = camPos;
        arrowScales[0].localScale = leftArrow;
        arrowScales[1].localScale = rightArrow;
    }

    private void InputPartsInteraction()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) == false) return;
        if (Physics.Raycast(ray, out RaycastHit hit,
                LayerMask.GetMask("LobbyInteractionParts")) == false) return;
        LobbyParts lobbyParts = hit.collider.GetComponent<LobbyParts>();
        if (lobbyParts != null)
            lobbyParts.PartsInteraction();
    }

    private IEnumerator TitleBackCoroutine()
    {
        audioManager.SetBgmClip(audioManager.AudioObject.bgmClips.TitleBgmClips[0]);
        player.transform.DOMoveY(0f, 2f);
        anim.SetBool(IS_GAME_START, false);
        lobbyUI.gameObject.SetActive(false);
        cams[0].gameObject.SetActive(true);
        cams[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        mainManager.IsGameStart = false;
        titleUI.gameObject.SetActive(true);
    }

    private IEnumerator GameReadyCoroutine()
    {
        lobbyUI.SetActive(false);
        cams[1].SetActive(false);
        cams[2].SetActive(true);
        mainManager.IsGameReady = true;
        yield return new WaitForSeconds(2f);
        readyUI.SetActive(true);
    }
}