---
title: "Import and Export Packages (SSIS Service) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "packages [Integration Services], importing"
  - "packages [Integration Services], exporting"
  - "importing packages"
  - "exporting packages"
ms.assetid: ef18ec11-b536-47d9-abd1-794099f43486
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Import and Export Packages (SSIS Service)
    
> [!IMPORTANT]  
>  This topic discusses the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service, a Windows service for managing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] supports the service for backward compatibility with earlier releases of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], you can manage objects such as packages on the Integration Services server.  
  
 Packages can be saved either in the sysssispackages table in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] msdb database or in the file system.  
  
 The package store, which is the logical storage that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service monitors and manages, can include both the msdb database and the file system folders specified in the configuration file for the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service.  
  
 You can import and export packages between the following storage types:  
  
-   File system folders anywhere in the file system.  
  
-   Folders in the SSIS Package Store. The two default folders are named File System and MSDB.  
  
-   The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] msdb database.  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] gives you the ability to import and export packages, and by doing this change the storage format and location of packages. Using the import and export features, you can add packages to the file system, package store, or msdb database, and copy packages from one storage format to another. For example, packages saved in msdb can be copied to the file system and vice versa.  
  
 You can also copy a package to a different format using the **dtutil** command prompt utility (dtutil.exe). For more information, see [dtutil Utility](dtutil-utility.md).  
  
## To import or export a package  
  
> [!IMPORTANT]  
>  This topic discusses the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service that is part of [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] supports the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service for backward compatibility with [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. For information about managing packages in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], see [Integration Services &#40;SSIS&#41; Server](catalog/integration-services-ssis-server-and-catalog.md).  
  
 You can import or export an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package from or to the following locations:  
  
-   You can import a package that is stored in an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], in the file system, or in the [!INCLUDE[ssIS](../includes/ssis-md.md)] package store. The imported package is saved to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or to a folder in the [!INCLUDE[ssIS](../includes/ssis-md.md)] package store.  
  
-   You can export a package that is stored in an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the file system, or the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store to a different storage format and location.  
  
 However, there are some restrictions on importing and exporting a package between different versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   On an instance of [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], you can import packages from an instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], but you cannot export packages to an instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)].  
  
-   On an instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], you cannot import packages from, or export packages to, an instance of [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
 The following procedures describe how to use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to import or export a package.  
  
#### To import a package by Using SQL Server Management Studio  
  
1.  Click **Start**, point to **Microsoft** [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and then click **SQL Server Management Studio**.  
  
2.  In the **Connect to Server** dialog box set the following options:  
  
    -   In the **Server type** box, select **Integration Services**.  
  
    -   In the **Server name** box, provide a server name or click **\<Browse for more...>** and locate the server to use.  
  
3.  If Object Explorer is not open, on the **View** menu, click **Object Explorer**.  
  
4.  In Object Explorer, expand the **Stored Packages** folder.  
  
5.  Expand the subfolders to locate the folder into which you want to import a package.  
  
6.  Right-click the folder, click **Import Package**. and then do one of the following:  
  
    -   To import from an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], select the **SQL Server** option, and then specify the server and select the authentication mode. If you select [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a user name and a password.  
  
         Click the browse button **(...)**, select the package to import, and then click **OK.**  
  
    -   To import from the file system, select the **File system** option.  
  
         Click the browse button **(...)**, select the package to import, and then click **Open.**  
  
    -   To import from the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store, select the **SSIS Package Store** option and specify the server.  
  
         Click the browse button **(...)**, select the package to import, and then click **OK.**  
  
7.  Optionally, update the package name.  
  
8.  To update the protection level of the package, click the browse button **(...)** and choose a different protection level by using the **Package Protection Level** dialog box. If the **Encrypt sensitive data with password** or the **Encrypt all data with password** option is selected, type and confirm a password.  
  
9. Click **OK** to complete the import.  
  
#### To export a package by Using SQL Server Management Studio  
  
1.  Click **Start**, point to **Microsoft** [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and then click **SQL Server Management Studio**.  
  
2.  In the **Connect to Server** dialog box, set the following options:  
  
    -   In the **Server type** box, select **Integration Services**.  
  
    -   In the **Server name** box, provide a server name or click **\<Browse for more...>** and locate the server to use.  
  
3.  If Object Explorer is not open, on the **View** menu, click **Object Explorer**.  
  
4.  In Object Explorer, expand the **Stored Packages** folder.  
  
5.  Expand the subfolders to locate the package you want to export.  
  
6.  Right-click the package, click **Export**, and then do one of the following:  
  
    -   To export to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], select the **SQL Server** option, and then specify the server and select the authentication mode. If you select [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a user name and a password.  
  
         Click the browse button **(...)**, and expand the **SSIS Packages** folder to locate the folder to which you want to save the package. Optionally, update the default name of the package, and then click **OK**.  
  
    -   To export to the file system, select the **File System** option.  
  
         Click the browse button **(...)** to locate the folder to which you want to export the package, type the name of the package file, and then click **Save.**  
  
    -   To export to the [!INCLUDE[ssIS](../includes/ssis-md.md)] package store, select the **SSIS Package Store** option, and specify the server.  
  
         Click the browse button **(...)**, expand the **SSIS Packages** folder, and select the folder to which you want to save the package. Optionally, enter a new name for the package in the **Package Name** text box. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
7.  To update the protection level of the package, click the browse button **(...)** and choose a different protection level by using the **Package Protection Level** dialog box. If the **Encrypt sensitive data with password** or the **Encrypt all data with password** option is selected, type and confirm a password.  
  
8.  Click **OK** to complete the export.  
  
## See Also  
 [Package Management &#40;SSIS Service&#41;](service/package-management-ssis-service.md)  
  
  
