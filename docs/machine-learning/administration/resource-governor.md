---
title: Manage with Resource Governor
description: Learn how to use Resource Governor to manage CPU, physical IO, and memory resources allocation for Python and R workloads in SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 08/06/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Manage Python and R workloads with Resource Governor in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Learn how to use [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) to manage CPU, physical IO, and memory resources allocation for Python and R workloads in SQL Server Machine Learning Services.

Machine learning algorithms in Python and R are computed intensive. Depending on your workload priorities, you might need to increase or decrease the resources available for Machine Learning Services.

For more general information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).

> [!NOTE] 
> Resource Governor is an Enterprise Edition feature.

## Default allocations

By default, the external script runtimes for machine learning are limited to no more than 20% of total machine memory. It depends on your system, but in general, you might find this limit inadequate for serious machine learning tasks such as training a model or predicting on many rows of data. 

## Manage resources with Resource Governor
 
By default, external processes use up to 20% of total host memory on the local server. You can modify the default resource pool to make server-wide changes, with R and Python processes using whatever capacity you make available to external processes.

Optionally, you can create custom **external resource pools**, with associated workload groups and classifiers, to determine resource allocation for requests originating from specific programs, hosts, or other criteria that you provide. An external resource pool is a type of resource pool introduced in [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] to help manage the R and Python processes external to the database engine.

1. [Enable resource governance](../../relational-databases/resource-governor/enable-resource-governor.md) (it's off by default).

2. Run [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md) to create and configure the resource pool, followed by [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md) to implement it.

3. Create a workload group for granular allocations, for example between training and scoring.

4. Create a classifier to intercept calls for external processing.

5. Execute queries and procedures using the objects you created.

For a walkthrough, see [Create a resource pool for SQL Server Machine Learning Services](create-external-resource-pool.md) for step-by-step instructions.

For an introduction to terminology and general concepts, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

## Processes under resource governance
  
 You can use an *external resource pool* to manage the resources used by the following executables on a database engine instance:

+ Rterm.exe when called locally from SQL Server or called remotely with SQL Server as the remote compute context
+ Python.exe when called locally from SQL Server or called remotely with SQL Server as the remote compute context
+ BxlServer.exe and satellite processes
+ Satellite processes launched by Launchpad, such as PythonLauncher.dll
  
> [!NOTE]
> Direct management of the Launchpad service by using Resource Governor is not supported. Launchpad is a trusted service that can only host launchers provided by Microsoft. Trusted launchers are explicitly configured to avoid consuming excessive resources.
  
## Next steps

+ [Create a resource pool for machine learning](create-external-resource-pool.md)
+ [Resource Governor resource pools](../../relational-databases/resource-governor/resource-governor-resource-pool.md)