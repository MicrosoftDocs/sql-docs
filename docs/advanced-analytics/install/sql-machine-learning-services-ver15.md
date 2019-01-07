---
title: Differences in SQL Server 2019 - SQL Server Machine Learning Services
description: Learn what's new for R and Python SQL Server machine learning extensions in the SQL Server 2019 preview release.
ms.prod: sql
ms.technology: machine-learning
ms.date: 09/08/2018  
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Differences in SQL Server Machine Learning Services installation in SQL Server 2019  
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

On Windows, SQL Server 2019 Setup changes the isolation mechanism for external processes. This change replaces local worker accounts with [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation), an isolation technology for client applications running on Windows. 

There are no specific action items for the administrator as a result of the modification. On a new or upgraded server, all external scripts and code executed from [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) follow the new isolation model automatically. This applies to R, Python, and the new Java language extension introduced in SQL Server 2019.

Summarized, the main differences in this release are:

+ Local user accounts under **SQL Restricted User Group (SQLRUserGroup)** are no longer created or used to run external processes. AppContainers replace them.
+ **SQLRUserGroup** membership has changed. Instead of multiple local user accounts, membership consists of just the SQL Server Launchpad service account. R, Python, and Java processes now execute under the Launchpad service identity, isolated through AppContainers.

Although the isolation model has changed, the Installation wizard and command line parameters remain the same in SQL Server 2019. For help with installation, see [Install SQL Server Machine Learning Services](sql-machine-learning-services-windows-install.md).

## About AppContainer isolation

In previous releases, **SQLRUserGroup** contained a pool of local Windows user accounts (MSSQLSERVER00-MSSQLSERVER20) used for isolating and running external processes. When an external process was needed, SQL Server Launchpad service would take an available account and use it to run a process. 

In SQL Server 2019, Setup no longer creates local worker accounts. Instead, isolation is achieved through [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation). At run time, when embedded script or code is detected in a stored procedure or query, SQL Server calls Launchpad with a request for an extension-specific launcher. Launchpad invokes the appropriate runtime environment in a process under its identity, and instantiates an AppContainer to contain it. This change is beneficial because local account and password management is no longer required. Also, on installations where local user accounts are prohibited, elimination of the local user account dependency means you can now use this feature.

As implemented by SQL Server, AppContainers are an internal mechanism. While you won't see physical evidence of AppContainers in Process Monitor, you can find them in outbound firewall rules created by Setup to prevent processes from making network calls.

## Firewall rules created by Setup

By default, SQL Server disables outbound connections by creating firewall rules. In the past, these rules were based on local user accounts, where Setup created one outbound rule for **SQLRUserGroup** that denied network access to its members (each worker account was listed as a local principle subject to the rule_. 

As part of the move to AppContainers, there are new firewall rules based on AppContainer SIDs: one for each of the 20 AppContainers created by SQL Server Setup. Naming conventions for the firewall rule name are **Block network access for AppContainer-00 in SQL Server instance MSSQLSERVER**, where 00 is the number of the AppContainer (00-20 by default), and MSSQLSERVER is the name of the SQL Server instance. 

> [!Note]
> If network calls are required, you can disable the outbound rules in Windows Firewall.

## Program file permissions

As with previous releases, the **SQLRUserGroup** continues to provide read and execute permissions on executables in the SQL Server **Binn**, **R_SERVICES**, and **PYTHON_SERVICES** directories. In this release, the only member of **SQLRUserGroup** is the SQL Server Launchpad service account.  When Launchpad service starts an R, Python, or Java execution environment, the process runs as LaunchPad service.

## Implied authentication

As before, additional configuration is still required for *implied authentication* in cases where script or code has to connect back to SQL Server using trusted authentication to retrieve data or resources. The additional configuration involves creating a database login for **SQLRUserGroup**, whose sole member is now the single SQL Server Launchpad service account instead of multiple worker accounts. For more information about this task, see [Add SQLRUserGroup as a database user](../security/add-sqlrusergroup-to-database.md).


## Symbolic link created by Setup

A symbolic link is created to the current default **R_SERVICES**  and **PYTHON_SERIVCES** as part of SQL Server Setup. If you don't want to create this link, an alternative is to grant 'all application packages' read permission to the hierarchy leading up to the folder.


## See also

+ [Install SQL Server Machine Learning Services on Windows](sql-machine-learning-services-windows-install.md)

+ [Install SQL Server 2019 Machine Learning Services on Linux](../../linux/sql-server-linux-setup-machine-learning.md)
