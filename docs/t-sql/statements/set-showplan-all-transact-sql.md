---
title: "SET SHOWPLAN_ALL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SET SHOWPLAN_ALL"
  - "SET_SHOWPLAN_ALL_TSQL"
  - "SHOWPLAN_ALL"
  - "SHOWPLAN_ALL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statements [SQL Server], estimates"
  - "execution information and estimates [SQL Server]"
  - "statements [SQL Server], information without processing"
  - "SET SHOWPLAN_ALL statement"
  - "SHOWPLAN_ALL option"
  - "canceling statement execution"
  - "stopping statement execution"
  - "estimated execution information [SQL Server]"
ms.assetid: a500b682-bae4-470f-9e00-47de905b851b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET SHOWPLAN_ALL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Causes Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] not to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns detailed information about how the statements are executed and provides estimates of the resource requirements for the statements.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET SHOWPLAN_ALL { ON | OFF }  
```  
  
## Remarks  
 The setting of SET SHOWPLAN_ALL is set at execute or run time and not at parse time.  
  
 When SET SHOWPLAN_ALL is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns execution information for each statement without executing it, and [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are not executed. After this option is set ON, information about all subsequent [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are returned until the option is set OFF. For example, if a CREATE TABLE statement is executed while SET SHOWPLAN_ALL is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message from a subsequent SELECT statement involving that same table, informing users that the specified table does not exist. Therefore, subsequent references to this table fail. When SET SHOWPLAN_ALL is OFF, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes the statements without generating a report.  
  
 SET SHOWPLAN_ALL is intended to be used by applications written to handle its output. Use SET SHOWPLAN_TEXT to return readable output for Microsoft Win32 command prompt applications, such as the **osql** utility.  
  
 SET SHOWPLAN_TEXT and SET SHOWPLAN_ALL cannot be specified inside a stored procedure; they must be the only statements in a batch.  
  
 SET SHOWPLAN_ALL returns information as a set of rows that form a hierarchical tree representing the steps taken by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query processor as it executes each statement. Each statement reflected in the output contains a single row with the text of the statement, followed by several rows with the details of the execution steps. The table shows the columns that the output contains.  
  
|Column name|Description|  
|-----------------|-----------------|  
|**StmtText**|For rows that are not of type PLAN_ROW, this column contains the text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For rows of type PLAN_ROW, this column contains a description of the operation. This column contains the physical operator and may optionally also contain the logical operator. This column may also be followed by a description that is determined by the physical operator. For more information, see [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md).|  
|**StmtId**|Number of the statement in the current batch.|  
|**NodeId**|ID of the node in the current query.|  
|**Parent**|Node ID of the parent step.|  
|**PhysicalOp**|Physical implementation algorithm for the node. For rows of type PLAN_ROWS only.|  
|**LogicalOp**|Relational algebraic operator this node represents. For rows of type PLAN_ROWS only.|  
|**Argument**|Provides supplemental information about the operation being performed. The contents of this column depend on the physical operator.|  
|**DefinedValues**|Contains a comma-separated list of values introduced by this operator. These values may be computed expressions which were present in the current query (for example, in the SELECT list or WHERE clause), or internal values introduced by the query processor in order to process this query. These defined values may then be referenced elsewhere within this query. For rows of type PLAN_ROWS only.|  
|**EstimateRows**|Estimated number of rows of output produced by this operator. For rows of type PLAN_ROWS only.|  
|**EstimateIO**|Estimated I/O cost* for this operator. For rows of type PLAN_ROWS only.|  
|**EstimateCPU**|Estimated CPU cost* for this operator. For rows of type PLAN_ROWS only.|  
|**AvgRowSize**|Estimated average row size (in bytes) of the row being passed through this operator.|  
|**TotalSubtreeCost**|Estimated (cumulative) cost* of this operation and all child operations.|  
|**OutputList**|Contains a comma-separated list of columns being projected by the current operation.|  
|**Warnings**|Contains a comma-separated list of warning messages relating to the current operation. Warning messages may include the string "NO STATS:()" with a list of columns. This warning message means that the query optimizer attempted to make a decision based on the statistics for this column, but none were available. Consequently, the query optimizer had to make a guess, which may have resulted in the selection of an inefficient query plan. For more information about creating or updating column statistics (which help the query optimizer choose a more efficient query plan), see [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md). This column may optionally include the string "MISSING JOIN PREDICATE", which means that a join (involving tables) is taking place without a join predicate. Accidentally dropping a join predicate may result in a query which takes much longer to run than expected, and returns a huge result set. If this warning is present, verify that the absence of a join predicate is intentional.|  
|**Type**|Node type. For the parent node of each query, this is the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement type (for example, SELECT, INSERT, EXECUTE, and so on). For subnodes representing execution plans, the type is PLAN_ROW.|  
|**Parallel**|**0** = Operator is not running in parallel.<br /><br /> **1** = Operator is running in parallel.|  
|**EstimateExecutions**|Estimated number of times this operator will be executed while running the current query.|  
  
 *Cost units are based on an internal measurement of time, not wall-clock time. They are used for determining the relative cost of a plan in comparison to other plans.  
  
## Permissions  
 In order to use SET SHOWPLAN_ALL, you must have sufficient permissions to execute the statements on which SET SHOWPLAN_ALL is executed, and you must have SHOWPLAN permission for all databases containing referenced objects.  
  
 For SELECT, INSERT, UPDATE, DELETE, EXEC *stored_procedure*, and EXEC *user_defined_function* statements, to produce a Showplan the user must:  
  
-   Have the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   Have SHOWPLAN permission on all databases containing objects referenced by the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, such as tables, views, and so on.  
  
 For all other statements, such as DDL, USE *database_name*, SET, DECLARE, dynamic SQL, and so on, only the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are needed.  
  
## Examples  
 The two statements that follow use the SET SHOWPLAN_ALL settings to show the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] analyzes and optimizes the use of indexes in queries.  
  
 The first query uses the Equals comparison operator (=) in the WHERE clause on an indexed column. This results in the Clustered Index Seek value in the **LogicalOp** column and the name of the index in the **Argument** column.  
  
 The second query uses the LIKE operator in the WHERE clause. This forces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use a clustered index scan and find the data that satisfies the WHERE clause condition. This results in the Clustered Index Scan value in the **LogicalOp** column with the name of the index in the **Argument** column, and the Filter value in the **LogicalOp** column with the WHERE clause condition in the **Argument** column.  
  
 The values in the **EstimateRows** and the **TotalSubtreeCost** columns are smaller for the first indexed query, indicating that it is processed much faster and uses fewer resources than the nonindexed query.  
  
```  
USE AdventureWorks2012;  
GO  
SET SHOWPLAN_ALL ON;  
GO  
-- First query.  
SELECT BusinessEntityID   
FROM HumanResources.Employee  
WHERE NationalIDNumber = '509647174';  
GO  
-- Second query.  
SELECT BusinessEntityID, EmergencyContactID   
FROM HumanResources.Employee  
WHERE EmergencyContactID LIKE '1%';  
GO  
SET SHOWPLAN_ALL OFF;  
GO  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET SHOWPLAN_TEXT &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-text-transact-sql.md)   
 [SET SHOWPLAN_XML &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-xml-transact-sql.md)  
  
  
