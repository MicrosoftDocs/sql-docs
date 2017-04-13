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

# What&#39;s New in Machine Learning with SQL Server

In SQL Server 2016, Microsoft introduced SQL Server R Services, a feature that supports enterprise-scale data science by integrating the R language with SQL Server database engine.  

In SQL Server vNext, machine learning becomes even more powerful, with addition of support for the popular Python language. To reflect the support for multiple languages, as of CTP 2.0, SQL Server R Services has also been renamed as **Machine Learning Services (In-Database)**.

## What's New in SQL Server vNext CTP 2.0

SQL Server vNext includes many new features to make it easier to build and deploy machine learning solutions that use SQL Server data. 

### Python integration in SQL Server

You can now run Python in SQL Server, using stored procedures or remote compute contexts. 

Machine Learning Services includes the RevoScalePy libraries, which have all the distributed algorithms and compute contexts supported in RevoScaleR.

For more information, see these topics:

+ [Set up Python in Machine Learning Services](../advanced-analytics/python/setup-python-machine-learning-services.md)
+ Demo 

### Pleasingly parallel compute for distributed models

Often the data scientist needs to efficiently train a large number of small models over subsets of a very large dataset. For example, you might have collected a large dataset covering multiple products or devices over many days, but want to create individual models that are tailored to the product, device, or certain periods. 

You can easily create multiple models in parallel by using the new **rxExecBy** function, available in SQL Server vNext CTP 2.0 and Microsoft R server 9.1.0. The function accepts a dataset containing ungrouped and unordered data, and lets you partition it by a single entity for training and model building. For example, you could partition by product, and then process the entire dataset in parallel. The outcome is multiple models, each model trained on the appropriate subset of data for each product. 

Supported compute contexts include RxSpark and RxInSQLServer.

For more information, see [Create Multiple Models using rxExecBy](../advanced-analytics/r/creating-multiple-models-using-rxexecby.md).

## Other R Features and Improvements  

R is the most popular programming language for advanced analytics, and offers an incredibly rich set of packages and a vibrant and fast-growing developer community. 

This section highlights the additions in SQL Server vNext that improve the scalability and performance of R.

+ Upgrade your R experience 

  The RevoScaleR package is included in SQL Server 2016, SQL Server vNext, and Microsoft R Server. It includes transforms and algorithms that support distributed or parallel processing, and multiple compute contexts. 
  
  If you installed an earlier version of RevoScaleR with SQL Server 2016, you can now upgrade to the very latest version by switching your server to use the Modern Lifecycle policy. By doing so you can take advantage of a faster release cycle for R and automatically upgrade all R components. For more information, see [Microsoft R Server 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new).  

+ MicrosoftML 

   MicrosoftML is a machine learning package for R from the Microsoft Data Science team. MicrosoftML brings increased speed, performance and scale for handling a large corpus of text data and high-dimensional categorical data in R models with just a few lines of code. It also includes five fast, highly accurate learners that are included in Azure Machine Learning. 
   
   For more information, see [Using the MicrosoftML Package in SQL Server R Services](using-the-microsoftml-package.md).
   
+ Improved package management for data scientists

  New package install and uninstall functions let you easily install and update R packages used in SQL Server from a client computer. 
  
  Using the roles included in SQL Server for managing permissions associated with packages, the DBA can also control packages at the database level, allow users to install their own packages, or create groups of users who can access shared packages. 
  
  In CTP 2.0, new functions have been added that let you easily back up and restore the package collections associated with users when you move or restore a database.
  
  For more information, see [R Package Management for SQL Server R Services](../advanced-analytics/r/r-package-management-for-sql-server-r-services.md). 


## See Also  



