using UnityEngine;

public class Movement : MonoBehaviour
{

    // Parameters
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float rcsThrust  = 50f;
   
    [SerializeField] AudioClip mainEngine;   
    
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rcsLeftParticles;
    [SerializeField] ParticleSystem rcsRightParticles;

    // Cache
    Rigidbody rigidBody;
    AudioSource audioSource;
    AudioController audioController;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

       private void ProcessThrust()
    {       
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
      private void StartThrusting()
    {
        rigidBody.AddForce(transform.up * mainThrust);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrusterParticles.isPlaying)
        {
            //Instantiate(mainThrusterParticles,transform.position,transform.rotation);
            mainThrusterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

    private void ProcessRotation()
    {   
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rcsThrust);
        if (!rcsRightParticles.isPlaying)
        {
            rcsRightParticles.Play();

        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rcsThrust);
        if (!rcsLeftParticles.isPlaying)
        {
            rcsLeftParticles.Play();
        }
    }

    private void StopRotating()
    {
        rcsLeftParticles.Stop();
        rcsRightParticles.Stop();
    }

  
}
