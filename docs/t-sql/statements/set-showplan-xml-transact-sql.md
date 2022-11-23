---
title: "SET SHOWPLAN_XML (Transact-SQL)"
description: SET SHOWPLAN_XML (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/09/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET SHOWPLAN_XML"
  - "SET_SHOWPLAN_XML_TSQL"
helpviewer_keywords:
  - "statements [SQL Server], estimates"
  - "SET SHOWPLAN_XML statement"
  - "execution information and estimates [SQL Server]"
  - "statements [SQL Server], information without processing"
  - "XML [SQL Server], statement execution information"
  - "SHOWPLAN_XML option"
  - "estimated execution information [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET SHOWPLAN_XML (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] not to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns detailed information about how the statements are going to be executed in the form of a well-defined XML document.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET SHOWPLAN_XML { ON | OFF }
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks

The setting of SET SHOWPLAN_XML is set at execute or run time and not at parse time.

When SET SHOWPLAN_XML is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns execution plan information for each statement without executing it, and [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are not executed. After this option is set ON, execution plan information about all subsequent [!INCLUDE[tsql](../../includes/tsql-md.md)] statements is returned until the option is set OFF. For example, if a CREATE TABLE statement is executed while SET SHOWPLAN_XML is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message from a subsequent SELECT statement involving that same table; the specified table does not exist. Therefore, subsequent references to this table fail. When SET SHOWPLAN_XML is OFF, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes the statements without generating a report.

SET SHOWPLAN_XML is intended to return output as **nvarchar(max)** for applications such as the **sqlcmd** utility, where the XML output is subsequently used by other tools to display and process the query plan information.

> [!NOTE]
> The dynamic management view, **sys.dm_exec_query_plan**, returns the same information as SET SHOWPLAN XML in the **xml** data type. This information is returned from the **query_plan** column of **sys.dm_exec_query_plan**. For more information, see [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md).

SET SHOWPLAN_XML cannot be specified inside a stored procedure. It must be the only statement in a batch.

SET SHOWPLAN_XML returns information as a set of XML documents. Each batch after the SET SHOWPLAN_XML ON statement is reflected in the output by a single document. Each document contains the text of the statements in the batch, followed by the details of the execution steps. The document shows the estimated costs, numbers of rows, accessed indexes, and types of operators performed, join order, and more information about the execution plans.

> [!NOTE]
> If **Include Actual Execution Plan** is selected in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], this SET option does not produce XML Showplan output. Clear the **Include Actual Execution Plan** button before using this SET option.

### Location of SHOWPLAN output

The document containing the XML schema for the XML output by SET SHOWPLAN_XML is copied during setup to a local directory on the computer on which Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. The document can be found on the drive containing the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation files, at a path similar to the following:

- `\Microsoft SQL Server\130\Tools\Binn\schemas\sqlserver\2004\07\showplan\showplanxml.xsd`

In the preceding path, the node `130\` is used by SQL Server 2016. The number 130 is derived from the first node of the value returned by `SELECT @@VERSION`, which is 13. For SQL Server 2017 the path would use `140\`, because the first node of its @@VERSION value is 14. For SQL Server 2019 the first value from @@VERSION is 15.

The Showplan Schema can also be found at [this Web site](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409).

## Permissions

In order to use SET SHOWPLAN_XML, you must have sufficient permissions to execute the statements on which SET SHOWPLAN_XML is executed, and you must have SHOWPLAN permission for all databases containing referenced objects.

For SELECT, INSERT, UPDATE, DELETE, EXEC *stored_procedure*, and EXEC *user_defined_function* statements, to produce a Showplan the user must:

- Have the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.

- Have SHOWPLAN permission on all databases containing objects referenced by the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, such as tables, views, and so on.

For all other statements, such as DDL, USE *database_name*, SET, DECLARE, dynamic SQL, and so on, only the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are needed.

## Examples

The two statements that follow use the SET SHOWPLAN_XML settings to show the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] analyzes and optimizes the use of indexes in queries.

The first query uses the Equals comparison operator (=) in the WHERE clause on an indexed column. The second query uses the LIKE operator in the WHERE clause. This forces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use a clustered index scan and find the data meeting the WHERE clause condition. The values in the **EstimateRows** and the **EstimatedTotalSubtreeCost** attributes are smaller for the first indexed query, indicating that it is processed much faster and uses fewer resources than the nonindexed query.

```sql
USE AdventureWorks2012;
GO
SET SHOWPLAN_XML ON;
GO
-- First query.
SELECT BusinessEntityID
FROM HumanResources.Employee
WHERE NationalIDNumber = '509647174';
GO
-- Second query.
SELECT BusinessEntityID, JobTitle
FROM HumanResources.Employee
WHERE JobTitle LIKE 'Production%';
GO
SET SHOWPLAN_XML OFF;
```

## See Also

[SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)
