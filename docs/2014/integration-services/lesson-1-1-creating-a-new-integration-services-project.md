---
title: "Step 1: Creating a New Integration Services Project | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: f14521b5-941e-443b-8f5e-385f98e37fbf
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 1: Creating a New Integration Services Project
  The first step in creating a package in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is to create an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project. This project includes the templates for the objects - data sources, data source views, and packages - that you use in a data transformation solution.  
  
 The packages that you will create in this [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tutorial interpret the values of locale-sensitive data. If your computer is not configured to use the regional option English (United States), you need to set additional properties in the package. The packages that you use in lessons 2 through 5 are copied from the package created in lesson 1, and you need not update locale-sensitive properties in the copied packages.  
  
> [!NOTE]  
>  This tutorial requires Microsoft SQL Server Data Tools.  
>   
>  For more information on installing the SQL Server Data Tools see [SQL Server Data Tools Download](https://msdn.microsoft.com/data/hh297027).  
  
### To create a new Integration Services project  
  
1.  On the **Start** menu, point to **All Programs**, point to **Microsoft SQL Server**, and click **SQL Server Data Tools**.  
  
2.  On the **File** menu, point to **New**, and click **Project** to create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
3.  In the **New Project** dialog box, expand the **Business Intelligence** node under **Installed Templates**, and select **Integration Services Project** in the **Templates** pane.  
  
4.  In the **Name** box, change the default name to **SSIS Tutorial**. Optionally, clear the **Create directory for solution** check box.  
  
5.  Accept the default location, or click **Browse** to browse to locate the folder you want to use. In the **Project Location** dialog box, click the folder and click **Select Folder**.  
  
6.  Click **OK**.  
  
     By default, an empty package, titled **Package.dtsx**, will be created and added to your project under SSIS Packages.  
  
7.  In **Solution Explorer** toolbar, right-click **Package.dtsx**, click **Rename**, and rename the default package to **Lesson 1.dtsx**.  
  
## Next Task in Lesson  
 [Step 2: Adding and Configuring a Flat File Connection Manager](lesson-1-2-adding-and-configuring-a-flat-file-connection-manager.md)  
  
  
