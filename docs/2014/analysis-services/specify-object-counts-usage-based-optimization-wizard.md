---
title: "Specify Object Counts (Usage-Based Optimization Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.storagedesignwizard.calculatingobjectcounts.f1"
ms.assetid: 306c7c25-ae24-4852-ab8c-c82f68a4bc1f
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify Object Counts (Usage-Based Optimization Wizard)
  Use the **Specify Object Counts** page to automatically calculate the count of objects in the cube or to manually enter estimated counts. The Usage-Based Optimization Wizard uses the object counts to estimate storage requirements.  
  
## Options  
 **Cube Objects**  
 Displays the dimensions and attributes in the cube. Only the attributes that do not have their `AggregationUsage` property set to None in the **Review Aggregation Usage** page of the wizard are shown because those are the only attributes that need counts specified.  
  
 **Estimated count**  
 Displays the estimated number of rows in the measure group and the estimated attribute member counts in the database dimensions. You can type a value to use as the estimated count or you can calculate the estimated count values. To calculate the count values, type 0 in the field and then click **Count**. Fields that already display a count are not updated.  
  
 **Partition count**  
 (Optional) Type the estimated number of rows in the measure group and the estimated attribute member counts in the partitions.  
  
 **Count**  
 Calculates and repopulates the values in the **Estimated count** column for all empty fields. Fields that already display a count are not updated.  
  
## See Also  
 [Aggregation Design Wizard F1 Help](aggregation-design-wizard-f1-help.md)   
 [Analysis Services Wizards &#40;Multidimensional Data&#41;](analysis-services-wizards-multidimensional-data.md)  
  
  
