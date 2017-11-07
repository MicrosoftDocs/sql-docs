---
title: "LINKEDSERVERS Rowset (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "LINKEDSERVERS rowset"
  - "enumerating data sources [OLE DB]"
ms.assetid: 2633fd8a-65e7-498d-9aed-8e4b1cca2381
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Schema Rowsets - LINKEDSERVERS Rowset
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

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
  
  