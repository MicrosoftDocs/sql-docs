---
title: Differences in SQL Server 2019 Machine Learning Services installation | Microsoft Docs
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

On Windows, SQL Server 2019 Setup changes the isolation mechanism for external processes running Java, R, or Python tasks by replacing local worker accounts with [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-for-legacy-applications-). AppContainers are a containment feature for client apps running on Windows. If you are adding programming extensions or machine learning to a database engine instance, this article explains how Setup provisions the server to contain those processes. 

Although process isolation has changed, the mechanics of installation remain the same. If you installed the previous version, you'll notice that the Installation wizard and command-line parameters are unchanged in SQL Server 2019. For help with installation, see [Install SQL Server Machine Learning Services](sql-machine-learning-services-windows-install.md).

There are no specific action items for the administrator as a result of this change.

+ On a new or upgraded server, extensions for R, Python, and Java use the new isolation model automatically. 
+ **SQLRUserGroup** continues to be used in Access Control Lists (ACLs). 
+ **SQL Server Launchpad service** continues in its role of starting up external processes, but is now also running those processes in individual AppContainers, one per process. 

## About AppContainer isolation

In previous releases, **SQLRUserGroup** contained a pool of local Windows user accounts (MSSQLSERVER00-MSSQLSERVER20) for isolating and running external processes. When an external process was needed, SQL Server Launchpad service would take an available account and use it to run a process. 

In SQL Server 2019, Setup no longer creates worker accounts. Instead, isolation is achieved through [AppContainers](https://docs.microsoft.com/windows/desktop/secauthz/appcontainer-isolation), which do a better job of restricting access to resources without the overhead of account management. At run time, when embedded script or code is detected in a stored procedure or query, Launchpad instantiates an AppContainer to contain the external process, but the process runs under the Launchpad service identity.

As implemented by SQL Server, AppContainers are an internal mechanism. While you won't see physical evidence of AppContainers in Process Monitor, you can find them in outbound firewall rules created by Setup to prevent processes from making network calls.

In SQL Server 2019, the only member of **SQLRUserGroup** is the SQL Server Launchpad service account. As with previous releases, the **SQL Restricted User Group (SQLRUserGroup)** continues to provide read and execute permissions on executables in the SQL Server **Binn**, **R_SERVICES**, and **PYTHON_SERVICES** directories. 

> [!NOTE]
> On SQL Server, stored procedures and T-SQL queries execute as a database user, but that user's security token is not used on new processes. Instead, embedded script or code executes under a different identity. In previous releases, the process identity was a worker account. In SQL Server 2019, the process identity is the Launchpad service account, with isolation provided by AppContainers.

Summarized, the main differences with AppContainer isolation are:

+ Physical accounts worker accounts under **SQLRUserGroup** are no longer created. This is beneficial for machines with policies that disable local users from logging on, and with passwords that expire. 
+ **SQLRUserGroup** continues to be granted 'read and execute' permissions to the SQL Server **Binn**, **R_SERVICES**, and **PYTHON_SERVICES** directories, but membership now consists of just the SQL Server Launchpad service.
+ All external scripts and code executed from [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) follow the new security model. This applies to R, Python, and the new Java language extension introduced in SQL Server 2019.

As before, additional configuration is still required for *implied authentication*, where script or code has to connect back to SQL Server to retrieve data or resources. The additional configuration is creating a database login for **SQLRUserGroup**. For more information, see [Add SQLRUserGroup as a database user](../r/add-sqlrusergroup-to-database.md)

> [!NOTE]
> Wehn code or script passes connection instructions, such as an ODBC connection string, back to SQL Server, the server refuses the request from Launchpad service by default. This occurs because there is no login created for Launchpad or the parent **SQLRUserGroup**. If you require an impersonation token for an identity other than the original caller, a database login for **SQLRUserGroup** is required.

## Firewall rules created by Setup

In previous releases, Setup created one outbound rule for **SQLRUserGroup** that denied network access to its members, with each worker account listed as a local principle subject to the rule.

In SQL Server 2019, SQL Server Setup creates individual firewall rules for each AppContainer (20 by default). Naming conventions for the firewall rule name are **Block network access for AppContainer-00 in SQL Server instance MSSQLSERVER**, where 00 is the number of the AppContainer (00-20 by default), and MSSQLSERVER is the name of the SQL Server instance. 

## Symbolic link created by Setup

A symbolic link is created to the current default **R_SERVICES location** as part of SQL Server Setup. To avoid creating this link, grant 'all application packages' read permission to the hierarchy leading up to the **R_SERVICES** folder.


## See also

+ [Install SQL Server Machine Learning Services on Windows](sql-machine-learning-services-windows-install.md)

+ [Install SQL Server 2019 Machine Learning Services on Linux](../../linux/sql-server-linux-setup-machine-learning.md)
