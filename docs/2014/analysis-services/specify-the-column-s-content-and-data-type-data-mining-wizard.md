---
title: "Specify the Column&#39;s Content and Data Type (Data Mining Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.dmwizard.specifycontentdatatype.f1"
ms.assetid: 7061f674-e806-46f2-8c15-e260a3c69a17
author: minewiskan
ms.author: owend
manager: craigg
---
# Specify the Column&#39;s Content and Data Type (Data Mining Wizard)
  Use the **Specify the Column's Content and Data Type** page to modify the column and content types that have already been set by the wizard. The wizard uses the data types of the source columns and the capabilities of the selected algorithm to determine the default data and content types for each column.  
  
 **For More Information:** [Data Mining Wizard &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-wizard-analysis-services-data-mining.md), [Create a Relational Mining Structure](data-mining/create-a-relational-mining-structure.md)  
  
## Options  
 **Columns**  
 A list of the columns that are defined in the **Specify the Training Data** page of the wizard.  
  
 **Content Type**  
 The content type that is assigned to each column. Click inside a cell to change the content type. For more information about content types, see [Content Types &#40;Data Mining&#41;](data-mining/content-types-data-mining.md).  
  
 **Data Type**  
 The data types that are assigned to each column. Click inside a cell to change the data type. For more information about data types, see [Data Types &#40;Data Mining&#41;](data-mining/data-types-data-mining.md).  
  
 **Detect**  
 Click to automatically detect the continuous and discrete content types for numeric column. This does not apply to mining structures that are based on OLAP data sources. For OLAP mining structures, the wizard automatically detects content types and chooses a type that is compatible with the selected algorithm.  
  
## See Also  
 [Completing the Wizard &#40;Data Mining Wizard&#41;](completing-the-wizard-data-mining-wizard.md)   
 [Data Mining Wizard F1 Help &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-f1-help-analysis-services-data-mining.md)   
 [Specify the Training Data &#40;Data Mining Wizard&#41;](specify-the-training-data-data-mining-wizard.md)  
  
  
