---
title: Native scoring in SQL Server machine learning | Microsoft Docs
description: Generate predictions using the PREDICT T-SQL function, scoring dta inputs against a pre-trained model written in R or Python on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Native scoring using the PREDICT T-SQL function
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Native scoring uses native C++ libraries from Microsoft that can read the model from a special binary format and generate prediction values or *scores* for new input data. This methodology offers the fastest processing speed of forecasting and prediction workloads, but comes with programming requirements so that code executes in closer proximity to the database engine.

Once you have a trained model, you can pass new input data to the function to generate scores. In SQL Server 2017 Windows or Linux, or in Azure SQL Database, you can use the PREDICT function in Transact-SQL to support native scoring. Native scoring uses model already trained, which you can call using T-SQL. 

## How native scoring works

Native scoring uses native C++ libraries from Microsoft that can read an already trained model, stored in a special binary format, and generate scores for new data inputs that you provide. Because the model is trained, published, and stored, it can be used for scoring without having to call the R or Python interpreter. As such, the overhead of multiple process interactions is reduced, resulting in much faster prediction performance in enterprise production scenarios.

To use native scoring, call the scoring function and pass the following required inputs:

+ A compatible model. See the [Requirements](#Requirements) section for details.
+ Input data, typically defined as a SQL query.

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

## Supported platforms

+ SQL Server 2017 Machine Learning Services
    
    Native scoring using PREDICT requires SQL Server 2017. It works on any version of SQL Server 2017, including Linux.

    You can also perform real-time scoring using sp_rxPredict. To use this stored procedure requires that you enable [SQL Server CLR integration](https://docs.microsoft.com/dotnet/framework/data/adonet/sql/introduction-to-sql-server-clr-integration).

+ SQL Server 2016 R Services

   Real-time scoring using sp_rxPredict is possible with SQL Server 2016, and can also be run on Microsoft R Server. This option requires SQLCLR to be enabled, and that you install the Microsoft R Server upgrade.
   For more information, see [Realtime scoring](Real-time-scoring.md)

## Model preparation

+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](#bkmk_native_supported_algos).
+ The model must be saved using the new serialization function provided in Microsoft R Server 9.1.0. The serialization function is optimized to support fast scoring.

## <a name="bkmk_native_supported_algos"></a> Algorithms that support native scoring

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdtree)
  + [rxDForest](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdforest)

If you need to use models from MicrosoftML, use real-time scoring with sp_rxPredict.

## Restrictions

The following model types are not supported:

+ Models containing other, unsupported types of R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR
+ PMML models
+ Models created using other R libraries from CRAN or other repositories
+ Models containing any other R transformation

## Example: Native scoring with PREDICT 

In this example, you create a model, and then call the real-time prediction function from T-SQL.

### Step 1. Prepare and save the model

Run the following code to create the sample database and required tables.

```SQL
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

```SQL
INSERT INTO iris_rx_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width" , "Species")
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N'iris_data <- iris;'
  , @input_data_1 = N''
  , @output_data_1_name = N'iris_data';
GO
```

Now, create a table for storing models.

```SQL
DROP TABLE IF EXISTS ml_models;
GO
CREATE TABLE ml_models ( model_name nvarchar(100) not null primary key
  , model_version nvarchar(100) not null
  , native_model_object varbinary(max) not null);
GO
```

The following code creates a model based on the **iris** dataset and saves it to the table named **models**.

```SQL
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

```SQL
SELECT *, datalength(native_model_object)/1024. as model_size_kb
FROM ml_models;
```

### Step 2. Run PREDICT on the model

The following simple PREDICT statement gets a classification from the decision tree model using the **native scoring** function. It predicts the iris species based on attributes you provide, petal length and width.

```SQL
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

## See also

[Real-time scoring in SQL Server machine learning ](real-time-scoring.md)