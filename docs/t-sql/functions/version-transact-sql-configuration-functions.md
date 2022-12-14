---
title: "@@VERSION (Transact-SQL)"
description: "@@VERSION - Transact SQL Configuration Functions"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/20/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
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
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

> [!NOTE]  
> We are aware of an issue where the product version reported by @@VERSION is incorrect for **Azure SQL Database**, **Azure SQL Managed Instance** and **Azure Synapse Analytics**.
> 
> The version of the SQL Server database engine run by Azure SQL Database, Azure SQL Managed Instance and Azure Synapse Analytics is always ahead of the on-premises version of SQL Server, and includes the latest security fixes. This means that the patch level is always on par with or ahead of the on-premises version of SQL Server, and that the latest features available in SQL Server are available in these services.
>
> To programmatically determine the engine edition, use SELECT SERVERPROPERTY('EngineEdition'). This query will return '5' for Azure SQL Database, '8' for Azure SQL Managed Instance, and '6' or '11' for Azure Synapse.
>
> We will update the documentation once this issue is resolved.
## Syntax  
  
```syntaxsql
@@VERSION  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
 For [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsmifull-md.md)], the following information is returned.  
  
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
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Return the current version of [!INCLUDE[ssDW](../../includes/ssdw-md.md)]  
  
```sql
SELECT @@VERSION AS 'SQL Server PDW Version';  
```  
  
## See Also  
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)  
  
  

