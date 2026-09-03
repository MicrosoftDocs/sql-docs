---
title: "Support Policies for OLE DB Driver for SQL Server"
description: "Learn about the support policies for OLE DB Driver for SQL Server and what operating systems and SQL database versions are supported with each driver version."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 05/26/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
---
# Support policies for OLE DB Driver for SQL Server

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE [Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

This article discusses how various data-access components can be used with OLE DB Driver for SQL Server.

## SQL version support

OLE DB Driver for SQL Server is tested with and supports connections to the following versions of SQL Server.

| Database version&nbsp;&#8594;<br />&#8595; Driver version | Azure SQL Database | Azure Synapse Analytics | Azure SQL Managed Instance | SQL Server 2025 | SQL Server 2022 | SQL Server 2019 | SQL Server 2017 | SQL Server 2016 | SQL Server 2014 | SQL Server 2012 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 19.3.5+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No |
| 19.3.3+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No |
| 19.0+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.7.4+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No |
| 18.6+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.2+ | Yes | Yes | Yes | No | No | Yes | Yes | Yes | Yes | Yes |
| 18.0+ | Yes | Yes | Yes | No | No | No | Yes | Yes | Yes | Yes |

## Supported operating system versions

The following table lists which operating systems are supported by OLE DB Driver for SQL Server.

| Operating system&nbsp;&#8594;<br />&#8595; Driver version | Windows Server 2025 | Windows Server 2022 | Windows Server 2019 | Windows Server 2016 | Windows Server 2012<sup>1</sup> | Windows Server 2012 R2<sup>2</sup> | Windows 11 | Windows 10 | Windows 8.1<sup>3</sup> |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 19.4.2+ | Yes | Yes | Yes | Yes | No | No | Yes | No | No |
| 19.3.3+ | Yes | Yes | Yes | Yes | No | No | Yes | Yes | No |
| 19.3+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No |
| 19.0+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.7+ | Yes | Yes | Yes | Yes | No | No | Yes | Yes | No |
| 18.6+ | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 18.2+ | No | No | Yes | Yes | Yes | Yes | No | Yes | Yes |
| 18.0+ | No | No | No | Yes | Yes | Yes | No | Yes | Yes |

<sup>1</sup> Supported on Windows Server 2012 with [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  
<sup>2</sup> Supported on Windows Server 2012 R2 with [April 2014 update](https://go.microsoft.com/fwlink/?linkid=2073785) and [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  
<sup>3</sup> Supported on Windows 8.1 with [April 2014 update](https://go.microsoft.com/fwlink/?linkid=2073785) and [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  

## ADO support policies

ADO applications can use the SQLOLEDB OLE DB provider that is included with Windows if they don't require any of the features of [!INCLUDE [ssVersion2005](../../../includes/ssversion2005-md.md)] or later versions.

ADO applications can use the OLE DB Driver for SQL Server, but if they do so they must specify `DataTypeCompatibility=80` in the connection strings. Only features from [!INCLUDE [ssVersion2005](../../../includes/ssversion2005-md.md)] are available when `DataTypeCompatibility=80` is present in the connection strings.

## OLE DB support policies

Applications can use the OLE DB provider (SQLOLEDB) included with the Windows operating system. However, that is in maintenance mode and no longer updated. Use the OLE DB Driver for SQL Server (MSOLEDBSQL19 or MSOLEDBSQL) instead.

## Related content

- [Building applications with OLE DB Driver for SQL Server](building-applications-with-oledb-driver-for-sql-server.md)
