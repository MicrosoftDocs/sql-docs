---
title: "ODBC Scalar Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CURDATE ODBC function"
  - "DAYOFMONTH ODBC function"
  - "WEEK ODBC function"
  - "functions, ODBC CONCAT"
  - "SECOND ODBC function"
  - "functions, ODBC CURRENT_TIME"
  - "functions, ODBC HOUR"
  - "functions, ODBC QUARTER"
  - "TRUNCATE ODBC function"
  - "functions, ODBC SECOND"
  - "DAYOFWEEK ODBC function"
  - "CURRENT_DATE ODBC function"
  - "DAYNAME ODBC function"
  - "CURTIME ODBC function"
  - "functions, ODBC CURRENT_DATE"
  - "MINUTE ODBC function"
  - "functions, ODBC BIT_LENGTH"
  - "functions, ODBC OCTET_LENGTH"
  - "CONCAT ODBC function"
  - "MONTHNAME ODBC function"
  - "functions, ODBC DAYOFYEAR"
  - "OCTET_LENGTH ODBC function"
  - "functions [SQL Server], ODBC scalar functions"
  - "BIT_LENGTH ODBC function"
  - "functions, ODBC DAYOFMONTH"
  - "CURRENT_TIME ODBC function"
  - "functions, ODBC CURDATE"
  - "functions, ODBC CURTIME"
  - "functions, ODBC TRUNCATE"
  - "functions, ODBC MONTHNAME"
  - "functions, ODBC DAYNAME"
  - "ODBC scalar functions"
  - "functions, ODBC MINUTE"
  - "functions, ODBC DAYOFWEEK"
  - "QUARTER ODBC function"
  - "DAYOFYEAR ODBC function"
  - "functions, ODBC WEEK"
  - "HOUR ODBC function"
ms.assetid: a0df1ac2-6699-4ac0-8f79-f362f23496f1
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ODBC Scalar Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  You can use [ODBC Scalar Functions](https://go.microsoft.com/fwlink/?LinkID=88579) in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. These statements are interpreted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They can be used in stored procedures and user-defined functions. These include string, numeric, time, date, interval, and system functions.  
  
## Usage  
 `SELECT {fn <function_name> [ (<argument>,....n) ] }`  
  
## Functions  
 The following tables list ODBC scalar functions that are not duplicated in [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
### String Functions  
  
|Function|Description|  
|--------------|-----------------|  
|BIT_LENGTH( string_exp ) (ODBC 3.0)|Returns the length in bits of the string expression.<br /><br /> Does not work only for string data types. Therefore, will not implicitly convert string_exp to string but instead will return the (internal) size of whatever data type it is given.|  
|CONCAT( string_exp1,string_exp2) (ODBC 1.0)|Returns a character string that is the result of concatenating string_exp2 to string_exp1. The resulting string is DBMS-dependent. For example, if the column represented by string_exp1 contained a NULL value, DB2 would return NULL but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] would return the non-NULL string.|  
|OCTET_LENGTH( string_exp ) (ODBC 3.0)|Returns the length in bytes of the string expression. The result is the smallest integer not less than the number of bits divided by 8.<br /><br /> Does not work only for string data types. Therefore, will not implicitly convert string_exp to string but instead will return the (internal) size of whatever data type it is given.|  
  
### Numeric Function  
  
|Function|Description|  
|--------------|-----------------|  
|TRUNCATE( numeric_exp, integer_exp) (ODBC 2.0)|Returns numeric_exp truncated to integer_exp positions right of the decimal point. If integer_exp is negative, numeric_exp is truncated to &#124;integer_exp&#124; positions to the left of the decimal point.|  
  
### Time, Date, and Interval Functions  
  
|Function|Description|  
|--------------|-----------------|  
|CURRENT_DATE( ) (ODBC 3.0)|Returns the current date.|  
|CURDATE( ) (ODBC 3.0)|Returns the current date.|  
|CURRENT_TIME`[( time-precision )]` (ODBC 3.0)|Returns the current local time. The time-precision argument determines the seconds precision of the returned value|  
|CURTIME() (ODBC 3.0)|Returns the current local time.|  
|DAYNAME( date_exp ) (ODBC 2.0)|Returns a character string that contains the data source-specific name of the day (for example, Sunday through Saturday or Sun. through Sat. for a data source that uses English, or Sonntag through Samstag for a data source that uses German) for the day part of date_exp.|  
|DAYOFMONTH( date_exp ) (ODBC 1.0)|Returns the day of the month based on the month field in date_exp as an integer value in the range of 1-31.|  
|DAYOFWEEK( date_exp ) (ODBC 1.0)|Returns the day of the week based on the week field in date_exp as an integer value in the range of 1-7, where 1 represents Sunday.|  
|HOUR( time_exp ) (ODBC 1.0)|Returns the hour based on the hour field in time_exp as an integer value in the range of 0-23.|  
|MINUTE( time_exp ) (ODBC 1.0)|Returns the minute based on the minute field in time_exp as an integer value in the range of 0-59.|  
|SECOND( time_exp ) (ODBC 1.0)|Returns the second based on the second field in time_exp as an integer value in the range of 0-59.|  
|MONTHNAME( date_exp ) (ODBC 2.0)|Returns a character string that contains the data source-specific name of the month (for example, January through December or Jan. through Dec. for a data source that uses English, or Januar through Dezember for a data source that uses German) for the month part of date_exp.|  
|QUARTER( date_exp ) (ODBC 1.0)|Returns the quarter in date_exp as an integer value in the range of 1-4, where 1 represents January 1 through March 31.|  
|WEEK( date_exp ) (ODBC 1.0)|Returns the week of the year based on the week field in date_exp as an integer value in the range of 1-53.|  
  
## Examples  
  
### A. Using an ODBC function in a stored procedure  
 The following example uses an ODBC function in a stored procedure:  
  
```  
CREATE PROCEDURE dbo.ODBCprocedure  
    (  
    @string_exp nvarchar(4000)  
    )  
AS  
SELECT {fn OCTET_LENGTH( @string_exp )};  
```  
  
### B. Using an ODBC Function in a user-defined function  
 The following example uses an ODBC function in a user-defined function:  
  
```  
CREATE FUNCTION dbo.ODBCudf  
    (  
    @string_exp nvarchar(4000)  
    )  
RETURNS int  
AS  
BEGIN  
DECLARE @len int  
SET @len = (SELECT {fn OCTET_LENGTH( @string_exp )})  
RETURN(@len)  
END ;  
  
SELECT dbo.ODBCudf('Returns the length.');  
--Returns 38  
  
```  
  
### C. Using an ODBC functions in SELECT statements  
 The following SELECT statements use ODBC functions:  
  
```  
DECLARE @string_exp nvarchar(4000) = 'Returns the length.';  
SELECT {fn BIT_LENGTH( @string_exp )};  
-- Returns 304  
SELECT {fn OCTET_LENGTH( @string_exp )};  
-- Returns 38  
  
SELECT {fn CONCAT( 'CONCAT ','returns a character string')};  
-- Returns CONCAT returns a character string  
SELECT {fn TRUNCATE( 100.123456, 4)};  
-- Returns 100.123400  
SELECT {fn CURRENT_DATE( )};  
-- Returns 2007-04-20  
SELECT {fn CURRENT_TIME(6)};  
-- Returns 10:27:11.973000  
  
DECLARE @date_exp nvarchar(30) = '2007-04-21 01:01:01.1234567';  
SELECT {fn DAYNAME( @date_exp )};  
-- Returns Saturday  
SELECT {fn DAYOFMONTH( @date_exp )};  
-- Returns 21  
SELECT {fn DAYOFWEEK( @date_exp )};  
-- Returns 7  
SELECT {fn HOUR( @date_exp)};  
-- Returns 1   
SELECT {fn MINUTE( @date_exp )};  
-- Returns 1  
SELECT {fn SECOND( @date_exp )};  
-- Returns 1  
SELECT {fn MONTHNAME( @date_exp )};  
-- Returns April  
SELECT {fn QUARTER( @date_exp )};  
-- Returns 2  
SELECT {fn WEEK( @date_exp )};  
-- Returns 16  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D. Using an ODBC function in a stored procedure  
 The following example uses an ODBC function in a stored procedure:  
  
```  
CREATE PROCEDURE dbo.ODBCprocedure  
    (  
    @string_exp nvarchar(4000)  
    )  
AS  
SELECT {fn BIT_LENGTH( @string_exp )};  
```  
  
### E. Using an ODBC Function in a user-defined function  
 The following example uses an ODBC function in a user-defined function:  
  
```  
CREATE FUNCTION dbo.ODBCudf  
    (  
    @string_exp nvarchar(4000)  
    )  
RETURNS int  
AS  
BEGIN  
DECLARE @len int  
SET @len = (SELECT {fn BIT_LENGTH( @string_exp )})  
RETURN(@len)  
END ;  
  
SELECT dbo.ODBCudf('Returns the length in bits.');  
--Returns 432  
  
```  
  
### F. Using an ODBC functions in SELECT statements  
 The following SELECT statements use ODBC functions:  
  
```  
DECLARE @string_exp nvarchar(4000) = 'Returns the length.';  
SELECT {fn BIT_LENGTH( @string_exp )};  
-- Returns 304  
  
SELECT {fn CONCAT( 'CONCAT ','returns a character string')};  
-- Returns CONCAT returns a character string  
SELECT {fn CURRENT_DATE( )};  
-- Returns todays date  
SELECT {fn CURRENT_TIME(6)};  
-- Returns the time  
  
DECLARE @date_exp nvarchar(30) = '2007-04-21 01:01:01.1234567';  
SELECT {fn DAYNAME( @date_exp )};  
-- Returns Saturday  
SELECT {fn DAYOFMONTH( @date_exp )};  
-- Returns 21  
SELECT {fn DAYOFWEEK( @date_exp )};  
-- Returns 7  
SELECT {fn HOUR( @date_exp)};  
-- Returns 1   
SELECT {fn MINUTE( @date_exp )};  
-- Returns 1  
SELECT {fn SECOND( @date_exp )};  
-- Returns 1  
SELECT {fn MONTHNAME( @date_exp )};  
-- Returns April  
SELECT {fn QUARTER( @date_exp )};  
-- Returns 2  
SELECT {fn WEEK( @date_exp )};  
-- Returns 16  
```  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  



