---
title: "Running Prepared Statements (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6249a65e-2fa7-4cfc-866f-96ac4c19a653
caps.latest.revision: 23
author: BarbKess
---
# Running Prepared Statements (SQL Server PDW)
This topic describes how to use prepared statements to submit queries to SQL Server PDW. Prepared statements, also called parameterized statements, are SQL statements that include one or more parameters in place of embedded values. Prepared statements are used by business intelligence tools and other applications that run the same query using different values in place of the parameters each time the query is submitted.  
  
For more information about data access, see the [Data Developer Center](http://go.microsoft.com/fwlink/?LinkId=62396) on MSDN.  
  
> [!WARNING]  
> **SET FMTONLY** can only be used with SQL Server Native Client. The use of **SET FMTONLY** to return metadata will be removed in the next version of SQL Server PDW. Do not use this feature in new development work. Modify applications that currently use this feature as soon as possible. Use the driver APIs to retrieve metadata instead.  
  
## Contents  
  
-   [Support for Named Query Parameters](#Named)  
  
-   [Support for Unnamed Query Parameters (?)](#Unnamed)  
  
-   [Limitations and Restrictions](#Limitations)  
  
-   [Examples](#Examples)  
  
## <a name="Named"></a>Support for Named Query Parameters  
Named parameters are supported only for connections that use SQL Server Native Client. Named parameters cannot be used with connections that use Client Tools for SQL Server PDW.  
  
## <a name="Unnamed"></a>Support for Unnamed Query Parameters (?)  
SQL Server PDW supports unnamed parameters for SQL Server Native Client connections, according to the supported scenarios listed in the following table.  
  
All unnamed parameters use the ‘?’ syntax.  
  
Additionally, any of the supported SELECT use cases can be used in the select portion of a CREATE TABLE AS SELECT, the CREATE REMOTE TABLE statement, or as a subquery of another command type that supports prepared statements.  
  
|Statement|Supported Locations|Supported Syntaxes|  
|-------------|-----------------------|----------------------|  
|SELECT|JOIN, WHERE, IN, LIKE, and in subqueries|SELECT \* FROM tbl1 JOIN tbl2 ON tbl1.col1 = tbl2.col1 AND tbl1.col2 = **?**;<br /><br />SELECT \* FROM tbl1 WHERE col1= **?** AND col2 <= **?**;<br /><br />SELECT col2 FROM tbl1 WHERE col1 IN (**?**);<br /><br />SELECT col2 FROM tbl1 WHERE col1 LIKE (**?**);<br /><br />SELECT \* FROM tbl1 WHERE col2 = (SELECT min(col1) FROM tbl2 WHERE col2= **?**);|  
|INSERT|VALUES and in subqueries|INSERT INTO tbl1 VALUES (**?**);<br /><br />INSERT INTO tbl1 VALUES (**?**,**?**);<br /><br />INSERT INTO tbl1 VALUES (SELECT col1 FROM tbl1 WHERE col1 = **?**));|  
|UPDATE|SET, WHERE, IN, LIKE, and in subqueries|UPDATE tbl1 SET col1 = **?**, col2 = **?** WHERE col2 > **?**;<br /><br />UPDATE tbl1 SET col1 = 0 WHERE col1 IN (**?**);<br /><br />UPDATE tbl1 SET col2 = 2 WHERE col1 LIKE **?**;<br /><br />UPDATE tbl1 SET col1 = **?**, col2 = (SELECT min(col1) FROM tbl2 WHERE col2= **?**);|  
|DELETE|WHERE, IN, LIKE, and in subqueries|DELETE FROM tbl1 WHERE col1 = **?**;<br /><br />DELETE FROM tbl1 WHERE col2 IN (**?**);<br /><br />DELETE FROM tbl1 WHERE col1 LIKE **?**;<br /><br />DELETE FROM tbl1 WHERE col1 = (SELECT count(\*) FROM tbl2 WHERE col1 = **?**);|  
|DECLARE|Assignment|DECLARE @p1 int = **?**;<br /><br />DECLARE @p1 int =  (SELECT col1 FROM tbl1 WHERE col1 =**?**);|  
|SET|Assignment and in subqueries|SET @p1 = **?**;<br /><br />SET @p1 = 'SELECT \* FROM tbl1 WHERE col2 = (SELECT min(col1) FROM tbl2 WHERE col2= **?**)';|  
  
## <a name="Limitations"></a>Limitations and Restrictions  
Money and smallmoney data types are not natively supported in the ADO.NET, OLE DB, and ODBC protocol for SQL Server PDW. To pass a currency value to a parameter in a prepared statement, you can represent money or smallmoney as a parameter of type decimal.  
  
If a prepared statement inserts a value of type decimal into an integer data type, the operation will succeed and the portion of the data to the right of the decimal point will be truncated. For example, when a prepared statement inserts $1234.5678 into a SQL Server PDW smallint, the resulting smallint is 1234.  
  
Following is a list of specific limitations to SQL Server PDW prepared statement support. This list is not exhaustive.  
  
|Restriction|Invalid Syntax Examples|  
|---------------|---------------------------|  
|SQL Server PDW does not support the reflexive property in the context of parameterization. Parameterized statements always require the column information on the left side of an expression.|SELECT col1 FROM tbl1 WHERE ? = col1;|  
|Parameters cannot be specified in the select list of a SELECT statement.|SELECT col1, ? FROM tbl1 WHERE col1 = 4;|  
|Parameters cannot be specified in the FROM portion of a SELECT statement.|SELECT col1 FROM ? WHERE col1 = 4;|  
|When providing parameters for an IN clause, no more than two values can be used and all values must be parameters.|UPDATE tbl1 SET col1 = 2 WHERE col1 in (?,?,?);<br /><br />SELECT col1, col2 FROM tbl1 WHERE col1 in (?,10);|  
|The use of parameters is not supported with the BETWEEN clause.|DELETE FROM tbl1 WHERE col1 between 1 AND ?;|  
|Parameters cannot be used as part of expressions, including as part of binary operands or computed columns.|SELECT col1 FROM tbl1 WHERE col1 = (1+ ?);<br /><br />SET @p1 = (?+5);|  
  
## <a name="Examples"></a>Code Examples  
The following table provides sample code for submitting prepared statements using each of the supported connection API’s.  
  
|Connection API|Example Code|  
|------------------|----------------|  
|ADO.NET|DbProviderFactory factory = DbProviderFactories.GetFactory(“Microsoft.SqlServer.DataWarehouse”);<br /><br />DbConnection connection = factor.CreateConnection();<br /><br />DbCommand command = connection.CreateCommand(“SELECT * FROM bing WHERE id=?”);<br /><br />DbParameter param1 = new DbParameter();<br /><br />Param1.Value = 1;<br /><br />Command.Parameters.Add(param1);<br /><br />Command.ExecuteQuery();<br /><br />Command.Close();|  
|ODBC|OdbcCommand command = OdbcConnection.CreateCommand(“SELECT * FROM bing WHERE id=?”);<br /><br />OdbcParameter param1 = new OdbcParameter();<br /><br />param1.value = 1;<br /><br />command.Parameters.Add(param1);<br /><br />Command.Prepare();<br /><br />Command.Execute();<br /><br />Command.Close();|  
|OLEDB|OleDbCommand command = OleDbConnection.CreateCommand(“SELECT * FROM bing WHERE id=?”);<br /><br />OleDbParameter param1 = new OleDbParameter();<br /><br />param1.value = 1;<br /><br />command.Parameters.Add(param1);<br /><br />Command.Prepare();<br /><br />Command.Ecxecute();<br /><br />Command.Close();|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
