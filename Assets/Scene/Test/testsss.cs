using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testsss : MonoBehaviour
{
    [SerializeField] private Button a;
    private PointerEventData testc { get {
            Debug.Log("a");
            return testc;
                } }

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        a.OnPointerDown(testc);
    }

}
