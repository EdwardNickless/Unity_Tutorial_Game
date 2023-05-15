using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class SceneScript : MonoBehaviour
{
    // Player references
    private Object playerRef; // Prefab reference
    private GameObject player;
    private PlayerScript playerScript;


    //Skeleton references
    private Object skeletonRef;
    private GameObject skeleton;
    private SkeletonScript skeletonScript;
    private float skeletonSpawnDelay;


    // Conditional bools
    internal bool playerIsAlive;
    internal bool skeletonIsAlive;

    //===================================================================================================//
    //                                       Start and Update Functions                                  //
    //===================================================================================================//

    // Start is called before the first frame update
    void Start()
    {
        // Load the prefabs
        playerRef = Resources.Load("Prefabs/Player/Doom Player");
        skeletonRef = Resources.Load("Prefabs/Enemies/Skeleton/Skeleton");

        // Instantiate the Player and skeleton at the start of the game
        LoadPlayer();
        LoadSkeleton();

        // Set the time waited before the skeleton respawns
        skeletonSpawnDelay = 3f;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerScript.GetStatus();


            if (skeleton)
            {
                skeletonScript.GetStatus();
            }
            else if (!skeletonIsAlive)
            {
                // Respawns the skelton after 5 seconds
                skeletonIsAlive = true;
                StartCoroutine(RespawnSkeleton());
            }
        }
    }

    //===================================================================================================//
    //                                         Player Code                                               //
    //===================================================================================================//

    // Load and initialise the player
    private void LoadPlayer()
    {
        // Initialise the playerIsAlive bool
        playerIsAlive = true;

        // Instantiate the GameObject
        player = (GameObject)Instantiate(playerRef, transform.position, transform.rotation);

        // Initialise Player script reference
        playerScript = player.GetComponent<PlayerScript>();

        // Instantiate the starting location
        player.transform.position = new Vector3(-4.5f, -.75f, 0);
    }

    //===================================================================================================//
    //                                        Skeleton Code                                              //
    //===================================================================================================//

    // Load and initialise the skeleton
    private void LoadSkeleton()
    {
        // Initialise the skeletonIsAlive bool
        skeletonIsAlive = true;
        
        // Instantiate the GameObject
        skeleton = (GameObject)Instantiate(skeletonRef, transform.position, transform.rotation);

        // Link skeletonScript to this Skeleton
        skeletonScript = skeleton.GetComponent<SkeletonScript>();
        skeletonScript.combatScript.attackOne.SetActive(false);
        skeletonScript.combatScript.attackTwo.SetActive(false);

        // Instantiate a random starting location within a certain range of 5.5f, .5f, 0 (x, y, z)
        skeleton.transform.position = new Vector3(UnityEngine.Random.Range(3.5f, 7.5f),
                                                  UnityEngine.Random.Range(-2.5f, 1.5f),
                                                  0);

        //Initialise Skeleton variables
        skeleton.GetComponent<SkeletonCombatScript>().InitialiseSkeleton();
    }

    IEnumerator RespawnSkeleton()
    {
        yield return new WaitForSeconds(skeletonSpawnDelay);
        LoadSkeleton();
    }
}
