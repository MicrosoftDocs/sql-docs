---
title: "Behavior Changes to Database Engine Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "behavior changes [SQL Server]"
  - "Database Engine [SQL Server], what's new"
  - "Transact-SQL behavior changes"
ms.assetid: 65eaafa1-9e06-4264-b547-cbee8013c995
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Behavior Changes to Database Engine Features in SQL Server 2014
  This topic describes behavior changes in the [!INCLUDE[ssDE](../includes/ssde-md.md)]. Behavior changes affect how features work or interact in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] as compared to earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## Behavior Changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], queries against an XML document that contains strings over a certain length (more than 4020 characters) can return incorrect results. In [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], such queries return the correct results.  
  
## Behavior Changes in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
  
### Metadata Discovery  
 Improvements in the [!INCLUDE[ssDE](../includes/ssde-md.md)] beginning with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] allow SQLDescribeCol to obtain more accurate descriptions of the expected results than those returned by SQLDescribeCol in previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../relational-databases/native-client/features/metadata-discovery.md).  
  
 The [SET FMTONLY](/sql/t-sql/statements/set-fmtonly-transact-sql) option for determining the format of a response without actually running the query is replaced with [sp_describe_first_result_set &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql), [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql), [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql), and [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql).  
  
### Changes to Behavior in Scripting a SQL Server Agent Task  
 In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], if you create a new job by copying the script from an existing job, the new job might inadvertently affect the existing job. To create a new job using the script from an existing job, manually delete the parameter *@schedule_uid* which is usually the last parameter of the section which creates the job schedule in the existing job. This will create a new independent schedule for the new job without affecting existing jobs.  
  
### Constant Folding for CLR User-Defined Functions and Methods  
 In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the following user-defined CLR objects are now foldable:  
  
-   Deterministic scalar-valued CLR user-defined functions.  
  
-   Deterministic methods of CLR user-defined types.  
  
 This improvement seeks to enhance performance when these functions or methods are called more than once with the same arguments. However, this change may cause unexpected results when non-deterministic functions or methods have been marked as deterministic in error. The determinism of a CLR function or method is indicated by the value of the `IsDeterministic` property of the `SqlFunctionAttribute` or `SqlMethodAttribute`.  
  
### Behavior of STEnvelope() Method Has Changed with Empty Spatial Types  
 The behavior of the `STEnvelope` method with empty objects is now consistent with the behavior of other [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] spatial methods.  
  
 In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], the `STEnvelope` method returned the following results when called with empty objects:  
  
```  
select geometry::Parse('POINT EMPTY').STEnvelope().ToString()  
-- returns POINT EMPTY  
select geometry::Parse('LINESTRING EMPTY').STEnvelope().ToString()  
-- returns LINESTRING EMPTY  
select geometry::Parse('POLYGON EMPTY').STEnvelope().ToString()  
-- returns POLYGON EMPTY  
```  
  
 In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the `STEnvelope` method now returns the following results when called with empty objects:  
  
```  
select geometry::Parse('POINT EMPTY').STEnvelope().ToString()  
-- returns GEOMETRYCOLLECTION EMPTY  
select geometry::Parse('LINESTRING EMPTY').STEnvelope().ToString()  
-- returns GEOMETRYCOLLECTION EMPTY  
select geometry::Parse('POLYGON EMPTY').STEnvelope().ToString()  
-- returns GEOMETRYCOLLECTION EMPTY  
```  
  
 To determine whether a spatial object is empty, call the [STIsEmpty &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stisempty-geometry-data-type) method.  
  
### LOG Function Has New Optional Parameter  
 The `LOG` function now has an optional *base* parameter. For more information, see [LOG &#40;Transact-SQL&#41;](/sql/t-sql/functions/log-transact-sql).  
  
### Statistics Computation during Partitioned Index Operations Has Changed  
 In [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], statistics are not created by scanning all rows in the table when a partitioned index is created or rebuilt. Instead, the query optimizer uses the default sampling algorithm to generate statistics. After upgrading a database with partitioned indexes, you may notice a difference in the histogram data for these indexes. This change in behavior may not affect query performance. To obtain statistics on partitioned indexes by scanning all the rows in the table, use CREATE STATISTICS or UPDATE STATISTICS with the FULLSCAN clause.  
  
### Data Type Conversion by the XML value Method Has Changed  
 The internal behavior of the `value` method of the `xml` data type has changed. This method performs an XQuery against the XML and returns a scalar value of the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data type. The xs type has to be converted to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data type. Previously, the `value` method internally converted the source value to an xs:string, then converted the xs:string to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data type. In [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], the conversion to xs:string is skipped in the following cases:  
  
|Source XS data type|Destination SQL Server data type|  
|-------------------------|--------------------------------------|  
|byte<br /><br /> short<br /><br /> int<br /><br /> integer<br /><br /> long<br /><br /> unsignedByte<br /><br /> unsignedShort<br /><br /> unsignedInt<br /><br /> unsignedLong<br /><br /> positiveInteger<br /><br /> nonPositiveInteger<br /><br /> negativeInteger<br /><br /> nonNegativeInteger|tinyint<br /><br /> smallint<br /><br /> int<br /><br /> bigint<br /><br /> decimal<br /><br /> numeric|  
|decimal|decimal<br /><br /> numeric|  
|float|real|  
|double|float|  
  
 The new behavior improves performance when the intermediate conversion can be skipped. However, when data type conversions fail, you see different error messages than those that were raised when converting from the intermediate xs:string value. For example, if the value method failed to convert the `int` value 100000 to a `smallint`, the previous error message was:  
  
 `The conversion of the nvarchar value '100000' overflowed an INT2 column. Use a larger integer column.`  
  
 In [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], without the intermediate conversion to xs:string, the error message is:  
  
 `Arithmetic overflow error converting expression to data type smallint.`  
  
### sqlcmd.exe Behavior Change in XML Mode  
 There are behavior changes if you use sqlcmd.exe with XML mode (:XML ON command) when executing a SELECT * from T FOR XML ....  
  
### DBCC CHECKIDENT Revised Message  
 In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the message returned by the DBCC CHECKIDENT command has changed only when it is used with RESEED *new_reseed_value*  to change current identity value. The new message is "Checking identity information: current identity value '\<current identity value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator."  
  
 In earlier versions, the message is "Checking identity information: current identity value '\<current identity value>', current column value '\<current column value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator." The message is unchanged when DBCC CHECKIDENT is specified with NORESEED, without a second parameter, or without a reseed value. For more information, see [DBCC CHECKIDENT &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkident-transact-sql).  
  
### Behavior of exist() function on XML datatype has changed  
 The behavior of the **exist()** function has changed when comparing an XML data type with a null value to 0 (zero). Consider the following example:  
  
```xml  
DECLARE @test XML;  
SET @test = null;  
SELECT COUNT(1) WHERE @test.exist('/dogs') = 0;  
```  
  
 In earlier versions, this comparison return 1 (true); now, this comparison returns 0 (zero, false).  
  
 The following comparisons have not changed:  
  
```xml  
DECLARE @test XML;  
SET @test = null;  
SELECT COUNT(1) WHERE @test.exist('/dogs') = 1; -- 0 expected, 0 returned  
SELECT COUNT(1) WHERE @test.exist('/dogs') IS NULL; -- 1 expected, 1 returned  
```  
  
## See Also  
 [Breaking Changes to Database Engine Features in SQL Server 2014](breaking-changes-to-database-engine-features-in-sql-server-2016.md)   
 [Deprecated Database Engine Features in SQL Server 2014](deprecated-database-engine-features-in-sql-server-2016.md)   
 [Discontinued Database Engine Functionality in SQL Server 2014](discontinued-database-engine-functionality-in-sql-server-2016.md)   
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level)  
  
  
