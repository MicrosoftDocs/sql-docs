---
title: "Create Full-Text Search Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "CONTAINS predicate (Transact-SQL)"
  - "queries [full-text search], creating"
  - "full-text queries [SQL Server], creating"
ms.assetid: 537fa556-390e-4c88-9b8e-679848d94abc
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Create Full-Text Search Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Full-text searches use the CONTAINS predicate to locate rows that have specified text in a given column. Full-Text searches are only possible on columns that have active full-text indexes. If you attempt to use the CONTAINS clause on a column that does not have a currently active full-text index, you will receive an error. For more information on full-text indexes and the CONTAINS clause, see [Full-Text Search (SQL Server)](../../relational-databases/search/full-text-search.md) and [CONTAINS (Transact-SQL)](https://msdn.microsoft.com/996c72fc-b1ab-4c96-bd12-946be9c18f84).  
  
### To create a full-text search query  
  
1.  Open a query from Solution Explorer or create a new one.  
  
2.  In the WHERE clause of your query, use the CONTAINS function to search a full-text column.  
  
## See Also  
[Supported Query Types &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/supported-query-types-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
