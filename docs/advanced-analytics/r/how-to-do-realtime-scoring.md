---
title: How to perform real-time scoring or native scoring in SQL Server Machine Learning | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# How to perform real-time scoring or native scoring in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Forecasting and predicting outcomes based on fresh data inputs and existing models is a core task in machine learning. This article enumerates the approaches, which include internal processing options for high speed predictions, where speed is based on increasing levels of proximity to the database engine. Using the internal processing infrastructure introduces code requirements. Models and other functions must be generated from the Microsoft libraries only. Arbitrary R or Python code is not an option.

The following table summarizes the scoring frameworks for forecasting and predictions. Speed of processing and not substance of the output is the differentiating feature. Assuming the same functions and inputs, the scored output should not vary based on the approach you use.

| Prediction mechanisms | Code restrictions | Processing behaviors |
|-----------------------|-------------------|----------------------|
| Extensibility framework | None. Models can be based on any R or Python function | Hundreds of milliseconds. Loading the R or Python runtime is a fixed cost, consuming three to four hundred milliseconds on average, before any new data is scored. |
| Real-time scoring | R: RevoScaleR and MicrosoftML <br/>Python: revoscalepy and microsoftml | tens of milliseconds. |
| Native scoring | R: RevoScaleR <br/>Python: revoscalepy | Ones of milliseconds, on average. | 

Both real-time scoring and native scoring are designed to let you use a machine learning model without having to install R. Given a pre-trained model in a compatible format - saved to a SQL Server database - you can use standard data access techniques to quickly generate prediction scores on new inputs.

## What is native scoring and how is it different from real-time scoring?

In SQL Server 2016, Microsoft created an extensibility framework that allows R scripts to be executed from T-SQL. This framework supports any operation you might perform in R, ranging from simple functions to training complex machine learning models. However, the dual-process architecture requires invoking an external R process for every call, regardless of the complexity of the operation. If you are loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external R process represents an unnecessary performance cost.

_Scoring_ is a two-step process. First, you specify a pre-trained model to load from a table. Second, pass new input data to the function, to generate prediction values (or _scores_). The input can be either tabular or single rows. You can choose to output a single column value representing a probability, or you might output several values, such as a confidence interval, error, or other useful complement to the prediction.

When the input includes many rows of data, it is usually faster to insert the prediction values into a table as part of the scoring process.  Generating a single score is more typical in a scenario where you get input values from a form or user request, and return the score to a client application. To improve performance when generating successive scores, SQL Server might cache the model so that it can be reloaded into memory.

To support fast scoring, SQL Server Machine Learning Services (and Microsoft Machine Learning Server) provide built-in scoring libraries that work in R or in T-SQL. There are different options depending on which version you have.

**Native scoring**

+ The PREDICT function in Transact-SQL supports _native scoring_ in any instance of SQL Server 2017. It requires only that you have a model already trained, which you can call using T-SQL. Native scoring using T-SQL has these advantages:

    + No additional configuration is required.
    + The R runtime is not called. There is no need to install R.

**Real-time scoring**

+ **sp_rxPredict** is a stored procedure for real-time scoring that can be used to generates scores from any supported model type, without calling the R runtime.

  This stored procedure is also available in SQL Server 2016, if you upgrade the R components using the standalone installer of Microsoft R Server. sp_rxPredict is also supported in SQL Server 2017. Therefore, you might use this function when generating scores with a model type not supported by the PREDICT function.

+ The rxPredict function can be used for fast scoring within R code.

For all of these scoring methods, you must use a model that was trained using one of the supported RevoScaleR or MicrosoftML algorithms.

For an example of real-time scoring in action, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

## Choose a scoring method

The following options are supported for fast batch prediction:

+ **Native scoring**: T-SQL PREDICT function in SQL Server 2017 Windows, SQL Server 2017 Linux, and Azure SQL Database.
+ **Real-time scoring**: Using the sp\_rxPredict stored procedure in either SQL Server 2016 or SQL Server 2017 (Windows only).

> [!NOTE]
> Use of the PREDICT function is recommended in SQL Server 2017.
> To use sp\_rxPredict requires that you enable SQLCLR integration. Consider the security implications before you enable this option.

The overall process of preparing the model and then generating scores is similar:

1. Create a model using a supported algorithm.
2. Serialize the model using a special binary format.
3. Make the model available to SQL Server. Typically this means storing the serialized model in a SQL Server table.
4. Call the function or stored procedure, and pass the model and input data.

## Requirements

+ The PREDICT function is available in all editions of SQL Server 2017 and is enabled by default. You do not need to install R or enable additional features.

+ If using sp\_rxPredict, some additional steps are required. See [Enable real-time scoring](#bkmk_enableRtScoring).

+ At this time, only RevoScaleR and MicrosoftML can create compatible models. Additional model types might become available in future. For the list of currently supported algorithms, see [Real-time scoring](../real-time-scoring.md).

## Serialization and storage

To use a model with either of the fast scoring options, save the model using a special serialized format, which has been optimized for size and scoring efficiency.

+ Call `rxSerializeModel` to write a supported model to the **raw** format.
+ Call `rxUnserializeModel` to reconstitute the model for use in other R code, or to view the model.

For more information, see [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel).

**Using SQL**

From SQL code, you can train the model using `sp_execute_external_script`, and directly insert the trained models into a table, in a column of type **varbinary(max)**.

For a simple example, see [this tutorial](../tutorials/rtsql-create-a-predictive-model-r.md)

**Using R**

From R code, there are two ways to save the model to a table:

+ Call the `rxWriteObject` function, from the RevoScaleR package, to write the model directly to the database.

  The `rxWriteObject()` function can retrieve R objects from an ODBC data source like SQL Server, or write objects to SQL Server. The API is modeled after a simple key-value store.
  
  If you use this function, be sure to serialize the model using the new serialization function first. Then, set the *serialize* argument in `rxWriteObject` to FALSE, to avoid repeating the serialization step.

+ You can also save the model in raw format to a file and then read from the file into SQL Server. This option might be useful if you are moving or copying models between environments.


## Scoring in related Microsoft products

If you are using the standalone server or a Microsoft Machine Learning Server instead of SQL Server in-database analytics, you have other options besides stored procedures and T-SQL functions for generating predictions.

Both the standalone server and Machine Learning Server support the concept of a *web service* for code deployment. You can bundle an R or Python pre-trained model as a web service, called at run time to evaluate new data inputs. For more information, see these articles:

+ [What are web services in Machine Learning Server?](https://docs.microsoft.com/machine-learning-server/operationalize/concept-what-are-web-services)
+ [What is operationalization?](https://docs.microsoft.com/machine-learning-server/operationalize/concept-operationalize-deploy-consume)
+ [Deploy a Python model as a web service with azureml-model-management-sdk](https://docs.microsoft.com/machine-learning-server/operationalize/python/quickstart-deploy-python-web-service)
+ [Publish an R code block or a real-time model as a new web service](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/publishservice)
+ [mrsdeploy package for R](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/mrsdeploy-package)
