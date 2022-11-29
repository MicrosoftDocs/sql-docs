---
title: Native scoring with T-SQL PREDICT
titleSuffix: SQL machine learning
description: Learn how to use native scoring with the PREDICT T-SQL function to generate prediction values for new data inputs in near-real-time. 
ms.service: sql
ms.subservice: machine-learning
ms.date: 05/27/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---

# Native scoring using the PREDICT T-SQL function with SQL machine learning

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa.md)]

Learn how to use native scoring with the [PREDICT T-SQL function](../../t-sql/queries/predict-transact-sql.md) to generate prediction values for new data inputs in near-real-time. Native scoring requires that you have an already-trained model.

The `PREDICT` function uses the native C++ extension capabilities in [SQL machine learning](../index.yml). This methodology offers the fastest possible processing speed of forecasting and prediction workloads and support models in [Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) format or models trained using the [RevoScaleR](../r/ref-r-revoscaler.md) and [revoscalepy](../python/ref-py-revoscalepy.md) packages.

## How native scoring works

Native scoring uses libraries that can read models in ONNX or a predefined binary format, and generate scores for new data inputs that you provide. Because the model is trained, deployed, and stored, it can be used for scoring without having to call the R or Python interpreter. This means that the overhead of multiple process interactions is reduced, resulting in faster prediction performance.

To use native scoring, call the `PREDICT` T-SQL function and pass the following required inputs:

+ A compatible model based on a supported model and algorithm.
+ Input data, typically defined as a T-SQL query.

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

## Prerequisites

`PREDICT` is available on:

+ All editions of SQL Server 2017 and later on Windows and Linux
+ Azure SQL Managed Instance
+ Azure SQL Database
+ Azure SQL Edge
+ Azure Synapse Analytics

The function is enabled by default. You do not need to install R or Python, or enable additional features.

## Supported models

The model formats supported by the `PREDICT` function depends on the SQL platform on which you perform native scoring. See the table below to see which model formats are supported on which platform.

| Platform | ONNX model format | RevoScale model format |
|-|-|-|
| SQL Server | No | Yes |
| Azure SQL Managed Instance | Yes | Yes |
| Azure SQL Database | No | Yes |
| Azure SQL Edge | Yes | No |
| Azure Synapse Analytics | Yes | No |

::: moniker range="=azuresqldb-mi-current||=azure-sqldw-latest"
### ONNX models

The model must be in an [Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) model format.
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current||=azuresqldb-current"
### RevoScale models

The model must be trained in advance using one of the supported **rx** algorithms listed below using the [RevoScaleR](../r/ref-r-revoscaler.md) or [revoscalepy](../python/ref-py-revoscalepy.md) package.

Serialize the model using [rxSerialize](/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R, and [rx_serialize_model](/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

<a name="bkmk_native_supported_algos"></a> 

#### Supported RevoScale algorithms

The following algorithms are supported in revoscalepy and RevoScaleR.

+ revoscalepy algorithms

  + [rx_lin_mod](/machine-learning-server/python-reference/revoscalepy/rx-lin-mod)
  + [rx_logit](/machine-learning-server/python-reference/revoscalepy/rx-logit) 
  + [rx_btrees](/machine-learning-server/python-reference/revoscalepy/rx-btrees) 
  + [rx_dtree](/machine-learning-server/python-reference/revoscalepy/rx-dtree) 
  + [rx_dforest](/machine-learning-server/python-reference/revoscalepy/rx-dforest) 

+ RevoScaleR algorithms

  + [rxLinMod](/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](/r-server/r-reference/revoscaler/rxdtree)
  + [rxDForest](/r-server/r-reference/revoscaler/rxdforest)

If you need to use an algorithms from MicrosoftML or microsoftml, use [real-time scoring with sp_rxPredict](../predictions/real-time-scoring.md).

Unsupported model types include the following types:

+ Models containing other transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR or revoscalepy equivalents
+ PMML models
+ Models created using other open-source or third-party libraries
::: moniker-end

## Examples
::: moniker range="=azuresqldb-mi-current||=azure-sqldw-latest"
### PREDICT with an ONNX model

This example shows how to use an ONNX model stored in the `dbo.models` table for native scoring.

```sql
DECLARE @model VARBINARY(max) = (
        SELECT DATA
        FROM dbo.models
        WHERE id = 1
        );

WITH predict_input
AS (
    SELECT TOP (1000) [id]
        , CRIM
        , ZN
        , INDUS
        , CHAS
        , NOX
        , RM
        , AGE
        , DIS
        , RAD
        , TAX
        , PTRATIO
        , B
        , LSTAT
    FROM [dbo].[features]
    )
SELECT predict_input.id
    , p.variable1 AS MEDV
FROM PREDICT(MODEL = @model, DATA = predict_input, RUNTIME=ONNX) WITH (variable1 FLOAT) AS p;
```

> [!NOTE]
> Because the columns and values returned by **PREDICT** can vary by model type, you must define the schema of the returned data by using a **WITH** clause.
::: moniker-end

::: moniker range=">=sql-server-2017||=azuresqldb-mi-current||>=sql-server-linux-2017"
### PREDICT with RevoScale model

In this example, you create a model using **RevoScaleR** in R, and then call the real-time prediction function from T-SQL.

#### Step 1. Prepare and save the model

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
> Be sure to use the [rxSerializeModel](/machine-learning-server/r-reference/revoscaler/rxserializemodel) function from RevoScaleR to save the model. The standard R `serialize` function cannot generate the required format.

You can run a statement such as the following to view the stored model in binary format:

```sql
SELECT *, datalength(native_model_object)/1024. as model_size_kb
FROM ml_models;
```

#### Step 2. Run PREDICT on the model

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

+ [PREDICT T-SQL function](../../t-sql/queries/predict-transact-sql.md)
+ [SQL machine learning documentation](../index.yml)
+ [Machine learning and AI with ONNX in SQL Edge](/azure/azure-sql-edge/onnx-overview)
+ [Deploy and make predictions with an ONNX model in Azure SQL Edge](/azure/azure-sql-edge/deploy-onnx)
+ [Score machine learning models with PREDICT in Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-predict)
