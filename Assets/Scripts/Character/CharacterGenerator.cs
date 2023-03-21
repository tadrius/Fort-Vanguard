using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{

    [SerializeField] PartSelector head;
    [SerializeField] List<GameObject> headChoices;

    [SerializeField] PartSelector mustache;
    [SerializeField] List<GameObject> mustacheChoices; 

    [SerializeField] PartSelector beard;
    [SerializeField] List<GameObject> beardChoices; 

    [SerializeField] PartSelector headwear;
    [SerializeField] List<GameObject> headwearChoices;    

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

    [SerializeField] PartSelector leftItem;
    [SerializeField] List<GameObject> leftItemChoices;   
    [SerializeField] PartSelector rightItem;
    [SerializeField] List<GameObject> rightItemChoices;  

    void Awake() {
        head.SetPossibleParts(headChoices);
        mustache.SetPossibleParts(mustacheChoices);
        beard.SetPossibleParts(beardChoices);
        headwear.SetPossibleParts(headwearChoices);
        chest.SetPossibleParts(chestChoices);
        leftUpperArm.SetPossibleParts(upperArmChoice);
        rightUpperArm.SetPossibleParts(upperArmChoice);
        leftForeArm.SetPossibleParts(forearmChoices);
        rightForeArm.SetPossibleParts(forearmChoices);
        leftHand.SetPossibleParts(handChoices);
        rightHand.SetPossibleParts(handChoices);
        leftLeg.SetPossibleParts(legChoices);
        rightLeg.SetPossibleParts(legChoices);
        leftItem.SetPossibleParts(leftItemChoices);
        rightItem.SetPossibleParts(rightItemChoices);

        // create parts
        head.CreateRandomPart();
        mustache.CreateRandomPart();
        beard.CreateRandomPart();
        headwear.CreateRandomPart();

        chest.CreateRandomPart();

        rightItem.CreateRandomPart();
        leftItem.CreateRandomPart();

        int index;
        index = leftUpperArm.CreateRandomPart();
        rightUpperArm.CreatePart(index); // create mirroring part

        index = leftForeArm.CreateRandomPart();
        rightForeArm.CreatePart(index);

        index = leftHand.CreateRandomPart();
        rightHand.CreatePart(index);

        index = leftLeg.CreateRandomPart();
        rightLeg.CreatePart(index);
    }
}
