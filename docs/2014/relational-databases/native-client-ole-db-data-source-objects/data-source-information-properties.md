---
title: "Data Source Information Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, data source properties"
  - "properties [OLE DB]"
  - "data source properties [OLE DB]"
  - "information properties [OLE DB]"
  - "OLE DB data source properties [SQL Server Native Client]"
ms.assetid: 7fd80e47-5bd9-41e2-a3d3-091a7c8c5f2b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Source Information Properties
  In the provider-specific property set DBPROPSET_SQLSERVERDATASOURCEINFO, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider defines the following data source information properties.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_COLUMNLEVELCOLLATION|Type: VT_BOOL<br /><br /> R/W: Read<br /><br /> Default: VARIANT_TRUE<br /><br /> Description: Used to determine if column collation is supported.<br /><br /> VARIANT_TRUE: Column level collation is supported.<br /><br /> VARIANT_FALSE: Column level collation is not supported.|  
|SSPROP_UNICODELCID|Type: VT_I4 R/W: Read<br /><br /> Description: Unicode locale ID.<br /><br /> This is the locale used for Unicode data sorting.|  
|SSPROP_UNICODECOMPARISONSTYLE|Type: VT_I4 R/W: Read<br /><br /> Description: Unicode comparison style.<br /><br /> The sorting options used for Unicode data sorting.|  
  
 In the provider-specific property set DBPROPSET_SQLSERVERSTREAM, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider defines the following additional property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_STREAM_XMLROOT|Type: VT_BSTR R/W: Read/Write<br /><br /> Description: The result of a FOR XML query may not be a well-formed document. When this property is specified, the result of a 'select ... for XML' query is wrapped in the root tag provided by this property to return a well formed XML document. If the query is executed in the browser it may cause the browser to display parser errors when loading the result. To avoid the error, SQL ISAPI supports the keyword ROOT. This keyword maps to SSPROP_STREAM_XMLROOT property.|  
  
## See Also  
 [Data Source Objects &#40;OLE DB&#41;](data-source-objects-ole-db.md)  
  
  
