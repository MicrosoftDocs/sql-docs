---
title: "sp_rxPredict | Microsoft Docs"
ms.custom: ""
ms.date: "08/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
  
ms.topic: "language-reference"
f1_keywords: 
  - "sp_rxPredict"
  - "sp_rxPredict_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_rxPredict procedure"
author: "HeidiSteen"
ms.author: "heidist"
manager: cgronlun
---
# sp_rxPredict  
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Generates a predicted value for a given input consisting of a machine learning model stored in a binary format in a SQL Server database.

Provides scoring on R and Python machine learning models in near real-time. `sp_rxPredict` is a stored procedure provided as a wrapper for the `rxPredict` R function in [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) and [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package), and the [rx_predict](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-predict) Python function in [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package). It is written in C++ and is optimized specifically for scoring operations.

Although the model must be created using R or Python, once it is serialized and stored in a binary format on a target database engine instance, it can be consumed from that database engine instance even when R or Python integration is not installed. For more information, see [Real-time scoring with sp_rxPredict](https://docs.microsoft.com/sql/advanced-analytics/real-time-scoring).


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
> There are security implications to enabing this option. Use an alternative implementation, such as the [Transact-SQL PREDICT](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql?view=sql-server-2017) function, if SQLCLR cannot be enabled on your server.

The user needs `EXECUTE` permission on the database.


### Supported algorithms

To create and train model, use one of the supported algorithms for R or Python, provided by [SQL Server 2016 R Services](https://docs.microsoft.com/sql/advanced-analytics/r/sql-server-r-services?view=sql-server-2017), [SQL Server 2016 R Server (Standalone)](https://docs.microsoft.com/sql/advanced-analytics/r/r-server-standalone?view=sql-server-2016), [SQL Server 2017 Machine Learning Services (R or Python)](https://docs.microsoft.com//sql/advanced-analytics/what-is-sql-server-machine-learning?view=sql-server-2017), or [SQL Server 2017 Server (Standalone) (R or Python)](https://docs.microsoft.com/sql/advanced-analytics/r/r-server-standalone?view=sql-server-2017).


#### R: RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdtree)
  + [rxdForest](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdforest)

#### R: MicrosoftML models

  + [rxFastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [rxFastForest](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastforest)
  + [rxLogisticRegression](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxlogisticregression)
  + [rxOneClassSvm](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxoneclasssvm)
  + [rxNeuralNet](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet)
  + [rxFastLinear](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastlinear)

#### R: Transformations supplied by MicrosoftML

  + [featurizeText](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [concat](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categorical)
  + [categoricalHash](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categoricalHash)
  + [selectFeatures](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/selectFeatures)

#### Python: revoscalepy models

  + [rx_lin_mod](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-lin-mod)
  + [rx_logit](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-logit)
  + [rx_btrees](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-btrees)
  + [rx_dtree](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dtree)
  + [rx_dforest](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dforest)


#### Python: microsoftml models

  + [rx_fast_trees](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-trees)
  + [rx_fast_forest](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-forest)
  + [rx_logistic_regression](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-logistic-regression)
  + [rx_oneclass_svm](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-oneclass-svm)
  + [rx_neural_network](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-neural-network)
  + [rx_fast_linear](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-linear)

#### Python: Transformations supplied by microsoftml

  + [featurize_text](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-trees)
  + [concat](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/categorical)
  + [categorical_hash](https://docs.microsoft.com/machine-learning-server/python-referencee/microsoftml/categorical-hash)
  
### Unsupported model types

The following model types are not supported:

+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR
+ PMML models in R
+ Models created using other third-party libraries 

## Examples

```sql
DECLARE @model = SELECT @model 
FROM model_table 
WHERE model_name = 'rxLogit trained';

EXEC sp_rxPredict @model = @model,
@inputData = N'SELECT * FROM data';
```

In addition to being a valid SQL query, the input data in *@inputData* must include columns compatible with the columns in the stored model.

`sp_rxPredict` supports only the following .NET column types: double, float, short, ushort, long, ulong and string. You may need to filter out unsupported types in your input data before using it for real-time scoring. 

  For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR Parameter Data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).

