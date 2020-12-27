//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class Parallax : MonoBehaviour {

    public GameObject cam;
    public float effectMultiplier;
    public Vector2 offset;
    public bool ignoreY;
    
    private float _length, _startPos;
    private float camX => cam.transform.position.x;
    private float camY => cam.transform.position.y;
    private float currentZ => transform.position.z;
    private float currentY => transform.position.y;

    void Start() {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate() {
        var temp = camX * (1 - effectMultiplier);
        var dist = camX * effectMultiplier;

        transform.position = new Vector3(_startPos + dist + offset.x, ignoreY ? currentY : camY + offset.y, currentZ);
        if (temp > _startPos + _length) _startPos += _length;
        else if (temp <  _startPos - _length) _startPos -= _length;
    }
}
