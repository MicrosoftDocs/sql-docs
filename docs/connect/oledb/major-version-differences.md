---
title: "Major version differences"
description: A description of the differences between the OLE DB Driver 19 for SQL Server and the OLE DB Driver for SQL Server
ms.custom: ""
ms.date: "02/16/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "MSOLEDBSQL, additional resources"
  - "MSOLEDBSQL19, additional resources"
  - "OLE DB Driver for SQL Server, additional resources"
  - "OLE DB Driver 19 for SQL Server, additional resources"
author: David-Engel
ms.author: v-davidengel
---
# Major version differences

## Encryption property changes

In the Microsoft OLE DB Driver 19 for SQL Server there are a number changes made to the encrypt property/connection string keyword. First the keywords for the `encrypt` provider connection string keyword have changed from `no`/`yes` to `Optional`/`Mandatory`/`Strict` and similarly for the `IDataInitialize` connection string the keywords have changed from `true`/`false` to `Optional`/`Mandatory`/`Strict`. The `Optional` value corresponds to the old `no`/`false` values and likewise the `Mandatory` value corresponds to the old `yes`/`true` values. `Strict` is a new value added in version 19.0.0 of the OLE DB Driver for SQL Server and encrypts `PRELOGIN` packets in addition to all other communication with the server. The OLE DB Driver 19 for SQL Server will continue to support all legacy keywords for backwards compatibility.

Second, the default value has changed from `no`/`false` to `Mandatory`. This change means that encrypted communicated is set by default. Previously, the driver would default to `no`/`false` and would only encrypt connections if explicitly set by the user and/or mandated by the SQL Server when the server side property `Force Encryption` was set to `yes`. To use old default behavior, include `Encrypt=Optional;` in the provider connection string, or `Use Encryption for Data=Optional;` in the `IDataInitialize` connection string.

## Driver name changes

The new Microsoft OLE DB Driver 19 for SQL Server supports side by side installation with the older Microsoft OLE DB Driver for SQL Server. To be able to differentiate the drivers, the name was changed to include the major version number. To use the new driver in an application the user must specify the new driver name. When connecting through the `IDBInitialize` interface, the call to `CoCreateInstance` must use the value `CLSID_MSOLEDBSQL19` as the CLSID to specify use of the OLE DB Driver 19 for SQL Server. For example:

```cpp
CoCreateInstance(CLSID_MSOLEDBSQL19,
                 nullptr,
                 CLSCTX_INPROC_SERVER,
                 IID_IDBInitialize,
                 static_cast<void**>(&pIDBInitialize))
```
To connect to the old driver with this method use `CLSID_MSOLEDBSQL`.

Connections through the `IDataInitialize` interface replace the value of the `Provider` keyword with `MSOLEDBSQL19` to specify the driver Microsoft OLE DB Driver 19 for SQL Server.

Similarly in GUI interfaces data link properties or linked server setup in SSMS "Microsoft OLE DB Driver 19 for SQL Server" must be selected from the list of installed providers.

## See also
[OLE DB Driver for SQL Server](../oledb/oledb-driver-for-sql-server.md)  
  