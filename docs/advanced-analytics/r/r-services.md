---
title: "Microsoft Machine Learning Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 341e80f5-3b59-4122-bbaa-969d7904297d
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Microsoft Machine Learning Services

The goal of Microsoft Machine Learning Services is to provide an extensible, scalable platform for integrating machine learning tasks and tools with the applications that consume machine learning services. The platform must serve the needs of all users involved in the data development and analytics process, from data scientists, to architects and database administrators.

Key concepts include:

+ High-performance scalable analytics. Write-once, deploy-anywhere solutions
+ Avoid data movement and data risk: bring analytics to the data
+ Let data scientists choose their own tools and languages
+ Integrate the best features of open source with Microsoft's enterprise capabilities
+ Simplify administration
+ Make it easier to deploy and consume predictive models

## Roadmap

In SQL Server 2016, Microsoft launched two server platforms for integrating the popular open source R language with business applications:

+ **SQL Server R Services (In-Database)**, for integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
+ **Microsoft R Server**, for enterprise-level R deployments on Windows and Linux servers

In SQL Server 2017 CTP 2.0, the name has been changed to reflect support for the popular Python language.

+ **SQL Server Machine Learning Services (In-Database)** supports both R and Python.
version-md.md)]
+ **Microsoft Machine Learning Server** supports R and Python deployments on Windows servers, with expansion to other supported platforms planned for late 2017.


## In-Database Analytics with SQL Server

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] brings the compute to the data by allowing R to run on the same computer as the database. It includes the Lacunhpad service, which runs outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process and communicates securely with the R or Python runtimes.

Using SQL Server Machine Learning Services (or SQL Server R Services), you can train models, generate plots, perform scoring, and easily move data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R or Python.

Data scientists who are testing and developing solutions can communicate with the server from a remote development computer to run code on the server, or deploy completed solutions to SQL Server by embedding machine learnign scripts in stored procedures.

When you install machine learning services for SQL Server, you get a distribution of the open source R language, as well as the Microsoft R tools and libraries, which include high-performance, scalable R and Python packages, as well as SQL Server services for faster connectivity with R or Python runtimes.

To get started, see [SQL Server R Services](../../advanced-analytics/r/sql-server-r-services.md).
For walkthroughs and tutorials, see [Machine Learning Tutorials](../tutorials/machine-learning-services-tutorials.md).

> [!NOTE]
> Support for Python is provided only in SQL Server 2017. This is a new feature and still under development.

## Microsoft R Server and Machine Learning Server (Standalone)

This standalone server system supports distributed, scalable R solutions on multiple platforms and using multiple enterprise data sources, including Linux, Hadoop, and Teradata.

For more information, see [Microsoft R Server (MSDN)](https://msdn.microsoft.com/microsoft-r/index).

If you don't need to integrate with SQL Server, you can install R Server to enable rapid development, deployment, and operationalization of machine learning solutions.

If you install Microsoft Machine Learning Server using SQL Server 2017 setup, you can also deploy and consume Python applications.

## Related Technologies

Microsoft provides broad support for machine learning ecosystems, including tools, providers, enhanced R and Python packages, a cloud development and services platform, and an integrated development environment.

### R Tools for Visual Studio

Visual Studio provides a full development environment for the R language. The plug-in includes an editor, interactive window, plotting, debugger, and more. You can use .NET languages from R or invoke R from .NET via open source libraries such as R.NET and rClr.

Visual Studio also has an excellent Python development environment. There's no easier way to work with machine learning languages while continuing to enjoy access to SQL database development tools.

For more information, see:

+ [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/)
+ [Python - Visual Studio](https://www.visualstudio.com/vs/python/)

### Azure Machine Learning

When you create your own workspace in Azure Machine Learning Studio, you'll have access to over 400 preloaded R packages. You can also choose when you create an experiment that uses R, to deploy R using either a standard CRAN R distribution, or Microsoft R Open. You can even create your own R packages and upload them to Azure to run as custom modules.

For more information, see:
+ [Extend your experiment with R](https://docs.microsoft.com/azure/machine-learning/machine-learning-extend-your-experiment-with-r)
+ [Author custom R modules in Azure Machine Learning](https://docs.microsoft.com/azure/machine-learning/machine-learning-custom-r-module)

Many of the algorithms provided in Azure ML are now included in Microsoft Machine Learning Services, as part of the MicrosoftML package. For more information, see [MicrosoftML](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml) on MSDN.

Azure Machine Learning is also a convenient platform if you want to build and train models to deploy as a Web service, and publish solutions to the [Machine Learning Marketplace](http://datamarket.azure.com/browse/data?category=machine-learning).

### Data Science Virtual Machines

You can deploy a pre-installed and pre-configured version of [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)] in Microsoft Azure, making it easy to get started with data exploration and modeling right away on the cloud without setting up a fully configured system on premises.

The Azure Marketplace contains several virtual machines that support data science:

+ The **Microsoft Data Science Virtual Machine** is configured with Microsoft R Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server Express edition.
+ **Microsoft R Server 2016 for Linux** contains the latest version of R Server (version 9.0.1). Separate VMs are available for CentOS version 7.2 and  Ubuntu version 16.04.
+ The **R Server Only SQL Server 2016 Enterprise** virtual machine includes a standalone installer for R Server 9.0.1 that supports the new Modern Software Lifecycle licensing model.

## See Also

[Getting Started with SQL Server R Services](../../advanced-analytics/r/getting-started-with-sql-server-r-services.md)

[Getting started with Microsoft R Server](../../advanced-analytics/r/getting-started-with-microsoft-r-server-standalone.md)

[Install SQL Server Database Engine](../../database-engine/install-windows/install-sql-server-database-engine.md)

