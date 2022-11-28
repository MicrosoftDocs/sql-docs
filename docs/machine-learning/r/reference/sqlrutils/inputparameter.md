---
title: "InputParameter function (sqlrutils)"
description: "InputParameter: generates an InputParameter Object, that captures the information about the input parameters of the R function that is to be embedded into a SQL Server stored procedure."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), InputParameter
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # InputParameter: Input Parameter for SQL Stored Procedure: Class Generator 
 

`InputParameter`: generates an InputParameter Object, that captures the
information about the input parameters of the R function that is
to be embedded into a SQL Server Stored Procesure. Those will become
the input parameters of the stored procedure. Supported R types of the input
parameters are POSIXct, numeric, character, integer, logical, and raw.


 ## Usage

```   
  InputParameter(name, type, defaultValue = NULL, defaultQuery = NULL,
  value = NULL, enableOutput = FALSE)

```

 ## Arguments



 ### `name`
 A character string, the name of the input parameter object. 



 ### `type`
 A character string representing the R type of the input parameter object. 



 ### `defaultValue`
 Default value of the parameter. Not supported for "raw". 



 ### `defaultQuery`
 A character string specifying the default query that will retrieve the data if a different query is not provided at the time of the execution of the stored procedure. 



 ### `value`
 A value that will be used for the parameter in the next run of the stored procedure. 



 ### `enableOutput`
 Make this an Input/Output Parameter 



 ## Value

InputParameter Object

 ## Examples

 ```

  ## Not run:

  # See ?StoredProcedure for creating the `cleandata` table.
  # and ?executeStoredProcedure for creating the `rdata` table. 

  # score1 makes a batch prediction given clean data(indata),
  # model object(model_param), and the new name of the variable
  # that is being predicted
  score1 <- function(indata, model_param, predVarName) {
  indata[,"DayOfWeek"] <- factor(indata[,"DayOfWeek"], levels=c("Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"))
  # The connection string
  conStr <- paste("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                  "Trusted_Connection=Yes;", sep = "")
  # The compute context
  computeContext <- RxInSqlServer(numTasks=4, connectionString=conStr)
  mm <- rxReadObject(as.raw(model_param))
  # Predict
  result <- rxPredict(modelObject = mm,
                      data = indata,
                      outData = NULL,
                      predVarNames = predVarName,
                      extraVarsToWrite = c("ArrDelay"),
                      writeModelVars = TRUE,
                      overwrite = TRUE)
}
# connections string
conStr <- paste0("Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;",
                 "Trusted_Connection=Yes;")
# create InpuData Object for an input parameter that is a data frame
id <- InputData(name = "indata", defaultQuery = "SELECT * from cleanData")
# InputParameter for the model_param input variable
model <- InputParameter("model_param", "raw",
                        defaultQuery =
                          "select top 1 value from rdata where [key] = 'linmod.v1'")
# InputParameter for the predVarName variable
name <- InputParameter("predVarName", "character", value = "ArrDelayEstimate")
sp_df_df <- StoredProcedure(score1, "sTest", id, model, name,
                        filePath = ".")
 ## End(Not run) 
```

