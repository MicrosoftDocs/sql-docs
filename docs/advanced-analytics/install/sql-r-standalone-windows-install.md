---
title: "Install SQL Server 2016 R Server (Standalone) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 35
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "On Demand"
---
# Install SQL Server 2016 R Server (Standalone)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to use SQL Server 2016 setup to install the standalone version of **SQL Server 2016 R Server**. If you have an Enterprise Edition or Software Assurance, installing the standalone R Server on a production server is free.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

SQL Server 2016 is required. If you have SQL Server 2017, please install [SQL Server 2017 Machine Learning Server (Standalone)](sql-machine-learning-standalone-windows-install.md) instead.

If you installed any previous version of the Revolution Analytics tools or packages, you must uninstall them first. 

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

 ###  <a name="bkmk_ga_instalpatch"></a> Install patch requirement 

Microsoft has identified a problem with the specific version of Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server. If this update to the VC runtime binaries is not installed, SQL Server may experience stability issues in certain scenarios. Before you install SQL Server follow the instructions at [SQL Server Release Notes](../../sql-server/sql-server-2016-release-notes.md#bkmk_ga_instalpatch) to see if your computer requires a patch for the VC runtime binaries.  

## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the setup wizard for SQL Server 2016. We recommend that you install Service Pack 1 or later.

2. On the **Installation** tab, click **New R Server (Standalone) installation**.
    
     ![Start setup of R Server Standalone](media/2016-setup-installation-rsvr.png "Start setup of R Server Standalone")
    
3.  On the **Feature Selection** page, the following option should be already selected:
    
    **R Server (Standalone)**  
    
    ![Feature selections for R Server Standalone](media/2016setup-rserver-features.png "Feature selections for R Server Standalone")
    
    All other options can be ignored. 
    
    > [!NOTE]
    > Avoid installing the **Shared Features** if you are running setup on a computer where R Services has already been installed for SQL Server in-database analytics. This creates duplicate libraries.
    > 
    > Whereas R scripts running in SQL Server are managed by SQL Server so as not to conflict with memory used by other database engine services, the standalone R Server has no such constraints, and can interfere with other database operations.
    > 
    > We generally recommend that you install R Server (Standalone) on a separate computer from SQL Server R Services (In-Database).

4.  Accept the license terms for downloading and installing Microsoft R Open. When the **Accept** button becomes unavailable, you can click **Next**.
    
    Installation of these components, and any prerequisites they might require, might take a while.
    
5.  On the **Ready to Install** page, verify your selections, and click **Install**.

## Default installation folders

When you install R Server using SQL Server setup, the R libraries are installed in a folder associated with the SQL Server version that you used for setup. In this folder, you will also find sample data, documentation for the R base packages, and documentation of the R tools and runtime.

However, if you installed Microsoft R Server using the separate Windows installer (not SQL Setup), or if you upgrade using the separate Windows installer, the R libraries are installed in a different folder.

Although we recommend against it, if you also installed an instance of SQL Server with R Services (In-Database) on the same computer, a second copy of R libraries and tools are installed in a different folder.

The following table lists the paths for each installation.

|Version| Installation method | Default folder|
|----|----|----|
|R Server (Standalone) |SQL Server 2016 setup wizard|`C:\Program Files\Microsoft SQL Server\130\R_SERVER`|
|R Server (Standalone) |Windows standalone installer|`C:\Program Files\Microsoft\R Server\R_SERVER`|
|Machine Learning Server (Standalone) |  SQL Server 2017 setup wizard, with R language option |`C:\Program Files\Microsoft SQL Server\140\R_SERVER`|
|Machine Learning Server (Standalone) |  SQL Server 2017 setup wizard, with Python language option |`C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER`|
|Machine Learning Server (Standalone) |  Windows standalone installer |`C:\Program Files\Microsoft\R Server\R_SERVER`|
|R Services (In-Database) |SQL Server 2016 setup wizard|`C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\R_SERVICES`|
|Machine Learning Services (In-Database) |SQL Server 2017 setup wizard, with R language option|`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\R_SERVICES`  |
|Machine Learning Services (In-Database) |SQL Server 2017 setup wizard, with Python language option| `C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\PYTHON_SERVICES` |

## Development tools

A development IDE is not installed as part of setup. Additional tools are not required, as all the standard tools are included that would be provided with a distribution of R or Python.

We recommend that you try the new release of [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)] or [Python for Visual Studio](https://docs.microsoft.com/en-us/visualstudio/python/installing-python-support-in-visual-studio). Visual Studio supports both R and Python, as well as database development tools, connectivity with SQL Server, and BI tools. However, you can use any preferred development environment, including RStudio.
  
## Get help

Need help with installation or upgrade? For answers to common questions and known issues, see the following article:

* [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md)

To check the installation status of the instance and fix common issues, try these custom reports.

* [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md)

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md).
+ [Tutorial: In-database analytics for R developers](tutorials/sqldev-in-database-r-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).

