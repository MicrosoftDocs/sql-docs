---
title: Creating an OLE DB Driver for SQL Server Application
description: Learn about the steps necessary to create an OLE DB Driver for SQL Server application and find other resources.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, application creation"
  - "applications [OLE DB Driver for SQL Server]"
  - "OLE DB, creating applications"
---
# Creating an OLE DB Driver for SQL Server Application

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Creating an OLE DB Driver for SQL Server application involves these steps:

1. Establishing a connection to a data source.
2. Executing a command.
3. Processing the results.

> [!NOTE]
> When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with [the Win32 cryptoAPI](/windows/win32/seccng/cng-portal).

## In this section

- [Establishing a Connection to a Data Source](establishing-a-connection-to-a-data-source.md)
- [Executing a Command](executing-a-command.md)
- [Processing Results](processing-results.md)
- [About OLE DB Properties](about-ole-db-properties.md)
- [Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server](using-the-output-clause-with-ole-db-in-oledb-driver-for-sql-server.md)

## Related content

- [OLE DB Driver for SQL Server Programming](../ole-db/oledb-driver-for-sql-server-programming.md)
