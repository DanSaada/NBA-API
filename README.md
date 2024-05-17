# NBA-API
A fictional system that integrates with an API offered by the NBA and produce some insights about a given game.


<img src="https://github.com/DanSaada/NBA-API/assets/112869076/a04b41fd-32c7-4b4e-892b-856ebd5f663f" width="200">


## Introduction
This project is a .NET Core Web API for accessing NBA game data. 
It includes endpoints to retrieve player names, actions by player names, and top performers in a game. 
Additionally, it contains unit tests to ensure the reliability of the API.

The information is taken from this JSON file: https://cdn.nba.com/static/json/liveData/playbyplay/playbyplay_0022000180.json


## Project Structure
* Models: The Models folder contains the data structures used throughout the application.
  Each class represents a specific entity within the NBA game data, such as GamesData, Game, and Action.
  This separation ensures that the data models are isolated from the business logic and the API controllers.

* Services: The Services folder includes the NBAService class, which encapsulates the business logic of the application.
  It interacts with the models to perform operations and calculations, such as retrieving player names and calculating top performers.
  This service layer promotes reusability and separation of concerns, making it easier to maintain and extend the application.

* Controllers: The Controllers folder contains the NBAController class, which defines the API endpoints.
  Each endpoint corresponds to a specific action that can be performed on the NBA game data, such as getting all player names or fetching actions by player name.
  The controller layer is responsible for handling HTTP requests and responses, delegating the actual processing to the service layer.


## API Endpoints
* **GET /api/NBA/GetAllPlayersNames:** Retrieve all player names grouped by their team.
* **GET /api/NBA/GetAllActionsByPlayerName?playerName={playerName}:** Retrieve all actions performed by the specified player.
* **GET /api/NBA/GetTopPerformers:** Retrieve the top performers in the game.


## Installing And Executing
    
To clone and run this application, you'll need [Git](https://git-scm.com) installed on your computer.
  
From your command line:

  
```bash
# Clone this repository.
$ git clone https://github.com/DanSaada/NBA-API.git

# Go into the repository.
$ cd NBA-API

# Open the solution file NBA_API.sln in VS Code and restore the NuGet packages:.
$ dotnet restore

# Build the project:
$ dotnet build

# Run the program.
 dotnet run --project NBA_API
```


## Author
- [Dan Saada](https://github.com/DanSaada)
