using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    // Parameters
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem fireParticles;

    // Cache
    AudioController audioController;
    Rigidbody _rigidbody;
    bool isTransitioning = false;
    public bool collisionDisabled = false;

    void Start()
    {
        audioController = GetComponent<AudioController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {

        if(isTransitioning || collisionDisabled) {return;} // Collision is disabled

        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":         
                StartCoroutine(StartSuccessSequence());
                break;
            default:
                StartCoroutine(StartCrashSequence());
                break;
        }
    }
    
    private IEnumerator StartCrashSequence()
    {
        isTransitioning = true;
        audioController.CrashAudio();
        crashParticles.Play();
        fireParticles.Play();
        _rigidbody.freezeRotation = false;
        GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(levelLoadDelay);
        ReloadLevel();        
    }

    private IEnumerator StartSuccessSequence()
    {
        isTransitioning = true;
        audioController.SuccessAudio();  
        successParticles.Play();
        GetComponent<Movement>().enabled = false;    
        yield return new WaitForSeconds(levelLoadDelay);
        LoadNextScene();
    }

    private void ReloadLevel()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }
        if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision on and off
            Debug.Log("Collisions Disabled");
        }
    }
}
