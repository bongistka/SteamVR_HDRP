using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSequenceClip : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Button removeButton;
    [SerializeField] Button StartSequenceButton;
    [Serializable]
    public struct SequencePrefab
    {
        public int verticalSize;
        public GameObject prefab;
    }
    [SerializeField] SequencePrefab[] sequencePrefabs;
    private List<SequencePrefab> currentClips = new List<SequencePrefab>();
    [SerializeField] GameObject parentObject;

    public void AddSequence(int i)
    {
        if (i == 5)
            return;

        GameObject go = Instantiate(sequencePrefabs[i].prefab);
        go.transform.SetParent(this.transform, false);

        Vector3 pos = parentObject.GetComponent<RectTransform>().anchoredPosition;

        go.GetComponent<RectTransform>().offsetMin = go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        Vector3 pos1 = pos;
        pos1.x -= 20;
        go.GetComponent<RectTransform>().anchoredPosition = pos1;
        
        pos.y -= ( sequencePrefabs[i].verticalSize + (sequencePrefabs[i].verticalSize/30)*10);
        parentObject.GetComponent<RectTransform>().anchoredPosition = pos;

        SequencePrefab sp = new SequencePrefab();
        sp.verticalSize = sequencePrefabs[i].verticalSize;
        sp.prefab = go;
        currentClips.Add(sp);
        dropdown.value = 5;
        
        if (currentClips.Count > 0)
            removeButton.interactable = StartSequenceButton.interactable = true;
        else
            removeButton.interactable = StartSequenceButton.interactable = false;
    }

    public void RemoveLastSequence()
    {
        if (currentClips.Count > 0) //prevent IndexOutOfRangeException for empty list
        {
            int i = currentClips.Count - 1;
            Destroy(currentClips[i].prefab);

            Vector3 pos = parentObject.GetComponent<RectTransform>().anchoredPosition;
            pos.y += (currentClips[i].verticalSize + (currentClips[i].verticalSize / 30) * 10);
            parentObject.GetComponent<RectTransform>().anchoredPosition = pos;

            currentClips.RemoveAt(i);
        }

        if (currentClips.Count > 0)
            removeButton.interactable = true;
        else
            removeButton.interactable = false;
    }

    public void AddClipsToSequence()
    {
        foreach(SequencePrefab clip in currentClips)
        {
            clip.prefab.GetComponentInChildren<SequenceClip>().AddClipToProtocol();
        }
    }

    public void ResetSequence()
    {
        foreach (SequencePrefab clip in currentClips)
        {
            Destroy(clip.prefab);
        }
        currentClips = new List<SequencePrefab>();
        Vector3 pos = parentObject.GetComponent<RectTransform>().anchoredPosition;
        pos.y = -55;
        parentObject.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    internal void SetDurationOfLast(string v)
    {
        if (currentClips.Count > 0) //prevent IndexOutOfRangeException for empty list
        {
            int i = currentClips.Count - 1;
            currentClips[i].prefab.GetComponentInChildren<SequenceClip>().SetDuration(v);
        }
    }

    internal void SetTypeOfLast(int v)
    {
        if (currentClips.Count > 0) //prevent IndexOutOfRangeException for empty list
        {
            int i = currentClips.Count - 1;
            currentClips[i].prefab.GetComponentInChildren<SequenceClip>().SetType(v);
        }
    }
}
