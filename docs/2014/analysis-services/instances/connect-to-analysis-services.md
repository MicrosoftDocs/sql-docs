---
title: "Connect to Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "instances of Analysis Services, connections"
ms.assetid: 73ee8171-3379-4384-bfc8-071b3eebbc8f
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to Analysis Services
  Use the information in this section to learn about connection string properties, client libraries used for connections, which authentication methods are supported by Analysis Services, and how to set up or clear connections before taking a server offline.  
  
## Analysis Services connections  
 Analysis Services uses TCP as the network protocol and XML for Analysis (XMLA) as a communication protocol. At the lowest level, all of the client libraries provided with Analysis Services implementing XMLA-over-TCP. Although it is possible to build applications based on raw XMLA, most applications and application developers use client libraries to take advantage of the object models and coding efficiencies that they provide. For client connections to Analysis Services, you can use IIS as an intermediary connection if you cannot use TCP across the stack. One advantage of using HTTP access via IIS is the ability to connect from applications that pass credentials on the connection string.  
  
 Any discussion involving connectivity typically includes authentication. In contrast with other SQL Server features, Analysis Services uses Windows credentials exclusively. You cannot use SQL Server database authentication, claims authentication, forms-based authentication, or digest on the backend connection to Analysis Services. More about authentication is provided in this section.  
  
##  <a name="bkmk_clientApps"></a> Connection Tasks  
  
|Link|Task Description|  
|----------|----------------------|  
|[Connect from client applications &#40;Analysis Services&#41;](connect-from-client-applications-analysis-services.md)|If you are new to Analysis Services, read this topic to get started with the tools and applications most often used with Analysis Services.|  
|[Connection String Properties &#40;Analysis Services&#41;](connection-string-properties-analysis-services.md)|Analysis Services includes numerous server and database properties, allowing you to customize a connection for a specific application, independent of how the instance or database is configured.|  
|[Authentication methodologies supported by Analysis Services](authentication-methodologies-supported-by-analysis-services.md)|This topic is a brief introduction to the authentication methods used by Analysis Services.|  
|[Configure Analysis Services for Kerberos constrained delegation](configure-analysis-services-for-kerberos-constrained-delegation.md)|Many business intelligence solutions require impersonation to ensure that only authorized data is returned to each user. In this topic, learn the requirements for using impersonation. This topic also explains the steps for configuring Analysis Services for Kerberos constrained delegation.|  
|[SPN registration for an Analysis Services instance](spn-registration-for-an-analysis-services-instance.md)|Kerberos authentication requires a valid Service Principle Name (SPN) for services that impersonate or delegate user identities in multi-server solutions. Use the information in this topic to learn the construction and steps for SPN registration for Analysis Services.|  
|[Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](configure-http-access-to-analysis-services-on-iis-8-0.md)|Basic authentication or cross-domain boundaries are two important reasons for configuring Analysis Services for HTTP access.|  
|[Data providers used for Analysis Services connections](data-providers-used-for-analysis-services-connections.md)|Analysis Services provides three client libraries for accessing server operations or Analysis Services data. This topic offers a brief introduction to ADOMD.NET, Analysis Services Management Objects (AMO), and the Analysis Services OLE DB provider (MSOLAP).|  
|[Disconnect Users and Sessions on Analysis Services Server](disconnect-users-and-sessions-on-analysis-services-server.md)|Clear existing connections and sessions before taking a server offline or conducting baseline performance tests.|  
  
## See Also  
 [Post-install Configuration &#40;Analysis Services&#41;](post-install-configuration-analysis-services.md)   
 [Configure Server Properties in Analysis Services](../server-properties/server-properties-in-analysis-services.md)   
 [Script Administrative Tasks in Analysis Services](../script-administrative-tasks-in-analysis-services.md)  
  
  
