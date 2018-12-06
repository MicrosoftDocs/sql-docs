---
title: "LINKEDSERVERS Rowset (OLE DB) | Microsoft Docs"
description: "LINKEDSERVERS Rowset (OLE DB)"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "LINKEDSERVERS rowset"
  - "enumerating data sources [OLE DB]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Schema Rowsets - LINKEDSERVERS Rowset
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

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
 [Schema Rowset Support &#40;OLE DB&#41;](../../oledb/ole-db/schema-rowset-support-ole-db.md)  
  
  
