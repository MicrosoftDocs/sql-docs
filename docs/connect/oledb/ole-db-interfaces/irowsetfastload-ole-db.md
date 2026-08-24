---
title: "IRowsetFastLoad (OLE DB driver)"
description: OLE DB Driver for SQL Server consumers can use the IRowsetFastLoad interface to rapidly add data to an existing SQL Server table.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "IRowsetFastLoad interface"
apitype: "COM"
---
# IRowsetFastLoad (OLE DB)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **IRowsetFastLoad** interface exposes support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] memory-based bulk-copy operations. OLE DB Driver for SQL Server consumers use the interface to rapidly add data to an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 If you set SSPROP_ENABLEFASTLOAD to VARIANT_TRUE for a session, you cannot read data from rowsets subsequently returned from that session. When SSPROP_ENABLEFASTLOAD is set to VARIANT_TRUE, all rowsets created on the session will be of type IRowsetFastLoad. IRowsetFastLoad rowsets do not support rowset fetch functionality; therefore, data from these rowsets cannot be read.  
  
## In This Section  
  
|Method|Description|  
|------------|-----------------|  
|[IRowsetFastLoad::Commit &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/irowsetfastload-commit-ole-db.md)|Marks the end of a batch of inserted rows and writes the rows to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.|  
|[IRowsetFastLoad::InsertRow &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/irowsetfastload-insertrow-ole-db.md)|Adds a row to the bulk copy rowset.|  
  
## Related content

- [OLE DB Driver for SQL Server (OLE DB) Interfaces](oledb-driver-for-sql-server-ole-db-interfaces.md)
- [Bulk Copy Data Using IRowsetFastLoad (OLE DB)](../ole-db-how-to/bulk-copy-data-using-irowsetfastload-ole-db.md)
- [Send BLOB Data to SQL SERVER Using IROWSETFASTLOAD and ISEQUENTIALSTREAM (OLE DB)](../ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md)
