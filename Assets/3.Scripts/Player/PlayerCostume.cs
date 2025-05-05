using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerCostume : MonoBehaviour
{
    public class Costume
    {
        public int HeadIndex;
        public int FaceIndex;
        public int BodyIndex;
        public int[] AccIndexs;

        public Costume(int headIndex, int faceIndex, int bodyIndex, int[] accIndex)
        {
            HeadIndex = headIndex;
            FaceIndex = faceIndex;
            BodyIndex = bodyIndex;
            AccIndexs = accIndex;
        }
    }
    
    [Header("Costume Settings")] 
    [SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private GameObject[] bodies;
    [SerializeField] private GameObject[] accs;
    
    private Costume costume;

    private void Awake()
    {
        costume = new Costume(1, 1, 1, new int[accs.Length]);
        LoadCostume();
    }

    private void LoadCostume()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("SaveCostume"))) return;
        
        costume = JsonConvert.DeserializeObject<Costume>(PlayerPrefs.GetString("SaveCostume"));
        
        ChangeCostume(0, costume.HeadIndex);
        ChangeCostume(1, costume.FaceIndex);
        ChangeCostume(2, costume.BodyIndex);
        
        for (int i = 0; i < costume.AccIndexs.Length; i++)
        {
            if (costume.AccIndexs[i] == 0)
            {
                ChangeAccCostume(0, true);
            }
            else
            {
                ChangeAccCostume(i);
            }
        }
    }
    
    public void ChangeCostume(int type, int costumeIndex)
    {
        GameObject[] temp = null;
        
        switch (type)
        {
            case 0:
                temp = heads;
                costume.HeadIndex = costumeIndex;
                break;
            case 1:
                temp = faces;
                costume.FaceIndex = costumeIndex;
                break;
            case 2:
                temp = bodies;
                costume.BodyIndex = costumeIndex;
                break;
        }
        
        foreach (GameObject t in temp)
        {
            t.SetActive(false);
        }
        
        PlayerPrefs.SetString("SaveCostume", JsonConvert.SerializeObject(costume));
        
        if (costumeIndex == 0) return;
        temp[costumeIndex - 1].SetActive(true);
    }
    
    public void ChangeAccCostume(int accIndex, bool allActiveFalse = false)
    {
        if (allActiveFalse)
        {
            for (int i = 0; i < accs.Length; i++)
            {
                accs[i].SetActive(false);
                costume.AccIndexs[i] = 0;
            }
        }
        else
        {
            if (accIndex == 0) return;
            accs[accIndex - 1].SetActive(true);
            costume.AccIndexs[accIndex - 1] = 1;
        }
        
        PlayerPrefs.SetString("SaveCostume", JsonConvert.SerializeObject(costume));
    }
}
