---
title: "Migrate Scripts to VSTA | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "SSIS Script task, converting scripts"
  - "Script component [Integration Services], converting scripts"
  - "Script task [Integration Services], converting scripts"
  - "SSIS Script component, converting scripts"
ms.assetid: d685098b-86a1-46bf-939a-63d56951e009
author: mashamsft
ms.author: douglasl
manager: craigg
---
# Migrate Scripts to VSTA
  When you upgrade [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] packages to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] migrates the scripts in any Script tasks or Script components to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA). VSTA is the scripting environment that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] uses. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the scripting environment for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] for Applications (VSA).  
  
 If the scripts in either the Script tasks or Script components reference interfaces, you might have to modify those references before you upgrade the package. Otherwise, the package will not be upgraded or the scripts will not be validated, depending on the upgrade method that you use. To modify these references, replace references to IDTS*xxx*90 interfaces with references to the corresponding IDTS*xxx*100 interfaces.  
  
 For more information about how to migrate scripts and upgrade packages, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
## Understanding Migration Failures  
 When you migrate the scripts, the migration can fail because of one of the following reasons:  
  
-   The entry point for the VSA script was renamed.  
  
     The entry point specifies the method in the `ScriptMain` class in the VSTA project that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime calls as the entry point into the Script task code. The `ScriptMain` class is the default class that the script templates generate.  
  
-   There is no entry point or there are multiple entry points in the VSA script.  
  
-   Assembly references could not be added.  
  
-   The `ScriptMain` class was modified to inherit from other classes in addition to the `ScriptObjectModelSSIS` class. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] does not support multiple inheritance.  
  
 You cannot convert a VSA script that uses [!INCLUDE[vbprvblong](../../includes/vbprvblong-md.md)] to a VSTA script that uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[csharp_orcas_long](../../includes/csharp-orcas-long-md.md)]. However, you can create a new VSTA script that uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[csharp_orcas_long](../../includes/csharp-orcas-long-md.md)]. For more information, see [Coding and Debugging the Script Task](../../integration-services/control-flow/script-task.md) and [Coding and Debugging the Script Component](../../integration-services/data-flow/transformations/script-component.md).  
  
## See Also  
 [Extending Packages with Scripting](../../relational-databases/server-management-objects-smo/tasks/scripting.md)  
  
  
