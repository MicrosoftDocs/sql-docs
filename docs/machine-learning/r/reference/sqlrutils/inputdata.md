---
title: "InputData function (sqlrutils)"
description: "InputData: generates an InputData Object that captures the information about the input parameter that is a data frame. The data frame needs to be populated upon the execution a given query."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), InputData
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # InputData: Input Data for SQL Stored Procedure: Class Generator 
 

`InputData`: generates an InputData Object that captures the
information about the input parameter that is a data frame.
The data frame needs to be populated upon the execution a given query.
This object is necessary  for creation of stored procedures in which
the embedded R function takes in a data frame input parameter.


 ## Usage

```r
  InputData(name, defaultQuery = NULL, query = NULL)

```

 ## Arguments



 ### `name`
 A character string, the name of the data input parameter into the R function supplied to StoredProcedure. 



 ### `defaultQuery`
 A character string specifying the default query that will retrieve the data if a different query is not provided at the time of the execution of the stored procedure. Must be a simple SELECT query. 



 ### `query`
 A character string specifing the query that will be used to retrieve the data in the next run of the stored procedure. 



 ## Value

InputData Object

 ## Examples

 ```r

  ## Not run:

  # See ?StoredProcedure for creating the "cleandata" table.

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

  # execute the stored procedure, note: non-data frame variables inside the
  # return list are not returned to R. However, if the execution is not sucessful
  # the error will be displayed
  model <- executeStoredProcedure(sp_df_op, connectionString = conStr)
  # get the linear model
  mm <- rxUnserializeModel(model$params$op1)
 ## End(Not run) 
```

