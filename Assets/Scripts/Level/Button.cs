using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    private bool canPlayerPush = true;

    [SerializeField]
    private bool canRedBlockPush = true;

    [SerializeField]
    private bool canYellowBlockPush = true;

    [SerializeField]
    private bool canGreenBlockPush = true;

    [SerializeField]
    private MovingThing[] thingsToTurnOn = new MovingThing[0];

    private int numThingsInTrigger = 0;

    public void OnTriggerEnter(Collider other) {
        if (IsPushable(other.gameObject)) {
            numThingsInTrigger++;
            if (numThingsInTrigger == 1) {
                foreach (MovingThing thing in thingsToTurnOn) {
                    thing.TurnOn();
                }
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (IsPushable(other.gameObject)) {
            numThingsInTrigger--;
            if (numThingsInTrigger == 0) {
                foreach (MovingThing thing in thingsToTurnOn) {
                    thing.TurnOff();
                }
            }
        }
    }

    private bool IsPushable(GameObject g) {
        if (g.layer == LayerMask.NameToLayer("Player") && canPlayerPush) {
            return true;
        } else if (g.layer == LayerMask.NameToLayer("Block")) {
            Block block = g.GetComponentInParent<Block>();
            if (block.Type == Block.BlockType.Red && canRedBlockPush) {
                return true;
            } else if (block.Type == Block.BlockType.Yellow && canYellowBlockPush) {
                return true;
            } else if (block.Type == Block.BlockType.Green && canGreenBlockPush) {
                return true;
            }
        }
        return false;
    }

}