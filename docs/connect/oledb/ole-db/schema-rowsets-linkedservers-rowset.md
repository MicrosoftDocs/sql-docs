---
title: "LINKEDSERVERS Rowset (OLE DB)"
description: The LINKEDSERVERS rowset enumerates organization data sources that can participate in distributed queries in OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/12/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "LINKEDSERVERS rowset"
  - "enumerating data sources [OLE DB]"
---
# Schema Rowsets - LINKEDSERVERS Rowset
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

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
  
## Related content

- [Schema Rowset Support (OLE DB)](schema-rowset-support-ole-db.md)
