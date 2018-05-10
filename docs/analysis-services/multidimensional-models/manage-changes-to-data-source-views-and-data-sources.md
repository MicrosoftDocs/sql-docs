---
title: "Manage Changes to Data Source Views and Data Sources | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Manage Changes to Data Source Views and Data Sources
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When the Schema Generation Wizard is rerun, it reuses the same data source and data source view that it used for the original generation. If you add a data source or a data source view, the wizard does not use it. If you delete the original data source or data source view after the initial generation, you must run the wizard from the beginning. All previous settings in the wizard are also deleted. Any existing objects in an underlying database that were bound to a deleted data source or data source view are treated as user-created objects the next time you run the Schema Generation Wizard.  
  
 If the data source view does not reflect the actual state of the underlying database at the time of generation, the Schema Generation Wizard may encounter errors when it generates the schema for the subject area database. For example, if the data source view specifies that the data type for a column is **int**, but data type for the column is actually **string**, the Schema Generation Wizard sets the data type for the foreign key to **INT** to match the data source view and then fails when it creates the relationship because the actual type is **string**.  
  
 On the other hand, if you change the data source connection string to a different database from the previous generation, no error is generated. The new database is used, and no change is made to the previous database.  
  
## See Also  
 [Understanding Incremental Generation](../../analysis-services/multidimensional-models/understanding-incremental-generation.md)  
  
  
