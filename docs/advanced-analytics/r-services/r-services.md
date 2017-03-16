---
title: "R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2016-12-20"
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
# R Services
  Microsoft R Services provides two server platforms for integrating the popular open source R language with business applications: **SQL Server R Services (In-Database)**, for integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and **Microsoft R Server**, for enterprise-level R deployments on Windows and Linux servers.  
  
-   **R Services (In-Database)**  
  
     The goal of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is to enable rapid development, deployment, and operationalization of R solutions based on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] platform and related services.  
  
     [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] brings the compute to the data by allowing R to run on the same computer as the database. It includes a database service that runs outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process and communicates securely with the R runtime. You can train R models, generate R plots, perform scoring, and easily move data between R and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Data scientists who are testing and developing solutions can communicate with the server from a remote development computer to run R code on the server, and deploy completed solutions to SQL Server by embedding calls to R in stored procedures.  
  
     This download includes a distribution of the open source R language, as well as ScaleR, a set of high-performance, scalable R packages. It also includes providers for easier, faster connectivity with all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technologies.  
  
     For more information, see [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md). For sample scenarios, see  [SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md).  
  
-   **Microsoft R Server**  
  
     This standalone server system supports distributed, scalable R solutions on multiple platforms and using multiple enterprise data sources, including Linux, Hadoop, and Teradata.  
  
     For more information, see [Microsoft R Server (MSDN)](https://msdn.microsoft.com/microsoft-r/index).  
  
## Related Technologies  
 Microsoft provides broad support for the open source R language ecosystem, including tools, providers, enhanced R packages, and integrated development environments.  
  
-   **R Tools for Visual Studio**  
  
     Visual Studio provides a full development environment for the R language. The plug-in includes an editor, interactive window, plotting, debugger, and more. You can use .NET languages from R or invoke R from .NET via open source libraries such as R.NET and rClr.  
  
     For more information, see [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/).  
  
-   **R in Azure Machine Learning**  
  
     Create your own workspace in Azure Machine Learning Studio, where you can access over 400 preloaded R packages. Build and train models to deploy as a Web service, or write custom scripts to transform data. Create your own R packages and upload them to Azure to run as custom modules, and publish solutions to the [Machine Learning Marketplace](http://datamarket.azure.com/browse/data?category=machine-learning).  
  
     For more information, see [Extend your experiment with R](https://docs.microsoft.com/azure/machine-learning/machine-learning-extend-your-experiment-with-r) and [Author custom R modules in Azure Machine Learning](https://docs.microsoft.com/azure/machine-learning/machine-learning-custom-r-modules).  
  
-   **Data Science Virtual Machines**  
  
     You can deploy a pre-installed and pre-configured version of [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)] in Microsoft Azure, making it easy to get started with data exploration and modeling right away on the cloud without setting up a fully configured system on premises.  
  
     The Azure Marketplace contains several virtual machines that support data science:
     + The **Microsoft Data Science Virtual Machine** is configured with Microsoft R Server, as well as Python (Anaconda distribution), a Jupyter notebook server, Visual Studio Community Edition, Power BI Desktop, the Azure SDK, and SQL Server Express edition. 
     + **Microsoft R Server 2016 for Linux** contains the latest version of R Server (version 9.0.1). Separate VMs are available for CentOS version 7.2 and  Ubuntu version 16.04. 
     + The **R Server Only SQL Server 2016 Enterprise** virtual machine includes a standalone installer for R Server 9.0.1 that supports the new Modern Software Lifecycle  licensing model.
 

## See Also  
[Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md) 

[Getting started with Microsoft R Server](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)  

 [Install SQL Server Database Engine](../../database-engine/install-windows/install-sql-server-database-engine.md)  
  
  