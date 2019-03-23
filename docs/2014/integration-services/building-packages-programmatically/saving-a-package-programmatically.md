---
title: "Saving a Package Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
helpviewer_keywords: 
  - "programmatically saving a package"
  - "saving a package programmatically"
ms.assetid: 4204f817-d5df-475a-9338-d7f01305d566
author: janinezhang
ms.author: janinez
manager: craigg
---
# Saving a Package Programmatically
  After building a new package programmatically, or modifying an existing one, you usually want to save your changes.  
  
 All of the methods used in this topic to save packages require a reference to the `Microsoft.SqlServer.ManagedDTS` assembly. After you add the reference in a new project, import the <xref:Microsoft.SqlServer.Dts.Runtime> namespace with a `using` or `Imports` statement.  
  
## Saving a Package Programmatically  
 To save a package programmatically, call one of the following methods of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] <xref:Microsoft.SqlServer.Dts.Runtime.Application> class:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|File|<xref:Microsoft.SqlServer.Dts.Runtime.Application.SaveToXml%2A>|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.SaveToDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.SaveToSqlServer%2A><br /><br /> or<br /><br /> <xref:Microsoft.SqlServer.Dts.Runtime.Application.SaveToSqlServerAs%2A>|  
  
> [!IMPORTANT]  
>  The methods of the <xref:Microsoft.SqlServer.Dts.Runtime.Application> class for working with the SSIS Package Store only support "." or the server name for the local server. You cannot use "(local)" or "localhost".  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Save Packages](../save-packages.md)  
  
  
