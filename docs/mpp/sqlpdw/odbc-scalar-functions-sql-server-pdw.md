---
title: "ODBC Scalar Functions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f766b2c3-2725-4a6f-aef0-8c9e97cf4acd
caps.latest.revision: 5
author: BarbKess
---
# ODBC Scalar Functions (SQL Server PDW)
You can use [ODBC Scalar Functions](http://go.microsoft.com/fwlink/?LinkID=88579) in Transact\-SQL statements. These statements are interpreted by SQL Server. They can be used in stored procedures and user-defined functions. These include string, numeric, time, date, interval, and system functions.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Usage  
`SELECT {fn <function_name> [ (<argument>,....n) ] }`  
  
## Functions  
The following tables list ODBC scalar functions that are not duplicated in Transact\-SQL.  
  
### String Functions  
  
|Function|Description|  
|------------|---------------|  
|BIT_LENGTH( string_exp ) (ODBC 3.0)|Returns the length in bits of the string expression.<br /><br />Does not work only for string data types. Therefore, will not implicitly convert string_exp to string but instead will return the (internal) size of whatever data type it is given.|  
|CONCAT( string_exp1,string_exp2) (ODBC 1.0)|Returns a character string that is the result of concatenating string_exp2 to string_exp1. The resulting string is DBMS-dependent. For example, if the column represented by string_exp1 contained a NULL value, DB2 would return NULL but SQL Server would return the non-NULL string.|  
  
### Time, Date, and Interval Functions  
  
|Function|Description|  
|------------|---------------|  
|CURRENT_DATE( ) (ODBC 3.0)|Returns the current date.|  
|CURDATE( ) (ODBC 3.0)|Returns the current date.|  
|CURRENT_TIME() (ODBC 3.0)|Returns the current local time. APS does not support the time-precision argument which determines the seconds precision of the returned value. In APS, CURRENT_TIME takes 0 arguments.|  
|CURTIME() (ODBC 3.0)|Returns the current local time.|  
|DAYNAME( date_exp ) (ODBC 2.0)|Returns a character string that contains the data source–specific name of the day (for example, Sunday through Saturday or Sun. through Sat. for a data source that uses English, or Sonntag through Samstag for a data source that uses German) for the day part of date_exp.|  
|DAYOFMONTH( date_exp ) (ODBC 1.0)|Returns the day of the month based on the month field in date_exp as an integer value in the range of 1–31.|  
|DAYOFWEEK( date_exp ) (ODBC 1.0)|Returns the day of the week based on the week field in date_exp as an integer value in the range of 1–7, where 1 represents Sunday.|  
|HOUR( time_exp ) (ODBC 1.0)|Returns the hour based on the hour field in time_exp as an integer value in the range of 0–23.|  
|MINUTE( time_exp ) (ODBC 1.0)|Returns the minute based on the minute field in time_exp as an integer value in the range of 0–59.|  
|SECOND( time_exp ) (ODBC 1.0)|Returns the second based on the second field in time_exp as an integer value in the range of 0–59.|  
|MONTHNAME( date_exp ) (ODBC 2.0)|Returns a character string that contains the data source–specific name of the month (for example, January through December or Jan. through Dec. for a data source that uses English, or Januar through Dezember for a data source that uses German) for the month part of date_exp.|  
|QUARTER( date_exp ) (ODBC 1.0)|Returns the quarter in date_exp as an integer value in the range of 1–4, where 1 represents January 1 through March 31.|  
|WEEK( date_exp ) (ODBC 1.0)|Returns the week of the year based on the week field in date_exp as an integer value in the range of 1–53.|  
  
## Examples  
  
### A. Using an ODBC function in a stored procedure  
The following example uses an ODBC function in a stored procedure:  
  
```  
CREATE PROCEDURE dbo.ODBCprocedure  
    (  
    @string_exp nvarchar(4000)  
    )  
AS  
SELECT {fn BIT_LENGTH( @string_exp )};  
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
SET @len = (SELECT {fn BIT_LENGTH( @string_exp )})  
RETURN(@len)  
END ;  
  
SELECT dbo.ODBCudf('Returns the length in bits.');  
--Returns 432  
```  
  
### C. Using an ODBC functions in SELECT statements  
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
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
