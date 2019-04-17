---
title: "MSSQLSERVER_4104 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "4104 (Database Engine error)"
ms.assetid: 52dc32d8-97ad-4ef0-834d-2e68f215d007
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_4104
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|4104|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|ALG_MULTI_ID_BAD|  
|Message Text|The multi-part identifier "%.*ls" could not be bound.|  
  
## Explanation  
The name of an entity in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is referred to as its *identifier*. You use identifiers whenever you reference entities, for example, by specifying column and table names in a query. A multi-part identifier contains one or more qualifiers as a prefix for the identifier. For example, a table identifier may be prefixed with qualifiers such as the database name and schema name in which the table is contained, or a column identifier may be prefixed with qualifiers such as a table name or table alias.  
  
Error 4104 indicates that the specified multi-part identifier could not be mapped to an existing entity. This error can be returned under the following conditions:  
  
-   The qualifier supplied as a prefix for a column name does not correspond to any table or alias name used in the query.  
  
    For example, the following statement uses a table alias (`Dept`) as a column prefix, but the table alias is not referenced in the FROM clause.  
  
    ```  
    SELECT Dept.Name FROM HumanResources.Department;  
    ```  
  
    In the following statements, a multi-part column identifier `TableB.KeyCol` is specified in the WHERE clause as part of a JOIN condition between two tables, however, `TableB` is not explicitly referenced in the query.  
  
    ```  
    DELETE FROM TableA WHERE TableA.KeyCol = TableB.KeyCol;  
    ```  
  
    ```  
    SELECT 'X' FROM TableA WHERE TableB.KeyCol = TableA.KeyCol;  
    ```  
  
-   An alias name for the table is supplied in the FROM clause, but the qualifier supplied for a column is the table name. For example, the following statement uses the table name `Department` as the column prefix; however, the table has an alias (`Dept`) referenced in the FROM clause.  
  
    ```  
    SELECT Department.Name FROM HumanResources.Department AS Dept;  
    ```  
  
    When an alias is used, the table name cannot be used elsewhere in the statement.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to determine if the multi-part identifier refers to a column prefixed by a table or to a property of a CLR user-defined data type (UDT) prefixed by a column. This happens because properties of UDT columns are referenced by using the period separator (.) between the column name and the property name in the same way that a column name is prefixed with a table name. The following example creates two tables, `a` and `b`. Table `b` contains column `a`, which uses a CLR UDT `dbo.myudt2` as its data type. The SELECT statement contains a multi-part identifier `a.c2`.  
  
    ```  
    CREATE TABLE a (c2 int);   
    GO  
    ```  
  
    ```  
    CREATE TABLE b (a dbo.myudt2);   
    GO  
    ```  
  
    ```  
    SELECT a.c2 FROM a, b;   
    ```  
  
    Assuming that the UDT `myudt2` does not have a property named `c2`, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot determine whether identifier `a.c2`refers to column `c2` in table `a` or to the column `a`, property `c2` in table `b`.  
  
## User Action  
  
-   Match the column prefixes against the table names or alias names specified in the FROM clause of the query. If an alias is defined for a table name in the FROM clause, you can only use the alias as a qualifier for columns associated with that table.  
  
    The statements above that reference the `HumanResources.Department` table can be corrected as follows:  
  
    ```  
    SELECT Dept.Name FROM HumanResources.Department AS Dept;  
    GO  
    ```  
  
    ```  
    SELECT Department.Name FROM HumanResources.Department;  
    GO  
    ```  
  
-   Ensure that all tables are specified in the query and that the JOIN conditions between tables are specified correctly. The DELETE statement above can be corrected as follows:  
  
    ```  
    DELETE FROM dbo.TableA  
    WHERE TableA.KeyCol = (SELECT TableB.KeyCol   
                            FROM TableB   
                            WHERE TableA.KeyCol = TableB.KeyCol);  
    GO  
    ```  
  
    The SELECT statement above for `TableA` can be corrected as follows:  
  
    ```  
    SELECT 'X' FROM TableA, TableB WHERE TableB.KeyCol = TableA.KeyCol;  
    ```  
  
    or  
  
    ```  
    SELECT 'X' FROM TableA INNER JOIN TableB ON TableB.KeyCol = TableA.KeyCol;  
    ```  
  
-   Use unique, clearly defined names for identifiers. Doing so makes your code easier to read and maintain, and it also minimizes the risk of ambiguous references to multiple entities.  
  
## See Also  
[MSSQLSERVER_107](~/relational-databases/errors-events/mssqlserver-107-database-engine-error.md)  
[Database Identifiers](~/relational-databases/databases/database-identifiers.md)  
  
