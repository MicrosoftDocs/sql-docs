---
title: "Resource Governance for Python | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Resource Governance for Python

Because Python is enabled through the the same extensibility architecture that was implemented for the R language in SQL Server 2016, you can use existing tools in SQL Server such as Resource Governor, DMVs, and extended events, to monitor the execution of Python scripts in SQL Server.

Resource governance in particular is important because analyzing large amounts of data in production can tax even advanced hardware.  To prevent data from being moved outside the database to computers that might not be managed or audited, it is important that the database administrator allocate sufficient resources for advanced analytics operations.

This section provides information about how you can manage resources used by the Python runtime and monitor use of resources by Python scripts jobs executing in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

> [!NOTE]
> Python support is a new feature in SQL Server 2017 and is in prerelease. Look for more information soon.
> In general, you can monitor any external script, including one that runs Python, using the same framework that was provided for resource governance of R scripts in SQL Server 2016.

## What is Resource Governance?

Resource governance is designed to identify and prevent problems that are common in a database server environment, where there are often multiple dependent applications and multiple services to support and balance. For [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], resource governance involves these tasks:  

+ **Identifying scripts that use excessive server resources**. The administrator needs to be able to terminate or throttle jobs that are consuming too many resources.

+ **Mitigating unpredictable workloads**. If multiple Python jobs are running concurrently on the server and the jobs are not isolated from each other by using resource pools, the resulting resource contention could lead to unpredictable performance or threaten completion of the workload.

+ **Prioritizing workloads**. The administrator or architect needs to be able to specify workloads that must take precedence, or guarantee certain workloads to complete if there is resource contention.

You can use [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) to manage the resources used by the external runtimes. This includes Python scripts that are started from a remote terminal but executed using the SQL Server computer as the compute context.

> [!NOTE] 
> Resource Governor is available in Enterprise Edition.

## How to Use Resource Governor to Manage Python Execution

In general, you manage resources allocated to any external script job, by creating an *external resource pool* and assigning workloads to the pool. An external resource pool is a new type of resource pool introduced in SQL Server 2016, to help manage the R runtime and other processes external to the database engine. It can be used to monitor Python jobs beginning with SQL Server 2017.

There are three types of default resource pools:

+ The *internal pool* represents the resources used by the SQL Server itself and cannot be altered or restricted.
+ The *default pool* is a predefined user pool that you can use to modify resource use for the server as a whole. You can also define user groups that belong to this pool, to manage access to resources.
+ The *default external pool* is a predefined user pool for external resources. Additionally, you can create new external resource pools and define user groups to belong to this pool.

In addition, you can create *user-defined resource pools* to allocate resources to the database engine or other applications, and create *user-defined external resource pools* to manage R and other external processes.

For a good introduction to terminology and general concepts, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

## Resource Management using Resource Governor

If you are new to Resource Governor, see this topic for a quick walkthrough of how to modify the instance default resources and create a new external resource pool:  [How To: Create a Resource Pool for R](../../advanced-analytics/r-services/how-to-create-a-resource-pool-for-r.md)

You can use the *external resource pool* mechanism to manage the resources used by the following supported executables:

+ Python35.exe when called from SQL Server or called remotely with SQL Server as the remote compute context
+ BxlServer.exe and satellite processes
+ Launchers started by Launchpad, such as PythonLauncher.dll

> [!NOTE]
> Direct management of the Launchpad service by using Resource Governor is not supported. That is because the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] is a trusted service that can by design host only launchers that are provided by Microsoft. Trusted launchers are also configured to avoid consuming excessive resources.

We recommend that you manage satellite processes using Resource Governor and tune them to meet the needs of the individual database configuration and workload.  For example, any individual satellite process can be created or destroyed on demand during execution.

## Disable or Enable External Script Execution

Support for external scripts is optional in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup, and even after setup is complete, external script execution is set to OFF by default for security reasons. Therefore, after you have completed installation of one of the machine learning languages and related services, you must explicitly reconfigure the instance and then restart the database engine service.

In the event of runaway scripts, you can disable all script execution. Just reverse this process, and set the property `external scripts enabled` to FALSE, or 0, on the instance. This will immediately disable any external script execution. You should reserve this option for security issues, or situations in which an administrator needs to mitigate resource issues immediately.

## See Also

[Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)

