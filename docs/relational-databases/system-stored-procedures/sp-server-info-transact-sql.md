---
title: "sp_server_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_server_info"
  - "sp_server_info_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_server_info"
ms.assetid: 2dc2c262-3cfa-4a84-8127-3632ba583543
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_server_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a list of attribute names and matching values for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the database gateway, or the underlying data source.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_server_info [[@attribute_id = ] 'attribute_id']  
```  
  
## Arguments  
 [ **@attribute_id =** ] **'***attribute_id***'**  
 Is the integer ID of the attribute. *attribute_id* is **int**, with a default of NULL.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**ATTRIBUTE_ID**|**int**|ID number of the attribute.|  
|**ATTRIBUTE_NAME**|**varchar(**60**)**|Attribute name.|  
|**ATTRIBUTE_VALUE**|**varchar(**255**)**|Current setting of the attribute.|  
  
 The following table lists the attributes. [!INCLUDE[msCoName](../../includes/msconame-md.md)] ODBC client libraries currently use attributes **1**, **2**, **18**, **22**, and **500** at connection time.  
  
|ATTRIBUTE_ID|ATTRIBUTE_NAME Description|ATTRIBUTE_VALUE|  
|-------------------|---------------------------------|----------------------|  
|**1**|DBMS_NAME|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|**2**|DBMS_VER|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] - *x.xx.xxxx*|  
|**10**|OWNER_TERM|owner|  
|**11**|TABLE_TERM|table|  
|**12**|MAX_OWNER_NAME_LENGTH|128|  
|**13**|TABLE_LENGTH<br /><br /> Specifies the maximum number of characters for a table name.|128|  
|**14**|MAX_QUAL_LENGTH<br /><br /> Specifies the maximum length of the name for a table qualifier (the first part of a three-part table name).|128|  
|**15**|COLUMN_LENGTH<br /><br /> Specifies the maximum number of characters for a column name.|128|  
|**16**|IDENTIFIER_CASE<br /><br /> Specifies the user-defined names (table names, column names, stored procedure names) in the database (the case of the objects in the system catalogs).|SENSITIVE|  
|**17**|TX_ISOLATION<br /><br /> Specifies the initial transaction isolation level the server assumes, which corresponds to an isolation level defined in SQL-92.|2|  
|**18**|COLLATION_SEQ<br /><br /> Specifies the ordering of the character set for this server.|charset=iso_1 sort_order=dictionary_iso charset_num=1 sort_order_num=51|  
|**19**|SAVEPOINT_SUPPORT<br /><br /> Specifies whether the underlying DBMS supports named savepoints.|Y|  
|**20**|MULTI_RESULT_SETS<br /><br /> Specifies whether the underlying database or the gateway itself supports multiple result sets (multiple statements can be sent through the gateway with multiple result sets returned to the client).|Y|  
|**22**|ACCESSIBLE_TABLES<br /><br /> Specifies whether in **sp_tables**, the gateway returns only tables, views, and so on, accessible by the current user (that is, the user who has at least SELECT permissions for the table).|Y|  
|**100**|USERID_LENGTH<br /><br /> Specifies the maximum number of characters for a username.|128|  
|**101**|QUALIFIER_TERM<br /><br /> Specifies the DBMS vendor term for a table qualifier (the first part of a three-part name).|database|  
|**102**|NAMED_TRANSACTIONS<br /><br /> Specifies whether the underlying DBMS supports named transactions.|Y|  
|**103**|SPROC_AS_LANGUAGE<br /><br /> Specifies whether stored procedures can be executed as language events.|Y|  
|**104**|ACCESSIBLE_SPROC<br /><br /> Specifies whether in **sp_stored_procedures**, the gateway returns only stored procedures that are executable by the current user.|Y|  
|**105**|MAX_INDEX_COLS<br /><br /> Specifies the maximum number of columns in an index for the DBMS.|16|  
|**106**|RENAME_TABLE<br /><br /> Specifies whether tables can be renamed.|Y|  
|**107**|RENAME_COLUMN<br /><br /> Specifies whether columns can be renamed.|Y|  
|**108**|DROP_COLUMN<br /><br /> Specifies whether columns can be dropped.|Y|  
|**109**|INCREASE_COLUMN_LENGTH<br /><br /> Specifies whether column size can be increased.|Y|  
|**110**|DDL_IN_TRANSACTION<br /><br /> Specifies whether DDL statements can appear in transactions.|Y|  
|**111**|DESCENDING_INDEXES<br /><br /> Specifies whether descending indexes are supported.|Y|  
|**112**|SP_RENAME<br /><br /> Specifies whether a stored procedure can be renamed.|Y|  
|**113**|REMOTE_SPROC<br /><br /> Specifies whether stored procedures can be executed through the remote stored procedure functions in DB-Library.|Y|  
|**500**|SYS_SPROC_VERSION<br /><br /> Specifies the version of the catalog stored procedures currently implemented.|Current version number|  
  
## Remarks  
 **sp_server_info** returns a subset of the information provided by **SQLGetInfo** in ODBC.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## See Also  
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
