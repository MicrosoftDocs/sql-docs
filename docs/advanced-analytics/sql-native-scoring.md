---
title: "Native scoring| Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# Native scoring

This topic describes features in SQL Server 2017 that provide scoring on machine learning models in near realtime.

+ What is native scoring vs. realtime scoring
+ How it works
+ Supported platforms and requirements

## What is native scoring and how is it different from realtime scoring?

In SQL Server 2016, Microsoft created an extensibility framework that allows R scripts to be executed from T-SQL. This framework supports any operation you might perform in R, ranging from simple functions to training complex machine learning models. However, the dual-process architecture that pairs R with SQL Server means that external R processes must be invoked for every call, regardless of the complexity of the operation. If you are loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external R process represents an unnecessary performance cost.

_Scoring_ is a two-step process: a pre-trained model is loaded from a table, and new input data, either tabular or single rows, is passed to the model, which generates new values (or _scores_). The output might be a single column value representing a probability, or several values, including a confidence interval, error, or other useful complement to the prediction.

When scoring many rows of data, the new values are typically inserted into a table as part of the scoring procedure.  However, you can also retrieve a single score in real time. When scoring successive inputs, the model might be cached so that it can be reloaded into memory fast.

To support fast scoring, SQL Server Machine Learning Services (and Microsoft Machine Learning Server) provide built-in scoring libraries that work in R or in T-SQL. There are different options depending on which version you have.

**Native scoring**

+ The PREDICT function in Transact-SQL can be used for _native scoring_ from any instance of SQL Server 2017. It requires only that you have a model already trained and saved in a table or can be called via T-SQL. It is a kind of realtime scoring that uses native T-SQL functions; no additional configuration required.

   The R runtime is not called and does not need to be installed.

**Realtime scoring**

+ **sp_rxPredict** is a stored procedure for realtime scoring that can be used to generates scores from any supported model type, without calling the R runtime.

  This option to use realtime scoring is also available in SQL Server 2016, if you upgrade the R components using the standalone installer of Microsoft R Server. sp_rxPredict is also supported in SQL Server 2017, and might be a good option if you are scoring on a model type not supported by the PREDICT function.

+ The rxPredict function can be used for fast scoring within R code.

For all of these scoring methods, you must use a model that was trained using one of the supported RevoScaleR or MicrosoftML algorithms.

For an example of realtime scoring in action, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

## How native scoring works

Native scoring uses native C++ libraries from Microsoft that can read the model from a special binary format and generate scores. Because a model can be published and used for scoring without having to call the R interpreter, the overhead of multiple process interactions is reduced. This supports much faster prediction performance in enterprise production scenarios.

To generate scores using this library, you call the scoring function, and pass the following required inputs:

+ A compatible model. See the [Requirements](#Requirements) section for details.
+ Input data, typically defined as a SQL query

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

For code samples, along with instructions on how to prepare the models in the required binary format, see this article:

+ [How to perform realtime scoring](r/how-to-do-realtime-scoring.md)

## Requirements

Supported platforms are as follows:

+ SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)
    
    Native scoring using PREDICT requires SQL Server 2017.
    It works on any version of SQL Server 2017, including Linux.

    You can also perform realtime scoring using sp_rxPredict, which requires that SQL CLR be enabled.

+ SQL Server 2016

   Real-time scoring using sp_rxPredict is possible with SQL Server 2016, and can also be run on Microsoft R Server. This option requires SQLCLR to be enabled, and that you install the Microsoft R Server upgrade.
   For more information, see [Realtime scoring](Real-time-scoring.md)

### Model preparation

+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](#bkmk_native_supported_algos).
+ The model must be saved using the new serialization function provided in Microsoft R Server 9.1.0. The serialization function is optimized to support fast scoring.

### <a name="bkmk_native_supported_algos"></a> Algorithms that support native scoring

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdtree)
  + [rxdForest](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdforest)

If you need to use models from MicrosoftML, use realtime scoring with sp_rxPredict.

### Restrictions

The following model types are not supported:

+ Models containing other, unsupported types of R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR
+ PMML models
+ Models created using other R libraries from CRAN or other repositories
+ Models containing any other kind of R transformation
