---
title: "Common Language Runtime (CLR) Host Protection Attributes"
description: The CLR provides a mechanism to annotate managed APIs in the .NET Framework with attributes such as SharedState, Synchronization, and ExternalProcessMgmt.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "host protection attributes [CLR integration]"
  - "HostProtectionAttribute [CLR integration]"
  - "common language runtime [SQL Server], host protection attributes"
  - "disallowed types and members [CLR integration]"
  - "common language runtime [SQL Server], disallowed types and members"
  - "HPAs [CLR integration]"
---
# Host protection attributes and CLR integration programming

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The common language runtime (CLR) provides a mechanism to annotate managed application programming interfaces (APIs) that are part of the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)]. These attributes might be of interest to a host of the CLR, such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Examples of such host protection attributes (HPAs) include:

- `SharedState`, which indicates whether the API exposes the ability to create or manage shared state (for example, static class fields).

- `Synchronization`, which indicates whether the API exposes the ability to perform synchronization between threads.

- `ExternalProcessMgmt`, which indicates whether the API exposes a way to control the host process.

Given these attributes, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] specifies a list of HPAs that are disallowed in the hosted environment through code access security (CAS). The CAS requirements are specified by one of three [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] permission sets: `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE`. One of these three security levels is specified when the assembly is registered on the server, using the `CREATE ASSEMBLY` statement. Code executing within the `SAFE` or `EXTERNAL_ACCESS` permission sets must avoid certain types or members that have the `System.Security.Permissions.HostProtectionAttribute` attribute applied. For more information, see [Create an assembly](../clr-integration/assemblies/creating-an-assembly.md) and [CLR integration programming model restrictions](../clr-integration/database-objects/clr-integration-programming-model-restrictions.md).

The `HostProtectionAttribute` isn't a security permission as much as a way to improve reliability, in that it identifies specific code constructs, either types or methods, that the host might disallow. The use of the `HostProtectionAttribute` enforces a programming model that helps protect the stability of the host.

## Host protection attributes

HPAs identify types or members that don't fit the host programming model and represent the following increasing levels of reliability threat:

- Are otherwise benign.
- Could lead to destabilization of server-managed user code.
- Could lead to destabilization of the server process itself.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disallows the use of a type or member that has a `HostProtectionAttribute` that specifies a `System.Security.Permissions.HostProtectionResource` enumeration with a value of `ExternalProcessMgmt`, `ExternalThreading`, `MayLeakOnAbort`, `SecurityInfrastructure`, `SelfAffectingProcessMgmt`, `SelfAffectingThreading`, `SharedState`, `Synchronization`, or `UI`. This prevents the assemblies from calling members that enable sharing state, perform synchronization, might cause a resource leak on termination, or affect the integrity of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process.

### Disallowed types and members

The following articles identify types and members whose `HostProtectionResource` values [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disallows.

The lists in these articles were generated from the supported assemblies. For more information, see [Supported .NET Framework libraries](../clr-integration/database-objects/supported-net-framework-libraries.md).

## In this section

| Article | Description |
| --- | --- |
| [Disallowed types and members in Microsoft.VisualBasic.dll](disallowed-types-and-members-in-microsoft-visualbasic-dll.md) | Lists the types and members in Microsoft.VisualBasic.dll whose HPA values are disallowed. |
| [Disallowed types and members in mscorlib.dll](disallowed-types-and-members-in-mscorlib-dll.md) | Lists the types and members in mscorlib.dll whose HPA values are disallowed. |
| [Disallowed types and members in System.dll](disallowed-types-and-members-in-system-dll.md) | Lists the types and members in System.dll whose HPA values are disallowed. |
| [Disallowed types and members in System.Data.dll](disallowed-types-and-members-in-system-data-dll.md) | Lists the types and members in System.Data.dll whose HPA values are disallowed. |
| [Disallowed types and members in System.Core.dll](disallowed-types-and-members-in-system-core-dll.md) | Lists the types and members in System.Core.dll whose HPA values are disallowed. |

## Related content

- [CLR integration Code Access Security](../clr-integration/security/clr-integration-code-access-security.md)
- [CLR integration programming model restrictions](../clr-integration/database-objects/clr-integration-programming-model-restrictions.md)
- [Create an assembly](../clr-integration/assemblies/creating-an-assembly.md)
