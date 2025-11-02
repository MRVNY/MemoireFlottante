using System.Collections;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        private float currentSpeed;
        float distanceTravelled;
        
        Vector3 randomRotation;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            
            currentSpeed = speed;
            randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            StartCoroutine(Rotate());
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += currentSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        
        public void OnFlipUnder()
        {
            //slow down to 0
            StartCoroutine(SlowDown());
        }
        
        IEnumerator SlowDown()
        {
            float startSpeed = currentSpeed;
            float endSpeed = 0.5f;
            float duration = 0.5f;
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                currentSpeed = Mathf.Lerp(startSpeed, endSpeed, t / duration);
                yield return null;
            }

            currentSpeed = endSpeed;
        }

        IEnumerator Rotate()
        {
            while (true)
            {
                transform.Rotate(randomRotation * Time.deltaTime / 10f);
                yield return null;
            }
        }

        public void OnFlipUp()
        {
            currentSpeed = speed;
        }
    }
}