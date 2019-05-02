---
title: "Lesson 1: Creating a Report Server Project (Reporting Services) | Microsoft Docs"
ms.date: 05/01/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 675671ca-e6c9-48a2-82e9-386778f3a49f
author: maggiesMSFT
ms.author: maggies
---
# Lesson 1: Creating a Report Server Project (Reporting Services)

In this lesson, you create a *report server project* and a *report definition (.rdl)* file using *Report Designer*.

> [!NOTE]
> [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment, where you can open, modify, preview, save, and deploy [!INCLUDE[ssrsnoversion_md](../includes/ssrsnoversion-md.md)] paginated report definitions, shared data sources, shared datasets, and report parts.

When you create reports with Report Designer, it creates a report server project that contains the report files and other resource files used by the report(s).

## To create a report server project
  
1. From the **File menu**, select **New** > **Project**.  

    ![ssrs-ssdt-file-01-new-project](../reporting-services/media/ssrs-ssdt-file-01-new-project.png)
  
2. In the left-most column under **Installed**, select **Reporting Services**. In some cases, it may be under the group **Business Intelligence**.

    ![select-report-server-project-template](../reporting-services/media/lesson-1-creating-a-report-server-project-reporting-services/select-report-server-project-template.png)

    > [!IMPORTANT]
    > For VS, if you don't see Reporting Services in the left column, add the Report Designer by installing the SSDT workload. From the **Tools menu**, select **Get Tools and Features...** and select the **SQL Server Data Tools** from the workloads displayed. If you don't see the Report Services objects in the center column, add the Reporting Services extensions. From the **Tools menu**, select **Extensions and Updates** > **Online**. In the center column, select **Microsoft Reporting Services Projects** > **Download** from the displayed extensions. For SSDT, See [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md).

3. Select the **Report Server Project icon** &nbsp;&nbsp;![ssrs_ssdt_report_server_project](media/ssrs-ssdt-report-server-project.png) &nbsp;&nbsp;in the center column of the **New Project dialog box**.

4. In the **Name: text box**, type "Tutorial" for the project name. By default, the **Location: text box** displays the path to your "Documents\Visual Studio 20xx\Projects\" folder. Report Designer creates a folder named Tutorial below this path, and creates the Tutorial project in this folder. If the project doesn't belong to a VS solution, then VS also creates a solution file (.sln).

5. Select the **OK button** to create the project. The Tutorial project is displayed in the **Solution Explorer pane** on the right.
  
## Creating a report definition file (RDL)  
  
1. In the **Solution Explorer pane**, right-click on the **Reports folder**.

2. Select **Add** > **New Item**.

    ![ssrs_ssdt_add_report](../reporting-services/media/ssrs-ssdt-add-report.png)

    > [!TIP]  
    > If you don't see the **Solution Explorer pane**, from the **View menu**, select **Solution Explorer**, or select the **Solution Explorer tab** if visible on the far right (outlined with a red rectangle) as displayed below.
    >
    > ![Position of the Solution Explorer pane in Visual Studio](media/lesson-1-creating-a-report-server-project-reporting-services/position-of-solution-explorer-in-vs-layout.png)

3. In the **Add New Item window**, select the **Report icon**.

4. Type "Sales Orders.rdl" into the **Name: text box**.

5. Select the **Add button** on the lower right side of the **Add New Item dialog box** to complete the process. Report Designer opens and displays the Sales Orders report file in Design view.

    ![ssrs-ssdt-01-new-report-designer](media/ssrs-ssdt-01-new-report-designer.png)

## Next steps

So far you've created the Tutorial report project and the Sales Orders report. In the remaining lessons, you're going to learn how to:

- Configure a data source for the report.
- Create a dataset from the data source.
- Design and format the report layout.

Continue with [Lesson 2: Specifying Connection Information &#40;Reporting Services&#41;](../reporting-services/lesson-2-specifying-connection-information-reporting-services.md).
