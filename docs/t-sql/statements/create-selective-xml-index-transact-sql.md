---
title: "CREATE SELECTIVE XML INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 1d769f62-f646-4057-b93a-bf5f90e935ed
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# CREATE SELECTIVE XML INDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Creates a new selective XML index on the specified table and XML column. Selective XML indexes improve the performance of XML indexing and querying by indexing only the subset of nodes that you typically query. You can also create secondary selective XML indexes. For information, see [Create, Alter, and Drop Secondary Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-secondary-selective-xml-indexes.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE SELECTIVE XML INDEX index_name  
    ON <table_object> (xml_column_name)  
    [WITH XMLNAMESPACES (<xmlnamespace_list>)]  
    FOR (<promoted_node_path_list>)  
    [WITH (<index_options>)]  
  
<table_object> ::=  
 { [database_name. [schema_name ] . | schema_name. ] table_name }  
  
<promoted_node_path_list> ::=   
<named_promoted_node_path_item> [, <promoted_node_path_list>]  
  
<named_promoted_node_path_item> ::=   
<path_name> = <promoted_node_path_item>  
  
<promoted_node_path_item>::=  
<xquery_node_path_item> | <sql_values_node_path_item>  
  
<xquery_node_path_item> ::=   
<node_path> [AS XQUERY <xsd_type_or_node_hint>] [SINGLETON]  
  
<xsd_type_or_node_hint> ::=   
[<xsd_type>] [MAXLENGTH(x)] | node()  
  
<sql_values_node_path_item> ::=  
<node_path> AS SQL <sql_type> [SINGLETON]  
  
<node_path> ::=   
character_string_literal  
  
<xsd_type> ::=   
character_string_literal  
  
<sql_type> ::=   
identifier  
  
<path_name> ::=   
identifier  
  
<xmlnamespace_list> ::=   
<xmlnamespace_item> [, <xmlnamespace_list>]  
  
<xmlnamespace_item> ::=   
<xmlnamespace_uri> AS <xmlnamespace_prefix>  
  
<xml_namespace_uri> ::=   
character_string_literal  
  
<xml_namespace_prefix> ::=   
identifier  
  
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
  
 *\<table_object>* 
 Is the table that contains the XML column to index. Use one of the following formats:  
  
-   `database_name.schema_name.table_name`  
  
-   `database_name..table_name`  
  
-   `schema_name.table_name`  
  
-   `table_name`  
  
 *xml_column_name*  
 Is the name of the XML column that contains the paths to index.  
  
 [WITH XMLNAMESPACES **(**\<xmlnamespace_list>**)**] 
 Is the list of namespaces used by the paths to index. For information about the syntax of the WITH XMLNAMESPACES clause, see [WITH XMLNAMESPACES &#40;Transact-SQL&#41;](../../t-sql/xml/with-xmlnamespaces.md).  
  
 FOR **(**\<promoted_node_path_list>**)** 
 Is the list of paths to index with optional optimization hints. For information about the paths and the optimization hints that you can specify in the CREATE or ALTER statement, see [Specify Paths and Optimization Hints for Selective XML Indexes](../../relational-databases/xml/specify-paths-and-optimization-hints-for-selective-xml-indexes.md).  
  
 WITH *\<index_options>* 
 For information about the index options, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/create-xml-index-selective-xml-indexes.md).  
  
## Best Practices  
 Create a selective XML index instead of an ordinary XML index in most cases for better performance and more efficient storage. However, a selective XML index is not recommended when either of the following conditions is true:  
  
-   You need to map a large number of node paths.  
  
-   You need to support queries for unknown elements or elements in an unknown location.  
  
## Limitations and Restrictions  
 For information about limitations and restrictions, see [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md).  
  
## Security  
  
### Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
## Examples  
 The following example shows the syntax for creating a selective XML index. It also shows several variations of the syntax for describing the paths to be indexed, with optional optimization hints.  
  
```  
CREATE TABLE Tbl ( id INT PRIMARY KEY, xmlcol XML );  
GO  
CREATE SELECTIVE XML INDEX sxi_index  
ON Tbl(xmlcol)  
FOR(  
    pathab   = '/a/b' as XQUERY 'node()',  
    pathabc  = '/a/b/c' as XQUERY 'xs:double',   
    pathdtext = '/a/b/d/text()' as XQUERY 'xs:string' MAXLENGTH(200) SINGLETON,  
    pathabe = '/a/b/e' as SQL NVARCHAR(100)  
);  
```  
  
 The following example includes a WITH XMLNAMESPACES clause.  
  
```  
CREATE SELECTIVE XML INDEX on T1(C1)  
WITH XMLNAMESPACES ('https://www.tempuri.org/' as myns)  
FOR ( path1 = '/myns:book/myns:author/text()' );  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)   
 [Create, Alter, and Drop Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-selective-xml-indexes.md)   
 [Specify Paths and Optimization Hints for Selective XML Indexes](../../relational-databases/xml/specify-paths-and-optimization-hints-for-selective-xml-indexes.md)  
  
  

