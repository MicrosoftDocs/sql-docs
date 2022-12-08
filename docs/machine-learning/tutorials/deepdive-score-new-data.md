---
title: Score data using RevoScaleR
description: "Use the logistic regression model that you created in the previous tutorial to score another data set that uses the same independent variables as inputs."
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 11/27/2018  
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Score new data (SQL Server and RevoScaleR tutorial)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This is tutorial 8 of the [RevoScaleR tutorial series](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this tutorial, you'll use the logistic regression model that you created in the previous tutorial to score another data set that uses the same independent variables as inputs.

> [!div class="checklist"]
> * Score new data
> * Create a histogram of the scores

> [!NOTE]
> You need DDL admin privileges for some of these steps.

## Generate and save scores
  
1. Update the sqlScoreDS data source (created in [tutorial two](deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)) to use column information created in the previous tutorial.
  
    ```R
    sqlScoreDS <- RxSqlServerData(
        connectionString = sqlConnString,
        table = sqlScoreTable,
        colInfo = ccColInfo,
        rowsPerRead = sqlRowsPerRead)
    ```
  
2. To make sure you don't lose the results, create a new data source object. Then, use the new data source object to populate a new table in the RevoDeepDive database.
  
    ```R
    sqlServerOutDS <- RxSqlServerData(table = "ccScoreOutput",
        connectionString = sqlConnString,
        rowsPerRead = sqlRowsPerRead )
    ```
    At this point, the table has not been created. This statement just defines a container for the data.
     
3. Check the current compute context using **rxGetComputeContext()**, and set the compute context to the server if needed.
  
    ```R
    rxSetComputeContext(sqlCompute)
    ```
  
4. As a precaution, check for the existence of the output table. If one already exists with the same name, you will get an error when attempting to write the new table.
  
    To do this, make a call to the functions [rxSqlServerTableExists](/machine-learning-server/r-reference/revoscaler/rxsqlserverdroptable) and [rxSqlServerDropTable](/machine-learning-server/r-reference/revoscaler/rxsqlserverdroptable), passing the table name as input.
  
    ```R
    if (rxSqlServerTableExists("ccScoreOutput"))     rxSqlServerDropTable("ccScoreOutput")
    ```
  
    + **rxSqlServerTableExists** queries the ODBC driver and returns TRUE if the table exists, FALSE otherwise.
    + **rxSqlServerDropTable** executes the DDL and returns TRUE if the table is successfully dropped, FALSE otherwise.

5. Execute [rxPredict](/machine-learning-server/r-reference/revoscaler/rxpredict) to create the scores, and save them in the new table defined in data source sqlScoreDS.
  
    ```R
    rxPredict(modelObject = logitObj,
        data = sqlScoreDS,
        outData = sqlServerOutDS,
        predVarNames = "ccFraudLogitScore",
          type = "link",
        writeModelVars = TRUE,
        overwrite = TRUE)
    ```
  
    The **rxPredict** function is another function that supports running in remote compute contexts. You can use the **rxPredict** function to create scores from models based on [rxLinMod](/machine-learning-server/r-reference/revoscaler/rxlinmod), [rxLogit](/machine-learning-server/r-reference/revoscaler/rxlogit), or [rxGlm](/machine-learning-server/r-reference/revoscaler/rxglm).
  
    - The parameter *writeModelVars* is set to **TRUE** here. This means that the variables that were used for estimation will be included in the new table.
  
    - The parameter *predVarNames* specifies the variable where results will be stored. Here you are passing a new variable, `ccFraudLogitScore`.
  
    - The *type* parameter for **rxPredict** defines how you want the predictions calculated. Specify the keyword **response** to generate scores based on the scale of the response variable. Or, use the keyword **link** to generate scores based on the underlying link function, in which case predictions are created using a logistic scale.

6. After a while, you can refresh the list of tables in Management Studio to see the new table and its data.

7. To add additional variables to the output predictions, use the *extraVarsToWrite* argument.  For example, in the following code, the variable *custID* is added from the scoring data table into the output table of predictions.
  
    ```R
    rxPredict(modelObject = logitObj,
            data = sqlScoreDS,
            outData = sqlServerOutDS,
            predVarNames = "ccFraudLogitScore",
              type = "link",
            writeModelVars = TRUE,
            extraVarsToWrite = "custID",
            overwrite = TRUE)
    ```

## Display scores in a histogram

After the new table has been created, compute and display a histogram of the 10,000 predicted scores. Computation is faster if you specify the low and high values, so get those from the database and add them to your working data.

1. Create a new data source, sqlMinMax, that queries the database to get the low and high values.
  
    ```R
    sqlMinMax <- RxSqlServerData(
        sqlQuery = paste("SELECT MIN(ccFraudLogitScore) AS minVal,",
        "MAX(ccFraudLogitScore) AS maxVal FROM ccScoreOutput"),
        connectionString = sqlConnString)
    ```

     From this example, you can see how easy it is to use **RxSqlServerData** data source objects to define arbitrary datasets based on SQL queries, functions, or stored procedures, and then use those in your R code. The variable does not store the actual values, just the data source definition; the query is executed to generate the values only when you use it in a function like **rxImport**.
      
2. Call the [rxImport](/machine-learning-server/r-reference/revoscaler/rximport) function to put the values in a data frame that can be shared across compute contexts.
  
    ```R
    minMaxVals <- rxImport(sqlMinMax)
    minMaxVals <- as.vector(unlist(minMaxVals))
    ```

    **Results**
     
    ```R
    > minMaxVals
     
    [1] -23.970256   9.786345
    ```

3. Now that the maximum and minimum values are available, use the values to create another data source for the generated scores.
  
    ```R
    sqlOutScoreDS <- RxSqlServerData(sqlQuery = "SELECT ccFraudLogitScore FROM ccScoreOutput",
        connectionString = sqlConnString,
        rowsPerRead = sqlRowsPerRead,
            colInfo = list(ccFraudLogitScore = list(
                low = floor(minMaxVals[1]),
                        high = ceiling(minMaxVals[2]) ) ) )
    ```

4. Use the data source object sqlOutScoreDS to get the scores, and compute and display a histogram. Add the code to set the compute context if needed.
  
    ```R
    # rxSetComputeContext(sqlCompute)
    rxHistogram(~ccFraudLogitScore, data = sqlOutScoreDS)
    ```
  
    **Results**
  
    ![complex histogram created by R](media/rsql-sue-complex-histogram.png "complex histogram created by R")
  
## Next steps

> [!div class="nextstepaction"]
> [Transform data using R](../../machine-learning/tutorials/deepdive-transform-data-using-r.md)