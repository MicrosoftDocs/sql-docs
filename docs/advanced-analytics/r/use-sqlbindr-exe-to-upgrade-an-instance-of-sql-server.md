---
title: "Use sqlBindR.exe to Upgrade an Instance of R Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server (starting with 2016 CTP3)"
ms.assetid: 4da80998-f929-4fad-a86f-87d09c1a79ef
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Use sqlBindR.exe to Upgrade an Instance of R Services

When you install the latest version of Microsoft R Server for Windows, it includes a tool that you can use to upgrade the R components associated with an instance of SQL Server R Services. The tool is called  **SqlBindR.exe** tool, and the upgrade process is referred to as **binding**, because it changes the support model for SQL Server machine learning components to use the new Modern Lifecycle Policy. However, the upgrade does not change the support model for the SQL Server database.

In general, this licensing system ensures that your data scientists will always be using the latest version of R. For more information about the terms of the Modern Lifecycle Policy, see [Support Timeline for Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver-servicing-support).

When you bind an instance, several things happen:
+ The support policy for R Server and SQL Server R Services is changed from the SQL Server 2016 support policy to the new Modern Lifecycle Policy.
+ The R components associated with that instance will be automatically upgraded with each release, in lock-step with the R Server version that is current under the new Modern Lifecycle Policy. 
+ New packages are added, which are included by default with R Server, such as RODBC, [MicrosoftML](../using-the-microsoftml-package.md), [olapR](../r/how-to-create-mdx-queries-using-olapr.md), and [sqlrutils](../r/how-to-create-a-stored-procedure-using-sqlrutils.md).
+ The instance can no longer be manually updated, except to add new packages. 

If you later decide that you want to stop upgrading the instance at each release, you must **unbind** the instance as described in [this section](#bkmk_Unbind), and then uninstall Microsoft R Server components as described in this article: [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows). When the process is complete, future R Server upgrades will no longer affect the instance.

> [!NOTE]
> The upgrade process is supported only for SQL Server 2016 instances that have been patched with Cumulative Update 3.0.  
> 
> If you are using Machine Learning Services in SQL Server vNext, you do not need to apply this upgrade. Machine learning components are always automatically upgraded at each milestone.

## How to upgrade an instance

You can upgrade the instance by using the command-line, or by using an interactive wizard.

+ [Using the command line](#bkmk_BindWizard)
+ [Using the wizard](#bkmk_BindCmd)

### <a name="bkmk_prereqs"></a> Prerequisites

+ Identify instances that are candidates for an upgrade. 
    + SQL Server 2016 with R Services installed
    + Service Pack 1 plus CU3

+ Get **R Server**, by downloading the separate Windows installer.
 
    [How to install R Server 9.0.1 on Windows using the standalone Windows installer](https://msdn.microsoft.com/microsoft-r/rserver-install-windows#howtoinstall)


### <a name="bkmk_BindWizard"></a>Upgrade using the new setup wizard

1. Run the new installer for R Server on the computer that has the instance you want to upgrade.
2. The installer will identify local instances that are candidates for binding.
3. Select the check box next to any instance that you want to upgrade.
4. Accept the licensing agreement for Microsoft R Server 9.1.0.
5. The process takes a while. During the installation, the R libraries used by SQL Server R Services are replaced with the libraries for Microsoft R Server 9.1.0. Launchpad is not affected by the process, but the libraries in the R_SERVICES folder will be removed and the service switched over to use the libraries in `C:\Program Files\Microsoft\R Server\R_SERVER`.

### <a name="bkmk_BindCmd"></a>Upgrade using the command line

After Microsoft R Server has been installed, you can run just the SqlBindR.exe tool from the command line.

1. Open a command prompt as administrator and navigate to the folder containing sqlbindr.exe. The default location is `C:\Program Files\Microsoft\R Server\Setup`
2. Type the following command to view a list of available instances:
   `SqlBindR.exe /list`
  
   Make a note of the full instance name as listed. For example, the instance name might be `MSSQL13.MSSQLSERVER` for the default instance, or something like `SERVERNAME.MYNAMEDINSTANCE`.
3. Run the **SqlBindR.exe** command with the */bind* argument, and specify the name of the instance to upgrade, as returned in the previous step. 

   For example, to upgrade the default instance, type:
    `SqlBindR.exe /bind MSSQL13.MSSQLSERVER`
4. When upgrade is complete, restart the Launchpad service associated with any instance that has been modified. 


## <a name="bkmk_Unbind"></a>Unbind an instance

To restore an instance of SQL Server to use the original R libraries installed by SQL Server, you must perform an **unbind** operation. You can do this either by re-running the setup wizard for Microsoft R Server, or by running the SqlBindR utility from the command line. 

Whn you unbind an instance, any R packages that were added with Microsoft R Server will be removed, and the R libraries that were installed by default with SQL Server R Services will be restored. However, you must manually install any extra R packages that might have been added to the instance.

### Unbind using the wizard

1. Download the new installer for Microsoft R Server 9.1.0.
2. Run the installer on the computer that has the instance you want to unbind.
2. The installer will identify local instances that are candidates for unbinding.
3. Deselect the check box next to the instance that you want to revert to the original SQL Server R Services configuration.
4. Accept the licensing agreement for Microsoft R Server 9.1.0. 
  > [!NOTE] You must perform this step even if you are removing R Server.
5. The process takes a while. During the installation, the R libraries used by SQL Server R Services are replaced with the libraries for Microsoft R Server 9.1.0. The SQL Server Launchpad is not affected by the process, but the libraries in the R_SERVICES folder will be removed and the service switched over to use the libraries in `C:\Program Files\Microsoft\R Server\R_SERVER`.

### Unbind using the command line

1. Open a command prompt and navigate to the folder that contains **sqlbindr.exe**, as described in the previous section. 

2. Run the **SqlBindR.exe** command with the */unbind* argument, and specify the instance. 

   For example, the following command reverts the default instance:
   
    `SqlBindR.exe /unbind MSSQL14.MSSQLSERVER`

> [!NOTE] 
> In the upgrade utility that was included with Microsoft R Server 9.0.1, the utility did not restore the original packages or R components completely, requiring that the user run repair on the instance, apply all service releases, and then restart the instance. 
> 
> However, the latest version of the upgrade utility, for Microsoft R Server 9.1.0, will perform the restore of R Services features automatically. Therefore, you do not need to reinstall the R components or re-patch the server. You will still need to install any R packages that might have been added after the initial install of SQL Server R Services.


## Known Issues

This section lists known issues specific to use of the SqlBindR.exe utility, or to upgrades using the Microsoft R Server setup utility that affect SQL Server instances.

### Cannot perform upgrade from 9.0.1

If you have previously upgraded a SQL Server R Services instance to 9.0.1, when you run the new installer for Microsoft R Server 9.1.0, it will display a list of all valid instances, and then by default select previously bound instances. If you continue, the previously bound instances are unbound. As a result, the earlier 9.0.1 installation is removed, including any related packages, but the new version of Microsoft R Server (9.1.0) is not installed. 

As a workaround, you can modify the existing R Server installation as follows:
1. In Control Panel, open **Add or Remove Programs**.
2. Locate Microsoft R Server, and click **Change/Modify**.
3. When the installer starts, select the instances you want to bind to 9.1.0.


### Binding or unbinding leaves multiple temporary folders

Sometimes the binding and unbinding operations fail to clean up temporary folders. 
If you find folders with a name like this, you can remove it after installation is complete: `R_SERVICES_<guid>`

Be sure to wait until all operations are complete, as it can take a long time to remove R libraries associated with one version and then add the new R libraries.

## sqlbindr.exe command syntax


### Usage

`sqlbindr [/list] [/bind <SQL_instance_ID>] [/unbind <SQL_instance_ID>]`

### Parameters

|Name|Description|
|------|------|
|*list*| Displays a list of all SQL database instance IDs on the current computer|
|*bind*| Upgrades the specified SQL database instance to the latest version of R Server and ensures the instance automatically gets future upgrades of R Server|
|*unbind*|Uninstalls the latest version of R Server from the specified SQL database instance and prevents future R Server upgrades from affecting the instance|

### Errors

The tool returns the following error messages:

|Error|Resolution|
|------|------|
|An error occurred while binding the instance| The instance could not be bound. Contact support for assistance.|
|The instance is already bound| You ran the *bind* command, but the specified instance is already bound. Choose a different instance.|
|The instance is not bound| You ran the *unbind* command, but the instance you specified is not bound. Choose a different instance that is compatible.|
|Not a valid SQL instance ID| You might have typed the instance name incorrectly. Run the command again with the *list* argument to see the available instance IDs.|
|No instances found| This computer does not have an instance of SQL Server R Services.|
|The instance must have a compatible version of SQL R Services (In-Database) installed.| See the compatibility requirements in this topic for details.|
|An error occurred while unbinding the instance| The instance could not be unbound. Contact support for assistance.|
|An unexpected error has occurred| Other errors. Contact support for assistance.  |
|No SQL instances found| This computer does not have an instance of SQL Server. |


## See Also

[R Server Release Notes](https://msdn.microsoft.com/microsoft-r/notes/r-server-notes)

