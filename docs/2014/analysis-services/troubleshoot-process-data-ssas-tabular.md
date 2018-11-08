---
title: "Troubleshoot Process Data (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 678f523c-e181-4456-9a54-7b7bf044b8d2
author: minewiskan
ms.author: owend
manager: craigg
---
# Troubleshoot Process Data (SSAS Tabular)
  This topic provides information about processing (refresh) model data when authoring a model by using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. This topic does not provide information about processing data in models that has been deployed to an Analysis Services server instance. For more information about processing data in a deployed model, see [Script Administrative Tasks in Analysis Services](script-administrative-tasks-in-analysis-services.md).  
  
 Sections in this topic:  
  
-   [How Data Processing Works](#bkmk_how_df_works)  
  
-   [Impact of Data Processing](#bkmk_impact_of_df)  
  
-   [Determining the Source of Data](#bkmk_det_source)  
  
-   [Determining When Data was Last Refreshed](#bkmk_det_last_ref)  
  
-   [Restrictions on Refreshable Data Sources](#bkmk_restrictions)  
  
-   [Restrictions on Changes to a Data Source](#bkmk_rest_changes)  
  
##  <a name="bkmk_how_df_works"></a> How Data Processing Works  
 When you process data, the data in the model designer is replaced with new data. You cannot import just new rows of data or just changed data. The model designer does not track which rows were added previously.  
  
 Processing of data takes place as a transaction. This means that once you begin updating data, the entire update must either fail or succeed; you will never have data that is partly correct.  
  
 Manual data process, which you initiate from [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], is handled by the local in-memory instance of Analysis Services. Therefore, the data process operation can affect the performance of other tasks on your computer. However, if you schedule automatic process of data in a deployed model by using a script, the instance of Analysis Services manages the import process and its timing.  
  
##  <a name="bkmk_impact_of_df"></a> Impact of Data Processing  
 A process of data usually triggers recalculation of data.  Processing data means getting the latest data from the external sources;  recalculating means updating the result of all formulas that use data that has changed. A process operation usually triggers recalculation.  
  
 Therefore you should always be mindful of the potential impact before you change data sources or process the data that is obtained from the data source, and consider these potential consequences:  
  
-   Some parts of the model data may be broken as a result of changes in the data source. If not all of the columns can be retrieved from the data source (for example, if they have been deleted or changed), process will fail, and you must update the mappings between the source data and the model data. For more information, see [Edit an Existing Data Source Connection &#40;SSAS Tabular&#41;](edit-an-existing-data-source-connection-ssas-tabular.md).  
  
-   After processing, some columns might be flagged as containing an error. This can happen because the DAX formula in the column uses data that became unavailable when you processed, the data type of a column changed, or an invalid value was added to the external data. To resolve the issue, you can edit the formula, or you can delete the column if it is based on data that is no longer available.  
  
-   Formulas that use the updated data will need to be recalculated. Depending on the size of the model, this can take some time.  
  
-   If your model contains multiple data sources, you might need to process the entire model (Process All) even if only one external data source has changed. For example, if you create measures that rely on calculated columns, and those calculated columns use values from other calculated columns, the model designer first analyzes the dependencies and then processes the entire chain of related objects in order. Depending on the complexity of the dependencies, this can take a long time.  
  
-   When you change a filter, the entire model must be recalculated.  
  
##  <a name="bkmk_det_source"></a> Determining the Source of Data  
 If you are not sure where the data in your model came from, you can use the tools in the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] to get the details, including the source file name and path.  
  
#### To find the source of existing data  
  
1.  In the model designer, select the table that contains the data for which you want to know the source.  
  
2.  Click on the **Table** menu, and the click **Table Properties**.  
  
3.  In the **Edit Table Properties** dialog box, make note of the value listed for **Connection Name**.  
  
4.  In [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Existing Connections**.  
  
5.  In the **Existing Connections** dialog box, select the data source with the name you found in step 3, and then click **Edit**.  
  
6.  In the **Edit Connections** dialog box, view the connection information, such as the database name, file path, or report path.  
  
##  <a name="bkmk_det_last_ref"></a> Determining When Data was Last Refreshed  
 You can use the Table Properties to determine when the data was last refreshed.  
  
#### To find the date and time that a table was last processed  
  
1.  In the model designer, select the table that contains the data for which you want to know the refresh date.  
  
2.  Click on the **Table** menu, and then click **Table Properties**.  
  
3.  In the **Edit Table Properties** dialog box, **Last Refreshed** shows the last date that the table was refreshed.  
  
##  <a name="bkmk_restrictions"></a> Restrictions on Refreshable Data Sources  
 Some restrictions apply to the data sources that can be automatically processed from a deployed model on an Analysis Services instance. Be sure to select only those data sources that meet the following criteria:  
  
-   The data source must be available at the time that data process occurs and available at the stated location. If the original data source is on a local disk drive of the user who authored the model, you must either exclude that data source from the data process operation, or find a way to publish that data source to a location that is accessible through a network connection. If you move a data source to a network location, be sure to open the model in the model designer and repeat the data retrieval steps. This is necessary to re-establish the connection information that is stored in the data source connection properties.  
  
-   The data source must be accessed using the credentials that are embedded in the data source connection. Embedded credentials are created in the data source connection when you connect to the external data source.  
  
-   Data process must succeed for all of the data sources that you specify. Otherwise, the processed data is discarded, leaving you with the last saved version of the model. Exclude any data sources that you are not sure about.  
  
-   Data process must not invalidate other data in your model. When you process a subset of your data, it is important that you understand whether the model is still valid once newer data is aggregated with static data that is not from the same time period. As a model designer, it is up to you to know your data dependencies and ensure that data process is appropriate for the model itself.  
  
     An external data source is accessed through an embedded connection string, URL, or UNC path that you specified when you imported the original data into the model using the Table Import Wizard. Original connection information that is stored in the data source connection is reused for subsequent data refresh operations. There is no separate connection information that is created and managed for data process purposes; only existing connection information is used.  
  
##  <a name="bkmk_rest_changes"></a> Restrictions on Changes to a Data Source  
 There are some restrictions on the changes that you can make to a data source:  
  
-   The data types of a column can only be changed to a compatible data type. For example, if the data in the column includes decimal numbers, you cannot change the data type to an integer. However, you can change numeric data to text. For more information about data types, see [Data Types Supported &#40;SSAS Tabular&#41;](tabular-models/data-types-supported-ssas-tabular.md).  
  
-   You cannot multi-select columns in different tables and change properties of the columns. You can work with only one table or view at a time.  
  
## See Also  
 [Manually Process Data &#40;SSAS Tabular&#41;](manually-process-data-ssas-tabular.md)   
 [Edit an Existing Data Source Connection &#40;SSAS Tabular&#41;](edit-an-existing-data-source-connection-ssas-tabular.md)  
  
  
