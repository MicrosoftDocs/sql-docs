---
title: "CREATE INDEX Statement Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "CREATE INDEX statement limitations [ODBC]"
  - "ODBC SQL grammar, CREATE INDEX statement limitations"
ms.assetid: 832dcda1-e452-48e6-8adb-7fb33c4fb4ff
author: MightyPen
ms.author: genemi
manager: craigg
---
# CREATE INDEX Statement Limitations
The CREATE INDEX statement is not supported for the Microsoft Excel or Text drivers.  
  
 An index can be defined on a maximum of 10 columns. If more than 10 columns are included in a CREATE INDEX statement, the index will not be recognized and the table will be treated as though no index were created.  
  
 The dBASE driver cannot create an index on a LOGICAL column.  
  
 When the dBASE driver is used, response time on large files can be improved by building an .mdx (or .ndx) index on the column (field) specified in the WHERE clauses of a SELECT statement. Existing .mdx indexes will automatically be applied for =, >, \<, >=, =<, and BETWEEN operators in a WHERE clause, and LIKE predicates, as well as in join predicates.  
  
 When the dBASE driver is used, the index created by a CREATE UNIQUE INDEX statement is actually non-unique, and duplicate values can be inserted into the indexed column. Only one record from a set with identical key values can be added to the index.  
  
 When the Paradox driver is used, a unique index must be defined upon a contiguous subset of the columns in a table, including the first column. A table cannot be updated by the Paradox driver if a unique index is not defined on the table or when the Paradox driver is used without the implementation of the Borland Database Engine.
