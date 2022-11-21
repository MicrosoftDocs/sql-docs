---
title: "executeStoredProcedure function (sqlrutils)"
description: "executeStoredProcedure: Executes a stored procedure registered with the database"
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (sqlrutils), executeStoredProcedure
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # executeStoredProcedure: Execute a SQL Stored Procedure 
 

`executeStoredProcedure`: Executes a stored procedure registered with the database


 ## Usage

```   
  executeStoredProcedure(sqlSP, ..., connectionString = NULL)

```

 ## Arguments



 ### `sqlSP`
 a valid StoredProcedure Object 



 ### ` ...`
 Optional input and output parameters for the stored procedure. All of the parameters that do not have default queries or values assigned to them must be provided 



 ### `connectionString`
 A character string (must be provided if the StoredProcedure object was created without a connection string). This function requires using an ODBC driver which supports ODBC 3.8 functionality. 



 ### `verbose`
 Boolean. Whether to print out the command used to execute the stored procedure 



 ## Value

TRUE on success, FALSE on failure

 ## Notes

This function relies that the ODBC driver used supports ODBC 3.8 features.
Otherwise it will fail.


 ## Examples

 ```

  ## Not run:

  # See ?StoredProcedure for creating the "cleandata" table.

  ############# Example 1 #############
  # Create a linear model and store in the "rdata" table.
  train <- function(in_df) {
    factorLevels <- c("Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday")
    in_df[,"DayOfWeek"] <- factor(in_df[,"DayOfWeek"], levels=factorLevels)
    # The model formula
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek

    # Train the model
    mm <- rxLinMod(formula, data = in_df, transformFunc = NULL, transformVars = NULL)

    # Store the model into the database
    # rdata needs to be created beforehand
    conStr <- paste0("Driver={ODBC Driver 13 for SQL Server};Server=.;",
                     "Database=RevoTestDB;Trusted_Connection=Yes;")
    out.table = "rdata"
    # write the model to the table
    ds = RxOdbcData(table = out.table, connectionString = conStr)

    rxWriteObject(ds, "linmod.v1", mm, keyName = "key",
                  valueName = "value")
    return(NULL)
  }

  conStr <- "Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;Trusted_Connection=Yes;"
  # create  an InputData object for the input data frame in_df
  indata <- InputData("in_df",
                      defaultQuery = paste0("select top 10000 ArrDelay,CRSDepTime,",
                                            "DayOfWeek,CRSDepHour from cleanData"))
  # create the sql server stored procedure object
  trainSP1 <- StoredProcedure('train', "spTrain_df_to_df", indata,
                              dbName = "RevoTestDB",
                              connectionString = conStr,
                              filePath = ".")
  # spRegisterSp and executeStoredProcedure do not require a connection string since we
  # provided one when we created trainSP1
  registerStoredProcedure(trainSP1)
  executeStoredProcedure(trainSP1, verbose = TRUE)


  ############# Example 2 #############
  # score1 makes a batch prediction given clean data(indata),
  # model object(model_param), and the new name of the variable
  # that is being predicted
score1 <- function(in_df, model_param, predVarNameInParam) {
    factorLevels <- c("Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday")
    in_df[,"DayOfWeek"] <- factor(in_df[,"DayOfWeek"], levels=factorLevels)
    mm <- rxReadObject(as.raw(model_param))
    # Predict
    result <- rxPredict(modelObject = mm,
                        data = in_df,
                        outData = NULL,
                        predVarNames = predVarNameInParam,
                        extraVarsToWrite = c("ArrDelay"),
                        writeModelVars = TRUE,
                        overwrite = TRUE)
    return(list(result = result, pvOutParam = mm$f.pvalue))
}

# create  an InputData object for the input data frame in_df
indata <- InputData(name = "in_df", defaultQuery = "SELECT top 10 * from cleanData")
# create InputParameter objects for model_param and predVarNameInParam
model <- InputParameter("model_param", "raw",
                        defaultQuery = paste("select top 1 value from rdata",
                                             "where [key] = 'linmod.v1'"))
predVarNameInParam <- InputParameter("predVarNameInParam", "character")
# create OutputData object for the data frame inside the return list
outData <- OutputData("result")
# create OutputParameter object for non data frame variable inside the return list
pvOutParam <- OutputParameter("pvOutParam", "numeric")
scoreSP1 <- StoredProcedure(score1, "spScore_df_param_df", indata, model, predVarNameInParam, outData, pvOutParam,
                            filePath = ".")
conStr <- "Driver={ODBC Driver 13 for SQL Server};Server=.;Database=RevoTestDB;Trusted_Connection=Yes;"
# connection string necessary for registrations and execution
# since we did not pass it to StoredProcedure
registerStoredProcedure(scoreSP1, conStr)
model <- executeStoredProcedure(scoreSP1, predVarNameInParam = "ArrDelayEstimate", connectionString = conStr, verbose = TRUE)
model$data
model$params[[1]]
 ## End(Not run) 
```

