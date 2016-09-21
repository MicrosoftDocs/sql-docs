---
title: "DROP EXTERNAL TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 32462043-77ec-4dfd-8388-5e67704dbf6f
caps.latest.revision: 7
author: BarbKess
---
# DROP EXTERNAL TABLE (SQL Server PDW)
Removes an external table from  SQL Server PDW. This does not delete the external data.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP EXTERNAL TABLE [ database_name . [schema_name ] . | schema_name . ]table_name   
[;]  
```  
  
## Arguments  
[ *database_name* . [*schema_name*] . | *schema_name* . ] *table_name*  
The one- to three-part name of the external table to remove. The table name can optionally include the schema, or the database and schema.  
  
## Permissions  
  
-   Requires **CONTROL** permission on the table, or **ALTER** permission on the schema to which the table belongs, or membership in the **db_ddladmin** fixed database role.  
  
-   Requires **ADMINISTER BULK OPERATIONS** permission.  
  
## General Remarks  
Dropping an external table removes all table-related metadata from SQL Server PDW. It does not delete the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL TABLE SalesPerson;  
DROP EXTERNAL TABLE dbo.SalesPerson;  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
### B. Dropping an external table from the current database  
The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  
  
```  
DROP EXTERNAL TABLE ProductVendor1;  
```  
  
### C. Dropping a table from another database  
The following example drops the `SalesPerson` table in the `EasternDivision` database.  
  
```  
DROP EXTERNAL TABLE EasternDivision.dbo.SalesPerson;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DROP TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-table-sql-server-pdw.md)  
[CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md)  
[ALTER TABLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-table-sql-server-pdw.md)  
  
