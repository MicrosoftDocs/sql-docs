---
title: SQL Server Launchpad service account configuration | Microsoft Docs
description: How to modify the SQL Server Launchpad service account used for external script execution on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/05/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# SQL Server Launchpad service configuration
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

A separate [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service is created for database engine instance to which you have added SQL Server machine learning (R or Python) integration.

## Service account configuration

By default, SQL Server Launchpad is configured to run under **NT Service\MSSQLLaunchpad**, which is provisioned with all necessary permissions to run external scripts. Stripping permissions from this account can result in Launchpad failing to start or to access the SQL Server instance where external scripts should be run.

Permission required for this account are listed below. If you modify the service account, be sure to use the **Local Security Policy** application to include these permissions:

+ Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
+ Bypass traverse checking (SeChangeNotifyPrivilege)
+ Log on as a service (SeServiceLogonRight)
+ Replace a process-level token (SeAssignPrimaryTokenPrivilege)

For more information about permissions required to run SQL Server services, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

<a name="bkmk_ChangingConfig"></a> 

## Configuration properties

1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). 

2. Right-click SQL Server Launchpad and select **Properties**.

    + To change the service account, click the **Log On** tab.

    + To increase the number of users, click the **Advanced** tab.

> [!Note]
> In early versions of SQL Server 2016 R Services, you could change some properties of the service by editing the [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] configuration file. This file is no longer used for changing configurations. SQL Server Configuration Manager is the right approach for changes to service configuration, such as the service account and number of users.

#### Debug settings

A few properties can only be changed by using the Launchpad's configuration file, which might be useful in limited cases, such as debugging. The configuration file is created during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup and by default is saved as a plain text file in the following location: `<instance path>\binn\rlauncher.config`

You must be an administrator on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to make changes to this file. If you edit the file, we recommend that you make a backup copy before saving changes.

The following table lists the advanced settings for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], with the permissible values. 

|**Setting name**|**Type**|**Description**|
|----|----|----|
|JOB\_CLEANUP\_ON\_EXIT|Integer |This is an internal setting only – do not change this value. </br></br>Specifies whether the temporary working folder created for each external runtime session should be cleaned up after the session is completed. This setting is useful for debugging. </br></br>Supported values are **0** (Disabled) or **1** (Enabled). </br></br>The default is 1, meaning log files are removed on exit.|
|TRACE\_LEVEL|Integer |Configures the trace verbosity level of  MSSQLLAUNCHPAD for debugging purposes. This affects trace files in the path specified by the LOG_DIRECTORY setting. </br></br>Supported values are: **1** (Error), **2** (Performance), **3** (Warning), **4**  (Information). </br></br>The default is 1, meaning output warnings only.|

All settings take the form of a key-value pair, with each setting on a separate line. For example, to change the trace level, you would add the line `Default: TRACE_LEVEL=4`.

## See also

[Extensibility framework](../concepts/extensibility-framework.md)
