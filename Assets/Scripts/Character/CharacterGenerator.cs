using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{

    [SerializeField] Material palette;

    [SerializeField] List<GameObject> headChoices;
    [SerializeField] List<GameObject> mustacheChoices; 
    [SerializeField] List<GameObject> beardChoices; 
    [SerializeField] List<GameObject> headwearChoices;    
    [SerializeField] List<GameObject> chestChoices; 
    [SerializeField] List<GameObject> upperArmChoice; 
    [SerializeField] List<GameObject> forearmChoices; 
    [SerializeField] List<GameObject> handChoices; 
    [SerializeField] List<GameObject> legChoices; 
    [SerializeField] List<GameObject> leftItemChoices;   
    [SerializeField] List<GameObject> rightItemChoices;

    [SerializeField] PartSelector head;
    [SerializeField] PartSelector mustache;
    [SerializeField] PartSelector beard;
    [SerializeField] PartSelector headwear;
    [SerializeField] PartSelector chest;
    [SerializeField] PartSelector leftUpperArm;
    [SerializeField] PartSelector rightUpperArm;
    [SerializeField] PartSelector leftForeArm;
    [SerializeField] PartSelector rightForeArm;
    [SerializeField] PartSelector leftHand;
    [SerializeField] PartSelector rightHand;
    [SerializeField] PartSelector leftLeg;
    [SerializeField] PartSelector rightLeg;
    [SerializeField] PartSelector leftItem;
    [SerializeField] PartSelector rightItem;

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
        head.CreateRandomPart(palette);
        mustache.CreateRandomPart(palette);
        beard.CreateRandomPart(palette);
        headwear.CreateRandomPart(palette);

        chest.CreateRandomPart(palette);

        rightItem.CreateRandomPart(palette);
        leftItem.CreateRandomPart(palette);

        int index;
        index = leftUpperArm.CreateRandomPart(palette);
        rightUpperArm.CreatePart(index, palette); // create mirroring part

        index = leftForeArm.CreateRandomPart(palette);
        rightForeArm.CreatePart(index, palette);

        index = leftHand.CreateRandomPart(palette);
        rightHand.CreatePart(index, palette);

        index = leftLeg.CreateRandomPart(palette);
        rightLeg.CreatePart(index, palette);
    }
}
