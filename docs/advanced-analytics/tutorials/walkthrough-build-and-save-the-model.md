---
title: "6. Build an R model and save to SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
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
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# 6.Build an R model and save to SQL Server

In this step, you'll learn how to build a machine learning model and save the model in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Create a classification model

The model you'll build is a binary classifier that predicts whether the taxi driver is likely to get a tip on a particular ride or not. You'll use the data source you created in the previous lesson, `featureDataSource,` to train the tip classifier, using logistic regression.

Here are the features you'll use in the model:

-   passenger count
-   trip distance
-   trip time in seconds
-   direct_distance between pickup and drop off points

1.  Call the **rxLogit** function, included in the **RevoScaleR** package, to create a logistic regression model.
  
    ```R
    system.time(logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = featureDataSource))
    ```

    > [!TIP]
    > The call that builds the model is enclosed in the system.time function. This lets you get the time required to build the model.
    
2.  After you build the model, you'll want to inspect it using the `summary` function, and view the coefficients.
  
    ```R
    summary(logitObj)
    ```

     *Results*

     *Logistic Regression Results for: tipped ~ passenger_count + trip_distance + trip_time_in_secs +*
     <br/>*direct_distance*
     <br/>*Data: featureDataSource (RxSqlServerData Data Source)*
     <br/>*Dependent variable(s): tipped*
     <br/>*Total independent variables: 5*
     <br/>*Number of valid observations: 17068*
     <br/>*Number of missing observations: 0*
     <br/>*-2\*LogLikelihood: 23540.0602 (Residual deviance on 17063 degrees of freedom)*
     <br/>*Coefficients:*
     <br/>*Estimate Std. Error z value Pr(>|z|)*
     <br/>*(Intercept)       -2.509e-03  3.223e-02  -0.078  0.93793*
     <br/>*passenger_count   -5.753e-02  1.088e-02  -5.289 1.23e-07 \*\*\**
     <br/>*trip_distance     -3.896e-02  1.466e-02  -2.658  0.00786 \*\**
     <br/>*trip_time_in_secs  2.115e-04  4.336e-05   4.878 1.07e-06 \*\*\**
     <br/>*direct_distance    6.156e-02  2.076e-02   2.966  0.00302 \*\**
     <br/>*---*
     <br/>*Signif. codes:  0 ‘\*\*\*’ 0.001 ‘\*\*’ 0.01 ‘\*’ 0.05 ‘.’ 0.1 ‘ ’ 1*
     <br/>*Condition number of final variance-covariance matrix: 48.3933*
     <br/>*Number of iterations: 4*

## Use the model for scoring

Now that the model is built, you can use to predict whether the driver is likely to get a tip on a particular drive or not.

1. First, define the data object to use for storing the scoring results.

    ```R
    scoredOutput <- RxSqlServerData(
      connectionString = connStr,
      table = "taxiScoreOutput"  )
    ```

    + To make this example simpler, the input to the logistic regression model is the same `featureDataSource` that you used to train the model.  More typically, you might have some new data to score with, or you might have set aside some data for testing vs. training.

    + The prediction results are saved in the table, _taxiscoreOutput_. Notice that the schema for this table is not defined when you create it using rxSqlServerData, but is obtained from the *scoredOutput* object output from rxPredict.
  
    + To create the table that stores the predicted values, the SQL login running the rxSqlServer data function must have DDL privileges in the database. If the login cannot create tables, the statement will fail.
  
2. Call the **rxPredict** function to generate results.
  
    ```R
    rxPredict(modelObject = logitObj,
        data = featureDataSource,
        outData = scoredOutput,
        predVarNames = "Score",
        type = "response",
        writeModelVars = TRUE, overwrite = TRUE)
    ```

## Plot model accuracy

To get an idea of the accuracy of the model, you can use the **rxRocCurve** function to plot the Receiver Operating Curve. Because rxRocCurve is one of the new functions provided by the RevoScaleR package that supports remote compute contexts, you have two options:

+ You can use the rxRocCurve function to execute the plot in the remote computer context and then return the plot to your local client.
+ You can also import the data to your R client computer, and use other R plotting functions to create the performance graph.

Let's try both techniques.

### Plot using the SQL Server compute context

1.  Call the function rxRocCurve and provide the data defined earlier as input.
  
    ```R
    rxRocCurve( "tipped", "Score", scoredOutput)
    ```
  
    Note that you must also specify the label, or the variable you are trying to predict (you'll find it in the column *tipped*) and the name of the column that stores the prediction (_Score_).

2.  View the graph that is generated by opening the R graphics device, or by clicking the **Plot** window in your R IDE.
  
    ![ROC plot for the model](media/rsql-e2e-rocplot.png "ROC plot for the model")

    Voila! The graph is created on the remote compute context, and then returned to your R environment.
    
### Create the plots in the local compute context using data from SQL Server

1.  Use the **rxImport** function to bring the specified data to your local R environment.
  
    ```R
    scoredOutput = rxImport(scoredOutput)
    ```
  
2.  Having loaded the data into local memory, you can then call the **ROCR** library to create some predictions, and  generate the plot.
  
    ```R
    library('ROCR')
    pred <- prediction(scoredOutput$Score, scoredOutput$tipped)
    
    acc.perf = performance(pred, measure = 'acc')
    plot(acc.perf)
    ind = which.max( slot(acc.perf, 'y.values')[[1]] )
    acc = slot(acc.perf, 'y.values')[[1]][ind]
    cutoff = slot(acc.perf, 'x.values')[[1]][ind]
    ```
  
3.  The following plot is generated in both cases.
  
    ![plotting model performance using R](media/rsql-e2e-performanceplot.png "plotting model performance using R")
  
## Deploy the model

After you have built a model and evaluated its accuracy, you might want to deploy it to production and optimize it for fast scoring-- a process sometimes referred to as *operationalizing*. Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] lets you invoke an R model using a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, it is extremely easy to use R in a client application and to use SQL Server features to improve scoring throughput.

However, before you can call the model from an external application, you must save the model to the database used for production. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], trained models are stored in binary form, in a single column of type **varbinary(max)**.

Moving a trained model from R to SQL Server includes these steps:

+ Serializing the model into a hexadecimal string
+ Transmitting the serialized object to the database
+ Saving the model in a varbinary(max) column

To perform scoring using a saved model requires these steps:

+ Getting the saved model from the table using a SELECT query
+ Deserializing the model as part of your R code
+ Generating results with a compatile prediction function

In this section, you will learn how to persist the model, and how to call it to make predictions.

### Serialize the model
  
+  In your local R environment, serialize the model and save it in a variable.
  
    ```R
    modelbin <- serialize(logitObj, NULL)
    modelbinstr=paste(modelbin, collapse="")
    ```
  
    The `serialize` function is included in the R **base** package, and provides a simple low-level interface for serializing to connections. For more information, see [http://www.inside-r.org/r-doc/base/serialize](http://www.inside-r.org/r-doc/base/serialize).

### Move the model to SQL Server

+ Open an ODBC connection, and call a stored procedure to store the binary representation of the model in a column in the database.
  
    ```R
    library(RODBC)
    conn <- odbcDriverConnect(connStr )
  
    # persist model by calling a stored procedure from SQL
    q\<-paste("EXEC PersistModel @m='", modelbinstr,"'", sep="")
    sqlQuery (conn, q)
    ```

Saving a model to a table requires only an INSERT statement. However, to make it easier, here we have used the _PersistModel_ stored procedure, which you can modify for your own use.

For reference, here is the complete code of the stored procedure:

```SQL
CREATE PROCEDURE [dbo].[PersistModel]  @m nvarchar(max)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements
   SET NOCOUNT ON;
   INSERT INTO nyc_taxi_models (model) values (convert(varbinary(max),@m,2))
END
```

> [!TIP]
> We recommend creating helper functions such as this stored procedure to make it easier to manage and update your R models in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

### Invoke the saved model

After you have saved the model to the database, you can call it directly from [!INCLUDE[tsql](../../includes/tsql-md.md)] code, using the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

For example, to generate predictions, you simply connect to the database and run a stored procedure that uses the saved model as an input, together with some input data. However, if you have a model you use often, it's easier to wrap the input query and the call to the model, as well as any other parameters,  in a custom stored procedure.

In the next step, you'll learn how to perform scoring against the saved model using [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Next step

[7. Deploy and Use the Model](/walkthrough-deploy-and-use-the-model.md)

## Previous step

[5. Create Data Features using R and SQL](walkthrough-create-data-features.md)