---
title: "Disallowed Types and Members in System.Data.dll"
description: SQL Server CLR programming disallows a type or member with certain values for the HostProtectionResource enum. This article lists System.Data.dll disallowed values.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "host protection attributes [CLR integration]"
  - "common language runtime [SQL Server], host protection attributes"
---
# Disallowed types and members in System.Data.dll

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] common language integration (CLR) programming disallows the use of a type or member that has a `HostProtectionAttribute` that specifies a `System.Security.Permissions.HostProtectionResource` enumeration with a value of `ExternalProcessMgmt`, `ExternalThreading`, `MayLeakOnAbort`, `SecurityInfrastructure`, `SelfAffectingProcessMgmt`, `SelfAffectingThreading`, `SharedState`, `Synchronization`, or `UI`. The following table lists the members and types of the System.Data.dll assembly whose Host Protection Attribute (HPA) values are disallowed.

> [!NOTE]  
> This list was generated from the supported assemblies. For more information, see [Supported .NET Framework libraries](../clr-integration/database-objects/supported-net-framework-libraries.md).

| Type or member | HPA values |
| --- | --- |
| `System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery()` | `ExternalThreading` |
| `System.Data.SqlClient.SqlCommand.BeginExecuteReader()` | `ExternalThreading` |
| `System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader()` | `ExternalThreading` |
| `System.Data.SqlClient.SqlDependency..ctor()` | `ExternalThreading` |
| `System.Data.SqlClient.SqlDependency.Start()` | `ExternalThreading` |
| `System.Data.SqlClient.SqlDependency.Stop()` | `ExternalThreading` |
| `System.Data.TypedDataSetGenerator` | `SharedState`, `Synchronization` |
| `System.Xml.XmlDataDocument` | `Synchronization` |

## Related content

- [Host protection attributes and CLR integration programming](host-protection-attributes-and-clr-integration-programming.md)
- [Disallowed types and members in Microsoft.VisualBasic.dll](disallowed-types-and-members-in-microsoft-visualbasic-dll.md)
- [Disallowed types and members in mscorlib.dll](disallowed-types-and-members-in-mscorlib-dll.md)
- [Disallowed types and members in System.dll](disallowed-types-and-members-in-system-dll.md)
- [Disallowed types and members in System.Core.dll](disallowed-types-and-members-in-system-core-dll.md)
