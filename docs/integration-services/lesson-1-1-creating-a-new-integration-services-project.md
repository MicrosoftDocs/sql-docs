---
title: "Step 1: Create a new Integration Services project | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: f14521b5-941e-443b-8f5e-385f98e37fbf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-1: Create a new Integration Services project

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



The first step in creating a package in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is to create an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. This example project includes templates for the data sources, data source views, and packages that make up a data transformation solution.  
  
The packages that you create in this [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tutorial interpret the values of locale-sensitive data. If your computer isn't configured to use the regional option **English (United States)**, you need to set additional properties in the package. 

The packages that you use in lessons 2 through 6 are copied from the package you create in this lesson.  
  
> [!NOTE]  
> If you haven't already, see the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites).

## Create a new Integration Services project  
  
1.  On the Windows **Start** menu, search for and select **Visual Studio (SSDT)**.  
  
2.  In Visual Studio, select **File** > **New** > **Project** to create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
3.  In the **New Project** dialog box, expand the **Business Intelligence** node under **Installed**, and select **Integration Services Project** in the **Templates** pane.  
  
4.  In the **Name** box, change the default name to **SSIS Tutorial**. To use a folder that already exists, clear the **Create directory for solution** check box.  
  
5.  Accept the default location, or select **Browse** to browse to locate the folder you want to use. In the **Project Location** dialog box, select the folder and then **Select Folder**.  
  
6.  Select **OK**.  
  
    By default, an empty package titled **Package.dtsx** is created and added to your project under **SSIS Packages**.  
  
7.  In **Solution Explorer**, right-click **Package.dtsx**, select **Rename**, and rename the default package to **Lesson 1.dtsx**.  
  
## Go to next task
[Step 2: Add and configure a Flat File connection manager](../integration-services/lesson-1-2-adding-and-configuring-a-flat-file-connection-manager.md)  
  
