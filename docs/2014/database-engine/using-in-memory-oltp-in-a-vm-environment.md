---
title: "Using In-Memory OLTP in a VM Environment | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 27ec7eb3-3a24-41db-aa65-2f206514c6f9
author: stevestein
ms.author: sstein
manager: craigg
---
# Using In-Memory OLTP in a VM Environment
  Server virtualization can help you lower IT capital and operational costs and attain greater IT efficiency with improved application provisioning, maintenance, availability, and backup/recovery processes. With recent technological advances, complex database workloads can be more readily consolidated using virtualization. This topic covers best practices for using [!INCLUDE[hek_1](../includes/hek-1-md.md)] in a virtualized environment.  
  
##  <a name="bkmk_memoryPreAllocation"></a> Memory pre-allocation  
 For memory in a virtualized environment, better performance and enhanced support are essential considerations. You must be able to both quickly allocate memory to virtual machines depending on their requirements (peak and off-peak loads) and ensure that the memory is not wasted. The Hyper-V Dynamic Memory feature increases agility in how the memory is allocated and managed between virtual machines running on a host.  
  
 Some best practices for virtualizing and managing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] need to be modified when virtualizing a database with memory-optimized tables. Without memory-optimized tables, two of the best practices are:  
  
-   If you use MIN_SERVER_MEMORY, it is better to assign only the amount of memory that is required so sufficient memory remains for other processes (thereby avoiding paging).  
  
-   Do not set the memory pre-allocation value too high. Otherwise, other processes may not get sufficient memory at the time when they require it, and this can result in memory paging.  
  
 If you follow the above practices for a database with memory-optimized tables, an attempt to restore and recover a database could result in the database being in a "Recovery Pending" state, even if you have sufficient memory to recover the database. The reason for this is that, when starting up, [!INCLUDE[hek_2](../includes/hek-2-md.md)] brings data into memory more aggressively than dynamic memory allocation allocates memory to the database.  
  
 **Resolution**  
  
 To mitigate this, pre-allocate sufficient memory to the database to recover or restart the database, not a minimum value relying on dynamic memory to provide the additional memory when needed.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
