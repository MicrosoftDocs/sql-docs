---
title: "Data sources from existing objects (Data Source Wizard) (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.datasourcewizard.specifyobject.f1"
ms.assetid: e6ef6dea-9db8-45c4-8959-f9febd7caf7b
author: minewiskan
ms.author: owend
manager: craigg
---
# Data sources from existing objects (Data Source Wizard) (Analysis Services)
  Use the **Data sources from existing objects** page to specify an existing data source or project on which to base the new data source.  
  
## Options  
 **Create a data source based on an existing data source in your solution**  
 Select to base the new data source on an existing data source in the solution. When a project that uses the new data source is built, refreshed, or deployed, the new data source acquires the settings from the data source you specify when you select this option.  
  
 **Data source**  
 Select a data source on which you want to base the new data source from the list of data sources, which is grouped by project.  
  
 **Create a data source based on an Analysis Services project**  
 Select to create a new data source that references another [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project in the current solution. The new data source acquires settings from the `TargetServer` and `TargetDatabase` properties of the selected project. When a project that uses the new data source is built, refreshed, or deployed, the new data source acquires settings from the data source you specify when you select this option.  
  
 **Project**  
 Select the project that you want to reference in the new data source.  
  
## See Also  
 [Data Source Wizard F1 Help &#40;Analysis Services&#41;](data-source-wizard-f1-help-analysis-services.md)   
 [Data Sources in Multidimensional Models](multidimensional-models/data-sources-in-multidimensional-models.md)   
 [Data Sources Supported &#40;SSAS Multidimensional&#41;](multidimensional-models/supported-data-sources-ssas-multidimensional.md)  
  
  
