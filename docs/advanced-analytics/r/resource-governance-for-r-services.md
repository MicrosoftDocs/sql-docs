---
title: "Resource governance for machine learning in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 18c9978a-aa55-42bd-9ab3-8097030888c9
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Resource governance for machine learning in SQL Server

This article provides an overview of resource governance features in SQL Server that help allocate and balance resources used by R and Python scripts.

**Applies to:** [!INCLUDE[sscurrent-md](../../includes/sscurrent-md.md)]
[!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] and [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)]

## Goals of resource governance for machine learning

One known pain point with machine learning languages such as R and Python is that data is often moved outside the database to computers not controlled by IT. Another is that R is single-threaded, meaning that you can only work with the data available in memory. 

SQL Server Machine Learning Services alleviates both these problems, and helps meet enterprise compliance requirements. It keeps advanced analytics inside the database, and supports increased performance over large datasets through features such as streaming and chunking operations. However, moving R and Python computations inside the databases can affect the performance of other services that use the database, including regular user queries, external applications, and scheduled database jobs.

This section provides information about how you can manage resources used by external runtimes, such as R and Python, to mitigate impact on other core database services. A database server environment typically is the hub for multiple dependent applications and services.

You can use [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) to manage the resources used by the external runtimes for R and Python.  For machine learning, resource governance involves these tasks:

+ Identifying scripts that use excessive server resources.
  
     The administrator needs to be able to terminate or throttle jobs that are consuming too many resources.
  
+ Mitigating unpredictable workloads.
  
     For example, if multiple machine learning jobs are running concurrently on the server, the resulting resource contention could lead to unpredictable performance or threaten completion of the workload. However, if resource pools are used, the jobs can be isolated from each other.
  
-   Prioritizing workloads.
  
     The administrator or architect needs to be able to specify workloads that must take precedence, or guarantee certain workloads to complete when there is resource contention.

## How to use Resource Governor to manage machine learning
 
You manage resources allocated to R or Python sessions by creating an *external resource pool*, and assigning workloads to the pool or pools. An external resource pool is a new type of resource pool introduced in [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] to help manage the R runtime and other processes external to the database engine.

SQL Server supports three types of default resource pools: 
  
-   The *internal pool* represents the resources used by the SQL Server itself and cannot be altered or restricted.
  
-   The *default pool* is a predefined user pool that you can use to modify resource use for the server as a whole. You can also define user groups that belong to this pool, to manage access to resources.
  
-   The *default external pool* is a predefined user pool for external resources. Additionally, you can create new external resource pools and define user groups to belong to this pool.
  
 In addition, you can create *user-defined resource pools* to allocate resources to the database engine or other applications, and create *user-defined external resource pools* to manage R and other external processes.
  
 For a good introduction to terminology and general concepts, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

  
## Resource management walkthrough with Resource Governor

If you are new to Resource Governor, see this topic for a quick walkthrough of how to modify the instance default resources and create a new external resource pool:  [Create a resource pool for external scripts](../../advanced-analytics/r/how-to-create-a-resource-pool-for-r.md)
  
 You can use the *external resource pool* mechanism to manage the resources used by the following executables that are used in machine learning:

+ Rterm.exe and satellite processes
+ Python.exe and satellite processes
+ BxlServer.exe and satellite processes
+ Satellite processes launched by Launchpad
  
> [!NOTE]
> 
> Direct management of the Launchpad service by using Resource Governor is not supported. That is because the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] is a trusted service that can by design host only launchers that are provided by Microsoft. Trusted launchers are configured to avoid consuming excessive resources.
>   
> We recommend that you manage satellite processes using Resource Governor and tune them to meet the needs of the individual database configuration and workload.  For example, any individual satellite process can be created or destroyed on demand during execution.
  
## Disable external script execution

Support for external scripts is optional in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. Even after installing the machine learning features, the ability to execute external scripts is OFF by default, and you must manually reconfigure the property and restart the instance to enable script execution.

Therefore, if there is a resource issue that needs to be mitigated immediately, or a security issue, an administrator can immediately disable any external script execution by using [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) and setting the property `external scripts enabled` to FALSE or 0.
  
## See also

[Managing and monitoring machine learning solutions](../../advanced-analytics/r/managing-and-monitoring-r-solutions.md)

[Create a resource pool for machine learning](../../advanced-analytics/r/how-to-create-a-resource-pool-for-r.md)

[Resource Governor resource pools](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
