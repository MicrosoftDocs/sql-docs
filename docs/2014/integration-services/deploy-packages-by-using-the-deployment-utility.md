---
title: "Deploy Packages by Using the Deployment Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "packages [Integration Services], installing"
  - "installing packages"
  - "dependencies [Integration Services]"
  - "deploying packages [Integration Services], installing"
ms.assetid: eaf4b56e-2023-4d17-971c-703031da758c
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Deploy Packages by Using the Deployment Utility
  When you have built a deployment utility to install packages from an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project on a different computer than the one on which the deployment utility was built, you must first copy the deployment folder to the destination computer.  
  
 The path of the deployment folder is specified in the DeploymentOutputPath property of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project for which you created the deployment utility. The default path is bin\Deployment, relative to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. For more information, see [Create a Deployment Utility](../../2014/integration-services/create-a-deployment-utility.md).  
  
 You use the Package Installation Wizard to install the packages. To launch the wizard, double-click the deployment utility file after you have copied the deployment folder to the server. This file is named \<project name>.SSISDeploymentManifest, and can be found in the deployment folder on the destination computer.  
  
> [!NOTE]  
>  Depending on the version of the package that you are deploying, you might encounter an error if you have different versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installed side-by-side. This error can occur because the .SSISDeploymentManifest file name extension is the same for all versions of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Double-clicking the file calls the installer (dtsinstall.exe) for the most recently installed version of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], which might not be the same version as the deployment utility file. To work around this problem, run the correct version of dtsinstall.exe from the command line, and provide the path of the deployment utility file.  
  
 The Package Installation Wizard guides you through the steps to install packages to the file system or to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You can configure the installation in the following ways:  
  
-   Choosing the location type and location to install the packages.  
  
-   Choosing location to install package dependencies.  
  
-   Validating the packages after they are installed on the target server.  
  
 The file-based dependencies for packages are always installed to the file system. If you install a package to the file system, the dependencies are installed in the same folder as the one that you specify for the package. If you install a package to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can specify the folder in which to store the file-based dependencies.  
  
 If the package includes configurations that you want to modify for use on the destination computer, you can update the values of the properties by using the wizard.  
  
 In addition to installing packages by using the Package Installation Wizard, you can copy and move packages by using the **dtutil** command prompt utility. For more information, see [dtutil Utility](dtutil-utility.md).  
  
### To deploy packages to an instance of SQL Server  
  
1.  Open the deployment folder on the target computer.  
  
2.  Double-click the manifest file, \<project name>.SSISDeploymentManifest, to start the Package Installation Wizard.  
  
3.  On the **Deploy SSIS Packages** page, select the **SQL Server deployment** option.  
  
4.  Optionally, select **Validate packages after installation** to validate packages after they are installed on the target server.  
  
5.  On the **Specify Target SQL Server** page, specify the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to install the packages to and select an authentication mode. If you select [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, you must provide a user name and a password.  
  
6.  On the **Select Installation Folder** page, specify the folder in the file system for the package dependencies that will be installed.  
  
7.  If the package includes configurations, you can edit configurations by updating values in the **Value** list on the Configure Packages page.  
  
8.  If you elected to validate packages after installation, view the validation results of the deployed packages.  
  
## See Also  
 [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md)  
  
  
