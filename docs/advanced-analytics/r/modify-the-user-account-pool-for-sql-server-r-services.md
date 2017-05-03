---
title: "Modify the User Account Pool for SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 58b79170-5731-46b5-af8c-21164d28f3b0
caps.latest.revision: 19
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Modify the User Account Pool for SQL Server R Services
  As part of the installation process for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], a new Windows *user account pool* is created to support execution of tasks by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. The purpose of these worker accounts is to isolate concurrent execution of R scripts by different SQL users. 

This topic describes the default configuration, security and capacity for the worker accounts, and how to change the default configuration.

## Worker Accounts Used by R Services   

The Windows account group is created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup for  each instance on which R Services is installed. Therefore, if you have installed multiple instance that support R, there will be multiple user groups.

-   In a default instance, the group name is **SQLRUserGroup**. 
-   In a named instance, the default group name is suffixed with the instance name: for example, **SQLRUserGroupMyInstanceName**. 

By default, the user account pool contains 20 user accounts. In most cases, 20 is more than adequate to support R sessions, but you can change the number of accounts.
-  In a default instance, the individual accounts are named **MSSQLSERVER01** through **MSSQLSERVER20**.  
-   For a named instance, the individual accounts are named after the instance name: for example, **MyInstanceName01** through **MyInstanceName20**.  

### <a name = "HowToChangeGroup"> </a>How to change the number of R worker accounts

To modify the number of users in the account pool, you must edit the properties of the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as described below.  
  
Passwords associated with each user account are generated at random, but you can change them later, after the accounts have been created.  
  
1. Open SQL Server Configuration Manager and select **SQL Server Services**.
2. Double-click the SQL Server Launchpad service and stop the service if it is running. 
3.  On the **Service** tab, make sure that the Start Mode is set to Automatic. R scripts will fail if the Launchpad is not running.
4.  Click the **Advanced** tab and edit the value of **External Users Count** if necessary. This setting controls how many different SQL users can run queries concurrently in R. The default is 20 accounts.
5. Optionally, you can set the option **Reset External Users Password** to _Yes_ if your organization has a policy that requires changing passwords on a regular basis. Doing this will regenerate the encrypted passwords that Launchpad maintains for the user accounts. For more information, see [Enforcing Password Policy](#bkmk_EnforcePolicy).    
6.  Restart the service.  

## Managing R Workload

The number of accounts in this pool determines how many R sessions can be active simultaneously.  By default, 20 accounts are created, meaning that 20 different users can have active R sessions at one time. If you anticipate the need for more concurrent users executing R scripts, you can increase the number of worker accounts. 

When the same user executes multiple R scripts concurrently, all the sessions run by that user will use the same worker account. For example, a single user might have 100 different R scripts running concurrently, as long as resources permit, using a single worker account.

The number of worker accounts that you can support, and the number of concurrent R sessions that any single user can run, is limited only by server resources.  Typically, memory is the first bottleneck that you will encounter when using the R runtime.

In R Services, the resources that can be used by R scripts are governed by SQL Server. We recommend that you monitor resource usage using SQL Server DMVs, or look at performance counters on the associated Windows job object, and adjust server memory use accordingly. 
 
If you have SQL Server Enterprise Edition, you can allocate resources used for running R scripts by configuring an [external resource pool](../../advanced-analytics/r-services/how-to-create-a-resource-pool-for-r.md). 

For additional information about managing R script capacity, see these articles:

- [SQL Server Configuration for R Services](../../advanced-analytics/r-services/sql-server-configuration-r-services.md)
-  [Performance Case Study for R Services](../../advanced-analytics/r-services/performance-case-study-r-services.md)

## Security

Each user group is associated with the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service on a specific instance and cannot support R jobs that run on other instances.

For each worker account, while the session is active, a temporary folder is created to store the script objects, intermediate results, and other information used by R and SQL Server during R script execution. These working files, located under the ExtensibilityData folder, are access-restricted to administrators, and are cleaned up by SQL Server after the script completes. 

For more information, see [Security Overview](../../advanced-analytics/r-services/security-overview-sql-server-r.md).

### <a name="bkmk_EnforcePolicy"></a>Enforcing password policy

If your organization has a policy that requires changing passwords on a regular basis,  you may need to force the Launchpad service to regenerate the encrypted passwords that Launchpad maintains for its worker accounts.  

To enable this setting and force password refresh, open the **Properties** pane for the Launchpad service in SQL Server Configuration Manager, click **Advanced**, and change **Reset External Users Password** to **Yes**. When you apply this change, the passwords will immediately be regenerated for all user accounts. To use R script after this change, you must restart the Launchpad service, at which time it will read the newly generated passwords. 

To reset passwords at regular intervals, you can either set this flag manually or use a script.

### Additional permission required to support remote compute contexts

By default, the group of R worker accounts does **not** have login permissions on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with which it is associated. This can be a problem if any R users connect to SQL Server from a remote client to run R scripts, or if a script uses ODBC to get additional data. 

To ensure that these scenarios are supported, the database administrator must provide the group of R worker accounts with permission to log into the SQL Server instance where R scripts will be run (**Connect to** permissions). This is referred to as *implied authentication*, and enables SQL Server to run the R scripts using the credentials of he remote user.

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation, because the SQL login credentials are explicitly passed from the R client to the SQL Server instance and then to ODBC.


### How to enable implied authentication

1. Open SQL Server Management Studio as an administrator on the instance where you will run R code.

2. Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance name.

    ```sql
    USE [master]
    GO
    
    CREATE LOGIN [computername\SQLRUserGroup] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO  
    ````


  
## See Also  
 [Configuration (SQL Server R Services)](../../advanced-analytics/r-services/configuration-sql-server-r-services.md)
  
