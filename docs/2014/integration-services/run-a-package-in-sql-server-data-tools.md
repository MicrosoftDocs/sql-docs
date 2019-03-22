---
title: "Run a Package in SQL Server Data Tools | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, running"
  - "SSIS packages, running"
  - "packages [Integration Services], running"
  - "SQL Server Integration Services packages, running"
ms.assetid: 318e6beb-5540-4101-82a5-18c9d47f0570
author: janinezhang
ms.author: janinez
manager: craigg
---
# Run a Package in SQL Server Data Tools
  You typically run packages in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] during the development, debugging, and testing of packages. When you run a package from [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, the package always runs immediately.  
  
 While a package is running, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer displays the progress of package execution on the **Progress** tab. You can view the start and finish time of the package and its tasks and containers, in addition to information about any tasks or containers in the package that failed. After the package finishes running, the run-time information remains available on the **Execution Results** tab. For more information, see the section, "Progress Reporting," in the topic, [Debugging Control Flow](control-flow/control-flow.md).  
  
 **Design-time deployment**. When you run a package in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], the package is built and then deployed to a folder. Before you run the package, you can specify the folder to which the package is deployed. If you do not specify a folder, the **bin** folder is used by default. This type of deployment is called design-time deployment.  
  
### To run a package in SQL Server Data Tools  
  
1.  In Solution Explorer, if your solution contains multiple projects, right-click the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package, and then click **Set as StartUp Object** to set the startup project.  
  
2.  In Solution Explorer, if your project contains multiple packages, right-click a package, and then click **Set as StartUp Object** to set the startup package.  
  
3.  To run a package, use one of the following procedures:  
  
    -   Open the package that you want to run and then click **Start Debugging** on the menu bar, or press F5. After the package finishes running, press Shift+F5 to return to design mode.  
  
    -   In Solution Explorer, right-click the package, and then click **Execute Package**.  
  
### To specify a different folder for design-time deployment  
  
1.  In Solution Explorer, right-click the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project folder that contains the package you want to run, and then click **Properties**.  
  
2.  In the **\<project name> Property Pages** dialog box, click **Build**.  
  
3.  Update the value in the OutputPath property to specify the folder you want to use for design-time deployment, and click **OK**.  
  
## See Also  
 [Execution of Projects and Packages](packages/run-integration-services-ssis-packages.md)   
 [Integration Services &#40;SSIS&#41; Packages](../../2014/integration-services/integration-services-ssis-packages.md)  
  
  
