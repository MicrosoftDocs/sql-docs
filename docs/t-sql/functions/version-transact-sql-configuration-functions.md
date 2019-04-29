---
title: "@@VERSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@VERSION"
  - "@@VERSION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@VERSION function"
  - "current SQL Server installation information"
  - "versions [SQL Server], @@VERSION"
  - "processors [SQL Server], types"
ms.assetid: 385ba80e-7c28-41a5-9cdb-5648f3785983
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# &#x40;&#x40;Version - Transact SQL Configuration Functions
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns system and build information for the current installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
  
 For [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the following information is returned.  
  
-   Edition- "Microsoft SQL Azure"  
  
-   Product level- "(RTM)"  
  
-   Product version  
  
-   Build date  
  
-   Copyright statement  

> [!NOTE]  
> We are aware of an issue where the product version reported by @@VERSION is incorrect for Azure SQL Database. 
> The version of the SQL Server database engine run by Azure SQL Database is always ahead of the on-premises version of SQL Server, and includes the latest security fixes. This means that the patch level is always on par with or ahead of the on-premises version of SQL Server, and that the latest features available in SQL Server are available in Azure SQL Database.
>
> To programmatically determine the engine edition, use SELECT SERVERPROPERTY('EngineEdition'). This query will return '5' for single databases/elastic pools and '8' for managed instances in Azure SQL Database. 
>
> We will update the documentation once this issue is resolved.

  
## Examples  
  
### A: Return the current version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The following example shows returning the version information for the current installation.  
  
```  
SELECT @@VERSION AS 'SQL Server Version';  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Return the current version of [!INCLUDE[ssDW](../../includes/ssdw-md.md)]  
  
```  
SELECT @@VERSION AS 'SQL Server PDW Version';  
```  
  
## See Also  
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)  
  
  

