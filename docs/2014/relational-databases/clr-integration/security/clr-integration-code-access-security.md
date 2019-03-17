---
title: "CLR Integration Code Access Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "UNSAFE assemblies"
  - "permissions [CLR integration]"
  - "common language runtime [SQL Server], security"
  - "SAFE assemblies"
  - "code access security [CLR integration]"
  - "EXTERNAL_ACCESS assemblies"
ms.assetid: 2111cfe0-d5e0-43b1-93c3-e994ac0e9729
author: rothja
ms.author: jroth
manager: craigg
---
# CLR Integration Code Access Security
  The common language runtime (CLR) supports a security model called code access security for managed code. In this model, permissions are granted to assemblies based on the identity of the code. For more information, see the "Code Access Security" section in the .NET Framework software development kit.  
  
 The security policy that determines the permissions granted to assemblies is defined in three different places:  
  
-   Machine policy: This is the policy in effect for all managed code running in the machine on which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is installed.  
  
-   User policy: This is the policy in effect for managed code hosted by a process. For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running.  
  
-   Host policy: This is the policy set up by the host of the CLR (in this case, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]) that is in effect for managed code running in that host.  
  
 The code access security mechanism supported by the CLR is based on the assumption that the runtime can host both fully trusted and partially trusted code. The resources that are protected by CLR code access security are typically wrapped by managed application programming interfaces that requirethe corresponding permission before allowing access to the resource. The demandfor the permission is satisfied only if all the callers (at the assembly level) in the call stack have the corresponding resource permission.  
  
 The set of code access security permissions that are granted to managed code when running inside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] grants a set of permissions to an assembly loaded in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the eventual set of permissions given to user code may be restricted further by the user and machine-level policies.  
  
## SQL Server Host Policy Level Permission Sets  
 The set of code access security permissions granted to assemblies by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] host policy level is determined by the permission set specified when creating the assembly. There are three permission sets: `SAFE`, `EXTERNAL_ACCESS` and `UNSAFE` (specified using the **PERMISSION_SET** option of[CREATE ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-assembly-transact-sql)).  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This policy is not meant for the default application domain that would be in effect when [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] creates an instance of the CLR.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] fixedpolicy for system assemblies and user-specified policy for user assemblies.  
  
 The fixed policy for CLR assemblies and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] system assemblies grants them full trust.  
  
 The user-specified portion of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] host policy is based on the assembly owner specifying one of three permission buckets for each assembly. For more information about the security permissions listed below, see the .NET Framework SDK.  
  
### SAFE  
 Only internal computation and local data access are allowed. `SAFE` is the most restrictive permission set. Code executed by an assembly with `SAFE` permissions cannot access external system resources such as files, the network, environment variables, or the registry.  
  
 `SAFE` assemblies have the following permissions and values:  
  
|Permission|Value(s)/Description|  
|----------------|-----------------------------|  
|`SecurityPermission`|`Execution:` Permission to execute managed code.|  
|`SqlClientPermission`|`Context connection = true`, `context connection = yes`: Only the context-connection can be used and the connection string can only specify a value of "context connection=true" or "context connection=yes".<br /><br /> **AllowBlankPassword = false:**  Blank passwords are not permitted.|  
  
### EXTERNAL_ACCESS  
 EXTERNAL_ACCESS assemblies have the same permissions as `SAFE` assemblies, with the additional ability to access external system resources such as files, networks, environmental variables, and the registry.  
  
 `EXTERNAL_ACCESS` assemblies also have the following permissions and values:  
  
|Permission|Value(s)/Description|  
|----------------|-----------------------------|  
|`DistributedTransactionPermission`|`Unrestricted:` Distributed transactions are allowed.|  
|`DNSPermission`|`Unrestricted:` Permission to request information from Domain Name Servers.|  
|`EnvironmentPermission`|`Unrestricted:` Full access to system and user environment variables is allowed.|  
|`EventLogPermission`|`Administer:` The following actions are allowed: creating an event source, reading existing logs, deleting event sources or logs, responding to entries, clearing an event log, listening to events, and accessing a collection of all event logs.|  
|`FileIOPermission`|`Unrestricted:` Full access to files and folders is allowed.|  
|`KeyContainerPermission`|`Unrestricted:` Full access to key containers is allowed.|  
|`NetworkInformationPermission`|`Access:` Pinging is permitted.|  
|`RegistryPermission`|Allows read rights to `HKEY_CLASSES_ROOT`, `HKEY_LOCAL_MACHINE`, `HKEY_CURRENT_USER`, `HKEY_CURRENT_CONFIG`, and `HKEY_USERS.`|  
|`SecurityPermission`|`Assertion:` Ability to assert that all the callers of this code have the requisite permission for the operation.<br /><br /> `ControlPrincipal:` Ability to manipulate the principal object.<br /><br /> `Execution:` Permission to execute managed code.<br /><br /> `SerializationFormatter:` Ability to provide serialization services.|  
|**SmtpPermission**|`Access:` Outbound connections to SMTP host port 25 are allowed.|  
|`SocketPermission`|`Connect:` Outbound connections (all ports, all protocols) on a transport address are allowed.|  
|`SqlClientPermission`|`Unrestricted:` Full access to the datasource is allowed.|  
|`StorePermission`|`Unrestricted:` Full access to X.509 certificate stores is allowed.|  
|`WebPermission`|`Connect:` Outbound connections to web resources are allowed.|  
  
### UNSAFE  
 UNSAFE allows assemblies unrestricted access to resources, both within and outside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Code executing from within an `UNSAFE` assembly can also call unmanaged code.  
  
 `UNSAFE` assemblies are given `FullTrust`.  
  
> [!IMPORTANT]  
>  `SAFE` is the recommended permission setting for assemblies that perform computation and data management tasks without accessing resources outside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. `EXTERNAL_ACCESS` assemblies by default execute as the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account, permission to execute `EXTERNAL_ACCESS` should only be given to logins trusted to run as the service account. From a security perspective, `EXTERNAL_ACCESS` and `UNSAFE` assemblies are identical. However, `EXTERNAL_ACCESS` assemblies provide various reliability and robustness protections that are not in `UNSAFE` assemblies. Specifying `UNSAFE` allows the code in the assembly to perform illegal operations against the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information about creating CLR assemblies in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Managing CLR Integration Assemblies](../../../relational-databases/clr-integration/assemblies/managing-clr-integration-assemblies.md).  
  
## Accessing External Resources  
 If a user-defined type (UDT), stored procedure, or other type of construct assembly is registered with the `SAFE` permission set, then managed code executing in the construct is unable to access external resources. However, if either the `EXTERNAL_ACCESS` or `UNSAFE` permission sets are specified, and managed code attempts to access external resources, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applies the following rules:  
  
|If|Then|  
|--------|----------|  
|The execution context corresponds to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login.|Attempts to access external resources are denied and a security exception is raised.|  
|The execution context corresponds to a Windows login and the execution context is the original caller.|The external resource is accessed under the security context of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account.|  
|The caller is not the original caller.|Access is denied and a security exception is raised.|  
|The execution context corresponds to a Windows login and the execution context is the original caller and the caller has been impersonated.|Access uses the caller security context; not the service account.|  
  
## Permission Set Summary  
 The following chart summarizes the restrictions and permissions granted to the `SAFE`, `EXTERNAL_ACCESS`, and `UNSAFE` permission sets.  
  
|||||  
|-|-|-|-|  
||`SAFE`|`EXTERNAL_ACCESS`|`UNSAFE`|  
|`Code Access Security Permissions`|Execute only|Execute + access to external resources|Unrestricted (including P/Invoke)|  
|`Programming model restrictions`|Yes|Yes|No restrictions|  
|`Verifiability requirement`|Yes|Yes|No|  
|`Local data access`|Yes|Yes|Yes|  
|`Ability to call native code`|No|No|Yes|  
  
## See Also  
 [CLR Integration Security](clr-integration-security.md)   
 [Host Protection Attributes and CLR Integration Programming](../../clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md)   
 [CLR Integration Programming Model Restrictions](../../../relational-databases/clr-integration/database-objects/clr-integration-programming-model-restrictions.md)   
 [CLR Hosted Environment](../clr-integration-architecture-clr-hosted-environment.md)  
  
  
