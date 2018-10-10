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

This article is for SQL Server database administrators who are responsible for deploying an efficient infrastructure for data science onto a system that simultaneously serves other data and computating needs across an organization. It describes administrative operations relevant to database administrators who need to manage R and Python code execution on SQL Server.

## Provide isolation and containment

The extensibility framework supporting R and Python machine learning processes is engineered for both integration and isolation. 

Integration is through T-SQL's data definition language that equips stored procedures that the ability to accept R and Python code as input paramters, and provides built-in functions like PREDICT that can consume a previously trained data model. Likewise, the data security model with database logins and roles, is another layer of integration. The same permssions on data access apply to scripts utilizing that same data.

Isolation is equally essential, and a dual architecture separating external processing from core processing ensures that R and Python scripts don't interfer directly with OLTP functions. In Task Manager, you can monitor R and Python processes, executing as least-priviledged worker identities, separate from the SQL Server service account.

To summarize the architectural features that provide isolation measures:

+ The extensibility framework runs R and Python processes separately. Low privilege physical user accounts are used to contain and isolate external script activity. You can terminate an R or Python process without impacting core database operations. 

+ The database administrator can specify who has permission to install and manage packages, execute scripts, and that data used in jobs is managed under the same security roles that control access through T-SQL queries.

## Allocate system resources

By default, the R and Python sessions are allowed up to 20% of memory usage on the host system. To re-adjust memory, CPU, and I/O allocations, you can create resource pools to precisely articulate the levels of computing power assigned to external script execution. For more information, see [Resource governance to modify resource levels for R and Python execution](resource-governance.md).

## Adapations/extensions

Data science introduces requirements for package deployment and administration. For a data scientist, it's common practice to include open-source and third-party packages providing function libraries used for solving specific problems. Some of those packages will have dependencies on other packages, in which case you might to evaluate and install multiple packages to get the required functionality.

As a DBA responsible for a server asset, deploying arbitrary R and Python packages into a production server represents an unfamiliar challenge. Before adding packages, you should assess whether the functionality provided by the external package is truly required, with no equivalent in the built-in R libraries and Python libraries installed by SQL Server Setup. In some cases, a data scientist might be able to [build and run solutions on an external workstation](../r/set-up-a-data-science-client.md), retrieving data from SQL Server, but with all analysis performed locally on the workstation instead of on the server itself. 

If you subsequently determine that external library functions are necessary and do not pose a risk to server operations or data as a whole, you can choose from multiple methodologies to add packages. To learn more, see [Install Python packages in SQL Server](../python/install-additional-python-packages-on-sql-server.md) and [Install R packages in SQL Server](install-additional-r-packages-on-sql-server.md).

## Next steps

Review the concepts and components of the [extensibility architecture](../concepts/extensibility-framework.md) and [security](../concepts/security.md). As part of feature installation, you might already be familiar with end-user data access control, but if not, see [Grant user permissions to SQL Server machine learning](../security/user-permissions.md) for details. Finally, for information on how to adjust the system resources for computation-intensive machine learning workloads, see [How to create a resource pool](../administration/how-to-create-a-resource-pool.md).
