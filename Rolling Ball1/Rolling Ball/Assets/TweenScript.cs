using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenScript : MonoBehaviour
{
    public Transform btnmoves;
    private float _cyclelenght = 2;
    private void Start()
    {
        transform.DOMove(btnmoves.position, _cyclelenght);
        //transform.DOMoveX(100, 1);
        
    }
    
}
