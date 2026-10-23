---
title: "SqlServiceType Property (SqlServiceAdvancedProperty)"
description: "SqlServiceType Property (SqlServiceAdvancedProperty Class)"
author: adbadram
ms.author: adbadram
ms.date: 12/16/2025
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SqlServiceType property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SqlServiceType Property (SqlServiceAdvancedProperty Class)"
apitype: "MOFDef"
---
# SqlServiceType property (SqlServiceAdvancedProperty class)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Gets the type of the managed service associated with the advanced property.

## Syntax

```csharp
object.SetBoolValue(NumValue)
```

## Parts

#### *object*

A [SqlServiceAdvancedProperty Class](sqlserviceadvancedproperty-class.md) object that represents an advanced property.

## Return value

A `uint32` value that specifies the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service type.

## Remarks

Return values can be one of the following:

| Type | Name | Definition |
| --- | --- | --- |
| `1` | `MSSQLSERVER` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service. |
| `2` | `SQLSERVERAGENT` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service. |
| `3` | `MSFTESQL` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Full-Text Search Engine service. |
| `4` | `MsDtsServer` | [!INCLUDE [ssISnoversion](../../../includes/ssisnoversion-md.md)] service. |
| `5` | `MSSQLServerOLAPService` | [!INCLUDE [ssASnoversion](../../../includes/ssasnoversion-md.md)] service. |
| `6` | `ReportServer` | [!INCLUDE [ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service. |
| `7` | `SQLBrowser` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Browser service. |
| `8` | `NsService` | [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Notification Services service. |
| `9` | `MSSQLFDLauncher` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Full-text Filter Daemon Launcher service. |
| `10` | `SQLPBENGINE` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] PolyBase Engine service. |
| `11` | `SQLPBDMS` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] PolyBase Data Movement service. |
| `12` | `MSSQLLaunchpad` | [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Launchpad service. |

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](../../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)
