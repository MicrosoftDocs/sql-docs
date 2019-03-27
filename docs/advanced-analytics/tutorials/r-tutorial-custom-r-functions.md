---
title: Run custom R functions on SQL Server using RevoScaleR rxExec - SQL Server Machine Learning
description: Tutorial walkthrough on how to run custom R script on SQL Server using RevoScaleR functions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Run custom R functions on SQL Server using rxExec
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can run custom R functions in the context of SQL Server by passing your function via [rxExec](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxexec), assuming that any libraries your script requires are also installed on the server and those libraries are compatible with the base distribution of R. 

The **rxExec** function in **RevoScaleR** provides a mechanism for running any R script you require. Additionally,  **rxExec** is able to explicitly distribute work across multiple cores in a single server, adding scale to scripts that are otherwise limited to the resource constraints of the native R engine.

In this tutorial, you will use simulated data to demonstrate execution of a custom R function that runs on a remote server.

## Prerequisites

+ [SQL Server 2017 Machine Learning Services (with R)](../install/sql-machine-learning-services-windows-install.md) or [SQL Server 2016 R Services (in-Database)](../install/sql-r-services-windows-install.md)
  
+ [Database permissions](../security/user-permission.md) and a SQL Server database user login

+ [A development workstation with the RevoScaleR libraries](../r/set-up-a-data-science-client.md)

The R distribution on the client workstation provides a built-in **Rgui** tool that you can use to run the R script in this tutorial. You can also use an IDE such as RStudio or R Tools for Visual Studio.

## Create the remote compute context

Run the following R commands on a client workstation. For example, you are using **Rgui**, start it from this location: C:\Program Files\Microsoft\R Client\R_SERVER\bin\x64\.

1. Specify the connection string for the SQL Server instance where computations are performed. The server must be configured for R integration. The database name is not used in this exercise, but the connection string requires one. If you have a test or sample database, you can use that.

    **Using a SQL login**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL-Server-instance-name>; Database=<database-name>;Uid=<SQL-user-name>;Pwd=<password>"
    ```

    **Using Windows authentication**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL-Server-instance-name>;Database=<database-name>;Trusted_Connection=True"
    ```

2. Create a remote compute context to the SQL Server instance referenced in the connection string.

    ```R
    sqlCompute <- RxInSqlServer(connectionString = sqlConnString)
    ```

3. Activate the compute context and then return the object definition as a confirmation step. You should see the properties of the compute context object.

    ```R
    rxSetComputeContext(sqlCompute)
    rxGetComputeContext()
    ```

## Create the custom function

In this exercise, you will create a custom R function that simulates a common casino consisting of rolling a pair of dice. Rules of the game determine a win or loss outcome:

+ Roll a 7 or 11 on your initial roll, you win.
+ Roll 2, 3, or 12, you lose.
+ Roll a 4, 5, 6, 8, 9, or 10, that number becomes your point, and you continue rolling until you either roll your point again (in which case you win) or roll a 7, in which case you lose.

The game is easily simulated in R, by creating a custom function, and then running it many times.

1.  Create the custom function using the following R code:
  
    ```R
    rollDice <- function()
    {
        result <- NULL
        point <- NULL
        count <- 1
            while (is.null(result))
            {
                roll <- sum(sample(6, 2, replace=TRUE))
  
                if (is.null(point))
                { point <- roll }
                if (count == 1 && (roll == 7 || roll == 11))
                {  result <- "Win" }
                else if (count == 1 && (roll == 2 || roll == 3 || roll == 12))
                { result <- "Loss" }
                else if (count > 1 && roll == 7 )
                { result <- "Loss" }
                else if (count > 1 && point == roll)
                { result <- "Win" }
                else { count <- count + 1 }
            }
            result
    }
    ```
  
2.  Simulate a single game of dice by running the function.
  
    ```R
    rollDice()
    ```
  
    Did you win or lose?
  
Now that you have an operational script, let's see how you can use **rxExec** to run the function multiple times to create a simulation that helps determine the probability of a win.

## Pass rollDice() in rxExec

To run an arbitrary function in the context of a remote SQL Server, call the **rxExec** function.

1. Call the custom function as an argument to **rxExec**, together with other parameters that modify the simulation.
  
    ```R
    sqlServerExec <- rxExec(rollDice, timesToRun=20, RNGseed="auto")
    length(sqlServerExec)
    ```
  
    + Use the *timesToRun* argument to indicate how many times the function should be executed.  In this case, you roll the dice 20 times.
  
    + The arguments *RNGseed* and *RNGkind* can be used to control random number generation. When *RNGseed* is set to **auto**, a parallel random number stream is initialized on each worker.
  
2. The **rxExec** function creates a list with one element for each run; however, you won't see much happening until the list is complete. When all the iterations are complete, the line starting with **length** will return a value.
  
    You can then go to the next step to get a summary of your win-loss record.
  
3. Convert the returned list to a vector using R's **unlist** function, and summarize the results using the **table** function.
  
    ```R
    table(unlist(sqlServerExec))
    ```
  
    Your results should look something like this:
  
     *Loss  Win*
     *12  8*

## Conclusion

Although this exercise is simplistic, it demonstrates an important mechanism for integrating arbitrary R functions in R script running on SQL Server. To summarize the key points that make this technique possible:

+ SQL Server must be configured for machine learning and R integration: [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the R feature, or [SQL Server 2016 R Services (in-Database)](../install/sql-r-services-windows-install.md).

+ Open-source or third-party libraries used in your function, including any dependencies, must be installed on SQL Server. For more information, see [Install new R packages](../r/install-additional-r-packages-on-sql-server.md).

+ Moving script from a development enviroment to a hardened production environment can introduce firewall and network restrictions. Test carefully to make sure your script is able to perform as expected.

## Next steps

For a more complex example of using **rxExec**, see this article: [Coarse grain parallelism with foreach and rxExec](https://blog.revolutionanalytics.com/2015/04/coarse-grain-parallelism-with-foreach-and-rxexec.html)
