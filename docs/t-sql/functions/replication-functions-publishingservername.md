---
title: "PUBLISHINGSERVERNAME (Transact-SQL)"
description: "Replication Functions - PUBLISHINGSERVERNAME"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PUBLISHINGSERVERNAME_TSQL"
  - "PUBLISHINGSERVERNAME"
helpviewer_keywords:
  - "PUBLISHINGSERVERNAME function"
  - "Publishers [SQL Server replication], names"
dev_langs:
  - "TSQL"
---
# Replication Functions - PUBLISHINGSERVERNAME
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the name of the originating Publisher for a published database participating in a database mirroring session. This function is executed at a Publisher instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the publication database. Use it to determine the original Publisher of the published database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
PUBLISHINGSERVERNAME()  
```  
  
## Return Types
 **nvarchar**  
  
## Remarks  
 PUBLISHINGSERVERNAME is used in all types of replication.  
  
 PUBLISHINGSERVERNAME is used when a database mirroring session exists on the publication database between the Publisher and a mirror partner instance.  
  
 This function must be executed within the context of a publication database. When PUBLISHINGSERVERNAME is executed on a publication database at the mirror server instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the name of the Publisher instance from which the published database originates is returned. When this function is executed on a database at the mirror server instance that is not published or that is published from the mirror server instance after a failover, the name of the mirror server instance is returned. When this function is executed at the original Publisher instance, the name of the Publisher is returned.  
  
## Related content

- [Database Mirroring and Replication (SQL Server)](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)
