---
title: "Data source information properties (OLE DB driver)"
description: Learn about the data source information properties for the provider-specific property set DBPROPSET_SQLSERVERDATASOURCEINFO in OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, data source properties"
  - "properties [OLE DB]"
  - "data source properties [OLE DB]"
  - "information properties [OLE DB]"
  - "OLE DB data source properties [OLE DB Driver for SQL Server]"
---
# Data Source Information Properties
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  In the provider-specific property set DBPROPSET_SQLSERVERDATASOURCEINFO, the OLE DB Driver for SQL Server defines the following data source information properties.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_COLUMNLEVELCOLLATION|Type: VT_BOOL<br /><br /> R/W: Read<br /><br /> Default: VARIANT_TRUE<br /><br /> Description: Used to determine if column collation is supported.<br /><br /> VARIANT_TRUE: Column level collation is supported.<br /><br /> VARIANT_FALSE: Column level collation is not supported.|  
|SSPROP_UNICODELCID|Type: VT_I4 R/W: Read<br /><br /> Description: Unicode locale ID.<br /><br /> This is the locale used for Unicode data sorting.|  
|SSPROP_UNICODECOMPARISONSTYLE|Type: VT_I4 R/W: Read<br /><br /> Description: Unicode comparison style.<br /><br /> The sorting options used for Unicode data sorting.|  
  
 In the provider-specific property set DBPROPSET_SQLSERVERSTREAM, the OLE DB Driver for SQL Server defines the following additional property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_STREAM_XMLROOT|Type: VT_BSTR R/W: Read/Write<br /><br /> Description: The result of a FOR XML query may not be a well-formed document. When this property is specified, the result of a 'select ... for XML' query is wrapped in the root tag provided by this property to return a well formed XML document. If the query is executed in the browser it may cause the browser to display parser errors when loading the result. To avoid the error, SQL ISAPI supports the keyword ROOT. This keyword maps to SSPROP_STREAM_XMLROOT property.|  
  
## See Also  
 [Data Source Objects &#40;OLE DB&#41;](../../oledb/ole-db-data-source-objects/data-source-objects-ole-db.md)  
  
  
