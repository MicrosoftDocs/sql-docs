---
title: "Connect to Source Data (Data Mining Client for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "connections"
ms.assetid: 548672ce-e403-4aca-b67a-c2c797f053dd
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to Source Data (Data Mining Client for Excel)
  This topic describes how to create and use connections used for storing data mining models, and for accessing external data stored in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 **Data mining connections.** The initial connection that you create when you start the add-ins is used to access algorithms, analyze data, and store mining structure and models.  
  
 A connection to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is required to use the modeling and visualization tools in the add-ins, because the add-ins depend on algorithms and data structures that are provided by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 **Connections to external data sources.** You can also create connections to external data as you are building models or saving the results. For example, you can create a data mining model on one server, and then perform a prediction query against the data mining model by using data stored in another instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], in an Excel data table, or in an external data source such as [!INCLUDE[msCoName](../includes/msconame-md.md)] Access. Each time that you access a new data source, you will be prompted to create a connection by using a dialog box.  
  
##  <a name="bkmk_prereq2"></a> Prerequisites  
 This version of the add-ins requires that your instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] be SQL Server 2012. A separate version of the add-ins is available if you want to connect to an earlier version of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. There are versions of the add-ins that support SQL Server 2005, SQL Server 2008, and SQL Server 2008 R2.  
  
 To connect to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, you must have permissions to access the database server. Moreover, data mining sessions must be enabled, and you must have read or read/write permissions on database objects stored on the server.  
  
##  <a name="bkmk_connect"></a> Creating Data Mining Server Connections  
 The **Connections** group in the Data Mining Client for Excel and the Table Analysis Tools for Excel provides tools for managing connections to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
-   You can create the connection when you install the add-in, or you can add a connection later.  
  
-   You can create multiple connections, and change connections at any time, unless you are in the process of creating or querying a model.  
  
     Do not change or close a connection when a data mining model is being processed. The data mining model might lose data, or the model might become unusable.  
  
-   Only one connection can be active at a particular time.  
  
### Connections in the Excel Add-ins  
 The **Connections** group in the Data Mining Client for Excel and the Table Analysis Tools for Excel is where you manage connections to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
##### Create a new server connection in the Excel add-ins  
  
1.  Click the **Connection** button on the **Analyze** or **Data Mining** ribbon.  
  
    > [!NOTE]  
    >  The text of the button indicates if a connection exists. When no connection has been made in the worksheet, the button contains the text "\<No connection>." If a connection was previously made in the workbook, the name of that connection appears in the button.  
  
2.  In the **Analysis Services Connections** dialog box, click **New**.  
  
3.  In the **New Analysis Services Connection** dialog box, type the name of the server.  
  
4.  Specify the authentication method.  
  
5.  Select a database from the **Catalog name** drop-down list. If no database exists on the instance, select **(default)**.  
  
6.  Type a friendly name for the connection.  
  
7.  Click **Test Connection** to verify that the server and database are available.  
  
8.  Click **OK**, and then click **Close**.  
  
### Connections using a Web Service  
 If you are using a thin-client architecture to enable browsing of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] cubes and data, you can also configure a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server through Web services. For information about how to define a Web-based client, see [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.  
  
 If you have access to a server that has been configured for Web services, you can specify the connection type when you first create the connection.  
  
##### Create an HTTP connection to Analysis Services  
  
1.  Open the **New Analysis Services Connection** dialog box.  
  
2.  For the server name, type http:// followed by the URL assigned to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server.  
  
3.  Type the user name and the password that is required to access the Web service.  
  
### Connections in the Visio Add-In  
 Unlike Excel, Visio does not provide a tool ribbon, and there are no buttons specifically for creating or monitoring connections. Instead, the data connection is created when you first select a data mining shape and drop it onto a Visio page. A wizard will prompt you to select the model for the shape and to set other options.  
  
 If you have previously used a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source in Excel, these connections are listed as possible data sources from which to select.  
  
##### Create a connection for a Visio shape  
  
1.  Open the Data Mining Template, and select one of the data mining shapes.  
  
2.  Drag and drop the shape to a blank page.  
  
3.  In the **Select a data source** dialog box, select a data source from the list, or click **New**.  
  
4.  If you select **New**, follow the procedure that is described earlier to specify a server and catalog name, or to connect through a Web service.  
  
##  <a name="bkmk_change"></a> Changing Connections  
 You can create multiple connections in the same worksheet, but only one connection can be active at a time. The name of the current connection is displayed in the **Connection** button.  
  
 In the Data Mining Client for Excel, you can also verify the connection string and status for the current connection by clicking **Trace** and then clicking **Current Connection**.  
  
#### Use a different server connection  
  
1.  Click **Connection**.  
  
2.  In the **Analysis Services Connections** pane, select a connection from the **Other Connections** list, and click **Make Current**.  
  
3.  Click **Test Connection** to verify that the connection is available.  
  
 After a mining model has finished processing, the results are stored locally, and there is no effect on the data if you close the connection to one server and then connect to another server. However, you should avoid changing connections or losing the connection when a data mining model is being processed, because this could corrupt data.  
  
#### Modify an existing server connection  
  
1.  You cannot modify an existing connection; if you want to connect to a different database or a different server, you should create a new connection.  
  
2.  If you must modify the connection string to increase the query timeout or add other parameters specific to your instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], one option is to edit the .dmc file, where the connection string is stored.  
  
     \<drive:>\Users\\<myusername\>\AppData\Local\Microsoft\Data Mining Add-in  
  
##  <a name="bkmk_extconnections"></a> Connecting to External Data Sources  
 Whereas the tools in the **Analyze** ribbon work exclusively with data in Excel, the tools in the **Data Mining** ribbon let you connect directly to external data sources to use as inputs for your model, or for sampling.  
  
 The following tools in these add-ins support use of external data for data mining:  
  
-   [Sample Data &#40;SQL Server Data Mining Add-ins&#41;](sample-data-sql-server-data-mining-add-ins.md)  
  
-   [Classify Wizard &#40;Data Mining Add-ins for Excel&#41;](classify-wizard-data-mining-add-ins-for-excel.md)  
  
-   [Estimate Wizard &#40;Data Mining Add-ins for Excel&#41;](estimate-wizard-data-mining-add-ins-for-excel.md)  
  
-   [Cluster Wizard &#40;Data Mining Add-ins for Excel&#41;](cluster-wizard-data-mining-add-ins-for-excel.md)  
  
-   [Forecast Wizard &#40;Data Mining Add-ins for Excel&#41;](forecast-wizard-data-mining-add-ins-for-excel.md)  
  
-   [Create Mining Structure &#40;SQL Server Data Mining Add-ins&#41;](create-mining-structure-sql-server-data-mining-add-ins.md)  
  
-   [Accuracy Chart &#40;SQL Server Data Mining Add-ins&#41;](accuracy-chart-sql-server-data-mining-add-ins.md)  
  
-   [Profit Chart &#40;SQL Server Data Mining Add-ins&#41;](profit-chart-sql-server-data-mining-add-ins.md)  
  
-   [Classification Matrix &#40;SQL Server Data Mining Add-ins&#41;](classification-matrix-sql-server-data-mining-add-ins.md)  
  
### Using Analysis Services as a Data Source  
 You cannot directly access data stored in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] cube or tabular model. Instead, create a connection in Excel to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server, and use the data to create a model.  
  
### Relational Data Sources  
 If you want to use data from a relational source as input to your model, you can connect to the following versions of SQL Server:  
  
-   SQL Server 2008  
  
-   SQL Server 2008 R2  
  
-   SQL Server 2012  
  
 You can also get data from any other relational data source that is supported as a data source by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. For information about supported data sources, see [Data Sources in Multidimensional Models](multidimensional-models/data-sources-in-multidimensional-models.md)  
  
 Note that the following data types cannot be used for data mining and will result in an error if included when you build a model:  
  
-   ntext  
  
-   binary  
  
## See Also  
 [Trace &#40;Data Mining Client for Excel&#41;](trace-data-mining-client-for-excel.md)  
  
  
