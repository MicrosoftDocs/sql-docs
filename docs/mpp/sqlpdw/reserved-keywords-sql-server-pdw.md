---
title: "Reserved Keywords (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 489dab7e-b401-421c-a36b-dca133b583b0
caps.latest.revision: 19
author: BarbKess
---
# Reserved Keywords (SQL Server PDW)
Microsoft SQL Server PDW uses reserved keywords for defining, manipulating, and accessing databases. Reserved keywords are part of the grammar of the SQL Server PDW language that is used by SQL Server PDW to parse and understand SQL Server PDW statements and batches. Although not enumerated below, all SQL Server Transact\-SQL reserved keywords are also reserved in SQL Server PDW. (See [Reserved Keywords (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189822(sql.105).aspx) on MSDN for a list of the Transact\-SQL reserved keywords.)  
  
The following table lists SQL Server PDW reserved keywords.  
  
||||  
|-|-|-|  
|ABS|COPY|NTILE|  
|ACOS|COS|NULLIFZERO|  
|ADD_MONTHS|DATE_TRUNC|NVL|  
|ADMIN|DATE_PART|PAGE|  
|AFTER|DATEPART|PAGE_SIZE|  
|AGE|DECODE|PARTITION|  
|AGGREGATE|DENSE_RANK|PARTITIONED|  
|ALIAS|DISKUSAGE|PARTITIONING|  
|ARRAY|DISTRIBUTE|PARTITIONS|  
|ASIN|DISTRIBUTE_ON|PARTITION_ON|  
|ASSIGNMENT|DISTRIBUTED_SIZE|PATINDEX|  
|ASYMMETRIC|DISTRIBUTION_ON|PASSWORD|  
|ATAN|EACH|QUERY|  
|ATTRIBUTES|ENCRYPTION|RANGE|  
|AUTOGROW|EXP|RANK|  
|AUTOMATIC|EXPLAIN|REPLACE|  
|AVG|EXPORT|REPLICATE|  
|BEFORE|EXTEND|REPLICATED_SIZE|  
|BINAY|FAST_INSERT|ROLE|  
|BITVAR|FLOAT|ROUND|  
|BOOLEAN|FLOAT8|ROW_ALLOCATION|  
|BYTE|FLOOR|ROW_COUNT|  
|CATALOG_NAME|FORMAT|ROW_NUMBER|  
|CEIL|GREATER|RPAD|  
|CEILING|GREATEST|RTRIM|  
|CHAIN|HISTORY|SAMPLE|  
|CHAR_LENGTH|IFNULL|SAVEPOINT|  
|CHARACTER_LENGTH|IIPARTITION|SESSION_ID|  
|CHARACTER_SET_CATALOG|INT1|SHOW|  
|CHARACTER_SET_NAME|INT2|SIGN|  
|CHARACTER_SET_SCHEMA|INT4|SIN|  
|CHARINDEX|INT8|SMALLER|  
|CHECKED|ISNUMERIC|SQRT|  
|CLASS_ORIGIN|ISO-WEEK|STATISTICS|  
|CLUSTER_ON|LABEL|STDDEV_POP|  
|COBOL|LAG|STDDEV_SAMP|  
|COLLATION_CATALOG|LEAD|STDEVP|  
|COLLATION_NAME|LEAST|STDEV|  
|COLLATION_SCHEMA|LEN|SUBPARTITION|  
|COLUMN_NAME|LENGTH|SYSDATETIME|  
|COMMAND_FUNCTION|LIMIT|TAN|  
|COMMAND_FUNCTION_NAME|LOCATE|TEXT|  
|COMMITTED|LOCK|THAN|  
|COMPLETE|LOG_SIZE|VARBINARY|  
|COMPRESSED|LOG|VAR|  
|CONDITION_NUMBER|LOG10|VARP|  
|CONNECTION_NAME|LPAD|VERSION|  
|CONSTRAINT_CATALOG|LTRIM|VIEWS|  
|CONSTRAINT_NAME|MOD|ZEROIFNULL|  
  
Additionally, the ISO standard defines a list of reserved keywords. Avoid using ISO reserved keywords for object names and identifiers.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
