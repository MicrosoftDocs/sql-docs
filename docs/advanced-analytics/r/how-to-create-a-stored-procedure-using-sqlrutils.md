---
title: How to create a stored procedure using sqlrutils - SQL Server Machine Learning Services
description: Use the sqlrutils R package in SQL Server to bundle R language code into a single function that can be passed as an argument to a stored procedure.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Create a stored pProcedure using sqlrutils
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the steps for converting your R code to run as a T-SQL stored procedure. For best possible results, your code might need to be modified somewhat, to ensure that all inputs can be parameterized.

## <a name="bkmk_rewrite"></a>Step 1. Rewrite R Script

For the best results, you should rewrite your R code to encapsulate it as a single function.

All variables used by the function should be defined inside the function, or should be defined as input parameters. See the [sample code](#samples) in this article.

Also, because the input parameters for the R function will become the input parameters of the SQL stored procedure, you must ensure that your inputs and outputs conform to the following type requirements:

### Inputs

Among the input parameters there can be at most one data frame.

The objects inside the data frame, as well as all other input parameters of the function, must be of the following R data types:
- POSIXct
- numeric
- character
- integer
- logical
- raw

If an input type is not one of the above types, it needs to be serialized and passed into the function as *raw*. In this case, the function must also include code to deserialize the input.

### Outputs

The function can output one of the following:

- A data frame containing the supported data types. All objects in the data frame must use one of the supported data types.
- A named list, containing at most one data frame. All members of the list should use one of the supported data types.
- A NULL, if your function does not return any result

## Step 2. Generate Required Objects

After your R code has been cleaned up and can be called as a single function, you will use the functions in the **sqlrutils** package to prepare the inputs and outputs in a form that can be passed to the constructor that actually builds the stored procedure.

**sqlrutils** provides functions that define the input data schema and type, and define the output data schema and type. It also includes functions that can convert R objects to the required output type. You might make multiple function calls to create the required objects, depending on the data types your code uses.

### Inputs

If your function takes inputs, for each input, call the following functions:

- `setInputData` if the input is a data frame
- `setInputParameter` for all other input types

When you make each function call, an R object is created that you will later pass as an argument to `StoredProcedure`, to create the complete stored procedure.

### Outputs

**sqlrutils** provides multiple functions for converting R objects such as lists to the data.frame required by SQL Server.
If your function outputs a data frame directly, without first wrapping it into a list, you can skip this step.
You can also skip the conversion this step if your function returns NULL.

When converting a list or getting a particular item from a list, choose from these functions:

- `setOutputData` if the variable to get from the list is a data frame
- `setOutputParameter` for all other members of the list

When you make each function call, an R object is created that you will later pass as an argument to `StoredProcedure`, to create the complete stored procedure.

## Step 3. Generate the Stored Procedure

When all input and output parameters are ready, make a call to the `StoredProcedure` constructor.

**Usage**

`StoredProcedure (func, spName, ..., filePath = NULL ,dbName = NULL, connectionString = NULL, batchSeparator = "GO")`

To illustrate, assume that you want to create a stored procedure named **sp_rsample** with these parameters:

- Uses an existing function **foosql**. The function was based on existing code in R function **foo**, but you  rewrote the function to conform to the requirements as described in [this section](#bkmk_rewrite), and named the updated function as **foosql**.
- Uses the data frame **queryinput** as input
- Generates as output a data frame with the R variable name, **sqloutput**
- You want to create the T-SQL code as a file in the `C:\Temp` folder, so that you can run it using SQL Server Management Studio later

```R
StoredProcedure (foosql, sp_rsample, queryinput, sqloutput, filePath = "C:\\Temp")
```

> [!NOTE]
> Because you are writing the file to the file system, you can omit the arguments that define the database connection.

The output of the function is a T-SQL stored procedure that can be executed on an instance of SQL Server 2016 (requires R Services) or SQL Server 2017 (requires Machine Learning Services with R). 

For additional examples, see the package help, by calling `help(StoredProcedure)` from an R environment.

## Step 4. Register and Run the Stored Procedure

There are two ways that you can run the stored procedure:

- Using T-SQL, from any client that supports connections to the SQL Server 2016 or SQL Server 2017 instance
- From an R environment

Both methods require that the stored procedure be registered in the database where you intend to use the stored procedure.

### Register the stored procedure

You can register the stored procedure using R, or you can run the CREATE PROCEDURE statement in T-SQL.

- Using T-SQL.  If you are more comfortable with T-SQL, open SQl Server Management Studio (or any other client that can run SQL DDL commands) and execute the CREATE PROCEDURE statement using the code prepared by the `StoredProcedure` function.
- Using R. While you are still in your R environment, you can use the `registerStoredProcedure` function in **sqlrutils** to register the stored procedure with the database.

  For example, you could register the stored procedure **sp_rsample** in the instance and database defined in *sqlConnStr*, by making this R call:

  ```R
  registerStoredProcedure(sp_rsample, sqlConnStr)
  ```


> [!IMPORTANT]
> Regardless of whether you use R or SQL, you must run the statement using an account that has permissions to create new database objects.

### Run using SQL

After the stored procedure has been created, open a connection to the SQL database using any client that supports T-SQL, and pass values for any parameters required by the stored procedure.

### Run using R

Some additional preparation is needed If you want to execute the stored procedure from R code, rather from SQL Server. For example, if the stored procedure requires input values, you must set those input parameters before the function can be executed, and then pass those objects to the stored procedure in your R code.

The overall process of calling the prepared SQL stored procedure is as follows:

1. Call `getInputParameters` to get a list of input parameter objects.
2. Define a `$query` or set a `$value` for each input parameter.
3. Use `executeStoredProcedure` to execute the stored procedure from the R development environment, passing the list of input parameter objects that you set.

## <a name = "samples"></a>Example

This example shows the before and after versions of an R script that gets data from a SQL Server database, performs some transformations on the data, and saves it to a different database.

This simple example is used only to demonstrate how you might rearrange your R code to make it easier to convert to a stored procedure.

### Before code preparation


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

> [!NOTE]
> 
> When you use an ODBC connection rather than invoking the *RxSqlServerData* function, you must open the connection using *rxOpen* before you can perform operations on the database.


### After code preparation

In the updated version, the first line defines the function name. All other code from the original R solution becomes a part of that function.

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

> [!NOTE]
> 
> Although you do not need to open the ODBC connection explicitly as part of your code, an ODBC connection is still required to use **sqlrutils**.

## See Also

[sqlrutils (SQL)](ref-r-sqlrutils.md)


