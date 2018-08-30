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

Once you have a pre-trained model, you can pass new input data to the function to generate prediction values or *scores*. In SQL Server 2017 Windows or Linux, or in Azure SQL Database, you can use the PREDICT function in Transact-SQL to support native scoring. It requires only that you have a model already trained, which you can call using T-SQL. 

+ How it works
+ Supported platforms and requirements



## How native scoring works

Native scoring uses native C++ libraries from Microsoft that can read the model from a special binary format and generate scores. Because a model can be published and used for scoring without having to call the R interpreter, the overhead of multiple process interactions is reduced. Hence, native scoring supports much faster prediction performance in enterprise production scenarios.

To generate scores using this library, you call the scoring function, and pass the following required inputs:

+ A compatible model. See the [Requirements](#Requirements) section for details.
+ Input data, typically defined as a SQL query

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

For code samples, along with instructions on how to prepare the models in the required binary format, see this article:

+ [How to perform real-time scoring](r/how-to-do-realtime-scoring.md)

For a complete solution that includes native scoring, see these samples from the SQL Server development team:

+ Deploy your ML script: [Using a Python model](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/step/3.html)
+ Deploy your ML script: [Using an R model](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction/step/3.html)

## Requirements

Supported platforms are as follows:

+ SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)
    
    Native scoring using PREDICT requires SQL Server 2017.
    It works on any version of SQL Server 2017, including Linux.

    You can also perform real-time scoring using sp_rxPredict. To use this stored procedure requires that you enable [SQL Server CLR integration](https://docs.microsoft.com/dotnet/framework/data/adonet/sql/introduction-to-sql-server-clr-integration).

+ SQL Server 2016

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

## See also

[Real-time scoring in SQL Server machine learning ](real-time-scoring.md)