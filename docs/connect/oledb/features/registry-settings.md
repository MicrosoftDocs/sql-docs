---
description: "Registry Settings"
title: "Registry Settings | Microsoft Docs"
ms.custom: ""
ms.date: "08/31/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: "reference"
author: David-Engel
ms.author: v-davidengel
---
# Registry settings
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Each major version of the OLE DB Driver for SQL Server uses its own set of registry settings. The following are the version specific base registry keys (referred to as `{base_registry_key}` later on):  
- HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI`{major_version}`.0
- HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\MSSQLServer\Client\SNI`{major_version}`.0

Replace the `{major_version}` placeholder in the above keys depending on the major version of the driver, for example: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI19.0` is the base key for versions 19.x.x.

## Encryption and certificate validation

### Force protocol encryption

Encryption can be controlled through the `Value` field of the `{base_registry_key}\GeneralFlags\Flag1` registry entry.  
Valid values are `0`, `1`, or `2` (which maps to `Optional`, `Mandatory`, and `Strict` connection property/keyword values respectively). The OLE DB driver chooses the most secure option between the registry and the connection property/keyword settings.

### Trust server certificate

Certificate validation can be controlled through the `Value` field of the `{base_registry_key}\GeneralFlags\Flag2` registry entry.  
Valid values are `0` or `1`. The OLE DB driver chooses the most secure option between the registry and the connection property/keyword settings. That is, the driver will validate the server certificate as long as at least one of the registry/connection settings enables server certificate validation.

## TCP Keep-Alive and Protocol Order registry properties

For MSOLEDBSQL driver versions 19.1 and above, Protocol Order, TCP Keep-Alive, and Keep-Alive Interval can be adjusted in the following registry entries:

- Protocol Order: `{base_registry_key}`\ProtocolOrder
- TCP Keep-Alive: `{base_registry_key}`\tcp\Property2\Value
- TCP Keep-Alive Interval: `{base_registry_key}`\tcp\Property3\Value

The Protocol Order property is an ordered sequence of null-terminated strings that represent supported protocols. The default Protocol Order value is `sm tcp np`.

The TCP Keep-Alive parameter (in milliseconds) controls how often TCP attempts to verify that an idle connection is still intact by sending a KEEPALIVE packet. The default is 30,000 milliseconds.

The Keep-Alive Interval parameter (in milliseconds) determines the interval separating KEEPALIVE retransmissions until a response is received. The default is 1000 milliseconds.

## See also 
[Encryption and certificate validation](./encryption-and-certificate-validation.md)  
[MSOLEDBSQL major version differences](../major-version-differences.md)  
