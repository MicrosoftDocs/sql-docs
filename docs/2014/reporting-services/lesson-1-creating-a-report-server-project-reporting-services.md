---
title: "Lesson 1: Creating a Report Server Project (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 675671ca-e6c9-48a2-82e9-386778f3a49f
author: markingmyname
ms.author: maghan
manager: kfile
---
# Lesson 1: Creating a Report Server Project (Reporting Services)
  To create a report in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you must first create a report server project where you will save your report definition (.rdl) file and any other resource files that you need for your report. Then you will create the actual report definition file, define a data source for your report, define a dataset, and define the report layout. When you run the report, the actual data is retrieved and combined with the layout, and then rendered on your screen, from where you can export it, print it, or save it.  
  
 In this lesson, you will learn how to create a report server project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. A report server project is used to create reports that run on a report server.  
  
### To create a report server project  
  
1.  Click **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)], and then click **SQL Server Data Tools**. If this is the first time you have opened [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Business Intelligence Settings** for the default environment settings.  
  
2.  On the **File** menu, point to **New**, and then click **Project**.  
  
3.  In the **Installed Templates** list, click **Business Intelligence**.  
  
4.  Click **Report Server Project**.  
  
5.  In **Name**, type **Tutorial**.  
  
6.  Click **OK** to create the project.  
  
     The Tutorial project is displayed in Solution Explorer.  
  
### To create a new report definition file  
  
1.  In Solution Explorer, right-click **Reports**, point to **Add**, and click **New Item**.  
  
    > [!NOTE]  
    >  If the **Solution Explorer** window is not visible, from the **View** menu, click **Solution Explorer**.  
  
2.  In the **Add New Item** dialog box, under **Templates**, click **Report**.  
  
3.  In **Name**, type **Sales Orders.rdl** and then click **Add**.  
  
     Report Designer opens and displays the new .rdl file in Design view.  
  
 Report Designer is a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] component that runs in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. It has two views: **Design** and **Preview**. Click each tab to change views.  
  
 You define your data in the **Report Data** pane. You define your report layout in **Design** view. You can run the report and see what it looks like in **Preview** view.  
  
## Next Task  
 You have successfully created a report project called "Tutorial" and added a report definition (.rdl) file to the report project. Next, you will specify a data source to use for the report. See [Lesson 2: Specifying Connection Information &#40;Reporting Services&#41;](lesson-2-specifying-connection-information-reporting-services.md).  
  
## See Also  
 [Create a Basic Table Report &#40;SSRS Tutorial&#41;](create-a-basic-table-report-ssrs-tutorial.md)  
  
  
