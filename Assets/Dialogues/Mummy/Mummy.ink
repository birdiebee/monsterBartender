VAR kill = false

-> mummy_introduction

=== mummy_introduction ===
Hey there, friend! Can I get a drink?

*** Sure, I'd love to make you a drink!
->suckup

*** Good God. You smell like fermented farts!
->insult

*** No. Bar's closed.
->rude

=== rude
Hooooo. People usually lie to me when they're scared.

But you're lying just to be mean. Why are you mean?

I hate liars! Just make me my drink!

*** I'm sorry. I'm just in a bad mood because of BOFA.
->bofa

*** Okay, fine. The bar is open. You CAN order a drink, I just don't want to make you one because you smell like shit.
->insult

*** I'm not lying. The bar is closed. Get out.
~kill = true
->DONE

=== bofa
What's BOFA?

...Oh. I just read your mind. That's kind of funny. I walked right into that one.

I can't tell if I respect you or not for doing that.

*** I'm sorry, I just think you're really disgusting and I'd prefer it if you left.
->insult

*** BOFA DEEZ NUTS
~kill = true
->DONE

=== suckup
...Why are you lying to me?

I can tell when you're lying. You think I look and smell bad.

*** Wow, you read my mind!
->insult

*** No, I genuinely like and respect all of the patrons of this fine establishment.
~kill = true
->DONE

=== insult
THANK you! Finally, an honest person in this phony world!

I know I'm smelly. I get it. I'm sick of the lies. People tiptoeing around it because they're afraid I'll snap...

So, how about my drink?

*** I'll make you a drink if you leave ASAP. Don't want to scrub decomp off the floorboards.
->getover

*** You're not a smelly, gross mess. Look, I'm sorry I was rude. I didn't mean it.
~kill = true
->DONE

=== getover
Thanks, friend.

As for my order, I want something that will remind me of the Nile River.

I want something crimson in color, with a lot of iron. 

Then, I want you to fill it with a dark liquid enough to wipe out life.

Put some ice in it and don't forget the organ of sight.

Hurry up! Iron, darkness, eyes and ice. Make it for me.
->END
