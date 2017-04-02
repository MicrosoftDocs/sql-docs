---
title: "Language Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
ms.assetid: 39da8062-36df-4ca0-b68b-49c3bcf298ca
caps.latest.revision: 18
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Language Reference
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-_md](../includes/tsql-appliesto-ss2008-asdb-asdw-pdw-md.md)]

  Microsoft SQL Shared Language Reference contains language reference content for the following areas.  
  
 [Transact-SQL Reference &#40;Database Engine&#41;](../t-sql/transact-sql-reference-database-engine.md)  
  
 [XQuery Language Reference &#40;SQL Server&#41;](../xquery/xquery-language-reference-sql-server.md)  
  
 [Integration Services Language Reference](../integration-services/integration-services-language-reference.md)  
  
 [Replication Language Reference](../relational-databases/replication/replication-language-reference.md)  
  
 [Analysis Services Language Reference](../mdx/analysis-services-language-reference.md)  
  
## Version and platform-inclusive documentation  
At the top of each [!INCLUDE[tsql](../includes/tsql-md.md)] topic in this SQL Shared Language Reference library you can see to which versions and platforms of SQL the function applies.
  
 By using this information you can easily identify the [!INCLUDE[tsql](../includes/tsql-md.md)] statements, commands, and system objects that apply to the versions and platforms for which you develop applications or administer and support. In addition, the consolidated topics provide a single "master" content set that will be maintained for all of the specified versions and platforms. That is, you can rely on this single content set for the most accurate and up-to-date product information regardless of version or platform.  
  
## Identifying Version and platform applicability  
 Version and platform applicability is provided in the top section of each topic in a single "applies to" statement. This statement identifies the applicable versions and platforms for the topic as a whole. The following examples demonstrate this statement.  
  
 **Example A**. Indicates that the topic applies to all the SQLs!  [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], to both the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] platforms, and to Azure SQL Data Warehouse and Parallel Data Warehouse.
 
 ![Applies to all](../t-sql/media/applies-to-all.png)
  
 **Example B**. Indicates that the topic applies only to SQL Server versions 2008-2016, and to only the SQL Server platform. 
  
 ![SQL Server from 2008 only](../t-sql/media/sql-server-from-2008-only.png)
    
## Exceptions  
 For most of the [!INCLUDE[tsql](../includes/tsql-md.md)] topics, the applies to banner statement is all that is required to identify version and platform applicability. However, some statements and system objects have changed over time to support new features in the product. These additions do not apply to the older versions. For example, the statement [ORDER BY Clause](../t-sql/queries/select-order-by-clause-transact-sql.md) applies to all SQL Server versions and to SQL Database, but a new `OFFSET…FETCH` argument was added in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] that applies only to that version and higher versions and to SQL Database. Such exceptions to the top-level applies to statement are identified in the **Arguments** section of the topic in the definition of the new syntax. For example, in the `ORDER BY` topic, the following statement is added to the definitions of `OFFSET` and `FETCH`. This statement indicates that these keywords cannot be used in versions of SQL Server earlier than [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
**Applies to**: [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)].  
  
 Exceptions to the top-level applies to statement can also occur when the topic is applicable to both the SQL Server and [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] but only some of the syntax is supported by [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)]. For example, the [DROP INDEX](../t-sql/statements/drop-index-transact-sql.md) statement applies to both platforms, however, [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] does not support all of the options and clauses that SQL Server provides. These exceptions are identified in the Arguments section. For example, in the definition of `MAXDOP`, the following statement is added to specify the versions and platforms that can specify this option.  
  
**Applies to**: [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
 Exceptions to the top-level applies to statement can also occur in system objects such as catalog views. For these objects, new columns can be added or new values returned by existing columns. When a new column is added, the version/platform applicability is included in the definition of the column. When new values are returned by an existing column, the applicability of the new values are identified in the description of the values. For example, the values returned in the **type** column of the [sys.indexes](../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view have changed from release to release. In the definition of that column, the applicability of the values are defined for each value that does not conform to the top-level "applies to" statement.  
  
  
