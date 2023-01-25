---
title: "StoredProcedure function (sqlrutils)"
description: "StoredProcedure: generates a SQLServer Stored Procedure Object and optionally a .sql file containing a query to create a stored  procedure."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), StoredProcedure
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # StoredProcedure: SQL Server Stored Procedure: Class Generator 
 

`StoredProcedure`: generates a SQLServer Stored Procedure Object
and optionally a .sql file containing a query to create a stored
procedure. StoredProcedure$registrationVec contains strings
representing the queries needed for creation of the stored procedure


 ## Usage

```   
  StoredProcedure (func, spName, ..., filePath = NULL ,dbName = NULL,
  connectionString = NULL, batchSeparator = "GO")

```

 ## Arguments



 ### `func`
 A valid R function or a string name of a valid R function: 1) All of the variables that the function relies on should be defined either inside the function or come in as input parameters. Among the input parameters there can be at most 1 data frame 2) The function should return either a data frame, a named list, or NULL. There can be at most one data frame inside the list. 



 ### `spName`
 A character string specifying name for the stored procedure. 



 ### ` ...`
 Optional input and output parameters for the stored procedure; must be objects of classes InputData, InputParameter, or outputParameter. 



 ### `filePath`
 A character string specifying a path to the directory in which to create the .sql. If NULL the .sql file is not generated. 



 ### `dbName`
 A character string specifying name of the database to use. 



 ### `connectionString`
 A character string specifying the connection string. 



 ### `batchSeparator`
 Desired SQL batch separator (only relevant if filePath is defined) 



 ## Value

SQLServer Stored Procedure Object

 ## Examples

 ```

  ## Not run:

  ############# Example 1 #############
  # etl1 - reads from and write directly to the database
  etl1 <- function() {
    # The query to get the data
    qq <- "select top 10000 ArrDelay,CRSDepTime,DayOfWeek from AirlineDemoSmall"
    # The connection string
    conStr <- paste("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                  "Trusted_Connection=Yes;", sep = "")
    # The data source - retrieves the data from the database
    dsSqls <- RxSqlServerData(sqlQuery=qq, connectionString=conStr)
    # The destination data source
    dsSqls2 <- RxSqlServerData(table ="cleanData",  connectionString = conStr)
    # A transformation function
    transformFunc <- function(data) {
      data$CRSDepHour <- as.integer(trunc(data$CRSDepTime))
      return(data)
    }
    # The transformation variables
    transformVars <- c("CRSDepTime")
    rxDataStep(inData = dsSqls,
               outFile = dsSqls2,
               transformFunc=transformFunc,
               transformVars=transformVars,
               overwrite = TRUE)
    return(NULL)
  }
  # Create a StoredProcedure object
  sp_ds_ds <- StoredProcedure(etl1, "spTest",
                         filePath = ".", dbName ="RevoTestDB")
  # Define a connection string
  conStr <- paste("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                  "Trusted_Connection=Yes;", sep = "")
  # register the stored procedure with a database
  registerStoredProcedure(sp_ds_ds, conStr)
  # execute the stored procedure
  executeStoredProcedure(sp_ds_ds, connectionString = conStr)


  ############# Example 2 #############
  # train 1 takes a data frame with clean data and outputs a model
  train1 <- function(in_df) {
    in_df[,"DayOfWeek"] <- factor(in_df[,"DayOfWeek"], levels=c("Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"))
    # The model formula
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek
    # Train the model
    rxSetComputeContext("local")
    mm <- rxLinMod(formula, data=in_df)
    mm <- rxSerializeModel(mm)
    return(list("mm" = mm))
  }
  # create InpuData Object for an input parameter that is a data frame
  # note: if the input parameter is not a data frame use InputParameter object
  id <- InputData(name = "in_df",
                 defaultQuery = paste0("select top 10000 ArrDelay,CRSDepTime,",
                                       "DayOfWeek,CRSDepHour from cleanData"))
  # create an OutputParameter object for the variable inside the return list
  # note: if that variable is a data frame use OutputData object
  out <- OutputParameter("mm", "raw")

  # connections string
  conStr <- paste0("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                   "Trusted_Connection=Yes;")
  # create the stored procedure object
  sp_df_op <- StoredProcedure("train1", "spTest1", id, out,
                         filePath = ".")
  # register the stored procedure with the database
  registerStoredProcedure(sp_df_op, conStr)

  # get the linear model
  model <- executeStoredProcedure(sp_df_op, connectionString = conStr)
  mm <- rxUnserializeModel(model$params$op1)
 ## End(Not run) 
```

