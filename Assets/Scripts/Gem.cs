﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Gem : MonoBehaviour {

    public Pedestal skull1;
    public Pedestal skull2;
    public GameObject gravityTrigger;
    public bool collected;

    private MeshRenderer meshRenderer;
    private new Light light;
    private Vector3 skullPos;

    void Start () {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        light = transform.FindChild("Light").gameObject.GetComponent<Light>();
        skullPos = new Vector3(0.00127f, 0.0013f, 0.00802f);
    }

    void Update () {
        if (collected) {
            // Player is at the first skull
            if (skull1.hasPlayer) {
                // Player places the gem
                if(Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.X)) {
                    collected = false;
                    transform.SetParent(skull1.gameObject.transform);
                    transform.localPosition = skullPos;
                    meshRenderer.enabled = true;
                    light.intensity = 2f;
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press X to Pick Up", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Pick Up", 100);
                }
            }

            // Player is at the second skull
            if (skull2.hasPlayer) {
                // Player places gem
                if(Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.X)) {
                    collected = false;
                    transform.SetParent(skull2.gameObject.transform);
                    transform.localPosition = skullPos;
                    meshRenderer.enabled = true;
                    light.intensity = 2f;
                    gravityTrigger.SetActive(false); // The puzzle is solved
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press X to Pick Up", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Pick Up", 100);
                }
            }
        } else {
            // Gem is in first skull & player is at first skull
            if (transform.parent.name == "Pedestal1" && skull1.hasPlayer) {
                // Pick up gem
                if (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.X)) {
                    collected = true;
                    transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                    transform.localPosition = Vector3.zero;
                    meshRenderer.enabled = false;
                    light.intensity = .5f;
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press X to Place Object", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Place Object", 100);
                }
            }
            // Gem is in second skull & player is at second skull
            if (transform.parent.name == "Pedestal2" && skull2.hasPlayer) {
                // Pick up gem
                if (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.X)) {
                    collected = true;
                    transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                    transform.localPosition = Vector3.zero;
                    meshRenderer.enabled = false;
                    light.intensity = .5f;
                    gravityTrigger.SetActive(true); // The puzzle is not
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press X to Place Object", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Place Object", 100);
                }
            }
        }
    }
}