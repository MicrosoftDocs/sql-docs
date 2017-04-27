---
title: "Resource Governance for R Services | Microsoft Docs"
ms.custom: ""
ms.date: "05/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 18c9978a-aa55-42bd-9ab3-8097030888c9
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Resource Governance for R Services
  One pain point with R is that analyzing large amounts of data in production requires additional hardware, and data is often moved outside the database to computers not controlled by IT.  To perform advanced analytics operations, customers want to leverage database server resources, and to protect their data, they require that such operations meet enterprise-level compliance requirements, such as security and performance.  
  
 This section provides information about how you can manage resources used by the R runtime and by R jobs running using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance as the compute context.  
  
## What is Resource Governance?  
 Resource governance is designed to identify and prevent problems that are common in a database server environment, where there are often multiple dependent applications and multiple services to support and balance. For [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], resource governance involves these tasks:  
  
-   Identifying scripts that use excessive server resources.  
  
     The administrator needs to be able to terminate or throttle jobs that are consuming too many resources.  
  
-   Mitigating unpredictable workloads.  
  
     For example, if multiple R jobs are running concurrently on the server and the jobs are not isolated from each other by using resource pools, the resulting resource contention could lead to unpredictable performance or threaten completion of the workload.  
  
-   Prioritizing workloads.  
  
     The administrator or architect needs to be able to specify workloads that must take precedence, or guarantee certain workloads to complete if there is resource contention.  
  
 In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], you can use [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) to manage the resources used by the R runtime and by remote R jobs.  
  
## How to Use Resource Governor to Manage R jobs  
 In general, you manage resources allocated to R jobs by creating *external resource pools* and assigning workloads to the pool or pools. An external resource pool is a new type of resource pool introduced in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], to help manage the R runtime and other processes external to the database engine.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], there are now three types of default resource pools .  
  
-   The *internal pool* represents the resources used by the SQL Server itself and cannot be altered or restricted.  
  
-   The *default pool* is a predefined user pool that you can use to modify resource use for the server as a whole. You can also define user groups that belong to this pool, to manage access to resources.  
  
-   The *default external pool* is a predefined user pool for external resources. Additionally, you can create new external resource pools and define user groups to belong to this pool.  
  
 In addition, you can create *user-defined resource pools* to allocate resources to the database engine or other applications, and create *user-defined external resource pools* to manage R and other external processes.  
  
 For a good introduction to terminology and general concepts, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).  

  
## Resource Management using Resource Governor 

   If you are new to Resource Governor, see this topic for a quick walkthrough of how to modify the instance default resources and create a new external resource pool:  [How To: Create a Resource Pool for R](../../advanced-analytics/r-services/how-to-create-a-resource-pool-for-r.md)   
  
 You can use the *external resource pool* mechanism to manage the resources used by the following R executables:  
  
-   Rterm.exe and satellite processes  
  
-   BxlServer.exe and satellite processes  
  
-   Satellite processes launched by LaunchPad  
  
 However,  direct management of the Launchpad service by using Resource Governor is not supported. That is because the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] is a trusted service that can by design host only launchers that are provided by Microsoft. Trusted launchers are also configured to avoid consuming excessive resources.  
  
 We recommend that you manage satellite processes using Resource Governor and tune them to meet the needs of the individual database configuration and workload.  For example, any individual satellite process can be created or destroyed on demand during execution.  
  
## Disable External Script Execution  
 Support for external scripts is optional in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. Even after installing [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)], the ability to execute external scripts is OFF by default, and you must manually reconfigure the property and restart the instance to enable script execution.  
  
 Therefore, if there is a resource issue that needs to be mitigated immediately, or a security issue, an administrator can immediately disable any external script execution by using [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) and setting the property `external scripts enabled` to FALSE, or 0.  
  
## See Also  
 [Managing and Monitoring R Solutions](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md)  
 [How To: Create a Resource Pool for R](../../advanced-analytics/r-services/how-to-create-a-resource-pool-for-r.md)  
 [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
  

