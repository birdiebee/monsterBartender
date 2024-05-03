VAR kill = false

-> chad_introduction

=== chad_introduction ===
MMMMRRRGHHUOOGGHHH

*** JESUS FUCKING CHRIST! What are you?
->scared

*** What's wrong, big fella?
->madeup

*** Just order already. I'm not in the mood.
->rude

=== rude
Oh, I'm sorry, are YOU having a bad day?

The love of my life just DUMPED ME. BRUTALLY. AND PUBLICLY.

I came here to let it out at the bar and get drunk. 

But sure, let's feel bad for YOU and YOUR bad day.

*** I was literally kidnapped and forced to work here, jackass. Fuck your problems.
~kill = true
->DONE

*** Ok, I apologize. What's on your mind?
->madeup

=== scared
Ohhh, you too, huh?

You think I'm a gross, ugly blob?

Well, guess what? I don't need you! I'm perfectly happy by myself!

...*sob*

*** Look, I'm sorry. What's wrong?
->madeup

*** MY GOD! IT'S LIKE I'M STARING INTO A MANATEE'S INFECTED ASSHOLE!
~kill = true
->DONE

=== madeup
It's...it's my girlfriend, Doris. She left me.

She's the most beautiful mermaid I've laid eyes on. Everyone always said she was too good for me.

I hate her, but...I miss her. *sob*
*** Why don't we get you nice and drunk so you can get over her?
->getover

*** Well, what are you gonna do about it?
->dosomething

*** Hold on. YOU bagged a MERMAID? Ha...hahhaha...There's no way.
~kill = true
->DONE

=== getover
I can't get over her. Don't you see?

I need to feel close to her again...any way I can.

Make me something to remind me of my sweet mermaid. Something nautical and icy, with a gem from the sea.
->END

=== dosomething
Me? I'm gonna get completely plastered and reminisce on all the things I did wrong in this relationship.
Make me something to remind me of my sweet mermaid. Something nautical and icy, with a gem from the sea.
->END
