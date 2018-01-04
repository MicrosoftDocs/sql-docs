---
title: "RevoScaleR | Microsoft Docs"
ms.custom: ""
ms.date: "11/29/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---

# RevoScaleR

RevoScaleR is a package of machine learning functions, provided by Microsoft, that supports data science at scale.

+ Functions support data import, data transformation, summarization, visualization, and analysis.

+ _At scale_ means that operations can be performed against very large datasets, in parallel, and on distributed file systems. Algorithms can operate over datasets that do not fit in memory, by using chunking and by reassembling results when operations are complete.

+ RevoScaleR provides many improvements over open source R functions. There are RevoScaleR functions corresponding to many of the most popular base R functions. RevoScaleR functions are denoted with an **rx** or **Rx** prefix to make them easy to identify.

+ RevoScaleR serves as a platform for distributed data science. For example, you can use the RevoScaleR compute contexts and transformations with the state-of-the-art algorithms in [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-the-microsoftml-package). You can also use [rxExec](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxexec) to run base R functions in parallel.

For examples of RevoScaleR in action, see these blogs: 

+ [Build and deploy a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction/)

+ [One million predictions per second with Machine Learning Server](https://blogs.msdn.microsoft.com/mlserver/2017/10/15/1-million-predictionssec-with-machine-learning-server-web-service/)

+ [Predicting taxi tips using MicrosoftML](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2017/01/17/predicting-nyc-taxi-tips-using-microsoftml/)

+ [Performance optimization with rxExec](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2016/11/14/performance-optimization-when-using-rxexec-to-parallelize-algorithms/)

## How to get RevoScaleR

The RevoScaleR package for R is installed for free in Microsoft R Client. If you have Machine Learning Server or are using R in SQL Server, RevoScaleR is included by default.

If you are using Python, the [revoscalepy](../python/what-is-revoscalepy.md) package provides equivalent functionality.

> [!IMPORTANT]
> The RevoScaleR package cannot be downloaded or used independently of the products and services that provide it.

## Use RevoScaleR in SQL Server

These tutorials and samples demonstrate how to use RevoScaleR functions to get data from SQL Server, build models, and save models to a database for scoring.

+ [Learn to use compute contexts](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [R for SQL developers: Train and operationalize a model](../tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [Microsoft product samples on GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples)

## Learn more about RevoScaleR

These tutorials demonstrate the use of RevoScaleR in other compute contexts supported by [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), including Hadoop.

+ [What is RevoScaleR?](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-revoscaler)

+ [Explore RevoScaleR in 25 functions](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler)

+ [RevoScaleR package reference](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler)

