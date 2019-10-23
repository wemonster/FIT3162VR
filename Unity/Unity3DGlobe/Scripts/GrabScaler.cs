using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
public class GrabScaler : MonoBehaviour
{
    public bool CanMove = false;

    public StateClick stateClick;

    private int HandsAttached = 0;

    //public Hand Left;
    //public Hand Right;

    private float CurrentDistance = 0f;
    private float OldMagnitude = 1f;

    private Vector3 OldScale;

    private Vector3 OldLocation;
    private float OldAngle;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags;

    private Interactable interactable;

    private List<Hand> attachedHands;
    private GameObject pointer;

    //public GameObject head;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        attachedHands = new List<Hand>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        //Debug.Log("Hand is hovering! " + hand.handType.ToString());
        GrabTypes startingGrabType = hand.GetGrabStarting();
        GrabTypes endingGrabType = hand.GetGrabEnding();
        //Debug.Log(startingGrabType + " " + endingGrabType);
        //bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        Hand other = hand.otherHand;

        //if(attachedHands.)

        // primary hover Hand grab initiate (only one Hand is registered as hovering at a time)
        if (!attachedHands.Contains(hand) && startingGrabType != GrabTypes.None)
        {
            //Debug.Log("Attached!");
            hand.HoverLock(interactable);
            AttachHand(hand);
        }

        // primary Hand stops grabbing
        else if (attachedHands.Contains(hand) && endingGrabType != GrabTypes.None)
        {
            //Debug.Log("Detached!");
            hand.HoverUnlock(interactable);
            DetachHand();

            // secondary Hand can't send hover updates so both must be detached
            HandsAttached = 0;
            attachedHands.Clear();

            //attachedHands.Remove(hand);
            //other.HoverLock(interactable);

        }

        // secondary Hand (if the other Hand is trying to hover the same Interactable as the first Hand)
        if (other)
        {
            if (other.isOverlapping)
            {
                //Debug.Log("Overlapping with " + other.handType);

                // secondary Hand grab initiate
                if (!attachedHands.Contains(other) && other.GetGrabStarting() != GrabTypes.None)
                {
                    //Debug.Log("Attached other!");
                    //hand.HoverLock(interactable);
                    AttachHand(other);
                }

                //HandHoverUpdate(hand.otherHand);
            }

            // secondary Hand won't send updates because it isn't hover locked so must manually check
            if (attachedHands.Contains(other) && other.GetGrabEnding() != GrabTypes.None)
            {
                //Debug.Log("Detached other!");
                DetachHand();
                attachedHands.Remove(other);
            }
        }
    }

    public void AttachHand(Hand hand)    
    {
        HandsAttached++;
        attachedHands.Add(hand);
        if (HandsAttached == 2)
        {
            //HandsAttached = 2;
            //Debug.Log("Two hands attached!");

            // the current distance between the two hands - used as a base point for scaling
            CurrentDistance = Vector3.Distance(hand.transform.position, hand.otherHand.transform.position);

            // the old size of the object
            OldMagnitude = transform.localScale.magnitude;
            OldScale = transform.localScale;

            // old position of object relative to the centre between the two hands
            OldLocation = transform.position - 0.5f * (attachedHands[0].transform.position + attachedHands[1].transform.position);

            OldAngle = Vector3.Angle(OldLocation, attachedHands[0].transform.position - attachedHands[1].transform.position);

            // disable the sphere collider to reduce collision lag
            transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;

            pointer = new GameObject("Grabber Reference");
            pointer.transform.localPosition = 0.5f * (attachedHands[0].transform.position + attachedHands[1].transform.position);
            pointer.transform.forward = attachedHands[0].transform.forward + attachedHands[1].transform.forward;
            pointer.transform.parent = transform.parent;
            transform.parent = pointer.transform;
            OldLocation = transform.localPosition;
        }
    }

    public void DetachHand()
    {
        if (HandsAttached == 2)
        {
            transform.GetChild(0).GetComponent<SphereCollider>().enabled = true;
            transform.parent = pointer.transform.parent;
            Destroy(pointer);
            stateClick.UpdateArcs();
        }
        HandsAttached--;
        if (HandsAttached == 0)
        {
            //HandsAttached = 0;
            //Debug.Log("No hands attached!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HandsAttached == 2)
        {
            // find the new scale multiplier, which is based on the ratio of distance between the two hands now compared to when scaling started
            float NewMagnitudeRatio = Vector3.Distance(attachedHands[0].transform.position, attachedHands[1].transform.position) / CurrentDistance;
            //transform.localScale = (transform.localScale * NewScale * OldMagnitude) / transform.localScale.magnitude;
            //transform.localScale = OldScale * NewMagnitudeRatio;
            pointer.transform.localScale = new Vector3(NewMagnitudeRatio, NewMagnitudeRatio, NewMagnitudeRatio);
            //transform.rotation = new Quaternion()
            //var head_location = SteamVR_Render.instance.transform.position;
            //var head_location = new Vector3(0, 0, 0);
            //Debug.Log(head_location);
            //var direction = Vector3.Normalize(transform.localPosition - head_location);
            //var hand_centre = 0.5f * (attachedHands[0].transform.position + attachedHands[1].transform.position);
            //transform.localPosition = hand_centre + OldLocation * (transform.localScale.magnitude / OldMagnitude);



            // important lines
            if (CanMove)
            {
                pointer.transform.localPosition = 0.5f * (attachedHands[0].transform.position + attachedHands[1].transform.position);// * NewMagnitudeRatio;
                pointer.transform.forward = attachedHands[0].transform.forward + attachedHands[1].transform.forward;
            }



            //transform.localPosition = OldLocation /** pointer.transform.localScale.magnitude;/*/ / OldLocation.magnitude;
            //transform.localPosition *= (float) System.Math.Sqrt(NewMagnitudeRatio);
            //var hands_dir = attachedHands[0].transform.position - attachedHands[1].transform.position;
            //transform.RotateAround(Vector3.Cross(hands_dir, OldLocation), OldAngle - Vector3.Angle(hands_dir, OldLocation));
            //OldLocation = transform.position - hand_centre;
            //transform.Rotate()

            //var new_direction = Vector3.Cross(attachedHands[0].transform.position - attachedHands[1].transform.position, Vector3.up);
            //new_direction.Normalize();
            //Debug.Log(new_direction);
            //transform.localPosition = hand_centre + OldLocation.magnitude * NewMagnitudeRatio * new_direction;
            //Quaternion quaternion = new Quaternion();
            //quaternion.SetFromToRotation(OldLocation, new_direction);
            //transform.rotation = quaternion * transform.rotation;
        }
    }
}
