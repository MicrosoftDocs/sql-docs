---
title: "Proactive Caching (Dimensions) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "dimensions [Analysis Services], proactive caching"
  - "proactive caching [Analysis Services]"
ms.assetid: 7d57fe93-6e5f-4a50-844f-dd2bbdbb94a5
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Proactive Caching (Dimensions)
  Proactive caching provides automatic MOLAP cache creation and management for OLAP objects. The cubes immediately incorporate changes that are made to the data in the database, based upon notifications received from the database. The goal of proactive caching is to provide the performance of traditional MOLAP, while retaining the immediacy and ease of management offered by ROLAP.  
  
 A simple <xref:Microsoft.AnalysisServices.ProactiveCaching> object is composed of: timing specification, and table notification. The timing specification defines the timeframe for updating the cache after a change notification has been received. The table notification defines the notification schema between the data table and the <xref:Microsoft.AnalysisServices.ProactiveCaching> object.  
  
  