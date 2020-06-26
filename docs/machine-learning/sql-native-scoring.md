---
title: Native scoring with T-SQL PREDICT
titleSuffix: SQL machine learning
description: Learn how to use native scoring with the PREDICT T-SQL function to generate prediction values for new data inputs in near-real-time. 
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 06/16/2020
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=azuresqldb-mi-current||=sqlallproducts-allversions"
---

# Native scoring using the PREDICT T-SQL function with SQL machine learning
[!INCLUDE[tsql-appliesto-ss2017-asdb-asdbmi-asdw-xxx-md](../includes/tsql-appliesto-ss2017-asdb-asdbmi-asdw-xxx-md.md)]

Learn how to use native scoring with the [PREDICT T-SQL function](../t-sql/queries/predict-transact-sql.md) to generate prediction values for new data inputs in near-real-time. Native scoring requires that you have an already trained model.

The `PREDICT` function uses the native C++ extension capabilities in SQL machine learning. This methodology offers the fastest possible processing speed of forecasting and prediction workloads, but comes with platform and library requirements: only functions from RevoScaleR and revoscalepy have C++ implementations.

> [!NOTE]
> This article covers Machine Learning Services in SQL Server, Azure SQL Managed Instance, and Azure SQL Database. `PREDICT` is also available in Azure SQL Edge and Azure Synapse Analytics. For more information, see the [PREDICT T-SQL function](../t-sql/queries/predict-transact-sql.md).

## How native scoring works

Native scoring uses native C++ libraries from Microsoft that can read an already trained model, previously stored in a special binary format or saved to disk as raw byte stream, and generate scores for new data inputs that you provide. Because the model is trained, deployed, and stored, it can be used for scoring without having to call the R or Python interpreter. As such, the overhead of multiple process interactions is reduced, resulting in much faster prediction performance in enterprise production scenarios.

To use native scoring, call the `PREDICT` T-SQL function and pass the following required inputs:

+ A compatible model based on a supported algorithm.
+ Input data, typically defined as a SQL query.

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

## Prerequisites

`PREDICT` is available on all editions of SQL Server 2017 and later on Windows and Linux, Azure SQL Managed Instance, and Azure SQL Database, and is enabled by default. You do not need to install R, Python, or enable additional features.

::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
+ The model must be trained in advance using one of the supported **rx** algorithms listed below, or use an [Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) model format.
::: moniker-end
::: moniker range=">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=sqlallproducts-allversions"
+ The model must be trained in advance using one of the supported **rx** algorithms listed below.
::: moniker-end

+ Serialize the model using [rxSerialize](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R, and [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

<a name="bkmk_native_supported_algos"></a> 

## Supported algorithms

::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
+ Algorithmns using the [Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) model format.
::: moniker-end

+ revoscalepy models

  + [rx_lin_mod](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-lin-mod)
  + [rx_logit](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-logit) 
  + [rx_btrees](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-btrees) 
  + [rx_dtree](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dtree) 
  + [rx_dforest](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dforest) 

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdtree)
  + [rxDForest](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdforest)

If you need to use models from MicrosoftML or microsoftml, use [real-time scoring with sp_rxPredict](real-time-scoring.md).

Unsupported model types include the following types:

+ Models containing other transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR or revoscalepy equivalents
+ PMML models
+ Models created using other open-source or third-party libraries

::: moniker range=">=sql-server-2017||=azuresqldb-mi-current||>=sql-server-linux-2017||=sqlallproducts-allversions"
## Example: PREDICT (T-SQL)

In this example, you create a model, and then call the real-time prediction function from T-SQL.

### Step 1. Prepare and save the model

Run the following code to create the sample database and required tables.

```sql
CREATE DATABASE NativeScoringTest;
GO
USE NativeScoringTest;
GO
DROP TABLE IF EXISTS iris_rx_data;
GO
CREATE TABLE iris_rx_data (
  "Sepal.Length" float not null, "Sepal.Width" float not null
  , "Petal.Length" float not null, "Petal.Width" float not null
  , "Species" varchar(100) null
);
GO
```

Use the following statement to populate the data table with data from the **iris** dataset.

```sql
INSERT INTO iris_rx_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width" , "Species")
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N'iris_data <- iris;'
  , @input_data_1 = N''
  , @output_data_1_name = N'iris_data';
GO
```

Now, create a table for storing models.

```sql
DROP TABLE IF EXISTS ml_models;
GO
CREATE TABLE ml_models ( model_name nvarchar(100) not null primary key
  , model_version nvarchar(100) not null
  , native_model_object varbinary(max) not null);
GO
```

The following code creates a model based on the **iris** dataset and saves it to the table named **models**.

```sql
DECLARE @model varbinary(max);
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N'
    iris.sub <- c(sample(1:50, 25), sample(51:100, 25), sample(101:150, 25))
    iris.dtree <- rxDTree(Species ~ Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, data = iris[iris.sub, ])
    model <- rxSerializeModel(iris.dtree, realtimeScoringOnly = TRUE)
    '
  , @params = N'@model varbinary(max) OUTPUT'
  , @model = @model OUTPUT
  INSERT [dbo].[ml_models]([model_name], [model_version], [native_model_object])
  VALUES('iris.dtree','v1', @model) ;
```

> [!NOTE]
> Be sure to use the [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) function from RevoScaleR to save the model. The standard R `serialize` function cannot generate the required format.

You can run a statement such as the following to view the stored model in binary format:

```sql
SELECT *, datalength(native_model_object)/1024. as model_size_kb
FROM ml_models;
```

### Step 2. Run PREDICT on the model

The following simple PREDICT statement gets a classification from the decision tree model using the **native scoring** function. It predicts the iris species based on attributes you provide, petal length and width.

```sql
DECLARE @model varbinary(max) = (
  SELECT native_model_object
  FROM ml_models
  WHERE model_name = 'iris.dtree'
  AND model_version = 'v1');
SELECT d.*, p.*
  FROM PREDICT(MODEL = @model, DATA = dbo.iris_rx_data as d)
  WITH(setosa_Pred float, versicolor_Pred float, virginica_Pred float) as p;
go
```

If you get the error, "Error occurred during execution of the function PREDICT. Model is corrupt or invalid", it usually means that your query didn't return a model. Check whether you typed the model name correctly, or if the models table is empty.

> [!NOTE]
> Because the columns and values returned by **PREDICT** can vary by model type, you must define the schema of the returned data by using a **WITH** clause.
::: moniker-end

## Next steps

+ [PREDICT T-SQL function](../t-sql/queries/predict-transact-sql.md)