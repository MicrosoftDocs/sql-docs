---
title: "Code Access Security in Reporting Services | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "code groups [Reporting Services]"
  - "code access security [Reporting Services]"
  - "permission sets [Reporting Services]"
  - "evidence [Reporting Services]"
  - "code access security [Reporting Services], about code access security"
  - "named permission sets [Reporting Services]"
ms.assetid: 97480368-1fc3-4c32-b1b0-63edfb54e472
author: maggiesMSFT
ms.author: maggies
---
# Code Access Security in Reporting Services
  Code access security centers on these core concepts: evidence, code groups, and named permission sets. In [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], the Report Manager, Report Designer, and Report Server components each have a policy file that configures code access security for custom assemblies as well as data, delivery, rendering, and security extensions. The following sections provide an overview of code access security. For more detailed information about the topics covered in this section, see "Security Policy Model" in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK documentation.  
  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses code access security because, although the report server is built on [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] technology, there is a substantial difference between a typical [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] application and the report server. A typical [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] application does not execute user code. In contrast, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses an open and extensible architecture that allows users to program against the report definition files using the **Code** element of the Report Definition Language and to develop specialized functionality into a custom assembly for use in reports. Furthermore, developers can design and deploy powerful extensions that enhance the capabilities of the report server. With this power and flexibility comes the need to provide as much protection and security as possible.  
  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] developers can use any [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly in their reports and natively call upon all of the functionality of assemblies deployed to the global assembly cache. The only thing that the report server can control is what permissions are given for report expressions and loaded custom assemblies. In [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], custom assemblies receive **Execute**-only permissions by default.  
  
## Evidence  
 Evidence is the information that the common language runtime (CLR) uses to determine a security policy for code assemblies. Evidence indicates to the runtime that code has a particular characteristic. Common forms of evidence include digital signatures and the location of an assembly. Evidence can also be custom designed to represent other information that is meaningful to the application.  
  
 Both assemblies and application domains receive permissions based on evidence. For example, the location of an assembly that [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] is attempting to access is one common form of evidence for weak-named assemblies. This is known as URL evidence. URL evidence for a custom data processing extension deployed to a report server might be "C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer\bin\Microsoft.Samples.ReportingServices.FsiDataExtension.dll". The strong name or digital signature of an assembly is another common form of evidence. In this case, the evidence is the public key information for an assembly.  
  
## Code Groups  
 A code group is a logical grouping of code that has a specified condition for membership. Any code that meets the membership condition is included in the group. Administrators configure a security policy by managing code groups and their associated permission sets.  
  
 A membership condition for a code group is based on evidence. For example, a URL membership for a code group is based on URL evidence. The common language runtime (CLR) uses identifying characteristics such as URL evidence to describe the code and to determine whether a group's membership condition has been met. For example, if the membership condition of a code group is "code in the assembly C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer\bin\Microsoft.Samples.ReportingServices.FsiDataExtension.dll", the runtime examines the evidence to determine whether the code originates from that location. An example of a configuration entry for this type of code group might look like the following:  
  
```  
<CodeGroup class="UnionCodeGroup"  
   version="1"  
   PermissionSetName="FullTrust"  
   Name="MyCodeGroup"  
   Description="Code group for my data processing extension">  
      <IMembershipCondition class="UrlMembershipCondition"  
         version="1"  
         Url="C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer\bin\Microsoft.Samples.ReportingServices.FsiDataExtension.dll"  
       />  
</CodeGroup>  
```  
  
 You should work with your system administrator or application deployment expert to determine the type of code access security and code groups that your custom assemblies or [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] extensions require.  
  
## Named Permission Sets  
 A named permission set is a set of permissions that administrators can associate with a code group. Most named permission sets consist of at least one permission, a name, and description for the permission set. Administrators can use named permission sets to establish or modify the security policy for code groups. More than one code group can be associated with the same named permission set. The CLR provides built-in named permission sets; among these are **Nothing**, **Execution**, **Internet**, **LocalIntranet**, **Everything**, and **FullTrust**.  
  
> [!NOTE]  
>  Custom data, delivery, rendering, and security extensions in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] must run under the **FullTrust** permission set. Work with your system administrator to add the appropriate code group and membership conditions for your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] extensions.  
  
 You can associate your own custom levels of permissions for custom assemblies that you use with reports. For example, if you want to allow an assembly to access a specific file, you can create a new named permission set with specific file I/O access and then assign the permission set to your code group. The following permission set grants read-only access to the file MyFile.xml:  
  
```  
<PermissionSet class="NamedPermissionSet"  
   version="1"  
   Name="MyNewFilePermissionSet"  
   Description="A special permission set that grants read access to my file.">  
    <IPermission class="FileIOPermission"  
       version="1"  
       Read="C:\MyFile.xml"/>  
    <IPermission class="SecurityPermission"  
       version="1"  
       Flags="Assertion, Execution"/>  
</PermissionSet>  
```  
  
 A code group that you grant this permission set might look like the following:  
  
```  
<CodeGroup class="UnionCodeGroup"  
   version="1"  
   PermissionSetName="MyNewFilePermissionSet"  
   Name="MyNewCodeGroup"  
   Description="A special code group for my custom assembly.">  
   <IMembershipCondition class="UrlMembershipCondition"  
      version="1"  
      Url="C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer\bin\MyCustomAssembly.dll"/>  
</CodeGroup>  
```  
  
## See Also  
 [Secure Development &#40;Reporting Services&#41;](../../../reporting-services/extensions/secure-development/secure-development-reporting-services.md)  
  
  
