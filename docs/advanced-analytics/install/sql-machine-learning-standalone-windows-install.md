---
title: "Install SQL Server 2017 Machine Learning Server (Standalone) | Microsoft Docs"
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
# Install SQL Server 2017 Machine Learning Server (Standalone) on Windows
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server setup includes the option to install a machine learning server that runs outside of SQL Server. This option might be useful if you need to develop high-performance machine learning solutions that can use remote compute contexts, switching interchangeably between the local server and a remote Machine Learning Server on a Spark cluster or on another SQL Server instance.
  
This article describes how to use SQL Server setup to install the standalone version of Machine Learning Server. If you have an Enterprise Edition or Software Assurance, installing the standalone machine learning server is free.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

If you installed a previous version of Microsoft R Server, we recommend that you uninstall it first.

## Run Setup

1. Start the setup wizard for SQL Server 2017.

2. Click the **Installation** tab, and select **New Machine Learning Server (Standalone) installation**.
    
     ![Install Machine Learning Server Standalone](media/2017setup-installation-page-mlsvr.png "Start installation of Machine Learning Server Standalone")

3. After the rules check is complete, accept SQL Server licensing terms, and select a new installation.

4. On the **Feature Selection** page, the following options should already be selected:

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

5.  Accept the license terms for downloading and installing the machine learning components. If you install both languages, a separate licensing agreement is required for Microsoft R and for Python.
    
     ![Python license agreement](media/2017setup-python-license.png "Python license agreement")
    
    Installation of these components, and any prerequisites they might require, might take a while. When the **Accept** button becomes unavailable, you can click **Next**.

6.  On the **Ready to Install** page, verify your selections, and click **Install**.

For more information about automated or off-line installation, see [Install Microsoft R Server from the command line](../../advanced-analytics/r/install-microsoft-r-server-from-the-command-line.md).



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

### Development tools

A development IDE is not installed as part of setup. Additional tools are not required, as all the standard tools are included that would be provided with a distribution of R or Python.

We recommend that you try the new release of [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)]. Visual Studio supports both R and Python, as well as database development tools, connectivity with SQL Server, and BI tools. However, you can use any preferred development environment, including RStudio.

## Troubleshooting

This section lists some common issues to be aware of when installing Machine Learning Server

### Incompatible version of R Client and R Server

If you install Microsoft R Client and use it to run R in a remote SQL Server compute context, you might get an  error like this:

*You are running version 9.0.0 of Microsoft R client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

In SQL Server 2016, it was required that the version of R that was running in SQL Server R Services be exactly the same as the libraries in Microsoft R Client. That requirement has been removed in later versions. However, we recommend that you always get the latest versions of the machine learning components, and install all service packs. 

If you have an earlier version of Microsoft R Server and need to ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262).


### Installation fails with error "Only one Revolution Enterprise product can be installed at a time."

You might encounter this error if you have an older installation of the Revolution Analytics products, or a pre-release version of SQL Server R Services. You must uninstall any previous versions before you can install a newer version of Microsoft R Server. Side-by-side installation with other versions of the Revolution Enterprise tools is not supported.

However, side-by-side installs are supported when using R Server Standalone with [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] or SQL Server 2016.

### Unable to uninstall older components

If you have problems removing an older version, you might need to edit the registry to remove related keys.

> [!IMPORTANT]
> This issue applies only if you installed a pre-release version of Microsoft R Server or a CTP version of SQL Server 2016 R Services.
  
1. Open the Windows Registry, and locate this key: `HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall`.
2. Delete any of the following entries if present, and if the key contains only the value `sEstimatedSize2`:
  
    -   E0B2C29E-B8FC-490B-A043-2CAE75634972        (for 8.0.2)
  
    -   46695879-954E-4072-9D32-1CC84D4158F4        (for 8.0.1)
  
    -   2DF16DF8-A2DB-4EC6-808B-CB5A302DA91B        (for 8.0.0)
  
    -   5A2A1571-B8CD-4AAF-9303-8DF463DABE5A        (for 7.5.0)
  
## See also

[Machine Learning Server (Standalone)](../../advanced-analytics/r/r-server-standalone.md)
