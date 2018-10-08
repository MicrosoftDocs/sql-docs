---
title: Advanced configuration for SQL Server Machine Learning Services | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Advanced configuration options for SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes changes that you can make after setup, to modify the configuration of the external script runtime and other services associated with machine learning in SQL Server.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

##  <a name="bkmk_Provisioning"></a> Provision additional user accounts for machine learning

External script processes in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] run in the context of low-privilege local user accounts. Running these processes in individual low-privilege accounts has the following benefits:

+ Reduces privileges of the external script runtime processes on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer
+ Provides isolation between sessions of an external runtime such as R or Python.

As part of setup, a new Windows *user account pool* is created that contains the local user accounts required for running external runtime processes, such as R or Python. You can modify the number of users as needed to support machine learning tasks. 

Additionally, your database administrator must give this group permission to connect to any instance where machine learning has been enabled. For more information, see [Security configuration for SQLRUserGroup](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

To protext sensitive resources on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can define an access control list (ACL) for this group. By specifying resources that the group is denied access to, you can prevent access by external processes such as the R or Python runtimes.

+ The user account pool is linked to a specific instance. A separate pool of worker accounts is needed for each instance on which machine learning has been enabled. Accounts cannot be shared between instances.

+ User account names in the pool are of the format SQLInstanceName*nn*. For example, if you are using the default instance for machine learning, the user account pool supports account names such as MSSQLSERVER01, MSSQLSERVER02, and so forth.

+ The size of the user account pool is static and the default value is 20. The number of external runtime sessions that can be launched simultaneously is limited by the size of this user account pool. To change this limit, an administrator should use SQL Server Configuration Manager.

For more information about how to make changes to the user account pool, see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../../advanced-analytics/administration/modify-user-account-pool.md).

##  <a name="bkmk_ManagingMemory"></a> Manage memory used by external script processes

By default, the external script runtimes for machine learning are limited to no more than 20% of total machine memory. It depends on your system, but in general, you might find this limit inadequate for serious machine learning tasks such as training a model or predicting on many rows of data. 

To support machine learning, an administrator can increase this limit. When you do so, you might need to reduce the amount of memory reserved for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or for other services. You should also consider using Resource Governor to define an external resource pool or pools, so that you can allocate specific resource pools to R or Python jobs.

For more information, see [Resource governance for machine learning](../../advanced-analytics/r/resource-governance-for-r-services.md).


## <a name="bkmk_Launchpad"></a>Modify the Launchpad service account

A separate [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service is created for each instance on which you have configured the machine learning services.

By default, the Launchpad is configured to run using the account, NT Service\MSSQLLaunchpad, which is provisioned with all necessary permissions to run R scripts. However, if you change this account, the Launchpad might not be able to start or to access the SQL Server instance where external scripts should be run.

If you modify the service account, be sure to use the **Local Security Policy** application and update the permissions on each service account to include these permissions:

+ Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
+ Bypass traverse checking (SeChangeNotifyPrivilege)
+ Log on as a service (SeServiceLogonRight)
+ Replace a process-level token (SeAssignPrimaryTokenPrivilege)

For more information about permissions required to run SQL Server services, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

##  <a name="bkmk_ChangingConfig"></a> Change advanced service options

In early versions of SQL Server 2016 R Services, you could change some properties of the service by editing the [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] configuration file. 

However, this file is no longer used for changing configurations. We recommend that you use SQL Server Configuration Manager to effect changes to service configuration, such as the service account and number of users.

**To modify Launchpad configuration**

1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). 
2. Right-click SQL Server Launchpad and select **Properties**.

    + To change the service account, click the **Log On** tab.

    + To increase the number of users, click the **Advanced** tab.


**To modify debug settings**

A few properties can only be changed by using the Launchpad's configuration file, which might be useful in limited cases, such as debugging. The  configuration file is created during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup and by default is saved as a plain text file in the following location: `<instance path>\binn\rlauncher.config`

You must be an administrator on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to make changes to this file. If you edit the file, we recommend that you make a backup copy before saving changes.

The following table lists the advanced settings for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], with the permissible values. 

|**Setting name**|**Type**|**Description**|
|----|----|----|
|JOB\_CLEANUP\_ON\_EXIT|Integer |This is an internal setting only – do not change this value. </br></br>Specifies whether the temporary working folder created for each external runtime session should be cleaned up after the session is completed. This setting is useful for debugging. </br></br>Supported values are **0** (Disabled) or **1** (Enabled). </br></br>The default is 1, meaning log files are removed on exit.|
|TRACE\_LEVEL|Integer |Configures the trace verbosity level of  MSSQLLAUNCHPAD for debugging purposes. This affects trace files in the path specified by the LOG_DIRECTORY setting. </br></br>Supported values are: **1** (Error), **2** (Performance), **3** (Warning), **4**  (Information). </br></br>The default is 1, meaning output warnings only.|

All settings take the form of a key-value pair, with each setting on a separate line. For example, to change the trace level, you would add the line `Default: TRACE_LEVEL=4`.

## See Also

[Security overview for the extensibility framework in SQL Server Machine Learning Services](../../advanced-analytics/concepts/security.md)
