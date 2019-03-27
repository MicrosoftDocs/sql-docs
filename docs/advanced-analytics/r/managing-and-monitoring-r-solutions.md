---
title: Manage and integrate machine learning workloads - SQL Server Machine Learning Services
description: As a SQL Server DBA, review the administrative tasks for deploying a machine learning R and Python subsystem on a database engine instance.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/10/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Manage and integrate machine learning workloads on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is for SQL Server database administrators who are responsible for deploying an efficient data science infrastructure on a server asset supporting multiple workloads. It frames the administrative problem space relevant to the management of R and Python code execution on SQL Server. 

## What is feature integration

R and Python machine learning is provided by [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md) as an extension to a database engine instance. Integration is primarily through the security layer and the data definition language, summarized as follows:

+ Stored procedures are equipped with the ability to accept R and Python code as input parameters. Developers and data scientists can use a [system stored procedure](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql?view=sql-server-2017) or create a custom procedure that wraps their code.
+ T-SQL functions (namely, [PREDICT](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql)) that can consume a previously trained data model. This capability is available through T-SQL. As such, it can called on a system that does not specifically have the machine learning extensions installed.
+ Existing database logins and role-based permissions cover the user-invoked scripts utilizing that same data. As a general rule, if users cannot access data through a query, they can't access it through script either.

## Feature availability

R and Python integration becomes available through a succession of steps. The first one is setup, when you [include or add the **Machine Learning Services**](../install/sql-machine-learning-services-windows-install.md) feature to a database engine instance. As a subsequent step, you must enable external scripting on the database engine instance (it's off by default).

At this point, only database administrators have full permission to create and run external scripts, add or delete packages, and create stored procedures and other objects.

All other users must be granted EXECUTE ANY EXTERNAL SCRIPT permission. Additional [standard database permissions](../security/user-permission.md) determine whether users can create objects, run scripts, consume serialized and trained models, and so forth. 

## Resource allocation

Stored procedures and T-SQL queries that invoke external processing use the resources available to the default resource pool. As part of the default configuration, external processes like R and Python sessions are allowed up to 20% of total memory on the host system. 

If you want to readjust resourcing, you can modify the default pool, with corresponding effect on machine learning workloads running on that system.

Another option is to create a custom external resource pool to capture sessions originating from specific programs, hosts, or activity occurring during specific time intervals. For more information, see [Resource governance to modify resource levels for R and Python execution](../administration/resource-governance.md) and [How to create a resource pool](../administration/how-to-create-a-resource-pool.md) for step-by-step instructions.

## Isolation and containment

The processing architecture is engineered to isolate external scripts from core engine processing. R and Python scripts run as separate processes, under local worker accounts. In Task Manager, you can monitor R and Python processes, executing under a low-privileged local user account, distinct from the SQL Server service account. 

Running R and Python processes in individual low-privilege accounts has the following benefits:

+ Isolates core engine processes from R and Python sessions You can terminate an R or Python process without impacting core database operations. 

+ Reduces privileges of the external script runtime processes on the host computer.

Least-privilege accounts are created during setup and placed in a Windows *user account pool* called **SQLRUserGroup**. By default, this group has permission to use executables, libraries, and built-in datasets in the program folder for R and Python under SQL Server. 

As a DBA, you can use SQL Server data security to specify who has permission to execute scripts, and that data used in jobs is managed under the same security roles that control access through T-SQL queries. As a system administrator, you can explicitly deny **SQLRUserGroup** access to sensitive data on the local server by creating ACLs.

>[!NOTE]
> By default, the **SQLRUserGroup** does not have a login or permissions in SQL Server itself. Should worker accounts require a login for data access, you must create it yourself. A scenario that specifically calls for the creation of a login is to support requests from a script in execution, for data or operations on the database engine instance, when the user identity is a Windows user and the connection string specifies a trusted user. For more information, see [Add SQLRUserGroup as a database user](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).

## Disable script execution

In the event of runaway scripts, you can disable all script execution, reversing the steps previously undertaken to enable external script execution in the first place.

1. In SQL Server Management Studio or another query tool, run this command to set `external scripts enabled` to 0 or FALSE.

    ```sql
    EXEC sp_configure  'external scripts enabled', 0
    RECONFIGURE WITH OVERRIDE
    ```
2. Restart the database engine service.

Once you resolve the issue, remember to re-enable script execution on the instance if you want to resume R and Python scripting support on the database engine instance. For more information, see [Enable script execution](../install/sql-machine-learning-services-windows-install.md#enable-script-execution)

## Extend functionality

Data science often introduces new requirements for package deployment and administration. For a data scientist, it's common practice to leverage open-source and third-party packages in custom solutions. Some of those packages will have dependencies on other packages, in which case you might need to evaluate and install multiple packages to reach your objective.

As a DBA responsible for a server asset, deploying arbitrary R and Python packages onto a production server represents an unfamiliar challenge. Before adding packages, you should assess whether the functionality provided by the external package is truly required, with no equivalent in the built-in [R language](r-libraries-and-data-types.md) and [Python libraries](../python/python-libraries-and-data-types.md) installed by SQL Server Setup. 

As an alternative to server package installation, a data scientist might be able to [build and run solutions on an external workstation](../r/set-up-a-data-science-client.md), retrieving data from SQL Server, but with all analysis performed locally on the workstation instead of on the server itself. 

If you subsequently determine that external library functions are necessary and do not pose a risk to server operations or data as a whole, you can choose from multiple methodologies to add packages. In most cases, administrator rights are required to add packages to SQL Server. To learn more, see [Install Python packages in SQL Server](../python/install-additional-python-packages-on-sql-server.md) and [Install R packages in SQL Server](install-additional-r-packages-on-sql-server.md).

> [!NOTE]
> For R packages, server admin rights are not specifically required for package installation if you use alternative methods. See [Install R packages in SQL Server](install-additional-r-packages-on-sql-server.md) for details.

## Monitoring script execution

R and Python scripts that run in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] are started by the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] interface. However, the Launchpad is not resource governed or monitored separately, as it is a secure service provided by Microsoft that manages resources appropriately.

External scripts that run under the Launchpad service are managed using the [Windows job object](/windows/desktop/ProcThread/job-objects). A job object allows groups of processes to be managed as a unit. Each job object is hierarchical and controls the attributes of all processes associated with it. Operations performed on a job object affect all processes associated with the job object.

Thus, if you need to terminate one job associated with an object, be aware that all related processes will also be terminated. If you are running an R script that is assigned to a Windows job object and that script runs a related ODBC job which must be terminated, the parent R script process will be terminated as well.

If you start an external script that uses parallel processing, a single Windows job object manages all  parallel child processes.

To determine if a process is running in a job, use the `IsProcessInJob` function.

## Next steps

+ Review the concepts and components of the [extensibility architecture](../concepts/extensibility-framework.md) and [security](../concepts/security.md) for more background.

+ As part of feature installation, you might already be familiar with end-user data access control, but if not, see [Grant user permissions to SQL Server machine learning](../security/user-permission.md) for details. 

+ Learn how to adjust the system resources for computation-intensive machine learning workloads. For more information, see [How to create a resource pool](../administration/how-to-create-a-resource-pool.md).
