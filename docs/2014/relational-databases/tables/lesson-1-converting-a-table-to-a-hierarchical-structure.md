---
title: "Lesson 1: Converting a Table to a Hierarchical Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "HierarchyID"
ms.assetid: 5ee6f19a-6dd7-4730-a91c-bbed1bd77e0b
author: stevestein
ms.author: sstein
manager: craigg
---
# Lesson 1: Converting a Table to a Hierarchical Structure
  Customers who have tables using self joins to express hierarchical relationships can convert their tables to a hierarchical structure using this lesson as a guide. It is relatively easy to migrate from this representation to one using `hierarchyid`. After migration, users will have a compact and easy to understand hierarchical representation, which can be indexed in several ways for efficient queries.  
  
 This lesson, examines an existing table, creates a new table containing a `hierarchyid` column, populates the table with the data from the source table, and then demonstrates three indexing strategies. This lesson contains the following topics:  
  
-   [Examining the Current Structure of the Employee Table](lesson-1-1-examining-the-current-structure-of-the-employee-table.md)  
  
-   [Populating a Table with Existing Hierarchical Data](lesson-1-2-populating-a-table-with-existing-hierarchical-data.md)  
  
-   [Optimizing the NewOrg Table](lesson-1-3-optimizing-the-neworg-table.md)  
  
-   [Summary: Converting a Table to a Hierarchical Structure](lesson-1-4-summary-converting-a-table-to-a-hierarchical-structure.md)  
  
## Prerequisites  
 This lesson requires the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
## Next Task in Lesson  
 [Examining the Current Structure of the Employee Table](lesson-1-1-examining-the-current-structure-of-the-employee-table.md)  
  
## Next Lesson  
 [Lesson 2: Creating and Managing Data in a Hierarchical Table](lesson-2-creating-and-managing-data-in-a-hierarchical-table.md)  
  
  
