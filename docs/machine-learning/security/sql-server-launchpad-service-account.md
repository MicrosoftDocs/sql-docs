---
title: Launchpad account configuration
description: How to modify the SQL Server Launchpad service account used for external script execution on SQL Server.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 09/16/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# SQL Server Launchpad service configuration
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

The [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] is a service that manages and executes external scripts, similar to the way that the full-text indexing and query service launches a separate host for processing full-text queries.

For more information, see the Launchpad sections in [Extensibility architecture in SQL Server Machine Learning Services](../../machine-learning/concepts/extensibility-framework.md#launchpad) and [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../machine-learning/concepts/security.md#launchpad).

## Account permissions

By default, SQL Server Launchpad is configured to run under **NT Service\MSSQLLaunchpad**, which is provisioned with all necessary permissions to run external scripts. Removing permissions from this account can result in Launchpad failing to start or to access the SQL Server instance where external scripts should be run.

If you modify the service account, be sure to use the [Local Security Policy console](/windows/security/threat-protection/security-policy-settings/how-to-configure-security-policy-settings).

Permissions required for this account are listed in the following table.

| Group policy setting | Constant name |
|----------------------|---------------|
| [Adjust memory quotas for a process](/windows/security/threat-protection/security-policy-settings/adjust-memory-quotas-for-a-process) | SeIncreaseQuotaPrivilege | 
| [Bypass traverse checking](/windows/security/threat-protection/security-policy-settings/bypass-traverse-checking) | SeChangeNotifyPrivilege | 
| [Log on as a service](/windows/security/threat-protection/security-policy-settings/log-on-as-a-service) | SeServiceLogonRight | 
| [Replace a process-level token](/windows/security/threat-protection/security-policy-settings/replace-a-process-level-token) | SeAssignPrimaryTokenPrivilege | 

For more information about permissions required to run SQL Server services, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

<a name="bkmk_ChangingConfig"></a> 

## Configuration properties

Typically, there is no reason to modify service configuration. Properties that could be changed include the service account, the count of external processes (20 by default), or the password reset policy for worker accounts.

1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

2. Under SQL Server Services, right-click SQL Server Launchpad and select **Properties**.
  + To change the service account, click the **Log On** tab.
  + To increase the number of users, click the **Advanced** tab and change the **Security Contexts Count**.

> [!Note]
> In early versions of SQL Server 2016 R Services, you could change some properties of the service by editing the [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] configuration file. This file is no longer used for changing configurations. SQL Server Configuration Manager is the right approach for changes to service configuration, such as the service account and number of users.

## Debug settings

A few properties can only be changed by using the Launchpad's configuration file, which might be useful in limited cases, such as debugging. The configuration file is created during the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup and by default is saved as a plain text file in `<instance path>\binn\rlauncher.config`.

You must be an administrator on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to make changes to this file. If you edit the file, we recommend that you make a backup copy before saving changes.

The following table lists the advanced settings for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], with the permissible values.

|**Setting name**|**Type**|**Description**|
|----|----|----|
|JOB\_CLEANUP\_ON\_EXIT|Integer |This is an internal setting only - do not change this value. </br></br>Specifies whether the temporary working folder created for each external runtime session should be cleaned up after the session is completed. This setting is useful for debugging. </br></br>Supported values are **0** (Disabled) or **1** (Enabled). </br></br>The default is 1, meaning log files are removed on exit.|
|TRACE\_LEVEL|Integer |Configures the trace verbosity level of  MSSQLLAUNCHPAD for debugging purposes. This affects trace files in the path specified by the LOG_DIRECTORY setting. </br></br>Supported values are: **1** (Error), **2** (Performance), **3** (Warning), **4**  (Information). </br></br>The default is 1, meaning output errors only.|

All settings take the form of a key-value pair, with each setting on a separate line. For example, to change the trace level, you would add the line `Default: TRACE_LEVEL=4`.

<a name="bkmk_EnforcePolicy"></a>

## Enforcing password policy

If your organization has a policy that requires changing passwords on a regular basis,  you may need to force the Launchpad service to regenerate the encrypted passwords that Launchpad maintains for its worker accounts.

To enable this setting and force password refresh, open the **Properties** pane for the Launchpad service in SQL Server Configuration Manager, click **Advanced**, and change **Reset External Users Password** to **Yes**. When you apply this change, the passwords will immediately be regenerated for all user accounts. To run an external script after this change, you must restart the Launchpad service, at which time it will read the newly generated passwords.

To reset passwords at regular intervals, you can either set this flag manually or use a script.

## Next steps

+ [Extensibility framework](../concepts/extensibility-framework.md)
+ [Security overview](../concepts/security.md)