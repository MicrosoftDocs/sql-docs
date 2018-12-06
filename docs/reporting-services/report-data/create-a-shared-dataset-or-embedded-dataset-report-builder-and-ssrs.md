---
title: "Create a Shared Dataset or Embedded Dataset (Report Builder and SSRS) | Microsoft Docs"
ms.date: 10/17/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: d1d7bc71-f0e9-4ce5-b3ad-6fee54388a31
author: markingmyname
ms.author: maghan
---
# Create a Shared Dataset or Embedded Dataset (Report Builder and SSRS)
Embedded datasets are for use in a single [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report. Shared datasets on a report server can be used by multiple reports, both mobile and paginated. To create a dataset, you need an embedded or shared data source.  
  
## Report Builder tasks

Use Report Builder for the following tasks:  
  
1.  Create a shared dataset in Dataset Design View. Shared datasets must use published shared data sources.  
  
2.   Create an embedded dataset in Report Design View.  
  
3.   Save the dataset directly to the report server or SharePoint site.  
  
## Report Designer tasks

Use Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] for the following tasks:  
  
1.  Create a shared dataset in Solution Explorer. Shared datasets must use data sources from the Shared Data Sources folder in Solution Explorer.  
  
2.  Create an embedded dataset in the Report Data pane.  
  
3.  Optionally deploy the shared datasets and shared data source with the report. For each type of item, use Project Properties to specify paths to folders on the report server or SharePoint site.  
  
 For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To create a shared dataset in Report Builder
  
1.  Open Report Builder. The **New report or dataset pane** opens, as shown in the following figure:  
  
     ![rs_NewSharedDataset](../../reporting-services/report-data/media/rs-newshareddataset.gif "rs_NewSharedDataset")  
  
    > [!NOTE]  
    >  If the **New report or dataset pane** does not appear, from the Report Builder button, click **New**.  
  
2.  In the left pane, under **Create a dataset**, click **Shared Dataset**.  
  
3.  In the right pane, click **Browse** to select a shared data source from the report server, and then click **Create**. The query designer associated with the shared data source opens.  
  
4.  In the query designer, specify the fields to include in the dataset.  
  
5.  Click **Run** (**!**) to run the query.  
  
6.  On the **Report Builder** button, click **Save** or **Save As** to save the shared dataset to the report server.  
  
7.  To exit Report Builder, click **Report Builder**, and then click **Exit Report Builder**. To work with reports, click **Report Builder**, and then click **New** or **Open**.  
  
## To set query parameter options  
  
1.  Open Report Builder.  
  
2.  Click **Open**.  
  
3.  Browse to the report server, and select the folder for the shared data source.  
  
4.  In **Items of type**, click Datasets (*.rsd) in the drop-down list.  
  
5.  Select the shared dataset, and then click **Open**. The associated query designer opens.  
  
6.  On the Ribbon, click **Dataset Properties**.  
  
7.  Click **Parameters**. On this page, set a default value to a constant or an expression, mark the parameter as read-only, nullable, or **Omit From Query**. For more information, see [Report Parameters](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  

  
## To create a dataset from a SQL Server relational database  
  
1.  In the Report Data pane, right-click the name of the data source, and then click **Add Dataset**. The **Query** page of the **Dataset Properties** dialog box opens.  
  
2.  In **Name**, type a name for the dataset or accept the default name.  
  
    > [!NOTE]  
    >  The dataset name is used internally within the report. For clarity, we recommend that the name of the dataset describe the data that the query returns.  
  
3.  In **Data source**, browse to and select the name of an existing shared data source, or click **New** to create a new embedded data source.  
  
4.  Select a **Query type** option. Options depend on the data source type.  
  
    -   Select **Text** to write a query using the query language of the data source.  
  
    -   Select **Table** to return all the fields in a relational database table.  
  
    -   Select **StoredProcedure** to run a stored procedure by name.  
  
5.  In **Query**, type the query, stored procedure, or table name. Alternatively, click **Query Designer** to open the graphical or text-based query designer tool, or **Import** to import the query from an existing report.  
  
     In a few cases, the field collection specified by the query can only be determined by running the query on the data source. For example, a stored procedure may return a variable set of fields in the result set. Click **Refresh Fields** to run the query on the data source and retrieve the field names that are needed to populate the dataset field collection in the Report Data pane. The field collection appears under the dataset node after you close the **Dataset Properties** dialog box.  
  
6.  In **Timeout**, type the number of seconds that the report server waits for a response from the database. The default value is 0 seconds. When the time out value is 0 seconds, the query does not time out.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     The dataset and its field collection appear in the Report Data pane under the data source node.  
  
## See Also  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)   
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)   
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/7e103637-4371-43d7-821c-d269c2cc1b34)   
 [Embedded and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/embedded-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
