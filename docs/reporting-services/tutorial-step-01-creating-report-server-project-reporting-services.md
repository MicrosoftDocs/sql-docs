---
title: "Tutorial: Create a report server project"
description: "In this article, you learn how to use Report Designer to create a report server project and a report definition (.rdl) file."
author: maggiesMSFT
ms.author: maggies
ms.date: 05/31/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL server user, I want to create a sample report project and report so that I can continue with the other lessons in this tutorial.
---

# Tutorial: Create a report server project

This tutorial is the first step in a six-step process that demonstrates how to create a SQL Server Reporting Services (SSRS) paginated report.

In this tutorial, you:

> [!div class="checklist"]
> * Start a new report server project
> * Create a Sales Orders report definition file (RDL)

## Create a report server project

First, you create a report server project by using SSRS. The project you set up within Visual Studio serves as the development environment for your reporting solutions.

 > [!NOTE]
 >[!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment. In this environment, you can open, modify, preview, save, and deploy [!INCLUDE[ssrsnoversion_md](../includes/ssrsnoversion-md.md)] paginated report definitions. You can also use and modify shared data sources, shared datasets, and report parts.
 >
 > Report parts are deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019. The report parts are no longer supported starting in SQL Server Reporting Services 2022 and Power BI Report Server.

1. In Visual Studio (VS), select **New** > **Project** from the **File** menu.   

    :::image type="content" source="../reporting-services/media/ssrs-ssdt-file-01-new-project.png" alt-text="Screenshot of Visual Studio showing the project option selected in the New menu in the File menu.":::
  
1. In the list of recently used templates, select **Report Server Project**, and then choose **Next**. You can also search for "Report Server Project".

    :::image type="content" source="../reporting-services/media/lesson-1-creating-a-report-server-project-reporting-services/select-report-server-project-template.png" alt-text="Screenshot of the New Project dialog with the Report Server Project template highlighted.":::

    > [!IMPORTANT]
    > For VS, if you don't see **Report Server Project** in the template list, add the Report Designer by installing the SSDT workload. 
    > 1. From the **Tools** menu, select **Get Tools and Features...**.
    > 1. Select the **SQL Server Data Tools** from the workloads displayed. 
    >
    > If you don't see the Report Services objects in the center column, add the Reporting Services extensions. 
    >
    > 1. From the **Extensions** menu, select **Manage extensions...**. 
    > 1. Search for "Microsoft Reporting Services Projects" in the list of extensions.
    > 1. Select **Install** when you find that extension. 
    >
    > In Visual Studio 2019, if previous steps didn't work, try installing Microsoft Reporting Service Projects extension.

1. In the **Project name** box, enter "Tutorial" for the project name. By default, the **Location** box displays the path to the `Users\<username>\source\repos\` folder. Report Designer creates a folder named Tutorial below this path and creates the Tutorial project. If the project doesn't belong to a Visual Studio solution, then Visual Studio creates a solution file (`.sln`).

1. To create the project, select **Create**. The Tutorial project displays in the **Solution Explorer** pane on the right.
  
## Create a report definition file (RDL) 

Next, you create a report definition file (RDL). This process involves setting up the report within the project you created.
  
1. In the **Solution Explorer** pane, right-click the **Reports** folder. If you don't see the **Solution Explorer** pane, select **View** > **Solution Explorer**.

1. Select **Add** > **New Item**.

    :::image type="content" source="../reporting-services/media/ssrs-ssdt-add-report.png" alt-text="Screenshot of the Solution Explorer with the Add option selected on the Reports context menu.":::

1. In the **Add New Item** window, select **Report**.

1. Enter "Sales Orders.rdl" into the **Name** box.

1. Select **Add** to complete the process. Report Designer opens and displays the Sales Orders report file in Design view.

    :::image type="content" source="media/ssrs-ssdt-01-new-report-designer.png" alt-text="Screenshot of Visual Studio showing the Report Designer and the Sales Orders report in Design view." lightbox="media/ssrs-ssdt-01-report-designer-pane.png":::


## Next step

> [!div class="nextstepaction"]
> [Step 2: Specify connection information &#40;Reporting Services&#41;](../reporting-services/lesson-2-specifying-connection-information-reporting-services.md)

