# Pizza Fam Template
With [Stanley the Good Dad](https://jimmycushnie.itch.io/stanley-the-good-dad) and [Sniggle: Savior of Snugs](https://jimmycushnie.itch.io/sniggle), we've settled on a really good formula for our jam games. This is a template project for that formula so we don't have to re-program the same systems again and again and again.

Features of this template:

* main Menu scene
  * new game button 
  * level select menu
    * levels are locked until you've reached them
  * options menu
    * adjust music volume
    * adjust SFX volume
    * toggle subtitles
  * about menu
  * quit game button
* cutscene player
  * subtitle support for cutscenes
  * cutscene pause menu
    * resume cutscene
    * skip cutscene
    * toggle subtitles
* in-game UI
  * text that tells you what level you're on
  * pause menu
    * resume
    * options menu (same as main menu)
    * quit to main menu
* sound player system
  * supports random clip variation
  * supports per-clip subtitles
  * supports random pitch variation
  * supports volume scaling
* music player system
  * intro that leads into a loop without skipping
* WebGL build template
  * custom loading bar
  * cusom image to display above the loading bar
  * auto-adjusting resolution
    
All of the above features work on both Windows and WebGL. I'd like to verify for Mac OS and Linux in the future.
  
To my knowledge, this is all within the [Ludum Dare rules](https://ldjam.com/events/ludum-dare/rules):

> You’re free to use **any** tools or libraries to create your game. You’re free to start with **any** base-code you may have.
