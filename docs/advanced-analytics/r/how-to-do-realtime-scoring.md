---
title: Generate forecasts and predictions using machine learning models - SQL Server Machine Learning Services
description: Use rxPredict, or sp_rxPredict for real-time scoring, or PREDICT T-SQL for native scoring for predictions and forecasting in R and Pythin in SQL Server Machine Learning.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/30/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# How to generate forecasts and predictions using machine learning models in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Using an existing model to forecast or predict outcomes for new data inputs is a core task in machine learning. This article enumerates the approaches for generating predictions in SQL Server. Among the approaches are internal processing methodologies for high-speed predictions, where speed is based on incremental reductions of run time dependencies. Fewer dependencies mean faster predictions.

Using the internal processing infrastructure (real-time or native scoring) comes with library requirements. Functions must be from the Microsoft libraries. R or Python code calling open-source or third-party functions is not supported in CLR or C++ extensions.

The following table summarizes the scoring frameworks for forecasting and predictions. 

| Methodology           | Interface         | Library requirements | Processing speeds |
|-----------------------|-------------------|----------------------|----------------------|
| Extensibility framework | [rxPredict (R)](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxpredict) <br/>[rx_predict (Python)](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict) | None. Models can be based on any R or Python function | Hundreds of milliseconds. <br/>Loading a runtime environment has a fixed cost, averaging three to six hundred milliseconds, before any new data is scored. |
| [Real-time scoring CLR extension](../real-time-scoring.md) | [sp_rxPredict](https://docs.microsoft.com//sql/relational-databases/system-stored-procedures/sp-rxpredict-transact-sql) on a serialized model | R: RevoScaleR, MicrosoftML <br/>Python: revoscalepy, microsoftml | Tens of milliseconds, on average. |
| [Native scoring C++ extension](../sql-native-scoring.md) | [PREDICT T-SQL function](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql) on a serialized model | R: RevoScaleR <br/>Python: revoscalepy | Less than 20 milliseconds, on average. | 

Speed of processing and not substance of the output is the differentiating feature. Assuming the same functions and inputs, the scored output should not vary based on the approach you use.

The model must be created using a supported function, then serialized to a raw byte stream saved to disk, or stored in binary format in a database. Using a stored procedure or T-SQL, you can load and use a binary model without the overhead of an R or Python language run time, resulting in faster time to completion when generating prediction scores on new inputs.

The significance of CLR and C++ extensions is proximity to the database engine itself. The native language of the database engine is C++, which means extensions written in C++ run with fewer dependencies. In contrast, CLR extensions depend on .NET Core. 

As you might expect, platform support is impacted by these run time environments. Native database engine extensions run anywhere the relational database is supported: Windows, Linux, Azure. CLR extensions with the .NET Core requirement is currently Windows only.

## Scoring overview

_Scoring_ is a two-step process. First, you specify an already trained model to load from a table. Second, pass new input data to the function, to generate prediction values (or _scores_). The input is often a T-SQL query, returning either tabular or single rows. You can choose to output a single column value representing a probability, or you might output several values, such as a confidence interval, error, or other useful complement to the prediction.

Taking a step back, the overall process of preparing the model and then generating scores can be summarized this way:

1. Create a model using a supported algorithm. Support varies by the scoring methodology you choose.
2. Train the model.
3. Serialize the model using a special binary format.
3. Save the model to SQL Server. Typically this means storing the serialized model in a SQL Server table.
4. Call the function or stored procedure, specifying the model and data inputs as parameters.

When the input includes many rows of data, it is usually faster to insert the prediction values into a table as part of the scoring process. Generating a single score is more typical in a scenario where you get input values from a form or user request, and return the score to a client application. To improve performance when generating successive scores, SQL Server might cache the model so that it can be reloaded into memory.

## Compare methods

To preserve the integrity of core database engine processes, support for R and Python is enabled in a dual architecture that isolates language processing from RDBMS processing. Starting in SQL Server 2016, Microsoft added an extensibility framework that allows R scripts to be executed from T-SQL. In SQL Server 2017, Python integration was added. 

The extensibility framework supports any operation you might perform in R or Python, ranging from simple functions to training complex machine learning models. However, the dual-process architecture requires invoking an external R or Python process for every call, regardless of the complexity of the operation. When the workload entails loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external processes adds latency that can be unacceptable in certain circumstances. For example, fraud detection requires fast scoring to be relevant.

To increase scoring speeds for scenarios like fraud detection, SQL Server added built-in scoring libraries as C++ and CLR extensions that eliminate the overhead of R and Python start-up processes.

[**Real-time scoring**](../real-time-scoring.md) was the first solution for high-performance scoring. Introduced in early versions of SQL Server 2017 and later updates to SQL Server 2016, real-time scoring relies on CLR libraries that stand in for R and Python processing over Microsoft-controlled functions in RevoScaleR, MicrosoftML (R), revoscalepy, and microsoftml (Python). CLR libraries are invoked using the **sp_rxPredict** stored procedure to generates scores from any supported model type, without calling the R or Python runtime.

[**Native scoring**](../sql-native-scoring.md) is a SQL Server 2017 feature, implemented as a native C++ library, but only for RevoScaleR and revoscalepy models. It is the fastest and more secure approach, but supports a smaller set of functions relative to other methodologies.

## Choose a scoring method

Platform requirements often dictate which scoring methodology to use.

| Product version and platform | Methodology |
|------------------------------|-------------|
| SQL Server 2017 on Windows, SQL Server 2017 Linux, and Azure SQL Database | **Native scoring** with T-SQL PREDICT |
| SQL Server 2017 (Windows only), SQL Server 2016 R Services at SP1 or higher | **Real-time scoring** with sp\_rxPredict stored procedure |

We recommend native scoring with the PREDICT function. Using sp\_rxPredict requires that you enable SQLCLR integration. Consider the security implications before you enable this option.

## Serialization and storage

To use a model with either of the fast scoring options, save the model using a special serialized format, which has been optimized for size and scoring efficiency.

+ Call [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel) to write a supported model to the **raw** format.
+ Call [rxUnserializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel)` to reconstitute the model for use in other R code, or to view the model.

**Using SQL**

From SQL code, you can train the model using [sp_execute_external_script](https://docs.microsoft.com//sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql), and directly insert the trained models into a table, in a column of type **varbinary(max)**. For a simple example, see [Create a preditive model in R](../tutorials/rtsql-create-a-predictive-model-r.md)

**Using R**

From R code, call the [rxWriteObject](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxwriteobject) function from RevoScaleR package to write the model directly to the database. The **rxWriteObject()** function can retrieve R objects from an ODBC data source like SQL Server, or write objects to SQL Server. The API is modeled after a simple key-value store.
  
If you use this function, be sure to serialize the model using [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel) first. Then, set the *serialize* argument in **rxWriteObject** to FALSE, to avoid repeating the serialization step.

Serializing a model to a binary format is useful, but not required if you are scoring predictions using R and Python run time environment in the extensibility framework. You can save a model in raw byte format to a file and then read from the file into SQL Server. This option might be useful if you are moving or copying models between environments.

## Scoring in related products

If you are using the [standalone server](r-server-standalone.md) or a [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), you have other options besides stored procedures and T-SQL functions for generating predictions quickly. Both the standalone server and Machine Learning Server support the concept of a *web service* for code deployment. You can bundle an R or Python pre-trained model as a web service, called at run time to evaluate new data inputs. For more information, see these articles:

+ [What are web services in Machine Learning Server?](https://docs.microsoft.com/machine-learning-server/operationalize/concept-what-are-web-services)
+ [What is operationalization?](https://docs.microsoft.com/machine-learning-server/what-is-operationalization)
+ [Deploy a Python model as a web service with azureml-model-management-sdk](https://docs.microsoft.com/machine-learning-server/operationalize/python/quickstart-deploy-python-web-service)
+ [Publish an R code block or a real-time model as a new web service](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/publishservice)
+ [mrsdeploy package for R](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/mrsdeploy-package)


## See also

+ [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel)  
+ [rxRealTimeScoring](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxrealtimescoring)
+ [sp-rxPredict](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-rxpredict-transact-sql)
+ [PREDICT T-SQL](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql)