---
title: "Configure Server Properties in Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "SSAS, configuration properties"
  - "Analysis Services, configuration properties"
  - "SQL Server Analysis Services, configuration properties"
  - "configuration options [Analysis Services]"
  - "server properties [Analysis Services]"
  - "properties [Analysis Services], configuration"
  - "properties [Analysis Services]"
ms.assetid: 274b89cd-14ed-4666-bc13-eedf1de51e18
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Server Properties in Analysis Services
  An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrator can modify default server configuration properties for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. Each instance has its own configuration properties that can be set independently of other instances on the same server.  
  
 To set server properties, use SQL Server Management Studio or edit the msmdsrv.ini file of a specific instance.  
  
 This topic contains the following sections:  
  
 [Configure Server (Instance) Properties](#bkmk_config)  
  
 [Server Property Reference](#bkmk_ref)  
  
##  <a name="bkmk_config"></a> Configure Server (Instance) Properties  
 The property pages in SQL Server Management Studio contain a subset of the available properties, showing only those properties that are more likely to be modified. The full set of properties can be found in the msmdsrv.ini file.  
  
> [!NOTE]  
>  This topic does not document the deployment configuration properties in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For more information about deployment configuration, see [Specifying Configuration Settings for Solution Deployment](../multidimensional-models/deployment-script-files-solution-deployment-config-settings.md).  
  
#### View or set configuration properties in Management Studio  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
     In Object Explorer, right-click the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, and then click **Properties**. The General page appears, displaying the more commonly used properties.  
  
2.  To view additional properties, click the **Show Advanced (All) Properties** checkbox at the bottom of the page.  
  
     Modifying server properties is supported only for tabular mode and multidimensional mode servers. If you installed [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], always use the default values unless you are directed otherwise by a Microsoft product support engineer.  
  
     For guidance on how to address operational or performance issues through server properties, see [SQL Server 2008 R2 Analysis Services Operations Guide](https://go.microsoft.com/fwlink/?LinkID=225539).  
  
     You can also read about server properties (many of which are unchanged over the last several releases) in this Microsoft white paper, [SQL Server 2005 Analysis Services (SSAS) Server Properties](https://go.microsoft.com/fwlink/?LinkID=199102).  
  
    > [!NOTE]  
    >  Some properties can only be set in the msmdrsrv.ini file. If the property you want to set is not visible even after you show advanced properties, you might need to edit the msmdsrv.ini file directly.  
  
#### View or edit configuration properties in the msmdsrv.ini file  
  
1.  Before you begin, check the **DataDir** property in the General property page in Management Studio to verify the location of the Analysis Services program files, including the msmdsrv.ini file. Verifying the location of the program files will ensure that you are modifying the correct file.  
  
    > [!NOTE]  
    >  In a default installation, the file can be found in the \Program Files\Microsoft SQL Server\MSAS12.MSSQLSERVER\OLAP\Config folder  
  
2.  Create a backup of the file in case you need to revert to the original file.  
  
3.  Use a text editor to view or edit the msmdsrv.ini file.  
  
4.  After you save the file, you must restart the service.  
  
##  <a name="bkmk_ref"></a> Server Property Reference  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration properties are important for fine tuning your system. For example, to make query log behavior consistent with your requirements, you can set the relevant properties.  
  
 The following topics explain the various [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration properties:  
  
|Topic|Description|  
|-----------|-----------------|  
|[General Properties](general-properties.md)|The general properties are both basic and advanced properties, and include properties that define the data directory, backup directory, and other server behaviors.|  
|[Data Mining Properties](data-mining-properties.md)|The data mining properties control which data mining algorithms are enabled and which are disabled. By default, all of the algorithms are enabled.|  
|DSO|DSO is no longer supported. DSO properties are ignored.|  
|[Feature Properties](feature-properties.md)|The feature properties pertain to product features, most of them advanced, including properties that control links between server instances.|  
|[Filestore Properties](filestore-properties.md)|The file store properties are for advanced use only. They include advanced memory management settings.|  
|[Lock Manager Properties](lock-manager-properties.md)|The lock manager properties define server behaviors pertaining to locking and timeouts. Most of these properties are for advanced use only.|  
|[Log Properties](log-properties.md)|The log properties controls if, where, and how events are logged on the server. This includes error logging, exception logging, flight recorder, query logging, and traces.|  
|[Memory Properties](memory-properties.md)|The memory properties control how the server uses memory. They are primarily for advanced use.|  
|[Network Properties](network-properties.md)|The network properties control server behavior pertaining to networking, including properties that control compression and binary XML. Most of these properties are for advanced use only.|  
|[OLAP Properties](olap-properties.md)|The OLAP properties control cube and dimension processing, lazy processing, data caching, and query behavior. These include both basic and advanced properties.|  
|[Security Properties](security-properties.md)|The security section contains both basic and advanced properties that define access permissions. This includes settings pertaining to administrators and users.|  
|[Thread Pool Properties](thread-pool-properties.md)|The thread pool properties control how many threads the server creates. These are primarily advanced properties.|  
  
## See Also  
 [Analysis Services Instance Management](../instances/analysis-services-instance-management.md)   
 [Specifying Configuration Settings for Solution Deployment](../multidimensional-models/deployment-script-files-solution-deployment-config-settings.md)  
  
  
