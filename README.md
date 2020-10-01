# Challenge29

This Repository contains the solution to the second task of the Technical Support Engineer Challenge.

The project's goal is to develop a simple search engine that, when given a pokemon name, returns its Shakespearean description.

Basically the solution is a .NET Core application consisting of App Layer, Business Layer and Data Layer.

* The App Layer is in the folder - Pokemon.Search
* The Business Layer is in the folder - Pokemon.Services
* The Data Layer is in the folder - Pokemon.Data
* The Pokemon.Clients folder contains the receiving end of the API request that receives data fetched using the PokeAPI and FunTranslations Shakespeare API.
* The Tests are written in the Pokemon.Tests folder.
* Swagger API has been used to display available calls of the APIs on execution demonstrate execution of these APIs by selecting whichever option is needed.


### Following are the steps to run the program.

* Make sure Visual Studio 19 is installed.
* Setup Github on the Team Explorer window and in the repository Click on the "Download Code" Dropdown and select "Open with Visual Studio".
* In Visual Studio the Team Explorer opens with the Local Git Repositories tab where we give the desired path specifications and click on "Clone".
* Now the entire project has been cloned to local system in Visual Studio 19.
* Build to make sure that all 6 modules are built successfully.
* In the Run menu the option selected by default is "IIS Express" and this needs to be changed by selecting the dropdown near the run option and selecting "Pokemon.Search".
* The Swagger API interface shows the UI of the Pokemon Search Engine where we can select the "GET  /Pokemon/{pokemonName}" method then click on "Try it Out".
* We then enter the desired Pokemon Name and click on "Execute".
* The translated Shakespearean Definition is seen in the response body 
* Example - Pokemon Name - Charizard, Translation - Spits fire yond is hot enow to melt boulders. Known to cause forest fires unintentionally.
* The image files in the repository "Swagger Screen" and "Swagger Output" will give you a glimpse of the finished product.


### Following are steps to execute Docker

* Make sure Docker is installed and Visualization settings are turned on.
* Navigate to the project source location and initiate a powershell window with administrator privilige.
* Check if Docker is installed properly using the command "docker --version".
* Test installation by usig the "docker run hello-world" command.
* We list the hello-world image by using the command "docker image ls"
* Now we execute the command "docker build -f Dockerfile -t pokemonapi:v1 ." to build the dockerfile into a container image.
* Next we use the command "docker-compose up" to start the services
* Then we navigate to the url "localhost:5000/swagger/index.html" and check the UI by using the steps mentioned as to how we run the program.


### Following are steps to run test cases

* Open a new powershell in the project source location.
* Then we execute the command "dc exec dcrapi sh".
* Then we execute the command "ls" to list all files in the project.
* Next we use the "cd Pokemon.Search.Test" to navigate to the folder containing the tests
* Finally we run the command "dotnet test".
* We see the success message that the 5 tests have been executed successfully.
