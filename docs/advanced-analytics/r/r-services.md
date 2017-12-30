---
title: "Microsoft Machine Learning Services | Microsoft Docs"
ms.date: "11/09/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology:
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 341e80f5-3b59-4122-bbaa-969d7904297d
caps.latest.revision: 23
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "On Demand"
---
# Microsoft Machine Learning Services

The goal of Microsoft Machine Learning Services is to provide an extensible, scalable platform for integrating machine learning tasks and tools with the applications that consume machine learning services. The platform must serve the needs of all users involved in the data development and analytics process, from data scientists, to architects and database administrators.

Key benefits include:

+ Scalable analytics
+ Multiple platforms and compute contexts, for "code once, deploy anywhere" solutions
+ Avoids data movement and data risk by bringing analytics to the data
+ Data scientists can choose their own tools and languages
+ Integrates the best features of open source with Microsoft's enterprise capabilities
+ Simplified administration
+ Easy to deploy and consume predictive models

## In-database analytics with SQL Server

In SQL Server 2016, Microsoft launched two server platforms for integrating the popular open source R language with business applications:

+ **SQL Server R Services (In-Database)**, for integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
+ **Microsoft R Server**, for enterprise-level R deployments on Windows and Linux servers

In SQL Server 2017, the name has been changed to reflect support for the popular Python language.

+ **SQL Server Machine Learning Services (In-Database)** supports both R and Python for in-database analytics.
+ **Microsoft Machine Learning Server** supports R and Python deployments on Windows, Linux, and HDInsight Spark and Hadoop clusters.

### Benefits

Microsoft Machine Learning Services brings the compute to the data by allowing R to run on the same computer as the database. It includes the Launchpad service, which runs outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process and communicates securely with the R or Python runtime.

Using SQL Server Machine Learning Services, you can train models, generate plots, perform scoring, and securely move data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R or Python.

Data scientists who are testing and developing solutions can send scripts from a remote development computer and run their code on the server without moving data. Developers can deploy completed solutions to SQL Server by embedding machine learning code in SQL stored procedures.

When you install machine learning for SQL Server, you get a distribution of the open source R or Python language, as well as the scalable R and Python libraries provided by Microsoft. The SQL Server database engine also includes new components designed to bolster connectivity and ensure faster, more secure communication with external languages such as R or Python.

### Where to get it

To get started, see these resources:

+ [SQL Server R Services](sql-server-r-services.md)
+ [SQL Server Python Services](../python/sql-server-python-services.md)
+ [Machine Learning Tutorials](../tutorials/machine-learning-services-tutorials.md)

> [!NOTE]
> Support for Python is provided only in SQL Server 2017. 

## Machine Learning Server (Standalone) and Microsoft R Server (Standalone)

This standalone server system supports distributed, scalable R solutions on multiple platforms and using multiple enterprise data sources, such as Linux and HDInsight. If you don't need to integrate with SQL Server, you can install R Server to enable rapid development, deployment, and operationalization of machine learning solutions. You can also use the R Server installers to upgrade the R components associated with a SQL Server instance and obtain the latest version of R.

If you install Microsoft Machine Learning Server using SQL Server 2017 setup, you can also deploy and consume Python applications.

For more information, see [Microsoft Machine Learning Server](https://docs.microsoft.com/r-server/index).

## Related technologies

Microsoft provides broad support for machine learning ecosystems, including tools, providers, enhanced R and Python packages, a cloud development and services platform, and an integrated development environment.

### R Tools for Visual Studio

Visual Studio provides a full development environment for the R language. The plug-in includes an editor, interactive window, plotting, debugger, and more. You can use .NET languages from R or invoke R from .NET via open source libraries such as R.NET and rClr.

Visual Studio also has an excellent Python development environment. There's no easier way to work with machine learning languages while continuing to enjoy access to SQL database development tools.

For more information, see:

+ [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/)
+ [Python - Visual Studio](https://www.visualstudio.com/vs/python/)

### Azure Machine Learning

When you create your own workspace in Azure Machine Learning Studio, you get access to over 400 preloaded R packages. You can also choose when you create an experiment that uses R, to deploy R using either a standard CRAN R distribution, or Microsoft R Open. You can even create your own R packages and upload them to Azure to run as custom modules.

Many of the algorithms provided in Azure ML are now included in Microsoft Machine Learning Services, as part of the MicrosoftML package. For more information, see [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package).

Azure Machine Learning is another convenient platform for data scientist and developers who need to build, train, and deploy models using Web services. You can publish solutions to the [Machine Learning Marketplace](http://datamarket.azure.com/browse/data?category=machine-learning).

For more information about exciting changes in the Azure Machine Learning service, to support professional data scientists, see these resources:

+ [What is Azure Machine Learning?](https://docs.microsoft.com/azure/machine-learning/preview/overview-what-is-azure-ml)
+ [Model management features](https://docs.microsoft.com/azure/machine-learning/preview/model-management-overview)

### Data Science Virtual Machines

You can deploy a pre-installed and pre-configured version of [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)] in Microsoft Azure, making it easy to get started with data exploration and modeling right away on the cloud without setting up a fully configured system on premises.

The Azure Marketplace contains several virtual machines that support data science.

+ The **Microsoft Data Science Virtual Machine** is configured with Machine Learning Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server.

    The new [Data Science VM for Windows Server 2016](http://aka.ms/dsvm/win2016) provides GPU versions of popular deep learning frameworks such as CNTK. Pre-installed tools include the GPU NVIDIA drivers, CUDA Toolkit 8.0, and the NVIDIA cuDNN library for GPU workloads. In just minutes, you can have a complete environment for building deep learning models that can run on either CPU or CPU plus GPU.

+ For R Server or Machine Learning Server, we recommend the Microsoft Machine Learning Server 2017 for Linux or for Windows 2016 Server.

+ To get an Azure image with SQL Server machine learning, we recommend any of the virtual machine offerings that include **SQL Server 2017**. When you select the image, follow the additional recommendations about tier and service level, to ensure that the VM can support machine learning workloads.

## Next steps

[Getting started with Machine Learning Services](getting-started-with-sql-server-r-services.md)

[Getting started with Machine Learning Server](getting-started-with-microsoft-r-server-standalone.md)

[Install the SQL Server database engine](../../database-engine/install-windows/install-sql-server-database-engine.md)
