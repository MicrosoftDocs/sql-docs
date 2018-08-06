---
title: "Features off by default (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Features off by default (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  An instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is designed to be secure by default. Therefore, features that might compromise security are disabled by default. The following features are installed in a disabled state and must specifically be enabled if you want to use them.  
  
## Feature List  
 To enable the following features, connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Right-click the instance name and choose **Facets**. Alternatively, you can enable these features through server properties, as described in the next section.  
  
-   Ad Hoc Data Mining (OpenRowset) Queries  
  
-   Linked Objects (To)  
  
-   Linked Objects (From)  
  
-   Listen Only On Local Connections  
  
-   User Defined Functions  
  
## Server properties  
 Additional features that are off by default can be enabled through server properties. Connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Right-click the instance name and choose **Properties**. Click **General**, and then click **Show Advanced** to display a larger property list.  
  
-   Ad Hoc Data Mining (OpenRowset) Queries  
  
-   Allow Session Mining Models (Data Mining)  
  
-   Linked Objects (To)  
  
-   Linked Objects (From)  
  
-   COM based user-defined functions  
  
-   Flight Recorder Trace Definitions (templates).  
  
-   Query logging  
  
-   Listen Only On Local Connections  
  
-   Binary XML  
  
-   Compression  
  
-   Group affinity. See [Thread Pool Properties](../../analysis-services/server-properties/thread-pool-properties.md) for details.  
  
  
