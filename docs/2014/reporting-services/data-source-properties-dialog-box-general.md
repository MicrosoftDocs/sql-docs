---
title: "Data Source Properties Dialog Box, General | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.datasourceproperties.general.f1"
  - "10120"
ms.assetid: 44b5edd3-5c11-4d3d-91b8-5871aa0572ed
author: markingmyname
ms.author: maghan
manager: kfile
---
# Data Source Properties Dialog Box, General
  Select **General** on the **Data Source Properties** dialog box to display and modify connection information for a data source in the report.  
  
## Options  
 **Name**  
 Type the name of the data source. The data source name must be unique within the report. By default, a general name, such as DataSource1 or DataSource2, is assigned to the data source.  
  
 **Embedded connection**  
 Select this option when you do not want to use a shared data source.  
  
 **Type**  
 Select a data processing extension. The list displays all registered extensions.  
  
 **Connection string**  
 Enter a connection string for the data source. Click **Edit** to build the connection string using the **Connection Properties** dialog box. Click the **Expressions** (*fx*) button to edit the expression.  
  
 **Use shared data source reference**  
 Select this option to link to a shared data source. Select a shared data source from the drop-down list. To edit the selected data source, click **Edit**. If **Use shared data source reference** is selected, **Type** and **Connection string** are disabled.  
  
 **Use a single transaction when processing the queries**  
 Select this option to indicate that datasets that use this data source are run in a single transaction against the database. To include transactions for subreports that use the same data source, set **MergeTransactions** to **True** in the subreport's **Other** properties section of the **Properties** pane.  
  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md)   
 [Create an Embedded or Shared Data Source &#40;SSRS&#41;](../../2014/reporting-services/create-an-embedded-or-shared-data-source-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Data Source Properties Dialog Box, Credentials](../../2014/reporting-services/data-source-properties-dialog-box-credentials.md)  
  
  
