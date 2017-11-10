# AliasUser


Alias Github searches for users with different logins and maps them. For this task it is necessary to inform the uri Git of the repository informed and a valid user and password for Github.

One point to emphasize is that the tool is designed so that the addition of new heuristics is performed easily.

The first screen of the tool is used to execute the heuristics to find the aliases

![image](https://user-images.githubusercontent.com/31331474/32660737-26fbc81e-c60b-11e7-9ffd-fe4905867e3d.png)


The tool initially implements 7 alias validation heusisticas.

Bird

BirdGithub
  With a change due to being observed some projects in GitHub where users had email with github @ ....
  Due to this particularity and disregarded these emails as alias

Confora

Goeminne and Mens (GOE)

Robles and Gonzales (ROB)

Olive

Kouters (Naive)

The built tool can be used in three ways:

1 - Using the AliasLib.Dll Dll which allows integrating alias validations into other projects.

2 - Running the application

Procedures for use:

To run only, in the root of the repository we have the file AliasUsersGitHub.rar compressed.

2.1- Unzip the file

2.22- Run the AliasUsers FileGitHub.exe

2.3-Inform the Url of the desired Repository

2.4-User and valid github password

2.5-OK

2.6-The application will show 4 tables

  2.6.1 The first with all users of the project
  
  2.6.2 The second shows the heuristics executed in the validation
  
  2.6.2 The third One Alias Cluster and quantity found for each row of the second selected table..
  
  2.6.3 The fourth users considered with Aliases for each row of the third selected table.
  
3 Downloading the project in github and running  
To be able to compile the project and use the compact dlls in AliasUser / AliasUser / AliasUser / Dlls External /

The tool also provides the option to export the data after processing the heuristics to csv or xml and import a previously exported xml.

The second screen of the tool is used for the validation from the alias pointed after the execution of the heuristics.

The second screen is composed of two tabs where the first one is used to create the data for the oracle manually that will validate the aliases. In this screen you can export and import an xml of data.

![image](https://user-images.githubusercontent.com/31331474/32661222-3e011508-c60d-11e7-8948-b2c94a51632e.png)

The first table should contain the names of the repositories in github anilisados. The second table lists the alias sequences found and the third alias.

The second tab is used to perform validation. In it we can import the aliases found by the heuristics in the first screen and export the data after the validation.

![image](https://user-images.githubusercontent.com/31331474/32661628-c9ffcecc-c60e-11e7-9074-df92b336c684.png)

this screen is made up of four tables.

The first one has the names of the GitHub repositories.

The second has the heuristics to be validated.

The third one has the alias sequences and a status that will be filled after the validation of the aliases found by the heuristics with the oracle. The Status can be (P = Positive, FP = False Positive and FN = FalseNegative)

The fourth table is the alias.

