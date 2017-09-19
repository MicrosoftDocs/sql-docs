---
title: "@@VERSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 40
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# &#x40;&#x40;Version - Transact SQL Configuration Functions
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

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
  
-   Edition- "Windows Azure SQL Database"  
  
-   Product level- "(CTP)" or "(RTM)"  
  
-   Product version  
  
-   Build date  
  
-   Copyright statement  
  
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
  
  

