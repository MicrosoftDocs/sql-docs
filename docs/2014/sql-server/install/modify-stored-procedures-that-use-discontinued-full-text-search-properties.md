---
title: "Modify stored procedures that use discontinued Full-Text Search properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "discontinued properties [Full-Text Search]"
  - "stored procedures [Full-Text Search]"
ms.assetid: 8d9392d9-a9ba-4378-84e4-59f516b67ddb
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Modify stored procedures that use discontinued Full-Text Search properties
  To ensure that your stored procedures perform correctly, you should edit existing procedures and remove those full-text related properties and settings that have been removed or deprecated.  
  
## Component  
 Full-Text Search  
  
## Description  
 The following Full-Text Search-related properties and settings have been removed.  
  
-   **DataTimeout**  
  
-   **ConnectTimeout**  
  
-   **Clean_up**  
  
-   **LogSize**  
  
 The following Full-Text Search-related properties and settings have been removed or deprecated:  
  
-   'Path' of the Full-Text Catalog. The Full-Text Catalog will be a logic object without a specific file path in the system.  
  
-   Enable/disable in SP_FULLTEXT_DATABASE has no effect anymore as databases are enabled for Full-Text Search at all times and by default in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## Corrective Action  
 Modify your stored procedures to remove these properties.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
