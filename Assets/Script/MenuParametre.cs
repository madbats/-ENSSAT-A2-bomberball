using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class MenuParametre : MonoBehaviour
{
    public GameObject p;
    Resolution[] res;
    public Dropdown resDropDown;
    public void Start(){
        res = Screen.resolutions;
        resDropDown.ClearOptions();
        List<string> options = new List<string>();
        int c = 0;
        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + "x" + res[i].height;
            options.Add(option);

            if (res[i].width == Screen.width && res[i].height == Screen.height){
                c = i;
            }
        }
        resDropDown.AddOptions(options);
        resDropDown.value = c;
        resDropDown.RefreshShownValue();
    }

    public void SetResolution(int index){
        Resolution r = res[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

    public void QuitterParametre(){
        p.SetActive(false);
    }
}
