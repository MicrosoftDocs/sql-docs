---
title: Create a simple simulation | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Wrap R code in the rxExec function to run it on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can call an arbitrary R function in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer by using the [rxExec](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxexec) function. You can also use **rxExec** to explicitly distribute work across cores in a single server.

In this tutorial, you will use simulated data to demonstrate execution of a custom R function that runs on a remote server. This simulation doesn't require any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.


## Create the remote compute context

1. Specify the connection string for the instance where computations are performed. The database name is not used, but the connectiong string requires one. If you have a test or sample database, you can use that.

    **Using a SQL login**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL-Server-instance-name>; Database=<database-name>;Uid=<SQL-user-name>;Pwd=<password>"
      ```

    **Using Windows authentication**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL-Server-instance-name>;Database=<database-name>;Trusted_Connection=True"
    ```

2. Create the compute context.

    ```R
    CCsqlrxExec <- RxInSqlServer(connectionString = sqlConnString)
    ```

3. Set the compute context and then return the object definition as a confirmation step.

    ```R
    rxSetComputeContext(CCsqlrxExec)
    rxGetComputeContext()
    ```

## Create the custom function

A common casino game consists of rolling a pair of dice, with these rules:

- If you roll a 7 or 11 on your initial roll, you win.
- If you roll 2, 3, or 12, you lose.
- If you roll a 4, 5, 6, 8, 9, or 10, that number becomes your point and you continue rolling until you either roll your point again (in which case you win) or roll a 7, in which case you lose.

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
  
2.  To simulate a single game of dice, run the function.
  
    ```R
    rollDice()
    ```
  
    Did you win or lose?
  
Now let's see how you can use **rxExec** to run the function multiple times, to create a simulation that helps determine the probability of a win.

## Create the simulation

To run an arbitrary function in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, you call the **rxExec** function. Although **rxExec** also supports distributed execution of a function in parallel across nodes or cores in a server context, here it runs your custom function on the SQL Server computer.

1. Call the custom function as an argument to **rxExec**, together with other parameters that modify the simulation.
  
    ```R
    sqlServerExec <- rxExec(rollDice, timesToRun=20, RNGseed="auto")
    length(sqlServerExec)
    ```
  
    - Use the *timesToRun* argument to indicate how many times the function should be executed.  In this case, you roll the dice 20 times.
  
    - The arguments *RNGseed* and *RNGkind* can be used to control random number generation. When *RNGseed* is set to **auto**, a parallel random number stream is initialized on each worker.
  
2. The **rxExec** function creates a list with one element for each run; however, you won't see much happening until the list is complete. When all the iterations are complete, the line starting with `length` will return a value.
  
    You can then go to the next step to get a summary of your win-loss record.
  
3. Convert the returned list to a vector using R's `unlist` function, and summarize the results using the `table` function.
  
    ```R
    table(unlist(sqlServerExec))
    ```
  
    Your results should look something like this:
  
     *Loss  Win*
     *12  8*

## Next steps

For a more complex example of using **rxExec**, see this article: [Coarse grain parallelism with foreach and rxExec](https://blog.revolutionanalytics.com/2015/04/coarse-grain-parallelism-with-foreach-and-rxexec.html)
