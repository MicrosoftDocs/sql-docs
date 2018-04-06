---
title: "OLE DB Driver for SQL Server | Microsoft Docs"
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb"
ms.reviewer: ""
ms.suite: "sql"
description: ""
ms.custom: ""
ms.technology:
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Active"
---
# OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

MSOLEDBSQL, or OLE DB Driver for SQL Server, is a term that is used interchangeably to refer to OLE DB Driver for SQL Server.

## Different generations of OLE DB Drivers

There are three distinct generations of Microsoft OLE DB providers for SQL Server.

### 1. Microsoft OLE DB Provider for SQL Server (SQLOLEDB)
The [Microsoft OLE DB Provider for SQL Server](../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md) (SQLOLEDB) still ships as part of [Windows Data Access Components](https://msdn.microsoft.com/en-us/library/ms692897.aspx). It is not maintained anymore and it is not recommended to use this driver for new development. 


### 2. SQL Server Native Client (SNAC)
Starting in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the [SQL Server Native Client (SNAC)](../../relational-databases/native-client/sql-server-native-client.md) includes an OLE DB provider interface (SQLNCLI) and is the OLE DB provider that shipped with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].

It was [announced as deprecated in 2011](https://blogs.msdn.microsoft.com/sqlnativeclient/2011/08/29/microsoft-is-aligning-with-odbc-for-native-relational-data-access/) and it is not recommended to use this driver for new development. For more information about the SNAC lifecycle and available downloads, refer to [SNAC lifecycle explained](https://blogs.msdn.microsoft.com/sqlreleaseservices/snac-lifecycle-explained/).

### 3. Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL)
OLE DB was [undeprecated](https://blogs.msdn.microsoft.com/sqlnativeclient/2017/10/06/announcing-the-new-release-of-ole-db-driver-for-sql-server/) and released in 2018, and can be downloaded [here](https://go.microsoft.com/fwlink/?linkid=871294).

The new OLE DB provider is called the Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL). The new provider will be updated with the most recent server features going forward.

> [!NOTE]
> To use the new Microsoft OLE DB Driver for SQL Server in existing applications, you should plan to convert your connection strings from SQLOLEDB or SQLNCLI, to MSOLEDBSQL.   

Information on the OLE DB Driver for SQL Server features:

-   [OLE DB Driver for SQL Server Support for LocalDB](../oledb/features/oledb-driver-for-sql-server-support-for-localdb.md)  

-   [Metadata Discovery](../oledb/features/metadata-discovery.md)  

-   [UTF-16 Support in OLE DB Driver for SQL Server](../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)  

-   [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)  

-   [Accessing Diagnostic Information in the Extended Events Log](../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md)  

## See also  
[Install OLE DB Driver for SQL Server](../oledb/applications/installing-oledb-driver-for-sql-server.md)     
[OLE DB Driver for SQL Server Features](../oledb/features/oledb-driver-for-sql-server-features.md )     
