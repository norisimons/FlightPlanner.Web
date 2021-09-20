# FlightPlanner.Web
1st itteration

Flight Planner ✈ ✈ ✈
Goal
Your goal is to create an application which can store flights between different airports and allows you to search them.

Getting Started
Download dependencies: npm install
Execute tests (your app must be running locally on port 8080): npm test
Generate demo data: npm run demo
Assignments
Before any step

Create a branch named exactly as required in each step, otherwise pull request will be declined.

1. Setup project (branch name: init)
Create a public repository called flight-planner under your github account. Remember - your code will be visible to the world!

Java
Definition of Done:

Build is successful
2. Implement in-memory type application (branch name: feature/in-memory-app)
Prerequisites:

Java
Goal:

Get all tests green while storing all the information in memory, list or any other suitable data structure can be used.
Definition of Done:

Build is successful
3. Implement database type application (branch name: feature/adding-database)
Prerequisites:

Persist flight data in the database
Keep possibility to run application in no-database mode
Java
Notes about the database schema:

Use sequence for id generation
Properly apply foreign keys
Naming
Indexes must be present
Apply non-null constraints where applicable
Apply unique constraints where applicable
Definition of Done:

Build is successful
External tests are passing in in-memory & database mode
