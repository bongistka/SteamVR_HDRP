using UnityEngine;
using UnityEngine.UI;

public class SwitchAvatar : MonoBehaviour
{
    [SerializeField] GameObject _femaleMesh;
    [SerializeField] GameObject _maleMesh;
    [SerializeField] Toggle _femaleToggle;
    [SerializeField] Toggle _maleToggle;

    private void Start()
    {
        SwitchFemaleMesh(_femaleToggle.isOn);
        SwitchMaleMesh(_maleToggle.isOn);
    }

    public void SwitchFemaleMesh(bool isOn)
    {
        _femaleMesh.SetActive(isOn);
        _maleMesh.SetActive(!isOn);
        _maleToggle.isOn = !isOn;
    }

    public void SwitchMaleMesh(bool isOn)
    {
        _maleMesh.SetActive(isOn);
        _femaleMesh.SetActive(!isOn);
        _femaleToggle.isOn = !isOn;
    }
}
