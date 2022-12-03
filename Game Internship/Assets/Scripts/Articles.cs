using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script contains all of the information used to populate newspaper articles for the end of the day
public class Articles
{
    //These variables are used to 
    public string[] GetNewspaperInfo(int day, bool wasPlayerCorrect)
    {
        switch (day)
        {
            //Our days are 0-indexed
            case 0:
                return dayOne(wasPlayerCorrect);
            case 1:
                return dayTwo(wasPlayerCorrect);
            case 2:
                break;
            case 3:
                return dayFour(wasPlayerCorrect);
        }

        //This should never happen. If an empty newspaper shows up something has gone wrong.
        Debug.LogWarning("Article.cs: empty string returned due to switch statement fallthrough.");
        return new string[] { "", "", ""};
    }

    #region dayTemplate
    private string[] dayX(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "", //Header
                "", //Deck
                ""
            };
        }
        else
        {
            return new string[]
            {
                "", //Header
                "", //Deck
                ""
            };
        }
    }
    #endregion

    #region day methods
    private string[] dayOne(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "CITY SAVED FROM CORRUPTION", //Header
                "CANDIDATE B RETURNS MILLIONS", //Deck
                "Recent investigations into the two mayoral candidates' affairs " + //Article
                "has revealed Candidate B received unlawful funding from Nefaripolis " +
                "Builders United. The candidate is now being forced by the city to " +
                "donate the 2.5 million dollars he received to the city's social services " +
                "department, to support local parks and shelters."
            };
        }
        else
        {
            return new string[]
            {
                "RICH PARK YACHTS WHILE WE STARVE", //Header
                "CORRUPTION REACHES ALL-TIME HIGH", //Deck
                "Many citizens of Nefaripolis are unhappy with recent legislation. " + //Article
                "Funding for many social services has been reduced to give give tax " +
                "cuts to the rich. A bill allocating 25% of Nefaripolis' remaining " +
                "green space to be converted into yacht parking also passed following " +
                "massive lobbying from yacht venders and their patrons. Many pantries " +
                "and shelters are have been forced to close their doors, causing many to go hungry."
            };
        }
    }

    private string[] dayTwo(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "EXONERATED HERO RETURNS ICE CREAM", //Header
                "CUSTARD CLEARD OF ALL CHARGES", //Deck
                "Buster Custard has been released following his retrial. Custard receieved " + //Article
                "this retrial thanks to new evidence uncovered by the hosts of the popular " +
                "podcast Cereal. The retrial found there was not enough evidence to convict " +
                "Custard of the murder of his girlfriend Mary Jane. Custard's recent FacePage " +
                "post shows him celebrating with Young Franklin star Zane Legitage"
            };
        }
        else
        {
            return new string[]
            {
                "SPRINKLE KILLER REMAINS AT LARGE", //Header
                "POLICED BAFFLED BY NEW MUDERS", //Deck
                "The infamous monster known only as \"The Sprinkle Killer\" has claimed another " + //Article
                "victim. Young Franklin star Zane Legitage was found murdered in his home with " +
                "the telltale signs of the Sprinkle Killer left behind at the scene. Zane makes " +
                "for the killer's third confirmed victim, though some believe there is also a " +
                "connection to the murder of Mary Jane, despite her boyfriend's conviction."
            };
        }
    }

    //Day 3 not written yet
    private string[] dayFour(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "BOOK AUDIT FINDS BIAS IN TEXTBOOK", //Header
                "NEW CURRICULUM APPROVED", //Deck
                "Review of the latest edition of the NP Official Textbook found high " + //Article
                "amounts of biased information being presented. Many of the textbook's " +
                "sources were found to be giving biased accounts of events and our history. " +
                "Lawmakers are now working with teachers to create a curriculum using " +
                "objective first and secondary historical sources."
            };
        }
        else
        {
            return new string[]
            {
                "CHILDREN UNSURE OF FACT OR FICTION", //Header
                "SCHOOL CURRICULUM TO BLAME", //Deck
                "A survey of Nefaripolis school children about the history of our city has " +
                "revealed that many youths believe falsehoods about historical events and " +
                "figures. Students are certain Nefaripolis has no faults and is the greatest " +
                "city in the world, stating their school classes have taught them everything " +
                "they need to know about their history. Students were found to have no " +
                "awareness of several unfortunate past city oversights and failures."
            };
        }
    }
    #endregion
}
