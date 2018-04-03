---
title: "Lesson 1: Converting a Table to a Hierarchical Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "HierarchyID"
ms.assetid: 5ee6f19a-6dd7-4730-a91c-bbed1bd77e0b
caps.latest.revision: 18
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# Lesson 1: Converting a Table to a Hierarchical Structure
  Customers who have tables using self joins to express hierarchical relationships can convert their tables to a hierarchical structure using this lesson as a guide. It is relatively easy to migrate from this representation to one using `hierarchyid`. After migration, users will have a compact and easy to understand hierarchical representation, which can be indexed in several ways for efficient queries.  
  
 This lesson, examines an existing table, creates a new table containing a `hierarchyid` column, populates the table with the data from the source table, and then demonstrates three indexing strategies. This lesson contains the following topics:  
  
-   [Examining the Current Structure of the Employee Table](../../2014/tutorials/examining-the-current-structure-of-the-employee-table.md)  
  
-   [Populating a Table with Existing Hierarchical Data](../../2014/tutorials/populating-a-table-with-existing-hierarchical-data.md)  
  
-   [Optimizing the NewOrg Table](../../2014/tutorials/optimizing-the-neworg-table.md)  
  
-   [Summary: Converting a Table to a Hierarchical Structure](../../2014/tutorials/summary-converting-a-table-to-a-hierarchical-structure.md)  
  
## Prerequisites  
 This lesson requires the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] sample database.  
  
## Next Task in Lesson  
 [Examining the Current Structure of the Employee Table](../../2014/tutorials/examining-the-current-structure-of-the-employee-table.md)  
  
## Next Lesson  
 [Lesson 2: Creating and Managing Data in a Hierarchical Table](../../2014/tutorials/lesson-2-creating-and-managing-data-in-a-hierarchical-table.md)  
  
  