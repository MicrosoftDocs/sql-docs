---
title: "Build and Save an R Model using SQL | Microsoft Docs"
ms.custom: ""
ms.date: "07/06/2016"
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
ms.assetid: 69b374c1-2042-4861-8f8b-204a6297c0db
caps.latest.revision: 20
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Build an R model and save it to SQL (walkthrough)

In this part of the data science walkthrough, you'll learn how to build a machine learning model using R, and save the model in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

You'll use these columns as the features in the model:

+ passenger_count
+ trip_distance
+ trip_time_in_secs
+ direct_distance

## Create a classification model using rxLogit

The model you'll build is a binary classifier that predicts whether the taxi driver is likely to get a tip on a particular ride or not. You'll use the data source you created in the previous lesson, `featureDataSource,` to train the tip classifier, using logistic regression.

1. Call the [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit) function, included in the **RevoScaleR** package, to create a logistic regression model.

    ```R
    system.time(logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = new_ds))
    ```

    The call that builds the model is enclosed in the system.time function. This lets you get the time required to build the model.

2. After you build the model, you'll want to inspect it using the `summary` function, and view the coefficients.

    ```R
    summary(logitObj)
    ```

     *Results*

    ```
    rxLogit(formula = tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = new_ds)
    
    Logistic Regression Results for: tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance
    Data: new_ds (RxSqlServerData Data Source)
    Dependent variable(s): tipped
    Total independent variables: 5
    Number of valid observations: 1703957
    Number of missing observations: 0
    -2*LogLikelihood: 2356636.8033 (Residual deviance on 1703952 degrees of freedom)
     
    Coefficients:
                        Estimate Std. Error z value Pr(>|z|)
    (Intercept)        1.253e-01  2.787e-03  44.954 2.22e-16 ***
    passenger_count   -3.130e-02  1.111e-03 -28.177 2.22e-16 ***
    trip_distance     -1.193e-07  2.460e-07  -0.485    0.628
    trip_time_in_secs  7.014e-08  8.861e-08   0.792    0.429
    direct_distance    1.129e-02  6.450e-04  17.507 2.22e-16 ***
    ---
    Signif. codes:  0 ‘***’ 0.001 ‘**’ 0.01 ‘*’ 0.05 ‘.’ 0.1 ‘ ’ 1
    
    Condition number of final variance-covariance matrix: 1.0592
    Number of iterations: 7
    ```

## Use the logistic regression model for scoring

Now that the model is built, you can use to predict whether the driver is likely to get a tip on a particular drive or not.

1. First, use the [RxSqlServerData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdata) function to define a data source object for storing the scoring results:

    ```R
    scoredOutput <- RxSqlServerData(
      connectionString = connStr,
      table = "taxiScoreOutput"  )
    ```
    + To make this example simpler, the input to the logistic regression model is the same feature data source (`new_ds`) that you used to train the model.  More typically, you might have some new data to score with, or you might have set aside some data for testing vs. training.
  
    + The prediction results will be saved in the table, _taxiscoreOutput_. Notice that the schema for this table is not defined when you create it using rxSqlServerData. The schema is obtained from the rxPredict output.
  
    + To create the table that stores the predicted values, the SQL login running the rxSqlServer data function must have DDL privileges in the database. If the login cannot create tables, the statement will fail.

2. Call the [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict) function to generate results.

    ```R
    rxPredict(modelObject = logitObj,
        data = new_ds,
        outData = scoredOutput,
        predVarNames = "Score",
        type = "response",
        writeModelVars = TRUE, overwrite = TRUE)
    ```
    
    If the statement succeeds, it should take some time to run. When complete, you can open SQL Server Management Studio and verify that the table was created and that it contains the Score column and other expected output.

## Plot model accuracy

To get an idea of the accuracy of the model, you can use the [rxRoc](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxroc) function to plot the Receiver Operating Curve. Because rxRoc is one of the new functions provided by the RevoScaleR package that supports remote compute contexts, you have two options:

+ You can use the rxRoc function to execute the plot in the remote compute context and then return the plot to your local client.

+ You can also import the data to your R client computer, and use other R plotting functions to create the performance graph.

In this section, you'll experiment with both techniques.

### Execute a plot in the remote (SQL Server) compute context

1. Call the function rxRoc and provide the data defined earlier as input.

    ```R
    scoredOutput = rxImport(scoredOutput)
    rxRoc(actualVarName= "tipped", predVarNames = "Score", scoredOutput)
    ```

    This call returns the values used in computing the ROC chart. The label column is _tipped_, which has the actual results you are trying to predict, while the _Score_ column has the prediction.

2. To actually plot the chart, you can save the ROC object and then draw it with the `plot` function. The graph is created on the remote compute context, and then returned to your R environment.

    ```R
    scoredOutput = rxImport(scoredOutput)
    rocObjectOut <- rxRoc(actualVarName= "tipped", predVarNames = "Score", scoredOutput)
    plot(rocObjectOut)
    ```

    View the graph by opening the R graphics device, or by clicking the **Plot** window in RStudio. 

    ![ROC plot for the model](media/rsql-e2e-rocplot.png "ROC plot for the model")

### Create the plots in the local compute context using data from SQL Server

1. For the local compute context, the process is much the same.

    Use the [rxImport](https://docs.microsoft.com/r-server/r-reference/revoscaler/rximport) function to bring the specified data to your local R environment.

    ```R
    scoredOutput = rxImport(scoredOutput)
    ```

2. Using the data into local memory, you can call the **ROCR** library to create some new predictions.

    ```R
    library('ROCR')
    pred <- prediction(scoredOutput$Score, scoredOutput$tipped)

3. Generate a local plot, based on the predictions.

    ```R
    acc.perf = performance(pred, measure = 'acc')
    plot(acc.perf)
    ind = which.max( slot(acc.perf, 'y.values')[[1]] )
    acc = slot(acc.perf, 'y.values')[[1]][ind]
    cutoff = slot(acc.perf, 'x.values')[[1]][ind]
    ```

    ![plotting model performance using R](media/rsql-e2e-performanceplot.png "plotting model performance using R")

> [!NOTE]
> Your charts might look different from these, depending on how many data points you used.

## Deploy the model

After you have built a model and ascertained that it is performing well, you probably want to deploy it to a site where users or people in your organization can make use of the model, or perhaps retrain and recalibrate the model on a regular basis. This process is sometimes called  *operationalizing* a model.

Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] lets you invoke an R model using a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, it is extremely easy to use R in a client application.

However, before you can call the model from an external application, you must save the model to the database used for production. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], trained models are stored in binary form, in a single column of type **varbinary(max)**.

Therefore, moving a trained model from R to SQL Server includes these steps:

+ Serializing the model into a hexadecimal string

+ Transmitting the serialized object to the database

+ Saving the model in a **varbinary(max)** column

In this section, you will learn how to persist the model, and how to call it to make predictions.

1. Switch back to your local R environment if you are not already using it, serialize the model, and save it in a variable.

    ```R
    rxSetComputeContext("local")
    modelbin <- serialize(logitObj, NULL)
    modelbinstr=paste(modelbin, collapse="")
    ```

2. Open an ODBC connection.

    ```R
    library(RODBC)
    conn <- odbcDriverConnect(connStr)
    ```

3. Call the stored procedure created by the PowerShell script, to store the binary representation of the model in a column in the database.

    ```R
    q <- paste("EXEC PersistModel @m='", modelbinstr,"'", sep="")
    sqlQuery (conn, q)
    ```

    Saving a model to a table requires only an INSERT statement. However, it's easier when wrapped in a stored procedure, such as _PersistModel_.

    > [!NOTE]
    > If you get an error such as "The EXECUTE permission was denied on the object PersistModel", make sure that your login has permission. You can grant explicit permissions on just the stored proceure by running a T-SQL statement like this: `GRANT EXECUTE ON [dbo].[PersistModel] TO [RTestUser]`

3. After you have created a model and saved it in a database, you can call it directly from [!INCLUDE[tsql](../../includes/tsql-md.md)] code, using the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

    However, if you have a model you use often, it's easier to wrap the input query and the call to the model, as well as any other parameters, in a custom stored procedure.

    Here is the complete code of the stored procedure. We recommend creating helper functions such as this stored procedure to make it easier to manage and update your R models in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

    ```tsql
    CREATE PROCEDURE [dbo].[PersistModel]  @m nvarchar(max)
    AS
    BEGIN
      SET NOCOUNT ON;
      INSERT INTO nyc_taxi_models (model) values (convert(varbinary(max),@m,2))
    END
    ```

  > [!NOTE]
  > Use the **SET NOCOUNT ON** clause to prevent extra result sets from interfering with SELECT statements.


In the next and final lesson, you'll learn how to perform scoring against the saved model using [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Next lesson

[Deploy the R model and use in SQL](/walkthrough-deploy-and-use-the-model.md)

## Previous lesson

[Create data features using R and SQL](/walkthrough-create-data-features.md)
