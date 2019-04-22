---
title: "Using Reporting Services Security Policy Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "code groups [Reporting Services]"
  - "CodeGroup elements"
  - "configuration files [Reporting Services]"
  - "code access security [Reporting Services], security policies"
  - "security policies [Reporting Services]"
  - "security configuration files [Reporting Services]"
  - "named permission sets [Reporting Services]"
ms.assetid: 2280fff6-3de7-44b1-87da-5db0ec975928
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Using Reporting Services Security Policy Files
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] stores component security policy information in three configuration files that are copied to the file system during setup. These configuration files can contain a combination of internal-use and user-defined security policies for code assemblies in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. The three configuration files correspond to three securable components in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]: The report server and Windows service, the Report Manager Web application, and the Report Designer preview window.  
  
> [!NOTE]  
>  There are two preview modes for Report Designer: the preview tab and the pop-up preview window that is launched when your Report Project is started in **DebugLocal** mode. The **Preview** tab is not a securable component and does not apply security policy settings. The preview window is meant to simulate the report server functionality and therefore has a policy configuration file that you or an administrator must modify to use custom assemblies and custom extensions in Report Designer.  
  
 The security policy configuration files contain security class information, some default named permission sets, and the code groups for assemblies in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. The policy configuration files of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] are similar to the Security.config file that determines the code group hierarchy and permission sets associated with machine and enterprise level policies in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)]. The location of this file is C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\CONFIG\security.config.  
  
## Policy Files in Reporting Services  
 The following table lists the policy configuration files in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], their locations (assuming a default installation), and their respective functions.  
  
|File name|Location (default installation)|Description|  
|---------------|---------------------------------------|-----------------|  
|rssrvpolicy.config|C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer|The report server policy configuration file. These security policies primarily affect report expressions and custom assemblies once a report is deployed to a report server. This policy file also affects custom data, delivery, rendering and security extensions deployed to the report server.|  
|rsmgrpolicy.config|C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportManager|Report Manager policy configuration file. These security policies affect all assemblies that extend Report Manager; for example, subscription user interface extensions for custom delivery.|  
|rspreviewpolicy.config|C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies|The Report Designer stand-alone preview policy configuration file. These security policies affect custom assemblies and report expressions that are used in reports during preview and development. These policies also affect custom extensions, such as data processing extensions, that are deployed to Report Designer.|  
  
## Modifying Configuration Files  
 Configuration settings are specified as either XML elements or attributes. If you understand XML and configuration files, you can use a text or code editor to modify user-definable settings. Security configuration files contain information about the code group hierarchy and permission sets associated with a policy level in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. It is recommended that you use the .NET Framework Configuration Utility (Mscorcfg.msc) or Code Access Security Policy Utility (Caspol.exe) to modify security policies in the Security.config file first, so that policy changes correspond to valid XML configuration elements for policy files. Once you have done that, you can cut and paste the new code groups and permission sets from Security.config to the policy file for the component to which you are adding code permissions.  
  
> [!IMPORTANT]  
>  You should backup your policy configuration files prior to making any changes.  
  
 Using this approach accomplishes two things. First, it enables you to use a visual tool to build your code groups and permission sets for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. This is much easier than writing XML configuration elements from scratch. Secondly, it ensures that you do not corrupt the security policy configuration files with malformed XML elements and attributes. For more information about the Code Access Security Policy Utility, see Using Reporting Services Security Policy Files on MSDN.  
  
 Before modifying policy configuration files, you should read all the information available in this section and related topics. Modifying the policy configuration of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] can have a significant security impact on how [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] components execute external code modules.  
  
## Placement of CodeGroup Elements for Extensions  
 The placement of CodeGroup elements in a security policy file is important. For extensions and custom assemblies that you develop, it is recommended that you place your custom code groups directly below the existing entry for the URL membership "$CodeGen$/*", as indicated by the following:  
  
```  
<CodeGroup  
    class="UnionCodeGroup"  
    version="1"  
    PermissionSetName="FullTrust">  
    <IMembershipCondition   
        class="UrlMembershipCondition"  
        version="1"  
        Url="$CodeGen$/*"  
    />  
</CodeGroup>  
<CodeGroup   
    class="UnionCodeGroup"  
    version="1"  
    PermissionSetName="FullTrust"  
    Name="MyCustomCodeGroup"  
    Description="Code group for my custom extension">  
        <IMembershipCondition class="UrlMembershipCondition"  
        version="1"  
        Url="C:\Program Files\Microsoft SQL Server\MSSQL\Reporting Services\ReportServer\bin\MyAssembly.dll"  
        />  
</CodeGroup>  
```  
  
 Additional code groups can be added one after another.  
  
## See Also  
 [Understanding Security Policies](understanding-security-policies.md)  
  
  
