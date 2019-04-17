---
title: "Trustworthy Bit | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 3198188a-2b59-4865-9560-10f760934b8e
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Trustworthy Bit
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule determines whether the dbo role for a database is assigned to the sysadmin fixed server role and the database has its trustworthy bit set to ON.  
  
 If these conditions are met, a privileged database user can elevate privileges to the sysadmin role. In this role, the user can create and run unsafe assemblies that compromise the system.  
  
## Best Practices Recommendations  
 Turn off the trustworthy bit or revoke sysadmin permissions from the dbo database role.  
  
## For More Information  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
