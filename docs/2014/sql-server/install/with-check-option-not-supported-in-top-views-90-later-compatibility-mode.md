---
title: "WITH CHECK OPTION is not supported in views that contain TOP in 90 or later compatibility modes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "TOP clause"
  - "WITH CHECK OPTION clause"
ms.assetid: 1b9581d0-bad9-43e0-b8fc-f32d8a8a04ca
author: mashamsft
ms.author: mathoma
manager: craigg
---
# WITH CHECK OPTION is not supported in views that contain TOP in 90 or later compatibility modes
  Upgrade Advisor detected a view that uses the WITH CHECK OPTION and a TOP clause in the SELECT statement of the view or in a referenced view. Views defined this way incorrectly allow data to be modified through the view and may produce inaccurate results when the database compatibility mode is set to 80 and earlier. Data cannot be inserted or updated through a view that uses WITH CHECK OPTION when the view or a referenced view uses the TOP clause and the database compatibility mode is set to 90 or later.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 When you upgrade, user databases maintain their compatibility mode. Before you change the database compatibility mode to 100 or later, modify views that use both WITH CHECK OPTION and TOP if data modification through the view is required. For more information, see [sp_dbcmptlevel &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbcmptlevel-transact-sql).  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
