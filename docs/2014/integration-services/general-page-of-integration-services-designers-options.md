---
title: "General Page | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Business_Intelligence_Designers.Data_Transformation_Designers.General"
ms.assetid: d695690a-923b-4036-945e-7621e8651deb
author: janinezhang
ms.author: janinez
manager: craigg
---
# General Page
  Use the **General** page of the **Integration Services Designers** page in the **Options** dialog box to specify the options for loading, displaying, and upgrading packages.  
  
 To open the **General** page, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], on the **Tools** menu, click **Options**, expand **Business Intelligence Designers**, and select **Integration Services Designers**.  
  
## Options  
 **Check digital signature when loading a package**  
 Select to have [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] check the digital signature when loading a package. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] will only check whether the digital signature is present, is valid, and is from a trusted source. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] will not check whether the package has been changed since the package was signed.  
  
 If you set the **BlockedSignatureStates** registry value, this registry value overrides the **Check digital signature when loading a package** option. For more information, see [Implement a Signing Policy by Setting a Registry Value](implement-a-signing-policy-by-setting-a-registry-value.md).  
  
 For more information about digital certificates and packages, see [Identify the Source of Packages with Digital Signatures](security/identify-the-source-of-packages-with-digital-signatures.md).  
  
 **Show warning if package is unsigned**  
 Select to display a warning when loading a package that is not signed.  
  
 **Show precedence constraint labels**  
 Select which label-Success, Failure, or Completion-to display on precedence constraints when viewing packages in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 **Scripting language**  
 Select the default scripting language for new Script tasks and Script components.  
  
 **Update connection strings to use new provider names**  
 When opening or upgrading [!INCLUDE[ssISversion2005](../includes/ssisversion2005-md.md)] packages, update connection strings to use the names for the following providers, for the current release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]:  
  
-   [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] OLE DB provider  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client  
  
 The [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard updates only connection strings that are stored in connection managers. The wizard does not update connection strings that are constructed dynamically by using the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] expression language, or by using code in a Script task.  
  
 **Create new package ID**  
 When upgrading [!INCLUDE[ssISversion2005](../includes/ssisversion2005-md.md)] packages, create new package IDs for the upgraded versions of the packages.  
  
## See Also  
 [Security Overview &#40;Integration Services&#41;](security/security-overview-integration-services.md)   
 [Extending Packages with Scripting](extending-packages-scripting/extending-packages-with-scripting.md)  
  
  
