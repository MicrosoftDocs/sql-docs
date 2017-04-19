---
title: "Move Data between SQL Server and XDF File (Data Science Deep Dive) | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: 40887cb3-ffbb-4769-9f54-c006d7f4798c
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 4-1 - Move Data between SQL Server and XDF File
When you are working in a local compute context, you have access to both local data files and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database (defined as an *RxSqlServerData* data source).  
  
In this section, you'll learn how to get data and store it in a file on the local computer so that you can perform transformations on the data. When you're done, you'll use the data in the file to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, by using *rxDataStep*.  
  
## Create a SQL Server Table from an XDF File  

The *rxImport* function lets you import data from any supported data source to a local XDF file. Using a local file can be convenient if you want to do many different analyses on data that is stored in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and you want to avoid running the same query over and over.  
  
For this exercise, you'll use the credit card fraud data again. In this scenario, you've been asked to do some extra analysis on users in the states of California, Oregon, and Washington. To be more efficient, you've decided to store data for just these states on your local computer and work with the variables gender, cardholder, state, and balance.  
  
1.  Re-use the *stateAbb* vector you created earlier to identify the levels to include, and then print to the console the new variable, *statesToKeep*.  
  
    ```  
    statesToKeep <- sapply(c("CA", "OR", "WA"), grep, stateAbb)   
  
    statesToKeep  
  
    ```  
 **Results**
CA |  OR  | WA 
-- | -- | --
 5 |  38  | 48 
  
2.  Now you'll define the data you want to bring over from SQL Server, using a [!INCLUDE[tsql](../../includes/tsql-md.md)] query.  Later you'll use this variable as the *inData* argument for *rxImport*.  
  
    ```R  
    importQuery <- paste("SELECT gender,cardholder,balance,state FROM",  sqlFraudTable,  "WHERE (state = 5 OR state = 38 OR state = 48)")  
  
    ```  
  
    Make sure there are no hidden characters such as line feeds or tabs in the query.  
  
3.  Next, you'll define the columns to use when working with the data in R.  
  For example, in the smaller data set, you need only three factor levels, because the query will return data for only three states.  You can re-use the *statesToKeep* variable to identify the correct levels to include.  
  
    ```R  
    importColInfo <- list(   
        gender = list( type = "factor",  levels = c("1", "2"), newLevels = c("Male", "Female")),       
        cardholder = list(  type = "factor",  levels = c("1", "2"), newLevels = c("Principal", "Secondary")),     
        state = list(   type = "factor",  levels = as.character(statesToKeep), newLevels = names(statesToKeep))   
            )  
  
    ```  
  
4.  Set  the compute context to **local**, because you want all the data available on your local computer.  
  
    ```R  
    rxSetComputeContext("local")   
    ```  
  
5.  Create the data source object by passing all the variables that you just defined as arguments to *RxSqlServerData*.  
  
    ```R  
    sqlServerImportDS <- RxSqlServerData(  
        connectionString = sqlConnString,   
        sqlQuery = importQuery,   
        colInfo = importColInfo)  
  
    ```  
  
6.  Then, call *rxImport* to save the data in the current working directory, in a file named `ccFraudSub.xdf`.  
  
    ```R  
    localDS <- rxImport(inData = sqlServerImportDS,   
        outFile = "ccFraudSub.xdf",    
        overwrite = TRUE)  
  
    ```  
  
    The *localDs* object returned by the *rxImport* function is a light-weight *RxXdfData* data source object that represents the ccFraud.xdf data file stored locally on disk.  
  
7.  Call *rxGetVarInfo* on the XDF file to verify that the data schema is the same.  
  
    ```R  
    rxGetVarInfo(data = localDS)   
    ```  
    **Results**
    
    *rxGetVarInfo(data = localDS)*    
    *Var 1: gender, Type: factor, no factor levels available*    
    *Var 2: cardholder, Type: factor, no factor levels available*    
    *Var 3: balance, Type: integer, Low/High: (0, 22463)*    
    *Var 4: state, Type: factor, no factor levels available*
  
8.  You can now call various R functions to analyze the *localDs* object, just as you would with the source data on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example:  
  
    ```R  
    rxSummary(~gender + cardholder + balance + state, data = localDS)    
    ```  
  
Now that you've mastered the use of compute contexts and working with various data sources, it's time to try something fun.  
  
In the next and final lesson, you'll create a simple simulation using a custom R function and run it on the remote server.  
  
## Next Step  
[Lesson 5: Create a Simple Simulation &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-5-create-a-simple-simulation-data-science-deep-dive.md)  
  
## Previous Step  
[Lesson 4: Analyze Data in Local Compute Context &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-4-analyze-data-in-local-compute-context-data-science-deep-dive.md)  
  
## See Also  
[Data Science Deep Dive: Using the RevoScaleR Packages](../../advanced-analytics/r-services/data-science-deep-dive-using-the-revoscaler-packages.md)  
  
  
  

