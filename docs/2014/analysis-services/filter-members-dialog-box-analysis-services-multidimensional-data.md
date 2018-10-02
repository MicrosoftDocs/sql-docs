---
title: "Filter Members Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensiondesigner.filtermembers.f1"
helpviewer_keywords: 
  - "Filter Members dialog box"
ms.assetid: 52c6da1d-9fb5-4dbc-bffa-248d11cd337c
author: minewiskan
ms.author: owend
manager: craigg
---
# Filter Members Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Filter Members** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to filter dimension members by member caption, member name, member unique name, key column value, or value column value for the current level while browsing a dimension in the **Browser** tab of **Dimension Designer**.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Filter expression**|Displays a grid of properties, operators, and values used to construct a filter expression. Note that after a row is added, it cannot be removed. You must close and reopen the dialog box to specify a new filter expression. The grid contains the following columns:<br /><br /> **Property**: Select the property of the member to use for the filter expression.<br /><br /> **Operator**: Select the operator to use for the filter expression.<br /><br /> **Value**: Type the value of the property selected in **Property** to evaluate using the operator specified in **Operator**.|  
|**Test pane**|When **Test** is clicked, this pane displays the members returned by the filter expression. If no members are returned using the criteria specified in **Filter expression**, a warning is displayed.|  
|**Test**|Click to test the criteria specified in **Filter expression**.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Browser &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](browser-dimension-designer-analysis-services-multidimensional-data.md)  
  
  
