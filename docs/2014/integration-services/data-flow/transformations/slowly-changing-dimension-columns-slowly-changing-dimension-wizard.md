---
title: "Slowly Changing Dimension Columns (Slowly Changing Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.loaddimwizard.scdsupport.f1"
ms.assetid: 36de99d5-5368-48e0-b876-17e9c6862c6c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Slowly Changing Dimension Columns (Slowly Changing Dimension Wizard)
  Use the **Slowly Changing Dimensions Columns** dialog box to select a change type for each slowly changing dimension column.  
  
 To learn more about this wizard, see [Slowly Changing Dimension Transformation](slowly-changing-dimension-transformation.md).  
  
## Options  
 **Dimension Columns**  
 Select a dimension column from the list.  
  
 **Change Type**  
 Select a **Fixed Attribute**, or select one of the two types of changing attributes. Use **Fixed Attribute** when the value in a column should not change; [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] then treats changes as errors. Use **Changing Attribute** to overwrite existing values with changed values. Use **Historical Attribute** to save changed values in new records, while marking previous records as outdated.  
  
 **Remove**  
 Select a dimension column, and remove it from the list of mapped columns by clicking **Remove**.  
  
## See Also  
 [Configure Outputs Using the Slowly Changing Dimension Wizard](configure-outputs-using-the-slowly-changing-dimension-wizard.md)  
  
  
