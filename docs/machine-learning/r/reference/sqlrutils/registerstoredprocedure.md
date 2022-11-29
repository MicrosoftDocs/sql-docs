---
title: "registerStoredProcedure function (sqlrutils)"
description: "registerStoredProcedure: Uses the StoredProcedure object to register the stored procedure with the specified database"
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), registerStoredProcedure
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # registerStoredProcedure: Register a SQL Stored Procedure with a Database 
 

`registerStoredProcedure`: Uses the StoredProcedure object to register
the stored procedure with the specified database


 ## Usage

```   
  registerStoredProcedure(sqlSP, connectionString = NULL)

```

 ## Arguments



 ### `sqlSP`
 a valid StoredProcedure object 



 ### `connectionString`
 A character string (must be provided if the StoredProcedure object was created without a connection string) 



 ## Value

TRUE on success, FALSE on failure

 ## Examples

 ```

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
 model <- executeStoredProcedure(sp_df_op, connectionString = conStr)

 # Getting back the model by unserializing it.
 mm <- rxUnserializeModel(model$params$op1)
 ## End(Not run) 
```

