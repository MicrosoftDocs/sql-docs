---
title: "Save Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.savecopyas.f1"
helpviewer_keywords: 
  - "Integration Services packages, saving"
  - "packages [Integration Services], saving"
  - "saving packages"
  - "SSIS packages, saving"
  - "SQL Server Integration Services packages, saving"
ms.assetid: 17c1de2c-637f-45c2-a148-79294bae0af4
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Save Packages
  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] you build packages by using [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and save the packages to the file system as XML files (.dtsx files). You can also save copies of the package XML file to the msdb database in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or to the package store. The package store represents the folders in the file system location that the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service manages.  
  
 If you save a package to the file system, you can later use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service to import the package to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or to the package store. For more information, see [Integration Services Service &#40;SSIS Service&#41;](../integration-services/service/integration-services-service-ssis-service.md).  
  
 You can also use a command prompt utility, **dtutil**, to copy a package between the file system and msdb. For more information, see [dtutil Utility](../integration-services/dtutil-utility.md).  
## Save a package to the file system  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to save to a file.  
  
2.  In Solution Explorer, click the package you want to save.  
  
3.  On the **File** menu, click **Save Selected Items**.  
  
    > [!NOTE]  
    >  You can verify the path and file name where the package was saved in the Properties window.  

## Save a copy of a package
  This section describes how to save a copy of a package to the file system, to the package store, or to the **msdb** database in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. When you specify a location to save the package copy, you can also update the name of the package.  
  
 The package store can include both the **msdb** database and the folders in the file system, only **msdb**, or only folders in the file system. In **msdb**, packages are saved to the **sysssispackages** table. This table includes a **folderid** column that identifies the logical folder to which the package belongs. The logical folders provide a useful way to group packages saved to **msdb** in the same way that folders in the file system provide a way to group packages saved to the file system. Rows in the **sysssispackagefolders** table in **msdb** define the folders.  
  
 If **msdb** is not defined as part of the package store, you can continue to associate packages with existing logical folders when you select SQL Server in the **Package Path** option.  
  
> [!NOTE]  
>  The package must be opened in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer before you can save a copy of the package.  
  
### To save a copy of a package  
  
1.  In Solution Explorer, double-click the package of which you want to save a copy.  
  
2.  On the **File** menu, click **Save Copy of \<package file> As**.  
  
3.  In the **Save Copy of Package** dialog box, select a package location in the **Package location** list. The following options are available:  
    -   SQL Server
    -   File System 
    -   SSIS Package Store 
  
4.  If the location is **SQL Server** or **SSIS Package Store**, provide a server name.  
  
5.  If saving to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], specify the authentication type and, if using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a user name and password.  
  
6.  To specify the package path, either type the path or click the browse button **(...)** to specify the location of the package. The default name of the package is Package. Optionally, update the package name to one that suits your needs.  
  
     If you select **SQL Server** as the **Package Path** option, the package path consists of logical folders in **msdb** and the package name. For example, if the package DownloadMonthlyData is associated with the Finance folder within the MSDB folder (the default name of the root logical folder in **msdb**), the package path for the package named DownloadMonthlyData is MSDB/Finance/DownloadMonthlyData  
  
     If you select **SSIS Package Store** as the **Package Path** option, the package path consists of the folder that the Integration Services service manages. For example, if the package UpdateDeductions is located in the HumanResources folder within the file system folder that the service manages, the package path is /File System/HumanResources/UpdateDeductions; likewise, if the package PostResumes is associated with the HumanResources folder within the MSDB folder, the package path is MSDB/HumanResources/PostResumes.  
  
     If you select **File System** as the **Package Path** option, the package path is the location in the file system and the file name. For example, if the package name is UpdateDemographics the package path is C:\HumanResources\Quarterly\UpdateDemographics.dtsx.  
  
7.  Review the package protection level.  
  
8.  Optionally, click the browse button **(...)** by the **Protection level** box to change the protection level.  
  
    -   In the **Package Protection Level** dialog box, select a different protection level.  
  
    -   Click **OK**.  
  
9. Click **OK**.  

## Save a package as a package template
 This section describes how to designate and use custom packages as templates when you create new Integration Services packages in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. By default, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] uses a package template that creates an empty package when you add a new package to an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. You cannot replace this default template, but you can add new templates.  
  
 You can designate multiple packages to use as templates. Before you can implement custom packages as templates, you must create the packages.  
  
 When you create package using custom packages as templates, the new packages have the same name and GUID as the template. To differentiate among packages you should update the value of the **Name** property and generate a new GUID for the **ID** property. For more information, see [Create Packages in SQL Server Data Tools](../integration-services/create-packages-in-sql-server-data-tools.md) and [Set Package Properties](../integration-services/set-package-properties.md).  
  
### To designate a custom package as a package template  
  
1.  In the file system, locate the package that you want to use as template.  
  
2.  Copy the package to the DataTransformationItems folder. By default this folder is in C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies\ProjectItems\DataTransformationProject.  
  
3.  Repeat steps 1 and 2 for each package that you want to use as a template.  
  
### To use a custom package as a package template  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in which you want to create a package.  
  
2.  In Solution Explorer, right-click the project, point to **Add**, and then click **New Item**.  
  
3.  In the **Add New Item -\<project name>** dialog box, click the package that you want to use as a template.  
  
     The list of templates includes the default package template named New SSIS Package. The package icon identifies templates that can be used as package templates.  
  
4.  Click **Add**.  
