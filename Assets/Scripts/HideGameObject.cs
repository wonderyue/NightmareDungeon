using System;
using UnityEngine;

public class HideGameObject : MonoBehaviour
{
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}