VAR kill = false

-> lady_introduction

=== lady_introduction ===
Hello! Hello! It's a slow day and my sweet baby's asleep.

I could really use a drink to pass the time!

*** Is that thing even alive?
->scared

*** Uh...I don't think you should drink while taking care of your baby.
->madeup

*** Your baby doesn't...look too good. You should take it to a hospital.
->rude

=== rude
My baby is fine.

She's sleeping.

I'd like to order a drink now.

*** I guess it's okay. This is a monster bar, I've seen crazier things than dead babies.
~kill = true
->DONE

*** Jesus, you're a terrible mother. That baby needs you.
->madeup

=== scared
~kill = true
->DONE

=== madeup
Ohhh...judging my parenting, are you?

I'll have you know I'm a perfectly responsible mother while under the influence.

Shame on you for suggesting otherwise!

*** Yep, I'm judging. You're a bad mother. I feel bad for your poor baby.
->getover

*** Okay, okay. I guess it doesn't matter since that baby isn't really alive.
~kill = true
->DONE

*** I'm only pretending to judge you, since you're only pretending that's a real baby.
~kill = true
->DONE

=== getover
ANYWAYS...my order is kind of complicated, so take notes!

Something terrible happened to me. I don't really know how to cope, or talk about what happened.

I believe it might be possible to make a drink that can bring back something I've lost.

I need two poisons, burnt bone, a magical entity, and eyes to see the truth with. Put that all on ice.
->END
