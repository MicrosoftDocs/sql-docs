---
title: "Transact-SQL Syntax Conventions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "sql13.TSQLExpandPortal.f1"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "conventions [SQL Server]"
  - "Applies to section in Transact-SQL topics"
  - "code example conventions [SQL Server]"
  - "objects [SQL Server], names"
  - "code [SQL Server], conventions"
  - "multipart names [SQL Server]"
  - "Transact-SQL syntax conventions"
  - "syntax conventions [SQL Server]"
  - "code [SQL Server]"
  - "Transact-SQL"
  - "naming conventions [SQL Server]"
  - "syntax [SQL Server], Transact-SQL"
ms.assetid: 35fbcf7f-8b55-46cd-a957-9b8c7b311241
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Transact-SQL Syntax Conventions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

The following table lists and describes conventions that are used in the syntax diagrams in the [!INCLUDE[tsql](../../includes/tsql-md.md)] Reference.  
  
|Convention|Used for|  
|----------------|--------------|  
|UPPERCASE|[!INCLUDE[tsql](../../includes/tsql-md.md)] keywords.|  
|_italic_|User-supplied parameters of [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax.|  
|**bold**|Type database names, table names, column names, index names, stored procedures, utilities, data type names, and text exactly as shown.|  
|underline|Indicates the default value applied when the clause that contains the underlined value is omitted from the statement.|  
|&#124; (vertical bar)|Separates syntax items enclosed in brackets or braces. You can use only one of the items.|  
|`[ ]` (brackets)|Optional syntax items. Don't type the brackets.|  
|{ } (braces)|Required syntax items. Don't type the braces.|  
|[**,**..._n_]|Indicates the preceding item can be repeated _n_ number of times. The occurrences are separated by commas.|  
|[..._n_]|Indicates the preceding item can be repeated _n_ number of times. The occurrences are separated by blanks.|  
|;|[!INCLUDE[tsql](../../includes/tsql-md.md)] statement terminator. Although the semicolon isn't required for most statements in this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it will be required in a future version.|  
|\<label> ::=|The name for a block of syntax. Use this convention to group and label sections of lengthy syntax or a unit of syntax that you can use in more than one location within a statement. Each location in which the block of syntax could be used is indicated with the label enclosed in chevrons: \<label>.<br /><br /> A set is a collection of expressions, for example \<grouping set>; and a list is a collection of sets, for example \<composite element list>.|  
  
## Multipart Names  
Unless specified otherwise, all [!INCLUDE[tsql](../../includes/tsql-md.md)] references to the name of a database object can be a four-part name in the following form:  
  
_server\_name_.[_database\_name_].[_schema\_name_]._object\_name_  
  
| _database\_name_.[_schema\_name_]._object\_name_  
 
| _schema\_name_._object\_name_  
  
| _object\_name_  
  
_server\_name_  
Specifies a linked server name or remote server name.  
  
_database\_name_  
Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database when the object resides in a local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When the object is in a linked server, *database_name* specifies an OLE DB catalog.  
  
_schema\_name_  
Specifies the name of the schema that contains the object if the object is in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. When the object is in a linked server, *schema_name* specifies an OLE DB schema name.  
  
_object\_name_  
Refers to the name of the object.  
  
When referencing a specific object, you don't always have to specify the server, database, and schema for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to identify the object. However, if the object can't be found, an error is returned.  
  
> [!NOTE]  
> To avoid name resolution errors, we recommend specifying the schema name whenever you specify a schema-scoped object.  
  
To omit intermediate nodes, use periods to indicate these positions. The following table shows the valid formats of object names.  
  
|Object reference format|Description|  
|-----------------------------|-----------------|  
|_server_._database_._schema_._object_|Four-part name.|  
|_server_._database_.._object_|Schema name is omitted.|  
|_server_.._schema_._object_|Database name is omitted.|  
|_server_..._object_|Database and schema name are omitted.|  
|_database_._schema_._object_|Server name is omitted.|  
|_database_.._object_|Server and schema name are omitted.|  
|_schema_._object_|Server and database name are omitted.|  
|_object_|Server, database, and schema name are omitted.|  
  
## Code Example Conventions  
Unless stated otherwise, the examples provided in the [!INCLUDE[tsql](../../includes/tsql-md.md)] Reference were tested by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and its default settings for the following options:  
  
-   ANSI_NULLS  
-   ANSI_NULL_DFLT_ON  
-   ANSI_PADDING  
-   ANSI_WARNINGS  
-   CONCAT_NULL_YIELDS_NULL  
-   QUOTED_IDENTIFIER  
  
Most code examples in the [!INCLUDE[tsql](../../includes/tsql-md.md)] Reference have been tested on servers that are running a case-sensitive sort order. The test servers were typically running the ANSI/ISO 1252 code page.  
  
Many code examples prefix Unicode character string constants with the letter **N**. Without the **N** prefix, the string is converted to the default code page of the database. This default code page may not recognize certain characters.  
  
## "Applies to" References  
The [!INCLUDE[tsql](../../includes/tsql-md.md)] reference includes articles related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)].   

There's a section near the top of each article indicating which products support the article's subject. If a product is omitted, then the feature described by the article isn't available in that product. For example, availability groups were introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. The **CREATE AVAILABILITY GROUP** article indicates it applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]) because it doesn't apply to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
The general subject of the article might be used in a product, but all of the arguments aren't supported in some cases. For example, contained database users were introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. Use the **CREATE USER** statement in any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product, however the **WITH PASSWORD** syntax can't be used with older versions. Additional **Applies to** sections are inserted into the appropriate argument descriptions in the body of the article.  
  
## See Also  
[Transact-SQL Reference &#40;Database Engine&#41;](../../t-sql/transact-sql-reference-database-engine.md)    
[Reserved Keywords &#40;Transact SQL&#41;](../../t-sql/language-elements/reserved-keywords-transact-sql.md)      
[Transact-SQL Design Issues](https://msdn.microsoft.com/library/dd193411.aspx)    
[Transact-SQL Naming Issues](https://msdn.microsoft.com/library/dd193246.aspx)        
[Transact-SQL Performance Issues](https://msdn.microsoft.com/library/dd172117.aspx)    


