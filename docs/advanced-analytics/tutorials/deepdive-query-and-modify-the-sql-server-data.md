---
title: Query and modify the SQL Server data using RevoScaleR - SQL Server Machine Learning
description: Tutorial walkthrough on how to query and modify data using the R language on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Query and modify the SQL Server data (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In the previous lesson, you loaded the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In this step, you can explore and modify data using **RevoScaleR**:

> [!div class="checklist"]
> * Return basic information about the variables
> * Create categorical data from raw data

Categorical data, or *factor variables*, are useful for exploratory data visualizations. You can use them as inputs to histograms to get an idea of what variable data looks like.

## Query for columns and types

Use an R IDE or RGui.exe to run R script. 

First, get a list of the columns and their data types. You can use the function [rxGetVarInfo](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxgetvarinfoxdf) and specify the data source you want to analyze. Depending on your version of **RevoScaleR**, you could also use [rxGetVarNames](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxgetvarnames). 
  
```R
rxGetVarInfo(data = sqlFraudDS)
```

**Results**

```R
Var 1: custID, Type: integer
Var 2: gender, Type: integer
Var 3: state, Type: integer
Var 4: cardholder, Type: integer
Var 5: balance, Type: integer
Var 6: numTrans, Type: integer
Var 7: numIntlTrans, Type: integer
Var 8: creditLine, Type: integer
Var 9: fraudRisk, Type: integer
```

## Create categorical data

All the variables are stored as integers, but some variables represent categorical data, called *factor variables* in R. For example, the column *state* contains numbers used as identifiers for the 50 states plus the District of Columbia. To make it easier to understand the data, you replace the numbers with a list of state abbreviations.

In this step, you create a string vector containing the abbreviations, and then map these categorical values to the original integer identifiers. Then you use the new variable in the *colInfo* argument, to specify that this column be handled as a factor. Whenever you analyze the data or move it, the abbreviations are used and the column is handled as a factor.

Mapping the column to abbreviations before using it as a factor actually improves performance as well. For more information, see [R and data optimization](../r/r-and-data-optimization-r-services.md).

1. Begin by creating an R variable, *stateAbb*, and defining the vector of strings to add to it, as follows.
  
    ```R
    stateAbb <- c("AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC",
        "DE", "FL", "GA", "HI","IA", "ID", "IL", "IN", "KS", "KY", "LA",
        "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NB", "NC", "ND",
        "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI","SC",
        "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY")
    ```

2. Next, create a column information object, named *ccColInfo*, that specifies the mapping of the existing integer values to the categorical levels (the abbreviations for states).
  
    This statement also creates factor variables for gender and cardholder.
  
    ```R
    ccColInfo <- list(
    gender = list(
              type = "factor",
              levels = c("1", "2"),
              newLevels = c("Male", "Female")
              ),
    cardholder = list(
                  type = "factor",
                  levels = c("1", "2"),
                  newLevels = c("Principal", "Secondary")
                   ),
    state = list(
             type = "factor",
             levels = as.character(1:51),
             newLevels = stateAbb
             ),
    balance = list(type = "numeric")
    )
    ```
  
3. To create the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source that uses the updated data, call the **RxSqlServerData** function as before, but add the *colInfo* argument.
  
    ```R
    sqlFraudDS <- RxSqlServerData(connectionString = sqlConnString,
    table = sqlFraudTable, colInfo = ccColInfo,
    rowsPerRead = sqlRowsPerRead)
    ```
  
    - For the *table* parameter, pass in the variable *sqlFraudTable*, which contains the data source you created earlier.
    - For the *colInfo* parameter, pass in the *ccColInfo* variable, which contains the column data types and factor levels.

4.  You can now use the function **rxGetVarInfo** to view the variables in the new data source.
  
    ```R
    rxGetVarInfo(data = sqlFraudDS)
    ```

    **Results**
    
    ```R
    Var 1: custID, Type: integer
    Var 2: gender  2 factor levels: Male Female
    Var 3: state   51 factor levels: AK AL AR AZ CA ... VT WA WI WV WY
    Var 4: cardholder  2 factor levels: Principal Secondary
    Var 5: balance, Type: integer
    Var 6: numTrans, Type: integer
    Var 7: numIntlTrans, Type: integer
    Var 8: creditLine, Type: integer
    Var 9: fraudRisk, Type: integer
    ```

Now the three variables you specified (*gender*, *state*, and *cardholder*) are  treated as factors.

## Next steps

> [!div class="nextstepaction"]
> [Define and use compute contexts](../../advanced-analytics/tutorials/deepdive-define-and-use-compute-contexts.md)