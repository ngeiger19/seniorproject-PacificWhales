Team Project Inception
=====================================
# Harmony
## Summary of Our Approach to Software Development

[What processes are we following?  What are we choosing to do and at what level of detail or extent?]
Our project is seperated into clear modules, which we will develop one at a time. These modules will be easy to break down into small user stories. Each user story will be a thin verticle slice through all three layers of the application. We will work together as a team to complete each module and to come up with solutions to difficult problems. We are using Disciplined Agile Delivery and Scrum to develop our project.

## Initial Vision Statment
**For** musicians and venue owners who need an easier way to book shows, **the** show booking application **is a** useful web application that will allow musicians and venue owners to search for and contact each other. This will make for a much smoother booking process and will aid venue owners and musicians in discovering one another. It will be helpful to musicians who play shows as a main source of income, as well those who are just starting out and lack experience. **Unlike** other booking websites, **our product** will be truly representative of, and useful to, independent musicians in the Salem area.

## Core Features
1. Allow musicians and venue owners to create accounts and profiles so that they can search for and find each other, communicate, and agree to work together, creating a much more modern and hassle-free way to book live music.
2. We want to be able to make useful reccomendations to both musicians and venue owners on who/where to book next. We need an algorithm to make reccomendations based on users' past booking experience so they can easily find more people to work with without sifting through tons of profiles.
3. There should be social media-like aspects of the site, like rating, commenting, messaging, and posting. We want users to interact with each other.

## Initial Requirements Elaboration and Elicitation

### Questions


### Interviews
1. Q: What social media should we allow musicians to link in their profile? A: Instagram, BandCamp, SoundCloud, Facebook, YouTube, and Spotify would be good too.
2. 
### Other Elicitation Activities?

## List of Needs and Features

1. There will be two different account types: musicians and venue owners. All users should have profile pages with a name, description, profile picture, contact information, location, and links to social media. A musician’s profile will include band member’s name(s) and role in the band, their music genre, and the option to include videos from their YouTube channel. A venue owner’s page will show their location on Google Maps and will have the option to upload photos of the venue.
2. A search feature should be implemented so users can search for each other. Include filters: by location, genre, name, whether the musician has videos, or popularity (or no filters - by distance).
3. To aid in scheduling, users will be able to display their availability on their profile with a calendar. When searching, users will have the option to filter by schedule to show only other users who are available on the day(s) they’ve selected. Users should also be able to confirm a scheduled show through the site after contacting each other. There will be a schedule button that when clicked, will prompt them for the day, time, description, and optionally, the agreed upon payment for the show. If a user schedules a show, the other user will be notified and can accept or decline the scheduled show. The show can be cancelled at any time. 
4. There will be a rating system that will allow musicians and venue owners to rate each other on a scale of 1-5 and leave a comment, only if they scheduled a show through the site and the date of the show has passed. This rating will appear on users’ profiles.
5. We want a browse page that will recommend musicians or venues to users. Recommendations should be based on search history and previous shows booked. Recommendation criteria will depend on whether the user is a musician or a venue owner. Musicians will be recommended venues in locations they’ve played before, venues that frequently book musicians of the same or similar genre as the musician browsing, and venues they’ve played at before and given a rating of 4 or 5. Venue owners will be recommended musicians whose genre is similar or the same as those they’ve booked before, musicians who frequently book shows in their area, and musicians they’ve worked with before and rated a 4 or 5. Users with no previous booking activity will be recommended users who are close, well-rated, or popular. We could also recommend based on schedule line-ups.
6. We will create an Events page where any registered user can create and post an event, like a music festival, a competition, or a get-together. Events should be seperated by city, and you should be able to select a city from a drop-down to view its events. Events should have a link the profile of the person who posted it.

## Initial Modeling

### Use Case Diagrams

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
6. [E] As a venue owner, I want to be able to show my location on a map in my profile so that musicians know where I am located.


7. [E] As a registered user, I want to set my availability and display it on my profile with a calender so that other users know when I am available for a show.
3. [E] As a user, I want to be able to search for other users and filter the results so that I can find people I want to work with.
4. [E] As a registered user, I want to contact people directly through the site so that I can communicate quickly and effectively.
5. [E] As a registered user, I want to be able to book shows through the site so that I have a clear record of the show date and time.
6. [E] As a registered user, I want to rate people that I've worked with on the site so that others can see what kind of experience I had with them.
7. [E] As a registered user, I want a page where I can view recommendations tailored to me so that I can more easily find someone to work with.
8. [E] As a registered user, I want to be able to post and view events in my area so that I have an easy way to find work or meet-ups.

## Initial Architecture Envisioning

## Agile Data Modeling

## Refined Vision Statement

## Timeline and Release Plan
