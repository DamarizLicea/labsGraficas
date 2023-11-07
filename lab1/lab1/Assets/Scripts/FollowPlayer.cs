using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This follow player class will update the camera position to follow the vehicle player.
/// Standar coding documentarion can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0,6,-7);


    ///<summary>
    // Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }
    /// <summary>
    // Update is called once per frame
    /// </summary>
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
