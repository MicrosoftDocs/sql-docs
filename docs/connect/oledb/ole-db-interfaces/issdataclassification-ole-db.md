---
title: "ISSDataClassification | Microsoft Docs"
description: "ISSDataClassification interface"
ms.custom: ""
ms.date: "08/28/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSDataClassification"
apitype: "COM"
helpviewer_keywords: 
  - "ISSDataClassification interface"
author: bazizi
ms.author: v-beaziz
---
# ISSDataClassification
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../../includes/applies-to-version/sql-asdb-asa.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **ISSDataClassification** interface provides the functionality to retrieve sensitivity classification data for the active rowset as received from SQL Server.
  
|Method|Description|  
|------------|-----------------|  
|[ISSDataClassification::GetSensitivityClassification](../../oledb/ole-db-interfaces/issdataclassification-getsensitivityclassification-ole-db.md)|Returns a pointer to a SENSITIVITYCLASSIFICATION structure that contains sensitivity classification information.|  

## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)   
 [Rowsets](../ole-db-rowsets/rowsets.md)   
 [Using data classification](../features/using-data-classification.md)
