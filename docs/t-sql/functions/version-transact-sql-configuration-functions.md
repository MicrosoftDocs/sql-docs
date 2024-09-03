---
title: "@@VERSION (Transact-SQL)"
description: "@@VERSION - Transact SQL Configuration Functions"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/20/2023"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@VERSION"
  - "@@VERSION_TSQL"
helpviewer_keywords:
  - "@@VERSION function"
  - "current SQL Server installation information"
  - "versions [SQL Server], @@VERSION"
  - "processors [SQL Server], types"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# @@VERSION - Transact SQL Configuration Functions
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns system and build information for the current installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

> [!IMPORTANT]  
> The [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] version numbers for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] are not comparable with each other, and represent internal build numbers for these separate products. The [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is based on the same code base as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Most importantly, the [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] always has the newest SQL [!INCLUDE[ssDE-md](../../includes/ssde-md.md)] bits. For example, version 12 of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is newer than version 16 of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Syntax  
  
```syntaxsql
@@VERSION  
```  

## Return Types
 **nvarchar**  
  
## Remarks  
 The @@VERSION results are presented as one nvarchar string. You can use the [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md) function to retrieve the individual property values.  
  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the following information is returned.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version  
  
-   Processor architecture  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] build date  
  
-   Copyright statement  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition  
  
-   Operating system version  
  
 For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], the following information is returned.  
  
-   Edition- "Microsoft SQL Azure"  
  
-   Product level- "(RTM)"  
  
-   Product version  
  
-   Build date  
  
-   Copyright statement  
  
## Examples  
  
### A: Return the current version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following example shows returning the version information for the current installation.  
  
```sql
SELECT @@VERSION AS 'SQL Server Version';  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Return the current version of [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]  
  
```sql
SELECT @@VERSION AS 'SQL Server PDW Version';  
```  
  
## See Also  
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)  
  
  

