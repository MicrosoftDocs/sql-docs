---
title: "Set Up Database Diagram Designer (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.diagnostic.InstallSqlDiagramSupport"
helpviewer_keywords: 
  - "Database Diagram Designer"
  - "database diagrams [SQL Server], Database Diagram Designer"
  - "diagrams [SQL Server], Database Diagram Designer"
ms.assetid: 927321ee-b459-4f5b-9719-4a7a95639143
author: stevestein
ms.author: sstein
manager: craigg
---
# Set Up Database Diagram Designer (Visual Database Tools)
  To use Database Diagram Designer, it must first be set up by a member of the **db_owner** role to control access to diagrams.  
  
### To set up database diagramming  
  
1.  From Object Explorer, expand a database node.  
  
2.  Expand the Database Diagrams node under the database connection.  
  
3.  Select **Yes** when prompted if you want to set up database diagramming.  
  
    > [!NOTE]  
    >  This will create the database diagram table, system stored procedures, and a system function on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
4.  Visual Studio will create the following objects on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    1.  sysdiagrams table  
  
    2.  sp_alterdiagram stored procedure  
  
    3.  sp_creatediagram stored procedure  
  
    4.  sp_dropdiagram stored procedure  
  
    5.  sp_renamediagram stored procedure  
  
    6.  fn_diagramobjects function  
  
    7.  sp_helpdiagrams stored procedure  
  
    8.  sp_helpdiagramsdefinition stored procedure  
  
    9. sp_upgraddiagrams stored procedure  
  
## See Also  
 [Understand Database Diagram Ownership &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Upgrade Database Diagrams from Previous Editions &#40;Visual Database Tools&#41;](upgrade-database-diagrams-from-previous-editions-visual-database-tools.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-authorization-transact-sql)  
  
  
