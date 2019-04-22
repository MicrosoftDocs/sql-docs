---
title: "Retrieve Data from a Data Mining Model (DMX) (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "retrieving report data"
  - "datasets [Reporting Services], with DMX queries"
  - "datasets [Reporting Services], Analysis Services"
  - "queries [Reporting Services], data mining prediction"
ms.assetid: d9cd3624-1594-4707-8887-55437dd7e07c
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Retrieve Data from a Data Mining Model (DMX) (SSRS)
  To use data from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data mining model in your report, you must define a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source and one or more report datasets. When you create the data source definition, you must specify a connection string and credentials so that you can access the data source from your client computer.  
  
 You can create an embedded data source definition for use by a single report or a shared data source definition that can be used by multiple reports. The procedures in this topic describe how to create an embedded data source. For more information about shared data sources, see [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](../embedded-and-shared-data-connections-or-data-sources-report-builder-and-ssrs.md) and [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](create-modify-and-delete-shared-data-sources-ssrs.md).  
  
 After you create a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source, you can create one or more datasets. For each dataset, you use a Data Mining Prediction Expression (DMX) query designer to create a DMX query that specifies the field collection. For more information, see [Analysis Services DMX Query Designer User Interface](analysis-services-dmx-query-designer-user-interface.md).  
  
 After you create a dataset, the name of the dataset appears in the Report Data pane as a node under its data source.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
### To create an embedded Microsoft SQL Server Analysis Services data source  
  
1.  On the toolbar in the Report Data pane, click **New**, and then click **Data Source**.  
  
2.  In the **Data Source Properties** dialog box, type a name in the **Name** text box, or accept the default name.  
  
3.  Verify that **Embedded connection** is selected.  
  
4.  From the **Type** drop-down list, select **Microsoft SQL Server Analysis Services**.  
  
5.  Specify a connection string that works with your [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source.  
  
     Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies the sample [!INCLUDE[ssSampleDBDWobject](../../includes/sssampledbdwobject-md.md)] database on the local client.  
  
    ```  
    Data Source=localhost;Initial Catalog=AdventureWorksDW2012  
    ```  
  
6.  Click **Credentials**.  
  
     Set the credentials to use to connect to the data source. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../integration-services/connection-manager/data-sources.md).  
  
    > [!NOTE]  
    >  To test the data source connection, click **Edit**. In the **Connection Properties** dialog box, click **Test Connection**. If the test is successful, you will see the information message "Test connection succeeded." If the test fails, you will see a warning message with more information about why the test was not successful.  
  
7.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
     The data source appears in the Report Data pane.  
  
### To create a dataset for a Microsoft SQL Server Analysis Services  
  
1.  In the **Report Data** pane, right-click the name of the data source that connects to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source, and then click **Add Dataset**.  
  
2.  In the **Dataset Properties** dialog box, type a name in the **Name** text box.  
  
3.  In the **Data source box**, verify that the name is the name of a data source that connects to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source.  
  
4.  Click **Query Designer** to open the graphical query designer to build a query interactively. If the query designer opens in MDX mode, click **Command Type DMX** (![Change to DMX query language view](../media/rsqdicon-commandtypedmx.gif "Change to DMX query language view")) on the toolbar to switch to the data mining query designer. For more information, see [Analysis Services DMX Query Designer User Interface](analysis-services-dmx-query-designer-user-interface.md).  
  
     Alternatively, to import an existing DMX query from another report, click **Import**, and then navigate to the .rdl file with the DMX query. Importing a query from an .dmx file is not supported.  
  
5.  After you create and run your query to see sample results, click **OK**.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
     The dataset and its field collection appear in the Report Data pane under the data source node.  
  
## See Also  
 [Analysis Services Connection Type for DMX &#40;SSRS&#41;](analysis-services-connection-type-for-dmx-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](dataset-fields-collection-report-builder-and-ssrs.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
