---
title: "Managing Packages and Folders Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "enumerators [Integration Services]"
  - "packages [Integration Services], managing"
  - "custom enumerators [Integration Services]"
ms.assetid: ec59b75d-ba09-44ac-9039-9d593bb462d9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Managing Packages and Folders Programmatically
  As you work programmatically with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, you may want to determine whether an individual package or folder exists, or to manage the folders in which packages are stored. The <xref:Microsoft.SqlServer.Dts.Runtime.Application> class of the <xref:Microsoft.SqlServer.Dts.Runtime> namespace provides a variety of methods to satisfy these requirements.  
  
##  <a name="exists"></a> Determining Whether a Package or Folder Exists  
 To determine programmatically whether a saved package exists, call one of the following methods before attempting to load and run the package:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.ExistsOnDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.ExistsOnSqlServer%2A>|  
  
 To determine programmatically whether a folder exists, call one of the following methods before attempting to list the packages stored in the folder, :  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.FolderExistsOnDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.FolderExistsOnSqlServer%2A>|  
  

  
##  <a name="managing"></a> Managing Packages and Folders  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Application> class of the <xref:Microsoft.SqlServer.Dts.Runtime> namespace provides additional methods for managing packages and the folders in which they are stored.  
  
###  <a name="managing_rempkg"></a> Removing a Package  
 To remove a saved package programmatically, call one of the following methods:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RemoveFromDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RemoveFromSqlServer%2A>|  
  

  
###  <a name="managing_create"></a> Creating a Folder  
 To create a storage folder programmatically, call one of the following methods:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.CreateFolderOnDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.CreateFolderOnSqlServer%2A>|  
  

  
###  <a name="managing_remfldr"></a> Removing a Folder  
 To remove a storage folder programmatically, call one of the following methods:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RemoveFolderFromDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RemoveFolderFromSqlServer%2A>|  
  
  
  
###  <a name="managing_rename"></a> Renaming a Folder  
 To rename a storage folder programmatically, call one of the following methods:  
  
|Storage Location|Method to Call|  
|----------------------|--------------------|  
|SSIS Package Store|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RenameFolderOnDtsServer%2A>|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|<xref:Microsoft.SqlServer.Dts.Runtime.Application.RenameFolderOnSqlServer%2A>|  
  

  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Package Management &#40;SSIS Service&#41;](../service/package-management-ssis-service.md)   
 [Enumerating Available Packages Programmatically](../run-manage-packages-programmatically/enumerating-available-packages-programmatically.md)  
  
  
