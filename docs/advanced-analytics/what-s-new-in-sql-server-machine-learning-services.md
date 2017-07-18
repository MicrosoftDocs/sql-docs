---
title: "What&#39;s New in Machine Learning Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/14/2017"
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
# What's new in Machine Learning Services in SQL Server

In SQL Server 2016, Microsoft introduced SQL Server R Services, a feature that supports enterprise-scale data science, by integrating the R language with the SQL Server database engine.

In SQL Server 2017, machine learning becomes even more powerful, with addition of support for the popular Python language. Along with the support for new languages comes a new name: **Machine Learning Services (In-Database)**.

## What's new in SQL Server 2017 Release Candidate 1

SQL Server 2017 builds on Microsoft's commitment to making it easy to build and deploy machine learning solutions, with the following new features:

- Improved and expanded Python libraries
- Updates to real-time scoring, plus a new native scoring function
- Updates to Microsoft R
- Installation of pre-trained models
- New package management capabilities for the DBA

### Improved Python integration in SQL Server

Support for Python in SQL Server was introduced in CTP 2.0.  When you select Python as the language to install with SQL Server, you get the **revoscalepy** module, which supports the same scalable, distributed algorithms and compute contexts that are provided in RevoScaleR.

In this release, the **revoscalepy** library is joined by **MicrosoftML for Python**, which provides fast, scalable machine learning algorithms and data transformations that run on the same compute contexts and data sources supported by revoscalepy.

For more information, see these topics:

+ [Set up Python in Machine Learning Services](../advanced-analytics/python/setup-python-machine-learning-services.md)

+ [revoscalepy function reference](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package)

+ [MicrosoftML for Python](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package)

### Native scoring and real-time scoring

Real-time scoring is supported for scoring in SQL Server and in Machine Learning Server (Standalone). This feature relies on native C++ libraries to read a model stored in an optimized binary format. From the model, the function can generate predictions without having to call the R runtime. This makes batch scoring much faster.

Additionally, this release of SQL Server 2017 includes a native T-SQL function for real-time scoring that can be run on any edition of SQL Server, even on Linux. The function is available by default, and requires no installation of R or extra configuration. This means you can train a model elsewhere, save it in SQL Server, and then perform scoring without ever calling R.

For more information, see these articles:

+ [Real-time scoring](real-time-scoring.md)
+ [Native scoring](sql-native-scoring.md)
+ [How to perform real-time scoring or native scoring](r/how-to-real-time-scoring.md)

## Other updates

This section lists features that were released previously, but that have been updated in this release.

### Improved package management for data scientists

To provide better support for databases administrators who must manage R package libraries, SQL Server now supports the CREATE EXTERNAL LIBRARY command.

For more information, see [R Package Management for SQL Server R Services](../advanced-analytics/r/r-package-management-for-sql-server-r-services.md).

### Upgrade your R experience with pre-trained models

The RevoScaleR package is included in SQL Server 2016, SQL Server 2017, and Microsoft R Server. It includes transforms and algorithms that support distributed or parallel processing, and multiple compute contexts.

If you installed an earlier version of RevoScaleR with SQL Server 2016, you can now upgrade to the latest version by switching your server to use the Modern Lifecycle policy. By doing so, you can take advantage of a faster release cycle for R and automatically upgrade all R components. For more information, see [Microsoft R Server 9.0.1](https://docs.microsoft.com/r-server/whats-new-in-r-server).

Additionally, when you install the new R components, you can get a collection of pre-trained models in binary format. These models support machine learning in scenarios such as image recognition, where it might be difficult for customers to find large datasets to train a model. After you install one of the pre-trained models, you can use it for prediction on your own data without the time and expense involved in training such a large and complex model.

For more information, see [Install pre-trained models in SQL Server](install-pretrained-models-sql-server.md)

> [!IMPORTANT]
> 
> Machine learning services, including use of R or Python, are currently not supported when running SQL Server on Linux. Look for changes in a later release. However, native scoring using the PREDICT function is currently supported in the Linux edition.
