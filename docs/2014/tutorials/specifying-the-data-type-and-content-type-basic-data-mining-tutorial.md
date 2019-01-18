---
title: "Specifying the Data Type and Content Type (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 72484d27-3ef1-4f16-813c-2f43231fc2da
author: minewiskan
ms.author: owend
manager: craigg
---
# Specifying the Data Type and Content Type (Basic Data Mining Tutorial)
  Now that you have selected which columns to use for building your structure and training your models, make any necessary changes to the default data and content types that are set by the wizard.  
  
#### Review and modify content type and data type for each column  
  
1.  On the **Specify Columns' Content and Data Type** page, click **Detect** to run an algorithm that determines the default data and content types for each column.  
  
2.  Review the entries in the **Content Type** and **Data Type** columns and change them if necessary, to make sure that the settings are the same as those listed in the following table.  
  
     Typically, the wizard will detect numbers and assign an appropriate numeric data type, but there are many scenarios where you might want to handle a number as text instead. For example, the **GeographyKey** should be handled as text, because it would be inappropriate to perform mathematical operations on this identifier.  
  
    |Column|Content Type|Data Type|  
    |------------|------------------|---------------|  
    |**Address Line1**|**Discrete**|**Text**|  
    |**Address Line2**|**Discrete**|**Text**|  
    |**Age**|**Continuous**|**Long**|  
    |**Bike Buyer**|**Discrete**|**Long**|  
    |**Commute Distance**|**Discrete**|**Text**|  
    |**CustomerKey**|**Key**|**Long**|  
    |**DateLastPurchase**|**Continuous**|**Date**|  
    |**Email Address**|**Discrete**|**Text**|  
    |**English Education**|**Discrete**|**Text**|  
    |**English Occupation**|**Discrete**|**Text**|  
    |**FirstName**|**Discrete**|**Text**|  
    |**Gender**|**Discrete**|**Text**|  
    |**Geography Key**|**Discrete**|**Text**|  
    |**House Owner Flag**|**Discrete**|**Text**|  
    |**Last Name**|**Discrete**|**Text**|  
    |**Marital Status**|**Discrete**|**Text**|  
    |**Number Cars Owned**|**Discrete**|**Long**|  
    |**Number Children At Home**|**Discrete**|**Long**|  
    |**Region**|**Discrete**|**Text**|  
    |**Total Children**|**Discrete**|**Long**|  
    |**Yearly Income**|**Continuous**|**Double**|  
  
3.  Click **Next**.  
  
## Next Task in Lesson  
 [Specifying a Testing Data Set for the Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/specifying-a-testing-data-set-for-the-structure-basic-data-mining-tutorial.md)  
  
## Previous Task in Lesson  
 [Creating a Targeted Mailing Mining Model Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/creating-a-targeted-mailing-mining-model-structure-basic-data-mining-tutorial.md)  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/content-types-data-mining.md)   
 [Data Types &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/data-types-data-mining.md)  
  
  
