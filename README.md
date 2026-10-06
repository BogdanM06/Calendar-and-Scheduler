# Scheduler

A desktop calendar and scheduling application developed using **C# Windows Forms** and **SQL Server**.

## Overview

Scheduler is a calendar application designed to help users organise their events and find suitable times to meet with friends. The application allows users to create and manage events, manage their friends, and identify available meeting times based on existing schedules.

The project was developed as part of my programming coursework and demonstrates the use of **object-oriented programming, database management, user interfaces and algorithms**.

## Features

* Create, edit and delete calendar events
* View events within a calendar interface
* Support for repeating events:

  * Daily
  * Weekly
  * Monthly
  * Annually
* Set event priorities
* Create all-day events
* Add and manage friends
* Find suitable meeting times based on users' existing events
* Send meeting invitations to friends
* Accept or reject invitations
* Store user and event information in a SQL database

## MeetupHub

The **MeetupHub** feature allows users to find suitable times to meet with their friends.

The application compares the user's schedule with their friends' schedules and identifies available time slots. Meeting invitations can then be sent to selected friends, with the event only being added to the relevant calendars once the invitation has been accepted.

## Technologies Used

* **C#**
* **Windows Forms**
* **SQL Server / LocalDB**
* **Microsoft.Data.SqlClient**
* **Visual Studio**
* **Object-Oriented Programming**

## Database

The application uses a SQL Server database to store information including:

* Users
* Friends
* Events
* Invitations

The database is included as a **SQL Server Database Project (`.sqlproj`)**, allowing the database structure to be recreated and managed separately from the application.

## Project Structure

```text
Scheduler_NEA/
├── Scheduler_NEA.sln
│
├── Scheduler_NEA/
│   ├── C# source files
│   ├── Forms
│   └── Properties
│
└── Database/
    ├── Database.sqlproj
    ├── Tables
    └── ...
```

## Screenshots

### Calendar

*Add a screenshot of your main calendar interface here.*

### MeetupHub

*Add a screenshot of the MeetupHub interface here.*

## What I Learned

Through developing this project, I gained experience in:

* Developing desktop applications using C# and Windows Forms
* Applying object-oriented programming principles
* Designing and interacting with relational databases
* Writing SQL queries
* Connecting a C# application to SQL Server
* Developing algorithms to compare users' schedules
* Designing a user-friendly graphical interface
* Debugging and testing a larger software project
