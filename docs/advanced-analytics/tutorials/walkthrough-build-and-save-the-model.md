---
title: Build an R model and save to SQL Server (walkthrough) - SQL Server Machine Learning
description: Tutorial showing how to build an R language model used for SQL Server in-database analytics.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/26/2018  
ms.topic: tutorial
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Build an R model and save to SQL Server (walkthrough)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this step, learn how to build a machine learning model and save the model in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By saving a model, you can call it directly from [!INCLUDE[tsql](../../includes/tsql-md.md)] code, using the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) or the [PREDICT (T-SQL) function](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql).

## Prerequisites

This step assumes an ongoing R session based on previous steps in this walkthrough. It uses the connection strings and data source objects created in those steps. The following tools and packages are used to run the script.

+ Rgui.exe to run R commands
+ Management Studio to run T-SQL
+ ROCR package
+ RODBC package

### Create a stored procedure to save models

This step uses a stored procedure to save a trained model to SQL Server. Creating a stored procedure to perform this operation makes the task easier.

Run the following T-SQL code in a query windows in Management Studio to create the stored procedure.

```sql
USE [NYCTaxi_Sample]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PersistModel')
  DROP PROCEDURE PersistModel
GO

CREATE PROCEDURE [dbo].[PersistModel] @m nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	insert into nyc_taxi_models (model) values (convert(varbinary(max),@m,2))
END
GO
```

> [!NOTE]
> If you get an error, make sure that your login has permission to create objects. You can grant explicit permissions to create objects by running a T-SQL statement like this: `exec sp_addrolemember 'db_owner', '<user_name>'`.

## Create a classification model using rxLogit

The model is a binary classifier that predicts whether the taxi driver is likely to get a tip on a particular ride or not. You'll use the data source you created in the previous lesson to train the tip classifier, using logistic regression.

1. Call the [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit) function, included in the **RevoScaleR** package, to create a logistic regression model. 

    ```R
    system.time(logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = featureDataSource));
    ```

    The call that builds the model is enclosed in the system.time function. This lets you get the time required to build the model.

2. After you build the model, you can inspect it using the `summary` function, and view the coefficients.

    ```R
    summary(logitObj);
    ```

    **Results**
    
    ```R
     *Logistic Regression Results for: tipped ~ passenger_count + trip_distance + trip_time_in_secs +*
     direct_distance* 
     *Data: featureDataSource (RxSqlServerData Data Source)*
     *Dependent variable(s): tipped*
     *Total independent variables: 5*
     *Number of valid observations: 17068*
     *Number of missing observations: 0*
     *-2\*LogLikelihood: 23540.0602 (Residual deviance on 17063 degrees of freedom)*
     *Coefficients:*
     *Estimate Std. Error z value Pr(>|z|)*
     *(Intercept)       -2.509e-03  3.223e-02  -0.078  0.93793*
     *passenger_count   -5.753e-02  1.088e-02  -5.289 1.23e-07 \*\*\**
     *trip_distance     -3.896e-02  1.466e-02  -2.658  0.00786 \*\**
     *trip_time_in_secs  2.115e-04  4.336e-05   4.878 1.07e-06 \*\*\**
     *direct_distance    6.156e-02  2.076e-02   2.966  0.00302 \*\**
     *---*
     *Signif. codes:  0 ‘\*\*\*’ 0.001 ‘\*\*’ 0.01 ‘\*’ 0.05 ‘.’ 0.1 ‘ ’ 1*
     *Condition number of final variance-covariance matrix: 48.3933*
     *Number of iterations: 4*
   ```

## Use the logistic regression model for scoring

Now that the model is built, you can use to predict whether the driver is likely to get a tip on a particular drive or not.

1. First, use the [RxSqlServerData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdata) function to define a data source object for storing the scoring result.

    ```R
    scoredOutput <- RxSqlServerData(
      connectionString = connStr,
      table = "taxiScoreOutput"  )
    ```

    + To make this example simpler, the input to the logistic regression model is the same feature data source (`sql_feature_ds`) that you used to train the model.  More typically, you might have some new data to score with, or you might have set aside some data for testing vs. training.
  
    + The prediction results will be saved in the table, _taxiscoreOutput_. Notice that the schema for this table is not defined when you create it using rxSqlServerData. The schema is obtained from the rxPredict output.
  
    + To create the table that stores the predicted values, the SQL login running the rxSqlServer data function must have DDL privileges in the database. If the login cannot create tables, the statement fails.

2. Call the [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict) function to generate results.

    ```R
    rxPredict(modelObject = logitObj,
        data = featureDataSource,
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
    scoredOutput = rxImport(scoredOutput);
    rxRoc(actualVarName= "tipped", predVarNames = "Score", scoredOutput);
    ```

    This call returns the values used in computing the ROC chart. The label column is _tipped_, which has the actual results you are trying to predict, while the _Score_ column has the prediction.

2. To actually plot the chart, you can save the ROC object and then draw it with the plot function. The graph is created on the remote compute context, and returned to your R environment.

    ```R
    scoredOutput = rxImport(scoredOutput);
    rocObjectOut <- rxRoc(actualVarName= "tipped", predVarNames = "Score", scoredOutput);
    plot(rocObjectOut);
    ```

    View the graph by opening the R graphics device, or by clicking the **Plot** window in RStudio.

    ![ROC plot for the model](media/rsql-e2e-rocplot.png "ROC plot for the model")

### Create the plots in the local compute context using data from SQL Server

You can verify the compute context is local by running `rxGetComputeContext()` at the command prompt. The return value should be "RxLocalSeq Compute Context".

1. For the local compute context, the process is much the same. You use the [rxImport](https://docs.microsoft.com/r-server/r-reference/revoscaler/rximport) function to bring the specified data into your local R environment.

    ```R
    scoredOutput = rxImport(scoredOutput)
    ```

2. Using the data in local memory, you load the **ROCR** package, and use the prediction function from that package to create some new predictions.

    ```R
    library('ROCR');
    pred <- prediction(scoredOutput$Score, scoredOutput$tipped);
    ```

3. Generate a local plot, based on the values stored in the output variable `pred`.

    ```R
    acc.perf = performance(pred, measure = 'acc');
    plot(acc.perf);
    ind = which.max( slot(acc.perf, 'y.values')[[1]] );
    acc = slot(acc.perf, 'y.values')[[1]][ind];
    cutoff = slot(acc.perf, 'x.values')[[1]][ind];
    ```

    ![plotting model performance using R](media/rsql-e2e-performanceplot.png "plotting model performance using R")

> [!NOTE]
> Your charts might look different from these, depending on how many data points you used.

## Deploy the model

After you have built a model and ascertained that it is performing well, you probably want to deploy it to a site where users or people in your organization can make use of the model, or perhaps retrain and recalibrate the model on a regular basis. This process is sometimes called  *operationalizing* a model. In SQL Server, operationalization is achieved by embedding R code in a stored procedure. Because code resides in the procedure, it can be called from any application that can connect to SQL Server.

Before you can call the model from an external application, you must save the model to the database used for production. Trained models are stored in binary form, in a single column of type **varbinary(max)**.

A typical deployment workflow consists of the following steps:

1. Serialize the model into a hexadecimal string
2. Transmit the serialized object to the database
3. Save the model in a varbinary(max) column

In this section, learn how to use a stored procedure to persist the model and make it available for predictions. The stored procedure used in this section is PersistModel. The definition of PersistModel is in [Prerequisites](#prerequisites).

1. Switch back to your local R environment if you are not already using it, serialize the model, and save it in a variable.

    ```R
    rxSetComputeContext("local");
    modelbin <- serialize(logitObj, NULL);
    modelbinstr=paste(modelbin, collapse="");
    ```

2. Open an ODBC connection using **RODBC**. You can omit the call to RODBC if you already have the package loaded.

    ```R
    library(RODBC);
    conn <- odbcDriverConnect(connStr);
    ```

3. Call the PersistModel stored procedure on SQL Server to transmite the serialized object to the database and store the binary representation of the model in a column. 

    ```R
    q <- paste("EXEC PersistModel @m='", modelbinstr,"'", sep="");
    sqlQuery (conn, q);
    ```

4. Use Management Studio to verify the model exists. In Object Explorer, right-click on the **nyc_taxi_models** table and click **Select Top 1000 Rows**. In Results, you should see a binary representation in the **models** column.

Saving a model to a table requires only an INSERT statement. However, it's often easier when wrapped in a stored procedure, such as *PersistModel*.

## Next steps

In the next and final lesson, learn how to perform scoring against the saved model using [!INCLUDE[tsql](../../includes/tsql-md.md)].

> [!div class="nextstepaction"]
> [Deploy the R model and use in SQL](walkthrough-deploy-and-use-the-model.md)
