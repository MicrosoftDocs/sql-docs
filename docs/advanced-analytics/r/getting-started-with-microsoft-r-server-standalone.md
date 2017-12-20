---
title: "Getting Started with Microsoft R Server (Standalone) | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 52347d0d-ce60-4bb8-98d2-6163e87716b0
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Getting Started with Machine Learning Server (Standalone)
 
In SQL Server 2016, Microsoft R Server (Standalone) helped bring the popular open source R language into the enterprise, to enable high-performance analytics solutions and integration with other business applications.  

In SQL Server 2017, the name was changed to Machine Learning Server (standalone) to reflect the addition of support for the Python language. Both versions are available for free to users of Enterprise Edition or Software Assurance.

This article provides a high-level overview of how you can use Machine Learning Server (or R Server), and how to get started with installation and samples.

## Why use a standalone server for machine learning

If you don't need to integrate your machine learning solutions with SQL Server data, Machine Learning Server gives you the same distributed, scalable support for R and Python, and moreover supports deployment of solutions to Hadoop, Spark, or Linux.

Machine Learning Server also includes pre-trained models for image analysis and sentiment analysis, which you can immediately use in applications.

The operationalization features of Machine Learning Server support deploying and distributing R and Python solutions by using web services, with remote execution, clustered topologies for Spark and Hadoop MapReduce, and support for Windows or Linux.

**SQL Server 2016**

+ Install Microsoft R Server (Standalone) from SQL Server 2016 setup.

    This option creates a standalone server that is entirely independent from R Services (In-Database). This version supports R only. Setup of a standalone server is included in your SQL Server Enterprise Edition support policy. For more information, see [Create a Standalone R Server](../../advanced-analytics/r/create-a-standalone-r-server.md).

+ Install R Server using the separate Windows installer.

    This installer creates a brand new instance of Microsoft R Server that uses the Microsoft Modern Software Lifecycle support policy. You can also install R Server for Linux, Cloudera, Teradata, and Hadoop.
    
    For more information, see [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

**SQL Server 2017**

+ Install Machine Learning Server (Standalone) from SQL Server 2017 setup. 

    This option creates a standalone server to support operationalization of machine learning in R, Python, or both. Setup of a standalone server is included in your SQL Server Enterprise Edition support policy. For more information, see [Create a Standalone R Server](../../advanced-analytics/r/create-a-standalone-r-server.md).  

+ Use the new standalone Windows installer.

    This installation method creates a new instance of Machine Learning Server that uses the Microsoft Modern Software Lifecycle support policy. You can also install Machine Learning Server on Hadoop, Spark, or Linux at no additional cost.
    
    For more information, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

**Upgrade an existing server**

+ If you have an existing server or instance that you want to upgrade, download and run the Windows-based installer to get the updates. 

    For more information, see [Using SqlBindR to upgrade an instance](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## Start using Machine Learning Server

 After you have set up the server components and configured your IDE to use the Machine Learning Server binaries, you can begin developing your solution using the new APIs, such as the RevoScaleR and revoscalepy, MicrosoftML, and olapR.
    
To get started, see these guides:

+ [Solution templates](https://docs.microsoft.com/machine-learning-server/r/sample-solutions)

    Theses samples are solutions that demonstrate how to apply machine learning in specific industries. Current scenarios are in retail, finance, health care, and marketing.

+ [Explore R and ScaleR in 25 Functions](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler): Explore this collection of distributable analytical functions that provide high performance and scaling to R solutions. Includes parallelizable versions of many of the most popular R modeling packages, such as k-means clustering, decision trees, and decision forests, and tools for data manipulation.

- [MicrosoftML](https://msdn.microsoft.com/library/mt790482.aspx)

    The MicrosoftML package is a set of new machine learning algorithms and transformations developed at Microsoft that are fast and scalable. YOu can use it in either R or Python. For more information, see [MicrosoftML for Python](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) and [MicrosoftML for R](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package).

## See also

[Getting Started with SQL Server Machine Learning Services](../../advanced-analytics/r/getting-started-with-sql-server-r-services.md)