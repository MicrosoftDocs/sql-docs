---
title: "Reserved Keywords (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ODBC function calls"
  - "keywords [SQL Server], reserved"
  - "reserved words [SQL Server]"
  - "keywords [SQL Server]"
ms.assetid: ed8b3e27-6796-40f0-aef3-0cac5e0e2418
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Reserved Keywords (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses reserved keywords for defining, manipulating, and accessing databases. Reserved keywords are part of the grammar of the [!INCLUDE[tsql](../../includes/tsql-md.md)] language that is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to parse and understand [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and batches. Although it is syntactically possible to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reserved keywords as identifiers and object names in [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, you can do this only by using delimited identifiers.  
  
 The following table lists [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and SQL Data Warehouse reserved keywords.  
  
||||  
|-|-|-|  
|ADD|EXTERNAL|PROCEDURE|  
|ALL|FETCH|PUBLIC|  
|ALTER|FILE|RAISERROR|  
|AND|FILLFACTOR|READ|  
|ANY|FOR|READTEXT|  
|AS|FOREIGN|RECONFIGURE|  
|ASC|FREETEXT|REFERENCES|  
|AUTHORIZATION|FREETEXTTABLE|REPLICATION|  
|BACKUP|FROM|RESTORE|  
|BEGIN|FULL|RESTRICT|  
|BETWEEN|FUNCTION|RETURN|  
|BREAK|GOTO|REVERT|  
|BROWSE|GRANT|REVOKE|  
|BULK|GROUP|RIGHT|  
|BY|HAVING|ROLLBACK|  
|CASCADE|HOLDLOCK|ROWCOUNT|  
|CASE|IDENTITY|ROWGUIDCOL|  
|CHECK|IDENTITY_INSERT|RULE|  
|CHECKPOINT|IDENTITYCOL|SAVE|  
|CLOSE|IF|SCHEMA|  
|CLUSTERED|IN|SECURITYAUDIT|  
|COALESCE|INDEX|SELECT|  
|COLLATE|INNER|SEMANTICKEYPHRASETABLE|  
|COLUMN|INSERT|SEMANTICSIMILARITYDETAILSTABLE|  
|COMMIT|INTERSECT|SEMANTICSIMILARITYTABLE|  
|COMPUTE|INTO|SESSION_USER|  
|CONSTRAINT|IS|SET|  
|CONTAINS|JOIN|SETUSER|  
|CONTAINSTABLE|KEY|SHUTDOWN|  
|CONTINUE|KILL|SOME|  
|CONVERT|LEFT|STATISTICS|  
|CREATE|LIKE|SYSTEM_USER|  
|CROSS|LINENO|TABLE|  
|CURRENT|LOAD|TABLESAMPLE|  
|CURRENT_DATE|MERGE|TEXTSIZE|  
|CURRENT_TIME|NATIONAL|THEN|  
|CURRENT_TIMESTAMP|NOCHECK|TO|  
|CURRENT_USER|NONCLUSTERED|TOP|  
|CURSOR|NOT|TRAN|  
|DATABASE|NULL|TRANSACTION|  
|DBCC|NULLIF|TRIGGER|  
|DEALLOCATE|OF|TRUNCATE|  
|DECLARE|OFF|TRY_CONVERT|  
|DEFAULT|OFFSETS|TSEQUAL|  
|DELETE|ON|UNION|  
|DENY|OPEN|UNIQUE|  
|DESC|OPENDATASOURCE|UNPIVOT|  
|DISK|OPENQUERY|UPDATE|  
|DISTINCT|OPENROWSET|UPDATETEXT|  
|DISTRIBUTED|OPENXML|USE|  
|DOUBLE|OPTION|USER|  
|DROP|OR|VALUES|  
|DUMP|ORDER|VARYING|  
|ELSE|OUTER|VIEW|  
|END|OVER|WAITFOR|  
|ERRLVL|PERCENT|WHEN|  
|ESCAPE|PIVOT|WHERE|  
|EXCEPT|PLAN|WHILE|  
|EXEC|PRECISION|WITH|  
|EXECUTE|PRIMARY|WITHIN GROUP|  
|EXISTS|PRINT|WRITETEXT|  
|EXIT|PROC||
  
The following table lists reserved keywords that are exclusive to **SQL Data Warehouse**.

|||
|-|-|
|**Label**||
  
 Additionally, the ISO standard defines a list of reserved keywords. Avoid using ISO reserved keywords for object names and identifiers. The ODBC reserved keyword list, shown in the following table, is the same as the ISO reserved keyword list.  
  
> [!NOTE]  
>  The ISO standards reserved keywords list sometimes can be more restrictive than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and at other times less restrictive. For example, the ISO reserved keywords list contains **INT**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have to distinguish this as a reserved keyword.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] reserved keywords can be used as identifiers or names of databases or database objects, such as tables, columns, views, and so on. Use either quoted identifiers or delimited identifiers. Using reserved keywords as the names of variables and stored procedure parameters is not restricted.  
  
## ODBC Reserved Keywords  
 The following words are reserved for use in ODBC function calls. These words do not constrain the minimum SQL grammar; however, to ensure compatibility with drivers that support the core SQL grammar, applications should avoid using these keywords.  
  
 This is the current list of ODBC reserved keywords.  
  
||||  
|-|-|-|  
|**ABSOLUTE**|**EXEC**|**OVERLAPS**|  
|**ACTION**|**EXECUTE**|**PAD**|  
|**ADA**|**EXISTS**|**PARTIAL**|  
|**ADD**|**EXTERNAL**|**PASCAL**|  
|**ALL**|**EXTRACT**|**POSITION**|  
|**ALLOCATE**|**FALSE**|**PRECISION**|  
|**ALTER**|**FETCH**|**PREPARE**|  
|**AND**|**FIRST**|**PRESERVE**|  
|**ANY**|**FLOAT**|**PRIMARY**|  
|**ARE**|**FOR**|**PRIOR**|  
|**AS**|**FOREIGN**|**PRIVILEGES**|  
|**ASC**|**FORTRAN**|**PROCEDURE**|  
|**ASSERTION**|**FOUND**|**PUBLIC**|  
|**AT**|**FROM**|**READ**|  
|**AUTHORIZATION**|**FULL**|**REAL**|  
|**AVG**|**GET**|**REFERENCES**|  
|**BEGIN**|**GLOBAL**|**RELATIVE**|  
|**BETWEEN**|**GO**|**RESTRICT**|  
|**BIT**|**GOTO**|**REVOKE**|  
|**BIT_LENGTH**|**GRANT**|**RIGHT**|  
|**BOTH**|**GROUP**|**ROLLBACK**|  
|**BY**|**HAVING**|**ROWS**|  
|**CASCADE**|**HOUR**|**SCHEMA**|  
|**CASCADED**|**IDENTITY**|**SCROLL**|  
|**CASE**|**IMMEDIATE**|**SECOND**|  
|**CAST**|**IN**|**SECTION**|  
|**CATALOG**|**INCLUDE**|**SELECT**|  
|**CHAR**|**INDEX**|**SESSION**|  
|**CHAR_LENGTH**|**INDICATOR**|**SESSION_USER**|  
|**CHARACTER**|**INITIALLY**|**SET**|  
|**CHARACTER_LENGTH**|**INNER**|**SIZE**|  
|**CHECK**|**INPUT**|**SMALLINT**|  
|**CLOSE**|**INSENSITIVE**|**SOME**|  
|**COALESCE**|**INSERT**|**SPACE**|  
|**COLLATE**|**INT**|**SQL**|  
|**COLLATION**|**INTEGER**|**SQLCA**|  
|**COLUMN**|**INTERSECT**|**SQLCODE**|  
|**COMMIT**|**INTERVAL**|**SQLERROR**|  
|**CONNECT**|**INTO**|**SQLSTATE**|  
|**CONNECTION**|**IS**|**SQLWARNING**|  
|**CONSTRAINT**|**ISOLATION**|**SUBSTRING**|  
|**CONSTRAINTS**|**JOIN**|**SUM**|  
|**CONTINUE**|**KEY**|**SYSTEM_USER**|  
|**CONVERT**|**LANGUAGE**|**TABLE**|  
|**CORRESPONDING**|**LAST**|**TEMPORARY**|  
|**COUNT**|**LEADING**|**THEN**|  
|**CREATE**|**LEFT**|**TIME**|  
|**CROSS**|**LEVEL**|**TIMESTAMP**|  
|**CURRENT**|**LIKE**|**TIMEZONE_HOUR**|  
|**CURRENT_DATE**|**LOCAL**|**TIMEZONE_MINUTE**|  
|**CURRENT_TIME**|**LOWER**|**TO**|  
|**CURRENT_TIMESTAMP**|**MATCH**|**TRAILING**|  
|**CURRENT_USER**|**MAX**|**TRANSACTION**|  
|**CURSOR**|**MIN**|**TRANSLATE**|  
|**DATE**|**MINUTE**|**TRANSLATION**|  
|**DAY**|**MODULE**|**TRIM**|  
|**DEALLOCATE**|**MONTH**|**TRUE**|  
|**DEC**|**NAMES**|**UNION**|  
|**DECIMAL**|**NATIONAL**|**UNIQUE**|  
|**DECLARE**|**NATURAL**|**UNKNOWN**|  
|**DEFAULT**|**NCHAR**|**UPDATE**|  
|**DEFERRABLE**|**NEXT**|**UPPER**|  
|**DEFERRED**|**NO**|**USAGE**|  
|**DELETE**|**NONE**|**USER**|  
|**DESC**|**NOT**|**USING**|  
|**DESCRIBE**|**NULL**|**VALUE**|  
|**DESCRIPTOR**|**NULLIF**|**VALUES**|  
|**DIAGNOSTICS**|**NUMERIC**|**VARCHAR**|  
|**DISCONNECT**|**OCTET_LENGTH**|**VARYING**|  
|**DISTINCT**|**OF**|**VIEW**|  
|**DOMAIN**|**ON**|**WHEN**|  
|**DOUBLE**|**ONLY**|**WHENEVER**|  
|**DROP**|**OPEN**|**WHERE**|  
|**ELSE**|**OPTION**|**WITH**|  
|**END**|**OR**|**WORK**|  
|**END-EXEC**|**ORDER**|**WRITE**|  
|**ESCAPE**|**OUTER**|**YEAR**|  
|**EXCEPT**|**OUTPUT**|**ZONE**|  
|**EXCEPTION**|||  
  
## Future Keywords  
 The following keywords could be reserved in future releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as new features are implemented. Consider avoiding the use of these words as identifiers.  
  
||||  
|-|-|-|  
|ABSOLUTE|HOST|RELATIVE|  
|ACTION|HOUR|RELEASE|  
|ADMIN|IGNORE|RESULT|  
|AFTER|IMMEDIATE|RETURNS|  
|AGGREGATE|INDICATOR|ROLE|  
|ALIAS|INITIALIZE|ROLLUP|  
|ALLOCATE|INITIALLY|ROUTINE|  
|ARE|INOUT|ROW|  
|ARRAY|INPUT|ROWS|  
|ASENSITIVE|INT|SAVEPOINT|  
|ASSERTION|INTEGER|SCROLL|  
|ASYMMETRIC|INTERSECTION|SCOPE|  
|AT|INTERVAL|SEARCH|  
|ATOMIC|ISOLATION|SECOND|  
|BEFORE|ITERATE|SECTION|  
|BINARY|LANGUAGE|SENSITIVE|  
|BIT|LARGE|SEQUENCE|  
|BLOB|LAST|SESSION|  
|BOOLEAN|LATERAL|SETS|  
|BOTH|LEADING|SIMILAR|  
|BREADTH|LESS|SIZE|  
|CALL|LEVEL|SMALLINT|  
|CALLED|LIKE_REGEX|SPACE|  
|CARDINALITY|LIMIT|SPECIFIC|  
|CASCADED|LN|SPECIFICTYPE|  
|CAST|LOCAL|SQL|  
|CATALOG|LOCALTIME|SQLEXCEPTION|  
|CHAR|LOCALTIMESTAMP|SQLSTATE|  
|CHARACTER|LOCATOR|SQLWARNING|  
|CLASS|MAP|START|  
|CLOB|MATCH|STATE|  
|COLLATION|MEMBER|STATEMENT|  
|COLLECT|METHOD|STATIC|  
|COMPLETION|MINUTE|STDDEV_POP|  
|CONDITION|MOD|STDDEV_SAMP|  
|CONNECT|MODIFIES|STRUCTURE|  
|CONNECTION|MODIFY|SUBMULTISET|  
|CONSTRAINTS|MODULE|SUBSTRING_REGEX|  
|CONSTRUCTOR|MONTH|SYMMETRIC|  
|CORR|MULTISET|SYSTEM|  
|CORRESPONDING|NAMES|TEMPORARY|  
|COVAR_POP|NATURAL|TERMINATE|  
|COVAR_SAMP|NCHAR|THAN|  
|CUBE|NCLOB|TIME|  
|CUME_DIST|NEW|TIMESTAMP|  
|CURRENT_CATALOG|NEXT|TIMEZONE_HOUR|  
|CURRENT_DEFAULT_TRANSFORM_GROUP|NO|TIMEZONE_MINUTE|  
|CURRENT_PATH|NONE|TRAILING|  
|CURRENT_ROLE|NORMALIZE|TRANSLATE_REGEX|  
|CURRENT_SCHEMA|NUMERIC|TRANSLATION|  
|CURRENT_TRANSFORM_GROUP_FOR_TYPE|OBJECT|TREAT|  
|CYCLE|OCCURRENCES_REGEX|TRUE|  
|DATA|OLD|UESCAPE|  
|DATE|ONLY|UNDER|  
|DAY|OPERATION|UNKNOWN|  
|DEC|ORDINALITY|UNNEST|  
|DECIMAL|OUT|USAGE|  
|DEFERRABLE|OVERLAY|USING|  
|DEFERRED|OUTPUT|VALUE|  
|DEPTH|PAD|VAR_POP|  
|DEREF|PARAMETER|VAR_SAMP|  
|DESCRIBE|PARAMETERS|VARCHAR|  
|DESCRIPTOR|PARTIAL|VARIABLE|  
|DESTROY|PARTITION|WHENEVER|  
|DESTRUCTOR|PATH|WIDTH_BUCKET|  
|DETERMINISTIC|POSTFIX|WITHOUT|  
|DICTIONARY|PREFIX|WINDOW|  
|DIAGNOSTICS|PREORDER|WITHIN|  
|DISCONNECT|PREPARE|WORK|  
|DOMAIN|PERCENT_RANK|WRITE|  
|DYNAMIC|PERCENTILE_CONT|XMLAGG|  
|EACH|PERCENTILE_DISC|XMLATTRIBUTES|  
|ELEMENT|POSITION_REGEX|XMLBINARY|  
|END-EXEC|PRESERVE|XMLCAST|  
|EQUALS|PRIOR|XMLCOMMENT|  
|EVERY|PRIVILEGES|XMLCONCAT|  
|EXCEPTION|RANGE|XMLDOCUMENT|  
|FALSE|READS|XMLELEMENT|  
|FILTER|REAL|XMLEXISTS|  
|FIRST|RECURSIVE|XMLFOREST|  
|FLOAT|REF|XMLITERATE|  
|FOUND|REFERENCING|XMLNAMESPACES|  
|FREE|REGR_AVGX|XMLPARSE|  
|FULLTEXTTABLE|REGR_AVGY|XMLPI|  
|FUSION|REGR_COUNT|XMLQUERY|  
|GENERAL|REGR_INTERCEPT|XMLSERIALIZE|  
|GET|REGR_R2|XMLTABLE|  
|GLOBAL|REGR_SLOPE|XMLTEXT|  
|GO|REGR_SXX|XMLVALIDATE|  
|GROUPING|REGR_SXY|YEAR|  
|HOLD|REGR_SYY|ZONE|  
  
## See Also  
 [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md)   
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)  
  
  
