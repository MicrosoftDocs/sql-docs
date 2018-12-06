---
title: "MSSQLSERVER_207 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "207 (Database Engine error)"
ms.assetid: d1ab00c7-0331-437a-84fe-bae53b82feec
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_207
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|207|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQ_BADCOL|  
|Message Text|Invalid column name '%.*ls'.|  
  
## Explanation  
This query error can be caused by one of the following problems.  
  
-   The column name is misspelled or the column does not exist in any of the specified tables.  
  
-   The collation of the database is case-sensitive and the case of the column name specified in the query does not match the case of the column defined in the table. For example, when a column is defined in a table as **LastName** and the database uses a case-sensitive collation, queries that refer to the column as **Lastname** or **lastname** will cause error 207 to return because the column name does not match.  
  
-   A column alias, defined in the SELECT clause, is referenced in another clause such as a WHERE or GROUP BY clause. For example, the following query defines the column alias `Year` in the SELECT clause and refers to it in the GROUP BY clause.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT DATEPART(yyyy,OrderDate) AS Year, SUM(TotalDue) AS Total  
    FROM Sales.SalesOrderHeader  
    GROUP BY Year;  
    ```  
  
    Due to the order in which query clauses are logically processed, the example returns error 207. The processing order is as follows:  
  
    1.  FROM  
  
    2.  ON  
  
    3.  JOIN  
  
    4.  WHERE  
  
    5.  GROUP BY  
  
    6.  WITH CUBE or WITH ROLLUP  
  
    7.  HAVING  
  
    8.  SELECT  
  
    9. DISTINCT  
  
    10. ORDER BY  
  
    11. TOP  
  
    Because a column alias is not defined until the SELECT clause is processed, the alias name is unknown when the GROUP BY clause is processed.  
  
-   The MERGE statement raises this error when the *<merge_matched>* clause references columns in the source table but no rows are returned by the source table in the WHEN NOT MATCHED BY SOURCE clause. The error occurs because the columns in the source table cannot be accessed when no rows are returned to the query. For example, the clause `WHEN NOT MATCHED BY SOURCE THEN UPDATE SET TargetTable.Col1 = SourceTable.Col1` may cause the statement to fail if `Col1` in the source table is inaccessible.  
  
## User Action  
Verify the following information and correct the statement as appropriate.  
  
-   The column name exists in the table and is spelled correctly. The following example queries the **sys.columns** catalog view to return all column names for a given table.  
  
    ```  
    SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('schema_name.table_name');  
    ```  
  
-   The case sensitivity of the database collation. The following statement returns the collation of the specified database.  
  
    ```  
    SELECT collation_name FROM sys.databases WHERE name = 'database_name';  
    ```  
  
    The abbreviation CS in the collation name indicates the collation is case-sensitive. For example, Latin1_General_CS_AS is a case-sensitive and accent-sensitive collation. Modify the column name to match the case of the column name as it is defined in the table.  
  
-   A column alias is referenced incorrectly. Modify the statement by repeating the expression that defines the alias in the appropriate clause or by using a derived table. The following example repeats the expressions that define the `Year` alias in the GROUP BY clause.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT DATEPART(yyyy,OrderDate) AS Year ,SUM(TotalDue) AS Total  
    FROM Sales.SalesOrderHeader  
    GROUP BY DATEPART(yyyy,OrderDate);  
    ```  
  
    The following example uses a derived table to make the alias name available to other clauses in the query. Notice that the alias `Year` is defined in the FROM clause, which is processed first, and so makes the alias available for use in other clauses in the query.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT d.Year, SUM(TotalDue) AS Total  
    FROM (SELECT DATEPART(yyyy,OrderDate) AS Year, TotalDue  
          FROM Sales.SalesOrderHeader)AS d  
    GROUP BY Year;  
    ```  
  
-   The WHEN NOT MATCHED BY SOURCE clause in the MERGE statement refers to a value that can be accessed. Modify the MERGE statement so that at least one row is returned by the source table in the WHEN NOT MATCHED BY SOURCE clause. For example, you might need to add or revise the search condition specified for the clause. Alternatively, you can modify the clause to specify a value that does not reference the source table. For example, `WHEN NOT MATCHED BY SOURCE THEN UPDATE SET TargetTable.Col1 = <expression, or other available value>`.  
  
## See Also  
[MERGE &#40;Transact-SQL&#41;](~/t-sql/statements/merge-transact-sql.md)  
[FROM &#40;Transact-SQL&#41;](~/t-sql/queries/from-transact-sql.md)  
[SELECT &#40;Transact-SQL&#41;](~/t-sql/queries/select-transact-sql.md)  
[UPDATE &#40;Transact-SQL&#41;](~/t-sql/queries/update-transact-sql.md)  
  
