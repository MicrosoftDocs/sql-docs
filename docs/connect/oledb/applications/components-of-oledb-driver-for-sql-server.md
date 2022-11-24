---
title: Components of OLE DB Driver for SQL Server
description: Learn about the OLE DB Driver for SQL Server components, including the library that contains the driver functionality, other libraries, and a header file.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/30/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "data access [OLE DB Driver for SQL Server], components"
  - "components [OLE DB Driver for SQL Server]"
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
---
# Components of OLE DB Driver for SQL Server

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

OLE DB Driver for SQL Server contains the components described in the following tables.

## OLE DB Driver 19 for SQL Server

|Component|Description|
|---------------|-----------------|
|msoledbsql19.dll|The dynamic-link library (DLL) file that contains all of the OLE DB Driver for SQL Server functionality.|
|msoledbsqlr19.rll|The accompanying resource file for the OLE DB Driver for SQL Server library.|
|msoledbsql.h|The OLE DB Driver for SQL Server header file that contains all of the new definitions needed in order to use OLE DB Driver for SQL Server. This header file replaces the sqloledb.h header file.<br /><br /> Note: You can reference msoledbsql.h and sqloledb.h in same program as long as sqloledb.h is defined first.|
|msoledbsql19.lib|The library file needed to directly call the [OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md) function that is part of the OLE DB Driver for SQL Server.<br /><br /> Note: If you do reference the msoledbsql19.lib file in your programming code, you need to make sure that the msoledbsql19.dll file is in your system path, and in the system path of the users that make use of your application.|

## OLE DB Driver 18 for SQL Server

|Component|Description|
|---------------|-----------------|
|msoledbsql.dll|The dynamic-link library (DLL) file that contains all of the OLE DB Driver for SQL Server functionality.|
|msoledbsqlr.rll|The accompanying resource file for the OLE DB Driver for SQL Server library.|
|msoledbsql.h|The OLE DB Driver for SQL Server header file that contains all of the new definitions needed in order to use OLE DB Driver for SQL Server. This header file replaces the sqloledb.h header file.<br /><br /> Note: You can reference msoledbsql.h and sqloledb.h in same program as long as sqloledb.h is defined first.|
|msoledbsql.lib|The library file needed to directly call the [OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md) function that is part of the OLE DB Driver for SQL Server.<br /><br /> Note: If you do reference the msoledbsql.lib file in your programming code, you need to make sure that the msoledbsql.dll file is in your system path, and in the system path of the users that make use of your application.|

## See also

[Building Applications with OLE DB Driver for SQL Server](building-applications-with-oledb-driver-for-sql-server.md)
