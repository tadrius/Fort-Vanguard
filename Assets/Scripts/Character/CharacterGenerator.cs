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
    [SerializeField] List<GameObject> rightItemChoices;
    [SerializeField] List<GameObject> leftItemChoices;   

    [SerializeField] PartSelector head;
    [SerializeField] PartSelector mustache;
    [SerializeField] PartSelector beard;
    [SerializeField] PartSelector headwear;
    [SerializeField] PartSelector chest;
    [SerializeField] PartSelector rightUpperArm;
    [SerializeField] PartSelector leftUpperArm;
    [SerializeField] PartSelector rightForeArm;
    [SerializeField] PartSelector leftForeArm;
    [SerializeField] PartSelector rightHand;
    [SerializeField] PartSelector leftHand;
    [SerializeField] PartSelector rightLeg;
    [SerializeField] PartSelector leftLeg;
    [SerializeField] PartSelector rightItem;
    [SerializeField] PartSelector leftItem;

    void Awake() {
        head.SetPossibleParts(headChoices);
        mustache.SetPossibleParts(mustacheChoices);
        beard.SetPossibleParts(beardChoices);
        headwear.SetPossibleParts(headwearChoices);
        chest.SetPossibleParts(chestChoices);
        rightUpperArm.SetPossibleParts(upperArmChoice);
        leftUpperArm.SetPossibleParts(upperArmChoice);
        rightForeArm.SetPossibleParts(forearmChoices);
        leftForeArm.SetPossibleParts(forearmChoices);
        rightHand.SetPossibleParts(handChoices);
        leftHand.SetPossibleParts(handChoices);
        rightLeg.SetPossibleParts(legChoices);
        leftLeg.SetPossibleParts(legChoices);
        rightItem.SetPossibleParts(rightItemChoices);
        leftItem.SetPossibleParts(leftItemChoices);

        // create parts
        head.CreateRandomPart(palette);
        mustache.CreateRandomPart(palette);
        beard.CreateRandomPart(palette);
        headwear.CreateRandomPart(palette);

        chest.CreateRandomPart(palette);

        rightItem.CreateRandomPart(palette);
        leftItem.CreateRandomPart(palette);

        int index;
        index = rightUpperArm.CreateRandomPart(palette);
        leftUpperArm.CreatePart(index, palette); // create mirroring part

        index = rightForeArm.CreateRandomPart(palette);
        leftForeArm.CreatePart(index, palette);

        index = rightHand.CreateRandomPart(palette);
        leftHand.CreatePart(index, palette);

        index = rightLeg.CreateRandomPart(palette);
        leftLeg.CreatePart(index, palette);
    }
}
