---
title: Install SQL Server R Server or Machine Learning Server (Standalone) | Microsoft Docs
description: Setup a non-instance-aware standalone machine learning server for R and Python development using RevoScaleR, revoscalepy, MicrosoftML and other packages.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/28/2018 
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# Install Machine Learning Server (Standalone) or R Server (Standalone) on Windows
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server setup includes the option to install a non-instance-aware, standalone machine learning server that runs outside of SQL Server. In SQL Server 2016 Setup, this feature is called **R Server (Standalone)**. In SQL Server 2017, this feature is called **Machine Learning Server (Standalone)** and includes R and Python. 

A standalone server as installed by SQL Server Setup is functionally equivalent to the non-SQL-branded versions of [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), supporting all of the same use cases and scenarios including remote execution, operationalization and web services, and the complete collection of RevoScaleR and revoscalepy functions.

As an independent server decoupled from SQL Server, the R and Python environment is configured, secured, and accessed using the underlying operating system and tools provided in the standalone server, not SQL Server.

As an adjunct to SQL Server, a standalone server useful if you need to develop high-performance machine learning solutions that can use remote compute contexts, switching interchangeably between the local server and a remote Machine Learning Server on a Spark cluster or on another SQL Server instance.
  

## <a name="bkmk_prereqs"> </a> Pre-install checklist

If you installed a previous version, such as SQL Server 2016 R Server (Standalone) or Microsoft R Server, uninstall the existing installation before continuing.

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
 ###  <a name="bkmk_ga_instalpatch"></a> Install patch requirement 

For SQL Server 2016 only: Microsoft has identified a problem with the specific version of Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server. If this update to the VC runtime binaries is not installed, SQL Server may experience stability issues in certain scenarios. Before you install SQL Server follow the instructions at [SQL Server Release Notes](../../sql-server/sql-server-2016-release-notes.md#bkmk_ga_instalpatch) to see if your computer requires a patch for the VC runtime binaries.  
::: moniker-end

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the installation wizard.

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
1. Click the **Installation** tab, and select **New Machine Learning Server (Standalone) installation**.
    
     ![Install Machine Learning Server Standalone](media/2017setup-installation-page-mlsvr.png "Start installation of Machine Learning Server Standalone")
::: moniker-end

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
1. On the **Installation** tab, click **New R Server (Standalone) installation**.
    
     ![Start setup of R Server Standalone](media/2016-setup-installation-rsvr.png "Start setup of R Server Standalone")
::: moniker-end

1. After the rules check is complete, accept SQL Server licensing terms, and select a new installation.

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
1. On the **Feature Selection** page, the following options should already be selected:

    - Microsoft Machine Learning Server (Standalone)

    - R and Python are both selected by default. You can deselect either language, but we recommend that you install at least one of the supported languages.

     ![Install Machine Learning Server Standalone](media/2017setup-features-page-mlsvr-rpy.png "Start installation of Machine Learning Server Standalone")
    
    All other options should be ignored. 
    
    > [!NOTE]
    > Avoid installing the **Shared Features** if the computer already has Machine Learning Services installed for SQL Server in-database analytics. This creates duplicate libraries.
    > 
    > Also, whereas R or Python scripts running in SQL Server are managed by SQL Server so as not to conflict with memory used by other database engine services, the standalone machine learning server has no such constraints, and can interfere with other database operations. Finally, remote access via RDP session, which is often used for operationalization, is typically blocked by database administrators.
    > 
    > For these reasons, we generally recommend that you install Machine Learning Server (Standalone) on a separate computer from SQL Server Machine Learning Services.
::: moniker-end

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
1.  On the **Feature Selection** page, the following option should be already selected:
    
    **R Server (Standalone)**  
    
    ![Feature selections for R Server Standalone](media/2016setup-rserver-features.png "Feature selections for R Server Standalone")
    
    All other options can be ignored. 
    
    > [!NOTE]
    > Avoid installing the **Shared Features** if you are running setup on a computer where R Services has already been installed for SQL Server in-database analytics. This creates duplicate libraries.
    > 
    > Whereas R scripts running in SQL Server are managed by SQL Server so as not to conflict with memory used by other database engine services, the standalone R Server has no such constraints, and can interfere with other database operations.
    > 
    > We generally recommend that you install R Server (Standalone) on a separate computer from SQL Server R Services (In-Database).
::: moniker-end

1.  Accept the license terms for downloading and installing Microsoft R Open. When the **Accept** button becomes unavailable, you can click **Next**.

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
1.  Accept the license terms for downloading and installing Anaconda.
    
     ![Python license agreement](media/2017setup-python-license.png "Python license agreement")
    
    Installation of these components, and any prerequisites they might require, might take a while. When the **Accept** button becomes unavailable, you can click **Next**.
::: moniker-end

1.  On the **Ready to Install** page, verify your selections, and click **Install**.

### Default installation folders

When you install R Server or Machine Learning Server using SQL Server setup, the R libraries are installed in a folder associated with the SQL Server version that you used for setup. In this folder, you will also find sample data, documentation for the R base packages, and documentation of the R tools and runtime.

However, if you install using the separate Windows installer, or if you upgrade using the separate Windows installer, the R libraries are installed in a different folder.

Just for reference, if you have installed an instance of SQL Server with R Services (In-Database) or Machine Learning Services (In-Database), and that instance is on the same computer, the R libraries and tools are installed by default in a different folder.

The following table lists the paths for each installation.

|Version| Installation method | Default folder|
|----|----|----|
|SQL Server 2017 Machine Learning Server (Standalone) |  SQL Server 2017 setup wizard |`C:\Program Files\Microsoft SQL Server\140\R_SERVER` <br/>`C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER`|
|Microsoft Machine Learning Server (Standalone) |  Windows standalone installer |`C:\Program Files\Microsoft\ML Server\R_SERVER`<br/>`C:\Program Files\Microsoft\ML Server\PYTHON_SERVER`|
|SQL Server 2017 Machine Learning Services (In-Database) |SQL Server 2017 setup wizard, with R language option|`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\R_SERVICES`  <br/>`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\PYTHON_SERVICES` |
|SQL Server 2016 R Server (Standalone) |  SQL Server 2016 setup wizard |`C:\Program Files\Microsoft SQL Server\130\R_SERVER`|
|SQL Server 2016 R Services (In-Database) |SQL Server 2016 setup wizard|`C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\R_SERVICES`|

## Development tools

A development IDE is not installed as part of setup. For more information on how to configure a development environment, see [Set up R tools](./r/set-up-a-data-science-client.md) and [Set up Python tools](./python/setup-python-client-tools-sql.md).

## Get help

Need help with installation or upgrade? For answers to common questions and known issues, see the following article:

* [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md)

To check the installation status of the instance and fix common issues, try these custom reports.

* [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md)

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).
