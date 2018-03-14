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

This article describes how to use SQL Server 2016 setup to install the standalone version of SQL Server 2016 R Server. If you have an Enterprise Edition or Software Assurance, installing the standalone R Server on a production server is free.

If you have installed any previous version of the Revolution Analytics tools or packages, you must uninstall them first. 

1. Run SQL Server 2016 setup. We recommend that you install Service Pack 1 or later.

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

## <a name="bkmk_upgrade"></a> Upgrade R Server

A standalone server is independent of the database engine and can be upgraded to newer versions of the software that are outside of SQL Server. Specifically, you can upgrade to the newest release of Microsoft Machine Learning Server for Windows. 

You might upgrade to use newer versions of the R components or to change the support policy from SQL Server to the Modern Software Lifecycle Support policy. This allows the instance to be updated more frequently, on a different schedule than the SQL Server releases.

1. Download the separate Windows-based installer from the locations listed here: 

    + [Install Machine Learning Server 9.3 or 9.2.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)
    + [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows)

2. Run the installer and follow the on-screen prompts. On the page where you select the features to install, select each instance of R Server that you want to upgrade.

## <a name ="bkmk_tips"></a> Installation tips

This section provides additional information related to setup.

### Which version should I install?

+ **Microsoft R Server** was first offered as a part of SQL Server 2016 and supports the R language. The last version of Microsoft R Server was 9.2.1.

+ **Microsoft Machine Learning Server** is the next generation of R Server, renamed to reflect the support for Python. Cumulative Update 1 for SQL Server 2017 includes version 9.2.1.24 of Machine Learning Server. We recommend installing Cumulative Update 1 to get the latest Python APIs.

+ **Upgrading in place**: Setup requires a SQL Server license and upgrades are typically aligned with the SQL Server release cadence. This ensures that your development tools are in synch with the version running in the SQL Server compute context. However, you can use the separate Windows-based installer to get more frequent updates, under the Modern Software Lifecycle support policy. You can also use this  installer to upgrade an instance of SQL Server 2016 or SQL Server 2017.

### Default installation folders

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
### Development tools

A development IDE is not installed as part of setup. Additional tools are not required, as all the standard tools are included that would be provided with a distribution of R or Python.

We recommend that you try the new release of [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)]. Visual Studio supports both R and Python, as well as database development tools, connectivity with SQL Server, and BI tools. However, you can use any preferred development environment, including RStudio.

## Troubleshooting

This section lists some common issues to be aware of when installing SQL Server R Server (Standalone).

### Incompatible version of R Client and R Server

If you install Microsoft R Client and use it to run R in a remote SQL Server compute context, you might get an  error like this:

*You are running version 9.0.0 of Microsoft R client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

In SQL Server 2016, it was required that the version of R that was running in SQL Server R Services be exactly the same as the libraries in Microsoft R Client. That requirement has been removed in later versions. However, we recommend that you always get the latest versions of the machine learning components, and install all service packs. 

If you have an earlier version of Microsoft R Server and need to ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262).

### Installing Microsoft R Server on an instance of SQL Server installed on Windows Core

In the RTM version of SQL Server 2016, there was a known issue when adding Microsoft R Server to an instance on Windows Server Core edition. This has been fixed.

If you encounter this issue, you can apply the fix described in [KB3164398](https://support.microsoft.com/kb/3164398) to add the R feature to the existing instance on Windows Server Core.   For more information, see [Can't install Microsoft R Server Standalone on a Windows Server Core operating system](https://support.microsoft.com/kb/3168691).

###  <a name="bkmk_Uninstall"></a> Upgrading from an older version of Microsoft R Server

If you installed a pre-release version of Microsoft R Server, you must uninstall it before you can upgrade to a newer version.

**To uninstall R Server (Standalone)**

1.  In **Control Panel**, click **Add/Remove Programs**, and select `Microsoft SQL Server 2016 <version number>`.

2.  In the dialog box with options to **Add**, **Repair**, or **Remove** components, select **Remove**.
  
3.  On the **Select Features** page, under **Shared Features**, select **R Server (Standalone)**. Click **Next**, and then click **Finish** to uninstall just the selected components.

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
