//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class KeepInScreen : MonoBehaviour {

    [Range(0, 100f)]
    public float widthPercentage = 50f;
    [Range(0, 100f)]
    public float heightPercentage = 50f;

    private float widthConst => widthPercentage / 100f;
    private float heightConst => heightPercentage / 100f;

    // Update is called once per frame
    private void Update() {
        keepIn(Screen.width, Screen.height);
    }

    private void keepIn(int width, int height) {
        gameObject.transform.position = new Vector3 (width * widthConst,  height * heightConst, 0);
    }
}
