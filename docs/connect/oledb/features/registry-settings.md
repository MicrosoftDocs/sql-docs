---
description: "Registry Settings"
title: "Registry Settings | Microsoft Docs"
ms.custom: ""
ms.date: "08/31/2022"
ms.prod: sql
ms.prod_service:  connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: David-Engel
ms.author: v-davidengel
---
# Registry Settings
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Encryption and Certificate Validation

Each major version of the OLE DB Driver for SQL Server uses its own set of registry settings. The following are the version specific base registry keys (referred to as `{base_registry_key}` later on):  
- HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI`{major_version}`.0\GeneralFlags
- HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\MSSQLServer\Client\SNI`{major_version}`.0\GeneralFlags

Replace the `{major_version}` placeholder in the above keys depending on the major version of the driver, e.g.: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI19.0\GeneralFlags` is the base key for versions 19.x.x.

### Force protocol encryption

Encryption can be controlled through the `Value` field of the `{base_registry_key}\Flag1` registry entry.  
Valid values are `0`, `1`, or `2` (which maps to `Optional`, `Mandatory`, and `Strict` connection property/keyword values respectively). The OLE DB driver chooses the most secure option between the registry and the connection property/keyword settings.

### Trust server certificate

Certificate validation can be controlled through the `Value` field of the `{base_registry_key}\Flag2` registry entry.  
Valid values are `0` or `1`. The OLE DB driver chooses the most secure option between the registry and the connection property/keyword settings. That is, the driver will validate the server certificate as long as at least one of the registry/connection settings enables server certificate validation.

## TCP Keep-Alive and Protocol Order registry properties

For MSOLEDBSQL driver versions 19.1 and above, TCP Keep-Alive, Keep-Alive Interval, and Protocol Order can be adjusted in the following registry entries:
<br/><br/>64 bit:<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI19.0 : ProtocolOrder`<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI19.0\tcp\Property2 : TCPKeepAlive`<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI19.0\tcp\Property3 : KeepALiveInterval`<br/>
<br/>32 bit:<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\MSSQLServer\Client\SNI19.0 : ProtocolOrder`<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\MSSQLServer\Client\SNI19.0\tcp\Property2 : TCPKeepAlive`<br/>`HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\MSSQLServer\Client\SNI19.0\tcp\Property3 : KeepALiveInterval`

## See Also 
[Encryption and certificate validation](./encryption-and-certificate-validation.md)  
[MSOLEDBSQL major version differences](../major-version-differences.md)  
