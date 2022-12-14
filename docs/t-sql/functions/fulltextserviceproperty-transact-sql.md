---
title: "FULLTEXTSERVICEPROPERTY (Transact-SQL)"
description: "FULLTEXTSERVICEPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FULLTEXTSERVICEPROPERTY_TSQL"
  - "FULLTEXTSERVICEPROPERTY"
helpviewer_keywords:
  - "full-text search [SQL Server], properties"
  - "FULLTEXTSERVICEPROPERTY function"
  - "services [SQL Server], full-text search properties"
  - "test"
dev_langs:
  - "TSQL"
---
# FULLTEXTSERVICEPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information related to the properties of the Full-Text Engine. These properties can be set and retrieved by using **sp_fulltext_service**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
FULLTEXTSERVICEPROPERTY ('property')  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *property*  
 Is an expression containing the name of the full-text service-level property. The table lists the properties and provides descriptions of the information returned.  
  
> [!NOTE]
>  The following properties will be removed in a future release of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: **ConnectTimeout**, **DataTimeout**, and **ResourceUsage**. Avoid using these properties in new development work, and plan to modify applications that currently use any of them.  
  
|Property|Value|  
|--------------|-----------|  
|**ResourceUsage**|Returns 0. Supported for backward compatibility only.|  
|**ConnectTimeout**|Returns 0. Supported for backward compatibility only.|  
|**IsFulltextInstalled**|The full-text component is installed with the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> 0 = Full-text is not installed.<br /><br /> 1 = Full-text is installed.<br /><br /> NULL = Invalid input, or error.|  
|**DataTimeout**|Returns 0. Supported for backward compatibility only.|  
|**LoadOSResources**|Indicates whether operating system word breakers and filters are registered and used with this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, this property is disabled to prevent inadvertent behavior changes by updates made to the operating system (OS). Enabling use of OS resources provides access to resources for languages and document types registered with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Indexing Service, but that do not have an instance-specific resource installed. If you enable the loading of OS resources, ensure that the OS resources are trusted signed binaries; otherwise, they cannot be loaded when **VerifySignature** is set to 1.<br /><br /> 0 = Use only filters and word breakers specific to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> 1 = Load OS filters and word breakers.|  
|**VerifySignature**|Specifies whether only signed binaries are loaded by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Search Service. By default, only trusted, signed binaries are loaded.<br /><br /> 0 = Do not verify whether or not binaries are signed.<br /><br /> 1 = Verify that only trusted, signed binaries are loaded.|  
  
## Return Types  
 **int**  
  
## Examples  
 The following example checks whether only signed binaries are loaded, and the return value indicates that this verification is not occurring.  
  
```sql  
SELECT fulltextserviceproperty('VerifySignature');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-----------   
0  
```  
  
 Note that to set signature verification back to its default value, 1, you can use the following `sp_fulltext_service` statement:  
  
```sql  
EXEC sp_fulltext_service @action='verify_signature', @value=1;  
GO  
```  
  
## See Also  
 [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sp_fulltext_service &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)  
  
  
