---
title: "LINKEDSERVERS Rowset (OLE DB)"
description: "Schema Rowsets - LINKEDSERVERS Rowset in SQL Server Native Client"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "LINKEDSERVERS rowset"
  - "enumerating data sources [OLE DB]"
---
# Schema Rowsets - LINKEDSERVERS Rowset in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-only](../../../includes/snac-removed-oledb-only.md)]

  The **LINKEDSERVERS** rowset enumerates organization data sources that can participate in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] distributed queries.  
  
 The **LINKEDSERVERS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|SVR_NAME|DBTYPE_WSTR|Name of a linked server.|  
|SVR_PRODUCT|DBTYPE_WSTR|Manufacturer or other name identifying the type of data store represented by the name of the linked server.|  
|SVR_PROVIDERNAME|DBTYPE_WSTR|Friendly name of the OLE DB provider used to consume data from the server.|  
|SVR_DATASOURCE|DBTYPE_WSTR|OLE DB DBPROP_INIT_DATASOURCE string used to acquire a data source from the provider.|  
|SVR_PROVIDERSTRING|DBTYPE_WSTR|OLE DB DBPROP_INIT_PROVIDERSTRING value used to acquire a data source from the provider.|  
|SVR_LOCATION|DBTYPE_WSTR|OLE DB DBPROP_INIT_LOCATION string used to acquire a data source from the provider.|  
  
 The rowset is sorted on SRV_NAME and a single restriction is supported on SRV_NAME.  
  
## See Also  
 [Schema Rowset Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/schema-rowset-support-ole-db.md)  
  
  
