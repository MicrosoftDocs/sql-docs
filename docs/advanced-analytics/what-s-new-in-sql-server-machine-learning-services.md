---
title: "What&#39;s New in Machine Learning Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/31/2017"
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

Catch the latest announcement here! [Python in SQL Server 2017: enhanced in-database machine learning](https://blogs.technet.microsoft.com/dataplatforminsider/2017/04/19/python-in-sql-server-2017-enhanced-in-database-machine-learning/)

## What's new in SQL Server 2017 Release Candidate 2

Microsoft Machine Learning Server in SQL Server now provides comprehensive support for building and deploying machine learning solutions. Here are the highlights of this release:

> [!IMPORTANT]
> 
> Machine learning services, including use of R or Python, are currently not supported when running SQL Server on Linux, or in Azure SQL database. Look for changes in a later release.
> 
> However, native scoring using the PREDICT function is currently supported in the Linux edition. 
 
### In-database Python integration

You can run Python in stored procedures, or execute Python remotely using the SQL Server computer as the compute context. This integration opens up new avenues for the vast community of Python developers and data scientists to use the power of SQL Server, and to explore innovations from Microsoft such as **revoscalepy** and **microsoftml**.

SQL Server developers gain access to the extensive Python libraries from the open source ecosystem, including popular frameworks such as scikit-learn, Tensorflow, Caffe and Theano/Keras. 

But running Python in-database isn't just for machine learning; there are a myriad of other potential applications for integrating Python with SQL, leveraging the strengths of the respective languages to deliver more intelligent, powerful solutions.

+ **revoscalepy**

    This release includes the final version of **revoscalepy**, which supplies Pythonic equivalents of the scalable, streaming algorithms in RevoScaleR. You can create Python models for linear and logistic regressions, decision trees, boosted trees, and random forests, all parallelizable and capable of being run in remote compute contexts.

    For more information, see [What is revoscalepy](python/what-is-revoscalepy.md).

+ Remote compute contexts for Python

    This release supports use of multiple data sources and remote compute contexts. The data scientist or developer can execute Python code on a remote SQL Server, to explore data and build models without moving data around. Use of remote compute contexts requires **revoscalepy**.

+ Python support in Microsoft Machine Learning Server (Standalone)

    SQL Server 2017 includes the option to install a standalone version of the Python and R platforms. By using Machine Learning Server, you can distribute and scale R or Python code without using SQL Server.

    For an example of Python use in Microsoft Machine Learning Server, see [Publish and consume Python code](python/publish-consume-python-code.md).

### New algorithms

The **MicrosoftML** package for both R and Python contains state-of-the-art machine learning algorithms and data transformation that can be scaled or run in remote compute contexts. Algorithms include customizable deep neural networks, fast decision trees and decision forests, linear regression, and logistic regression.

The MicrosoftML package comes with both R and Python interfaces, and is based on Microsoft Machine Learning Server version 9.2.0.

For more information, see [Introduction to MicrosoftML](using-the-microsoftml-package.md) and [microsoftml for Python](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package).

### Operationalization

This release contains multiple options and features to help you deploy and distribute machine learning tasks:

+ Operationalization with T-SQL

    The integration of Python with T-SQL means that you can call any Python code using `sp_execute_external_script`. This secure infrastructure enables enterprise-grade deployment of Python models and scripts that can be called from an application using a simple stored procedure. Additional performance is by streaming data from SQL to Python processes and MPI ring parallelization.

+ **mrsdeploy** for Python

    The **mrsdeploy** package for Microsoft R Server now supports deployment of Python models and scripts as web services, and is available as an option in Machine Learning Server (Standalone). For an example of how it works, see [Publish and consume Python code](python/publish-consume-python-code.md).

+ Performance

    Microsoft has pushed the boundaries of performance for scoring. With in-database scoring, we processed a million rows per second using R models. In this release, new features for **realtime scoring** and **native scoring** support better performance in single-row scoring and small batches as well. 

### Realtime scoring and native scoring

Realtime scoring relies on native C++ libraries to read a model stored in an optimized binary format, and then generate predictions without having to call the R runtime. This makes scoring operations much faster.

Additionally, this release of SQL Server 2017 includes a native T-SQL function for fast scoring that can be run on any edition of SQL Server, even on Linux. The function requires no installation of R or extra configuration. This means you can train a model elsewhere, save it in SQL Server, and then perform scoring without ever calling R. This feature is referred to as _native scoring_.

  - Native scoring is available only in SQL Server 2017. It uses a T-SQL function that can run in any edition of SQL Server, including Linux.
 - Realtime scoring is supported in SQL Server 2017, and in Microsoft Machine Learning Server. You can runa  stored procedure or perform realtime scoring from R code.
 - Realtime scoring is also available for SQL Server 2016, if the instance is upgraded to the latest release of Microsoft R Server.

For more information, see these articles:

 + [Realtime scoring](real-time-scoring.md)
 + [Native scoring](sql-native-scoring.md)
 + [How to perform realtime scoring or native scoring](r/how-to-do-realtime-scoring.md)

### Upgrade your ML experience and get pre-trained models

If you installed an earlier version of SQL Server 2016 R Services, you can now upgrade to the latest version by switching your server to use the Modern Lifecycle policy. By doing so, you can take advantage of a faster release cycle for R and automatically upgrade all R components. For more information, see [Microsoft R Server 9.0.1](https://docs.microsoft.com/r-server/whats-new-in-r-server).

The installer also offers the option to install a collection of pre-trained models in binary format. These models support machine learning in scenarios such as image recognition, where it might be difficult for customers to find large datasets to train a model. After you install one of the pre-trained models, you can use it for prediction on your own data without the time and expense involved in training such a large and complex model.

For more information, see [Install pre-trained models in SQL Server](r/install-pretrained-models-sql-server.md)

### Package management

This release includes many improvements in package management for SQL Server. These include:

- database roles to help the DBA manage and audit permissions
- the CREATE EXTERAL LIBRARY statement in T-SQL
- a rich set of R functions to help install, remove, or list packages owned by users.

For more information, see [Package management](r/r-package-management-for-sql-server-r-services.md).

### Get started

+ [Set up Python in SQL Server Machine Learning Services](../advanced-analytics/python/setup-python-machine-learning-services.md)

+ [Set up R in SQL Server Machine Learning Services](r/set-up-sql-server-r-services-in-database.md)

+ [Machine learning tutorials](tutorials/machine-learning-services-tutorials.md)