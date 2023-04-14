using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;


public class Collectible : MonoBehaviour
{
  

    #region Public Fields



    public bool isInPlayer = false;
    public bool isLastCollectible = false;
    public GameObject _lastCollectible;
    public GameObject _Collectible;
    public Rigidbody playerRb;

    public float jumpForce = 10f;



    #endregion

    #region Private Fields

    private Collider _objCollider;
    private List<GameObject> _collectibles;
    private Rigidbody _objRigidbody;

    #endregion

    #region Serialized Fields

    [SerializeField] private float _distanceBetweenCollectibles = 1.2f;

  

    #endregion


    #region Unity Methods

    private void Awake()
    {
        _objCollider = GetComponent<Collider>();
        _objRigidbody = GetComponent<Rigidbody>();
       

        DOTween.Init();
    }

    private void Start()
    {
        _collectibles = PlayerController1.Instance.collectibles;

    }

    private void FixedUpdate()
    {
        if (isInPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, _lastCollectible.transform.position,
                10 * Time.fixedDeltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y,
                _lastCollectible.transform.position.z + _distanceBetweenCollectibles);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectible otherCollectible = other.GetComponent<Collectible>();


        if (other.CompareTag("Player") && !isInPlayer)
        {
            rotatehead();
            _objCollider.isTrigger = false;
            Destroy(_objRigidbody);


            if (_collectibles.Count == 0)
            {
                _lastCollectible = PlayerController1.Instance.gameObject;
                _collectibles.Add(gameObject);
                isLastCollectible = true;
            }
            else
            {
                _lastCollectible = _collectibles[_collectibles.Count - 1];
                _lastCollectible.GetComponent<Collectible>().isLastCollectible = false;

                _collectibles.Add(gameObject);
                isLastCollectible = true;
            }


            isInPlayer = true;
        }
       
        if (other.gameObject.CompareTag("Player"))
        {

           
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump here");
        }
















    }

    #endregion

    #region Private Methods

    

 
    private void rotatehead()
    {

        this.transform.DORotate(new Vector3(0, 0, 0), 1, RotateMode.Fast).SetEase(Ease.Linear);

    }
    #endregion


}