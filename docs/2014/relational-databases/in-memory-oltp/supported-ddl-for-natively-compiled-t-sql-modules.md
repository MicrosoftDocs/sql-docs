---
title: "Supported Constructs on Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 6b21f47e-bceb-4054-8b3c-9d39bb9583c0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported Constructs on Natively Compiled Stored Procedures
  This topic lists the supported constructs on natively compiled stored procedures.  
  
 For information about unsupported constructs, see [Transact-SQL Constructs Not Supported by In-Memory OLTP](transact-sql-constructs-not-supported-by-in-memory-oltp.md).  
  
## Procedure DDL  
 The following are supported:  
  
-   CREATE PROCEDURE  
  
-   DROP PROCEDURE  
  
-   SCHEMABINDING (required for natively compiled stored procedures)  
  
-   NATIVE_COMPILATION  
  
-   Parameters can be declared as NOT NULL.  
  
-   Table-valued parameters.  
  
## Security  
 The following are supported:  
  
-   For procedures: EXECUTE AS OWNER, SELF, and user.  
  
-   GRANT and DENY permissions on tables and procedures.  
  
## See Also  
 [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)  
  
  
