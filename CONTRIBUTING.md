# Contributing to **Harmony** :musical_score::musical_keyboard::microphone::saxophone::trumpet::guitar:
### *WE SEE SHARP. WE C#.*

#### Happy Senior Project :slightly_smiling_face::slightly_smiling_face::slightly_smiling_face:

## Expectation (on myself too of course)
* ~~Work Hard Play hard~~ 
* Respect all group members, **COMMUNICATE** **COMMUNICATE** **COMMUNICATE** !
* Most documentations in markdown format :newspaper:
* Please let Louis know if anything goes wrong with this repo

## General Guideline
* There should be three branches, master, Dev and Integration
* Fork this repo once you have it, and then clone your own forked, ~~not fork you cloned~~
* Create a `remote` called upstream off the original repo, and pull from upstream everytime otherwise you won't be in sync
* After you `git pull upstream <branch>`, don't forget to `git push origin <branch>`
* ALWAYS work on a `feature` branch, not on `dev`; all you do for `dev` and `master` will be `pull`
* Don't submit any pull requests to `master`, always to `dev`
* Submit your final pull request for each sprint by the end of Saturday unless I am notified
* Submit pull requests daily, which can even be tiny, to be consistent following Scrum
* DON'T submit code that won't compile

## CSS Guideline
* Use Bootstrap 4
* Name your `id=` in a readable format; use - to seperate words, for example: `<div class="" id="footer-background-color">`
* Theme color for Harmony: 
* Navbar background-color:
* Navbar font-color:
* Content grid background-color:
* Content grid font-color:
* 

## Coding Guideline
* PLEASE document you code; even a short note will be helpful

## Database Design
* Table names should be plural, e.g `Users`
* Each table should have a **primary key** named `id` in lowercase
* Name all the **foreign keys** `<Entity>ID`, e.g. `UserID`
* Review all relationships before adding a new table
* Update up, down, seed scripts, document it accordingly if needed
* Update ER-Diagram

### Avoid Merge Conflicts for Pull Requests
When you are ready to submit a pull requet, please `merge` your `dev` branch to your `<feature>` branch to check for any merge conflict. If there is any, resolve it before you `push` it. When all the merge conflicts are resolved, `push origin <feature>` then you can submit a pull request from `<feature>` -> `dev`
