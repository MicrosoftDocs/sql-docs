---
title: "Completing the Wizard (Data Mining Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.dmwizard.finish.f1"
ms.assetid: 6aef1548-35eb-42fd-ae87-63650a79eda1
author: minewiskan
ms.author: owend
manager: craigg
---
# Completing the Wizard (Data Mining Wizard)
  Use the **Completing the Wizard** page to review the mining structure that will be created when you finish the wizard. You can also set the name of the mining structure.  
  
 If you opted to create an associated mining model, you can also set the name of the associated mining model, and enable drillthrough on the mining model.  
  
> [!NOTE]  
>  This page changes depending on whether you selected **From existing relational database or data warehouse** or **From existing cube** on the **Select the Definition Method** page of the wizard.  
  
 **For More Information:** [Data Mining Wizard &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-wizard-analysis-services-data-mining.md), [Data Mining Designer](data-mining/data-mining-designer.md), [Create a Relational Mining Structure](data-mining/create-a-relational-mining-structure.md)  
  
## Options  
 **Mining structure name**  
 Type the name of the mining structure defined by the **Data Mining Wizard**.  
  
 **Mining model name**  
 Type the name of the mining model defined by the **Data Mining Wizard**.  
  
 **Allow Drill Through**  
 Select to support drillthrough capabilities in the new mining model, if you created one.  
  
> [!NOTE]  
>  Drillthrough on the mining structure is enabled by default.  
  
 For more information about drillthrough options, see [Drillthrough Queries &#40;Data Mining&#41;](data-mining/drillthrough-queries-data-mining.md).  
  
 **Preview**  
 Displays the mining structure to be created.  
  
 **Create Mining Model Dimension**  
 Select to create a dimension based on the mining model to be created. After you select this option, enter the name of the dimension to be created. Selecting this option will enable **Create cube using mining model dimension**.  
  
> [!NOTE]  
>  This option is available if you selected **From existing cube** on the **Select the Definition Method** page of the wizard.  
  
 **Create cube using mining model dimension**  
 Select to create a cube based on the mining model to be created. After you select this option, enter the name of the cube to be created.  
  
> [!NOTE]  
>  This option is available if you selected **From existing cube** on the **Select the Definition Method** page and if you have selected **Create Mining Model Dimension** on the current page of the wizard.  
  
## See Also  
 [Data Mining Wizard F1 Help &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-f1-help-analysis-services-data-mining.md)   
 [Select Data Source View &#40;Data Mining Wizard&#41;](select-data-source-view-data-mining-wizard.md)   
 [Specify the Training Data &#40;Data Mining Wizard&#41;](specify-the-training-data-data-mining-wizard.md)  
  
  
