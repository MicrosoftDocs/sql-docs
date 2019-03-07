---
title: "Review Aggregation Usage (Usage-Based Optimiation Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.storagedesignwizard.reviewaggregationusage.f1"
ms.assetid: 49ce2094-c4dc-4e46-8cef-c17c5db084ca
author: minewiskan
ms.author: owend
manager: craigg
---
# Review Aggregation Usage (Usage-Based Optimiation Wizard)
  Use the **Review Aggregation Usage** page to configure aggregation usage settings.  
  
## Options  
 **Default**  
 Select to set the aggregation usage setting for the attribute to Default. By using this setting, the designer applies a default rule based on the type of attribute and dimension.  
  
 **Full**  
 Select to set the aggregation usage setting for the attribute to Full. By using this setting, every aggregation for the cube must include this attribute or a related attribute that is lower in the attribute chain. The Full aggregation usage setting should be avoided when an attribute contains many members. If specified for multiple attributes or attributes that have many members, this setting might prevent aggregations from being designed because of excessive size.  
  
 **None**  
 Select to set the aggregation usage setting for the attribute to None. By using this setting, no aggregation for the cube can include this attribute.  
  
 **Unrestricted**  
 Select to set the aggregation usage setting for the attribute to Unrestricted. By using this setting, no restrictions are put on the aggregation designer. However, the attribute must still be evaluated to determine whether it is a valuable aggregation candidate.  
  
 **Set All to Default**  
 Select to set the aggregation usage settings for all attributes to Default.  
  
## See Also  
 [Aggregation Design Wizard F1 Help](aggregation-design-wizard-f1-help.md)   
 [Analysis Services Wizards &#40;Multidimensional Data&#41;](analysis-services-wizards-multidimensional-data.md)  
  
  
