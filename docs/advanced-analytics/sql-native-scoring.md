---
title: Native scoring using PREDICT T-SQL statement - SQL Server Machine Learning Services
description: Generate predictions using the PREDICT T-SQL function, scoring dta inputs against a pre-trained model written in R or Python on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---

# Native scoring using the PREDICT T-SQL function
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Native scoring uses [PREDICT T-SQL function](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql) and the native C++ extension capabilities in SQL Server 2017 to generate prediction values or *scores* for new data inputs in near-real-time. This methodology offers the fastest possible processing speed of forecasting and prediction workloads, but comes with platform and library requirements: only functions from RevoScaleR and revoscalepy have C++ implementations.

Native scoring requires that you have an already trained model. In SQL Server 2017 Windows or Linux, or in Azure SQL Database, you can call the PREDICT function in Transact-SQL to invoke native scoring against new data that you provide as an input parameter. The PREDICT function returns scores over data inputs you provide.

## How native scoring works

Native scoring uses native C++ libraries from Microsoft that can read an already trained model, previously stored in a special binary format or saved to disk as raw byte stream, and generate scores for new data inputs that you provide. Because the model is trained, published, and stored, it can be used for scoring without having to call the R or Python interpreter. As such, the overhead of multiple process interactions is reduced, resulting in much faster prediction performance in enterprise production scenarios.

To use native scoring, call the PREDICT T-SQL function and pass the following required inputs:

+ A compatible model based on a supported algorithm.
+ Input data, typically defined as a SQL query.

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

## Prerequisites

PREDICT is available on all editions of SQL Server 2017 database engine and enabled by default, including SQL Server 2017 Machine Learning Services on Windows, SQL Server 2017 (Windows), SQL Server 2017 (Linux), or Azure SQL Database. You do not need to install R, Python, or enable additional features.

+ The model must be trained in advance using one of the supported **rx** algorithms listed below.

+ Serialize the model using [rxSerialize](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R, and [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

<a name="bkmk_native_supported_algos"></a> 

## Supported algorithms

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

## Next steps

For a complete solution that includes native scoring, see these samples from the SQL Server development team:

+ Deploy your ML script: [Using a Python model](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/step/3.html)
+ Deploy your ML script: [Using an R model](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction/step/3.html)