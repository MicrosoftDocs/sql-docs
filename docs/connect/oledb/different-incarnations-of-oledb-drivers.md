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
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Active"
---

## Different incarnations of OLE DB drivers

There are three distinct incarnations of Microsoft OLE DB providers for SQL Server.


### 1. Microsoft OLE DB Provider for SQL Server (SQLOLEDB)

The Microsoft OLE DB Provider for SQL Server (SQLOLEDB) still ships as part of [Windows Data Access Components](https://go.microsoft.com/fwlink/?LinkId=107907). It is not recommended to use this driver for new development.


### 2. SQL Server Native Client (SNAC)

Starting in SQL Server 2005, the [SQL Server Native Client (SNAC)](../../relational-databases/native-client/sql-server-native-client.md) includes an OLE DB provider interface (SQLNCLI) and is the OLE DB provider that shipped with SQL Server 2005 through SQL Server 2017.

It was [announced as deprecated in 2011](https://blogs.msdn.microsoft.com/sqlnativeclient/2011/08/29/microsoft-is-aligning-with-odbc-for-native-relational-data-access/) and it is not recommended to use this driver for new development.


### 3. Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL)

OLE DB was [announced as undeprecated in 2017](https://blogs.msdn.microsoft.com/sqlnativeclient/2017/10/06/announcing-the-new-release-of-ole-db-driver-for-sql-server/). A new planned release was announced for 2018.

The new OLE DB provider is called the [Microsoft OLE DB Driver for SQL Server](oledb-driver-for-sql-server.md) (MSOLEDBSQL). The new provider will be updated with the most recent server features going forward.
