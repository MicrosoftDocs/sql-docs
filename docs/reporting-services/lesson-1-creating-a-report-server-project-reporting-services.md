---
title: "Lesson 1: Create a report server project"
description: "In this article, you learn how to use Report Designer to create a report server project and a report definition (.rdl) file."
author: maggiesMSFT
ms.author: maggies
ms.date: 05/21/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
#customer intent: As a SQL server user, I want to create a sample report project and report so that I can continue with the other lessons in this tutorial.
---
# Create a report server project by using Report Designer

In this lesson, you create a *report server project* and a *report definition (.rdl)* file using *Report Designer*.

> [!NOTE]
> [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment, where you can open, modify, preview, save, and deploy [!INCLUDE[ssrsnoversion_md](../includes/ssrsnoversion-md.md)] paginated report definitions, shared data sources, shared datasets, and report parts.
>
> Report parts are deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019, and discontinued starting in SQL Server Reporting Services 2022 and Power BI Report Server.


When you create reports with Report Designer, it creates a report server project that contains the report files and other resource files used by the report or reports.

## Create a report server project
  
1. From the **File** menu, select **New** > **Project**.  

    :::image type="content" source="../reporting-services/media/ssrs-ssdt-file-01-new-project.png" alt-text="Screenshot of Visual Studio showing the project option selected in the New menu in the File menu.":::
  
1. In the list of recently used templates, select **Report Server Project**, and then choose **Next**. You can also search for "Report Server Project".

    :::image type="content" source="../reporting-services/media/lesson-1-creating-a-report-server-project-reporting-services/select-report-server-project-template.png" alt-text="Screenshot of the New Project dialog box with Report Server Project template highlighted.":::

    > [!IMPORTANT]
    > For Visual Studio (VS), if you don't see **Report Server Project** in the template list, add the Report Designer by installing the SSDT workload. From the **Tools** menu, select **Get Tools and Features...** and select the **SQL Server Data Tools** from the workloads displayed. If you don't see the Report Services objects in the center column, add the Reporting Services extensions. From the **Extensions** menu, select **Manage extensions...**. Search for "Microsoft Reporting Services Projects" in the list of extensions, and select **Install** when you find that extension. For SSDT, see [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md). In Visual Studio 2019, if previous steps didn't work, try installing [Microsoft Reporting Service Projects extension](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio).


1. In the **Project name** box, enter "Tutorial" for the project name. By default, the **Location** box displays the path to your "Users\<username>\source\repos\" folder. Report Designer creates a folder named Tutorial below this path, and creates the Tutorial project in this folder. If the project doesn't belong to a Visual Studio solution, then Visual Studio also creates a solution file (`.sln`).

1. To create the project, select **Create**. The Tutorial project displays in the **Solution Explorer** pane on the right.
  
## Create a report definition file (RDL) 
  
1. In the **Solution Explorer** pane, right-click on the **Reports** folder. If you don't see the **Solution Explorer** pane, select **View** > **Solution Explorer**.

1. Select **Add** > **New Item**.

    :::image type="content" source="../reporting-services/media/ssrs-ssdt-add-report.png" alt-text="Screenshot of the Solution Explorer with the Add option selected on the Reports context menu.":::

1. In the **Add New Item** window, select **Report**.

1. Enter "Sales Orders.rdl" into the **Name** box.

1. Select **Add** to complete the process. Report Designer opens and displays the Sales Orders report file in Design view.

    :::image type="content" source="media/ssrs-ssdt-01-new-report-designer.png" alt-text="Screenshot of Visual Studio showing the Report Designer and the Sales Orders report in Design view." lightbox="media/ssrs-ssdt-01-report-designer-pane.png":::

In this lesson, you created the Tutorial report project and the Sales Orders report. In the remaining lessons, you're going to learn how to:

- Configure a data source for the report
- Create a dataset from the data source
- Design and format the report layout

## Related content

Continue with [Lesson 2: Specify connection information &#40;Reporting Services&#41;](../reporting-services/lesson-2-specifying-connection-information-reporting-services.md).
