---
title: Manage and integrate machine learning workloads in SQL Server | Microsoft Docs
description: As a SQL Server DBA, review the administrative tasks for deploying a machine learning R and Python subsystem on a database engine instance.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/10/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Manage and integrate machine learning workloads on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is for SQL Server database administrators who are responsible for deploying an efficient data science infrastructure on a shared server asset. It frames the administrative operations relevant to database administrators who need to manage R and Python code execution on SQL Server.

## Isolation and containment

As a DBA, understanding the mechanisms by which isolation and integration of R and Python workloads is attained on the database engine is an imperative.

Integration is primarily through the data definition language and the data security model:

+ [Stored procedures](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql?view=sql-server-2017) equipped with the ability to accept R and Python code as input parameters. 
+ Built-in functions (namely, [PREDICT](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql)) that can consume a previously trained data model. 
+ Security integration. Database logins and data access permissions apply to user-invoked scripts utilizing that same data. If users cannot access data through a query, they can't access it through script either.

Understanding the isolation mechanism also has practical implications for the DBA. Isolation is implemented primarily through a dual processing architecture, separating external processing from core processing to ensure that R and Python scripts don't interfere directly with OLTP functions. In Task Manager, you can monitor R and Python processes, executing as least-priviledged worker identities, separate from the SQL Server service account.

Running R and Python processes in individual low-privilege accounts has the following benefits:

+ Provides isolation between core engine processes and sessions of R and Python. You can terminate an R or Python process without impacting core database operations. 

+ Reduces privileges of the external script runtime processes on the host computer

Least-privilege accounts are created during setup and placed in a Windows *user account pool* called **SQLRUserGroup**. By default, this group has permissions to use executables, libraries, and built-in datasets in the program folder for R and Python under SQL Server. 

As a DBA, you can use SQL Server data security to specify who has permission to install and manage packages, execute scripts, and that data used in jobs is managed under the same security roles that control access through T-SQL queries. As a system administrator, you can explicitly deny **SQLRUserGroup** access to sensitive data on the local server by creating ACLs.

>[!NOTE]
> By default, the **SQLRUserGroup** does not have a login or permissions in SQL Server itself. Should worker accounts require a login for data access, you must create it yourself. A scenario that specifically calls for the creation of a login is to support requests from a script in execution, for data or operations on the database engine instance, when the user identity is a Windows user and the connection string specifies a trusted user. For more information, see [Add SQLRUserGroup as a database user](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

## Adapations/extensions

Data science introduces requirements for package deployment and administration. For a data scientist, it's common practice to include open-source and third-party packages providing function libraries used for solving specific problems. Some of those packages will have dependencies on other packages, in which case you might need to evaluate and install multiple packages to get the required functionality.

As a DBA responsible for a server asset, deploying arbitrary R and Python packages into a production server represents an unfamiliar challenge. Before adding packages, you should assess whether the functionality provided by the external package is truly required, with no equivalent in the built-in R libraries and Python libraries installed by SQL Server Setup. In some cases, a data scientist might be able to [build and run solutions on an external workstation](../r/set-up-a-data-science-client.md), retrieving data from SQL Server, but with all analysis performed locally on the workstation instead of on the server itself. 

If you subsequently determine that external library functions are necessary and do not pose a risk to server operations or data as a whole, you can choose from multiple methodologies to add packages. To learn more, see [Install Python packages in SQL Server](../python/install-additional-python-packages-on-sql-server.md) and [Install R packages in SQL Server](install-additional-r-packages-on-sql-server.md).

## Allocate system resources

By default, the R and Python sessions are allowed up to 20% of memory usage on the host system. To readjust memory, CPU, and I/O allocations, you can create resource pools to precisely articulate the levels of computing power assigned to external script execution. For more information, see [Resource governance to modify resource levels for R and Python execution](resource-governance.md).

## Next steps

+ Review the concepts and components of the [extensibility architecture](../concepts/extensibility-framework.md) and [security](../concepts/security.md) for more background.

+ As part of feature installation, you might already be familiar with end-user data access control, but if not, see [Grant user permissions to SQL Server machine learning](../security/user-permissions.md) for details. 

+ Learn how to adjust the system resources for computation-intensive machine learning workloads. For more information, see [How to create a resource pool](../administration/how-to-create-a-resource-pool.md).
