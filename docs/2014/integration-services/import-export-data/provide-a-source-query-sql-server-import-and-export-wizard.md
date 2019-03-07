---
title: "Provide a Source Query (SQL Server Import and Export Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.impexpwizard.providesourcequery.f1"
ms.assetid: c8cbd07e-b9c3-422f-94b8-d6fc8cf31cf5
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Provide a Source Query (SQL Server Import and Export Wizard)
  Use the **Provide a Source Query** page to type the SQL statement that will generate the data to copy from the data source to the destination.  
  
 To learn more about this wizard, see [SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md). To learn about the options for starting the wizard, as well as the permissions required to run the wizard successfully, see [Run the SQL Server Import and Export Wizard](start-the-sql-server-import-and-export-wizard.md).  
  
 The purpose of the SQL Server Import and Export Wizard is to copy data from a source to a destination. The wizard can also create a destination database and destination tables for you. However, if you have to copy multiple databases or tables, or other kinds of database objects, you should use the Copy Database Wizard instead. For more information, see [Use the Copy Database Wizard](../../relational-databases/databases/use-the-copy-database-wizard.md).  
  
## Options  
 **SQL statement**  
 Type a query statement to retrieve selected rows of data from the source database. For example, the following query statement retrieves the **SalesPersonID**, **SalesQuota**, and **SalesYTD** from the AdventureWorks database for sales persons whose commission percentage is more than 1.5 percent.  
  
```  
SELECT SalesPersonID, SalesQuota, SalesYTD  
FROM Sales.SalesPerson  
WHERE CommissionPct > 0.015  
```  
  
 **Parse**  
 Checks the syntax of the SQL statement in the **SQL statement** text box.  
  
> [!NOTE]  
>  If the time that is required to check the syntax of the statement exceeds the time-out value of 30 seconds, parsing stops and generates an error. You will not be able to move past this page of the wizard until parsing succeeds. One solution is to create a database view based on the query, and to query the view from the wizard, instead of entering the query text directly.  
  
 **Browse**  
 Select a file containing a SQL statement by using the **Open** dialog box. Selecting a file copies the text from the file into the **Query statement** text box.  
  
  
