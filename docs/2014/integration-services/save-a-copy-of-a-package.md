---
title: "Save a Copy of a Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, saving"
  - "packages [Integration Services], saving"
  - "saving packages"
  - "SSIS packages, saving"
  - "SQL Server Integration Services packages, saving"
ms.assetid: 21482a20-e420-4452-b7eb-8f9fa1929f31
author: janinezhang
ms.author: janinez
manager: craigg
---
# Save a Copy of a Package
  This procedure describes how to save a copy of a package to the file system, to the package store, or to the **msdb** database in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. When you specify a location to save the package copy, you can also update the name of the package.  
  
 The package store can include both the **msdb** database and the folders in the file system, only **msdb**, or only folders in the file system. In **msdb**, packages are saved to the **sysssispackages** table. This table includes a **folderid** column that identifies the logical folder to which the package belongs. The logical folders provide a useful way to group packages saved to **msdb** in the same way that folders in the file system provide a way to group packages saved to the file system. Rows in the **sysssispackagefolders** table in **msdb** define the folders.  
  
 If **msdb** is not defined as part of the package store, you can continue to associate packages with existing logical folders when you select SQL Server in the **Package Path** option.  
  
> [!NOTE]  
>  The package must be opened in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer before you can save a copy of the package.  
  
### To save a copy of a package  
  
1.  In Solution Explorer, double-click the package of which you want to save a copy.  
  
2.  On the **File** menu, click **Save Copy of \<package file> As**.  
  
3.  In the **Save Copy of Package** dialog box, select a package location in the **Package location** list.  
  
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
  
## See Also  
 [Integration Services &#40;SSIS&#41; Packages](../../2014/integration-services/integration-services-ssis-packages.md)   
 [Configuring the Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md)  
  
  
