---
description: "Understand Database Diagram Ownership (Visual Database Tools)"
title: Understand Database Diagram Ownership
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.diagnostic.CannotOpenWithInvalidOwner"
helpviewer_keywords: 
  - "diagrams [SQL Server], ownership"
  - "database diagrams [SQL Server], ownership"
  - "owners [SQL Server], database diagrams"
ms.assetid: 4a27a48e-c4ef-4017-82b8-0cac4d0bbcac
author: markingmyname
ms.author: maghan
ms.reviewer: 

---

# Understand Database Diagram Ownership (Visual Database Tools)

[!INCLUDE[SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

To use Database Diagram Designer it must first be set up by a member of the [db_owner](../../relational-databases/security/authentication-access/database-level-roles.md#fixed-database-roles) role (a role of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases) to control access to diagrams. Each diagram has one and only one owner, the user who created it. For more information on setting up diagramming see [Set Up Database Diagram Designer](set-up-database-diagram-designer-visual-database-tools.md).  

## Diagram ownership considerations

Some points to keep in mind about diagram ownership:  
  
- Although any user with access to a database can create a diagram, once the diagram has been created, the only users who can see it are the diagram creator and any member of the db_owner role. If the diagram creator is part of a user group and not a member of the db_owner role then they require their own login to view the diagram.

- Ownership of diagrams can only be transferred to members of the db_owner role. This is only possible if the previous owner of the diagram has been removed from the database.  
  
- If the owner of a diagram has been removed from the database, the diagram will remain in the database until a member of the db_owner role attempts to open it. At that point the db_owner member can choose to take over ownership of the diagram.  
  
## See Also

[Work with Database Diagrams](work-with-database-diagrams-visual-database-tools.md)  
[Set Up Database Diagram Designer](set-up-database-diagram-designer-visual-database-tools.md)
