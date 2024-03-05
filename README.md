"# NoIntegrity.Org Plugin for Unturned LDM" 
<br>
This plugin is written specifically for Unturned LDM and targeted at custom
"Creative" servers.
<br><br>
It references the most current DLL's available for Unturned and LDM (Rocket).
<br><br>
Some code was borrowed from other projects, which might be considered defunct:
- uEssentials
- Kits
- Item Vault
- Loadouts
- BTAdvanceRestrictor
...and probably a few others.
<br><br>
Also thanks to BlazingFlame and MrKan in the LDM Community Discord for their
guidance and suggestions!!!
<br><br>
The reason for this plugin was two-fold:
-   I needed database save/load slots for my creative server, which I had
    originally purchased from Mr. Kwabs long ago, but Imperial decided to
    lock my key.  And I'm not paying for it again.  (sorry Mr. Kwabs!)
-   I eventually needed to learn C#, so what better and more fun way to do
    it than with a game?  ^_^
<br><br>
    (I don't very much like C#)
<br><br>
I am releasing the source for this plugin to the public so that others can
compile it for themselves, make modifications, improve it, etc.
<br><br>
My giveback to a game I thought I'd never get addicted to. (thanks, Nelson)
<br><br>
You may use bits and pieces of this code for your own projects - just give
credit where credit is due, whether to myself or to others who contributed
to this ecosystem.
<br><br>
This plugin requires access to a MySQL server.  I am not doing JSON for
data storage as it's less friendly with live servers, so please don't ask
for a version that does JSON.
<br><br>
I do keep a Trello board, but it's not public.
<br><br>
If you have questions, feel free to email me:  warpkat@nointegrity.org
<br><br>
NoIntegrity.Org Discord:  https://discordapp.com/invite/AZMwbmj
NoIntegrity.Org Website:  <should be pretty obvious>
<br><br>
--- PLAY LIKE YOU GOT A PAIR! ---
<br><br>
Commands:
------------------------------------------------------------------------------
/slotsave x
<br>
    Saves current loadout to slot x.
<br>
    Permission:  slotsave
<br>
<br>
/slotload x
<br>
    Loads the saved loadout from slot x.
<br>
    Permission:  slotload
<br>
<br>
/bladd x
<br>
    For admin use, adds item x to the blacklist with immediate effect.
<br>
    Permission:  bladd
<br>
<br>
/bldel x
<br>
    For admin use, deletes item x from the blacklist with immediate effect.
 <br>
   Permission:  bldel
<br>
<br>
/stats x
<br>
    Provides Kills/Deaths/Headshot Percentage and KDR stats to the user.
 <br>
   Permission:  stats
