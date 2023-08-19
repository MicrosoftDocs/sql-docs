---
title: "Files and version numbers"
description: "File and version number information for SQL Management Objects (SMO)"
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: randolphwest
ms.date: 08/08/2023
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "SQL Server Management Objects, versions"
  - "components [SMO]"
  - "files [SMO], components"
  - "SMO [SQL Server], versions"
  - "versions [SMO]"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Files and version numbers

[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

All required SQL Server Management Object (SMO) components are included in the `Microsoft.SqlServer.SqlManagementObjects` NuGet package. SMO is implemented in several managed assemblies. You can develop SMO applications on either a client or a server.

## Remarks

The file version of the SMO assemblies is displayed as *Major*.**0**.*Build*.*Revision*. But the embedded assembly version is *Major*.**100**.*Build*.*Revision*. This is done to keep the version of SMO used in each application separate, so updates to one don't affect any others.

For this reason, don't install the NuGet version of SMO assemblies to the Global Assembly Cache (GAC). Doing so could cause other applications, including [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, to break.

## File and version information

| File | Description |
| --- | --- |
| `Microsoft.SqlServer.ConnectionInfo.dll` | Contains support for connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `Microsoft.SqlServer.ServiceBrokerEnum.dll` | Contains support for programming the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Service Broker. This is required only in programs that access the Service Broker. |
| `Microsoft.SqlServer.Smo.dll` | Contains the most of the SMO classes. |
| `Microsoft.SqlServer.SmoExtended.dll` | Contains support for the SMO classes. |
| `Microsoft.SqlServer.Management.Sdk.Sfc.dll` | Contains support for the SMO classes. |
| `Microsoft.SqlServer.SqlEnum.dll` | Contains support for the SMO classes. |
| `Microsoft.SqlServer.WmiEnum.dll` | Contains the Windows Management Instrumentation (WMI) Provider classes. This is required only for programs that use the WMI Provider classes. |
| `Microsoft.SqlServer.RegSvrEnum.dll` | Contains the Registered Server classes. This is required only for programs that use the Registered Server classes. |
