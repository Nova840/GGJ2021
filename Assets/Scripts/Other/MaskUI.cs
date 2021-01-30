using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskUI : MonoBehaviour {

    [SerializeField]
    private Transform shardContainer = default;

    [SerializeField]
    private GameObject shardPrefab = default;

    [SerializeField]
    private float spaceBetweenImages = 30;

    private void Start() {
        for (int i = 0; i < MaskPart.NumberOfMaskPartsInScene(); i++) {
            GameObject shard = Instantiate(shardPrefab, shardContainer);
            RectTransform rt = shard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(spaceBetweenImages * i, 0);
        }
    }

}