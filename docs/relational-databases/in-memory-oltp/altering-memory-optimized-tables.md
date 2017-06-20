---
title: "Altering Memory-Optimized Tables | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/19/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 690b70b7-5be1-4014-af97-54e531997839
caps.latest.revision: 20
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Altering Memory-Optimized Tables
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Schema and index changes on memory-optimized tables can be performed by using the ALTER TABLE statement. In SQL Server 2016 ALTER TABLE operations on memory-optimized tables are OFFLINE, meaning that the table is not available for querying while the operation is in progress. The database application can continue to run, and any operation that is accessing the table is blocked until the alteration process is completed. It is possible to combine multiple ADD, DROP or ALTER operations in a single ALTER TABLE statement.
  
## ALTER TABLE  
 
The ALTER TABLE syntax is used for making changes to the table schema, as well as for adding, deleting, and rebuilding indexes. Indexes are considered part of the table definition:  
  
-   The syntax ALTER TABLE … ADD/DROP/ALTER INDEX is supported only for memory-optimized tables.  
  
-   Without using an ALTER TABLE statement, the statements CREATE INDEX and DROP INDEX and ALTER INDEX are *not* supported for indexes on memory-optimized tables.  
  
 The following is the syntax for the ADD and DROP and ALTER INDEX clauses on the ALTER TABLE statement.  
  
```
| ADD   
     {   
        <column_definition>  
      | <table_constraint>  
      | <table_index>    
     } [ ,...n ]  
  
| DROP   
     {  
         [ CONSTRAINT ]   
         {   
              constraint_name   
         } [ ,...n ]  
         | COLUMN   
         {  
              column_name   
         } [ ,...n ]  
         | INDEX   
         {  
              index_name   
         } [ ,...n ]  
     } [ ,...n ]  
  
| ALTER INDEX index_name  
     {   
         REBUILD WITH ( <rebuild_index_option> )     
     }  
}  
```  
  
 The following types of alterations are supported.  
  
-   Changing the bucket count  
  
-   Adding and removing an index  
  
-   Changing, adding and removing a column  
  
-   Adding and removing a constraint  
  
 For more information on ALTER TABLE functionality and the complete syntax, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
## Schema-bound Dependency  
 Natively compiled stored procedures are required to be schema-bound, meaning they have a schema-bound dependency on the memory optimized tables they access, and the columns they reference. A schema-bound dependency is a relationship between two entities that prevents the referenced entity from being dropped or incompatibly modified as long as the referencing entity exists.  
  
 For example, if a schema-bound natively compiled stored procedure references a column *c1* from table *mytable*, column *c1* cannot be dropped. Similarly, if there is such a procedure with an INSERT statement without column list (e.g., `INSERT INTO dbo.mytable VALUES (...)`), then no column in the table can be dropped.  
 
## Logging of ALTER TABLE on memory-optimized tables
On a memory-optimized table, most ALTER TABLE scenarios now run in parallel and result in an optimization of writes to the transaction log. The optimization is achieved by only logging the metadata changes to the transaction log. However, the following ALTER TABLE operations run single-threaded and are not log-optimized.

The single-threaded operation in this case would log the entire content of the altered table to the transaction log. A list of single-threaded operations follows:

- Alter or add a column to use a large object (LOB) type: nvarchar(max), varchar(max), or varbinary(max).

- Add or drop a COLUMNSTORE index.

- Almost anything that affects an [off-row column](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md).

    - Cause an on-row column to move off-row.

    - Cause an off-row column to move on-row.

    - Create a new off-row column.

    - *Exception:* Lengthening an already off-row column is logged in the optimized way. 
  
## Examples  
 The following example alters the bucket count of an existing hash index. This rebuilds the hash index with the new bucket count while other properties of the hash index remain the same.  
  
```tsql
ALTER TABLE Sales.SalesOrderDetail_inmem   
       ALTER INDEX imPK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID  
              REBUILD WITH (BUCKET_COUNT=67108864);  
GO
```
  
 The following example adds a column with a NOT NULL constraint and with a DEFAULT definition, and uses WITH VALUES to provide values for each existing row in the table. If WITH VALUES is not used, each row has the value NULL in the new column.  
  
```tsql  
ALTER TABLE Sales.SalesOrderDetail_inmem  
       ADD Comment NVARCHAR(100) NOT NULL DEFAULT N'' WITH VALUES;  
GO
```  
  
 The following example adds a primary key constraint to an existing column.  
  
```tsql
CREATE TABLE dbo.UserSession (   
   SessionId int not null,   
   UserId int not null,   
   CreatedDate datetime2 not null,   
   ShoppingCartId int,   
   index ix_UserId nonclustered hash (UserId) with (bucket_count=400000)   
)   
WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_ONLY) ;  
GO  
  
ALTER TABLE dbo.UserSession  
       ADD CONSTRAINT PK_UserSession PRIMARY KEY NONCLUSTERED (SessionId);  
GO
```  
  
 The following example removes an index.  
  
```tsql
ALTER TABLE Sales.SalesOrderDetail_inmem  
       DROP INDEX ix_ModifiedDate;  
GO
```  
  
 The following example adds an index.  
  
```tsql  
ALTER TABLE Sales.SalesOrderDetail_inmem  
       ADD INDEX ix_ModifiedDate (ModifiedDate);  
GO  
```  
  
 The following example adds multiple columns, with an index and constraints.  
  
```tsql
ALTER TABLE Sales.SalesOrderDetail_inmem  
       ADD    CustomerID int NOT NULL DEFAULT -1 WITH VALUES,  
              ShipMethodID int NOT NULL DEFAULT -1 WITH VALUES,  
              INDEX ix_Customer (CustomerID);  
GO  
```


<a name="logging-of-alter-table-on-memory-optimized-tables-124"></a>


## See Also  

[Memory-Optimized Tables](../../relational-databases/in-memory-oltp/memory-optimized-tables.md)  
  

