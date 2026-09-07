---
title: "ISSDataClassification"
description: "ISSDataClassification interface"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: "09/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "ISSDataClassification interface"
apiname: "ISSDataClassification"
apitype: "COM"
---
# ISSDataClassification
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asa-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **ISSDataClassification** interface provides the functionality to retrieve sensitivity classification data for the active rowset as received from SQL Server.
  

## Methods

|Method|Description|  
|------------|-----------------|  
|[ISSDataClassification::GetSensitivityClassification](../../oledb/ole-db-interfaces/issdataclassification-getsensitivityclassification-ole-db.md)|Returns a pointer to a SENSITIVITYCLASSIFICATION structure that contains sensitivity classification information.|  

## Related content

- [OLE DB Driver for SQL Server (OLE DB) Interfaces](oledb-driver-for-sql-server-ole-db-interfaces.md)
- [Rowsets](../ole-db-rowsets/rowsets.md)
- [Using data classification](../features/using-data-classification.md)
