---
title: "sp_rxPredict"
description: sp_rxPredict generates a predicted value for a given input consisting of a machine learning model stored in a binary format in a SQL Server database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: "reference"
f1_keywords:
  - "sp_rxPredict"
  - "sp_rxPredict_TSQL"
helpviewer_keywords:
  - "sp_rxPredict procedure"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016"
---
# sp_rxPredict

[!INCLUDE [SQL Server 2016 Windows only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

Generates a predicted value for a given input consisting of a machine learning model stored in a binary format in a SQL Server database.

Provides scoring on R and Python machine learning models in near real time. `sp_rxPredict` is a stored procedure written in C++, and is optimized specifically for scoring operations. `sp_rxPredict` is a wrapper for:

| Function | Wrapper |
| --- | --- |
| `rxPredict` R function | - [RevoScaleR](/r-server/r-reference/revoscaler/revoscaler)<br />- [MicrosoftML](../../machine-learning/r/ref-r-microsoftml.md) |
| [rx_predict](/machine-learning-server/python-reference/revoscalepy/rx-predict) Python function | - [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package)<br />- [microsoftml](../../machine-learning/python/ref-py-microsoftml.md) |

The model must be created using R or Python. However, once it's serialized and stored in a binary format on a target database engine instance, it can be consumed from that database engine instance, even when R or Python integration isn't installed. For more information, see [Real-time scoring with sp_rxPredict in SQL Server](../../machine-learning/predictions/real-time-scoring.md).

## Syntax

```syntaxsql
sp_rxPredict ( @model , @input )
[ ; ]
```

## Arguments

#### *@model*

A pretrained model in a supported format.

#### *@input*

A valid SQL query.

## Return values

A score column is returned, as well as any pass-through columns from the input data source.

Extra score columns, such as confidence interval, can be returned if the algorithm supports generation of such values.

## Remarks

To enable use of the stored procedure, SQLCLR must be enabled on the instance.

> [!NOTE]  
> There are security implications to enabling this option. Use an alternative implementation, such as the [PREDICT (Transact-SQL)](../../t-sql/queries/predict-transact-sql.md) function, if SQLCLR can't be enabled on your server.

The user needs `EXECUTE` permission on the database.

## Supported algorithms

To create and train model, use one of the supported algorithms for R or Python, provided by [SQL Server Machine Learning Services (R or Python)](../../machine-learning/sql-server-machine-learning-services.md), [SQL Server 2016 R Services](../../machine-learning/r/sql-server-r-services.md), [SQL Server Machine Learning Server (Standalone) (R or Python)](../../machine-learning/r/r-server-standalone.md), or [SQL Server 2016 R Server (Standalone)](../../machine-learning/r/r-server-standalone.md?view=sql-server-2016&preserve-view=true).

#### R: RevoScaleR models

- [rxLinMod](/machine-learning-server/r-reference/revoscaler/rxlinmod) <sup>1</sup>
- [rxLogit](/machine-learning-server/r-reference/revoscaler/rxlogit) <sup>1</sup>
- [rxBTrees](/machine-learning-server/r-reference/revoscaler/rxbtrees) <sup>1</sup>
- [rxDtree](/machine-learning-server/r-reference/revoscaler/rxdtree) <sup>1</sup>
- [rxdForest](/machine-learning-server/r-reference/revoscaler/rxdforest) <sup>1</sup>

<sup>1</sup> Models also support native scoring with the `PREDICT` function.

#### R: MicrosoftML models

- [rxFastTrees: Fast Tree](../../machine-learning/r/reference/microsoftml/rxfasttrees.md)
- [rxFastForest: Fast Forest](../../machine-learning/r/reference/microsoftml/rxfastforest.md)
- [rxLogisticRegression: Logistic Regression](../../machine-learning/r/reference/microsoftml/rxlogisticregression.md)
- [rxOneClassSvm: OneClass SVM](../../machine-learning/r/reference/microsoftml/rxoneclasssvm.md)
- [rxNeuralNet: Neural Net](../../machine-learning/r/reference/microsoftml/rxneuralnet.md)
- [rxFastLinear: Fast Linear Model](../../machine-learning/r/reference/microsoftml/rxfastlinear.md)

#### R: Transformations supplied by MicrosoftML

- [rxFastTrees: Fast Tree](../../machine-learning/r/reference/microsoftml/rxfasttrees.md)
- [concat: Machine Learning Concat Transform](../../machine-learning/r/reference/microsoftml/concat.md)
- [categorical: Machine Learning Categorical Data Transform](../../machine-learning/r/reference/microsoftml/categorical.md)
- [categoricalHash: Machine Learning Categorical HashData Transform](../../machine-learning/r/reference/microsoftml/categoricalHash.md)
- [selectFeatures: Machine Learning Feature Selection Transform](../../machine-learning/r/reference/microsoftml/selectFeatures.md)

#### Python: revoscalepy models

- [rx_lin_mod](/machine-learning-server/python-reference/revoscalepy/rx-lin-mod) <sup>1</sup>
- [rx_logit](/machine-learning-server/python-reference/revoscalepy/rx-logit) <sup>1</sup>
- [rx_btrees](/machine-learning-server/python-reference/revoscalepy/rx-btrees) <sup>1</sup>
- [rx_dtree](/machine-learning-server/python-reference/revoscalepy/rx-dtree) <sup>1</sup>
- [rx_dforest](/machine-learning-server/python-reference/revoscalepy/rx-dforest) <sup>1</sup>

<sup>1</sup> Models also support native scoring with the `PREDICT` function.

#### Python: microsoftml models

- [*microsoftml.rx_fast_trees*: Boosted Trees](../../machine-learning/python/reference/microsoftml/rx-fast-trees.md)
- [*microsoftml.rx_fast_forest*: Random Forest](../../machine-learning/python/reference/microsoftml/rx-fast-forest.md)
- [*microsoftml.rx_logistic_regression*: Logistic Regression](../../machine-learning/python/reference/microsoftml/rx-logistic-regression.md)
- [*microsoftml.rx_oneclass_svm*: Anomaly Detection](../../machine-learning/python/reference/microsoftml/rx-oneclass-svm.md)
- [*microsoftml.rx_neural_network*: Neural Network](../../machine-learning/python/reference/microsoftml/rx-neural-network.md)
- [*microsoftml.rx_fast_linear*: Linear Model with Stochastic Dual Coordinate Ascent](../../machine-learning/python/reference/microsoftml/rx-fast-linear.md)

#### Python: Transformations supplied by microsoftml

- [*microsoftml.rx_fast_trees*: Boosted Trees](../../machine-learning/python/reference/microsoftml/rx-fast-trees.md)
- [*microsoftml.concat*: Concatenates multiple columns into a single vector](../../machine-learning/python/reference/microsoftml/concat.md)
- [*microsoftml.categorical*: Converts a text column into categories](../../machine-learning/python/reference/microsoftml/categorical.md)
- [*microsoftml.categorical_hash*: Hashes and converts a text column into categories](../../machine-learning/python/reference/microsoftml/categorical-hash.md)

## Unsupported model types

The following model types aren't supported:

- Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR.

- PMML models in R.

- Models created using other non-Microsoft libraries.

- Models using a transformation function or formula containing a transformation, such as `A ~ log(B)` aren't supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on input data before passing the data to real-time scoring.

Real-time scoring doesn't use an interpreter, so any functionality that might require an interpreter isn't supported during the scoring step.

## Examples

```sql
DECLARE @model =
    SELECT @model
    FROM model_table
    WHERE model_name = 'rxLogit trained';

EXEC sp_rxPredict @model = @model,
    @inputData = N'SELECT * FROM data';
```

In addition to being a valid SQL query, the input data in *@inputData* must include columns compatible with the columns in the stored model.

`sp_rxPredict` supports only the following .NET column types: `double`, `float`, `short`, `ushort`, `long`, `ulong`, and `string`. You might need to filter out unsupported types in your input data before using it for real-time scoring.

For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR parameter data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).
