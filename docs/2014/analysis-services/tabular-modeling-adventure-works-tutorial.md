---
title: "Tabular Modeling (Adventure Works Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 140d0b43-9455-4907-9827-16564a904268
author: minewiskan
ms.author: owend
manager: craigg
---
# Tabular Modeling (Adventure Works Tutorial)
  This tutorial provides lessons on how to create a [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Analysis Services tabular model by using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## What You Will Learn  
 During the course of this tutorial, you will learn the following:  
  
-   How to create a new tabular model project in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)].  
  
-   How to import data from a SQL Server relational database into a tabular model project.  
  
-   How to create and manage relationships between tables in the model.  
  
-   How to create and manage calculations, measures, and Key Performance Indicators that help users analyze model data.  
  
-   How to create and manage perspectives and hierarchies that help users more easily browse model data by providing business and application specific viewpoints.  
  
-   How to create partitions that divide table data into smaller logical parts that can be processed independent from other partitions.  
  
-   How to secure model objects and data by creating roles with user members.  
  
-   How to deploy a tabular model to a sandbox or production instance of Analysis Services running in Tabular mode.  
  
## Tutorial Scenario  
 This tutorial is based on [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)], a fictitious company. [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is a large, multinational manufacturing company that produces and distributes metal and composite bicycles to commercial markets in North America, Europe, and Asia. The headquarters for [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] is in Bothell, Washington, where the company employs 500 workers. Additionally, [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] employs several regional sales teams throughout its market base.  
  
 To better support the data analysis needs of sales and marketing teams and of senior management, you are tasked with creating a tabular model for users to analyze internet sales data in the AdventureWorksDW sample database.  
  
 In order to complete the tutorial, and the Adventure Works Internet Sales tabular model, you must complete a number of lessons. Within each lesson are a number of tasks; completing each task in order is necessary for completing the lesson. While in a particular lesson there may be several tasks that accomplish a similar outcome; however, how you complete each task is slightly different. This is to show that there is often more than one way to complete a particular task, and to challenge you by using skills you learned in previous tasks.  
  
 The purpose of the lessons is to guide you through authoring a basic tabular model running in In-Memory mode by using many of the features included in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. Because each lesson builds upon the previous lesson, you should complete the lessons in order. Once you have completed all of the lessons, you will have authored and deployed the Adventure Works Internet Sales sample tabular model on an Analysis Services server.  
  
> [!NOTE]  
>  This tutorial does not provide lessons or information about managing a deployed tabular model database by using SQL Server Management Studio, or using a reporting client application to connect to a deployed model to browse model data.  
  
## Prerequisites  
 In order to complete this tutorial, you must have the following prerequisites installed:  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Analysis Services instance running in Tabular mode.  
  
-   [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
-   AdventureWorksDW sample database. This sample database includes the data necessary to complete this tutorial. To download the sample database, see [https://go.microsoft.com/fwlink/?LinkID=335807](https://go.microsoft.com/fwlink/?LinkID=335807).  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel 2003 or later (for use with the Analyze in Excel feature in lesson 11)  
  
## Lessons  
 This tutorial includes the following lessons:  
  
|Lesson|Estimated time to complete|  
|------------|--------------------------------|  
|[Lesson 1: Create a New Tabular Model Project](lesson-1-create-a-new-tabular-model-project.md)|10 minutes|  
|[Lesson 2: Add Data](lesson-2-add-data.md)|20 minutes|  
|[Lesson 3: Rename Columns](rename-columns.md)|20 minutes|  
|[Lesson 4: Mark as Date Table](lesson-3-mark-as-date-table.md)|3 minutes|  
|[Lesson 5: Create Relationships](lesson-4-create-relationships.md)|10 minutes|  
|[Lesson 6: Create Calculated Columns](lesson-5-create-calculated-columns.md)|15 minutes|  
|[Lesson 7: Create Measures](lesson-6-create-measures.md)|30 minutes|  
|[Lesson 8: Create Key Performance Indicators](lesson-7-create-key-performance-indicators.md)|15 minutes|  
|[Lesson 9: Create Perspectives](lesson-8-create-perspectives.md)|5 minutes|  
|[Lesson 10: Create Hierarchies](lesson-9-create-hierarchies.md)|20 minutes|  
|[Lesson 11: Create Partitions](lesson-10-create-partitions.md)|15 minutes|  
|[Lesson 12: Create Roles](lesson-11-create-roles.md)|15 minutes|  
|[Lesson 13: Analyze in Excel](lesson-12-analyze-in-excel.md)|20 minutes|  
|[Lesson 14: Deploy](lesson-13-deploy.md)|5 minutes|  
  
## Supplemental Lessons  
 This tutorial also includes [Supplemental Lessons](../tutorials/supplemental-lessons.md). Topics in this section are not required to complete the tutorial, but can be helpful in better understanding advanced tabular model authoring features.  
  
 This tutorial includes the following supplemental lessons:  
  
|Lesson|Estimated time to complete|  
|------------|--------------------------------|  
|[Implement Dynamic Security by Using Row Filters](../tutorials/implement-dynamic-security-by-using-row-filters.md)|30 minutes|  
|[Configure Reporting Properties for Power View Reports](supplemental-lesson-configure-reporting-properties-for-power-view-reports.md)Configure Reporting Properties for Power View Reports|30 minutes|  
  
## Next Step  
 To begin the tutorial, continue to the first lesson: [Lesson 1: Create a New Tabular Model Project](lesson-1-create-a-new-tabular-model-project.md).  
  
  
