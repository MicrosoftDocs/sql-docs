---
title: "Impact Analysis Dialog Box (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.process.impactanalysisdialog.f1"
ms.assetid: 208268eb-4e14-44db-9c64-6f74b776adb6
author: minewiskan
ms.author: owend
manager: craigg
---
# Impact Analysis Dialog Box (Analysis Services - Multidimensional Data)
  Use the **Impact Analysis** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to identify and optionally process dependent objects that are affected if the objects listed in the **Process** dialog box are processed. You can display the **Impact Analysis** dialog box by clicking **Impact Analysis** from the **Process** dialog box.  
  
> [!NOTE]  
>  An object appears more than once if it is affected in more than one way.  
  
## Options  
 **Object list**  
 Displays a list of dependent objects in a grid. The grid contains the following columns:  
  
 **Object Name**  
 Displays the name of the dependent object that may need to be processed. The icon to the left of the name indicates the object type.  
  
 **Type**  
 Displays the type of dependent object that may need to be processed.  
  
 **Impact Type**  
 Displays the effect that processing the objects in the **Process** dialog box has on the dependent object. The following table lists the possible effects of processing and notes whether each one results in a warning or an error.  
  
|Impact|Message|  
|------------|-------------|  
|Object will be cleared (unprocessed)|Warning|  
|Object would be invalid|Error|  
|Aggregation would be dropped|Warning|  
|Flexible aggregation would be dropped|Warning|  
|Indexes will be dropped|Warning|  
|Non-child object will be processed|Warning|  
  
 **Process Object**  
 Select the dependent objects that you want to process with the processing operation. Dependent objects that are not selected must be processed after the processing operation is finished. Otherwise, they cannot be used.  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Process Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](process-dialog-box-analysis-services-multidimensional-data.md)  
  
  
