---
title: "SET SHOWPLAN_TEXT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SHOWPLAN_TEXT"
  - "SET_SHOWPLAN_TEXT_TSQL"
  - "SET SHOWPLAN_TEXT"
  - "SHOWPLAN_TEXT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statements [SQL Server], estimates"
  - "execution information and estimates [SQL Server]"
  - "statements [SQL Server], information without processing"
  - "SET SHOWPLAN_TEXT statement"
  - "canceling statement execution"
  - "SHOWPLAN_TEXT option"
  - "stopping statement execution"
  - "estimated execution information [SQL Server]"
ms.assetid: 2c4f3fc8-ff2c-4790-8b74-e7e8ef58f9a6
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET SHOWPLAN_TEXT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Causes Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] not to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns detailed information about how the statements are executed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET SHOWPLAN_TEXT { ON | OFF }  
```  
  
## Remarks  
 The setting of SET SHOWPLAN_TEXT is set at execute or run time and not at parse time.  
  
 When SET SHOWPLAN_TEXT is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns execution information for each [!INCLUDE[tsql](../../includes/tsql-md.md)] statement without executing it. After this option is set ON, execution plan information about all subsequent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statements is returned until the option is set OFF. For example, if a CREATE TABLE statement is executed while SET SHOWPLAN_TEXT is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message from a subsequent SELECT statement involving that same table informing the user that the specified table does not exist. Therefore, subsequent references to this table fail. When SET SHOWPLAN_TEXT is OFF, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes statements without generating a report with execution plan information.  
  
 SET SHOWPLAN_TEXT is intended to return readable output for Microsoft Win32 command prompt applications such as the **sqlcmd** utility. SET SHOWPLAN_ALL returns more detailed output intended to be used with programs designed to handle its output.  
  
 SET SHOWPLAN_TEXT and SET SHOWPLAN_ALL cannot be specified in a stored procedure. They must be the only statements in a batch.  
  
 SET SHOWPLAN_TEXT returns information as a set of rows that form a hierarchical tree representing the steps taken by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query processor as it executes each statement. Each statement reflected in the output contains a single row with the text of the statement, followed by several rows with the details of the execution steps. The table shows the column that the output contains.  
  
|Column name|Description|  
|-----------------|-----------------|  
|**StmtText**|For rows which are not of type PLAN_ROW, this column contains the text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For rows of type PLAN_ROW, this column contains a description of the operation. This column contains the physical operator and may optionally also contain the logical operator. This column may also be followed by a description which is determined by the physical operator. For more information about physical operators, see the **Argument** column in [SET SHOWPLAN_ALL &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-all-transact-sql.md).|  
  
 For more information about the physical and logical operators that can be seen in Showplan output, see [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)  
  
## Permissions  
 In order to use SET SHOWPLAN_TEXT, you must have sufficient permissions to execute the statements on which SET SHOWPLAN_TEXT is executed, and you must have SHOWPLAN permission for all databases containing referenced objects.  
  
 For SELECT, INSERT, UPDATE, DELETE, EXEC *stored_procedure*, and EXEC *user_defined_function* statements, to produce a Showplan the user must:  
  
-   Have the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   Have SHOWPLAN permission on all databases containing objects referenced by the Transact-SQL statements, such as tables, views, and so on.  
  
 For all other statements, such as DDL, USE *database_name*, SET, DECLARE, dynamic SQL, and so on, only the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are needed.  
  
## Examples  
 This example shows how indexes are used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as it processes the statements.  
  
 This is the query using an index:  
  
```  
USE AdventureWorks2012;  
GO  
SET SHOWPLAN_TEXT ON;  
GO  
SELECT *  
FROM Production.Product   
WHERE ProductID = 905;  
GO  
SET SHOWPLAN_TEXT OFF;  
GO  
```  
  
 Here is the result set:  
  
```  
StmtText                                             
---------------------------------------------------  
SELECT *  
FROM Production.Product   
WHERE ProductID = 905;   
  
StmtText                                                                                                                                                                                        
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
|--Clustered Index Seek(OBJECT:([AdventureWorks2012].[Production].[Product].[PK_Product_ProductID]), SEEK:([AdventureWorks2012].[Production].[Product].[ProductID]=CONVERT_IMPLICIT(int,[@1],0)) ORDERED FORWARD)   
```  
  
 Here is the query not using an index:  
  
```  
USE AdventureWorks2012;  
GO  
SET SHOWPLAN_TEXT ON;  
GO  
SELECT *  
FROM Production.ProductCostHistory  
WHERE StandardCost < 500.00;  
GO  
SET SHOWPLAN_TEXT OFF;  
GO  
```  
  
 Here is the result set:  
  
```  
StmtText                                                                  
------------------------------------------------------------------------  
SELECT *  
FROM Production.ProductCostHistory  
WHERE StandardCost < 500.00;   
  
StmtText                                                                                                                                                                                                  
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
|--Clustered Index Scan(OBJECT:([AdventureWorks2012].[Production].[ProductCostHistory].[PK_ProductCostHistory_ProductCostID]), WHERE:([AdventureWorks2012].[Production].[ProductCostHistory].[StandardCost]<[@1]))  
```  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET SHOWPLAN_ALL &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-all-transact-sql.md)  
  
  
