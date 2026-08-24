---
title: "IColumnsRowset (OLE DB driver)"
description: "The DBCOLUMN_BASETABLEINSTANCE column in IColumnsRowset::GetColumnRowset is reserved for use by Microsoft in OLE DB Driver for SQL Server."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ms.custom:
  - ignite-2025
---
# IColumnsRowset
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  OLE DB Driver for SQL Server adds the DBCOLUMN_BASETABLEINSTANCE column to IColumnsRowset::GetColumnRowset. This column returns DBTYPE_I2 and is reserved for use by Microsoft. The information in this column is subject to change in future releases.  
  
## Related content

- [OLE DB Driver for SQL Server (OLE DB) Interfaces](oledb-driver-for-sql-server-ole-db-interfaces.md)
