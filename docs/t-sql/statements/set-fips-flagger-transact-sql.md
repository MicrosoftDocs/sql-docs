---
title: "SET FIPS_FLAGGER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FIPS_FLAGGER"
  - "SET_FIPS_FLAGGER_TSQL"
  - "FIPS_FLAGGER_TSQL"
  - "SET FIPS_FLAGGER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SET FIPS_FLAGGER statement"
  - "FIPS 127-2 standard"
  - "FIPS_FLAGGER option"
ms.assetid: e82f6bee-6cf6-4061-be22-9ad2e8e9d3d6
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET FIPS_FLAGGER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Specifies checking for compliance with the FIPS 127-2 standard. This is based on the ISO standard. For information about SQL Server FIPS compliance, see [How to use SQL Server 2016 in FIPS 140-2-compliant mode](https://support.microsoft.com/help/4014354/how-to-use-sql-server-2016-in-fips-140-2-compliant-mode). 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SET FIPS_FLAGGER ( 'level' |  OFF )  
```  
  
## Arguments  
 **'** *level* **'**  
 Is the level of compliance against the FIPS 127-2 standard for which all database operations are checked. If a database operation conflicts with the level of ISO standards chosen, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a warning.  
  
 *level* must be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|ENTRY|Standards checking for ISO entry-level compliance.|  
|FULL|Standards checking for ISO full compliance.|  
|INTERMEDIATE|Standards checking for ISO intermediate-level compliance.|  
|OFF|No standards checking.|  
  
## Remarks  
 The setting of `SET FIPS_FLAGGER` is set at parse time and not at execute or run time. Setting at parse time means that if the SET statement is present in the batch or stored procedure, it takes effect, regardless of whether code execution actually reaches that point; and the `SET` statement takes effect before any statements are executed. For example, even if the `SET` statement is in an `IF...ELSE` statement block that is never reached during execution, the `SET` statement still takes effect because the `IF...ELSE` statement block is parsed.  
  
 If `SET FIPS_FLAGGER` is set in a stored procedure, the value of `SET FIPS_FLAGGER` is restored after control is returned from the stored procedure. Therefore, a `SET FIPS_FLAGGER` statement specified in dynamic SQL does not have any effect on any statements following the dynamic SQL statement.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
