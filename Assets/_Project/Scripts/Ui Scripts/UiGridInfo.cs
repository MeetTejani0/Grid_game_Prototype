using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGridInfo : MonoBehaviour
{
    [SerializeField] private Text xPositionText;
    [SerializeField] private Text yPositionText;
    [SerializeField] private Text gridTypeText;


    public void ShowInfo(int x,int y,GridStat.GridType type)
    {
        xPositionText.text = x.ToString();
        yPositionText.text = y.ToString();
        gridTypeText.text = type.ToString();
    }
    

}
