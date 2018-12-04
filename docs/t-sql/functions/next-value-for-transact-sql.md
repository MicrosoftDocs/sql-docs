---
title: "NEXT VALUE FOR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "NEXT_VALUE_TSQL"
  - "NEXT"
  - "NEXT VALUE"
  - "NEXT VALUE FOR"
  - "NEXT_TSQL"
  - "NEXT_VALUE_FOR_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "NEXT VALUE FOR function"
  - "sequence number object, NEXT VALUE FOR function"
ms.assetid: 92632ed5-9f32-48eb-be28-a5e477ef9076
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# NEXT VALUE FOR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Generates a sequence number from the specified sequence object.  
  
 For a complete discussion of both creating and using sequences, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md). Use [sp_sequence_get_range](../../relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql.md) to generate reserve a range of sequence numbers.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
NEXT VALUE FOR [ database_name . ] [ schema_name . ]  sequence_name  
   [ OVER (<over_order_by_clause>) ]  
```  
  
## Arguments  
 *database_name*  
 The name of the database that contains the sequence object.  
  
 *schema_name*  
 The name of the schema that contains the sequence object.  
  
 *sequence_name*  
 The name of the sequence object that generates the number.  
  
 *over_order_by_clause*  
 Determines the order in which the sequence value is assigned to the rows in a partition. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).  
  
## Return Types  
 Returns a number using the type of the sequence.  
  
## Remarks  
 The **NEXT VALUE FOR** function can be used in stored procedures and triggers.  
  
 When the **NEXT VALUE FOR** function is used in a query or default constraint, if the same sequence object is used more than once, or if the same sequence object is used both in the statement supplying the values, and in a default constraint being executed, the same value will be returned for all columns referencing the same sequence within a row in the result set.  
  
 The **NEXT VALUE FOR** function is nondeterministic, and is only allowed in contexts where the number of generated sequence values is well defined. Below is the definition of how many values will be used for each referenced sequence object in a given statement:  
  
-   **SELECT** - For each referenced sequence object, a new value is generated once per row in the result of the statement.  
  
-   **INSERT** ... **VALUES** - For each referenced sequence object, a new value is generated once for each inserted row in the statement.  
  
-   **UPDATE** - For each referenced sequence object, a new value is generated for each row being updated by the statement.  
  
-   Procedural statements (such as **DECLARE**, **SET**, etc.) - For each referenced sequence object, a new value is generated for each statement.  
  
## Limitations and Restrictions  
 The **NEXT VALUE FOR** function cannot be used in the following situations:  
  
-   When a database is in read-only mode.  
  
-   As an argument to a table-valued function.  
  
-   As an argument to an aggregate function.  
  
-   In subqueries including common table expressions and derived tables.  
  
-   In views, in user-defined functions, or in computed columns.  
  
-   In a statement using the **DISTINCT**, **UNION**, **UNION ALL**, **EXCEPT** or **INTERSECT** operator.  
  
-   In a statement using the **ORDER BY** clause unless **NEXT VALUE FOR** ... **OVER** (**ORDER BY** ...) is used.  
  
-   In the following clauses: **FETCH**, **OVER**, **OUTPUT**, **ON**, **PIVOT**, **UNPIVOT**, **GROUP BY**, **HAVING**, **COMPUTE**, **COMPUTE BY**, or **FOR XML**.  
  
-   In conditional expressions using **CASE**, **CHOOSE**, **COALESCE**, **IIF**, **ISNULL**, or **NULLIF**.  
  
-   In a **VALUES** clause that is not part of an **INSERT** statement.  
  
-   In the definition of a check constraint.  
  
-   In the definition of a rule or default object. (It can be used in a default constraint.)  
  
-   As a default in a user-defined table type.  
  
-   In a statement using **TOP**, **OFFSET**, or when the **ROWCOUNT** option is set.  
  
-   In the **WHERE** clause of a statement.  
  
-   In a **MERGE** statement. (Except when the **NEXT VALUE FOR** function is used in a default constraint in the target table and default is used in the **CREATE** statement of the **MERGE** statement.)  
  
## Using a Sequence Object in a Default Constraint  
 When using the **NEXT VALUE FOR** function in a default constraint, the following rules apply:  
  
-   A single sequence object may be referenced from default constraints in multiple tables.  
  
-   The table and the sequence object must reside in the same database.  
  
-   The user adding the default constraint must have REFERENCES permission on the sequence object.  
  
-   A sequence object that is referenced from a default constraint cannot be dropped before the default constraint is dropped.  
  
-   The same sequence number is returned for all columns in a row if multiple default constraints use the same sequence object, or if the same sequence object is used both in the statement supplying the values, and in a default constraint being executed.  
  
-   References to the **NEXT VALUE FOR** function in a default constraint cannot specify the **OVER** clause.  
  
-   A sequence object that is referenced in a default constraint can be altered.  
  
-   In the case of an `INSERT ... SELECT` or `INSERT ... EXEC` statement where the data being inserted comes from a query using an **ORDER BY** clause, the values being returned by the **NEXT VALUE FOR** function will be generated in the order specified by the **ORDER BY** clause.  
  
## Using a Sequence Object with an OVER ORDER BY Clause  
 The **NEXT VALUE FOR** function supports generating sorted sequence values by applying the **OVER** clause to the **NEXT VALUE FOR** call. By using the **OVER** clause, a user is guaranteed that the values being returned are generated in the order of the **OVER** clause's **ORDER B**Y subclause. The following additional rules apply when using the **NEXT VALUE FOR** function with the **OVER** clause:  
  
-   Multiple calls to the **NEXT VALUE FOR** function for the same sequence generator in a single statement must all use the same **OVER** clause definition.  
  
-   Multiple calls to the **NEXT VALUE FOR** function that reference different sequence generators in a single statement can have different **OVER** clause definitions.  
  
-   An **OVER** clause applied to the **NEXT VALUE FOR** function does not support the **PARTITION BY** sub clause.  
  
-   If all calls to the **NEXT VALUE FOR** function in a **SELECT** statement specifies the **OVER** clause, an **ORDER BY** clause may be used in the **SELECT** statement.  
  
-   The **OVER** clause is allowed with the **NEXT VALUE FOR** function when used in a **SELECT** statement or `INSERT ... SELECT ...` statement. Use of the **OVER** clause with the **NEXT VALUE FOR** function is not allowed in **UPDATE** or **MERGE** statements.  
  
-   If another process is accessing the sequence object at the same time, the numbers returned could have gaps.  
  
## Metadata  
 For information about sequences, query the [sys.sequences](../../relational-databases/system-catalog-views/sys-sequences-transact-sql.md) catalog view.  
  
## Security  
  
### Permissions  
 Requires **UPDATE** permission on the sequence object or the schema of the sequence. For an example of granting permission, see example F later in this topic.  
  
### Ownership Chaining  
 Sequence objects support ownership chaining. If the sequence object has the same owner as the calling stored procedure, trigger, or table (having a sequence object as a default constraint), no permission check is required on the sequence object. If the sequence object is not owned by the same user as the calling stored procedure, trigger, or table, a permission check is required on the sequence object.  
  
 When the **NEXT VALUE FOR** function is used as a default value in a table, users require both **INSERT** permission on the table, and **UPDATE** permission on the sequence object, to insert data using the default.  
  
-   If the default constraint has the same owner as the sequence object, no permissions are required on the sequence object when the default constraint is called.  
  
-   If the default constraint and the sequence object are not owned by the same user, permissions are required on the sequence object even if it is called through the default constraint.  
  
### Audit  
 To audit the **NEXT VALUE FOR** function, monitor the SCHEMA_OBJECT_ACCESS_GROUP.  
  
## Examples  
 For examples of both creating sequences and using the **NEXT VALUE FOR** function to generate sequence numbers, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
 The following examples use a sequence named `CountBy1` in a schema named `Test`. Execute the following statement to create the `Test.CountBy1` sequence. Examples C and E use the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, so the `CountBy1` sequence is created in that database.  
  
```  
USE AdventureWorks2012 ;  
GO  
  
CREATE SCHEMA Test;  
GO  
  
CREATE SEQUENCE Test.CountBy1  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
```  
  
### A. Using a sequence in a select statement  
 The following example creates a sequence named `CountBy1` that increases by one every time that it is used.  
  
```  
SELECT NEXT VALUE FOR Test.CountBy1 AS FirstUse;  
SELECT NEXT VALUE FOR Test.CountBy1 AS SecondUse;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
FirstUse  
1  
  
SecondUse  
2
```  
  
### B. Setting a variable to the next sequence value  
 The following example demonstrates three ways to set a variable to the next value of a sequence number.  
  
```  
DECLARE @myvar1 bigint = NEXT VALUE FOR Test.CountBy1  
DECLARE @myvar2 bigint ;  
DECLARE @myvar3 bigint ;  
SET @myvar2 = NEXT VALUE FOR Test.CountBy1 ;  
SELECT @myvar3 = NEXT VALUE FOR Test.CountBy1 ;  
SELECT @myvar1 AS myvar1, @myvar2 AS myvar2, @myvar3 AS myvar3 ;  
GO  
```  
  
### C. Using a sequence with a ranking window function  
  
```  
USE AdventureWorks2012 ;  
GO  
  
SELECT NEXT VALUE FOR Test.CountBy1 OVER (ORDER BY LastName) AS ListNumber,  
    FirstName, LastName  
FROM Person.Contact ;  
GO  
```  
  
### D. Using the NEXT VALUE FOR function in the definition of a default constraint  
 Using the **NEXT VALUE FOR** function in the definition of a default constraint is supported. For an example of using **NEXT VALUE FOR** in a **CREATE TABLE** statement, see Example C[Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md). The following example uses `ALTER TABLE` to add a sequence as a default to a current table.  
  
```  
CREATE TABLE Test.MyTable  
(  
    IDColumn nvarchar(25) PRIMARY KEY,  
    name varchar(25) NOT NULL  
) ;  
GO  
  
CREATE SEQUENCE Test.CounterSeq  
    AS int  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
  
ALTER TABLE Test.MyTable  
    ADD   
        DEFAULT N'AdvWorks_' +   
        CAST(NEXT VALUE FOR Test.CounterSeq AS NVARCHAR(20))   
        FOR IDColumn;  
GO  
  
INSERT Test.MyTable (name)  
VALUES ('Larry') ;  
GO  
  
SELECT * FROM Test.MyTable;  
GO  
```  
  
### E. Using the NEXT VALUE FOR function in an INSERT statement  
 The following example creates a table named `TestTable` and then uses the `NEXT VALUE FOR` function to insert a row.  
  
```  
CREATE TABLE Test.TestTable  
     (CounterColumn int PRIMARY KEY,  
    Name nvarchar(25) NOT NULL) ;   
GO  
  
INSERT Test.TestTable (CounterColumn,Name)  
    VALUES (NEXT VALUE FOR Test.CountBy1, 'Syed') ;  
GO  
  
SELECT * FROM Test.TestTable;   
GO  
  
```  
  
### E. Using the NEXT VALUE FOR function with SELECT ... INTO  
 The following example uses the `SELECT ... INTO` statement to create a table named `Production.NewLocation` and uses the `NEXT VALUE FOR` function to number each row.  
  
```  
USE AdventureWorks2012 ;   
GO  
  
SELECT NEXT VALUE FOR Test.CountBy1 AS LocNumber, Name   
    INTO Production.NewLocation  
    FROM Production.Location ;  
GO  
  
SELECT * FROM Production.NewLocation ;  
GO  
```  
  
### F. Granting permission to execute NEXT VALUE FOR  
 The following example grants **UPDATE** permission to a user named `AdventureWorks\Larry` permission to execute `NEXT VALUE FOR` using the `Test.CounterSeq` sequence.  
  
```  
GRANT UPDATE ON OBJECT::Test.CounterSeq TO [AdventureWorks\Larry] ;  
```  
  
## See Also  
 [CREATE SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-sequence-transact-sql.md)   
 [ALTER SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-sequence-transact-sql.md)   
 [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md)  
  
  
