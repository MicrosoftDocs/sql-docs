---
title: "Read (Database Engine)"
description: "Read (Database Engine) by using CSharp"
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/16/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "Read_TSQL"
  - "Read"
helpviewer_keywords:
  - "Read [Database Engine]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Read (Database Engine) by using CSharp
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Read reads binary representation of **SqlHierarchyId** from the passed-in **BinaryReader** and sets the **SqlHierarchyId** object to that value. Read cannot be called by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. Use CAST or CONVERT instead.
  
## Syntax  

```csharp
void Read(BinaryReader r)
```  

## Arguments
*r*  
 The **BinaryReader** object that produces a binary stream corresponding to a binary representation of a **hierarchyid** node.  
  
## Return types
 **CLR return type:void**  
  
## Remarks  
 Read does not validate its input. If an invalid binary input is given, Read might raise an exception. Or, it might succeed and produce an invalid **SqlHierarchyId** object whose methods can either give unpredictable results or raise an exception.  
  
 Read can only be called on a newly created **SqlHierarchyId** object.  
  
 Read is used internally by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when it is necessary, such as when writing data to **hierarchyid** column. Read is also called internally when a conversion is done between **varbinary** and **hierarchyid**.  
  
## Examples  
  
```csharp
Byte[] encoding = new byte[] { 0x58 };  
MemoryStream stream = new MemoryStream(encoding, false /*not writable*/);  
BinaryReader br = new BinaryReader(stream);  
SqlHierarchyId hid = new SqlHierarchyId();  
hid.Read(br);   
```  
  
## Related content

- [Write (Database Engine)](write-database-engine.md)
- [ToString (Database Engine)](tostring-database-engine.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [hierarchyid data type method reference](hierarchyid-data-type-method-reference.md)
