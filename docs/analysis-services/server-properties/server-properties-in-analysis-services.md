---
title: "Server Properties in Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "SSAS, configuration properties"
  - "Analysis Services, configuration properties"
  - "SQL Server Analysis Services, configuration properties"
  - "configuration options [Analysis Services]"
  - "server properties [Analysis Services]"
  - "properties [Analysis Services], configuration"
  - "properties [Analysis Services]"
ms.assetid: 274b89cd-14ed-4666-bc13-eedf1de51e18
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Server Properties in Analysis Services
  An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrator can modify default server configuration properties of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. Each instance has its own configuration properties, which are set independently of other instances on the same server.  
  
 To configure the server, use SQL Server Management Studio or edit the msmdsrv.ini file of a specific instance.  
 
Property pages in SQL Server Management Studio show a subset of the properties most likely to be modified. The full list of properties is found in the msmdsrv.ini file.   
  
> [!NOTE]  
>  In a default installation, msmdsrv.ini can be found in the \Program Files\Microsoft SQL Server\MSAS13.MSSQLSERVER\OLAP\Config folder.
> 
> Other properties affecting server configuration include deployment configuration properties in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For more information about those properties, see [Specifying Configuration Settings for Solution Deployment](../../analysis-services/multidimensional-models/deployment-script-files-solution-deployment-config-settings.md).
 
##  <a name="bkmk_config"></a> Configure properties in Management Studio 
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
2. In Object Explorer, right-click the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, and then click **Properties**. The General page appears, displaying the more commonly used properties.  

3.  To view additional properties, click the **Show Advanced (All) Properties** checkbox at the bottom of the page.  
  
     Modifying server properties is supported only for tabular mode and multidimensional mode servers. If you installed [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], always use the default values unless otherwise directed otherwise by Microsoft Support.  
  
     For guidance on how to address operational or performance issues through server properties, see [SQL Server 2008 R2 Analysis Services Operations Guide](http://go.microsoft.com/fwlink/?LinkID=225539).  
  
     You can also read about server properties (many of which are unchanged over the last several releases) in this Microsoft white paper, [SQL Server 2005 Analysis Services (SSAS) Server Properties](http://go.microsoft.com/fwlink/?LinkID=199102).    
  
##  <a name="bkmk_msmdsrvini"></a> Configure properties in msmdsrv.ini
  Some properties can only be set in the msmdrsrv.ini file. If the property you want to set is not visible even after you show advanced properties, you might need to edit the msmdsrv.ini file directly.
  
1.  Check the **DataDir** property in the General property page in Management Studio to verify the location of the Analysis Services program files, including the msmdsrv.ini file.

     On a server that has multiple instances, checking the program file location ensures you're modifying the correct file.  
  
2.  Navigate to the **config** folder of the program files folder location.

3. Create a backup of the file in case you need to revert to the original file.  
  
4.  Use a text editor to view or edit the msmdsrv.ini file.  
  
5.  Save the file and restart the service.  
  
##  <a name="bkmk_ref"></a> Server Property Reference  
  
 The following topics describe the various [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configuration properties:  
  
|Topic|Description|  
|-----------|-----------------|  
|[General Properties](../../analysis-services/server-properties/general-properties.md)|The general properties are both basic and advanced properties, and include properties that define the data directory, backup directory, and other server behaviors.|  
|[Data Mining Properties](../../analysis-services/server-properties/data-mining-properties.md)|The data mining properties control which data mining algorithms are enabled and which are disabled. By default, all of the algorithms are enabled.| 
|[DAX Properties](../../analysis-services/server-properties/dax-properties.md)|Defines properties related to DAX queries.|
|DSO|DSO is no longer supported. DSO properties are ignored.|  
|[Feature Properties](../../analysis-services/server-properties/feature-properties.md)|The feature properties pertain to product features, most of them advanced, including properties that control links between server instances.|  
|[Filestore Properties](../../analysis-services/server-properties/filestore-properties.md)|The file store properties are for advanced use only. They include advanced memory management settings.|  
|[Lock Manager Properties](../../analysis-services/server-properties/lock-manager-properties.md)|The lock manager properties define server behaviors pertaining to locking and timeouts. Most of these properties are for advanced use only.|  
|[Log Properties](../../analysis-services/server-properties/log-properties.md)|The log properties controls if, where, and how events are logged on the server. This includes error logging, exception logging, flight recorder, query logging, and traces.|  
|[Memory Properties](../../analysis-services/server-properties/memory-properties.md)|The memory properties control how the server uses memory. They are primarily for advanced use.|  
|[Network Properties](../../analysis-services/server-properties/network-properties.md)|The network properties control server behavior pertaining to networking, including properties that control compression and binary XML. Most of these properties are for advanced use only.|  
|[OLAP Properties](../../analysis-services/server-properties/olap-properties.md)|The OLAP properties control cube and dimension processing, lazy processing, data caching, and query behavior. These include both basic and advanced properties.|  
|[Security Properties](../../analysis-services/server-properties/security-properties.md)|The security section contains both basic and advanced properties that define access permissions. This includes settings pertaining to administrators and users.|  
|[Thread Pool Properties](../../analysis-services/server-properties/thread-pool-properties.md)|The thread pool properties control how many threads the server creates. These are primarily advanced properties.|  
  
## See Also  
 [Analysis Services Instance Management](../../analysis-services/instances/analysis-services-instance-management.md)   
 [Specifying Configuration Settings for Solution Deployment](../../analysis-services/multidimensional-models/deployment-script-files-solution-deployment-config-settings.md)  
  
  