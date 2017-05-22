---
title: "Query and Modify the SQL Server Data| Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2017"
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
ms.assetid: 8c7007a9-9a8f-4dcd-8068-40060d4f6444
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Query and Modify the SQL Server Data

Now that you've loaded the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can use the data sources you created  as arguments to R functions in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], to get basic information about the variables, and generate summaries and histograms.

In this step, you'll re-use the data sources to do some quick analysis and then enhance the data.

## Query the Data

First, get a list of the columns and their data types.

1.  Use the function **rxGetVarInfo** and specify the data source you want to analyze.
  
    ```R
    rxGetVarInfo(data = sqlFraudDS)
    ```

    **Results**
    
    *Var 1: custID, Type: integer*
    
    *Var 2: gender, Type: integer*
    
    *Var 3: state, Type: integer*
    
    *Var 4: cardholder, Type: integer*
    
    *Var 5: balance, Type: integer*
    
    *Var 6: numTrans, Type: integer*
    
    *Var 7: numIntlTrans, Type: integer*
    
    *Var 8: creditLine, Type: integer*
    
    *Var 9: fraudRisk, Type: integer*


## Modify Metadata

All the variables are stored as integers, but some variables represent categorical data,called *factor variables* in R. For example, the column *state* contains numbers used as identifiers for the 50 states plus the District of Columbia.  To make it easier to understand the data, you replace the numbers with a list of state abbreviations.

In this step, you will provide a string vector containing the abbreviations, and then map these categorical values to the original integer identifiers. After this varaible is ready, you'll use it in the *colInfo* argument, to specify that this column be handled as a factor. Thereafter, the abbreviations will be used and the column handled as a factor whenever this data is analyzed or imported.

1. Begin by creating an R variable, *stateAbb*, and defining the vector of strings to add to it, as follows:
  
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
  
3. To create the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source that uses the updated data, call the *RxSqlServerData* function as before but add the *colInfo* argument.
  
    ```R
    sqlFraudDS <- RxSqlServerData(connectionString = sqlConnString,
    table = sqlFraudTable, colInfo = ccColInfo,
    rowsPerRead = sqlRowsPerRead)
    ```
  
    - For the *table* parameter, pass in the variable *sqlFraudTable*, which contains the data source you created earlier.
    - For the *colInfo* parameter, pass in the *ccColInfo* variable, which contains the column data types and factor levels.
    - Mapping the column to abbreviations before using it as a factor actually improves performance as well. For more information, see [R and Data Optimization](https://msdn.microsoft.com/library/mt723575.aspx)
  
4.  You can now use the function rxGetVarInfo to view the variables in the new data source.
  
    ```R
    rxGetVarInfo(data = sqlFraudDS)
    ```

    **Results**
    
    *Var 1: custID, Type: integer*
    
    *Var 2: gender  2 factor levels: Male Female*
    
    *Var 3: state   51 factor levels: AK AL AR AZ CA ... VT WA WI WV WY*
    
    *Var 4: cardholder  2 factor levels: Principal Secondary*
    
    *Var 5: balance, Type: integer*
    
    *Var 6: numTrans, Type: integer*
    
    *Var 7: numIntlTrans, Type: integer*
    
    *Var 8: creditLine, Type: integer*
    
    *Var 9: fraudRisk, Type: integer*

Now the three variables you specified (_gender_, _state_, and _cardholder_) are  treated as factors.

## Next Step

[Define and Use Compute Contexts](../../advanced-analytics/tutorials/deepdive-define-and-use-compute-contexts.md)

## Previous Step

[Create SQL Server Data Objects using RxSqlServerData](../../advanced-analytics/tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)



