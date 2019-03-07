---
title: "Web Configuration Reference (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "web configuration file [Master Data Services]"
ms.assetid: b8cc9a35-97ab-4fe0-ab4b-c07f13d9793a
author: leolimsft
ms.author: lle
manager: craigg
---
# Web Configuration Reference (Master Data Services)
  [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] uses a Web.config file to contain the configuration settings that enable Internet Information Services (IIS) to host the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] Web application and the Web service. This Web.config file is located in the WebApplication folder of the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] installation path. For more information about the path and permissions, see [Folder and File Permissions &#40;Master Data Services&#41;](folder-and-file-permissions-master-data-services.md).  
  
## Web.Config Elements  
 The Web.config file contains a custom [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] element, **\<masterDataServices>**, in addition to standard IIS, .NET Framework, ASP.NET, and Windows Communication Foundation (WCF) configuration elements. The following table describes the elements included in the Web.config file.  
  
|Configuration Element|Description|  
|---------------------------|-----------------|  
|`masterDataServices`|Custom element. Connects the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] Web service to a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|  
|`connectionStrings`|ASP.NET element. For more information, see [connectionStrings Element (ASP.NET Settings Schema)](https://go.microsoft.com/fwlink/?LinkId=178347) in the MSDN Library.|  
|`system.web`|ASP.NET element. For more information, see [system.web Element (ASP.NET Settings Schema)](https://go.microsoft.com/fwlink/?LinkId=178348) in the MSDN Library.|  
|`startup`|.NET Framework element. For more information, see [\<startup> Element](https://go.microsoft.com/fwlink/?LinkId=178349) in the MSDN Library.|  
|`runtime`|.NET Framework element. For more information, see [\<runtime> Element](https://go.microsoft.com/fwlink/?LinkId=178350) in the MSDN Library.|  
|`system.codedom`|.NET Framework element. For more information, see [\<system.codedom> Element](https://go.microsoft.com/fwlink/?LinkId=178351) in the MSDN Library.|  
|`system.web.extensions`|ASP.NET element. For more information, see [system.web.extensions Element (ASP.NET Settings Schema)](https://go.microsoft.com/fwlink/?LinkId=178352) in the MSDN Library.|  
|`system.webServer`|Section group that contains IIS elements. For more information, see [system.webServer Section Group \[IIS 7 Settings Schema\]](https://go.microsoft.com/fwlink/?LinkId=178353) in the MSDN Library.|  
|`system.serviceModel`|WCF element. For more information, see [\<system.serviceModel>](https://go.microsoft.com/fwlink/?LinkId=178354) in the MSDN Library.|  
|`system.diagnostics`|.NET Framework element. For more information, see [\<system.diagnostics> Element](https://go.microsoft.com/fwlink/?LinkId=178355) in the MSDN Library.|  
|`appSettings`|ASP.NET element. For more information, see [appSettings Element (General Settings Schema)](https://go.microsoft.com/fwlink/?LinkId=178356) in the MSDN Library.|  
  
## masterDataServices Element  
 The **\<masterDataServices>** element is a custom element that is used to connect a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] Web service to a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
### Syntax  
  
```  
<masterDataServices>  
   <instance virtualPath="path" siteName="name" connectionName="name" serviceName="name" />  
</masterDataServices>  
```  
  
### Elements and Attributes  
  
|Item|Description|  
|----------|-----------------|  
|`instance`|Child element. Contains attributes that specify information for the Web service and database connection string.|  
|`virtualPath`|Attribute. Specifies the virtual path of the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] Web application and service. This corresponds to the `path` attribute of the **\<application>** element under the **\<site>** element in the IIS ApplicationHost.config file.|  
|`siteName`|Attribute. Specifies the name of the site that hosts the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] Web application and service. This corresponds to the `name` attribute of the **\<site>** element under **\<sites>** in the IIS ApplicationHost.config file.|  
|`connectionName`|Attribute. Specifies the name of the connection to use. This corresponds to the `name` attribute of the **\<add>** element under the **\<connectionStrings>** element in Web.config.|  
|`serviceName`|Attribute. Specifies the name of the Web service. This corresponds to the `name` attribute of the **\<service>** element under the **\<services>** element in Web.config.|  
  
### Example  
 The following example demonstrates a service named MDS1 on the Contoso site and /MDS path using a connection string specified by MDSDB.  
  
```  
<masterDataServices>  
   <instance virtualPath="/MDS" siteName="Contoso" connectionName="MDSDB" serviceName="MDS1" />  
</masterDataServices>  
```  
  
  
