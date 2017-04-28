---
title: "What&#39;s New in SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6aff043a-8b37-4f3f-9827-10a671e1ad1c
caps.latest.revision: 36
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# What's New in Machine Learning with SQL Server

In SQL Server 2016, Microsoft introduced SQL Server R Services, a feature that supports enterprise-scale data science by integrating the R language with SQL Server database engine.

In SQL Server 2017, machine learning becomes even more powerful, with addition of support for the popular Python language. To reflect the support for multiple languages, as of CTP 2.0, SQL Server R Services has also been renamed as **Machine Learning Services (In-Database)**.

## What's New in SQL Server 2017 CTP 2.0

SQL Server 2017 includes many new features to make it easier to build and deploy machine learning solutions that use SQL Server data.

> [!IMPORTANT]
> 
> Machine learning services, including use of R or Python, are currently not supported when running SQL Server on Linux. This feature will be added in a later release.

### Python integration in SQL Server

You can now run Python in SQL Server, using stored procedures or remote compute contexts.

Machine learning with Python includes the **revoscalepy** module, which includes  subset of the distributed algorithms and compute contexts supported in RevoScaleR. More algorithms are transformations will be added.

For more information, see these topics:

+ [Set up Python in Machine Learning Services](../advanced-analytics/python/setup-python-machine-learning-services.md)


### "Pleasingly parallel" compute for distributed models

Often the data scientist needs to efficiently train a large number of small models over subsets of a very large dataset. For example, you might have collected a large dataset covering multiple products or devices over many days, but want to create individual models that are tailored to the product, device, or certain periods.

You can easily create multiple models in parallel from R, by using the new **rxExecBy** function. The function accepts a dataset containing ungrouped and unordered data, and lets you partition it by a single entity for training and model building. For example, you could partition by product, and then process the entire dataset in parallel. The outcome is multiple models, each model trained on the appropriate subset of data for each product.

The rxExecBy function is available in SQL Server 2017 CTP 2.0 and Microsoft R Server 9.1.0. Supported compute contexts include RxSpark and RxInSQLServer.

For more information, see [Create Multiple Models using rxExecBy](../advanced-analytics/r/creating-multiple-models-using-rxexecby.md).


### mrsdeploy for Microsoft Machine Learning Server

When you install Microsoft Machine Learning Server by using SQL Server setup, you'll now get support for deploying and consuming models using web services, using the mrsdeploy package and related tools.

For more information, see [Deploying and Consuming Analytics](../advanced-analytics/operationalization-with-mrsdeploy.md).

## Other Updates

This section lists features that were released previously, but that have been updated in CTP 2.0.

### Improved package management for data scientists

SQL Server now provides roles for managing permissions associated with packages. The DBA can control packages at the database level and assign users the right to install their own packages, or share packages with other users. Users who belong to these roles can install and uninstall R packages on the SQL Server computer from a remote development client, without having to go through the database administrator each time.

In CTP 2.0, new functions have been added that let you easily back up and restore the package collections associated with users when you move or restore a database.

For more information, see [R Package Management for SQL Server R Services](../advanced-analytics/r/r-package-management-for-sql-server-r-services.md). 

### Upgrade your R experience

The RevoScaleR package is included in SQL Server 2016, SQL Server 2017, and Microsoft R Server. It includes transforms and algorithms that support distributed or parallel processing, and multiple compute contexts.
  
If you installed an earlier version of RevoScaleR with SQL Server 2016, you can now upgrade to the very latest version by switching your server to use the Modern Lifecycle policy. By doing so you can take advantage of a faster release cycle for R and automatically upgrade all R components. For more information, see [Microsoft R Server 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new).

In CTP 2.0, Microsoft R components are upgraded to version 9.1.0.

### New functions and features in MicrosoftML

MicrosoftML is a machine learning package for R from the Microsoft Data Science team. MicrosoftML brings increased speed, performance and scale for handling a large corpus of text data and high-dimensional categorical data in R models with just a few lines of code. It also includes five fast, highly accurate learners that are included in Azure Machine Learning.
   
In CTP 2.0, MicrosoftML includes new image and test featurization functions, as well as support for parallelizable models with rxExecby.

For more information, see the [MicrosoftML Package Reference](https://msdn.microsoft.com/en-us/microsoft-r/microsoftml/microsoftml).


