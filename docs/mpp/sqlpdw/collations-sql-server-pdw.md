---
title: "Collations (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 95bb2cd0-f304-432b-bf7b-c6244cd6ae8e
caps.latest.revision: 32
author: BarbKess
---
# Collations (SQL Server PDW)
A collation specifies the rules for how strings of character data are sorted and compared, based on the norms of particular languages and locales. Use collations in SQL Server PDW to internationalize strings, so that sorting and comparing strings fit the language and locale of the database user.  
  
## Contents  
  
-   [Understanding Collations in SQL Server PDW](#UnderstandingCollations)  
  
    -   [Overview](#Overview)  
  
    -   [Key Terms](#KeyTerms)  
  
    -   [Differences between SQL Server PDW and SQL Server Collations](#Differences)  
  
-   [Examples](#Examples)  
  
-   [Related Collation Tasks](#RelatedTasks)  
  
## <a name="UnderstandingCollations"></a>Understanding Collations in SQL Server PDW  
  
### <a name="Overview"></a>Overview  
SQL Server PDW has a case insensitive appliance-level collation, Latin1_General_100_CI_AS_KS_WS, which is used for both object names and string data.  
  
For object names, the appliance-level collation is fixed and cannot be changed. For example, login names, database names, and database object names, are always Latin1_General_100_CI_AS_KS_WS.  
  
For string data, the appliance-level collation is the default. However, there is a COLLATE clause that allows you to change the collation of columns and expressions to one of the Windows collations supported by SQL Server. You can specify the collation for a table column by using the COLLATE clause in the [CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md) statement or in the SELECT portion of the [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-as-select-sql-server-pdw.md) statement. You can also apply the COLLATE clause to any scalar [Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md) that evaluates to a string data type.  
  
### <a name="KeyTerms"></a>Key Terms  
collation  
Specifies the rules for how strings of character data are sorted and compared, based on the norms of particular languages and locales. For example, in an ORDER BY clause, an English speaker would expect the character string 'Chiapas' to come before 'Colima' in ascending order. However, a Spanish speaker in Mexico might expect words beginning with 'Ch' to appear at the end of a list of words starting with 'C'. Collations dictate these kinds of sorting and comparison rules. The Latin_1 General collation will sort 'Chiapas' before 'Colima' in an ORDER BY ASC clause, whereas the Traditional_Spanish collation will sort 'Chiapas' after 'Colima'.  
  
appliance-level collation  
Specifies the collation that is applied to all metadata and databases in the SQL Server PDW appliance. When a database is created, the default collation is the appliance-level collation.  
  
### <a name="Specify"></a>Specify Collations in SQL Server PDW  
The following table describes how collations are specified in SQL Server PDW.  
  
|Collation Level|SQL Server PDW Description|  
|-------------------|-----------------------------------------------------|  
|Appliance|Appliance-level collation is Latin1_General_100_CI_AS_KS_WS. This is pre-configured and cannot be changed. This collation is case-insensitive. All system-wide metadata, login names and database names use this collation and are case-insensitive.|  
|Database|The database collation is set to the appliance-level collation and cannot be changed. By default, all table names, view names, column names, variable names, expressions, string constants, and identifiers use the database-level collation and are case-insensitive.|  
|Column|By default, the column collation is set to the database collation. Column collations only apply to columns of type char, varchar, nchar, or nvarchar.<br /><br />You can use the COLLATE clause to specify the collation of a string column when the table is created. This is supported in CREATE TABLE and also in the SELECT portion of the CREATE TABLE AS SELECT statement.|  
|Expression|The collation of each expression is determined according to the SQL Server collation precedence rules. For more information, see [Collation Precedence](http://msdn.microsoft.com/en-us/library/ms179886.aspx) on MSDN.<br /><br />You can use the COLLATE clause to explicitly convert the collation of a scalar expression that evaluates to a string data type. For more information, see [Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)|  
|Identifier|The collation of an identifier depends on the level at which it is defined. Identifiers of appliance-level objects, such as logins and database names, are assigned the collation of the appliance. Identifiers of objects within a database, such as tables, views, and column names, are assigned the collation of the database, which in this release is equal to the appliance-level collation.|  
  
### <a name="Differences"></a>Differences between SQL Server PDW and SQL Server Collations  
SQL Server PDW handles collations in the same manner as SMP SQL Server, except for the following differences:  
  
-   SQL Server PDW has an appliance-level collation which is similar in purpose to the SMP SQL Server server collation. In MMP SQL Server PDW, the appliance-level collation is the default collation for all objects and string data in the appliance. In SMP SQL Server, the server collation is the default collation for all objects and string data in the SMP SQL Server instance.  
  
-   In SQL Server PDW, the database-level collation is the same as the appliance-level collation and cannot be changed. In SMP SQL Server, the database collation can be changed by using the COLLATE clause of the ALTER DATABASE statement..  
  
-   SQL Server PDW supports the [Windows collations](http://msdn.microsoft.com/en-us/library/ms188046.aspx) that are supported by SQL Server, except for collations that are deprecated in SQL Server. The deprecated collations are listed in the following table.  
  
    |Deprecated Windows Collations|Suggested Replacement|  
    |---------------------------------|-------------------------|  
    |Korean_Wansung_Unicode||  
    |Lithuanian_Classic||  
    |Hindi|Indic_General_90|  
    |Macedonian|Macedonian_FYROM_90|  
    |Azeri_Latin_90|Azeri_Latin_100|  
    |Azeri_Cyrilllic_90|Azeri_Cyrilllic_100|  
  
-   SQL Server PDW does not support the SQL Server-only collations that are supported by SQL Server.  
  
-   SQL Server PDW errors when an expression has a no-collation label, whereas SQL Server does not. As a result, SQL Server PDW gives an error when string columns in the same expression have different implicit collations, whereas SQL Server does not. For more information on expressions with a no-collation label, see [Collation Precedence (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms179886(v=SQL.110).aspx) on MSDN.  
  
-   Each string column in a table has an implicit collation, which is specified by the CREATE TABLE statement.  
  
    In SQL Server PDW, when string columns do not have matching implicit collations, use the COLLATE clause to explicitly change the collation of any of the column operands. By having at least one explicit COLLATE clause, the expression result will have a collation.  
  
    For example, SQL Server PDW will give an error for the string concatenation operation in the following UPDATE statement. The error occurs because x and y have different implicit collations and + is a collation-insensitive operator.  
  
    ```  
    CREATE TABLE abc  
    (  
    x nvarchar(20) COLLATE Corsican_100_CI_AI,  
    y nvarchar(20) COLLATE Greek_BIN2  
    )  
    ;  
    UPDATE abc SET x = x + y;  
    ```  
  
    In SQL Server PDW, use the COLLATE clause to explicitly change the collation of any of the column operands and thus the collation of the expression result. For example, the following statements will work in SQL Server PDW.  
  
    ```  
    UPDATE abc SET x = x + y COLLATE Corsican_100_CI_AI;  
    ```  
  
-   SQL Server PDW supports applying a COLLATE clause on an expression that already contains an explicit COLLATE clause. SQL Server gives an error for this. For example, the following statement will work in SQL Server PDW and will not work in SQL Server.  
  
    ```  
    CREATE TABLE SomeTable (id int, name nvarchar(20) COLLATE French_CS_AS);  
    INSERT INTO SomeTable VALUES (340, N'Francois');  
  
    SELECT id FROM SomeTable  
    WHERE name = (N‘Francois’COLLATE French_CI_AS) COLLATE French_CS_AS;  
    ;  
    ```  
  
-   In SQL Server PDW, local variables of sql_variant type always have the appliance-level collation, which is case-insensitive. This overrides an assigned collation when the variables are declared. Both of the following examples return 1.  
  
    ```  
    --@var1 and @var2 actually have the PDW-wide   
    --collation Latin1_General_100_CI_AS_KS_WS  
    DECLARE @var1 sql_variant = 'a' COLLATE Latin1_General_CS_AS;  
    DECLARE @var2 sql_variant = 'A' COLLATE Latin1_General_CS_AS  
    IF @var1 = @var2  
        SELECT 1;  
    ELSE  
        SELECT 2  
  
    DECLARE @var1 sql_variant = 'a' COLLATE Corsican_100_CI_AI;  
    DECLARE @var2 sql_variant = 'A' COLLATE Latin1_General_CS_AS  
    IF @var1 = @var2  
        SELECT 1;  
    ELSE  
        SELECT 2  
    ```  
  
For more information about how to work with collations in SQL Server, see [Working With Collations](http://msdn.microsoft.com/en-us/library/ms187582.aspx) on MSDN.  
  
## <a name="Examples"></a>Examples  
  
### A. Explicitly convert a string to a collation  
  
```  
CREATE TABLE abc  
(  
x nvarchar(20) COLLATE Corsican_100_CI_AI,  
y nvarchar(20) COLLATE Greek_BIN2  
)  
;  
UPDATE abc SET x = x + y COLLATE Corsican_100_CI_AI;  
```  
  
### B. Use nested COLLATE clauses  
  
```  
CREATE TABLE SomeTable (id int, name nvarchar(20) COLLATE French_CS_AS);  
INSERT INTO SomeTable VALUES (340, N'Francois');  
  
SELECT id FROM SomeTable  
WHERE name = (N‘Francois’COLLATE French_CI_AS) COLLATE French_CS_AS;  
;  
```  
  
### C. Use CREATE TABLE AS SELECT (CTAS) to change collations  
This example uses CTAS to change the collation of all the columns in the DatabaseLog table to Latin1_General_100_CI_AS_KS_WS, which is the default collation for the appliance. The example puts the results of the CTAS into the DatabaseLog2 table  
  
```  
/* CTAS the DatabaseLog table to DatabaseLog2 table with different collation */  
USE AdventureWorksPDW2012;  
If EXISTS (SELECT * from sys.tables t where t.name = 'DatabaseLog2')  
DROP TABLE DatabaseLog2;  
  
CREATE TABLE [AdventureWorksPDW2012].[dbo].[DatabaseLog2]  
WITH ( DISTRIBUTION = REPLICATE )  
AS SELECT   
    [DatabaseUser] COLLATE Latin1_General_100_CI_AS_KS_WS AS [DatabaseUser],  
[Event] COLLATE Latin1_General_100_CI_AS_KS_WS AS [Event],  
[Schema] COLLATE Latin1_General_100_CI_AS_KS_WS AS [Schema],  
[Object] COLLATE Latin1_General_100_CI_AS_KS_WS AS [Object],  
[TSQL] COLLATE Latin1_General_100_CI_AS_KS_WS AS [TSQL]  
FROM AdventureWorksPDW2012.dbo.DatabaseLog;  
```  
  
### D. Use ALTER TABLE to change the collation of a column  
This example uses [ALTER TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-table-sql-server-pdw.md) to change the collation of the LastName column in the dimEmployee table.  
  
This change affects only the metadata.  
  
```  
USE AdventureWorksPDW2012;  
ALTER TABLE AdventureWorksPDW2012.dbo.dimEmployee  
    ALTER COLUMN LastName nvarchar(50)  
    COLLATE Latin1_General_100_CI_AS_KS_WS;  
```  
  
## <a name="RelatedTasks"></a>Related Collation Tasks  
  
|Description|Links|  
|---------------|---------|  
|Use the COLLATE clause to specify the collation of a column in the [CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md) statement.|[CREATE TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-sql-server-pdw.md)|  
|Use the COLLATE clause to specify the collation of a column in the SELECT portion of the CREATE TABLE AS SELECT statement.|[CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-table-as-select-sql-server-pdw.md)|  
|Use the COLLATE clause to change the collation of a column in an existing table.|[ALTER TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-table-sql-server-pdw.md)|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
