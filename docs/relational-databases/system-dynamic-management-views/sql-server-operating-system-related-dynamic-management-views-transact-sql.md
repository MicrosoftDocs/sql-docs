---
title: "SQL Server Operating System Related Dynamic Management Views (Transact-SQL)"
description: SQL Server Operating System Related Dynamic Management Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/17/2018"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "operating systems [SQL Server], dynamic management objects"
  - "SQL Operating System dynamic management objects [SQL Server]"
  - "SQL OS dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], SQL OS"
dev_langs:
  - "TSQL"
ms.assetid: 3030c86a-0a74-4fed-ac0f-392e244cb965
---
# SQL Server Operating System Related Dynamic Management Views (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section documents dynamic management views (DMV) that are associated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Operating System (SQLOS). SQLOS is responsible for managing operating system resources that are specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

:::row:::
   :::column span="":::
      **sys.dm_os_buffer_descriptors**<br>      **sys.dm_os_buffer_pool_extension_configuration**<br>      **sys.dm_os_child_instances**<br>      **sys.dm_os_cluster_nodes** <br>      **sys.dm_os_cluster_properties**<br>      **sys.dm_os_dispatcher_pools** <br>      **sys.dm_os_enumerate_fixed_drives**<br>      **sys.dm_os_host_info** <br>      **sys.dm_os_hosts**<br>      **sys.dm_os_latch_stats** <br>      **sys.dm_os_loaded_modules**<br>      **sys.dm_os_memory_brokers**<br>      **sys.dm_os_memory_cache_clock_hands**<br>      **sys.dm_os_memory_cache_counters** <br>      **sys.dm_os_memory_cache_entries**<br>      **sys.dm_os_memory_cache_hash_tables**<br>      **sys.dm_os_memory_clerks**<br>      **sys.dm_os_memory_nodes**
   :::column-end:::
   :::column span="":::
      **sys.dm_os_nodes**<br>      **sys.dm_os_performance_counters**<br>      **sys.dm_os_process_memory**<br>      **sys.dm_os_schedulers**<br>      **sys.dm_os_server_diagnostics_log_configurations**<br>      **sys.dm_os_spinlock_stats** <br>      **sys.dm_os_stacks**<br>      **sys.dm_os_sys_info**<br>      **sys.dm_os_sys_memory**<br>      **sys.dm_os_tasks**<br>      **sys.dm_os_threads**<br>      **sys.dm_os_virtual_address_dump**<br>      **sys.dm_os_volume_stats**<br>      **sys.dm_os_waiting_tasks**<br>      **sys.dm_os_wait_stats**<br>      **sys.dm_os_windows_info**<br>      **sys.dm_os_workers** 
   :::column-end:::
:::row-end:::

 The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Operating System-related dynamic management views are [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)].  
  
:::row:::
   :::column span="":::
      **sys.dm_os_function_symbolic_name**<br>      **sys.dm_os_ring_buffers**  <br>      **sys.dm_os_memory_allocations**
   :::column-end:::
   :::column span="":::
      **sys.dm_os_sublatches**  <br>      **sys.dm_os_worker_local_storage**  
   :::column-end:::
:::row-end:::
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  

