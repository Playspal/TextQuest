using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter
{
    private GameObject _model;
    private Transform _modelTransform;

    private float _x;
    private float _xTarget = 0;
    private float _y = 0;
    
    private float _speedX = 1f;
    private float _speedY = 1f;

    private float _idle = 1;
    
    public GameCharacter()
    {
        _model = GameObject.Instantiate
        (
            Resources.Load<GameObject>("Prefabs/Characters/Character01"),
            GameLayers.GetLayerCharacters()
         );
         
        _modelTransform = _model.transform;

        _x = Random.Range(-3f, 3f);
        _xTarget = Random.Range(-3f, 3f);
        _idle = Random.Range(1f, 5f);

        _speedX = Random.Range(0.85f, 1.15f);
        _speedY = Random.Range(1.5f, 2.5f);
    }
    
    public void Update()
    {
        if (_idle <= 0)
        {
            UpdateWalking();
        }
        else
        {
            _idle -= Time.deltaTime;
        }
    
        _y = Mathf.Cos(Time.realtimeSinceStartup * 10 * _speedY) / 20f;
    
        _modelTransform.localPosition = new Vector3(_x, _y, 0);
    }
    
    private void UpdateWalking()
    {
        if(Mathf.Abs(_xTarget - _x) < _speedX * Time.deltaTime)
        {
            _xTarget = Random.Range(-3f, 3f);
            _idle = Random.Range(1f, 5f);
        }
    
        if(_x < _xTarget)
        {
            _x += _speedX * Time.deltaTime;
        }
        
        if(_x > _xTarget)
        {
            _x -= _speedX * Time.deltaTime;
        }
    }
}
