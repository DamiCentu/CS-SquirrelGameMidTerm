using UnityEngine;
using System.Collections;
using System;

public class QuestionNodeBeetle : NodeBeetle {

    public Questions question;
    public NodeBeetle trueNode;
    public NodeBeetle falseNode;

    public enum Questions {
        InSight
    }

    public override void Execute(BeetleBehaviur reference)
    {
        switch (question)
        {
            //case Questions.InSight:
            //    if (reference.lineOfSight.IsInSight) {

            //    }
            //    else {
                    
            //    }
            //    break;
            //case Questions.HEARSCREAM:
            //    if (reference.HearScream())
            //    {
            //        //print("Escuche un ruido");
            //        trueNode.Execute(reference);
            //    }
            //    else
            //    {
            //        //print("NO Escuche un ruido");
            //        falseNode.Execute(reference);
            //    }
            //    break;
            //case Questions.NOISEREACHED:
            //    if (reference.reachDestination)
            //    {
            //        // print("llegue al sonido");
            //        trueNode.Execute(reference);
            //    }
            //    else
            //    {
            //        //print("no llegue al sonido");
            //        falseNode.Execute(reference);
            //    }
            //    break;
            //case Questions.RETURNINGREACH:
            //    if (reference.reachDestination)
            //    {
            //        // print("Volvi A la ruta normal");
            //        trueNode.Execute(reference);
            //    }
            //    else
            //    {
            //        // print("No Volvi A la ruta normal");
            //        falseNode.Execute(reference);
            //    }
            //    break;
        }
    }
}