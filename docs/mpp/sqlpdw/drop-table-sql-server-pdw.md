---
title: "DROP TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fee19723-b3d6-4a82-ad89-20ce6deccd49
caps.latest.revision: 34
author: BarbKess
---
# DROP TABLE (SQL Server PDW)
Removes and deletes a user-defined table from  SQL Server PDW.  
  
To drop an external table, see [DROP EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-external-table-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP TABLE [ database_name . [ schema_name ] . | schema_name . ]table_name   
[;]  
```  
  
## Arguments  
[ *database_name* . [*schema_name* ] . | *schema_name* . ] *table_name*  
The one- to three-part name of the table to remove and delete. The table name can optionally include the schema, or the database and schema.  
  
## Permissions  
Requires **CONTROL** permission on the table, **ALTER** permission on the schema to which the table belongs, or membership in the **db_ddladmin** fixed database role. To drop a partitioned table also requires **ALTER ANY DATASPACE** permission or membership in the **db_ddladmin** fixed database role.  
  
## General Remarks  
Dropping a table removes all table-related data, including the table definition, indexes, row data, constraints, and table permissions.  
  
## Limitations and Restrictions  
Any views dependent on the table will be broken after the table is dropped.  
  
## Locking  
Takes an exclusive lock on the table. Takes a shared lock on the DATABASE, and SCHEMA objects.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP TABLE SalesPerson;  
DROP TABLE dbo.SalesPerson;  
DROP TABLE EasternDivision.dbo.SalesPerson;  
```  
  
### B. Dropping a table from the current database  
The following example removes the `ProductVendor1` table, its data, indexes, and any dependent views from the current database.  
  
```  
DROP TABLE ProductVendor1;  
```  
  
### C. Dropping a table from another database  
The following example drops the `SalesPerson` table in the `EasternDivision` database.  
  
```  
DROP TABLE EasternDivision.dbo.SalesPerson;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DROP EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-external-table-sql-server-pdw.md)  
[CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md)  
[ALTER TABLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-table-sql-server-pdw.md)  
  
