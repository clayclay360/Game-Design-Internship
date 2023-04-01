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
                return dayThree(wasPlayerCorrect);
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
                "LAYMAN'S JOURNAL", //Header
                "Citizens call for impeachment of Councilman Cameron Rook", //Deck
                "Overwhelming evidence has flooded in these past few days of " +
                "Councilman C. Rook’s connections to crimes throughout the city, as well " +
                "as a blatant disregard for the denizens of the Canton city-state. " +
                "Justifiably, the populus are disgusted with the actions of the councilman, " +
                "and the people’s demands are clear: Remove C. Rook!"
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "Councilman C. Rook running for a 5th term!", //Deck
                "Many citizens of Nefaripolis are unhappy with recent legislation. " + //Article
                "With civil unrest at an all time low, our beautiful utopia thrives! " +
                "In light of the successes of the council, Councilman Cameron Rook " +
                "has announced his campaign for the Legislative seat of the Canton city council!"
            };
        }
    }

    private string[] dayTwo(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "LAYMAN'S JOURNAL", //Header
                "Citizens gather outside Canton Police Station, cursing Canton Clubber.", //Deck
                "Citizens restlessly wait outside the Canton Police station this evening, " +
                "following the arrest of Police Sergeant Kevin Iller, the alleged Canton Clubber. " +
                "Many have come to protest the Police Station, claiming they could have done " +
                "more from the beginning to prevent these murders. Some are just concerned " +
                "with the punishment of the Canton Clubber, calling for capital punishment."
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "Kevin Iller released after detainment period.", //Deck
                "Following the arrest of Police Sergeant Kevin Iller last night, " +
                "the Canton Police Force has released Kevin Iller from the " +
                "precinct tonight, unable to find approval to extend the " +
                "detainment period for the alleged Canton Clubber. Kevin " +
                "Iller is to remain on paid leave until a verdict has been given on the case."
            };
        }
    }

    private string[] dayThree(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "THE LAYMAN'S ALMANAC", //Header
                "Many to blame for Acid Rain", //Deck
                "A recent investigation into Nefaropolis' acid jean wash factory invoked " + //Article
                "April showers bring May flowers all around the world, however in Canton, " +
                "they have now started to bring medical bills. Citizens look for someone " +
                "to blame, however there is no sole culprit for these frequent natural " +
                "disasters. Climate scientists believe that the rapid industrialization " +
                "of Canton to keep up with our sister country, as well as the lack of " +
                "solid regulations of these factories, have led to dangerous changes " +
                "in our environment, and endangered the public in exchange for profit and progress."
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "Trapezoid Pants Factory found to be cause of pollution!", //Deck
                "A recent investigation into the local acid jean wash factory found " + //Article
                "On March 9th, 2023, Canton had witnessed the most severe Acid Rainstorm " +
                "in world history, leaving over $2 million in damages and about 140 " +
                "injured. The government had immediately intervened to investigate " +
                "the cause of such a horrible natural catastrophe, and found Trapezoid " +
                "Pants Factory to be in multiple violations of Environmental and Work " +
                "safety. Matt Wiener, owner of Trapezoid Pants Factory, to stand trial in 6 months."
            };
        }
    }

    private string[] dayFour(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "THE LAYMAN'S ALMANAC", //Header
                "Canton schools adopt unreliable & inaccurate textbooks.", //Deck
                "The Department of Education has recently approved and distributed " +
                "a new history textbook to middle school students throughout the " +
                "nation, The History of Glorious Canton 8th edition. This " +
                "textbook, however, is far from the standards it should have " +
                "reached. Using Facepage and Saiddit posts as sources for the " +
                "book, as well as other outdated or otherwise unreliable " +
                "sources, this is a laughable excuse for an educational tool for our upcoming generation."
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "Department of Education to create a new round of textbooks.", //Deck
                "With the positive reactions to the newest edition of The " +
                "History of Glorious Canton 8th edition, the Department " +
                "of Education has begun creating new editions for each " +
                "of their textbooks in circulation. The new, updated " +
                "editions will fully replace past editions, and " +
                "government officials advise that families dispose " +
                "of their old books as soon as they gain access " +
                "to the newest editions."
            };
        }
    }
    #endregion
}
