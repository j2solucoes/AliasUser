# AliasUser

Alias Github searches for users with different logins and maps them. For this task it is necessary to inform the uri Git of the repository informed and a valid user and password for Github.

![image](https://user-images.githubusercontent.com/31331474/29950471-62ee7bc4-8e92-11e7-9338-9146df5a04d3.png)

In the example below was searched in the Angular Rpositorio with the user jonathasas and password of the same.

3 Aliases were found

1 two Brandon users with brandonroberts and aboveyou00 logins

2 two Mike users with BeastCode and mikeybyker logins

3 James and James Ward with CrazyPython and jamesward logins

A threshold of 93% was adopted and the Levenshtein algorithm was used to compare the texts.

To analyze the aliases, the following heuristics are used:

1- Calculation of Levenshtein between user name A and user B> = threshold.

Or

2- Calculation of Levenshtein between the first name of users A and B> = threshold and the initials of the last name are equal.

Or

3- email from user B until @ contains the initial and last name of user A.

Or

4- Mail from user B to the @ contain the initial name of user A and emailsB contain the letter the initial letter of the last name.

Or

5-email from user B until the @ contain the final name of user A and emailsB contain the initial letter of the first name.

Or

6 - email from user A to @ was not equal to "GITHUB and Levenshtein's calculation of the initial names of user A and B be >= threshold / 2 and Levenshtein's calculation enter the emails to be> = threshold)


Procedures for use:

To run only, in the root of the repository we have the file AliasUsersGitHub.rar compressed.

1- Unzip the file

2- Run the AliasUsers FileGitHub.exe

3-Inform the Url of the desired Repository

4-User and valid github password

5-OK

6-The application will show 3 tables

  6.1 The first with all users of the project
  
  6.2 The second One Alias Cluster and quantity found.
  
  6.3 The third users considered with Aliases for each row of the second selected table.
  
  
To be able to compile the project and use the compact dlls in AliasUser / AliasUser / AliasUser / Dlls External /
