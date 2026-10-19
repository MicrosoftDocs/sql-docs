---
title: CLR Integration Code Access Security
description: For SQL Server CLR integration, CLR supports code access security for managed code, where permissions are granted to assemblies based on code identity.
author: uc-msft
ms.author: umajay
ms.date: 07/23/2025
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "UNSAFE assemblies"
  - "permissions [CLR integration]"
  - "common language runtime [SQL Server], security"
  - "SAFE assemblies"
  - "code access security [CLR integration]"
  - "EXTERNAL_ACCESS assemblies"
---
# CLR integration code access security

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The common language runtime (CLR) supports a security model called code access security for managed code. In this model, permissions are granted to assemblies based on the identity of the code. For more information, see [Code Access Security](/previous-versions/dotnet/framework/code-access-security/code-access-security).

The security policy that determines the permissions granted to assemblies is defined in three different places:

- **Machine policy**: This policy is in effect for all managed code running in the machine on which [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is installed.

- **User policy**: This policy is in effect for managed code hosted by a process. For [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], the user policy is specific to the Windows account on which the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service is running.

- **Host policy**: This policy is set up by the host of the CLR (in this case, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]) that is in effect for managed code running in that host.

The code access security mechanism supported by the CLR is based on the assumption that the runtime can host both fully trusted and partially trusted code. The resources that are protected by CLR code access security are typically wrapped by managed application programming interfaces that require the corresponding permission before allowing access to the resource. The demand for the permission is satisfied only if all the callers (at the assembly level) in the call stack have the corresponding resource permission.

The set of code access security permissions that are granted to managed code when running inside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is the intersection of the set of permissions granted by the previous three policy levels. Even if [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] grants a set of permissions to an assembly loaded in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], the eventual set of permissions given to user code might be further restricted by the user and machine-level policies.

## Code access security no longer supported

[!INCLUDE [code-access-security](../../../database-engine/includes/code-access-security.md)]

## SQL Server host policy level permission sets

The set of code access security permissions granted to assemblies by the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] host policy level is determined by the permission set specified when creating the assembly. There are three permission sets: `SAFE`, `EXTERNAL_ACCESS`, and `UNSAFE` (specified using the `PERMISSION_SET` option of [CREATE ASSEMBLY](../../../t-sql/statements/create-assembly-transact-sql.md)).

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] supplies a host-level security policy level to the CLR while hosting it. This policy is an extra policy level below the two policy levels that are always in effect. This policy is set for every application domain that is created by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. This policy isn't meant for the default application domain that would be in effect when [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] creates an instance of the CLR.

The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] host-level policy is a combination of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] fixed policy for system assemblies and user-specified policy for user assemblies.

The fixed policy for CLR assemblies and [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] system assemblies grants them full trust.

The user-specified portion of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] host policy is based on the assembly owner specifying one of three permission buckets for each assembly. For more information about the following security permissions, see the .NET Framework SDK.

### SAFE

Only internal computation and local data access are allowed. `SAFE` is the most restrictive permission set. Code executed by an assembly with `SAFE` permissions can't access external system resources such as files, the network, environment variables, or the registry.

`SAFE` assemblies have the following permissions and values:

| Permission | Values / Description |
| --- | --- |
| `SecurityPermission` | `Execution`: Permission to execute managed code. |
| `SqlClientPermission` | `Context connection = true`, `context connection = yes`: Only the context-connection can be used, and the connection string can only specify a value of `context connection=true` or `context connection=yes`.<br /><br />`AllowBlankPassword = false`: Blank passwords aren't permitted. |

### EXTERNAL_ACCESS

`EXTERNAL_ACCESS` assemblies have the same permissions as `SAFE` assemblies, with the additional ability to access external system resources such as files, networks, environmental variables, and the registry.

`EXTERNAL_ACCESS` assemblies also have the following permissions and values:

| Permission | Values / Description |
| --- | --- |
| `DistributedTransactionPermission` | `Unrestricted`: Distributed transactions are allowed. |
| `DNSPermission` | `Unrestricted`: Permission to request information from Domain Name Servers. |
| `EnvironmentPermission` | `Unrestricted`: Full access to system and user environment variables is allowed. |
| `EventLogPermission` | `Administer`: The following actions are allowed: create an event source, read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs. |
| `FileIOPermission` | `Unrestricted`: Full access to files and folders is allowed. |
| `KeyContainerPermission` | `Unrestricted`: Full access to key containers is allowed. |
| `NetworkInformationPermission` | `Access`: Pinging is permitted. |
| `RegistryPermission` | Allows read rights to `HKEY_CLASSES_ROOT`, `HKEY_LOCAL_MACHINE`, `HKEY_CURRENT_USER`, `HKEY_CURRENT_CONFIG`, and `HKEY_USERS.` |
| `SecurityPermission` | `Assertion`: Ability to assert that all the callers of this code have the requisite permission for the operation.<br /><br />`ControlPrincipal`: Ability to manipulate the principal object.<br /><br />`Execution`: Permission to execute managed code.<br /><br />`SerializationFormatter`: Ability to provide serialization services. |
| `SmtpPermission` | `Access`: Outbound connections to SMTP host port 25 are allowed. |
| `SocketPermission` | `Connect`: Outbound connections (all ports, all protocols) on a transport address are allowed. |
| `SqlClientPermission` | `Unrestricted`: Full access to the datasource is allowed. |
| `StorePermission` | `Unrestricted`: Full access to X.509 certificate stores is allowed. |
| `WebPermission` | `Connect`: Outbound connections to web resources are allowed. |

### UNSAFE

`UNSAFE` allows assemblies unrestricted access to resources, both within and outside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. Code executing from within an `UNSAFE` assembly can also call unmanaged code.

`UNSAFE` assemblies are given `FullTrust`.

## Recommended permission settings

`SAFE` is the recommended permission setting for assemblies that perform computation and data management tasks without accessing resources outside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

`EXTERNAL_ACCESS` is recommended for assemblies that access resources outside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. `EXTERNAL_ACCESS` assemblies by default execute as the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account. It's possible for `EXTERNAL_ACCESS` code to explicitly impersonate the caller's Windows Authentication security context. Since the default is to execute as the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account, permission to execute `EXTERNAL_ACCESS` should only be given to logins trusted to run as the service account.

From a security perspective, `EXTERNAL_ACCESS` and `UNSAFE` assemblies are identical. However, `EXTERNAL_ACCESS` assemblies provide various reliability and robustness protections that aren't in `UNSAFE` assemblies.

Specifying `UNSAFE` allows the code in the assembly to perform illegal operations against the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] process space, and hence can potentially compromise the robustness and scalability of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information about creating CLR assemblies in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], see [Manage CLR integration assemblies](../assemblies/managing-clr-integration-assemblies.md).

> [!IMPORTANT]  
> [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] contains CLR assemblies that the [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] uses to provide certain functionality. The `Microsoft.SQLServer.Types` assembly that is included with SQL Server installation appears in the metadata as an `UNSAFE` assembly. This is by design. These assemblies are considered trusted & secure by default.

## Access external resources

If a user-defined type (UDT), stored procedure, or other type of construct assembly is registered with the `SAFE` permission set, then managed code executing in the construct is unable to access external resources. However, if either the `EXTERNAL_ACCESS` or `UNSAFE` permission sets are specified, and managed code attempts to access external resources, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] applies the following rules:

| If | Then |
| --- | --- |
| The execution context corresponds to a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] login. | Attempts to access external resources are denied and a security exception is raised. |
| The execution context corresponds to a Windows login and the execution context is the original caller. | The external resource is accessed under the security context of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service account. |
| The caller isn't the original caller. | Access is denied and a security exception is raised. |
| The execution context corresponds to a Windows login and the execution context is the original caller and the caller is impersonated. | Access uses the caller security context, and not the service account. |

## Permission set summary

The following chart summarizes the restrictions and permissions granted to the `SAFE`, `EXTERNAL_ACCESS`, and `UNSAFE` permission sets.

| Functionality | `SAFE` | `EXTERNAL_ACCESS` | `UNSAFE` |
| --- | --- | --- | --- |
| Code Access Security permissions | Execute only | Execute + access to external resources | Unrestricted (including P/Invoke) |
| Programming model restrictions | Yes | Yes | No restrictions |
| Verifiability requirement | Yes | Yes | No |
| Local data access | Yes | Yes | Yes |
| Ability to call native code | No | No | Yes |

## Related content

- [CLR integration security](clr-integration-security.md)
- [Host protection attributes and CLR integration programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md)
- [CLR integration programming model restrictions](../database-objects/clr-integration-programming-model-restrictions.md)
- [CLR integration architecture - CLR hosted environment](../clr-integration-architecture-clr-hosted-environment.md)
