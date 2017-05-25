---
title: "Tabular Model Programming for Compatibility Levels 1050 through 1103 | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0ceb461e-12c1-44ea-97ac-b99bf308676b
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Tabular Model Programming for Compatibility Levels 1050 through 1103

[!INCLUDE[ssas-appliesto-sql2016-later](../../includes/ssas-appliesto-sql2016-later.md)]

  Tabular models use relational constructs to model the Analysis Services data used by analytical and reporting applications. These models run on an Analysis Service instance that is configured for tabular mode, using an in-memory analytics engine for storage and fast table scans that aggregate and calculate data as it is requested.  
  
 If the requirements of your custom BI solution are best met by a tabular model database, you can use any of the Analysis Services client libraries and programming interfaces to integrate your application with tabular models on an Analysis Services instance. To query and calculate tabular model data, you can use either embedded MDX or DAX in your code.  
  
 For Tabular models created in earlier versions of Analysis Services, in particular models at compatibility levels 1050 through 1103, the objects you work with programmatically in AMO, ADOMD.NET, XMLA, or OLE DB are fundamentally the same for both tabular and multidimensional solutions. Specifically, the object metadata defined for multidimensional models is also used for tabular model compatibility levels 1050-1103.  
  
 Beginning with SQL Server 2016, Tabular models can be built or upgraded to the 1200 or higher compatibility level, which uses tabular metadata to define the model. Metadata and programmability are fundamentally different at this level. See [Tabular Model Programming for Compatibility Level 1200 and higher](../../analysis-services/tabular-model-programming-compatibility-level-1200/tabular-model-programming-for-compatibility-level-1200.md) and [Upgrade Analysis Services](../../database-engine/install-windows/upgrade-analysis-services.md) for more information.  
  
## In This Section  
 [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/csdl-annotations-for-business-intelligence-csdlbi.md)  
  
 [Understanding the Tabular Object Model at Compatibility Levels 1050 through 1103](../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)  
  
 [Technical Reference for BI Annotations to CSDL](../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/technical-reference-for-bi-annotations-to-csdl.md)  
  

[IMDEmbeddedData Interface](../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/imdembeddeddata-interface.md)


  
  