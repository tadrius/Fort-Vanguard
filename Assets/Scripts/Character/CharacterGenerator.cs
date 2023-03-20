using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{

    [SerializeField] PartSelector head;
    [SerializeField] List<GameObject> headChoices; 

    [SerializeField] PartSelector chest;
    [SerializeField] List<GameObject> chestChoices; 

    [SerializeField] PartSelector leftUpperArm;
    [SerializeField] PartSelector rightUpperArm;
    [SerializeField] List<GameObject> upperArmChoice; 


    [SerializeField] PartSelector leftForeArm;
    [SerializeField] PartSelector rightForeArm;
    [SerializeField] List<GameObject> forearmChoices; 


    [SerializeField] PartSelector leftHand;
    [SerializeField] PartSelector rightHand;
    [SerializeField] List<GameObject> handChoices; 

    [SerializeField] PartSelector leftLeg;
    [SerializeField] PartSelector rightLeg;
    [SerializeField] List<GameObject> legChoices; 


    void Awake() {
        head.SetPossibleParts(headChoices);
        chest.SetPossibleParts(chestChoices);
        leftUpperArm.SetPossibleParts(upperArmChoice);
        rightUpperArm.SetPossibleParts(upperArmChoice);
        leftForeArm.SetPossibleParts(forearmChoices);
        rightForeArm.SetPossibleParts(forearmChoices);
        leftHand.SetPossibleParts(handChoices);
        rightHand.SetPossibleParts(handChoices);
        leftLeg.SetPossibleParts(legChoices);
        rightLeg.SetPossibleParts(legChoices);

        head.CreateRandomPart();
        chest.CreateRandomPart();

        GameObject upperArm = leftUpperArm.CreateRandomPart();
        rightUpperArm.CreatePart(upperArm);

        GameObject forearm = leftForeArm.CreateRandomPart();
        rightForeArm.CreatePart(forearm);

        GameObject hand = leftHand.CreateRandomPart();
        rightHand.CreatePart(hand);

        GameObject leg = leftLeg.CreateRandomPart();
        rightLeg.CreatePart(leg);
    }
}
