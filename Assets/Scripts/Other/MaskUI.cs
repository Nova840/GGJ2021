using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour {

    [SerializeField]
    private Transform shardContainer = default;

    [SerializeField]
    private GameObject shardPrefab = default;

    [SerializeField]
    private Sprite pickedUpShardSprite = default;//not picked up is what prefab already has

    [SerializeField]
    private float spaceBetweenImages = 30;

    [SerializeField]
    private Image completionFillImage = default;

    private List<GameObject> shards = new List<GameObject>();

    private static MaskUI instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Refresh();
    }

    public static void Refresh() {
        while (instance.shards.Count > 0) {
            Destroy(instance.shards[0]);
        }
        instance.shards.Clear();

        int numPartsPickedUp = MaskPart.NumberOfPickedUpMaskPartsInScene();
        for (int i = 0; i < MaskPart.NumberOfMaskPartsInScene(); i++) {
            GameObject shard = Instantiate(instance.shardPrefab, instance.shardContainer);
            RectTransform rt = shard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(instance.spaceBetweenImages * i, 0);
            if (i < numPartsPickedUp) {
                rt.GetComponent<Image>().sprite = instance.pickedUpShardSprite;
            }
        }

        float sumPercents = 0;
        HubManager.ForEachLevelName(ln => {
            sumPercents += PlayerPrefs.GetFloat(ln + " % Complete", 0);
        });
        instance.completionFillImage.fillAmount = sumPercents / HubManager.NumberOfLevels();
    }

}