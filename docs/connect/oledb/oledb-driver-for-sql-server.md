---
title: "OLE DB Driver for SQL Server | Microsoft Docs"
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb"
ms.reviewer: ""
ms.suite: "sql"
description: ""
ms.custom: ""
ms.technology:
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Active"
---
# OLE DB Driver for SQL Server
There are three distinct incarnations of Microsoft OLE DB providers for SQL Server. The first "Microsoft OLE DB Provider for SQL Server" (SQLOLEDB) still ships as part of [Windows Data Access Components](#microsoft-or-windows-data-access-components). It is not recommended to use this driver for new development. Starting in SQL Server 2005, the [SQL Server Native Client](#sql-server-native-client) includes an OLE DB provider interface (SQLNCLI) and is the OLE DB provider that shipped with SQL Server 2005 through SQL Server 2017. It was [announced as deprecated in 2011](https://blogs.msdn.microsoft.com/sqlnativeclient/2011/08/29/microsoft-is-aligning-with-odbc-for-native-relational-data-access/) and it is not recommended to use this driver for new development. In 2017, OLE DB was subsequently [undeprecated and a new planned release was announced](https://blogs.msdn.microsoft.com/sqlnativeclient/2017/10/06/announcing-the-new-release-of-ole-db-driver-for-sql-server/) for 2018. The new OLE DB provider is called the "Microsoft OLE DB Driver for SQL Server" (MSOLEDBSQL) and will be updated with the most recent server features going forward.

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

MSOLEDBSQL, or OLE DB Driver for SQL Server, is a term that has been used interchangeably to refer to OLE DB Driver for SQL Server.

Information on the OLE DB Driver for SQL Server features:

-   [OLE DB Driver for SQL Server Support for LocalDB](../oledb/features/oledb-driver-for-sql-server-support-for-localdb.md)  

-   [Metadata Discovery](../oledb/features/metadata-discovery.md)  

-   [UTF-16 Support in OLE DB Driver for SQL Server](../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)  

-   [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)  

-   [Accessing Diagnostic Information in the Extended Events Log](../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md)  

## See also  
[Install OLE DB Driver for SQL Server](../oledb/applications/installing-oledb-driver-for-sql-server.md)  
 [OLE DB Driver for SQL Server Features](../oledb/features/oledb-driver-for-sql-server-features.md )  
