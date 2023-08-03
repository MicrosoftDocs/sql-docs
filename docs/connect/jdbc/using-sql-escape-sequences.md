---
title: "Using SQL escape sequences in JDBC"
description: "The Microsoft JDBC Driver for SQL Server supports the use of SQL escape sequences, as defined by the JDBC API."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using SQL escape sequences

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the use of SQL escape sequences, as defined by the JDBC API. Escape sequences are used within an SQL statement to tell the driver that the escaped part of the SQL string should be handled differently. When the JDBC driver processes the escaped part of an SQL string, it translates that part of the string into SQL code that SQL Server understands.  
  
 There are five types of escape sequences that the JDBC API requires, and all are supported by the JDBC driver:  
  
- LIKE wildcard literals
- Function handling
- Date and time literals
- Stored procedure calls
- Outer joins
- Limit escape syntax

The escape sequence syntax used by the JDBC driver is the following:  
  
`{keyword ...parameters...}`  
  
> [!NOTE]  
> SQL escape processing is always turned on for the JDBC driver.  
  
The following sections describe the five types of escape sequences and how they are supported by the JDBC driver.  
  
## LIKE wildcard literals

The JDBC driver supports the `{escape 'escape character'}` syntax for using LIKE clause wildcards as literals. For example, the following code will return values for col3, where the value of col2 literally begins with an underscore (and not its wildcard usage).  

```java
ResultSet rst = stmt.executeQuery("SELECT col3 FROM test1 WHERE col2   
LIKE '\\_%' {escape '\\'}");  
```

> [!NOTE]  
> The escape sequence must be at the end of the SQL statement. For multiple SQL statements in a command string, the escape sequence needs to be at the end of each relevant SQL statement.  

## Function handling

The JDBC driver supports function escape sequences in SQL statements with the following syntax:  

```sql
{fn functionName}  
```

where `functionName` is a function supported by the JDBC driver. For example: 

```sql
SELECT {fn UCASE(Name)} FROM Employee  
```

The following table lists the various functions that are supported by the JDBC driver when using a function escape sequence:  
  
| String Functions                                                                                                                                                                                                                                                                                                                        | Numeric Functions                                                                                                                                                                                                                                                                                                                                                                                                   | Datetime Functions                                                                                                                                                                                                                                                                                                                                             | System Functions                             |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------- |
| ASCII<br /><br /> CHAR<br /><br /> CONCAT<br /><br /> DIFFERENCE<br /><br /> INSERT<br /><br /> LCASE<br /><br /> LEFT<br /><br /> LENGTH<br /><br /> LOCATE<br /><br /> LTRIM<br /><br /> REPEAT<br /><br /> REPLACE<br /><br /> RIGHT<br /><br /> RTRIM<br /><br /> SOUNDEX<br /><br /> SPACE<br /><br /> SUBSTRING<br /><br /> UCASE | ABS<br /><br /> ACOS<br /><br /> ASIN<br /><br /> ATAN<br /><br /> ATAN2<br /><br /> CEILING<br /><br /> COS<br /><br /> COT<br /><br /> DEGREES<br /><br /> EXP<br /><br /> FLOOR<br /><br /> LOG<br /><br /> LOG10<br /><br /> MOD<br /><br /> PI<br /><br /> POWER<br /><br /> RADIANS<br /><br /> RAND<br /><br /> ROUND<br /><br /> SIGN<br /><br /> SIN<br /><br /> SQRT<br /><br /> TAN<br /><br /> TRUNCATE | CURDATE<br /><br /> CURTIME<br /><br /> DAYNAME<br /><br /> DAYOFMONTH<br /><br /> DAYOFWEEK<br /><br /> DAYOFYEAR<br /><br /> EXTRACT<br /><br /> HOUR<br /><br /> MINUTE<br /><br /> MONTH<br /><br /> MONTHNAME<br /><br /> NOW<br /><br /> QUARTER<br /><br /> SECOND<br /><br /> TIMESTAMPADD<br /><br /> TIMESTAMPDIFF<br /><br /> WEEK<br /><br /> YEAR | DATABASE<br /><br /> IFNULL<br /><br /> USER |

> [!NOTE]  
> If you try to use a function that the database does not support, an error will occur.  

## Date and time literals

The escape syntax for date, time, and timestamp literals is the following:  

```sql
{literal-type 'value'}  
```

where `literal-type` is one of the following:  
  
| Literal Type | Description | Value Format               |
| ------------ | ----------- | -------------------------- |
| d            | Date        | yyyy-mm-dd                 |
| t            | Time        | hh:mm:ss [1]               |
| ts           | TimeStamp   | yyyy-mm-dd hh:mm:ss[.f...] |
  
For example:  

```sql
UPDATE Orders SET OpenDate={d '2005-01-31'}
WHERE OrderID=1025  
```

## Stored procedure calls

The JDBC driver supports the `{? = call proc_name(?,...)}` and `{call proc_name(?,...)}` escape syntax for stored procedure calls, depending on whether you need to process a return parameter.  
  
A procedure is an executable object stored in the database. Generally, it is one or more SQL statements that have been precompiled. The escape sequence syntax for calling a stored procedure is the following:  

```sql
{[?=]call procedure-name[([parameter][,[parameter]]...)]}  
```

where `procedure-name` specifies the name of a stored procedure and `parameter` specifies a stored procedure parameter.  
  
For more information about using the `call` escape sequence with stored procedures, see [Using Statements with Stored Procedures](../../connect/jdbc/using-statements-with-stored-procedures.md).  

## Outer joins

The JDBC driver supports the SQL92 left, right, and full outer join syntax. The escape sequence for outer joins is the following:  

```sql
{oj outer-join}  
```

where outer-join is:  

```sql
table-reference {LEFT | RIGHT | FULL} OUTER JOIN
{table-reference | outer-join} ON search-condition  
```

where `table-reference` is a table name and `search-condition` is the join condition you want to use for the tables.  
  
For example:  

```sql
SELECT Customers.CustID, Customers.Name, Orders.OrderID, Orders.Status
   FROM {oj Customers LEFT OUTER JOIN
      Orders ON Customers.CustID=Orders.CustID}
   WHERE Orders.Status='OPEN'  
```

The following outer join escape sequences are supported by the JDBC driver:  

- Left outer joins
- Right outer joins
- Full outer joins
- Nested outer joins

## Limit escape syntax  

> [!NOTE]  
> The LIMIT escape syntax is only supported by Microsoft JDBC Driver 4.2 (or higher) for SQL Server when using JDBC 4.1 or higher.  
  
 The escape syntax for LIMIT is as follows:  

```sql
LIMIT <rows> [OFFSET <row offset>]  
```

The escape syntax has two parts: \<*rows*> is mandatory and specifies the number of rows to return. OFFSET and \<*row offset*> are optional and specify the number of rows to skip before beginning to return rows. The JDBC driver supports only the mandatory part by transforming the query to use TOP instead of LIMIT. SQL Server does not support the LIMIT clause. **The JDBC driver does not support the optional \<row offset> and the driver will throw an exception if it is used**.  
  
## See also

[Using statements with the JDBC driver](../../connect/jdbc/using-statements-with-the-jdbc-driver.md)  
