---
title: "Connect to a Data Mining Server | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "connections"
  - "getting started"
ms.assetid: 85962ad6-d840-4bc6-905e-c667c3276944
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Data Mining Server
  ![Connections button](media/misc-connection.gif "Connections button")  
  
 Click the **Connection** button to select an existing connection, or to create a new connection to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 **Why do I need to connect to a server?**  
  
 When you create a connection, it enables you to use the data mining algorithms that are provided by the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server, and to take advantage of the optimized processing of the server.  
  
 You do not have to keep your data or your results in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, or in a SQL Server database. The Excel data mining add-ins can work with data stored only in Excel, or data that you connect to as an Excel data source.  
  
## How to Create a New Connection  
  
1.  Click the **Connection** button.  
  
2.  In the **Analysis Services Connections** dialog box, click **New**.  
  
3.  In the **Connect to Analysis Services** dialog box, type a server name.  
  
4.  Specify the authentication method.  
  
5.  Specify the catalog, or [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, where you will store your data mining models.  
  
    > [!NOTE]  
    >  If you have not created any databases yet, you can use (default) to create and then test the connection; however, you cannot add mining models to the default connection. Before you create any mining models, you should create a connection to an existing database.  
  
6.  If you are connecting to the server via a Web service, type the user name and password required by the Web service.  
  
7.  Type a friendly name for the new connection.  
  
8.  Click **Test Connection** to verify that the server is available.  
  
## Troubleshooting Connections  
 This section provides answers to some common questions about connections to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 **I get a message saying "No connection found."**  
  
 If the text in the lower part of the button says **No Connection**, it means that you have not created a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, or that the connection failed. You can continue to work with data in Excel from Access or other sources, but to create a data mining model or run a prediction query you must have an active connection.  
  
 **Suppose I don't have permission to use the server?**  
  
 If you do not have sufficient permissions to store your mining models on the server, or if you want to experiment with data mining and not save your work, you can use the Table Analysis Tools, which create temporary data structures and temporary models. You still need to be able to store temporary models on the server. Ask your administrator to enable use of *session mining models* on the server.  
  
 If you want to ensure that your models are saved, you can disable the option to use temporary models, or you can use the wizards in the Data Mining Client. These wizards store all models on the server. You will need administrative access to the database where the models are stored, so we recommend that you get the administrator to create a database especially for creating mining models with the add-ins.  
  
 **I lost my connection; did I lose all my work?**  
  
 If you terminate the connection to the server, your results and data will not be lost, because they are stored in Excel. However, if you created some temporary models, those models will be deleted from the server after a short time. So if you lose the connection temporarily, sometime the models won't be deleted yet.  
  
 Any data or results that were generated will not be lost, because all reports and tables are stored in Excel.  
  
> [!NOTE]  
>  Do not disconnect from the server or from the network while the add-in is communicating with the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server. For example, never disconnect when a model is being created, or when the data is being processed. In some situations your data could be corrupted.  
  
 **I cannot connect to the model using the Visio tools**  
  
 The Data Mining Templates for Visio cannot use temporary mining structures and models. To create a diagram of a mining model, the model must be stored on a server.  
  
 **How can I monitor usage of the connection?**  
  
 The [Trace &#40;Data Mining Client for Excel&#41;](trace-data-mining-client-for-excel.md) tool creates a log of all activity between the add-ins and the specified server.  
  
 To enable monitoring of session models, select the **Use session models** option in the **Tracer** dialog box.  
  
 The trace lets you view the DMX and XMLA commands generated while models and structures are being created. You can also view the queries that are sent by the client to generate results and reports in Excel.  
  
 **What is a temporary model? How can I save a temporary model?**  
  
 The Table Analysis Tools for Excel by default creates temporary data structures and mining models. You can continue to browse and query temporary models as long as you keep the workbook open and do not disconnect from [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 The structures and models that you have created will be deleted as soon as you close the Excel workbook, or if you change or end your connection to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 When you complete a wizard in the Data Mining Client for Excel, models are saved to the server by default, but on the final page of each wizard there is the option to use a temporary model. If you select this option, the data mining structure and model that you created are stored in a temporary file. You can browse, manage, and modify the structure and model as long as Excel remains open. However, once you close Excel, the structure and any related models are deleted.  
  
 You can also explicitly create a temporary structure or model by using the **Data Mining Advanced Query Editor** and selecting one of the DMX templates. Add the `USE SESSION MODELS` clause to specify that objects be temporary.   
  
### Creating Backups of Mining Models and Structures  
 To create a backup of a model or structure, you can export it by using [Manage Models &#40;SQL Server Data Mining Add-ins&#41;](manage-models-sql-server-data-mining-add-ins.md), in the Data Mining Client for Excel.  
  
 If you created a temporary mining model, it typically has a name that is difficult to understand, such as Table5_593679_TS_62446. However, you can use the [Trace &#40;Data Mining Client for Excel&#41;](trace-data-mining-client-for-excel.md) tool to discover the names of temporary structures and models that were created by the Table Analysis Tools and then back them up using **Manage Models**.  
  
##### Identify and export a temporary model  
  
1.  In the **Connections** group of the Data Mining Client for Excel, click **Trace**.  
  
2.  View the connection activity log, and locate the model by reviewing the columns and predictable outputs (for example).  
  
     Advanced users: If you are familiar with DMX or XMLA, you can copy the statements to a file for later use.  
  
3.  When you have found the name of the temporary model and structure, open **Manage Model** and select the model.  
  
4.  Click Export this mining model to generate a script file in a location you specify.  
  
## See Also  
 [Connect to Source Data &#40;Data Mining Client for Excel&#41;](connect-to-source-data-data-mining-client-for-excel.md)   
 [Server Configuration Utility &#40;Data Mining Add-ins for Excel&#41;](server-configuration-utility-data-mining-add-ins-for-excel.md)  
  
  
