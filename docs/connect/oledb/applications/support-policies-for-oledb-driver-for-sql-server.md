---
title: "Support policies for OLE DB Driver for SQL Server | Microsoft Docs"
description: "Support policies for OLE DB Driver for SQL Server"
ms.date: "08/28/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.custom: ""
ms.technology: connectivity
ms.topic: "reference"
author: pmasl
ms.author: pelopes
---
# Support policies for OLE DB Driver for SQL Server
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

This article discusses how various data-access components can be used with OLE DB Driver for SQL Server.  

## SQL version support  

OLE DB Driver for SQL Server is tested with and supports connections to the following versions of SQL Server.

| Driver version | Azure SQL Database | Azure SQL DW | Azure SQL Managed Instance | SQL Server 2019 | SQL Server 2017 | SQL Server 2016 | SQL Server 2014 | SQL Server 2012 |
|----|-|-|-|-|-|-|-|-|
|18.5|Y|Y|Y|Y|Y|Y|Y|Y|
|18.4|Y|Y|Y|Y|Y|Y|Y|Y|
|18.3|Y|Y|Y|Y|Y|Y|Y|Y|
|18.2|Y|Y|Y|Y|Y|Y|Y|Y|
|18.1|Y|Y|Y| |Y|Y|Y|Y|
|18.0|Y|Y|Y| |Y|Y|Y|Y|
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |

## Supported operating system versions  

The following table lists which operating systems are supported by OLE DB Driver for SQL Server.  

| Driver version | Windows Server 2019 | Windows Server 2016 | Windows Server 2012<sup>1</sup> | Windows Server 2012 R2<sup>2</sup> | Windows 10 | Windows 8.1<sup>3</sup> |
|----|-|-|-|-|-|-|
|18.5|Y|Y|Y|Y|Y|Y|
|18.4|Y|Y|Y|Y|Y|Y|
|18.3|Y|Y|Y|Y|Y|Y|
|18.2|Y|Y|Y|Y|Y|Y|
|18.1| |Y|Y|Y|Y|Y|
|18.0| |Y|Y|Y|Y|Y|
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |

<sup>1</sup> Supported on Windows Server 2012 with [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  
<sup>2</sup> Supported on Windows Server 2012 R2 with [April 2014 update](https://go.microsoft.com/fwlink/?linkid=2073785) and [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  
<sup>3</sup> Supported on Windows 8.1 with [April 2014 update](https://go.microsoft.com/fwlink/?linkid=2073785) and [KB2999226](https://go.microsoft.com/fwlink/?linkid=2074061).  

## ADO support policies  

ADO applications can use the SQLOLEDB OLE DB provider that is included with Windows if they don't require any of the features of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or later.  

ADO applications can use the OLE DB Driver for SQL Server, but if they do so they must specify `DataTypeCompatibility=80` in the connection strings. Only features from [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] are available when `DataTypeCompatibility=80` is present in the connection strings.  

## OLE DB support policies  

Applications can use the OLE DB provider (SQLOLEDB) included with the Windows operating system. However, that is in maintenance mode and no longer updated. Use the OLE DB Driver for SQL Server (MSOLEDBSQL) instead.

## See also  

[Building applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)
