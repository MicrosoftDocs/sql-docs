---
title: "Keep the Locks Configuration Option Default Value | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: f214f05b-5f0b-4786-b2ad-b8b4b6e58d72
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Keep the Locks Configuration Option Default Value
  This rule checks the value of the locks configuration option. This option determines the maximum number of available locks. This limits how much memory the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] uses for locks. The default setting of 0 enables the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to allocate and deallocate lock structures dynamically based on changing system requirements.  
  
 If locks is nonzero, batch jobs will stop, and an "out of locks" error message will be generated, if the value specified is exceeded.  
  
## Best Practices Recommendations  
 Use the sp_configure system stored procedure to change the value of locks to its default setting by using the following statement:  
  
```  
EXEC sp_configure 'locks', 0;  
```  
  
## For More Information  
 [Configure the locks Server Configuration Option](../../database-engine/configure-windows/configure-the-locks-server-configuration-option.md)  
  
 [sys.dm_tran_locks &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql)  
  
 [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql)  
  
 [Microsoft Knowledge Base article 271509](https://go.microsoft.com/fwlink/?linkid=117788)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
