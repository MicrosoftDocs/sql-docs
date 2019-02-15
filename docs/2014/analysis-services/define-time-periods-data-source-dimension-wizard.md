---
title: "Define Time Periods (Data Source) (Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensionwizard.timeperioddefinition.f1"
ms.assetid: a5e6b9ff-69fa-4896-a840-de2b3e063ca9
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Time Periods (Data Source) (Dimension Wizard)
  Use the **Define Time Periods** page to define attributes representing time periods in the time dimension with columns in the table specified in the **Select the Dimension Type** page.  
  
> [!NOTE]  
>  This page will appear only if you have selected **Build the dimension using a data source** on the **Dimension Definition** page and **Time dimension** on the **Select the Dimension Type** page.  
  
## Options  
 **Time Property Name**  
 Displays the attribute types used to indicate time periods within time dimensions. For more information about attribute types, see [Type Element &#40;DimensionAttribute&#41; &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/type-element-dimensionattribute-assl).  
  
> [!NOTE]  
>  The `Date` attribute type should be used only for columns with a DateTime data type.  
  
 **Time Table Columns**  
 Lists the columns on which the corresponding attribute types will be based.  
  
 To change the column, click the column, and then select a different column from the list.  
  
## See Also  
 [Dimension Wizard F1 Help](dimension-wizard-f1-help.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)   
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
