---
title: "Add and Verify a Data Connection or Data Source (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 1d3b2573-e29d-480d-9dde-d26379c86618
author: markingmyname
ms.author: maghan
manager: craigg
---
# Add and Verify a Data Connection or Data Source (Report Builder and SSRS)
  In Report Builder, you can add a shared data source from the report server or create an embedded data source for your report. In Report Designer, you can create a shared data source or an embedded data source and deploy it to a report server.  
  
 To add a shared data source to your report, browse to a report server and select a shared data source. The shared data source in your report points to the shared data source definition on the report server.  
  
 To create an embedded data source, you must have connection information to the external source of data and you must know which permissions you need to access the data. This information usually comes from the owner of the data source. You can test the connection to verify that the credentials that are specified are sufficient.  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md) and [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To create a reference to a shared data source  
  
1.  On the toolbar in the Report Data pane, click **New,** and then click **Data Source**. The **Data Source Properties** dialog box opens.  
  
2.  In the **Name** text box, type a name for the data source.  
  
    > [!NOTE]  
    >  This name is saved in the local report definition. This name is not the name of the shared data source on the report server.  
  
3.  Select **Use a shared connection or report model**. The list of recently used shared data sources and report models appears. To select one from a report server, click **Browse** and browse to the folder on the report server where shared data sources are available.  
  
4.  Select the shared data source and then click **Open**.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 The data source appears in the Report Data pane.  
  
### To create an embedded data source  
  
1.  On the toolbar in the Report Data pane, click **New**, and then click **Data Source**. The **Data Source Properties** dialog box opens.  
  
2.  In the **Name** text box, type a name for the data source or accept the default.  
  
3.  Verify that **Use a connection embedded in my report** is selected.  
  
    1.  From the **Select connection type** drop-down list, select a data source type; for example, **Microsoft SQL Server** or **OLE DB**.  
  
    2.  Specify a connection string by using one of the following alternatives:  
  
    -   Type the connection string directly in the **Connection string** text box. For a list of example connection strings, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
    -   Click the expression (**fx)** button to create an expression that evaluates to a connection string. In the **Expression** dialog box, type the expression in the Expression pane. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
    -   Click **Build** to open the **Connection Properties** dialog box for the data source type that you chose in step 2.  
  
         Fill in the fields in the **Connection Properties** dialog box as appropriate for the data source type. Connection properties include the type of data source, the name of the data source, and the credentials to use. After you specify values in this dialog box, click **Test Connection** to verify that the data source is available and that the credentials you specified are correct.  
  
4.  Click **Credentials**.  
  
     Specify the credentials to use for this data source. The owner of the data source chooses the type of credentials that are supported. In some cases, the owner of the data source maintains a shared data source on a report server and configures the data source with credentials that you can use. Contact the data source owner for this information. For more information, see [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 The data source appears in the Report Data pane.  
  
### To verify a data connection  
  
1.  On the toolbar in the Report Data pane, double-click the data source. The **Data Source Properties** dialog box opens.  
  
2.  Click **Test Connection**.  
  
3.  If the connection is successful, the following message appears: "Connection created successfully". [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
4.  If the connection is not successful, the following message appears: "Unable to connect to the data source."  
  
5.  Click **Details**, and use the information to correct the issue.  
  
     For more information, see [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md)  
  
  
