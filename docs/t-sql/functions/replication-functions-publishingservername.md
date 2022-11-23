---
title: "PUBLISHINGSERVERNAME (Transact-SQL)"
description: "Replication Functions - PUBLISHINGSERVERNAME"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
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
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
PUBLISHINGSERVERNAME()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **nvarchar**  
  
## Remarks  
 PUBLISHINGSERVERNAME is used in all types of replication.  
  
 PUBLISHINGSERVERNAME is used when a database mirroring session exists on the publication database between the Publisher and a mirror partner instance.  
  
 This function must be executed within the context of a publication database. When PUBLISHINGSERVERNAME is executed on a publication database at the mirror server instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the name of the Publisher instance from which the published database originates is returned. When this function is executed on a database at the mirror server instance that is not published or that is published from the mirror server instance after a failover, the name of the mirror server instance is returned. When this function is executed at the original Publisher instance, the name of the Publisher is returned.  
  
## See Also  
 [Database Mirroring and Replication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)   
 [Replication Functions &#40;Transact-SQL&#41;]()  
  
