---
title: "CREATE XML INDEX (Selective XML Indexes) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 1f510151-41d5-45c2-9cd0-b1ca0246fffe
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# CREATE XML INDEX (Selective XML Indexes)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Creates a new secondary selective XML index on a single path that is already indexed by an existing selective XML index. You can also create primary selective XML indexes. For information, see [Create, Alter, and Drop Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-selective-xml-indexes.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE XML INDEX index_name  
    ON <table_object> ( xml_column_name )  
    USING XML INDEX sxi_index_name  
    FOR ( <xquery_or_sql_values_path> )  
    [WITH ( <index_options> )]  
  
<table_object> ::=   
{ [database_name. [schema_name ] . | schema_name. ] table_name }  
  
<xquery_or_sql_values_path>::=   
<path_name>   
  
<path_name> ::=   
character string literal  
  
<xmlnamespace_list> ::=   
<xmlnamespace_item> [, <xmlnamespace_list>]  
  
<xmlnamespace_item> ::=   
xmlnamespace_uri AS xmlnamespace_prefix  
  
<index_options> ::=   
(    
  | PAD_INDEX  = { ON | OFF }  
  | FILLFACTOR = fillfactor  
  | SORT_IN_TEMPDB = { ON | OFF }  
  | IGNORE_DUP_KEY = OFF  
  | DROP_EXISTING = { ON | OFF }  
  | ONLINE = OFF  
  | ALLOW_ROW_LOCKS = { ON | OFF }  
  | ALLOW_PAGE_LOCKS = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
)  
```  
  
##  <a name="Arguments"></a> Arguments  
 *index_name*  
 Is the name of the new index to create. Index names must be unique within a table, but do not have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 ON *\<table_object>* 
 Is the table that contains the XML column to index. You can use the following formats:  
  
-   `database_name.schema_name.table_name`  
  
-   `database_name..table_name`  
  
-   `schema_name.table_name`  
  
 *xml_column_name*  
 Is the name of the XML column that contains the path to index.  
  
 USING XML INDEX *sxi_index_name*  
 Is the name of the existing selective XML index.  
  
 FOR **(** \<xquery_or_sql_values_path> **)** 
 Is the name of the indexed path on which to create the secondary selective XML index. The path to index is the assigned name from the CREATE SELECTIVE XML INDEX statement. For more information, see [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md).  
  
 WITH \<index_options> 
 For information about the index options, see [CREATE XML INDEX](../../t-sql/statements/create-xml-index-selective-xml-indexes.md).  
  
## Remarks  
 There can be multiple secondary selective XML indexes on every XML column in the base table.  
  
## Limitations and Restrictions  
 A selective XML index on an XML column must exist before secondary selective XML indexes can be created on the column.  
  
## Security  
  
### Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
## Examples  
 The following example creates a secondary selective XML index on the path `pathabc`. The path to index is the assigned name from the [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md).  
  
```  
CREATE XML INDEX filt_sxi_index_c  
ON Tbl(xmlcol)  
USING XML INDEX sxi_index  
FOR ( pathabc );  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)   
 [Create, Alter, and Drop Secondary Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-secondary-selective-xml-indexes.md)  
  
  

