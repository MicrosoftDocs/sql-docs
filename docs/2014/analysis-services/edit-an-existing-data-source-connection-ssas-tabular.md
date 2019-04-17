---
title: "Edit an Existing Data Source Connection (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.selexistconn.f1"
ms.assetid: 97e63f18-a01d-4c91-a411-e7e6d40a0647
author: minewiskan
ms.author: owend
manager: craigg
---
# Edit an Existing Data Source Connection (SSAS Tabular)
  This topic describes how to edit the properties of an existing data source connection in a tabular model.  
  
 After you have created a connection to an external data source, you can later modify that connection in these ways:  
  
-   You can change the connection information, including the file, feed, or database used as a source, its properties, or other provider-specific connection options.  
  
-   You can change table and column mappings, and remove references to columns that are no longer used.  
  
-   You can change the tables, views, or columns that you get from the external data source.  
  
## Modify a Connection  
 This procedure describes how to modify a database data source connection. Some options for working with data sources differ depending on the data source type; however, you should be able to easily identify those differences.  
  
#### To change the external data source used by a current connection  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click the **Model** menu, and then click **Existing Connections**.  
  
2.  Select the data source connection you want to modify, and then click **Edit**.  
  
3.  In the **Edit Connection** dialog box, click **Browse** to locate another database of the same type but with a different name or location.  
  
     As soon as you change the database file, a message appears indicating that you need to save and refresh the tables in order to see the new data.  
  
4.  Click **Save**, and then click **Close**.  
  
5.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click the **Model**, click **Process**, and then click **Process All**.  
  
     The tables are re-processed using the new data source, but with the original data selections.  
  
    > [!NOTE]  
    >  If the new data source contains any additional tables that were not present in the original data source, you must re-open the changed connection and add the tables.  
  
## Edit Table and Column Mappings (Bindings)  
 This procedure describes how to edit the mappings after you change a data source.  
  
#### To edit column mappings when a data source changes  
  
1.  In the model designer, select a table.  
  
2.  Click on the **Table** menu, and then click **Table Properties**.  
  
     The name of the selected table is displayed in the **Table Name** box. The **Source Name** box contains the name of the table in the external data source. If columns are named differently in the source and in the model, you can toggle between the two sets of column names by selecting the options **Source** or **Model**.  
  
3.  To change the table that is used as a data source, for **Source Name**, select a different table than the current one.  
  
4.  Change column mappings if needed:  
  
    1.  To add columns that are present in the source but not in the model, select the checkbox beside the column name.  
  
         The actual data will be loaded into the model the next time you refresh.  
  
    2.  If some columns in the model are no longer available in the current data source, a message appears in the notification area that lists the invalid columns. You do not need to do anything else.  
  
5.  Click **Save** to apply the changes to your model.  
  
     When you save the current set of table properties, a message may appear indicating that you need to process the tables. Click **Process** to load updated data into your model.  
  
## See Also  
 [Process Data &#40;SSAS Tabular&#41;](process-data-ssas-tabular.md)   
 [Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md)  
  
  
