---
title: "Real-time scoring and native scoring| Microsoft Docs"
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

# Native scoring and real-time scoring

This topic describes features in SQL Server 2016 and SQL Server 2017 that provide scoring on machine learning models in near real-time.

+ What is native scoring vs. real-time scoring
+ How fast scoring works
+ Supported platforms

## What is native scoring and how is it different from real-time scoring?

In SQL Server 2016, Microsoft created an extensibility framework that allows R scripts to be executed from T-SQL. This framework supports any operation you might perform in R, ranging from simple functions to training complex machine learning models. However, the dual-process architecture that pairs R with SQL Server means that external R processes must be invoked for every call, regardless of the complexity of the operation. If you are loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external R process represents an unnecessary performance cost.

_Scoring_ is a two-step process: a pre-trained model is loaded from a table, and new input data, either tabular or single rows, is passed to the model, which generates new values (or _scores_). The output might be a single column value representing a probability, or several values, including a confidence interval, error, or other useful complement to the prediction.

When scoring many rows of data, the new values are typically inserted into a table as part of the scoring procedure.  However, you can also retrieve a single score in real time. When scoring successive inputs, the model might be cached so that it can be reloaded into memory very fast.

To support fast scoring, SQL Server Machine Learning Services (and Microsoft Machine Learning Server) provide built-in scoring libraries that work in R or in T-SQL. There are different options depending on which version you have.

**Native scoring**

+ The PREDICT function in Transact-SQL can be used for native scoring from any instance of SQL Server 2017. It requires only that you have a model already trained and saved in a table or can be called via T-SQL.

   The R runtime is not called and does not need to be installed.

**Real-time scoring**

+ The sp_rxPredict stored procedure can be used to generates scores from any supported model type, without calling the R runtime. 

  The option to use real-time scoring is available in SQL Server 2016, if you upgrade the R components using the standalone installer of Microsoft R Server. This option is also supported in SQL Server 2017, and might be a good option if you are scoring on a model type not supported by the PREDICT function.

+ The rxPredict function can be used for fast scoring within R code. However, to use rxPredict also requires that you use a model that was trained using one of the supported RevoScaleR or MicrosoftML algorithms.

For an example of real-time scoring in action, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

## How real-time and native scoring work

Both real-time scoring and native scoring use native C++ libraries from Microsoft that support scoring from multiple machine learning algorithms.

Because a model can be published and used for scoring without having to call the R interpreter, the overhead of multiple process interactions is reduced. This supports much faster prediction performance in enterprise production scenarios.

To generate scores using this library, you call one of the new scoring functions, and pass the following required inputs:

+ A compatible model. See the [Requirements](#Requirements) section for details.
+ A SQL query that defines the input data

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

For code samples, along with instrucitons on how to prepare the models in the required binary format, see this article:

+ [How to perform native scoring or real-time scoring](r/how-to-real-time-scoring.md)

 SQL Server R Services 2016, with an upgrade of the R Services instance to Microsoft R Server 9.1.0 or later

## Requirements

Supported platforms for native scoring are as follows:

+ SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)
+ Native scoring works with any version of SQL Server 2017. However, the PREDICT function works only with models that were trained using one of the supported algorithms, and saved using the optimized serialization function.

Real-time scoring is backward-compatible wth SQL Server 2016, and can also be run on Microsoft R Server. For more information, see [Real-time scoring](Real-time-scoring.md)

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

### Restrictions

The following model types are not supported:

+ Models containing other, unsupported types of R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR
+ PMML models
+ Models created using other R libraries from CRAN or other repositories
+ Models containing any other kind of R transformation
