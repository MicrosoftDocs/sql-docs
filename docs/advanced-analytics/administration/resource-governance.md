---
title: Resource governance for machine learning in SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Resource governance for machine learning in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Data science and machine learning algorithms are computationally intensive. Depending on workload priorities, you might need to increase the resources available for data science, or decrease resourcing if R and Python script execution undermines the performance of other services running concurrently. 

When you need to rebalance the distribution of system resources across multiple workloads, you can use [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) to allocate CPU, physical IO, and memory resources consumed by the external runtimes for R and Python. If you do so, you might need to reduce the amount of memory reserved for other workloads and services. 

> [!NOTE] 
> Resource Governor is an Enterprise Edition feature.

## Default allocations

By default, the external script runtimes for machine learning are limited to no more than 20% of total machine memory. It depends on your system, but in general, you might find this limit inadequate for serious machine learning tasks such as training a model or predicting on many rows of data. 

## Using Resource Governor to manage machine learning resourcing
 
For R and Python sessions, set up one or more *external resource pools* and assign workgroups accordingly. An external resource pool is a type of resource pool introduced in [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] to help manage the R and Python processes external to the database engine.

1. [Enable resource governance](https://docs.microsoft.com/sql/relational-databases/resource-governor/enable-resource-governor) (it is off by default).

2. Run [CREATE EXTERNAL RESOURCE POOL](https://docs.microsoft.com/sql/t-sql/statements/create-external-resource-pool-transact-sql) to create and configure the resource pool, followed by [ALTER RESOURCE GOVERNOR](https://docs.microsoft.com/sql/t-sql/statements/alter-resource-governor-transact-sql) to implement it.

For a walkthrough, see [How to create a resource pool for external R and Python scripts](../../advanced-analytics/r/how-to-create-a-resource-pool-for-r.md) for step-by-step instructions.

> [!Tip]
> For a good introduction to terminology and general concepts, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

## Processes under resource governance
  
 You can use an *external resource pool* to manage the resources used by the following executables on a database engine instance:

+ Rterm.exe when called locally from SQL Server or called remotely with SQL Server as the remote compute context
+ Python.exe when called locally from SQL Server or called remotely with SQL Server as the remote compute context
+ BxlServer.exe and satellite processes
+ Satellite processes launched by Launchpad, such as PythonLauncher.dll
  
> [!NOTE]
> Direct management of the Launchpad service by using Resource Governor is not supported. Launchpad is a trusted service that can only host launchers provided by Microsoft. Trusted launchers are explicitly configured to avoid consuming excessive resources.
  
## Disable external script execution

In the event of runaway scripts, you can disable all script execution, reversing the steps previously undertaken to enable external script execution in the first place.

1. In SQL Server Management Studio or another query tool, run this command to set `external scripts enabled` to 0 or FALSE.

    ```sql
    EXEC sp_configure  'external scripts enabled', 0
    RECONFIGURE WITH OVERRIDE
    ```
2. Restart the database engine service.

Once you resolve the issue, remember to re-enable script execution on the instance if you want to resume R and Python scripting support on the database engine instance. For more information, see [Enable script execution](../install/sql-machine-learning-services-windows-install.md#enable-script-execution)
  
## See also

+ [Manage machine learning integration](../r/managing-and-monitoring-r-solutions.md)
+ [Create a resource pool for machine learning](../r/how-to-create-a-resource-pool-for-r.md)
+ [Resource Governor resource pools](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
