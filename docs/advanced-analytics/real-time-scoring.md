---
title: "Real-time scoring in SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
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

# Real-time scoring in SQL Server 2016

This topic describes a feature that you can add to an instance of SQL Server 2016 to support scoring on machine learning models in near real-time.

> [!TIP]
> A related feature in SQL Server 2017 supports fast scoring in T-SQL, by using the PREDICT function. For more information, see [Real-time scoring and native scoring](real-time-scoring.md).

## How real-time scoring works

Real-time scoring is supported in both SQL Server 2017 and SQL Server 2016, on specific model types created by using either RevoScaleR or MicrosoftML algorithms. It uses native C++ libraries to generate scores, based on user input provided to a machine learning model stored in a special binary format.

Because a trained model can be used for scoring without having to call an external language runtime, the overhead of multiple processes is reduced. This supports much faster prediction performance for production scoring scenarios. Because the data never leaves SQL Server, results can be generated and inserted into a new table without any data translation between R and SQL.

Real-time scoring is a multi-step process:

1. The feature must be enabled on a per-database basis.
2. You load the pre-trained model in binary format.
3. You provide new input data, either tabular or single rows, as input to the model.
4. To generate scores, call the sp_rxPredict stored procedure.

## Get started

For code examples and instructions, see [How to perform native scoring or real-time scoring](r/how-to-real-time-scoring.md).

For an example of how rxPredict can used for scoring, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

> [!TIP]
> If you are working exclusively in R code, you can also use the [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict) function for fast scoring.

## Requirements

Real-time scoring is supported on these platforms:

+ SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)
+ SQL Server R Services 2016, with an upgrade of the R Services instance to Microsoft R Server 9.1.0 or later
+ Microsoft Machine Learning Server (Standalone)

On SQL Server, you must enable the real-time scoring feature in advance. This is because the feature requires installation of CLR-based libraries into SQL Server.

For information regarding real-time scoring in a distributed environment based on Microsoft R Server, please refer to the [publishService](https://msdn.microsoft.com/microsoft-r/mrsdeploy/packagehelp/publishservice) function available in the [mrsDeploy package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy), which supports publishing models for real-time scoring as a new a web service running on R Server.

### Restrictions

+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](#bkmk_rt_supported_algos).

+ The model must be saved using the new serialization function provided in Microsoft R Server 9.1.0. The serialization method has been optimized to support fast scoring.

+ Real-time scoring does not use an R interpreter; therefore, any functionality that might require R interpreter is not supported during real-time scoring.  These might include:

  + Models using the `rxGlm` or `rxNaiveBayes` algorithms are not currently supported

  + RevoScaleR models that use an R transformation function, or a formula that contains a transformation, such as <code>A ~ log(B)</code> are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on the to input data before passing the data to real-time scoring.

+ Real-time scoring is currently optimized for fast predictions on smaller data sets, ranging from a few rows to  hundreds of thousand of rows. On very large datasets, scoring in R using rxPredict might be faster.

### <a name="bkmk_rt_supported_algos">Algorithms that support real-time scoring

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdtree)
  + [rxdForest](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdforest)

+ MicrosoftML models

  + [rxFastTrees](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfasttrees)
  + [rxFastForest](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfastforest)
  + [rxLogisticRegression](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxlogisticregression)
  + [rxOneClassSvm](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxoneclasssvm)
  + [rxNeuralNet](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxneuralnet)
  + [rxFastLinear](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfastlinear)

+ Transformations supplied by MicrosoftML

  + [featurizeText](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfasttrees)
  + [concat](https://docs.microsoft.com/r-server/r-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/r-server/r-reference/microsoftml/categorical)
  + [categoricalHash](https://docs.microsoft.com/r-server/r-reference/microsoftml/categoricalHash)
  + [selectFeatures](https://docs.microsoft.com/r-server/r-reference/microsoftml/selectFeatures)


### Unsupported model types

The following model types are not supported:

+ Models containing other, unsupported types of R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR
+ PMML models
+ Models created using other R libraries from CRAN or other repositories
+ Models containing any other kind of R transformation other than those listed here

### Known issues

+ `sp_rxPredict` returns an inaccurate message when a NULL value is passed as the model: "System.Data.SqlTypes.SqlNullValueException:Data in Null". You can ignore this message.
