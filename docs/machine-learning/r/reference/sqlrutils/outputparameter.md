---
title: "OutputParameter function (sqlrutils)"
description: "OutputParameter: generates an OutputParameter Object that captures the information about the output parameters of the function that is to be embedded into a SQL Server stored procedure."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), OutputParameter
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # OutputParameter: Output Parameter for SQL Stored Procedure: Class Generator 
 

`OutputParameter`: generates an OutputParameter Object that captures the
information about the output parameters of the function that is
to be embedded into a SQL Server Stored Procesure. Those will become
the output parameters of the stored procedure. Supported R types of the output
parameters are POSIXct, numeric, character, integer, logical, and raw.
This object must be created if the R function is returning a named
list for non-data frame memebers of the list


 ## Usage

```   
  OutputParameter(name, type)

```

 ## Arguments



 ### `name`
 A character string, the name of the output parameter object. 



 ### `type`
 R type of the output parameter object. 



 ## Value

OutputParameter Object

 ## Examples

 ```

  ## Not run:

  # See ?StoredProcedure for creating the "cleandata" table.

  # train 2 takes a data frame with clean data and outputs a model
  # as well as the data on the basis of which the model was built
  train2 <- function(in_df) {
  in_df[,"DayOfWeek"] <- factor(in_df[,"DayOfWeek"], levels=c("Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"))
    # The model formula
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek
    # Train the model
    rxSetComputeContext("local")
    mm <- rxLinMod(formula, data=in_df, transformFunc=NULL, transformVars=NULL)
    mm <- rxSerializeModel(mm)
    return(list(mm = mm, in_df = in_df))
  }
  # create InpuData Object for an input parameter that is a data frame
  # note: if the input parameter is not a data frame use InputParameter object
  id <- InputData(name = "in_df",
                  defaultQuery = paste0("select top 10000 ArrDelay,CRSDepTime,",
                                        "DayOfWeek,CRSDepHour from cleanData"))
  out1 <- OutputData("in_df")
  # create an OutputParameter object for the variable "mm" inside the return list
  out2 <- OutputParameter("mm", "raw")
  # connections string
  conStr <- paste0("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                   "Trusted_Connection=Yes;")
  # create the stored procedure object
  sp_df_op <- StoredProcedure(train2, "spTest2", id, out1, out2,
                          filePath = ".")
  registerStoredProcedure(sp_df_op, conStr)
  result <- executeStoredProcedure(sp_df_op, connectionString = conStr)
  # Get back the linear model.
  mm <- rxUnserializeModel(result$params$op1)
 ## End(Not run) 
```

