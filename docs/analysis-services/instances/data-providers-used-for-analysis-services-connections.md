---
title: "Data providers used for Analysis Services connections | Microsoft Docs"
ms.custom: ""
ms.date: "12/06/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 128f6dde-409d-4c12-9820-3305bab57b75
caps.latest.revision: 19
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "On Demand"
---
# Client libraries (data providers) used for Analysis Services connections
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]

Analysis Services provides three client libraries, also known as **data providers**, for server and data access from tools and client applications. Tools like SSMS and SSDT, and applications like Power BI Desktop and Excel connect to Analysis Services by using these libraries. Two of the client libraries, ADOMD.NET and Analysis Services Management Objects (AMO), are managed client libraries. The Analysis Services OLE DB provider (MSOLAP DLL) is a native client library. 
  
##  <a name="bkmk_downloadsite"></a> Where to get newer versions  
 The version installed on the client computer should match the major version of the server providing the data. If the server installation is newer than the data providers installed on the workstations in your network, you might need to install newer libraries.  

Client libraries included in earlier SQL Server Feature Packs correspond to that SQL version; however, they may not be the latest. Connecting to Azure Analysis Services may require later versions. All versions are backward compatible.

To get the latest,  see [Client libraries for connecting to Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-data-providers). 
  
## See also  
 [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md)  
  
  
