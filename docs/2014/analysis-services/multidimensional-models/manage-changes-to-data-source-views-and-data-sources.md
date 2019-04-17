---
title: "Manage Changes to Data Source Views and Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying data sources"
  - "modifying data source views"
  - "data source views [Analysis Services], schema updates"
  - "data sources [Analysis Services], schema updates"
ms.assetid: 928c9f63-365a-43fd-9bbd-78828cc7e54d
author: minewiskan
ms.author: owend
manager: craigg
---
# Manage Changes to Data Source Views and Data Sources
  When the Schema Generation Wizard is rerun, it reuses the same data source and data source view that it used for the original generation. If you add a data source or a data source view, the wizard does not use it. If you delete the original data source or data source view after the initial generation, you must run the wizard from the beginning. All previous settings in the wizard are also deleted. Any existing objects in an underlying database that were bound to a deleted data source or data source view are treated as user-created objects the next time you run the Schema Generation Wizard.  
  
 If the data source view does not reflect the actual state of the underlying database at the time of generation, the Schema Generation Wizard may encounter errors when it generates the schema for the subject area database. For example, if the data source view specifies that the data type for a column is **int**, but data type for the column is actually **string**, the Schema Generation Wizard sets the data type for the foreign key to **INT** to match the data source view and then fails when it creates the relationship because the actual type is **string**.  
  
 On the other hand, if you change the data source connection string to a different database from the previous generation, no error is generated. The new database is used, and no change is made to the previous database.  
  
## See Also  
 [Understanding Incremental Generation](understanding-incremental-generation.md)  
  
  
