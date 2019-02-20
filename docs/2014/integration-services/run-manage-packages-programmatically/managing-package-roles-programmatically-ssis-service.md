---
title: "Managing Package Roles Programmatically (SSIS Service) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Integration Services packages, roles"
  - "roles [Integration Services]"
  - "packages [Integration Services], roles"
ms.assetid: 2e0ca0d5-d4f5-421d-b17d-a48b37b923e5
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Managing Package Roles Programmatically (SSIS Service)
  As you work programmatically with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, you may want to determine which roles are available to apply to packages, or to determine or set the roles applied to an individual package. The <xref:Microsoft.SqlServer.Dts.Runtime.Application> class of the <xref:Microsoft.SqlServer.Dts.Runtime> namespace provides a variety of methods to satisfy these requirements.  
  
 Roles apply only to packages stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **msdb** database. For more information about package roles, see [Integration Services Roles &#40;SSIS Service&#41;](../security/integration-services-roles-ssis-service.md).  
  
 All the methods discussed in this topic require a reference to the `Microsoft.SqlServer.ManagedDTS` assembly. After you add the reference in a new project, import the <xref:Microsoft.SqlServer.Dts.Runtime> namespace by using a `using` or `Imports` statement.  
  
> [!IMPORTANT]  
>  The methods of the <xref:Microsoft.SqlServer.Dts.Runtime.Application> class for working with the SSIS Package Store support only ".", localhost, or the server name for the local server. You cannot use "(local)".  
  
## Determining Which Roles Are Available  
 To determine which roles are available for the packages stored on a particular server, call the <xref:Microsoft.SqlServer.Dts.Runtime.Application.GetDtsServerRoles%2A> method of the <xref:Microsoft.SqlServer.Dts.Runtime.Application> class.  
  
## Determining Which Roles Are Assigned  
 To determine which roles have already been assigned to a particular package, call the <xref:Microsoft.SqlServer.Dts.Runtime.Application.GetPackageRoles%2A> method. To assign roles to a package, call the <xref:Microsoft.SqlServer.Dts.Runtime.Application.SetPackageRoles%2A> method.  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Integration Services Roles &#40;SSIS Service&#41;](../security/integration-services-roles-ssis-service.md)  
  
  
