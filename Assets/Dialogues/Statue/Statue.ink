VAR kill = false

-> statue_introduction

=== statue_introduction ===
Hellooooo? I need a drink. Now.

*** Have you considered something called Patience?
->scared

*** Sick abs, bro.
->madeup

*** I'll take your order when I'm done taking the orders of everyone that came before you, okay?
->rude

=== rude
No, that's not okay. I need a drink NOW. What part of this is so difficult for you to understand?

Just get me my drink so I can get out of here.

*** I'd consider it if you weren't such an asshole.
~kill = true
->DONE

*** Okay, okay, I'll make your drink first!
->madeup

=== scared
Have you considered something called Not Being Sarcastic Towards A Paying Customer?

Who do you think you are, anyway? You're just a scrawny little human bartender who needs to hit the gym.

*** Okay, okay. I can see from your physique that I shouldn't mess with you. What would you like?
->madeup

*** How about you hobble your way out of my bar and go find some sheetrock to jerk off with?
~kill = true
->DONE

=== madeup
Thanks, bro. I'm the best and I deserve the best.

Now give me a drink that fits a glorious masterpiece like me.

*** I get it, man. Go ahead and order.
->getover

*** Your head is on backwards. You're not a masterpiece.
~kill = true
->DONE

*** You know who thinks I'M a masterpiece? Your mom.
~kill = true
->DONE

=== getover
Cool, cool. I need something only Greek deities drink.

I deserve it because I am a god...no, greater than a god!

Golden liquid, golden fruit wedge, shimmering pixie dust. Drop in the most expensive jewel you own as an offering to me. And don't forget ice.
->END