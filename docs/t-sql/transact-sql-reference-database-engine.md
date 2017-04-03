---
title: "Transact-SQL Reference (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "04/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sql13.tsqlref.f1"
  - "devlang-tsql"
helpviewer_keywords: 
  - "Transact-SQL"
ms.assetid: dbba47d7-e08e-4435-b876-35dced1f325d
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Transact-SQL Reference (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

  [!INCLUDE[tsql](../includes/tsql-md.md)] is central to using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. All applications that communicate with an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] do so by sending [!INCLUDE[tsql](../includes/tsql-md.md)] statements to the server, regardless of the user interface of the application.  
  
 The following is a list of the kinds of applications that can generate [!INCLUDE[tsql](../includes/tsql-md.md)]:  
  
-   General office productivity applications.  
  
-   Applications that use a graphical user interface (GUI) to let users select the tables and columns from which they want to see data. This includes Microsoft development tools such as [SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx) and [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt).  
  
-   Applications that use general language sentences to determine what data a user wants to see.  
  
-   Line of business applications that store their data in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases. These applications can include both applications written by vendors and applications written in-house.  
  
-   [!INCLUDE[tsql](../includes/tsql-md.md)] scripts that are run by using utilities such as [sqlcmd](../tools/sqlcmd-utility).  
  
-   Applications created by using development systems such as [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vcprvc](../includes/vcprvc-md.md)], [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vbprvb](../includes/vbprvb-md.md)], or [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual J++ that use database APIs such as ADO, OLE DB, and ODBC.  
  
-   Web pages that extract data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases.  
  
-   Distributed database systems from which data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is replicated to various databases, or distributed queries are executed.  
  
-   Data warehouses in which data is extracted from online transaction processing (OLTP) systems and summarized for decision-support analysis.  
  
 **To view the Transact-SQL reference topics**  
  
 To find [!INCLUDE[tsql](../includes/tsql-md.md)] topics, use search at the top right of this page, or use the table of contents on the left side of the page. You can also type a [!INCLUDE[tsql](../includes/tsql-md.md)] key word in the Management Studio Query Editor window, and press F1. 
  
System tables, views, functions, and procedures, are in the [Database Features](../relational-databases/database-features) section.  
  
 For a short tutorial about how to write [!INCLUDE[tsql](../includes/tsql-md.md)], see [Tutorial: Writing Transact-SQL Statements](../t-sql/tutorial-writing-transact-sql-statements.md).  
  
 For help with [!INCLUDE[tsql](../includes/tsql-md.md)] statements, see [MSDN Transact-SQL Forum](http://social.msdn.microsoft.com/Forums/en-US/home?forum=transactsql).  
  
## "Applies to" References  
 The [!INCLUDE[tsql](../includes/tsql-md.md)] reference includes topics related multiple versions of SQL Server, beginning with [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], and related to [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)], Azure SQL Data Warehouse, and Parallel Data Warehouse. Near the top of each topic is a section indicating which products support the subject of the topic. If a product is omitted, then the feature described by the topic is not available in that product. For example, availability groups were introduced in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. The **CREATE AVAILABILTY GROUP** topic indicates it applies to **SQL Server (SQL Server 2012 through current version)** because it does not apply to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)].  
  
 In some cases, the general subject of topic can be used in a product, but all of the arguments are not supported. For example, contained database users were introduced in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. The **CREATE USER** statement can be used in any [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] product, however the **WITH PASSWORD** syntax cannot be used with older versions. In this case, additional **Applies to** sections are inserted into the appropriate argument descriptions in the body of the topic.  
  
## See Also  
 [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)   
 [SQL Server Database Engine](../database-engine/configure-windows/sql-server-database-engine.md)  
  
  
