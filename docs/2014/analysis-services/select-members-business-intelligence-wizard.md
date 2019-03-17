---
title: "Select Members (Business Intelligence Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.biwizard.currencyconversion.memberconversion.f1"
ms.assetid: 1a147461-d594-41e7-a41d-09d2d003e1e0
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Members (Business Intelligence Wizard)
  Use the **Select Members** page to determine to which members the Business Intelligence Wizard should apply the currency conversion functionality specified on the **Set Currency Conversion Options** page.  
  
> [!NOTE]  
>  This page does not appear if the Business Intelligence Wizard was started from Dimension Designer or by right-clicking a dimension in Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Options  
 **Measures dimension**  
 Select to apply the currency conversion functionality to one or more measures in the cube.  
  
 If selected, the grid displays the options listed in the following table.  
  
|Option|Description|  
|------------|-----------------|  
|**Built-in Measure Types**|Select to include currency conversion functionality for the specified measure.|  
|**Measures**|Select the measure from the rate measure group that contains the exchange rate to use when the measure selected in **Built-in Measure Types** is converted.|  
  
 **Account hierarchy**  
 Select to apply the currency conversion functionality to one or more members in the account hierarchy of the account dimension included in the cube. The account hierarchy is the hierarchy within the account dimension whose `Type` property is set to *Account*.  
  
 If selected, the grid displays the options listed in the following table.  
  
|Option|Description|  
|------------|-----------------|  
|**Account Member**|Select to include currency conversion functionality for the specified member of the account hierarchy.|  
|**Measures**|Select the measure from the rate measure group that contains the exchange rate to use when the measures for the member selected in **Account Member** are converted.|  
  
 **Account hierarchy based on type**  
 Select to apply the currency conversion functionality to all members of attributes in the account hierarchy whose `Type` property is set to a specified account type.  
  
 If selected, the grid displays the options listed in the following table.  
  
|Option|Description|  
|------------|-----------------|  
|**Account Type**|Select to include currency conversion functionality for the specified account type.|  
|**Measures**|Select the measure from the rate measure group that contains the exchange rate to use when measures for the members of attributes using the account type selected in **Account Type** are converted.|  
  
## See Also  
 [Business Intelligence Wizard F1 Help](business-intelligence-wizard-f1-help.md)   
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Dimension Designer &#40;Analysis Services - Multidimensional Data&#41;](dimension-designer-analysis-services-multidimensional-data.md)  
  
  
