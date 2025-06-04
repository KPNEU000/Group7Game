using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door : MonoBehaviour
	{
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = -90.0f;
		float DoorCloseAngle = 0.0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		
    	public GameObject correctKey;
    	public bool openable;

		[Header("Audio")]
   		[SerializeField]
    	private AudioClip doorLocked;

		// Use this for initialization
		void Start()
		{
			asource = GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update()
		{
			if (open)
			{
				var target = Quaternion.Euler(0, DoorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);

			}
			else
			{
				var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);

			}
		}

		public void OpenDoor()
		{
			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
		}

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
        {
            foreach (GameObject key in other.GetComponent<PlayerMovement>().keys)
            {
                if (key == correctKey)
                {
                    openable = true;
                }
            }
            if (openable)
                {
                    open = true;
                    //other.GetComponent<PlayerMovement>().key = null;
                    //AudioSource.PlayClipAtPoint(doorOpened, transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(doorLocked, transform.position);
                }
        }
        }
    }
}