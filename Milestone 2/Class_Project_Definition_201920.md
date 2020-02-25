2019-20 Class Project Inception
=====================================

## Summary of Our Approach to Software Development

[What processes are we following?  What are we choosing to do and at what level of detail or extent?]

## Initial Vision Discussion with Stakeholders

Primary Stakeholder -- Katimichael Phelpedecky, swimming legend and hopeful entrepreneur

Katimichael's experience being on the US Olympic team led to an appreciation of how advanced tools can help athletes perform at their best.  The problem is those tools are very expensive and require personnel with advanced training, i.e. elite analysts for elite athletes.  They want to create a business to give regular swimming coaches, from high school, club, college, and masters, advanced analytical and predictive tools to help the athletes on their teams.  Katimichael has assembled a team of investors to fund this project and is hiring your team to create the product.

The product is centered around three core features:

1. Record, store and provide tracking, viewing and simple stats for race results for swimming athletes.  This would have a number of features found in [Athletic.net](https://www.athletic.net/), which is used for Track and Cross Country running.
2. Provide complex analysis of athlete performance over time and over different race types, to give coaches deep insight into their athlete's fitness and performance that they cannot get from their own analyses.  This includes machine learning to predict future performance based on records of past race performance, given different training scenarios.  Validation of this feature will enable the next feature.
3. Create a tool that will optimize a coaches strategy for winning a specific meet.  This feature will automatically assign athletes to specific races based on their predicted race times in order to beat an opponent coache's strategy.  There will be two modes: one in which we have no knowledge of the opponent team's performance, and one where we do have their performance and can predict their times.

## Initial Requirements Elaboration and Elicitation

### Questions
1. Louis asked for #5, We want to have admins and contractors added by "super" admin after they get their offline confirmation. So is "super" admin a different level of admin or just an existing admin who has access to add another admin or contractor? What else can super admin do?
2. What training scenarios should we include to help predict athletes' future performances?
3. If a coach makes a prediction for an athlete, or optimizes their team for a meet, would they want the option to save that information?

### Interviews
1. "super" admin in this case means our employees and yes "super" admin will have to have different level of access with regular admin. Super admin can be the tech team leader of the company also there should not be too many super admins.
2. Training scenarios will mainly rely on the number of hours the athlete has trained for a certain event.
3. Yes, that would be helpful. There could be a tab for coaches to acess that allows them to view and delete their saved predictions and team optimizations.

### Other Elicitation Activities?

## List of Needs and Features

1. They want a nice looking site, with a clean light modern style, images that evoke swimming and competition.  (More like [Strava](https://www.strava.com/features) and less like [Athletic.net](https://www.athletic.net/TrackAndField/Division/Event.aspx?DivID=100004&Event=14))  It should be easy to find the features available for free and then have an obvious link to register for an account or log in.  It should be fast and easily navigable.  
2. The general public will be able to view all results (just the race distance, type and time).  These are public events and the results should be freely available.  They should be able to search by athlete name, team, coach or possibly event date and location.  Not sure if they want to be able to filter or drill down as Athletic.net does.  They're not trying to organize by state, school, etc. Athletes are athletes and it doesn't matter where they're competing.  This is completely general, but only for swimming.
3. Logins will be required for viewing statistics and all other advanced features.  We eventually plan to offer paid plans for accessing these advanced features.  They'll be free initially and we'll transition to paid plans once we get people hooked.
4. Admin logins are needed for entering new data.  Only employees and contractors will be allowed to enter, edit or delete data.
5. "Standard" logins are fine.  Use email (must be unique) for username and then require an 8+ character password.  Will eventually need to confirm email to try to prevent some forms of misuse.  Admins and contractors must have an offline confirmation by our employees and then the "super" admin adds them manually.
6. The core entity is the athlete.  They are essentially free agents in the system.  They can be a member of one or more teams at one time, then change at any time.  Later when we want to have teams and do predictive analysis we'll let the coaches assemble their own teams and add/remove athletes from their rosters.
7. The first stats we want are: 1) display PR's prominantly in each race event, 2) show a historical picture/plot of performance, per race type and distance, 3) some measure of how they rank compared to other athletes, both current and historical, 4) something that shows how often they compete in each race event, i.e. which events are they competing in most frequently, and alternately, which events are they "avoiding"
8. They might want to have a platform or forum for coaches, swimmers, and admins to communicate or share some of their personal experience or techniques about swimming.
9. We want a page where we can input the number of hours an athelte has trained for an event to estimate their future performance time, or estimate the hours they would need to train in order to reach a given time for an event.
10. There should be a seperate account for coaches, so that only they may access the prediction and optimization features of the site, and only use them for their team. Coaches should also be able to save and delete their predictions and optimizations.
11.There should be a button you can click on to show the plot/graph of athlete progress over certain time(you are able to choose the period) for coaches.


## Initial Modeling

### Use Case Diagrams

### Other Modeling
![image not available](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%202/mind_map.jpg "mind_map")
## Identify Non-Functional Requirements

1. User accounts and data must be stored indefinitely.  They don't want to delete; rather, mark items as "deleted" but don't actually delete them.  They also used the word "inactive" as a synonym for deleted.
2. Passwords should not expire
3. Site should never return debug error pages.  Web server should have a custom 404 page that is cute or funny and has a link to the main index page.
4. All server errors must be logged so we can investigate what is going on in a page accessible only to Admins.
5. English will be the default language.
6. All threads should mostly be related to swimming. 

## Identify Functional Requirements (User Stories)

E: Epic  
U: User Story  
T: Task  

1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
   1. [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
   2. [T] Choose CSS library (Bootstrap 3, 4, or ?) and use it for all pages
   3. [T] Create nice homepage: write initial content, customize navbar, hide links to login/register
   4. [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.
2. [U] As a visitor to the site I would like to be able to register an account so I will be able to access athlete statistics
   1. [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP script
   2. [T] Configure web app to use our db with Identity tables in it
   3. [T] Create a user table and customize user pages to display additional data
   4. [T] Re-enable login/register links
   5. [T] Manually test register and login; user should easily be able to see that they are logged in
3. [E] As an administrator I want to be able to upload a spreadsheet of results so that new data can be added to our system
4. [U] As a visitor I want to be able to search for an athlete and then view their athlete page so I can find out more information about them
5. [U] As a visitor I want to be able to view race results for an athlete so I can see how they have performed
6. [U] As a visitor I want to be able to view PR's (personal records) for an athlete so I can see their best performances
7. [E] As a swimmer I want to have a platform for us swimmers, coaches to exchange information of practices, techniques, prep works for meets.
    1. [U] As a swimmer, I want to be able to **publish** a thread about my experience in a meet so I can share my experience or techniques I used
    2. [U] As a swimmer, I want to be able to **read** other swimmers' threads so that I can learn about how other swimmers prepared for their meets and what techniques that used.
    3. [U] As a swimmer, I want to be able to **comment** on others' threads so that we all can exchange information and give peer advices to each other.
    4. [U] As an admin, I want to be able to **delete** threads that are inappropriate so that we can have all the information under control and stay on topic.
8. [U] As a robot I would like to be prevented from creating an account on your website so I don't ask millions of my friends to join your website and try to add comments about male enhancement drugs.
9. [U] As a coach, I want to be able to easily drop and add athletes from my team.
   1. [T] Create a coach entity with a one-to-many relationship with athletes.
   2. [T] Create an seperate account type for coaches, so only they can access certain features of the site.
   3. [T] Make a page that each coach can access to view their team and make changes.
10. [E] As a coach, I want to predict an athlete's future performance and save that information for later use.
  1. [U] As a coach, I want to use athletes' past performances and training time to estimate future performance times.
    1. [T] Create a page for coaches to select and athlete and an event and input information for a prediction.
    2. [T] Write an algorithm to make accurate predictions of an athlete's performance time given past performances and hours trained.
    3. [T] Allow only users with coach accounts to access this page.
    4. [T] Add a predictions entity to our database, connected many-to-one with the coach entity.
    5. [T] Allow this information to be saved for the coach to view later.
  2. [U] As a coach, I want to input a desired performance time for an athlete in a specific event and predict how long they would need to train compete at that time.
    1. [T] Modify our prediction algorithm to estimate hours of training needed to acheive a given time for an event.
  3. [U] As a coach, I want to be able to view and delete my saved predictions.
    1. [T] Create a seperate page to list coaches' saved predictions.
    2. [T] Allow for deletion of any of the items.
11. [U] As a coach, I want to be able to view the athlete progress over certain period.
    1. [T] Create a dropdown list that the coach can choose the time period.
    2. [T] Creat a button that the coach can click to show the graph.



## Initial Architecture Envisioning
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%202/architecture_diagram.jpg "Architure Diagram" )
## Agile Data Modeling
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%202/initial_ER_classproject.jpg "ER_Diagram")
## Refined Vision Statement
Our vision is to provide affordable software that allows coaches to track athletes progress and optimize their performance.
## Timeline and Release Plan
