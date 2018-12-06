---
title: "DROP INDEX (Selective XML Indexes) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP XML INDEX statement"
dev_langs: 
  - "TSQL"
ms.assetid: 4779ae84-e5f4-4d04-8fc1-e24a6631b428
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# DROP INDEX (Selective XML Indexes)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Drops an existing selective XML index or secondary selective XML index in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP INDEX index_name ON <object>  
    [ WITH ( <drop_index_option> [ ,...n ] ) ]  
  
<object> ::=  
{  
    [ database_name. [ schema_name ] . | schema_name. ]   
        table_or_view_name  
}  
  
<drop_index_option> ::=  
{  
    MAXDOP = max_degree_of_parallelism  
    | ONLINE = { ON | OFF }  
}  
```  
  
##  <a name="Arguments"></a> Arguments  
 *index_name*  
 Is the name of the existing index to drop.  
  
 *\< object>* 
 Is the table that contains the indexed XML column. Use one of the following formats:  
  
-   `database_name.schema_name.table_name`  
  
-   `database_name..table_name`  
  
-   `schema_name.table_name`  
  
-   `table_name`  
  
 *\<drop_index_option>* 
 For information about the drop index options, see [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md).  
  
## Security  
  
### Permissions  
 ALTER permission on the table or view is required to run DROP INDEX. This permission is granted by default to the sysadmin fixed server role and the db_ddladmin and db_owner fixed database roles.  
  
## Example  
 The following example shows a DROP INDEX statement.  
  
```  
DROP INDEX sxi_index ON tbl;  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)   
 [Create, Alter, and Drop Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-selective-xml-indexes.md)  
  
  

