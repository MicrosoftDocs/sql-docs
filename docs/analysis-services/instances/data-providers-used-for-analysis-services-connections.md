---
title: "Data providers used for Analysis Services connections | Microsoft Docs"
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
# Client libraries (data providers) used for Analysis Services connections
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

Analysis Services provides three client libraries, also known as **data providers**, for server and data access from tools and client applications. Tools like SSMS and SSDT, and applications like Power BI Desktop and Excel connect to Analysis Services by using these libraries. Two of the client libraries, ADOMD.NET and Analysis Services Management Objects (AMO), are managed client libraries. The Analysis Services OLE DB provider (MSOLAP DLL) is a native client library. Client libraries are the same for both SQL Server Analysis Services and Azure Analysis Services.
  
##  <a name="bkmk_downloadsite"></a> Where to get newer versions  
 The version installed on a client computer should match the major version of the server providing the data. If the server installation is newer than the data providers installed on the workstations in your network, you might need to install newer libraries.  

Client libraries included in earlier SQL Server Feature Packs correspond to that SQL version; however, they may not be the latest. Connecting to Azure Analysis Services may require later versions. All versions are backward compatible.

To get the latest,  see [Client libraries for connecting to Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-data-providers). 
  
## See also  
 [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md)  
  
  
