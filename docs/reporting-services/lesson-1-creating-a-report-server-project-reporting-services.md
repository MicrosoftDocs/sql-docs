---
title: "Lesson 1: Create a Report Server Project | Microsoft Docs"
description: "In this lesson, you create a report server project and a report definition (.rdl) file using Report Designer."
ms.date: 12/09/2019
ms.service: reporting-services
ms.subservice: reporting-services

ms.topic: conceptual
ms.assetid: 675671ca-e6c9-48a2-82e9-386778f3a49f
author: maggiesMSFT
ms.author: maggies
---
# Lesson 1: Create a Report Server Project (Reporting Services)

In this lesson, you create a *report server project* and a *report definition (.rdl)* file using *Report Designer*.

> [!NOTE]
> [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment, where you can open, modify, preview, save, and deploy [!INCLUDE[ssrsnoversion_md](../includes/ssrsnoversion-md.md)] paginated report definitions, shared data sources, shared datasets, and report parts.
>
> Report parts are deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019, and discontinued starting in SQL Server Reporting Services 2022 and Power BI Report Server.


When you create reports with Report Designer, it creates a report server project that contains the report files and other resource files used by the report(s).

## To create a report server project
  
1. From the **File** menu, select **New** > **Project**.  

    ![Screenshot of Visual Studio showing File > New > Project selected.](../reporting-services/media/ssrs-ssdt-file-01-new-project.png)
  
2. In the left-most column under **Installed**, select **Reporting Services**. In some cases, it may be under the group **Business Intelligence**.

    ![Screenshot of the New Project dialog box showing Reporting Services selected and the Report Server Project template highlighted.](../reporting-services/media/lesson-1-creating-a-report-server-project-reporting-services/select-report-server-project-template.png)

    > [!IMPORTANT]
    > For VS, if you don't see Reporting Services in the left column, add the Report Designer by installing the SSDT workload. From the **Tools** menu, select **Get Tools and Features...** and select the **SQL Server Data Tools** from the workloads displayed. If you don't see the Report Services objects in the center column, add the Reporting Services extensions. From the **Tools** menu, select **Extensions and Updates** > **Online**. In the center column, select **Microsoft Reporting Services Projects** > **Download** from the displayed extensions. For SSDT, See [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md). In Visual Studio 2019, if previous steps didn't work, try installing [Microsoft Reporting Service Project extension](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio).


3. Select the **Report Server Project** icon &nbsp;&nbsp;![ssrs_ssdt_report_server_project](media/ssrs-ssdt-report-server-project.png) &nbsp;&nbsp;in the center column of the **New Project** dialog box.

4. In the **Name** text box, type "Tutorial" for the project name. By default, the **Location** text box displays the path to your "Documents\Visual Studio 20xx\Projects\" folder. Report Designer creates a folder named Tutorial below this path, and creates the Tutorial project in this folder. If the project doesn't belong to a VS solution, then VS also creates a solution file (.sln).

5. Select **OK** to create the project. The Tutorial project is displayed in the **Solution Explorer** pane on the right.
  
## Creating a report definition file (RDL)  
  
1. In the **Solution Explorer** pane, right-click on the **Reports** folder. If you don't see the **Solution Explorer** pane, select **View** menu > **Solution Explorer**.

2. Select **Add** > **New Item**.

    ![Screenshot of Solution Explorer showing Reports > Add > New Item selected.](../reporting-services/media/ssrs-ssdt-add-report.png)

3. In the **Add New Item** window, select the **Report** icon.

4. Type "Sales Orders.rdl" into the **Name** text box.

5. Select the **Add button** on the lower right side of the **Add New Item** dialog box to complete the process. Report Designer opens and displays the Sales Orders report file in Design view.

    ![Screenshot of Visual Studow showing the Report Designer and the Sales Orders report in Design view.](media/ssrs-ssdt-01-new-report-designer.png)

## Next steps

So far you've created the Tutorial report project and the Sales Orders report. In the remaining lessons, you're going to learn how to:

- Configure a data source for the report.
- Create a dataset from the data source.
- Design and format the report layout.

Continue with [Lesson 2: Specifying Connection Information &#40;Reporting Services&#41;](../reporting-services/lesson-2-specifying-connection-information-reporting-services.md).
