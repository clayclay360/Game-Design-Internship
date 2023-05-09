using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script contains all of the information used to populate newspaper articles for the end of the day
public class Articles
{
    /// <summary>
    /// The main method usesd to get our article information. The method returns an array of three strings which are used to populate newspaper info.
    /// </summary>
    /// <param name="day">The current day that we're fetching articles for</param>
    /// <param name="wasPlayerCorrect">Whether the player was able to correctly sort enough articles to win the day</param>
    /// <returns></returns>
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
            case 4:
                return dayFive(wasPlayerCorrect);
            case 5:
                return daySix(wasPlayerCorrect);
        }

        //This should never happen. If an empty newspaper shows up something has gone wrong.
        Debug.LogWarning("Article.cs: empty string returned due to switch statement fallthrough.");
        return new string[] { "", "", ""};
    }


    #region dayTemplate
    /// <summary>
    /// Empty day to use for creating extra days. Only necessary if you plan on adding extra days.
    /// </summary>
    /// <param name="wasPlayerCorrect">Whether the player was able to correctly sort enough articles to win the day</param>
    /// <returns></returns>
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
                "Citizens curse Canton Clubber outside Canton Police Station", //Deck
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
                "Department of Education to create a new round of textbooks", //Deck
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

    private string[] dayFive(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "THE LAYMAN'S ALMANAC", //Header
                "Code Cobra's Strike at the Throat!", //Deck
                "Today, March 11th 2023, the citizens of Canton have experienced one of " +
                "the worst tragedies since the Spanish Flu crisis of the 1920s. The " +
                "Code Cobras, an anarchist hacking organization, has revealed the " +
                "sensitive information of every citizen in Canton, including their " +
                "bank information, claiming to use the data vaults residing within " +
                "the Department of Information, likely using the backdoors hardwired " +
                "within each government-assigned computer. With anonymous criminals " +
                "withdrawing money in the names of innocent people, the people of " +
                "Canton are experiencing an unprecedented economic crisis."
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "Terrorists Leak Information, Banks to Blame!", //Deck
                "The Code Cobras, a domestic terrorist organization, have recently " +
                "leaked the sensitive information of many citizens, resulting in mass " +
                "identity theft and the complete emptying of digital bank accounts. " +
                "The treasury has promised compensation to those with more than $100,000 lost."
            };
        }
    }

    private string[] daySix(bool wasPlayerCorrect)
    {
        if (wasPlayerCorrect)
        {
            return new string[]
            {
                "THE LAYMAN'S ALMANAC", //Header
                "The Cash Act, Section XII, and the end of our freedoms", //Deck
                "The Cash Act, emergency legislation put forward to seize wealth from " +
                "thieves taking advantage of the mass data breach and redistribute " +
                "wealth to the citizens of Canton. The Cash Act, however, holds much " +
                "more severe consequences for citizens of Canton, stripping them of " +
                "their rights to own businesses and forcefully handing control over " +
                "to the Canton government"
            };
        }
        else
        {
            return new string[]
            {
                "THE CANTON HERALD", //Header
                "The Cash Act is set to pass in two days!", //Deck
                "Our glorious leaders have now set the bill to pass in two days time, " +
                "with overwhelming public support. With just under 80% of funds seized " +
                "already, the population will be able to rest easy knowing their banks " +
                "will be full once again in a few days time."
            };
        }
    }

    #endregion
}
