---
title: "sp_rxPredict"
description: sp_rxPredict generates a predicted value for a given input consisting of a machine learning model stored in a binary format in a SQL Server database. 
ms.date: "04/05/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: machine-learning-services
ms.topic: "reference"
f1_keywords: 
  - "sp_rxPredict"
  - "sp_rxPredict_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_rxPredict procedure"
author: rwestMSFT
ms.author: randolphwest
ms.custom: 
# NOTE: sp_rxPredict is not supported on Linux.
monikerRange: ">=sql-server-2016"
---
# sp_rxPredict  
[!INCLUDE [SQL Server 2016 Windows only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

Generates a predicted value for a given input consisting of a machine learning model stored in a binary format in a SQL Server database.

Provides scoring on R and Python machine learning models in near real time. `sp_rxPredict` is a stored procedure provided as a wrapper for 
- `rxPredict` R function in [RevoScaleR](/r-server/r-reference/revoscaler/revoscaler) and [MicrosoftML](../../machine-learning/r/ref-r-microsoftml.md), and the [rx_predict](/machine-learning-server/python-reference/revoscalepy/rx-predict) Python function in [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](../../machine-learning/python/ref-py-microsoftml.md). It's written in C++ and is optimized specifically for scoring operations.

Although the model must be created using R or Python, once it's serialized and stored in a binary format on a target database engine instance, it can be consumed from that database engine instance even when R or Python integration is not installed. For more information, see [Real-time scoring with sp_rxPredict](../../machine-learning/predictions/real-time-scoring.md).

## Syntax

```
sp_rxPredict  ( @model, @input )
```

### Arguments

**model**

A pretrained model in a supported format. 

**input**

A valid SQL query

### Return values

A score column is returned, as well as any pass-through columns from the input data source.
Additional score columns, such as confidence interval, can be returned if the algorithm supports generation of such values.

## Remarks

To enable use of the stored procedure, SQLCLR must be enabled on the instance.

> [!NOTE]
> There are security implications to enabling this option. Use an alternative implementation, such as the [Transact-SQL PREDICT](../../t-sql/queries/predict-transact-sql.md?view=sql-server-2017&preserve-view=true) function, if SQLCLR cannot be enabled on your server.

The user needs `EXECUTE` permission on the database.

### Supported algorithms

To create and train model, use one of the supported algorithms for R or Python, provided by [SQL Server Machine Learning Services (R or Python)](../../machine-learning/sql-server-machine-learning-services.md), [SQL Server 2016 R Services](../../machine-learning/r/sql-server-r-services.md), [SQL Server Machine Learning Server (Standalone) (R or Python)](../../machine-learning/r/r-server-standalone.md), or [SQL Server 2016 R Server (Standalone)](../../machine-learning/r/r-server-standalone.md?view=sql-server-2016&preserve-view=true).

#### R: RevoScaleR models

  + [rxLinMod](/machine-learning-server/r-reference/revoscaler/rxlinmod) \*
  + [rxLogit](/machine-learning-server/r-reference/revoscaler/rxlogit) \*
  + [rxBTrees](/machine-learning-server/r-reference/revoscaler/rxbtrees) \*
  + [rxDtree](/machine-learning-server/r-reference/revoscaler/rxdtree) \*
  + [rxdForest](/machine-learning-server/r-reference/revoscaler/rxdforest) \*

Models marked with \* also support native scoring with the `PREDICT` function.

#### R: MicrosoftML models

  + [rxFastTrees](../../machine-learning/r/reference/microsoftml/rxfasttrees.md)
  + [rxFastForest](../../machine-learning/r/reference/microsoftml/rxfastforest.md)
  + [rxLogisticRegression](../../machine-learning/r/reference/microsoftml/rxlogisticregression.md)
  + [rxOneClassSvm](../../machine-learning/r/reference/microsoftml/rxoneclasssvm.md)
  + [rxNeuralNet](../../machine-learning/r/reference/microsoftml/rxneuralnet.md)
  + [rxFastLinear](../../machine-learning/r/reference/microsoftml/rxfastlinear.md)

#### R: Transformations supplied by MicrosoftML

  + [featurizeText](../../machine-learning/r/reference/microsoftml/rxfasttrees.md)
  + [concat](../../machine-learning/r/reference/microsoftml/concat.md)
  + [categorical](../../machine-learning/r/reference/microsoftml/categorical.md)
  + [categoricalHash](../../machine-learning/r/reference/microsoftml/categoricalHash.md)
  + [selectFeatures](../../machine-learning/r/reference/microsoftml/selectFeatures.md)

#### Python: revoscalepy models

  + [rx_lin_mod](/machine-learning-server/python-reference/revoscalepy/rx-lin-mod) \*
  + [rx_logit](/machine-learning-server/python-reference/revoscalepy/rx-logit) \*
  + [rx_btrees](/machine-learning-server/python-reference/revoscalepy/rx-btrees) \*
  + [rx_dtree](/machine-learning-server/python-reference/revoscalepy/rx-dtree) \*
  + [rx_dforest](/machine-learning-server/python-reference/revoscalepy/rx-dforest) \*

Models marked with \* also support native scoring with the `PREDICT` function.

#### Python: microsoftml models

  + [rx_fast_trees](../../machine-learning/python/reference/microsoftml/rx-fast-trees.md)
  + [rx_fast_forest](../../machine-learning/python/reference/microsoftml/rx-fast-forest.md)
  + [rx_logistic_regression](../../machine-learning/python/reference/microsoftml/rx-logistic-regression.md)
  + [rx_oneclass_svm](../../machine-learning/python/reference/microsoftml/rx-oneclass-svm.md)
  + [rx_neural_network](../../machine-learning/python/reference/microsoftml/rx-neural-network.md)
  + [rx_fast_linear](../../machine-learning/python/reference/microsoftml/rx-fast-linear.md)

#### Python: Transformations supplied by microsoftml

  + [featurize_text](../../machine-learning/python/reference/microsoftml/rx-fast-trees.md)
  + [concat](../../machine-learning/python/reference/microsoftml/concat.md)
  + [categorical](../../machine-learning/python/reference/microsoftml/categorical.md)
  + [categorical_hash](../../machine-learning/python/reference/microsoftml/categorical-hash.md)
  
### Unsupported model types

The following model types are not supported:

+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR.
+ PMML models in R.
+ Models created using other third-party libraries.
+ Models using a transformation function or formula containing a transformation, such as `A ~ log(B` are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on input data before passing the data to real-time scoring.

Real-time scoring does not use an interpreter, so any functionality that might require an interpreter is not supported during the scoring step.

## Examples

```sql
DECLARE @model = SELECT @model 
FROM model_table 
WHERE model_name = 'rxLogit trained';

EXEC sp_rxPredict @model = @model,
@inputData = N'SELECT * FROM data';
```

In addition to being a valid SQL query, the input data in *\@inputData* must include columns compatible with the columns in the stored model.

`sp_rxPredict` supports only the following .NET column types: double, float, short, ushort, long, ulong and string. You may need to filter out unsupported types in your input data before using it for real-time scoring.

For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR Parameter Data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).
