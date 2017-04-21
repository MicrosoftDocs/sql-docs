---
title: "Analysis Services Configuration - Account Provisioning | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "setup-install"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Analysis Services configuration"
  - "account provisioning"
ms.assetid: 169b1af2-6fe2-467f-8ca4-919f24c620ce
caps.latest.revision: 29
ms.author: "mikeray"
manager: "erikre"
robots: noindex,nofollow
---
# Analysis Services Configuration - Account Provisioning
  Use this page to set the server mode, and to grant administrative permissions to users or services requiring unrestricted access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Setup does not automatically add the local Windows Group BUILTIN\Administrators to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server administrator role of the instance you are installing. If you want to add the local Administrators group to the server administrator role, you must explicitly specify that group.  
  
 If you are installing [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], be sure to grant administrative permissions to SharePoint farm administrators or service administrators who are responsible for a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server deployment in a [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] farm. For more information about [!INCLUDE[ssGeminiMTS](../../includes/ssgeminimts-md.md)] installation and service account requirements, see [Install SQL Server BI Features with SharePoint &#40;Power Pivot and Reporting Services&#41;](../Topic/Install%20SQL%20Server%20BI%20Features%20with%20SharePoint%20\(Power%20Pivot%20and%20Reporting%20Services\).md).  
  
## Options  
 **Server Mode** - The server mode specifies the type of Analysis Services databases that can be deployed to the server. Server modes are determined during Setup and cannot be modified later. Each mode is mutually exclusive, which means that you will need two instances of Analysis Services, each configured for a different mode, to support both classic OLAP and tabular model solutions.  
  
 **Specify Administrators** - You must specify at least one server administrator for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The users or groups that you specify will become members of the server administrator role of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance you are installing. These must be Windows domain user accounts in the same domain as the computer on which you are installing the software.  
  
> [!NOTE]  
>  User Account Control (UAC) is a Windows security feature that requires an administrator to specifically approve administrative actions or applications before they are allowed to run. Because UAC is on by default, you will be prompted to allow specific operations that require elevated privileges. You can configure UAC to change the default behavior or customize UAC for specific programs. For more information about UAC and UAC configuration, see [User Account Control Step by Step Guide](http://go.microsoft.com/fwlink/?linkid=196350) and [User Account Control (Wikipedia)](http://go.microsoft.com/fwlink/?linkid=196351).  
  
## See Also  
 [Configure Service Accounts &#40;Analysis Services&#41;](../Topic/Configure%20Service%20Accounts%20\(Analysis%20Services\).md)   
 [Configure Windows Service Accounts and Permissions](../Topic/Configure%20Windows%20Service%20Accounts%20and%20Permissions.md)  
  
  