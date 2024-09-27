---
title: "Integration Services (SSIS) Projects and Solutions"
description: "Integration Services (SSIS) Projects and Solutions"
author: chugugrace
ms.author: chugu
ms.reviewer: vanto, mikeray
ms.date: 09/17/2024
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "sql13.ssis.importprojectwizard.f1"
helpviewer_keywords:
  - "projects [Integration Services], creating"
  - "folders [Integration Services], projects"
  - "files [Integration Services], projects"
  - "folders [Integration Services]"
  - "projects [Integration Services], about projects"
---

# Integration Services (SSIS) Projects and Solutions

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

  [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] provides [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the development of [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] packages.

[!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] packages reside in projects. To create and work with [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] projects, you must install [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md). For more information, see [Install Integration Services](../integration-services/install-windows/install-integration-services.md).

When you create a new [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project in [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the **New Project** dialog box includes an **Integration Services Project** template. This project template creates a new project that contains a single package.

## Projects and solutions

Projects are stored in solutions. You can create a solution first and then add an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project to the solution. If no solution exists, [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] automatically creates one for you when you first create the project. A solution can contain multiple projects of different types.

> [!TIP]  
> By default, when you create a new project in [!INCLUDE [ssBIDevStudio](../includes/ssbidevstudio-md.md)], the solution is not shown in **Solution Explorer** pane. To change this default behavior, on the **Tools** menus, select **Options**. In the **Options** dialog box, expand **Projects and Solutions**, and then select **General**. On the **General** page, select **Always show solution**.

## Solutions contain projects

A solution is a container that groups and manages the projects that you use when you develop end-to-end business solutions. A solution lets you handle multiple projects as one unit and to bring together one or more related projects that contribute to a business solution.

Solutions can include different types of projects. If you want to use [!INCLUDE [ssIS](../includes/ssis-md.md)] Designer to create an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] package, you work in an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project in a solution provided by [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].

When you create a new solution, [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] adds a solution folder to Solution Explorer. The solution folder contains these types of files:

- `.sln`: contains information about the solution configuration and lists the projects in the solution.

- `.suo`: contains information about your preferences for working with the solution.

While [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] automatically creates a solution when you create a new project, you can also create a blank solution, and then add projects later.

## Integration Services projects contain packages

A project is a container in which you develop [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] packages.

In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project stores and groups the files that are related to the package. For example, a project includes the files that are required to create a specific extract, transfer, and load (ETL) solution.

Before you create an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project, you should become familiar with the basic contents of this kind of project. After you understand what a project contains, you can begin creating and working with an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project.

## Folders in Integration Services projects

The following image shows the folders in an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project in [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].

:::image type="content" source="media/ssis-solution-explorer.png" alt-text="Screenshot of Solution Explorer showing the folders in the project.":::

The following table describes the folders that appear in an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project.

| Folder | Description |
| --- | --- |
| Connection Managers | Contains Project Connection Managers. For more information, see [Integration Services (SSIS) Connections](../integration-services/connection-manager/integration-services-ssis-connections.md). |
| [!INCLUDE [ssIS](../includes/ssis-md.md)] Packages | Contains packages. For more information, see [Integration Services (SSIS) Packages](../integration-services/integration-services-ssis-packages.md). |
| Package Parts | Contains Package Parts that can be reused or imported. For more information, see [Reuse Control Flow across Packages by Using Control Flow Package Parts](reuse-control-flow-across-packages-by-using-control-flow-package-parts.md) |
| Miscellaneous | Contains files other than package files. |

## Files in Integration Services projects

When you add a new or an existing [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project to a solution, [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] creates project files. Project files include:

- `.dtproj`: contains information about project configurations and items such as packages.

- `*.dtproj.user`: contains information about your preferences for working with the project.

- `*.database`: contains information that [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] requires to open the [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project.

- `Project.params`: contains information about the [Project parameters](integration-services-ssis-package-and-project-parameters.md).

## Version targeting in Integration Services projects

In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], you can create, maintain, and run packages that target specific versions of SQL Server.

In Solution Explorer, right-click on an Integration Services project and select **Properties** to open the property pages for the project. On the **General** tab of **Configuration Properties**, select the **TargetServerVersion** property, and then choose the version you want.

:::image type="content" source="media/targetserverversion2.png" alt-text="Screenshot of TargetServerVersion property in project properties dialog box." lightbox="media/targetserverversion2.png":::

## Create a new Integration Services project

1. Open [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].

1. On the **File** menu, point to **New**, and then select **Project**.

1. In the **New Project** dialog box, select **Business Intelligence**, and then select the **Integration Services Project** template.

     The **Integration Services Project** template creates an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project that contains a single, empty package.

  :::image type="content" source="media/ssis-ssdt-new-project.png" alt-text="Screenshot of the New Project dialog box with the Integration Services Project - Business Intelligence template highlighted." lightbox="media/ssis-ssdt-new-project.png":::

1. (Optional) Edit the project name and the location.

     The solution name is automatically updated to match the project name.

1. To create a separate folder for the solution file, select the default option, specifically **Create directory for solution**.

1. If source control software is installed on the computer, select **Add to source control** to associate the project with source control.

1. If the source control software is [!INCLUDE [msCoName](../includes/msconame-md.md)] Visual SourceSafe, the **Visual SourceSafe Login** dialog box opens. In **Visual SourceSafe Login**, provide a user name, a password, and the name of the [!INCLUDE [msCoName](../includes/msconame-md.md)] Visual SourceSafe database. Select **Browse** to locate the database.

    > [!NOTE]  
    > To view and change the selected source control plug-in and to configure the source control environment, select **Options** on the **Tools** menu, and then expand the **Source Control** node.

1. Select **OK** to add the solution to **Solution Explorer** and add the project to the solution.

## Import an existing project with the Import Project Wizard

1. In [!INCLUDE [vsprvs](../includes/vsprvs-md.md)], select **New** > **Project** on the **File** menu.

1. In the **Installed Templates** area of the **New Project** window, expand **Business Intelligence**, and select **Integration Services**.

1. Select **Integration Services Import Project Wizard** from the project types list.

1. Type a name for the new project to be created in the **Name** text box.

1. Type the path or location for the project in the **Location** text box, or select **Browse** to select one.

1. Type a name for the solution in the **Solution name** text box.

1. Select **OK** to launch the **Integration Services Import Project Wizard** dialog box.

1. Select **Next** to switch to the **Select Source** page.

1. If you're importing from an `.ispac` file, type the path including file name in the **Path** text box. Select **Browse** to navigate to the folder where you want the solution to be stored and type file name in the **File name** text box, and select **Open**.

     If you're importing from an **Integration Services Catalog**, type the database instance name in the **Server name** text box or select **Browse** and select the database instance that contains the catalog.

     Select **Browse** next to **Path** text box, expand folder in the catalog, select the project you want to import, and select **OK**.

     Select **Next** to switch to the **Review** page.

1. Review the information and select **Import** to create a project based on the existing project you selected.

1. Optional: select **Save Report** to save the results to a file

1. Select **Close** to close the **Integration Services Import Project Wizard** dialog box.

## Add a project to a solution

When you add a project, you can have [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] create a new, blank project, or you can add a project that you have already created for a different solution. You can only add a project to an existing solution when the solution is visible in [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].

### Add a new project to a solution

1. In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution to which you want to add a new [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project, and either:

    - Right-click the solution, select **Add**, and then select **New Project**.

    or

    - On the **File** menu, point to **Add**, and then select **New Project**.

1. In the **Add New Project** dialog box, select **Integration Services Project** in the **Templates** pane.

1. Optionally, edit the project name and location.

1. Select **OK**.

### Add an existing project to a solution

1. In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution to which you want to add an existing [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project, and do one of the following:

    - Right-click the solution, point to **Add**, and then select **Existing Project**.

    - On the **File** menu, select **Add**, and then select **Existing Project**.

1. In the **Add Existing Project** dialog box, browse to locate the project you want to add, and then select **Open**.

1. The project is added to the solution folder in **Solution Explorer**.

## Remove a project from a solution

You can only remove a project from a solution when the solution is visible in [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. After the solution is visible, you can remove all except one project. As soon as only one project remains, [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] no longer displays the solution folder. You can't remove the last project.

1. In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution from which you want to remove an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project.

1. In Solution Explorer, right-click the project, and then select **Unload Project**.

1. Select **OK** to confirm the removal.

## Add an item to a project

1. In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution that contains the [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project to which you want to add an item.

1. In Solution Explorer, right-click the project, point to **Add**, and do one of the following:

    - Select **New Item**, and then select a template from the **Templates** pane in the **Add New Item** dialog box.

    - Select **Existing Item**, browse in the **Add Existing Item** dialog box to locate the item you want to add to the project, and then select **Add**.

1. The new item appears in the appropriate folder in Solution Explorer.

## Copy project items

You can copy objects within an [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project or between [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] projects. You can also copy objects between the other types of [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] projects, [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] and [!INCLUDE [ssASnoversion](../includes/ssasnoversion-md.md)]. To copy between projects, the project must be part of the same [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] solution.

1. In [!INCLUDE [ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project or solution that you want to work with.

1. Expand the project and item folder to copy from.

1. Right-click the item and select **Copy**.

1. Right-click the [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project to copy to and select **Paste**.

     The items are automatically copied to the correct folder. If you copy items to the [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] project that aren't packages, the items are copied to the **Miscellaneous** folder.

## Related content

- [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)
- [SSIS How to Create an ETL Package](ssis-how-to-create-an-etl-package.md)
