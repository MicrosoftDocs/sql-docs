---
title: Microsoft OLE DB Driver for SQL Server known issues
description: Learn about known issues and fixes for the Microsoft OLE DB Driver for SQL Server.
author: David-Engel
ms.author: davidengel
ms.reviewer: wiassaf
ms.date: 01/24/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# Microsoft OLE DB Driver for SQL Server known issues

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

This article contains a list of known issues and fixes for the Microsoft OLE DB Driver for SQL Server.

## Known issues

- The driver returns the following error message: `Error loading adal.dll from system directory. Verify that Active Directory Authentication Library for SQL Server is properly installed`. The most likely cause of this error is incorrect bitness of `adal.dll`. It can be caused by the installation of version 19.2.0 of the OLE DB Driver. Version 19.2.0 erroneously places a 64-bit ADAL library into the 32-bit system directory. To resolve this issue, install the OLE DB Driver version 19.3 or higher. It includes ADAL 3.6.1 with the correct bitness.
- Currently, MSOLEDBSQL19 prevents the creation of linked servers without encryption and a trusted certificate (a self-signed certificate is insufficient). For more information, see [Linked servers](../../relational-databases/linked-servers/linked-servers-database-engine.md?view=sql-server-ver16&preserve-view=true#msoledbsql19-and-linked-servers).
- Use of MSOLEDBSQL19 within a linked server is only supported on SQL Server 2022 or newer.

## Next steps

- [Installing OLE DB Driver for SQL Server](applications/installing-oledb-driver-for-sql-server.md)
- [Support policies for OLE DB Driver for SQL Server](applications/support-policies-for-oledb-driver-for-sql-server.md)
