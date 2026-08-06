
// VAR DEBUG = true
// MAYBE REPLACE 'introduction' WITH 'lobby' OR SMTH?
-> introduction

// ----------------------------------
== introduction ==
INTRODUCTION

This is my first attempt at trying to learn ink. I thought it might be a good idea to paraphrase the official documentation and write up a tutorial that I can format into an interactive piece as an exercise.
// link to documentation: https://github.com/inkle/ink/blob/master/Documentation/WritingWithInk.md

    * [PART ONE - THE BASICS]
    -> theBasics
    * [Reach End]
    -> exitKnot

// ----------------------------------
    == theBasics ==
    PART ONE - THE BASICS
    This section contains the fundamentals for learning ink. Choose one of these options to navigate to the associated information.

        * [a. Basic Rules and Content]
        -> basicRulesAndContent
        * [b. Choices]
        -> choices
        * [c. Knots]
        -> knotsAndDiverts
        //* [d. Diverts]
        //-> diverts
        
            /* REMOVE
            * [e. Branching the Flow]
            -> branching
            * [f. Includes and Stitches]
            -> includesAndStitches
            * [g. Varying Choices]
            -> varyingChoices
            * [h. Variable Text]
            -> variableText
            * [i. Game Queries and Functions]
            -> gameQueriesAndFunctions
            REMOVE */

        * [-Go Back-]
        -> introduction
        // --------------------------
        
        == basicRulesAndContent ==
        1A - BASIC RULES AND CONTENT
            UNDERSTANDING INK FILES:
                Everything in an ink file will appear as plain text when the script is running unless marked up in a specific manner. You can create paragraphs by using text on separate lines, and you can mark sections to be ignored by the compiler using comments.
            
            USING COMMENTS:
            Two forward slashes will exclude the content proceeding them for a single line. To hide an unlimited block of text, mark a starting point with a single slash and an asterisk, and then mark an end point with these symbols in reverse. I've written these explanations out twice in the ink document, once with comments and once without, so you can see what I mean more clearly. Feel free to open the ink document and read along to see this in action! 
            // You can do this in VSCode if you want it accurate to how I wrote it. I think notebook/standard text file readers might format it strangely.

                // Two forward slashes will exclude the content proceeding them for a single line.

                /* And to hide an unlimited block of text, 
                mark a starting point with a single slash 
                and an asterisk, and then mark an end 
                point with these symbols in reverse. */
            
            /** [Next]
            -> tags
            // ----------------------

            == tags ==*/
            
            MARKUPS - TAGS AND TO DO'S:  
            You can use a different markup to create a reminder for the authors of the document. This will be printed out in the compiler as a 'to do' message. You can also use tags for particular lines that you would like to behave differently.
            When you wish to mark a line of content with extra information that tells the game what to do with that line, you can use a hashtag. These will not appear as the standard text in the document would, but can be read by the game and used as you see fit.

                // eg.
                // TODO: Learn to use Unity's Ink integration!
                  
                // eg.
                // This is a normal line of game text
                This is a normal line of game text with a tag # colour it red
                    // Jump to [RUNNING INK] for details.

        //That's all for the basic rules and content!
            * [Back to Basics Page]
            -> theBasics
            * [Back to Home Page]
            -> introduction
            * [Exit]
            -> exitKnot
        // --------------------------

        == choices ==
        1B - CHOICES

        BASIC TEXT CHOICES:
            When you want your player to be able to make a decision in your .ink file, you can use text choices. Text choices are marked with an asterisk. Once a choice has been selected, it will lead into the following line of text unless specific instructions are given.

            /* REMOVE
            eg.
            This line of text is a prompt that leads into the choice.
            *   This is a choice the player can make.
                This is the text that follows the choice made by the player.
                REMOVE */

        CHANGING THE OUTPUT OF CHOICE TEXT:
            The text of a choice appears in the output section by default. To make it so that the text of the choice is hidden when selected, use square brackets to indicate the choice. These brackets separate the option content.

            /* REMOVE
            eg.
            The cave is dark and empty, and you are filled with a strange impulse.
            *   [Yell into the darkness]
                Your voice carries into the cave, bouncing off its cold, damp walls.
                REMOVE */

        ADVANCED CHOICES AND OUTPUT:
            Since square brackets separate option content, you can also use them to set up more advanced choices. Anything in the line BEFORE the brackets will be printed in both the choice and the output, while anything inside them will only appear in the choice. Anything in the line AFTER the brackets will only be printed in the output section.

            /* REMOVE
            eg.
            This section shows you how to use a single line so that it will return two separate things.
            *   Using ink [seems complicated at first, but in reality...] is actually rather straightforward!
                This is a lie I will keep telling myself until I believe it!
                REMOVE */

            // NOTE: You can use this very effectively for dialogue choices!

            /* REMOVE
            eg.
            "I've been expecting you," the stereotypical villain was waiting for me. Strangely, he sat in a decrepit lawn chair.
            *   "[Your chair looks dumb."]No, really. It's fucking goofy. You're gucci'd down to the socks, so what gives?"
                "What gives," his jaw twitches in annoyance, "is you. I've had to hire twice as many men because of you. We needed to make some budget cuts. I'm trying to pay my stereotypical villain employees fairly."
            *   "[So you ditched the herman miller for a vergil?"]What a strange decision," I smirk, but shrink back when he returns it.
                "Not as strange as this dialogue. You are a poorly written character." The best villain ever written says.
                REMOVE */


        MULTIPLE CHOICES:
            For a branching story with multiple choices (kinda required for them to feel like actual choices), you need to list them out.
            // Let's return to my brilliant writing (i'm lacing this document with stupidity, trust and believe) -- but this time, you're going to be able to choose what you say.

            /* REMOVE
            eg.
            "You seem like you wish to say something further... What is it?" he asked.
            *   [Choose the story game response: "Nothing much..."] "... Other than that you're a stupid dumb dumb and I don't like you!"
                I clap a hand to my mouth, appaled. That's not what I thought that dialogue option was going to say at all, how infuriating!
            *   "Don't worry about it."[] I said, demonstrating another way that someone might be able to use square brackets in the .ink document.
                "What's a .ink document?" The man rises from his chair, "And how did you just pronounce a fullstop?"
            *   [Start beatboxing]
                I start to beatbox. Unfortunately, it's not something I ever really tried to do before, so I just kinda blow raspberries and make a fool out of myself.
                REMOVE */

        // That's all for choices!
                * [Back to Basics Page]
                -> theBasics
                * [Back to Home Page]
                -> introduction
                * [Exit]
                -> exitKnot
            // --------------------------

        == knotsAndDiverts ==
        1C - KNOTS AND DIVERTS
        THE CORE STRUCTURE FOR INK:
        To make the game branch out, you need to mark up sections of content inside the .ink document with names. These are called knots, and are used to structure your ink content. 

        WRITING A KNOT:
            To begin a new knot, bracket a name with two or more equals signs. The knot's name should be a single word with no spaces.
            The start of the knot is a header, and everything that follows will fall inside it. Content outside of knots will be ran automatically, but knots will not. To see the content of your knots, you need to direct the compiler to it with a divert arrow.
        
        DIVERT ARROWS:
            To move from knot to knot, you can use a divert arrow. To do this, combine (-) with (>), and then the name of one of your knots to send the compiler to that particular knot. These happen immediately and without input once running. 
            
            Just remember that ink requires you to close off every knot with either an End/Done statement, a Choice, or a Divert. If you do not do this, it will print an error on running. You can always check these for more information!

        // That's all for knots and diverts!
                * [Back to Basics Page]
                -> theBasics
                * [Back to Home Page]
                -> introduction
                * [Exit]
                -> exitKnot
            // --------------------------

// ----------------------------------

== exitKnot ==
This is the knot that reaches the end state.
-> END