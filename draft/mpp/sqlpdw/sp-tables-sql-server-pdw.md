---
title: "sp_tables (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 608ca1f5-e24c-46c0-9460-f9a67f68c33e
caps.latest.revision: 6
author: BarbKess
---
# sp_tables (SQL Server PDW)
Returns a list of objects that can be queried in the current environment. This means any table or view, except synonym objects.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_tables [ [ @table_name= ] 'name' ]   
     [ , [ @table_owner= ] 'owner' ]   
     [ , [ @table_qualifier= ] 'qualifier' ]   
     [ , [ @table_type= ] "type" ]   
     [ , [@fUsePattern = ] 'fUsePattern'];  
```  
  
## Arguments  
[ **@table_name=** ] **'***name***'**  
Is the table used to return catalog information. *name* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported.  
  
[ **@table_owner=** ] **'***owner***'**  
Is the table owner of the table used to return catalog information. *owner* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported. If the owner is not specified, the default table visibility rules of the underlying DBMS apply.  
  
In SQL Server, if the current user owns a table with the specified name, the columns of that table are returned. If the owner is not specified and the current user does not own a table with the specified name, this procedure looks for a table with the specified name owned by the database owner. If one exists, the columns of that table are returned.  
  
[ **@table_qualifier=** ] **'***qualifier***'**  
Is the name of the table qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier***.***owner***.***name*). In SQL Server, this column represents the database name. In some products, it represents the server name of the table's database environment.  
  
[ **,** [ **@table_type=** ] **"'***type***'**, **'**type**'"** ]  
Is a list of values, separated by commas, that gives information about all tables of the table types that are specified. These include **TABLE**, **SYSTEMTABLE**, and **VIEW**. *type* is **varchar(100)**, with a default of NULL.  
  
> [!NOTE]  
> Single quotation marks must enclose each table type, and double quotation marks must enclose the whole parameter. Table types must be uppercase. If SET QUOTED_IDENTIFIER is ON, each single quotation mark must be doubled and the whole parameter must be enclosed in single quotation marks.  
  
[ **@fUsePattern =** ] **'***fUsePattern***'**  
Determines whether the underscore ( _ ), percent ( % ), and bracket ( [ or ] ) characters are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *fUsePattern* is **bit**, with a default of 1.  
  
## Return Code Values  
None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**TABLE_QUALIFIER**|**sysname**|Table qualifier name. In SQL Server, this column represents the database name. This field can be NULL.|  
|**TABLE_OWNER**|**sysname**|Table owner name. In SQL Server, this column represents the name of the database user who created the table. This field always returns a value.|  
|**TABLE_NAME**|**sysname**|Table name. This field always returns a value.|  
|**TABLE_TYPE**|**varchar(32)**|Table, system table, or view.|  
|**REMARKS**|**varchar(254)**|SQL Server does not return a value for this column.|  
  
## Remarks  
For maximum interoperability, the client should assume only SQL-92-standard SQL pattern matching (the % and _ wildcard characters).  
  
**sp_tables** is equivalent to **SQLTables** in ODBC. The results returned are ordered by **TABLE_TYPE**, **TABLE_QUALIFIER**, **TABLE_OWNER**, and **TABLE_NAME**.  
  
## Permissions  
Requires SELECT permission on the schema.  
  
## Examples  
  
### A. Returning a list of objects that can be queried in the current environment  
The following example returns a list of objects that can be queries in the current environment.  
  
```  
EXEC sp_tables ;  
```  
  
### B. Returning information about the tables in a specified schema  
The following example returns information about the dimension tables in the `AdventureWorksPDW201` database.  
  
```  
USE AdventureWorksPDW2012;  
GO  
EXEC sp_tables   
   @table_name = 'Dim%',  
   @table_owner = 'dbo',  
   @table_qualifier = 'AdventureWorksPDW2012';  
```  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../sqlpdw/procedures-sql-server-pdw.md)  
  
