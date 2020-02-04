Team Project Inception
=====================================
# Harmony
## Summary of Our Approach to Software Development

[What processes are we following?  What are we choosing to do and at what level of detail or extent?]
Our project is seperated into clear modules, which we will develop one at a time. These modules will be easy to break down into small user stories. Each user story will be a thin verticle slice through all three layers of the application. We will work together as a team to complete each module and to come up with solutions to difficult problems. We are using Disciplined Agile Delivery and Scrum to develop our project.

## Initial Vision Statment
Music Booking For musicians and venues who need an easy way to book shows, the show booking application is a useful web application that will allow musicians and venues to search for and contact each other. This will make for a much smoother booking process and will aid venues and musicians in discovering one another. Unlike other booking applications, our product will be truly representative of, and useful to, the independent music scene in the Salem area.

Culture For students/travelers/teachers who need or interested in learning new cultures before they travel or for the sake of diversity, the Culture Learning application is a website that will provide information of different cultures such as food, languages, history, landmarks, etc. The website will have a main search functionality and categories for visitors to look for a specific culture that they are interested in learning. This website will also act as a social media for people to exchange their culture with multimedia. Unlike a typical language learning website, our application will generate a lot of more info than just languages and will also be able to help people with different purpose of learning such as traveling or educational purposes.

## Core Features
1. Allow musicians and venue owners to create accounts and profiles so that they can search for and find each other, communicate, and agree to work together, creating a much more modern and hassle-free way to book live music.
2. We want to be able to make useful reccomendations to both musicians and venue owners on who/where to book next. We need an algorithm to make reccomendations based on users' past booking experience so they can easily find more people to work with without sifting through tons of profiles.
3. There should be social media-like aspects of the site, like rating, commenting, messaging, and posting. We want users to interact with each other.

## Initial Requirements Elaboration and Elicitation
### Questions and Interviews
1. Q: What social media should we allow musicians to link in their profile? 
A: Instagram, BandCamp, SoundCloud, Facebook, YouTube, and Spotify would be good too.
2. Q: Do you know people in Salem who would be interested in using this web app?
A: Yes, a lot of local musicians would use this.
3. Q: Would it be helpful to have a record of all the shows you've booked through the site?
A: Yes, it would be nice to be able look back at all the shows I've played. It would be great if you could use some kind of calender along with that.

### Other Elicitation Activities?

## List of Needs and Features

1. There will be two different account types: musicians and venue owners. All users should have profile pages with a name, description, profile picture, contact information, location, and links to social media. A musician’s profile will include band member’s name(s) and role in the band, their music genre, and the option to include videos from their YouTube channel. A venue owner’s page will show their location on Google Maps and will have the option to upload photos of the venue. Users should be able to see their distance from other users in miles.
2. A search feature should be implemented so users can search for each other. Include filters: by location, genre, name, whether the musician has videos, or popularity (or no filters - by distance). We also want a browse page that will recommend musicians or venues to users. Recommendations should be based on previous shows booked. Recommendation criteria will depend on whether the user is a musician or a venue owner. Musicians will be recommended venues in locations they’ve played before, venues that frequently book musicians of the same or similar genre as the musician browsing, and venues they’ve played at before. Venue owners will be recommended musicians whose genre is similar or the same as those they’ve booked before, musicians who frequently book shows in their area, and musicians they’ve worked with before. Users with no previous booking activity will be recommended users who are close, or popular. When a user finds someone to work with, they should be able to communicate and through the site or easily email the other user.
3. To aid in scheduling, users will be able to display their availability on their profile with a calendar. We will use a calender API for this. When searching, users will have the option to filter by schedule to show only other users who are available on the day(s) they’ve selected. Users should also be able to confirm a scheduled show through the site after contacting each other. After a show is booked, it should appear on users' calenders. If a user schedules a show, the other user will be notified and can accept or decline the scheduled show. The show can be cancelled at any time.
4. We will create an Events page where any registered user can create and post an event, like a music festival, a competition, or a get-together. Events should be seperated by city, and you should be able to select a city from a drop-down to view its events. Events should have a link the profile of the person who posted it.

## Initial Modeling
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%203/mindmap.jpg "Mindmap" )
### Use Case Diagrams
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%204/UseCase_Diagram.jpg "UseCaseDiagram" )
### Other Modeling

## Non-Functional Requirements
1. Posts on the Events page should not be personal advertisements or spam.
2. Prevent people from creating fake accounts.
## Identify Functional Requirements (User Stories)

E: Epic  
U: User Story  
T: Task  

1. [U] As a visitor, I want the site to look clean and professional so that I feel inclined to use it.
2. [E] As a visitor, I would like to be able to create an account so that my information is saved.
    1. [U] As a venue owner, I want to be able to create a venue owners account so that my experience is customized for venue owners.
    2. [U] As a musicians, I want to be able to create a musicians account so that my experience is customized for musicians.
3. [E] As a registered user I want to be able to display my information on a profile so that other users can find out more about me.
    1. [U] As a venue owner, I want to display my venue name and location and photos on my profile so that other users can learn about my venue.
    2. [U] As a musician, I want to display information about my band and my music on my profile so that other users can learn about me as an artist.
4. [E] As a registered user I want to be able to link my social media on my profile so that other users can follow me on different platforms.
5. [E] As a musician, I want to be able to share my youtube videos on my profile so that venue owners can listen to my music.
6. [E] As a venue owner, I want to be able to show my location with a map on my profile so that musicians know where I am located.
7. [E] As a registered user, I want to be able to see how far away other users are from me in miles so that I know how far I/they will have to travel to work with together.

8. [E] As a visitor, I want to be able to search for musicians or venues so that I can find someone to work with.
9. [E] As a user, I want to have other users recommended to me based on their location, popularity, and, who I've worked with before.
10. [E] As a user, I want to be able to filter results when I look for other users so that I can
11. [E] As a user, I want to be able to send messages to other users on the site so that we have an easy way to communicate.
12. [E] As a user, I want to be able easily send an email through the site so that I don't have open a new tab and navigate to my email.
13. [E] As a user, I want to be protected from interacting with bot accounts so that my information is safe and I don't waste my time.

14. [E] As a user, I want to set my availability and display it on my profile with a calender so that other users know when I am available for a show.
15. [E] As a user, I want to be able to search for other users who are available on a selected day so that I can easily book shows for specific dates.
16. [E] As a user, I want to be able to book a show with another user through the website so that we can have a record of the scheduled show.
17. [E] As a user, I want to recieve notifications when someone tries to book a show with me or contact with me so that I can respond quickly.
18. [E] As a user, I want my booked shows to appear on my public calender so that other users will know that I am not available on that day.
19. [E] As a user, I want to be able to view a list and statistics of all previous shows I've booked through the site so that I can see who I've worked with and when.

20. [E] As a registered user, I want to be able to post and view events in my area so that I have an easy way to find work or meet-ups.
21. [E] As a user, I only want to see events in my city by default so that events are relevant to me.
22. [E] As a user, I want to be able to search for and filter events so that I can find one I'm interested.
23. [E] As a user, I want to be able to RSVP for events so that other users know I am planning to attend.
24. [E] As a user, I want events I'm hosting or attending to show up on my public calender so that others will know that I won't be available to book shows during those events.
25. [E] As a user, I want want to be notified when other users RSVP to my event so that I can message them and have an idea of how many people will be attending.
26. [E] As a user, I want to be able to control who seees my event and have the ability to approve/decline people from attending so that I can control who shows up.
27. [E] As a user, I want to be able to report spam posts so that the events page will only contain legitimate events.

## Initial Architecture Envisioning
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%204/Architecture_Diagram.jpg "ArchitectureDiagram" )
## Agile Data Modeling
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%204/Initial_ER_Diagram.jpg "ERDiagram" )
## Refined Vision Statement
**For** musicians and venue owners who need an easier way to book shows, **the** show booking application **is a** useful web application that will allow musicians and venue owners to search for and contact each other. This will make for a much smoother booking process and will aid venue owners and musicians in discovering one another. It will be helpful to musicians who play shows as a main source of income, as well those who are just starting out and lack experience. **Unlike** other booking websites, **our product** will be truly representative of, and useful to, independent musicians in the Salem area.

## Timeline and Release Plan
![image unavailable](https://github.com/lawlouis/seniorproject-PacificWhales/blob/dev/Milestone%204/Timeline.jpg "imeline" )
