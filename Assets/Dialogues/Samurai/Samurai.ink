VAR kill = false

-> samurai_introduction

=== samurai_introduction ===
Excuse me, sir. Where am I?

*** You're at a bar for monsters.
->scared

*** You're at a party filled with all of your bestest friends!
->madeup

*** Look, man. You're at a bar and I'm shackled here. 
->rude

=== rude
You're...shackled? Why? What's going on?

Who's gonna kill you?

*** I was kidnapped and forced to serve monsters like you. 
~kill = true
->DONE

*** Did something happen to you? You don't seem right.
->madeup

=== scared
Monsters? I'm not a monster. Why am I here? How did I end up here...?

I was with my friends in a glorious battle. I was looking for them, and I ended up here.

I don't like this. What's happening?

*** You're...you're just at a bar. Maybe your friends will show up here, too.
->madeup

*** But you ARE a monster. This is a bar for monsters, and you are one of them.
~kill = true
->DONE

=== madeup
Something...must have happened. I don't remember anything.

I'm so confused. Everything's in a haze.

*** Maybe you can order a drink and wait for your friends.
->getover

*** Well, you're at a bar now. You might as well order something, right?
->getover

*** You're at a monster bar for monsters. You are a monster and you probably killed others. 
~kill = true
->DONE

=== getover
Hmm...maybe I should get a drink.

I need something to help me remember. I'm so confused.

I was in a green field, the smell of death filled the air. It was cold... and I saw a woman with a parasol, lips stained with seeds of a fruit.
->END