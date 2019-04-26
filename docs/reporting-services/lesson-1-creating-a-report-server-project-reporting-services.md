---
title: "Lesson 1: Creating a Report Server Project (Reporting Services) | Microsoft Docs"
ms.date: 04/22/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 675671ca-e6c9-48a2-82e9-386778f3a49f
author: maggiesMSFT
ms.author: maggies
---

# Lesson 1: Creating a Report Server Project (Reporting Services)

In this lesson, you'll create a *report server project* and a *report definition (.rdl)* file using the Report Designer tool within SQL Server Data Tools (SSDT) / Visual Studio.  

>[!NOTE]
> SSDT is a free development tool of MSSQL Server (a shell of Visual Studio to create projects for Reporting Services, Integration Services and Analysis Services. More detailed information about SSDT can be found here: [Reporting Services in SQL Server Data Tools (SSDT)](tools/reporting-services-in-sql-server-data-tools-ssdt.md)
>
When using the Report Designer to create a report, create a Report Server project first to contain the report files and other resource files used by the reports.

## To create a report server project  
  
1. Open Visual Studio or (SSDT).  
  
2. From the **File** menu, select **New** > **Project**.  

    ![ssrs-ssdt-file-01-new-project](../reporting-services/media/ssrs-ssdt-file-01-new-project.png)
  
3. In the left column under **Installed**, select **Reporting Services**.

    ![select-report-server-project-template](../reporting-services/media/lesson-1-creating-a-report-server-project-reporting-services/select-report-server-project-template.png)

    > [!TIP]  
    > If you don't see **Reporting Services** in the left column or **Report Services** objects in the center column, you need to add the Report Designer to Visual Studio / SSDT by installing the Reporting Services extensions. See [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md) if you are using SSDT, or install the SSDT workload from the Visual Studio installation manager, then the Reporting Services extensions from the Market Place.
  
4. Select the **Report Server Project** icon displayed here (the other one is a Report Project Wizard) &nbsp;&nbsp;![ssrs_ssdt_report_server_project](../reporting-services/media/ssrs-ssdt-report-server-project.png) &nbsp;&nbsp;in the center template display area.

5. In the **Name** text box, type **Tutorial** for the project name. By default, the **Tutorial** project is created in its own folder in your "Documents\Visual Studio 20xx\"Projects\" folder.

6. Select **OK** to create the project. The Tutorial project is displayed in the Solution Explorer pane on the right.
  
## To create a new report definition file  
  
1. In the **Solution Explorer** pane, right-click on the **Reports**  folder then:
    a. Select **Add** > **New Item**

     ![ssrs_ssdt_add_report](../reporting-services/media/ssrs-ssdt-add-report.png)

    > [!TIP]  
    > If you don't see the **Solution Explorer** pane, on the **View** menu, select the **Solution Explorer** tab on the far right (outlined with a red rectangle).

    ![Position of the Solution Explorer pane in Visual Studio](media/lesson-1-creating-a-report-server-project-reporting-services/position-of-solution-explorer-in-vs-layout.png)

    b. In the **Add New Item** window, select the **Report** icon.  

    c. Type **Sales Orders.rdl** into the **Name** text box.

    d. Select **Add** to complete the process.
  
 Report Designer opens and displays the new report file in Design view.  

 ![ssrs-ssdt-01-new-report-designer](../reporting-services/media/ssrs-ssdt-01-new-report-designer.png)
  
> [!NOTE]  
> Report Designer is a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] component that runs in Visual Studio / [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].
>
The Report Designer has:
>
- a **Report Data** pane to define and select data fields
- a  **Design** view to define your report layout
- a **Preview** view to display the current output of the report as you design it  
  
## Next lesson

In this lesson, we created the "Tutorial" report project and the "Sales Orders" report. In the remaining lessons, you'll learn how to:

- select a data source for the report
- create a dataset from the data source
- design and format the report layout

 See [Lesson 2: Specifying Connection Information &#40;Reporting Services&#41;](../reporting-services/lesson-2-specifying-connection-information-reporting-services.md).  

> [!NOTE]
> When running a report, the data is retrieved as defined in the data set from the data source and formatted by the report layout. The report output is then displayed on your screen, where you may view it, print it, export it to another data format, or save it to a file.  

## See Also

[Create a Basic Table Report &#40;SSRS Tutorial&#41;](../reporting-services/create-a-basic-table-report-ssrs-tutorial.md)
