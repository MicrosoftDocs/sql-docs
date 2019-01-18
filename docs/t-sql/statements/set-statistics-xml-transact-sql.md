---
title: "SET STATISTICS XML (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SET_STATISTICS_XML_TSQL"
  - "SET STATISTICS XML"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statistical information [SQL Server], statement processing"
  - "STATISTICS XML option"
  - "SET STATISTICS XML statement"
  - "statements [SQL Server], statistical information"
  - "XML [SQL Server], statement execution information"
ms.assetid: 2b6d4c5a-a7f5-4dd1-b10a-7632265b1af7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SET STATISTICS XML (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Causes Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and generate detailed information about how the statements were executed in the form of a well-defined XML document.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET STATISTICS XML { ON | OFF }  
```  
  
## Remarks  
 The setting of SET STATISTICS XML is set at execute or run time and not at parse time.  
  
 When SET STATISTICS XML is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns execution information for each statement after executing it. After this option is set ON, information about all subsequent [!INCLUDE[tsql](../../includes/tsql-md.md)] statements is returned until the option is set to OFF. Note that SET STATISTICS XML need not be the only statement in a batch.  
  
 SET STATISTICS XML returns output as **nvarchar(max)** for applications, such as the **sqlcmd** utility, where the XML output is subsequently used by other tools to display and process the query plan information.  
  
 SET STATISTICS XML returns information as a set of XML documents. Each statement after the SET STATISTICS XML ON statement is reflected in the output by a single document. Each document contains the text of the statement, followed by the details of the execution steps. The output shows run-time information such as the costs, accessed indexes, and types of operations performed, join order, the number of times a physical operation is performed, the number of rows each physical operator produced, and more.  
  
 The document containing the XML schema for the XML output by SET STATISTICS XML is copied during setup to a local directory on the computer on which Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. It can be found on the drive containing the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation files, at:  
  
 \Microsoft SQL Server\100\Tools\Binn\schemas\sqlserver\2004\07\showplan\showplanxml.xsd  
  
 The Showplan Schema can also be found at [this Web site](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409).  
  
 SET STATISTICS PROFILE and SET STATISTICS XML are counterparts of each other. The former produces textual output; the latter produces XML output. In future versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], new query execution plan information will only be displayed through the SET STATISTICS XML statement, not the SET STATISTICS PROFILE statement.  
  
> [!NOTE]  
>  If **Include Actual Execution Plan** is selected in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], this SET option does not produce XML Showplan output. Clear the **Include Actual Execution Plan** button before using this SET option.  
  
## Permissions  
 To use SET STATISTICS XML and view the output, users must have the following permissions:  
  
-   Appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   SHOWPLAN permission on all databases containing objects that are referenced by the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
 For [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that do not produce STATISTICS XML result sets, only the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are required. For [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that do produce STATISTICS XML result sets, checks for both the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement execution permission and the SHOWPLAN permission must succeed, or the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement execution is aborted and no Showplan information is generated.  
  
## Examples  
 The two statements that follow use the SET STATISTICS XML settings to show the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] analyzes and optimizes the use of indexes in queries. The first query uses the Equals (=) comparison operator in the WHERE clause on an indexed column. The second query uses the LIKE operator in the WHERE clause. This forces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use a clustered index scan to find the data that satisfies the WHERE clause condition. The values in the **EstimateRows** and the **EstimatedTotalSubtreeCost** attributes are smaller for the first indexed query indicating that it was processed much faster and used fewer resources than the nonindexed query.  
  
```  
USE AdventureWorks2012;  
GO  
SET STATISTICS XML ON;  
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
SET STATISTICS XML OFF;  
GO  
```  
  
## See Also  
 [SET SHOWPLAN_XML &#40;Transact-SQL&#41;](../../t-sql/statements/set-showplan-xml-transact-sql.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)  
  
  
