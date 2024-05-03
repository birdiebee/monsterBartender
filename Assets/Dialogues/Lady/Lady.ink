VAR kill = false

-> lady_introduction

=== lady_introduction ===
Good evening. I'd like to order a beverage.

*** Sure. What can I get ya?
->scared

*** Good evening, madame. I would graciously accept your order.
->madeup

*** Okay?
->rude

=== rude
Please, sir. You could not sound any less enthused.

I do not appreciate your tone.

You remind me of my third husband. I don't like it.

*** Just order.
~kill = true
->DONE

*** My apologies, madame. I will do my best to ensure you receive the service you deserve.
->madeup

=== scared
Do not address me so informally, sir. As a high-class noble, don't you think I deserve the utmost respect?

*** Uh...good day, my lady. What beverage would you like me to prepare for you?
->madeup

*** Why don't you respect deez nuts?
~kill = true
->DONE

=== madeup
My, my! What a gentleman!

You remind me of my second husband. What a polite man. If only he had come from more generous means...

*** I thank you for your compliments, my lady. May I take your order?
->getover

*** Yes ma'am, those damn men with their lean bank accounts. They sure piss me off!
~kill = true
->DONE

*** Can you just hurry up and order, please?
~kill = true
->DONE

=== getover
Yes, I'd like that very much.

I love the taste of death. Poisoned my third husband. 

I charmed all my men with my "water nymph-like" beauty. 

Lastly, I need something to make my wishes come true, perhaps one of those djinns.

In short: two poisons, water-nymphs, and djinns. Make it quick!
->END
