---
title: "Tabular Modeling (Adventure Works Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
keywords: 
  - "Analysis Services"
  - "Tabular Model"
  - "Tutorial"
  - "SSAS"
ms.assetid: 140d0b43-9455-4907-9827-16564a904268
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Tabular Modeling (Adventure Works Tutorial)
[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

This tutorial provides lessons on how to create an Analysis Services tabular model at the [1200 compatibility level](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md) by using [SQL Server Data Tools (SSDT)](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt), and deploy your model to an Analysis Services server on-premises or in Azure.  
  
  
## What you'll learn   
  
-   How to create a new tabular model project in SSDT.
  
-   How to import data from a SQL Server relational database into a tabular model project.  
  
-   How to create and manage relationships between tables in the model.  
  
-   How to create and manage calculations, measures, and Key Performance Indicators that help users analyze model data.  
  
-   How to create and manage perspectives and hierarchies that help users more easily browse model data by providing business and application specific viewpoints.  
  
-   How to create partitions that divide table data into smaller logical parts that can be processed independent from other partitions.  
  
-   How to secure model objects and data by creating roles with user members.  
  
-   How to deploy a tabular model to an Analysis Services server on-premises or in Azure.  
  
## Scenario  
This tutorial is based on Adventure Works Cycles, a fictitious company. Adventure Works is a large, multinational manufacturing company that produces and distributes metal and composite bicycles to commercial markets in North America, Europe, and Asia. With headquarters in Bothell, Washington, the company employs 500 workers. Additionally, Adventure Works employs several regional sales teams throughout its market base.  
  
To better support the data analysis needs of sales and marketing teams and of senior management, you are tasked with creating a tabular model for users to analyze Internet sales data in the AdventureWorksDW sample database.  
  
In order to complete the tutorial, and the Adventure Works Internet Sales tabular model, you must complete a number of lessons. Within each lesson are a number of tasks; completing each task in order is necessary for completing the lesson. While in a particular lesson there may be several tasks that accomplish a similar outcome, but how you complete each task is slightly different. This is to show that there is often more than one way to complete a particular task, and to challenge you by using skills you've learned in previous tasks.  
  
The purpose of the lessons is to guide you through authoring a basic tabular model running in In-Memory mode by using many of the features included in SSDT. Because each lesson builds upon the previous lesson, you should complete the lessons in order. Once you've completed all of the lessons, you will have authored and deployed the Adventure Works Internet Sales sample tabular model on an Analysis Services server.  
  
This tutorial does not provide lessons or information about managing a deployed tabular model database by using SQL Server Management Studio, or using a reporting client application to connect to a deployed model to browse model data.  
  
## Prerequisites  
In order to complete this tutorial, you'll need the following prerequisites:  
  
-   The latest version of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. [Get the latest version](https://msdn.microsoft.com/library/mt204009.aspx).

-   The latest version of SQL Server Management Studio. [Get the latest version](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms). 
  
-   A client application such as [Power BI Desktop](https://powerbi.microsoft.com/desktop/) or [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel.    
  
-   A SQL Server instance with the Adventure Works DW 2014 sample database. This sample database includes the data necessary to complete this tutorial. [Get the latest version](http://go.microsoft.com/fwlink/?LinkID=335807).  
  

-   An Azure Analysis Services or SQL Server 2016 or later Analysis Services instance to deploy your model to. [Sign up for a free Azure Analysis Services trial](https://azure.microsoft.com/services/analysis-services/).
  
## Lessons  
This tutorial includes the following lessons:  
  
|Lesson|Estimated time to complete|  
|----------|------------------------------|  
|[Lesson 1: Create a New Tabular Model Project](../analysis-services/lesson-1-create-a-new-tabular-model-project.md)|10 minutes|  
|[Lesson 2: Add Data](../analysis-services/lesson-2-add-data.md)|20 minutes|  
|[Lesson 3: Mark as Date Table](../analysis-services/lesson-3-mark-as-date-table.md)|3 minutes|  
|[Lesson 4: Create Relationships](../analysis-services/lesson-4-create-relationships.md)|10 minutes|  
|[Lesson 5: Create Calculated Columns](../analysis-services/lesson-5-create-calculated-columns.md)|15 minutes|
|[Lesson 6: Create Measures](../analysis-services/lesson-6-create-measures.md)|30 minutes|  
|[Lesson 7: Create Key Performance Indicators](../analysis-services/lesson-7-create-key-performance-indicators.md)|15 minutes|  
|[Lesson 8: Create Perspectives](../analysis-services/lesson-8-create-perspectives.md)|5 minutes|  
|[Lesson 9: Create Hierarchies](../analysis-services/lesson-9-create-hierarchies.md)|20 minutes|  
|[Lesson 10: Create Partitions](../analysis-services/lesson-10-create-partitions.md)|15 minutes|  
|[Lesson 11: Create Roles](../analysis-services/lesson-11-create-roles.md)|15 minutes|  
|[Lesson 12: Analyze in Excel](../analysis-services/lesson-12-analyze-in-excel.md)|20 minutes| 
|[Lesson 13: Deploy](../analysis-services/lesson-13-deploy.md)|5 minutes|  
  
## Supplemental lessons  
This tutorial also includes [Supplemental Lessons](http://msdn.microsoft.com/library/2018456f-b4a6-496c-89fb-043c62d8b82e). Topics in this section are not required to complete the tutorial, but can be helpful in better understanding advanced tabular model authoring features.  
  
|Lesson|Estimated time to complete|  
|----------|------------------------------|  
|[Implement Dynamic Security by Using Row Filters](../analysis-services/supplemental-lesson-implement-dynamic-security-by-using-row-filters.md)|30 minutes|  

  
## Next step  
To begin the tutorial, continue to the first lesson: [Lesson 1: Create a New Tabular Model Project](../analysis-services/lesson-1-create-a-new-tabular-model-project.md).  
  
  
  

