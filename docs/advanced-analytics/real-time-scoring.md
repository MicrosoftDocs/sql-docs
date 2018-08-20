---
title: Real-time scoring in SQL Server machine learning | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Real-time scoring in SQL Server machine learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article explains how scoring in near-real-time works for SQL Server relational data, using machine learning models written in R. 

> [!Note]
> Native scoring is a special implementation of real-time scoring that uses the native T-SQL PREDICT function for very fast scoring. For more information and availability, see [Native scoring](sql-native-scoring.md).

## How real-time scoring works

Real-time scoring is supported in both SQL Server 2017 and SQL Server 2016, on specific model types based on RevoScaleR or MicrosoftML functions such as  [rxLinMod (RevoScaleR)](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlinmod) or [rxNeuralNet (MicrosoftML)](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet). It uses native C++ libraries to generate scores, based on user input provided to a machine learning model stored in a special binary format.

Because a trained model can be used for scoring without having to call an external language runtime, the overhead of multiple processes is reduced. This supports much faster prediction performance for production scoring scenarios. Because the data never leaves SQL Server, results can be generated and inserted into a new table without any data translation between R and SQL.

Real-time scoring is a multi-step process:

1. The stored procedure that does scoring must be enabled on a per-database basis.
2. You load the pre-trained model in binary format.
3. You provide new input data, either tabular or single rows, as input to the model.
4. To generate scores, call the sp_rxPredict stored procedure.

## Get started

For code examples and instructions, see [How to perform native scoring or real-time scoring](r/how-to-do-realtime-scoring.md).

For an example of how rxPredict can be used for scoring, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

> [!TIP]
> If you are working exclusively in R code, you can also use the [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict) function for fast scoring.

## Requirements

Real-time scoring is supported on these platforms:

+ SQL Server 2017 Machine Learning Services
+ SQL Server R Services 2016, with an upgrade of R components to 9.1.0 or later

On SQL Server, you must enable the real-time scoring feature in advance to add the CLR-based libraries to SQL Server.

For information regarding real-time scoring in a distributed environment based on Microsoft R Server, refer to the [publishService](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/publishservice) function available in the [mrsDeploy package](https://docs.microsoft.com/machine-learning-server/r-reference/mrsdeploy/mrsdeploy-package), which supports publishing models for real-time scoring as a new a web service running on R Server.

### Restrictions

+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](#bkmk_rt_supported_algos). Real-time scoring with `sp_rxPredict` supports both RevoScaleR and MicrosoftML algorithms.

+ The model must be saved using the new serialization functions: [rxSerialize](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R, and [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

+ Real-time scoring does not use an interpreter; therefore, any functionality that might require an interpreter is not supported during the scoring step.  These might include:

  + Models using the `rxGlm` or `rxNaiveBayes` algorithms are not currently supported

  + RevoScaleR models that use an R transformation function, or a formula that contains a transformation, such as <code>A ~ log(B)</code> are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on the to input data before passing the data to real-time scoring.

+ Real-time scoring is currently optimized for fast predictions on smaller data sets, ranging from a few rows to hundreds of thousands of rows. On big datasets, using [rxPredict](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxpredict) might be faster.

### <a name="bkmk_rt_supported_algos">Algorithms that support real-time scoring

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlinmod) \*
  + [rxLogit](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlogit) \*
  + [rxBTrees](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxbtrees) \*
  + [rxDtree](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdtree) \*
  + [rxdForest](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdforest) \*
  
  Models marked with \* also support native scoring with the PREDICT function.

+ MicrosoftML models

  + [rxFastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [rxFastForest](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastforest)
  + [rxLogisticRegression](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxlogisticregression)
  + [rxOneClassSvm](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxoneclasssvm)
  + [rxNeuralNet](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet)
  + [rxFastLinear](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastlinear)

+ Transformations supplied by MicrosoftML

  + [featurizeText](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [concat](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categorical)
  + [categoricalHash](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categoricalHash)
  + [selectFeatures](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/selectFeatures)

### Unsupported model types

Real-time scoring is not supported for R transformations other than those explicitly listed in the previous section. 

For developers accustomed to working with RevoScaleR and other Microsoft R-specific libraries, unsupported functions include 
 `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR, PMML models, and other models created using other R libraries from CRAN or other repositories.

### Known issues

+ `sp_rxPredict` returns an inaccurate message when a NULL value is passed as the model: "System.Data.SqlTypes.SqlNullValueException:Data in Null".

## Next steps

[How to do real-time scoring](r/how-to-do-realtime-scoring.md)
