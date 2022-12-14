---
title: "ASSEMBLYPROPERTY (Transact-SQL)"
description: "ASSEMBLYPROPERTY (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ASSEMBLYPROPERTY_TSQL"
  - "ASSEMBLYPROPERTY"
helpviewer_keywords:
  - "ASSEMBLYPROPERTY statement"
  - "assemblies [CLR integration], properties"
dev_langs:
  - "TSQL"
---
# ASSEMBLYPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns information about a property of an assembly.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ASSEMBLYPROPERTY('assembly_name', 'property_name')  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*assembly_name*  
The name of the assembly.
  
*property_name*  
The name of a property about which to retrieve information. *property_name* can have one of the following values:
  
|Value|Description|  
|---|---|
|**CultureInfo**|Locale of the assembly.|  
|**PublicKey**|Public key or public key token of the assembly.|  
|**MvID**|Complete, compiler-generated version identification number of the assembly.|  
|**VersionMajor**|Major component (first part) of the four-part version identification number of the assembly.|  
|**VersionMinor**|Minor component (second part) of the four-part version identification number of the assembly.|  
|**VersionBuild**|Build component (third part) of the four-part version identification number of the assembly.|  
|**VersionRevision**|Revision component (fourth part) of the four-part version identification number of the assembly.|  
|**SimpleName**|Simple name of the assembly.|  
|**Architecture**|Processor architecture of the assembly.|  
|**CLRName**|Canonical string that encodes the simple name, version number, culture, public key, and architecture of the assembly. This value uniquely identifies the assembly on the common language runtime (CLR) side.|  
  
## Return type
**sql_variant**
  
## Examples  
This example assumes a `HelloWorld` assembly that is registered in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. For more information, see [Hello World Sample](/previous-versions/sql/sql-server-2016/ff878250(v=sql.130)).
  
```sql
USE AdventureWorks2012;  
GO  
SELECT ASSEMBLYPROPERTY ('HelloWorld' , 'PublicKey');  
```  
  
## See also
[CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)  
[DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)
  
