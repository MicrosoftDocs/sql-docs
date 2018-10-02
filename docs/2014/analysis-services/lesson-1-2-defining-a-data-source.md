---
title: "Defining a Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5a3e83c9-8788-431e-85b0-a68c79377ff3
author: minewiskan
ms.author: owend
manager: craigg
---
# Defining a Data Source
  After you create an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, you generally start working with the project by defining one or more data sources that the project will use. When you define a data source, you are defining the connection string information that will be used to connect to the data source. For more information, see [Create a Data Source &#40;SSAS Multidimensional&#41;](multidimensional-models/create-a-data-source-ssas-multidimensional.md).  
  
 In the following task, you define the AdventureWorksDWSQLServer2012 sample database as the data source for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project. While this database is located on your local computer for the purposes of this tutorial, source databases are frequently hosted on one or more remote computers.  
  
### To define a new data source  
  
1.  In Solution Explorer (on the right of the Microsoft Visual Studio window), right-click **Data Sources**, and then click **New Data Source**.  
  
2.  On the **Welcome to the Data Source Wizard** page of the **Data Source Wizard**, click **Next** to open the **Select how to define the connection** page.  
  
3.  On the **Select how to define the connection** page, you can define a data source based on a new connection, based on an existing connection, or based on a previously defined data source object. In this tutorial, you define a data source based on a new connection. Verify that **Create a data source based on an existing or new connection** is selected, and then click **New**.  
  
4.  In the **Connection Manager** dialog box, you define connection properties for the data source. In the **Provider** list box, verify that **Native OLE DB\SQL Server Native Client 11.0** is selected.  
  
     [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] also supports other providers, which are displayed in the **Provider** list.  
  
5.  In the **Server name** text box, type `localhost`.  
  
     To connect to a named instance on your local computer, type **localhost\\<instance name\>**. To connect to the specific computer instead of the local computer, type the computer name or IP address.  
  
6.  Verify that **Use Windows Authentication** is selected. In the **Select or enter a database name** list, select **AdventureWorksDW2012**.  
  
7.  Click **Test Connection** to test the connection to the database.  
  
8.  Click **OK**, and then click **Next**.  
  
9. On the **Impersonation Information** page of the wizard, you define the security credentials for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to use to connect to the data source. Impersonation affects the Windows account used to connect to the data source when Windows Authentication is selected. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not support impersonation for processing OLAP objects. Select **Use the service account**, and then click **Next**.  
  
10. On the **Completing the Wizard** page, accept the default name, **Adventure Works DW 2012**, and then click **Finish** to create the new data source.  
  
> [!NOTE]  
>  To modify the properties of the data source after it has been created, double-click the data source in the **Data Sources** folder to display the data source properties in **Data Source Designer**.  
  
## Next Task in Lesson  
 [Defining a Data Source View](lesson-1-3-defining-a-data-source-view.md)  
  
## See Also  
 [Create a Data Source &#40;SSAS Multidimensional&#41;](multidimensional-models/create-a-data-source-ssas-multidimensional.md)  
  
  
