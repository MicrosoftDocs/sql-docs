---
title: "ALTER INDEX (Selective XML Indexes)"
description: ALTER INDEX (Selective XML Indexes)
author: markingmyname
ms.author: maghan
ms.date: "05/01/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
---
# ALTER INDEX (Selective XML Indexes)
[!INCLUDE [SQL Server Azure SQL Database ](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Modifies an existing selective XML index. The ALTER INDEX statement changes one or more of the following items:  
  
-   The list of indexed paths (FOR clause).  
  
-   The list of namespaces (WITH XMLNAMESPACES clause).  
  
-   The index options (WITH clause).  
  
 You cannot alter secondary selective XML indexes. For more information, see [Create, Alter, and Drop Secondary Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-secondary-selective-xml-indexes.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER INDEX index_name  
    ON <table_object>   
    [WITH XMLNAMESPACES ( <xmlnamespace_list> )]  
    FOR ( <promoted_node_path_action_list> )  
    [WITH ( <index_options> )]  
  
<table_object> ::=   
{ database_name.schema_name.table_name | schema_name.table_name | table_name }  
<promoted_node_path_action_list> ::=   
<promoted_node_path_action_item> [, <promoted_node_path_action_list>]  
  
<promoted_node_path_action_item>::=   
<add_node_path_item_action> | <remove_node_path_item_action>  
  
<add_node_path_item_action> ::=  
ADD <path_name> = <promoted_node_path_item>  
  
<promoted_node_path_item>::=  
<xquery_node_path_item> | <sql_values_node_path_item>  
  
<remove_node_path_item_action> ::= REMOVE <path_name>   
  
<path_name_or_typed_node_path>::=   
<path_name> | <typed_node_path>  
  
<typed_node_path> ::=   
<node_path> [[AS XQUERY <xsd_type_ext>] | [AS SQL <sql_type>]]  
  
<xquery_node_path_item> ::=   
<node_path> [AS XQUERY <xsd_type_or_node_hint>] [SINGLETON]  
  
<xsd_type_or_node_hint> ::=   
[<xsd_type>] [MAXLENGTH(x)] | 'node()'  
  
<sql_values_node_path_item> ::=   
<node_path> AS SQL <sql_type> [SINGLETON]  
  
<node_path> ::=   
character_string_literal  
  
<xsd_type_ext> ::=   
character_string_literal  
  
<sql_type> ::=   
identifier  
  
<path_name> ::=   
identifier  
  
<xmlnamespace_list> ::=   
<xmlnamespace_item> [, <xmlnamespace_list>]  
  
<xmlnamespace_item> ::=   
<xmlnamespace_uri> AS <xmlnamespace_prefix>  
  
<xml_namespace_uri> ::= character_string_literal  
<xml_namespace_prefix> ::= identifier  
  
<index_options> ::=   
(   
  | PAD_INDEX  = { ON | OFF }  
  | FILLFACTOR = fillfactor  
  | SORT_IN_TEMPDB = { ON | OFF }  
  | IGNORE_DUP_KEY =OFF  
  | DROP_EXISTING = { ON | OFF }  
  | ONLINE =OFF  
  | ALLOW_ROW_LOCKS = { ON | OFF }  
  | ALLOW_PAGE_LOCKS = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
)  
```  
  
##  <a name="Arguments"></a> Arguments  
 *index_name*  
 Is the name of the existing index to alter.  
  
 *\<table_object>*  
 Is the table that contains the XML column to index. Use one of the following formats:  
  
-   `database_name.schema_name.table_name`  
  
-   `database_name..table_name`  
  
-   `schema_name.table_name`  
  
-   `table_name`  
  
 [WITH XMLNAMESPACES **(** \<xmlnamespace_list> **)**]  
 Is the list of namespaces used by the paths to index. For information about the syntax of the WITH XMLNAMESPACES clause, see [WITH XMLNAMESPACES &#40;Transact-SQL&#41;](../../t-sql/xml/with-xmlnamespaces.md).  
  
 FOR **(** \<promoted_node_path_action_list> **)**  
 Is the list of indexed paths to add or remove.  
  
-   **ADD a path.** When you ADD a path, you use the same syntax that is used to create paths with the CREATE SELECTIVE XML INDEX statement. For information about the paths that you can specify in the CREATE or ALTER statement, see [Specify Paths and Optimization Hints for Selective XML Indexes](../../relational-databases/xml/specify-paths-and-optimization-hints-for-selective-xml-indexes.md).  
  
-   **REMOVE a path.** When you REMOVE a path, you provide the name that was given to the path when it was created.  
  
 [WITH **(** \<index_options> **)**]  
 You can only specify \<index_options> when you use ALTER INDEX without the FOR clause. When you use ALTER INDEX to add or remove paths in the index, the index options are not valid arguments. For information about the index options, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/create-xml-index-selective-xml-indexes.md).  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
  
> [!IMPORTANT]  
>  When you run an ALTER INDEX statement, the selective XML index is always rebuilt. Be sure to consider the impact of this process on server resources.  
  
## Security  
  
### Permissions  
 ALTER permission on the table or view is required to run ALTER INDEX.  
  
## Examples  
 The following example shows an ALTER INDEX statement. This statement adds the path `'/a/b/m'` to the XQuery part of the index and deletes the path `'/a/b/e'` from the SQL part of the index created in the example in the topic [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md). The path to delete is identified by the name that was given to it when it was created.  
  
```sql  
ALTER INDEX sxi_index  
ON Tbl  
FOR   
(  
    ADD pathm = '/a/b/m' as XQUERY 'node()' ,  
    REMOVE pathabe  
);  
```  
  
 The following example shows an ALTER INDEX statement that specifies index options. Index options are permitted because the statement does not use a FOR clause to add or remove paths.  
  
```sql  
ALTER INDEX sxi_index  
ON Tbl  
PAD_INDEX = ON;  
```  
  
## See Also  
 [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)   
 [Create, Alter, and Drop Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-selective-xml-indexes.md)   
 [Specify Paths and Optimization Hints for Selective XML Indexes](../../relational-databases/xml/specify-paths-and-optimization-hints-for-selective-xml-indexes.md)  
  
  
