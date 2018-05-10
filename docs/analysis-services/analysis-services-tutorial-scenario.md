---
title: "Analysis Services Tutorial Scenario | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Analysis Services Tutorial Scenario
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]
This tutorial is based on [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)], a fictitious company. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is a large, multinational manufacturing company that produces and distributes metal and composite bicycles to commercial markets in North America, Europe, and Asia. The headquarters for [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is Bothell, Washington, where the company employs 500 workers. Additionally, [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] employs several regional sales teams throughout its market base.  
  
In recent years, [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] bought a small manufacturing plant, Importadores Neptuno, which is located in Mexico. Importadores Neptuno manufactures several critical subcomponents for the [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] product line. These subcomponents are shipped to the Bothell location for final product assembly. In 2005, Importadores Neptuno became the sole manufacturer and distributor of the touring bicycle product group.  
  
Following a successful fiscal year, [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] now wants to broaden its market share by targeting advertising to its best customers, extending product availability through an external Web site, and reducing the cost of sales by reducing production costs.  
  
## Current Analysis Environment  
To support the data analysis needs of the sales and marketing teams and of senior management, the company currently takes transactional data from the [!INCLUDE[ssSampleDBnormal](../includes/sssampledbnormal-md.md)] database, and non-transactional information such as sales quotas from spreadsheets, and consolidates this information into the **AdventureWorksDW2012** relational data warehouse. However, the relational data warehouse presents the following challenges:  
  
-   Reports are static. Users have no way to interactively explore the data in the reports to obtain more detailed information, such as they could do with a [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Excel pivot table. Although the existing set of predefined reports is sufficient for many users, more advanced users need direct query access to the database for interactive queries and specialized reports. However, because of the complexity of the **AdventureWorksDW2012** database, too much time is needed for such users to master how to create effective queries.  
  
-   Query performance is widely variable. For example, some queries return results very quickly, in only a few seconds, while other queries take several minutes to return.  
  
-   Aggregate tables are difficult to manage. In an attempt to improve query response times, the data warehouse team at [!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] built several aggregate tables in the **AdventureWorksDW2012** database. For example, they built a table that summarizes sales by month. However, while these aggregate tables greatly improve query performance, the infrastructure that they built to maintain the tables over time is fragile and prone to errors.  
  
-   Complex calculation logic is buried in report definitions and is difficult to share between reports. Because this business logic is generated separately for each report, summary information sometimes is different between reports. Therefore, management has limited confidence in the data warehouse reports.  
  
-   Users in different business units are interested in different views of the data. Each group is distracted and confused by data elements that are irrelevant to them.  
  
-   Calculation logic is particularly challenging for users who need specialized reports. Because such users must define the calculation logic separately for each report, there is no centralized control over how the calculation logic is defined. For example, some users know that they should use basic statistical techniques such as moving averages, but they do not know how to construct such calculations and so do not use these techniques.  
  
-   It is difficult to combine related sets of information. Specialized queries that combine two sets of related information, such as sales and sales quotas, are difficult for business users to construct. Such queries overwhelmed the database, so the company requires that users request cross-subject-area sets of data from the data warehouse team. As a result, only a handful of predefined reports have been defined that combine data from multiple subject areas. Additionally, users are reluctant to try to modify these reports because of their complexity.  
  
-   Reports are focused primarily on business information in the United States. Users in the non-U.S. subsidiaries are very dissatisfied with this focus, and want to be able to view reports in different currencies and different languages.  
  
-   Information is difficult to audit. The Finance department currently uses the **AdventureWorksDW2012** database only as a source of data from which to query in bulk. They then download the data into individual spreadsheets, and spend significant time preparing the data and manipulating the spreadsheets. The corporate financial reports are therefore difficult to prepare, audit, and manage across the company.  
  
## The Solution  
The data warehouse team recently performed a design review of the current analysis system. The review included a gap analysis of current issues and future demands. The data warehouse team determined that the **AdventureWorksDW2012** database is a well-designed dimensional database with conformed dimensions and surrogate keys. Conformed dimensions enable a dimension to be used in multiple data marts, such as a time dimension or a product dimension. Surrogate keys are artificial keys that link dimension and fact tables and that are used to ensure uniqueness and to improve performance. Moreover, the data warehouse team determined that there currently are no significant problems with the loading and management of the base tables in the **AdventureWorksDW2012** database. The team has therefore decided to use [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to accomplish the following:  
  
-   Provide unified data access through a common metadata layer for analytical analysis and reporting.  
  
-   Simplify users' view of data, speeding the development of both interactive and predefined queries and predefined reports.  
  
-   Correctly construct queries that combine data from multiple subject areas.  
  
-   Manage aggregates.  
  
-   Store and reuse complex calculations.  
  
-   Present a localized experience to business users outside the United States.  
  
The lessons in the Analysis Services tutorial provide guidance in building a cube database that meets all of these goals. To get started, continue to the first lesson: [Lesson 1: Create a New Tabular Model Project](../analysis-services/lesson-1-create-a-new-tabular-model-project.md).  
  
## See Also  
[Multidimensional Modeling &#40;Adventure Works Tutorial&#41;](../analysis-services/multidimensional-modeling-adventure-works-tutorial.md)  
  
  
  
