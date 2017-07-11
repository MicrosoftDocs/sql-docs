---
title: "View the XML for an Analysis Services Project (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "projects [Analysis Services], viewing XML"
ms.assetid: dd1a4bc6-57b5-47df-8619-09f921aa6351
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# View the XML for an Analysis Services Project (SSDT)
  When you are working with an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in project mode, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] creates an XML definition for each object within the project folder. You can view the contents of the XML file for each object within [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can also edit the XML file directly; however, this is not recommended in most circumstances as you may make changes that make the XML unreadable by [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
> [!NOTE]  
>  You cannot view the xml code for an entire project, but rather you view the code for each object because a separate file exists for each object. The only way to view the code for an entire project is to build the project and view the ASSL code in the \<project name>.asdatabase file.  
  
### To view the XML code for an object  
  
1.  Open the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the object in Solution Explorer and then click **View Code**.  
  
     The XML code for the object appears in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
## See Also  
 [Build Analysis Services Projects &#40;SSDT&#41;](../../analysis-services/multidimensional-models/build-analysis-services-projects-ssdt.md)  
  
  