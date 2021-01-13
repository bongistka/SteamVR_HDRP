using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMesh : MonoBehaviour
{
    [Serializable]
    public class SwitchableMesh
    {
        public GameObject _mesh;
        public Toggle _toggle;
    }

    [SerializeField]
    SwitchableMesh[] switchableMeshes;

    private void Start()
    {
        foreach (SwitchableMesh switchableMesh in switchableMeshes)
        {
            DoSwitchMesh(switchableMesh._mesh, switchableMesh._toggle.isOn);
        }
    }

    public void DoSwitchMesh(GameObject mesh, bool isOn)
    {
        mesh.SetActive(isOn);
    }

    public void SwitchMeshWithInt(int order)
    {
        DoSwitchMesh(switchableMeshes[order]._mesh, switchableMeshes[order]._toggle.isOn);
        int other;
        if (order % 2 == 0)
            other = order + 1;
        else
            other = order - 1;
        DoSwitchMesh(switchableMeshes[other]._mesh, !switchableMeshes[order]._toggle.isOn);
        switchableMeshes[other]._toggle.isOn = !switchableMeshes[order]._toggle.isOn;
    }
}
