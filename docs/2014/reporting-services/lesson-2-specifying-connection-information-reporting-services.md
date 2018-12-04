---
title: "Lesson 2: Specifying Connection Information (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 54405a3a-d7fa-4d95-8963-9aa224e5901e
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Lesson 2: Specifying Connection Information (Reporting Services)
  After you add a report to the Tutorial project, you need to define a *data source*, which is connection information the report uses to access data from either a relational database, multidimensional database, or other resource.  
  
 In this lesson, you will use the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] sample database as your data source. This tutorial assumes that this database is located in a default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] that is installed on your local computer.  
  
### To set up a connection  
  
1.  In the **Report Data** pane, click **New** and then click **Data Source...**.  
  
    > [!NOTE]  
    >  If the **Report Data** pane is not visible, from the **View** menu, click **Report Data**.  
  
2.  In **Name**, type [!INCLUDE[ssSampleDBUserInputNonLocal](../includes/sssampledbuserinputnonlocal-md.md)].  
  
3.  Make sure **Embedded connection** is selected.  
  
4.  In **Type**, select **Microsoft SQL Server**.  
  
5.  In **Connection string**, type the following:  
  
    ```  
    Data source=localhost; initial catalog=AdventureWorks2012  
    ```  
  
     This connection string assumes that [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the report server, and the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database are all installed on the local computer and that you have permission to log on to the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database.  
  
    > [!NOTE]  
    >  If you are using [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] with Advanced Services or a named instance, the connection string must include instance information:  
    >   
    >  `Data source=localhost\SQLEXPRESS; initial catalog=AdventureWorks2012`  
    >   
    >  For more information about connection strings, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](data-connections-data-sources-and-connection-strings-in-reporting-services.md) and [Data Source Properties Dialog Box, General](data-source-properties-dialog-box-general.md).  
  
6.  Click **Credentials** in the left pane and click **Use Windows Authentication (integrated security)**.  
  
7.  [!INCLUDE[clickOK](../includes/clickok-md.md)] data source [!INCLUDE[ssSampleDBnormal](../includes/sssampledbnormal-md.md)] is added to the **Report Data** pane.  
  
## Next Task  
 You have successfully defined a connection to the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] sample database. Next, you will create the report. See [Lesson 3: Defining a Dataset for the Table Report &#40;Reporting Services&#41;](lesson-3-defining-a-dataset-for-the-table-report-reporting-services.md).  
  
## See Also  
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](data-connections-data-sources-and-connection-strings-in-reporting-services.md)  
  
  
