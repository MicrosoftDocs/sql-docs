---
title: "Design the Query | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptwizard.designquery.f1"
ms.assetid: 2dad800f-10a1-453c-8761-2935b9826d84
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Design the Query
  Use this page of the Report Wizard to create a query by typing the query manually, by using Query Builder to interactively build a query, or by importing a query from another report.  
  
 The data source type you chose on the Select the Data Source page, a previous page in the Report Wizard, determines the query you can enter on this page. For example, if the data source type is [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can enter [!INCLUDE[tsql](../includes/tsql-md.md)] statements or stored procedure names. If the data source type is [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the Query pane is disabled and you cannot enter a query directly. You can specify the query by using Query Builder.  
  
## Options  
 **Query string**  
 Type a query that retrieves the data you want to use in your report.  
  
 **Query Builder**  
 Click **Query Builder** to open a query designer for the data source, or to import a query from another report.  
  
 For more information about query designers, see [Reporting Services Query Designers](../../2014/reporting-services/reporting-services-query-designers.md).  
  
## Example  
 For the data source type **Microsoft SQL Server**, the following query retrieves a list of last names from the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database `Person` table.  
  
```  
SELECT LastName FROM Person.Person;  
```  
  
 For the data source type **Microsoft SQL Server**, the following query runs the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] stored procedure `uspGetEmployeeManagers` for the employee with identification number 1:  
  
```  
EXEC uspgetEmployeeManagers '1';  
```  
  
## See Also  
 [Report Wizard Help](../../2014/reporting-services/report-wizard-help.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md)  
  
  
