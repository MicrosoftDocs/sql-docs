---
title: "Create a simple simulation (SQL and R deep dive) | Microsoft Docs"
ms.custom: ""
ms.date: "12/14/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: f420b816-ddab-4a1a-89b9-c8285a2d33a3
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Create a simple simulation (SQL and R deep dive)

This article is the final step in the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

Until now you've been using R functions that are designed specifically for moving data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a local compute context. However, suppose you write a custom R function of your own, and want to run it in the server context?

You can call an arbitrary function in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, by using the [rxExec](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxexec) function. You can also use **rxExec** to explicitly distribute work across cores in a single server.

In this lesson, you use the remote server to create a simple simulation. The simulation doesn't require any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data; the example only demonstrates how to design a custom function and then call it using the **rxExec** function.

For a more complex example of using **rxExec**, see this article: [Coarse grain parallelism with foreach and rxExec](http://blog.revolutionanalytics.com/2015/04/coarse-grain-parallelism-with-foreach-and-rxexec.html)

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
                { result \<- "Loss" }
                else if (count > 1 && roll == 7 )
                { result \<- "Loss" }
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

## Conclusions

In this tutorial, you have become proficient with these tasks:
  
-   Getting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to use in analyses
  
-   Creating and modifying data sources in R
  
-   Passing models, data, and plots between your workstation and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] server
  

If you would like to experiment with these techniques using a larger dataset of 10 million observations, the data files are available from the Revolution analytics web site: [Index of datasets](http://packages.revolutionanalytics.com/datasets)

To re-use this walkthrough with the larger data files, download the data, and then modify each of the data sources as follows:

1. Modify the variables `ccFraudCsv` and `ccScoreCsv` to point to the new data files
2. Change the name of the table referenced in *sqlFraudTable* to `ccFraud10`
3. Change the name of the table referenced in *sqlScoreTable* to `ccFraudScore10`

## Additional samples

Now that you've mastered the use of compute contexts and RevoScaler functions to pass and transform data, check out these tutorials:

[R tutorials for Machine Learning Services](machine-learning-services-tutorials.md)
## Previous step

[Move data between SQL Server and XDF file](../../advanced-analytics/tutorials/deepdive-move-data-between-sql-server-and-xdf-file.md)
