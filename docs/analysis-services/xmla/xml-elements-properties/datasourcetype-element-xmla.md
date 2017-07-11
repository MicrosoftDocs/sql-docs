---
title: "DataSourceType Element (XMLA) | Microsoft Docs"
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
apiname: 
  - "DataSourceType Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DataSourceType"
  - "microsoft.xml.analysis.datasourcetype"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DataSourceType"
helpviewer_keywords: 
  - "DataSourceType element"
ms.assetid: f5a348b1-911b-4139-832e-4bcb6d80a728
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# DataSourceType Element (XMLA)
  Indicates whether a [Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md) element specified for a [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command is local or remote.  
  
## Syntax  
  
```xml  
  
<Location>  
   ...  
   <DataSourceType>...</DataSourceType>  
   ...  
</Location>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Remote*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DataSourceType** element determines whether the data source defined by the **Location** element contains a local data source or a remote data source. For more information about backing up and restoring remote partitions, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Local*|The **Location** element defines a local data source. If this value is used, the **Restore** and **Synchronize** commands use the information defined in the **Location** element to update the data source, retrieved from either the backup file specified in the **File** element for the **Backup** command or the database specified in the **Source** element for the **Synchronize** command, identified in the **DataSourceID** element of the **Location** element.<br /><br /> This value allows objects that use relational OLAP (ROLAP) storage, after being restored or synchronized, to use a different database for their data and metadata.<br /><br /> Note: ROLAP data, such as data in dimension tables or writeback tables, is not restored or synchronized. Only metadata for ROLAP objects is restored or synchronized.|  
|*Remote*|The **Location** element defines a remote data source. If this value is used, the **Restore** and **Synchronize** commands use the information defined in the **Location** element to restore or synchronize remote partitions, retrieved from either the backup file specified in the **File** element of the **Backup** command or the database specified in the **Source** element for the **Synchronize** command, to the remote instance identified in the **DataSourceID** of the **Location** element.|  
  
## See Also  
 [ConnectionString Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/connectionstring-element-xmla.md)   
 [DataSourceID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/datasourceid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  