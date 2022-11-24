---
title: "Set the AUTO_CLOSE Database Option to OFF | Microsoft Docs"
description: Check whether the AUTO_ CLOSE option is OFF. The AUTO_ CLOSE option has implications for performance in SQL Server.
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: e6b03364-263a-4ec4-9794-de9869d396ce
author: VanMSFT
ms.author: vanto
---
# Set the AUTO_CLOSE Database Option to OFF
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether the AUTO_ CLOSE option is set OFF. When AUTO_CLOSE is set ON, this option can cause performance degradation on frequently accessed databases because of the increased overhead of opening and closing the database after each connection. AUTO_CLOSE also flushes the procedure cache after each connection.  
  
## Best Practices Recommendations  
 If a database is accessed frequently, set the AUTO_CLOSE option to OFF for the database.  
  
## For More Information  
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
