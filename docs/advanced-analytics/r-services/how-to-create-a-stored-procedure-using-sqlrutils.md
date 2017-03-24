---
title: "How to Create a Stored Procedure Using sqlrutils | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 5ba99b49-481e-4b30-967a-a429b855b1bd
caps.latest.revision: 10
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# How to Create a Stored Procedure Using sqlrutils
This topic describes the steps for converting your R code to run as a T-SQL stored procedure. For best possible results, your code might need to be modified somewhat, to ensure that all inputs can be parameterized.

## Step 1. Format your R script

1. Wrap all code into a single function.

   What this means is that all variables used by the function must be defined inside the function, or should be defined as input parameters. See the [sample code](#samples) in this topic.

## Step 2. Standardize the inputs and outputs

The input parameters of the function will become the input parameters of the SQL stored procedure, and thus should conform to the following type requirements:
- Among the input parameters there can be at most one data frame.
- The objects inside the data frame, as well as all other input parameters of the function, must be of the following R data types:
    - POSIXct
    - numeric
    - character
    - integer
    - logical
    - raw

- If an input type is not one of the above types, it needs to be serialized and passed into the function as *raw*. In this case, the function must also include code to deserialize the input.

The function can output one of the following:

- A data frame containing the supported data types. All objects in the data frame must use one of the supported data types.
- A named list, containing at most one data frame. All members of the list should use one of the supported data types. 
- A NULL, if your function does not return any result

## Step 3. Make calls to the sqlrutils package to generate the stored procedure

After your R code has been cleaned up and can be called as a single function, you can begin the process of using the functions in **sqlrutils** to convert your code to a stored procedure.

Depending on the data types of the parameters, you will use different functions to capture the data and create a parameter object.

1. If your function takes input parameters, for each parameter,  create one of the following objects: 
    - If the input parameter is a data frame, use `setInputData`.
    - For all other input parameters, use `setInputParameter`.

2. If your function outputs a list, create an object to handle the desired data from the list as follows: 
    - If the variable in the list is a data frame, use `setOutputData`.
    - For all other members of the list, use `setOutputParameter`.
    - If your function outputs a data frame directly, without first wrapping it into a list, you can skip this step. 
    - Skip this step if your function returns NULL.

3. When all input and output parameters are ready, make a call to the `StoredProcedure` constructor to generate the stored procedure containing the R function.
4. To immediately register the stored procedure with the database, use `registerStoredProcedure`.

## Step 4. Execute the stored procedure

1. If you want to execute the stored procedure from R code, rather from SQL Server, and the stored procedure requires input, you must set those input parameters before the function can be executed: 
    - Call `getInputParameters` to get a list of input parameter objects.
    - Define a `$query` or set a `$value` for each input parameter. 

2. Use `executeStoredProcedure` to execute the stored procedure from the R development environment, passing the list of input parameter objects that you set.

## <a name = "samples"></a>Examples: Prepare your code 

In this example, the R code reads from a database, performs some transformations on the data, and saves it to another database. This simple example is used only to demonstrate how you might rearrange your R code to provide a simpler interface for stored procedure conversion.

**Before formatting**


```R
sqlConnFrom <- "Driver={ODBC Driver 13 for SQL Server};Server=MyServer01;Database=AirlineSrc;Trusted_Connection=Yes;"
  
sqlConnTo <- "Driver={ODBC Driver 13 for SQL Server};Server=MyServer01;Database=AirlineTest;Trusted_Connection=Yes;"
  
sqlQueryAirline <- "SELECT TOP 10000 ArrDelay, CRSDepTime, DayOfWeek FROM [AirlineDemoSmall]"
  
dsSqlFrom <- RxSqlServerData(sqlQuery = sqlQueryAirline, connectionString = sqlConnFrom)
  
dsSqlTo <- RxSqlServerData(table = "cleanData", connectionString = sqlConnTo)
  
xFunc <- function(data) {
    data$CRSDepHour <- as.integer(trunc(data$CRSDepTime))
    return(data)
    }
  
xVars <- c("CRSDepTime")
  
sqlCompute <- RxInSqlServer(numTasks = 4, connectionString = sqlConnTo)
  
rxOpen(dsSqlFrom)
rxOpen(dsSqlTo)
  
if (rxSqlServerTableExists("cleanData", connectionString = sqlConnTo))   {
    rxSqlServerDropTable("cleanData")}
  
rxDataStep(inData = dsSqlFrom, 
     outFile = dsSqlTo,
     transformFunc = xFunc,
     transformVars = xVars,
     overwrite = TRUE)
```
Note that when you use an ODBC connection rather than invoking the *RxSqlServerData* function, you must open the connection using *rxOpen* before you can perform operations on the database.



**After formatting**

In the reformatted version, the first line defines the function name.

All other code from your original R solution becomes a part of that function. 

```R
myetl1function <- function() { 
   sqlConnFrom <- "Driver={ODBC Driver 13 for SQL Server};Server=MyServer01;Database=Airline01;Trusted_Connection=Yes;"
   sqlConnTo <- "Driver={ODBC Driver 13 for SQL Server};Server=MyServer02;Database=Airline02;Trusted_Connection=Yes;"
    
   sqlQueryAirline <- "SELECT TOP 10000 ArrDelay, CRSDepTime, DayOfWeek FROM [AirlineDemoSmall]"

   dsSqlFrom <- RxSqlServerData(sqlQuery = sqlQueryAirline, connectionString = sqlConnFrom)
  
   dsSqlTo <- RxSqlServerData(table = "cleanData", connectionString = sqlConnTo)
  
   xFunc <- function(data) {
     data$CRSDepHour <- as.integer(trunc(data$CRSDepTime))
     return(data)}
  
   xVars <- c("CRSDepTime")
  
   sqlCompute <- RxInSqlServer(numTasks = 4, connectionString = sqlConnTo)
  
   if (rxSqlServerTableExists("cleanData", connectionString = sqlConnTo)) {rxSqlServerDropTable("cleanData")}
  
   rxDataStep(inData = dsSqlFrom, 
        outFile = dsSqlTo,
        transformFunc = xFunc,
        transformVars = xVars,
        overwrite = TRUE)
   return(NULL)
}
```
Although you do not need to open the ODBC connection explicitly as part of your code, an ODBC connection is still required to use **sqlrutils**. 


## See Also

[Generating a Stored Procedure using sqlrutils](../../advanced-analytics/r-services/generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md)


