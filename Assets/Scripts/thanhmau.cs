using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thanhmau : MonoBehaviour
{
   
    public Slider _slider;
    
    public Image _fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _slider.value++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _slider.value--;
        }
    }
}
