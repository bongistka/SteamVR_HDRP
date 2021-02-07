using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSequenceClip : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Button removeButton;
    [SerializeField] Button startSequenceButton;
    [SerializeField] RectTransform container;
    public float gapSize = 20;
    private float containerSize = 120;
    private float defaultContainerSize;
    [Serializable]
    public struct SequencePrefab
    {
        public int verticalSize;
        public GameObject prefab;
    }
    [SerializeField] SequencePrefab[] sequencePrefabs;
    private List<SequencePrefab> currentClips = new List<SequencePrefab>();
    [SerializeField] GameObject parentObject;

    private void Start()
    {
        defaultContainerSize = container.sizeDelta.y;
    }

    public void AddSequence(int i)
    {
        if (i == 5)
            return;

        GameObject go = Instantiate(sequencePrefabs[i].prefab);
        go.transform.SetParent(container, false);

        Vector3 pos = parentObject.GetComponent<RectTransform>().anchoredPosition;

        go.GetComponent<RectTransform>().offsetMin = go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        Vector3 pos1 = pos;
        pos1.x -= gapSize;
        go.GetComponent<RectTransform>().anchoredPosition = pos1;
        
        pos.y -= ( sequencePrefabs[i].verticalSize + (sequencePrefabs[i].verticalSize/30)*10);
        parentObject.GetComponent<RectTransform>().anchoredPosition = pos;

        containerSize += (sequencePrefabs[i].verticalSize + (sequencePrefabs[i].verticalSize / 30) * 10);
        if (containerSize > defaultContainerSize)
            container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, containerSize);

        SequencePrefab sp = new SequencePrefab();
        sp.verticalSize = sequencePrefabs[i].verticalSize;
        sp.prefab = go;
        currentClips.Add(sp);
        dropdown.value = 5;
        
        if (currentClips.Count > 0)
            removeButton.interactable = startSequenceButton.interactable = true;
        else
            removeButton.interactable = startSequenceButton.interactable = false;
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
            
            containerSize -= (currentClips[i].verticalSize + (currentClips[i].verticalSize / 30) * 10);

            if (containerSize > defaultContainerSize)
                container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, containerSize);
            else
                container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultContainerSize);

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
        containerSize = 120;
        container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultContainerSize);

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
