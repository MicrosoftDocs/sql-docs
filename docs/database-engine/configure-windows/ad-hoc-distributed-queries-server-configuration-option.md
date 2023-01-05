---
title: "ad hoc distributed queries Server Configuration Option"
description: Find out how to enable ad hoc distributed queries in SQL Server. You can then use OPENROWSET and OPENDATASOURCE to connect to remote OLE DB data sources.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/18/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "OPENROWSET function, ad hoc distributed queries option"
  - "Ad Hoc Distributed Queries option"
  - "ad hoc distributed queries"
  - "7415 (Database Engine Error)"
  - "OPENDATASOURCE function, ad hoc distributed queries option"
  - "ad hoc access"
---
# ad hoc distributed queries Server Configuration Option

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow ad hoc distributed queries using OPENROWSET and OPENDATASOURCE. When this option is set to 1, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows ad hoc access. When this option isn't set or is set to 0, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow ad hoc access.  
  
Ad hoc distributed queries use the OPENROWSET and OPENDATASOURCE functions to connect to remote data sources that use OLE DB. OPENROWSET and OPENDATASOURCE should be used only to reference OLE DB data sources that are accessed infrequently. For any data sources that will be accessed more than several times, define a linked server.  
  
Enabling the use of ad hoc names means that any authenticated login to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can access the provider. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators should enable this feature for providers that are safe to be accessed by any local login.  
  
## Remarks

If you attempt to make an ad hoc connection with **ad hoc distributed queries** disabled, you'll see the following error:

```output
Msg 7415, Level 16, State 1, Line 1  
  
Ad hoc access to OLE DB provider 'Microsoft.ACE.OLEDB.12.0' has been denied. You must access this provider through a linked server.  
```
  
## Examples

 The following example enables ad hoc distributed queries and then queries a server named `Seattle1` using the `OPENROWSET` function.  
  
```sql
sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'Ad Hoc Distributed Queries', 1;  
RECONFIGURE;  
GO  
  
SELECT a.*  
FROM OPENROWSET('MSOLEDBSQL', 'Server=Seattle1;Trusted_Connection=yes;',  
     'SELECT GroupName, Name, DepartmentID  
      FROM AdventureWorks2012.HumanResources.Department  
      ORDER BY GroupName, Name') AS a;  
GO  
```

## Azure SQL Database and Azure SQL Managed Instance

See the [Features comparison: Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison) for reference.

## See also

- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
- [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md)
- [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)
- [OPENDATASOURCE &#40;Transact-SQL&#41;](../../t-sql/functions/opendatasource-transact-sql.md)
- [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)  
