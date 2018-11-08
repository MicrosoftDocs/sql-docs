---
title: "Select the Source Cube Dimension (Data Mining Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.dmwizard.selectsourcecube.f1"
ms.assetid: 556e216b-5e21-4160-967d-4c57591fbab4
author: minewiskan
ms.author: owend
manager: craigg
---
# Select the Source Cube Dimension (Data Mining Wizard)
  Use the **Select the Source Cube Dimension** page to select the dimension from the cube that contains the cases you want to analyze. For example, if you are building a model that analyzes the purchasing behavior of customers based on demographics, you would select the Customer dimension, which typically contains a unique record for each customer and various attributes that represent demographics, such as gender, location, or income. Later in the wizard you will have the opportunity to add a table that is related to this case table: for example, you might add a nested table that shows which products the customer has purchased.  
  
> [!NOTE]  
>  This page will appear only if you have selected **From existing cube** on the **Select the Definition Method** page of the wizard.  
  
## Options  
 **Select a Source Cube Dimension**  
 Select the dimension of the cube that will provide the source data for your mining structure.  
  
## Choosing a Dimension  
 Because you can select only one dimension for use in your model, it is important that you understand the cube structure and select the dimension that provides the best information for your model. If you are not sure which dimension to use, it might be helpful to browse the cube and review the fields and the data in them by using Dimension Designer. For more information, see [Browse Dimension Data in Dimension Designer](multidimensional-models/database-dimensions-browse-dimension-data-in-dimension-designer.md).  
  
 If you are unfamiliar with dimensions in general, see [Introduction to Dimensions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md).  
  
 For more information about the type of information that is typically contained in a single dimension, including attributes and measures that might be useful for data mining, see [Dimension Relationships](multidimensional-models-olap-logical-cube-objects/dimension-relationships.md).  
  
 If the dimension that you choose does not contain all of the related attributes that you need to build the data mining model, you might need to modify the dimension. For more information, see [Define Database Dimensions](multidimensional-models/define-database-dimensions.md).  
  
## See Also  
 [Data Mining Wizard F1 Help &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-f1-help-analysis-services-data-mining.md)   
 [Create the Data Mining Structure &#40;Data Mining Wizard&#41;](create-the-data-mining-structure-data-mining-wizard.md)   
 [Select the Case Key &#40;Data Mining Wizard&#41;](select-the-case-key-data-mining-wizard.md)  
  
  
