---
title: "Upgrade and Installation FAQ (SQL Server R Services) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/13/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 001e66b9-6c3f-41b3-81b7-46541e15f9ea
caps.latest.revision: 58
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Upgrade and Installation FAQ (SQL Server R Services)
  This topic provides answers to common questions about installation and upgrades. It also contains questions about upgrades from  preview releases of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].
  
 If you are installing [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] for the first time,  follow the procedures for setting up [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and the R components as described here: [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).  

 
  This topic also contains some issues related to setup and upgrade of R Server (Standalone). For issues related to Microsoft R Server on other platforms, see [Microsoft R Server Release Notes](https://msdn.microsoft.com/microsoft-r/notes/r-server-notes) and [Run Microsoft R Server for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows). 
     
## Important changes from pre-release versions  
 If you installed any pre-release version of SQL Server 2016, or if you are using setup instructions that were published prior to the public release of SQL Server 2016, it is important to be aware that the setup process is completely different between pre-release and RTM versions. These changes include both the options available in the SQL Server setup wizard and post-installation steps. 
   
> [!WARNING]
> New installation of any pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is no longer supported. We recommend that you upgrade as soon as possible.  
+ If you installed [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] in CTP3, CTP3.1, CTP3.2, RC0, or RC1, you will need to re-run the post-configuration installation script to uninstall the previous versions of the R components and of R Services. 
+ The post-configuration installation script is provided solely to help customers uninstall pre-release versions.  Do not run the script when installing any newer version.


## Upgrading R components

As hotfixes or improvements to SQL Server 2016 are released, R components will be upgraded or refreshed as well, if your instance already includes the R Services feature. 

If you install or upgrade servers that are not connected to the Internet, you must download an updated version of the R components manually before beginning the refresh. For more information, see [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).

If you are using SQL Server 2017, upgrades to R components are automatically installed.

As of December 2016, it is now possible to upgrade R components on a faster cadence than the SQL Server release cycle, by _binding_ an instance of R Services to the Modern Software Lifecycle policy. For more information, see [Use SqlBindR to Upgrade an Instance of SQL Server R Services](../../advanced-analytics/r-services/use-sqlbindr-exe-to-upgrade-an-instance-of-r-services.md)

## Support for slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that the SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

+ In SQL Server 2016, you can start a slipstream upgrade in SQL Server Management Studio by clicking **Tools**, and selecting **Check for Updates**.

+ If the server does not have Internet access, be sure to download the SQL Server installer. You must also separately download matching versions of the R component installers **before** beginning the update process. For download locations, see [Installing R Components without Internet Acccess](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).  

    When all setup files have been copied to a local directory, start the setup utility by typing SETUP.EXE from the command line.
      
    - Use the */UPDATESOURCE* argument to specify the location of a local file containing the SQL Server update, such as a Cumulative Update or Service Pack release.  
    - Use the */MRCACHEDIRECTORY* argument to specify the folder containing the R component CAB files.

For more information, see this blog by the R Services Support team: [Deploying R Services on Computers without Internet Access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/).


## New license agreement for R components required for unattended installs  
 If you use the command line to upgrade an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] that already has [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed, you must modify the command line to use the new license agreement parameter, */IACCEPTROPENLICENSEAGREEMENT*. 
 
 Failure to use the correct argument can cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to fail.  
  
## After running setup, [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is still not enabled  
 The feature that supports running external scripts is disabled by default, even if installed. This is by design, for surface area reduction.  
  
 To enable R scripts, an administrator can run the following statement in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
1.  On the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance where you want to use R, run this statement.  
  
    ```  
    sp_configure 'external scripts enabled',1 
    reconfigure with override  
    ```  
  
2.  Restart the instance.  
  
3.  After the instance has restarted, open **SQL Server Configuration Manager** or the **Services** panel and verify that the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service is running.  

> [!TIP]
> To install an instance of R Services that is preconfigured, use the Azure Virtual machine image that includes Enterprises Edition with R Services enabled. For more information, see [Installing SQL Server R Services on an Azure Virtual Machine](../../advanced-analytics/r-services/installing-sql-server-r-services-on-an-azure-virtual-machine.md).
 

## Setup of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] not available in a failover cluster  
 Currently, it is not possible to install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a failover cluster.  
    
 However, you can install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a standalone computer that uses Always On and is part of an availability group. For more information about using R Services in an Always On availability group, see [Always On Availability Groups: Interoperability](../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md).  

Another option is to set up a replica on a different SQL Server instance for the purpose of running R scripts. The replica could be created using either replication or log shipping.

## <a name="bkmk_LaunchpadTS"></a>Launchpad service cannot be started  

There are several issues that can prevent Launchpad from starting.

### **8dot3 notation is not enabled**.  

To install R Services (In-Database), the drive where the feature is installed must support creation of short file names using the **8dot3** notation.  An 8.3 filename is also called a short filename and is used for compatibility with older versions of Microsoft Windows prior or as an alternate filename to the long filename. 

If the volume where you are installing [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] does not support the short filenames, the processes that launch R from SQL Server might not be able to locate the correct executable and the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] will not start.  
  
As a workaround, you should enable the 8dot3 notation on the volume where SQL Server is installed and R Services is installed. You must then provide the short name for the working directory in the R Services configuration file. 

1. To enable 8dot3 notation, run the **fsutil** utility with the *8dot3name* argument as described here: [fsutil 8dot3name](https://technet.microsoft.com/library/ff621566(v=ws.11).aspx).
2. After the 8dot3 notation is enabled, open the file  RLauncher.config. In a default installation, the file RLauncher.config is located in C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn
3. Make a note of the property for WORKING_DIRECTORY.
4. Use the fsutil utility with the *file* argument to specify a short file path for the folder specified in WORKING_DIRECTORY.
5. Edit the configuration file to use the short name for WORKING_DIRECTORY.   
     
Alternatively, you can specify a different directory for WORKING_DIRECTORY that has a path compatible with the 8dot3 notation.     
   
> [!NOTE]
> This restriction has been removed in later releases. If you experience this issue,  please install one of the following:
>
>    + SQL Server 2016 SP1 and CU1: [Cumulative Update 1 for SQL Server](https://support.microsoft.com/help/3208177/cumulative-update-1-for-sql-server-2016-sp1)
>    + SQL Server 2016 RTM, Service Pack 3, and this [hotfix](https://support.microsoft.com/help/3210110/on-demand-hotfix-update-package-for-sql-server-2016-cu3), available on demand  
 
### **The account that runs Launchpad has been changed or necessary permissions have been removed.** 

By default, [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] uses the following account on startup, which is configured by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] setup to have all necessary permissions:  `NT Service\MSSQLLaunchpad`

Therefore, if you assign a different account to the Launchpad or the right is removed by a policy on the SQL Server machine, the account might not have necessary permissions, and you might see this error: 

*ERROR_LOGON_TYPE_NOT_GRANTED
1385 (0x569) Logon failure: the user has not been granted the requested logon type at this computer.*

To give the necessary permissions to the new service account, use the **Local Security Policy** application and update the permissions on the account to include these permissions:
  + Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
  + Bypass traverse checking (SeChangeNotifyPrivilege)
  + Log on as a service (SeServiceLogonRight)
  + Replace a process-level token (SeAssignPrimaryTokenPrivilege)

### **User group for Launchpad is missing system right "Allow log in locally"**

During setup of R Services, SQL Server creates the Windows user group,  **SQLRUserGroup**, and by default provisions it with all rights necesssary for Launchpad to connect to SQL Server and run external script jobs.  
    
However, in organizations where more restrictive security policies are enforced, this right might be manually removed, or revoked by policy. If this happens, Launchpad can no longer connect to SQL Server, and R Services will be unable to function.
    
To correct the problem, ensure that the group **SQLRUserGroup** has the system right **Allow log on locally**.

For more information, see [Configure Windows Service Accounts and Permissions](https://msdn.microsoft.com/library/ms143504.aspx#Windows).
  
## Error: Unable to communicate with the Launchpad service

If you install R Services and enable the feature, but get this error when you try to run an R script, it might be that the Launchpad service for the instance has stopped running.

1. From a Windows command prompt, open the SQL Server Configuration Manager. For more information, see [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).
2. Right-click SQL Server Launchpad for the instance where R Services is not working, and select **Properties**.
3. Click the **Service** tab and verify that the service is running. If it is not, change the **Start Mode** to **Automatic** and click **Apply**.
4. Typically, restarting the service enables R scripts. If it does not, make a note of the path and arguments in the **Binary Path** property.
    + Review the rlauncher.config file and ensure that the working directory is valid
    + Ensure that the Windows group used by Launchpad has the ability to connect to the SQL Server instance, as described in the [previous section](#bkmk_LaunchpadTS). 
    + Restart the Launchpad service if you make any changes to service properties.


## Side by side installation not supported  
 Do not create a side-by-side installation using another version of R or other releases from Revolution Analytics.  

## Offline installation of R components for localized version of SQL Server 
If you are installing R Services on a computer that does not have Internet access, you must take two additional steps: you must download the R component installer to a local folder before you run SQL Server setup, and you must edit the installer file to ensure that the correct language is installed. 

The language identifier used for the R components must be the same as the SQL Server setup language being installed, or the **Next** button is disabled and you cannot complete setup.

For more information, see [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md). 
  
## Unable to launch runtime for R script
[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] creates a Windows users group that is used by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] to run R jobs. This user group must have the ability to log into the instance that is running R Services in order to execute R on the behalf of remote users who are using Windows integrated authentication.
  
 In an environment where the Windows group for R users (**SQLRUsers**) does not have this permission, you might see the following errors:  
  
-   When trying to run R scripts:  
  
     *Unable to launch runtime for 'R' script. Please check the configuration of the 'R' runtime.*  
  
     *An external script error occurred. Unable to launch the runtime.*  
  
-   Errors generated by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service:  
  
     *Failed to initialize the launcher RLauncher.dll*  
  
     *No launcher dlls were registered!*  
  
-   Security logs indicate that the account, NT SERVICE\MSSQLLAUNCHPAD was unable to log in.  
  
For information about how to give this user group the necessary permissions, see [Set up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md). 

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation. 

  
## Remote execution via ODBC   
 If you use a data science workstation and connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer to run R commands using the **RevoScaleR** functions, you might get an error when using ODBC calls that write data to the server. 
 
 The reason is the same as described in the previous section: at setup, R Services creates a group of worker accounts that are used for running R tasks. However, if these accounts cannot connect to the server, ODBC calls cannot be executed on your behalf. 
 
 Note that this limitation does not apply if you use SQL logins to run R scripts from a remote workstation, because the SQL login credentials are explicitly passed from the R client to the SQL Server instance and then to ODBC.
  
To enable implied authentication, you must give this group of worker accounts permissions as follows:  
    
1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] as an administrator on the instance where you will run R code. 

2. Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance name.
    ```sql
    USE [master]
    GO
    
    CREATE LOGIN [computername\SQLRUserGroup] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO  
    ```

For more information and the steps for doing this using the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] UI, see [Set up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).


## How to uninstall previous versions of R Services

It is important that you uninstall previous versions of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] and its related R components in the correct order, particularly if you installed any of the pre-reelease versions.

### Step 1. Run script to deregister Windows user group and components before uninstalling previous components
If you installed a pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], you must first run run the script `RegisteREext.exe` with the `/uninstall` argument.

By doing so, you deregister old components and remove the Windows user group associated with Launchpad. If you do not do this, you will be unable to create the Windows user group required for any new instances that you install.
  
For example, if you installed R Services on the default instance, run this command from the directory where the script is installed:  
  
~~~~
    RegisterRExt.exe /UNINSTALL  
~~~~ 
  
If you installed R Services  on a named instance, specify the instance name after *INSTANCE:*  
  
~~~~ 
RegisterRExt.exe /UNINSTALL /INSTANCE:<instancename>   
~~~~  

You might need to run the script more than once to remove all components.

**Important**: The default location for this script is different, depending on the pre-release version you installed. If you try to run the wrong version of the script, you might get an error. 

+ **CTP 3.1, 3.2, or 3.3**
    
    Additional steps are required to uninstall existing components. 
    1. First, download an updated version of the post-installation configuration script from the [Microsoft Download Center](http://go.microsoft.com/fwlink/?LinkId=723194). The updated script supports de-registration of older components. 
    2. Click the link and select **Save As** to save the script to a local folder. 
    3. Rename the existing script, and then copy the new script into the folder where the script will be executed. 

+ **RC0**
    
    The script file is located in this folder:  `C:\Program Files\Microsoft\MRO-for-RRE\8.0\R-3.2.2\library\RevoScaleR\rxLibs\x64`  

+ **Release versions (13.0.1601.5 or later)**

    No script is needed to install or configure components. The script should be used solely for removing older components. 


### Step 2. Uninstall any older versions of the Revolution Enterprise tools, including components installed with CTP releases.

The order of uninstallation of the R components is critical. Always uninstall [!INCLUDE[rsql_rre-noversion](../../includes/rsql-rre-noversion-md.md)] first. Then, uninstall [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)].  

 If you mistakenly uninstall R Open first and then get an error when trying to uninstall Revolution R Enterprise, one workaround is to reinstall Revolution R Open or Microsoft R Open, and then uninstall both components in the correct order.  

### Step 3. Uninstall any other version of Microsoft R Open.

Finally, uninstall all other versions of Microsoft R Open.
 
### Step 4. Upgrade SQL Server  
  
After all pre-release components have been uninstalled, restart the computer. This is a requirement of SQL Server setup and you will not be able to proceed with an updated installation until a restart is completed.     

After this is done, run SQL Server setup and follow these instructions to install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]: [Set up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).  
  
No additional components are needed; the R packages and connectivity packages are installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. 

## Problems uninstalling older versions of R  
In some cases, older versions of Revolution R Open or Revolution R Enterprise are not completely removed by the uninstall process.  
  
 If you have problems removing an older version, you can also edit the registry to remove related keys.  
  
 Open the Windows Registry, and locate this key: `HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall`.  
  
 Delete any of the following entries, if present, and if the key contains only a single value `sEstimatedSize2`:  
  
-   E0B2C29E-B8FC-490B-A043-2CAE75634972        (for 8.0.2)  
  
-   46695879-954E-4072-9D32-1CC84D4158F4        (for 8.0.1)  
  
-   2DF16DF8-A2DB-4EC6-808B-CB5A302DA91B        (for 8.0.0)  
  
-   5A2A1571-B8CD-4AAF-9303-8DF463DABE5A        (for 7.5.0)  


  
  
## Upgrade of R Server (Standalone) to RC3 requires uninstallation using the RC2 setup utility

 Microsoft R Server (Standalone) first became available in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] RC2. To upgrade to the RC3 version of Microsoft R Server, you must first uninstall using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] RC2 setup, and then reinstall using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] RC3 setup.  
  
1.  Uninstall R Server (Standalone) for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] RC2 using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup.  
  
2.  Upgrade [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to RC3 and select the option to install R Services (In-Database). This upgrades the instance of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] to RC3; no additional configuration is necessary.  
  
3.  Run [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] setup for RC3 once more, and install Microsoft R Server (Standalone).  

> [!NOTE] 
> This workaround is not required when upgrading to the RTM version of Microsoft R Server. 
> 
> For additional issues related to Microsoft R Server, see [Microoft R Server Release Notes](https://msdn.microsoft.com/microsoft-r/notes/r-server-notes).

## See Also 
 
 [Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)   
 [Getting Started with Microsoft R Server &#40;Standalone&#41;](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)  
  
  
