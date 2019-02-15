---
title: "Create Mining Structure (SQL Server Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures, creating"
ms.assetid: b8b1eedc-4d6d-4429-a578-e629ec573934
author: minewiskan
ms.author: owend
manager: craigg
---
# Create Mining Structure (SQL Server Data Mining Add-ins)
  ![Create Mining Structure button, Data Mining ribbon](media/dmc-createstruct.gif "Create Mining Structure button, Data Mining ribbon")  
  
 Use the **Advanced** option in the **Data Modeling** group when you want to create a data set used for analysis without necessarily creating a model. This is useful when you want to experiment with different algorithms.  
  
 After you have created the mining structure, use the [Add Model to Structure](add-model-to-structure-data-mining-add-ins-for-excel.md) wizard to create a model based on that structure. You can also create new models by using the **Data Mining Advanced Query Editor**.  
  
 You can also use this option when you intend to build models using one of the advanced algorithms, which are supported by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] but not available through a wizard, such as linear regression or sequence clustering, or if you are using a custom algorithm.  
  
> [!NOTE]  
>  When you create the mining structure, you can also establish a randomly selected testing data set that you can use to validate all your models. This is handy because you can easily compare model accuracy against a common data set. Just select the option, **Split data into training and testing sets** and specify an appropriate percentage of data to reserve for testing, usually around 30 percent.  
  
## Use the wizard to create a mining structure  
  
1.  In the **Data Mining** ribbon, click **Advanced**, and select **Create Structure**.  
  
2.  In the **Select source data** dialog box, specify the Excel range, Excel data table, or external data source that contains the data you want to use for analysis.  
  
     Click **Next**.  
  
3.  In the **Select Columns** dialog box, review the list of columns available in the selected data source.  
  
4.  Click the arrow to the right of the column name to change the **usage** of the column, choosing from these values:  
  
    -   **Key**. At least one key is required for each model.  
  
    -   **Key time**. This option is available only for forecasting models, where it is required.  
  
    -   **Include**. Indicates that the column should be made available in the mining structure but is not a key column.  
  
    -   **Do not use**. Indicates that the column should not be included in the mining structure.  
  
     Remember that you can always ignore columns when you build the model, but to add columns later requires that you reprocess the structure and model.  
  
5.  Click the browse **(...)** button to set the content type, data type, and modeling flags.  
  
    > [!NOTE]  
    >  If the column contains numeric data, you should always open this dialog box to ensure that the correct data type is chosen. In some cases, even if the input data is a number, you will want to treat it as a categorical variable, or discrete value, instead of a continuous number.  
    >   
    >  For example, a postal code column might be listed by default as a continuous long data type, but to get better results, you can specify that it be handled as a discrete text value.  
    >   
    >  For more information, see the section on content types in [Choosing Data for Data Mining](choosing-data-for-data-mining.md).  
  
     Click **OK** to close the dialog box.  
  
6.  Click **Next**.  
  
     Depending on what type of data you are using, you might complete the wizard after this step. In that case, jump ahead to the **Finish** page to name your mining structure.  
  
     For other models, you have the additional option to create a testing data set.  
  
7.  In the **Split data into training and testing data sets** dialog box, specify how you want your data partitioned. By default, 30 percent of data is used for testing.  
  
     Optionally, type the maximum number of rows to use for testing.  
  
     Click **Next**.  
  
8.  In the **Finish** dialog, type a name and description for the new mining structure.  
  
9. Click **Finish**.  
  
### Related Options  
  
|Option|Comments|  
|------------|--------------|  
|**Select Source Data** dialog box|When you select an Excel table, you should indicate whether the data already has headers. If you skip this, the first row of data will be used as the column name.<br /><br /> If you use the option, **External data source**, you can use any kind of data that can be defined in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data source. However, the dialog box in the add-in for creating new data sources does not include the full range of data sources supported by Analysis Services, so we recommend that you create the data sources on the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server in advance and then connect using the add-ins.|  
|**Data Source Query Editor** dialog box|After you have connected to the specified data source, you can add columns, or create a custom query to generate custom columns.|  
|**Split data into training and testing data sets**|A recommended value for training vs. testing sets is 70 percent for training and 30 percent for testing; however, if you have a lot of data, you can specify a maximum number of rows for testing.|  
|Finish dialog box|The options for drillthrough are available on some model types and are very useful if you included detail columns in your mining structure. For example, if you create a clustering model, you can include details such as name or email address for drillthrough but not analysis, to make it easier to contact customers in a particular cluster.|  
  
###  <a name="Bkmk_strctcolumn"></a> Setting Column Usage in the Create Mining Structure Wizard  
 When you create a new mining structure, you can specify which columns in the data source should be included in the mining structure, and how those columns should be used. Remember that a mining structure can support multiple mining models.  
  
|Values|Description|  
|------------|-----------------|  
|**Include**|Specifies that the column contains data that can be used for analysis or prediction.|  
|**Key**|Specifies that the column contains a transaction ID, a series ID, or another key required for processing.<br /><br /> All algorithms require a Key column. However, some algorithms permit only a single key, while others permit multiple keys.<br /><br /> If the column contains a key but is not required for processing, select **Do Not Use**.|  
|**Key Time**|Specifies that the column contains a date or other numeric value that can be used to uniquely identify items in a time series.|  
|**Do Not Use**|Specifies that the column should be ignored. The data in the column will not be processed.|  
  
 To process a model correctly, the algorithm must know which column is the key column that uniquely identifies each row, which column is the target column for creating predictions if you are creating a predictable model, and which columns to use as input columns to create the relationships that predict the target column.  
  
-   Columns that are specified as **Do not use** will not be present in the mining structure.  
  
     If you add columns that are unnecessary or have bad values, it can adversely affect the results of analysis. Therefore, be sure to include only those columns that are relevant. However, remember that the columns that you do not use in the mining structure will not be available for querying.  
  
-   Columns that are specified as the **Include** type will be included in the mining structure and later can be used for either analysis or prediction in the mining models.  
  
     If you are not sure whether you will need to use the column, you can always include the column in the mining structure and then create a mining model that does not use that column. For example, you might include a phone number column in your data for later reference, but create a clustering model that ignores phone numbers. After the clusters have been created, you can create a query that returns the phone numbers of people who belong to a particular cluster.  
  
-   All algorithms require a **Key** column. The values in the Key column must be unique. A **Key Time** column is required only for forecasting or time series models. .  
  
### Requirements  
 To create a data mining structure, you must have a connection to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. A connection is required even if you are working with temporary structures. For more information about how to create or change a connection, see [Connect to Source Data &#40;Data Mining Client for Excel&#41;](connect-to-source-data-data-mining-client-for-excel.md).  
  
## See Also  
 [Creating a Data Mining Model](creating-a-data-mining-model.md)  
  
  
