---
title: "Lesson 2: Specifying Connection Information (Reporting Services) | Microsoft Docs"
ms.date: 04/29/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 54405a3a-d7fa-4d95-8963-9aa224e5901e
author: maggiesMSFT
ms.author: maggies
---
# Lesson 2: Specifying Connection Information (Reporting Services)

In lesson 1, you added a [!INCLUDE[ssrsnoversion-md](../includes/ssrsnoversion-md.md)] paginated report to your tutorial project.
  
In this lesson, you will define a *data source*, which is the connection information the report uses to access data from a relational database, multidimensional database, or another source. For this report, you will add the **AdventureWorks2016** sample database as your data source. For simplicity, this tutorial assumes that this database is located in a default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] installed on your local computer.  

## To set up a connection  

1. In the **Report Data** pane, click **New** > **Data Source**. If the **Report Data** pane is not visible, from the **View** menu, select **Report Data** or type *Ctrl-Alt-D*.

    ![ssrs-table-tutorial-2-new-data-source](media/ssrs-table-tutorial-2-new-data-source.png)

    The *Data Source Properties* dialog box opens with the *General* section selected..

    ![The Data Source Properties Dialog Box](media/lesson-2-specifying-connection-information-reporting-services/vs-datasource-connection-properties-dialog-box.png)

2. In the **Name:** text box, type *AdventureWorks2016*.

3. Select the **Embedded connection:** radio button.

4. In the **Type:** dropdown, type or select *Microsoft SQL Server*.
  
5. In the **Connection string:** text box, type the following:
  
    `Data source=localhost; initial catalog=AdventureWorks2016`

    > [!NOTE]
    > This connection string assumes that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the report server, and the  **AdventureWorks2016** database are all installed on the local computer and that you have permission to access the **AdventureWorks2016**  database. If your **AdventureWorks2016** database is not on the local computer, change the connection string and replace **localhost** with the name of your database server instance. If you are using [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] with Advanced Services or a named instance, the connection string must include instance information:
    >
        `Data source=localhost\SQLEXPRESS; initial catalog=AdventureWorks2016`
    >
    For more information about connection strings, see below under the **See also** section.

6. Select the Credentials tab, and under the section *Change the credentials used to connect to the data source.*, select the **Use Windows Authentication (integrated security)** radio button.

7. Select the **OK** button to complete the process.

The data source **AdventureWorks2016** is added to the **Report Data** pane.

   ![ssrs-adventureworks-datasource](media/lesson-2-specifying-connection-information-reporting-services/ssrs-adventureworks-datasource2016.png)

## Next lesson

In this lesson, we have successfully defined a connection to the **AdventureWorks2016** sample database. Next, you will create the report. Continue with [Lesson 3: Defining a Dataset for the Table Report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md).  
  
## See also

[Data Connections, Data Sources, and Connection Strings in Reporting Services](report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)
