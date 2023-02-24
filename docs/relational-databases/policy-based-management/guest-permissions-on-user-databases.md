---
title: "Guest Permissions on User Databases | Microsoft Docs"
description: Determine whether the guest user has permission to access user databases in SQL Server. Revoke the guest user permission if it is not required.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 540f1c6d-df51-497e-958a-3a0f429d2920
author: VanMSFT
ms.author: vanto
---
# Guest Permissions on User Databases
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule determines whether the guest user has permission to access the database. This rule applies to user databases only.  
  
## Best Practices Recommendations  
 Revoke the guest user permission to access the database if it is not required.  
  
 The guest user cannot be dropped, but guest user can be disabled by revoking its CONNECT permission by executing REVOKE CONNECT FROM GUEST within any database other than master, tempdb, or msdb.  
  
## For More Information  
 [Securing SQL Server](../../relational-databases/security/securing-sql-server.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
