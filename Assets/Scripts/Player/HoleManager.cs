using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleManager : MonoBehaviour
{
    private float circleCapacity;
    [SerializeField] private Image circleImg;
    [SerializeField] private Transform holeGameObj;

    private void Start()
    {
        circleImg.fillAmount = 0f;
    }
    private void ProgressBarCircle(int number)
    {
        circleCapacity = 1f / number;
        circleImg.fillAmount += circleCapacity;
       

        if (circleImg.fillAmount.Equals(1))
        {
            holeGameObj.localScale += new Vector3(0.5f, 0f, 0.5f);
            circleImg.fillAmount = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            ProgressBarCircle(10);
            other.gameObject.SetActive(false); // return to pool
        }
    }
}
