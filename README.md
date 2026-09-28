# HLogger

Console based CRUD application to track Habits and store it using SQLite.

## Given Requirements:

- This is an application where you’ll log occurrences of a habit.
- This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- Users need to be able to input the date of the occurrence of the habit
- The application should store and retrieve data from a real database
- When the application starts, it should create a sqlite database, if one isn’t present.
- It should also create a table in the database, where the habit will be logged.
- The users should be able to insert, delete, update and view their logged habit.
- You should handle all possible errors so that the application never crashes.
- You can only interact with the database using ADO.NET. You can’t use mappers such as Entity Framework or Dapper.
- Follow the DRY Principle, and avoid code repetition.
- Your project needs to contain a Read Me file where you'll explain how your app works and tell a little bit about your thought progress. What was hard? What was easy? What have you learned? 

### Optional Challenges included in the app
- Let the users create their own habits to track. That will require that you let them choose the unit of measurement of each habit. Hot tip: You should not create a table for each habit.
- Seed Data into the database automatically when the database gets created for the first time, generating a few habits and inserting a hundred records with randomly generated values. This is specially helpful during development so you don't have to reinsert data every time you create the database. 

## Features

- SQLite database connection
- CRUD DB functions
- A console based UI using spectre 

## Challenges

  It was my first time using SQL. I did the follow along video without having any knowledge of SQL and I was really confused about what I was doing.
	Everything felt cryptic. I decided to do a course on SQL and then redo the project, using the video project as a memo.
  
  As i did the Object Oriented course and It was using spectre, I decided to use spectre as well to learn how it works. 
	During the previous course, spectre felt cryptic too. I used this project as an opporunity to read the documentation.
  
  After finishing a first new draft, my "Program.cs" file was cluttered and I started to refactor and put everything in separate files.
	It was quite challenging and rewarding to do so. As I added a function to view habits per category, I saw I had to do it everywhere, which brought quite some complexity.
	I wanted to have versatile functions (especially for the prompt) but 75% of the cases it had to be specific.
  
  It took me 4 hours to follow the video. Redoing it was around 16 Hours, with 6-8 refactoring.. And I spend 10 Hour with SQL.

## Lessons Learned

- Getting familiar with SQLite
- SQL basic queries.
- Getting more confortable and confident with refactoring
- Getting at ease with Spectre
- Stay more focused and don't loose time with petty things like [Orange1] and [Orange2]...

## Areas to Improve

- I got frustrated with tabs and indend and the overall presentation, so there might be somethings to improve there.
- Spectre is not handling correctly long multiselection prompt and I decided to let it. 
		(As I am writing this and am testing in VS code terminal, I see even more visual bugs as within windows terminal.)
- I wanted to learn about parameterized queries, but I decided to skip it and get confident with SQL. I'll do in another project, so this project can be a reference.
- Enums, types and inference. I don't always get what is happening.
- Using git rather than going freestyle without backup when refactoring
- I skiped the DayTime validation complexity using Spectre. I will have to face it somewhen. Seems like great fun.
	
## Resources Used
	
- The c# academy Habit Tracker App Tutorial
- CodeCademy : Learn SQL
- Spectre documentation
- StackOverflow forum for specific function (Addition DateTimes)
- CoPilot when getting stuck, to explain synthax when I am confused, or look for some function I assumed should have existed

