---
title: Microsoft OLE DB Driver for SQL Server known issues
description: Learn about known issues and fixes for the Microsoft OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 02/07/2023
ms.prod: sql
ms.technology: connectivity
ms.topic: reference
---
# Microsoft OLE DB Driver for SQL Server known issues

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

This article contains a list of known issues and fixes for the Microsoft OLE DB Driver for SQL Server.

## Known issues

- The driver returns the following error message: `Error loading adal.dll from system directory. Verify that Active Directory Authentication Library for SQL Server is properly installed`. The most likely cause of this error is incorrect bitness of `adal.dll`.
- The installer shows the following error message: `Invalid bitness of adal.dll detected`. This error indicates that the 32-bit system directory contains a 64-bit version of `adal.dll`.

The above errors can be caused by the installation of version 19.2.0 of the OLE DB Driver. Version 19.2.0 erroneously places a 64-bit ADAL library into the 32-bit system directory. In addition, the installer ships an incorrect version of the ADAL library (3.6.0).

To resolve these issues, the user needs to:
1. Delete the `adal.dll` file from the following system folders:
- `%windir%\System32\adal.dll`
- `%windir%\SysWOW64\adal.dll` (64-bit version of Windows only)
2. Install the OLE DB Driver version 19.3 or higher. This step will install ADAL 3.4.1 with the correct bitness.

## See also
[Installing OLE DB Driver for SQL Server](applications/installing-oledb-driver-for-sql-server.md)