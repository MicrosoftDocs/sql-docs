---
title: "Understanding Security Policies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "code groups [Reporting Services]"
  - "code access security [Reporting Services], security policies"
  - "Execution permission set"
  - "custom assemblies [Reporting Services], security"
  - "permission sets [Reporting Services]"
  - "expressions [Reporting Services], security"
  - "evidence [Reporting Services]"
  - "FullTrust permission set"
  - "security policies [Reporting Services]"
  - "named permission sets [Reporting Services]"
ms.assetid: a9bf043a-139a-4929-9a58-244815323df0
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Understanding Security Policies
  Any code that is executed by a report server must be part of a specific code access security policy. These security policies consist of code groups that map evidence to a set of named permission sets. Often, code groups are associated with a named permission set that specifies the allowable permissions for code in that group. The runtime uses evidence provided by a trusted host or by the loader to determine which code groups the code belongs to and, therefore, which permissions to grant the code. [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] adheres to this security policy architecture as defined by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] common language runtime (CLR). The following sections describe the various types of code in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and the policy rules associated with them.  
  
## Report Server Assemblies  
 Report server assemblies are those that contain code that is part of the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] product. [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] is written using managed code assemblies; all of these assemblies are strong-named (that is, digitally signed). The code groups for these assemblies are defined using the **StrongNameMembershipCondition**, which provides evidence based on public key information for the assembly's strong name. The code group is granted the **FullTrust** permission set.  
  
## Report Server Extensions (Rendering, Data, Delivery, and Security)  
 Report server extensions are custom data, delivery, rendering, and security extensions that you or other third-parties create in order to extend the functionality of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. You must grant **FullTrust** to these extensions or assembly code in the policy configuration files associated with the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] component you are extending. Extensions shipped as a part of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] are signed with the report server public key and receive the **FullTrust** permission set.  
  
> [!IMPORTANT]  
>  You must modify the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] policy configuration files to allow **FullTrust** for any third-party extensions. If you do not add a code group with **FullTrust** for your custom extensions, they cannot be used by the report server.  
  
 For more information about the policy configuration files in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], see [Using Reporting Services Security Policy Files](using-reporting-services-security-policy-files.md).  
  
## Expressions Used in Reports  
 Report expressions are inline code expressions or user-defined methods contained within the **Code** element of a report definition language file. There is a code group that is already configured in the policy files that grants these expressions the **Execution** permission set by default. The code group looks like the following:  
  
```  
<CodeGroup  
   class="UnionCodeGroup"  
   version="1"  
   PermissionSetName="Execution"  
   Name="Report_Expressions_Default_Permissions"  
   Description="This code group grants default permissions for code in report expressions and Code element. ">  
    <IMembershipCondition  
       class="StrongNameMembershipCondition"  
       version="1"  
       PublicKeyBlob="002400..."  
    />  
</CodeGroup>  
```  
  
 **Execution** permission allows code to run (execute), but not to use protected resources. All expressions found within a report are compiled into an assembly (called an "expression host" assembly) that is stored as a part of the compiled report. When the report is executed, the report server loads the expression host assembly and makes calls into that assembly to execute expressions. Expression host assemblies are signed with a specific key that is used to define the code group for all expression hosts.  
  
 Report expressions reference report object model collections (fields, parameters, etc.) and perform simple tasks like arithmetic and string operations. Code that performs these simple operations only requires **Execution** permission. By default, user-defined methods in the **Code** element and any custom assemblies are granted **Execution** permission in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. Thus, for most expressions, the current configuration does not require that you modify any security policy files. To grant additional permissions to expression host assemblies, an administrator needs to modify the policy configuration files of the report server and Report Designer, and change the report expressions code group. Because it is a global setting, changing default permissions for the expression hosts affects all reports. For this reason, it is highly recommended that you place all code that requires additional security into a custom assembly. Only this assembly will be granted the permissions you need.  
  
> [!IMPORTANT]  
>  Code that calls external assemblies or protected resources should be incorporated into a custom assembly for use in reports. Doing so gives you more control over the permissions requested and asserted by your code. You should not make calls to secure methods within the **Code** element. Doing so requires you to grant **FullTrust** to the report expression host and grants all custom code full access to the CLR.  
  
> [!CAUTION]  
>  Do not grant **FullTrust** to the code group for a report expression host. If you do, you enable all report expressions to make protected system calls.  
  
## Custom Assemblies Referenced in Reports  
 Some report expressions can call third-party code assemblies, also known in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] as custom assemblies. The report server expects these assemblies to have at least **Execution** permission in the policy configuration files. By default, policy files that ship with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] grant **Execution** permission to all assemblies starting from the 'My Computer' zone. You can grant additional permissions to custom assemblies as needed.  
  
 In some cases, you may need to perform an operation that requires specific code permissions in a report expression. Typically, this means that a report expression needs to make a call to a secured CLR library method (such as one that accesses files or the system registry). The [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] documentation describes the code permissions that are required to make this secure call; to execute the call, the calling code must be granted these specific, secure permissions. If you make the call from a report expression or the **Code** element, the expression host assembly must be granted the appropriate permissions. However, once you grant the expression host the permissions, all code that runs in any expression in any report is now granted that specific permission. It is much more secure to make the call from a custom assembly and grant that custom assembly the specific permissions.  
  
## See Also  
 [Code Access Security in Reporting Services](code-access-security-in-reporting-services.md)   
 [Secure Development &#40;Reporting Services&#41;](secure-development-reporting-services.md)  
  
  
