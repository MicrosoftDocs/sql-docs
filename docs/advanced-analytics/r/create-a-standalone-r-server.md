---
title: "Create a Standalone R Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 408e2503-5c7d-4ec4-9d3d-bba5a8c7661d
caps.latest.revision: 35
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Create a Standalone R Server
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup includes the option to install **Microsoft R Server (Standalone)**. 
  
  Typically, you might install Microsoft R Server (Standalone) if you need to develop high performance R solutions on Windows. You can run your solutions locally on the computer where you installed R Server (Standalone), leveraging the ScaleR technology to scale and parallelize your R code, or you can deploy your solution to these powerful compute contexts:
  
  + An instance of SQL Server running R Services
  + An instance of R Server using a Hadoop or Spark cluster
  + Teradata in-database analytics
  + R Server running in Linux 
 
 For more information, see [R Server Platform](https://www.microsoft.com/cloud-platform/r-server) and [What's Included](#bkmk_Included).
  
 Microsoft R Server is available only in Enterprise Edition.  
  

## Licensing and Updates
  
You can install Microsoft R Server using two methods:

+ Use the SQL Server setup wizard to create a standalone installation of R Server for Windows, as described [in this section](#bkmk_installRServer). 

    Setup requires a SQL Server license and upgrades are typically aligned with the SQL Server release cadence. This ensures that your development tools are in synch with the version running in the SQL Server compute context.

+ Use the new, simplified Windows installer, as described separately, here: [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows). 

    Running this setup wizard enrolls the instance in the [Modern Lifecycle](https://support.microsoft.com/help/447912) policy. This support offering ensures that you are always using the most current version of R.

> [!TIP]
> You can also use the new Windows installer for Microsoft R Server to convert any instance of SQL Server 2016 R Services to the new support policy. This option might be desirable if you want more frequent updates of the R components. For more information, see [Use sqlBindR.exe to upgrade an instance of R Services](http://www.bing.com).   
  
##  <a name="bkmk_installRServer"></a> How to Install Microsoft R Server (Standalone)  
  
1.  If you have installed a previous version of Microsoft R Server, you must uninstall it first.  See [Upgrading from an Older Version of Microsoft R Server](#bkmk_Uninstall). 

2. Run SQL Server setup.  
  
2.  On the **Installation** tab, click **New R Server (Standalone) installation** .  
  
     ![Setup option for R Server Standalone](../../advanced-analytics/r-services/media/rsql-rstandalonesetup.png "Setup option for R Server Standalone")  
  
3.  On the **Feature Selection** page, the following option should be already selected:  
  
    -   **R Server (Standalone)**  
  
         This  option installs shared features, including open source R tools and base packages, and the enhanced R packages and connectivity tools provided by Microsoft R.  
  
     All other options can be ignored.  
  
4.  Accept the license terms for downloading and installing Microsoft R Open. When the **Accept** button becomes unavailable, you can click **Next**. Installation of these components (and any prerequisites they might require) might take a while.   
  
5.  On the **Ready to Install** page, verify your selections, and click **Install**.  
  
> [!TIP]
> For more information about automated or off-line installation, see [Install Microsoft R Server from the Command Line](../../advanced-analytics/r-services/install-microsoft-r-server-from-the-command-line.md).

## How to Upgrade R Server to 9.0.1

If you have already installed Microsoft R Server (Standalone) you can upgrade the instance to use the new Modern Lifecycle Support policy. This allows the instance to be updated more frequently, separately from the SQL Server support schedule. 

1. Install Microsoft R Server (Standalone), if it is not already installed.

2. Download the separate Windows-based installer for R Server 9.0.1. For download locations, see [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows#howtoinstall).

3. Run the installer and follow [these instructions](https://msdn.microsoft.com/microsoft-r/rserver-install-windows#download-r-server-installer)

  
 ## <a name ="bkmk_Included"></a> What is Included
 
 When you install Microsoft R Server, you get the same enhanced R packages and connectivity tools that are provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. All Microsoft R Server deployments include the R base packages, a set of enhanced R packages that support parallel processing, improved performance, and connectivity to data sources including [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Hadoop.
 
 An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not required. When you run R code using in a local compute context and Microsoft R Server (Standalone), the R script is executed using its own set of R binaries and package libraries. 
 To run your code in-database, you would either deploy the solution to SQL Server, or use a remote compute context.  

  
### R  packages 
  
The R libraries are installed in a folder associated with the SQL Server version that you used for setup. In this folder you will also find sample data, documentation for the R base packages, and documentation of the R tools and runtime.  

**R Server (Standalone) with setup using SQL Server 2016**  
`C:\Program Files\Microsoft SQL Server\130\R_SERVER`         

**R Server (Standalone) with setup using SQL Server 2017**  
`C:\Program Files\Microsoft SQL Server\140\R_SERVER`   
       
**R Server (Standalone) with setup using the separate Windows installer**     
`C:\Program Files\Microsoft\R Server\R_SERVER`
      
> [!IMPORTANT]  
>  If you have installed an instance of SQL Server with R Services (In-Database) on the same computer, the R libraries and tools are installed into a different folder:  `C:\Program Files\Microsoft SQL Server\<instance_name>\R_SERVICES`  
>   
>  Do not directly call the R packages or utilities associated with the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance. If both R Server and R Services are installed on the same computer, always use the R tools and packages in the R_SERVER folder. 

  
### R  tools  
  
An R development IDE is not installed as part of setup. Additional tools are not required, as all the standard base R tools are included in `C:\Program Files\Microsoft SQL Server\130\R_SERVER\bin`.  
  
However, we recommend that you install [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)],  or another preferred development environment, such as RStudio. For more information, see [Setup or Configure R Tools](../../advanced-analytics/r-services/setup-or-configure-r-tools.md) or [Getting Started with RTVS](http://rtvs/).  


## Troubleshooting  

### Incompatible version of R Client and R Server

If you install the latest version of Microsoft R Client and use it to run R on SQL Server using a remote compute context, you might get the following error:

*You are running version 9.0.0 of Microsoft R client on your computer, which is incompatible with the Microsoft R server version 8.0.3. Download and install a compatible version.*

Typically, the version of R that is installed with SQL Server R Services is updated when service releases are published. To ensure that you always have the most up-to-date versions of R components, install all service packs. For compatibility with Microsoft R Client 9.0.0, you must install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262). 


### Installing Microsoft R Server on an instance of SQL Server installed on Windows Core
In the release version of SQL Server 2016, there was a known issue when adding Microsoft R Server to an instance on Windows Server Core edition. This has been fixed. 

If you encounter this issue, you can applied the fix described in [KB3164398](https://support.microsoft.com/kb/3164398) to add the R feature to the existing instance on Windows Server Core.   For more information, see [Can't install Microsoft R Server Standalone on a Windows Server Core operating system](https://support.microsoft.com/kb/3168691).


###  <a name="bkmk_Uninstall"></a> Upgrading from an Older Version of Microsoft R Server  
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
  
## See Also  
 [Microsoft R Server](../../advanced-analytics/r-services/r-server-standalone.md)  
  
  
