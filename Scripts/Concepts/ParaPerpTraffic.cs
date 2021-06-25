using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParaPerpTraffic : MonoBehaviour
{
    public GameObject[] scenarioCW;
    public GameObject[] scenarioCCW;
    public GameObject[] q;
    public GameObject[] qq;
    string purpose;

    int learnerState;
    int learnerStateNovice = 0, learnerStateJourneyman = 1, learnerStateExpert = 2;

    int age;
    int ageChild = 1, ageAdult = 2;

    int rand, i , j;

    int[] UsedIndex;

    public void SelectScenarios(GameObject[] apprSc, int size, int[] used, GameObject[] Sc)
    {
        for (i = 0; i < size; i++)
        {
            rand = Random.Range(0, apprSc.Length); //picking the random index to pick from appr scenarios block
            Sc[i] = apprSc[rand];
            used[used.Length] = rand;
            for (j = rand - 1; j < apprSc.Length; j++)
            {
                apprSc[j] = apprSc[j + 1];
            }
        }
    }

    void run()
    {
        SelectScenarios(TrafficIntersections.ti.appropriateCWScenario, 2, UsedIndex, scenarioCW);
        SelectScenarios(TrafficIntersections.ti.appropriateCCWScenario, 2, UsedIndex, scenarioCCW);
        //in pseudo code it is scw = selectScenarios(apCWsc, size, used) but instead scw is being passed into the function as well
        q = scenarioCW.Concat(scenarioCCW).ToArray();

        if (purpose == "purposeInstruction")
        {
            if (learnerState == learnerStateNovice && age == ageChild)
            {
                //trying to figure out how to do a QQ block
                for (i = 0; i < qq.Length; i++)
                {

                }
            }
        }


    }
}
