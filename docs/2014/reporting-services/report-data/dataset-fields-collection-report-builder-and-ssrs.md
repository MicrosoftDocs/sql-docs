---
title: "Dataset Fields Collection (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: b3884576-1f7e-4d40-bb7d-168312333bb3
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Dataset Fields Collection (Report Builder and SSRS)
  Dataset fields represent the data from a data connection. A field can represent either numeric or non-numeric data. Examples include sales amounts, total sales, customer names, database identifiers, URLs, images, spatial data, and e-mail addresses. On the design surface, fields appear as expressions in report items such as text boxes, tables, and charts.  
  
 A report has three types of fields and displays them in the Report Data pane: dataset fields, dataset calculated fields, and built-in fields.  
  
-   **Dataset fields.** The metadata that represents the collection of fields that will be returned when the dataset query runs on the data source.  
  
-   **Dataset calculated fields.** Additional fields that you create for the dataset. Each calculated field is created by evaluating an expression that you define.  
  
-   **Built-in fields.** The metadata that represents a collection of fields provided by Report Builder that provide report information such as the report name or the time when the report was processed. For more information, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](../report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md).  
  
 Dataset field names are saved as part of the report dataset definition. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Fields"></a> Dataset Fields and Queries  
 Dataset fields are specified by the dataset query command and by any calculated fields that you define. The collection of fields that you see in your report depends on the type of dataset you have:  
  
-   **Shared dataset.** The field collection is the list of fields for the query in the shared dataset definition at the time that you directly added the shared dataset to your report, or when you added a report part that included the shared dataset. The local field collection does not change when the shared dataset definition changes on the report server. To update the local field collection, you must refresh the list for the local shared dataset.  
  
-   **Embedded dataset.** The field collection is the list of fields that is returned from running the current query against the data source.  
  
 For more information see, [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md)  
  

  
### Calculated Fields  
 You specify a calculated field manually by creating an expression. Calculated fields can be used to create new values that do not exist on the data source. For example, a calculated field can represent a new value, a custom sort order for a set of field values, or an existing field that is converted to a different data type.  
  
 Calculated fields are local to a report and cannot be saved as part of a shared dataset.  
  
 For more information, see [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  

  
### Entities and Entity Fields  
 If you are working with a report model data source, you specify the entities and entity fields as your report data. In the query designer for a report model, you can interactively explore and select related entities and choose the fields that you want to include in your report dataset. After you finish designing the query, you can see the collection of entity identifiers and entity fields in the Report Data pane. Entity identifiers are generated automatically by the report model and are typically not displayed for the end user.  
  
### Using Extended Field Properties  
 Data sources that support multidimensional queries, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], support field properties on fields. Field properties appear in the result set for a query, but are not visible in the **Report Data** pane. They are still available to use in your report. To refer to a property for a field, drag the field onto the report, and change the default property `Value` to the field name of the property you want. For example, in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube, you can define formats for values in the cube cells. The formatted value is available by using the field property `FormattedValue`. To use the value directly instead of using a value and setting the format property of the text box, drag the field to the text box and change the default expression `=Fields!FieldName.Value` to `=Fields!FieldName.FormattedValue`.  
  
> [!NOTE]  
>  Not all `Field` properties can be used for all data sources. The `Value` and `IsMissing` properties are defined for all data sources. Other predefined properties (such as `Key`, `UniqueName`, and `ParentUniqueName` for multidimensional data sources) are supported only if the data source provides those properties. Custom properties are supported by some data providers. For more information, see specific topics about extended field properties for your data source type in [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md). For example, for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source, see [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](extended-field-properties-for-an-analysis-services-database-ssrs.md).  
  

  
##  <a name="Defaults"></a> Understanding Default Expressions for Fields  
 A text box can be a text box report item in the report body, or a text box in a cell in a tablix data region. When you link a field with a text box, the location of the text box determines the default expression for the field reference. In the report body, a text box value expression must specify an aggregate and a dataset. If only one dataset exists in the report, this default expression is created for you. For a field that represents a numeric value, the default aggregate function is Sum. For a field that represents a non-numeric value, the default aggregate is First.  
  
 In a tablix data region, the default field expression depends on the row and group memberships of the text box that you add the field to. The field expression for the field Sales, when added to a text box in the detail row of a table, is `[Sales]`. If you add the same field to a text box in a group header, the default expression is `(Sum[Sales])`, because the group header displays summary values for the group, not detail values. When the report runs, the report processor evaluates each expression and substitutes the result in the report.  
  
 For more information about expressions, see [Expressions &#40;Report Builder and SSRS&#41;](../report-design/expressions-report-builder-and-ssrs.md).  
  

  
##  <a name="DataTypes"></a> Field Data Types  
 When you create a dataset, the data types of the fields on the data source may not be exactly the data types used in a report. Data types may go through one or two mapping layers. The data processing extension or data provider may map data types from the data source to common language runtime (CLR) data types. The data types returned by data processing extensions are mapped to a subset of common language runtime (CLR) data types from the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)].  
  
 On the data source, the data is stored in data types supported by the data source. For example, data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database must be one of the supported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types such as `nvarchar` or `datetime`. When you retrieve data from the data source, the data passes through a data processing extension or data provider that is associated with the data source type. Depending on the data processing extension, data may be converted from the data types used by data source into data types supported by the data processing extension. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses data types supported by the common language runtime (CLR) that is installed with [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The data provider maps each column in the result set from the native data type to a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) data type.  
  
 At each stage, the data is represented by the data types as described in the following list:  
  
-   **Data source** The data types supported by the version of the type of data source to which you are connecting.  
  
     For example, typical data types for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source include `int`, `datetime`, and `varchar`. Data types introduced by [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] added support for `date`, `time`, `datetimetz`, and `datetime2`. For more information, see [Data Types (Transact-SQL)](https://go.microsoft.com/fwlink/?linkid=98362).  
  
-   **Data provider or data processing extension** The data types supported by the version of the data provider of the data processing extension you select when you connect to the data source. Data providers based on the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] use data types supported by the CLR. For more information about [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider data types, see [Data Type Mappings (ADO.NET)](https://go.microsoft.com/fwlink/?LinkId=112178) and [Working with Base Types](https://go.microsoft.com/fwlink/?LinkId=112177) on MSDN.  
  
     For example, typical data types supported by the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] include `Int32` and `String`. Calendar dates and times are supported by the `DateTime` structure. The [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 2.0 Service Pack 1 introduced support for the `DateTimeOffset` structure for dates with a time zone offset.  
  
    > [!NOTE]  
    >  The report server uses the data providers that are installed and configured on the report server. Report authoring clients in Preview mode use the installed and configured data processing extensions on the client machine. You must test your report in both the report client and the report server environment.  
  
-   **Report processor** The data types are based on the version of the CLR installed when you installed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
     For example, the data types the report processor uses for the new date and time types introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] are shown in the following table:  
  
    |SQL Data Type|CLR Data Type|Description|  
    |-------------------|-------------------|-----------------|  
    |`Date`|`DateTime`|Date only|  
    |`Time`|`TimeSpan`|Time only|  
    |`DateTimeTZ`|`DateTimeOffset`|Date and time with time zone offset|  
    |`DateTime2`|`DateTime`|Date and time with fractional milliseconds|  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database types, see [Data Types (Database Engine)](https://go.microsoft.com/fwlink/?linkid=98362) and [Date and Time Data Types and Functions (Transact-SQL)](https://go.microsoft.com/fwlink/?linkid=98360).  
  
 For more information about including references to a dataset field from an expression, see [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../report-design/data-types-in-expressions-report-builder-and-ssrs.md).  
  

  
##  <a name="MissingFields"></a> Detecting Missing Fields at Run Time  
 When the report is processed, the result set for a dataset may not contain values for all of the columns specified because the columns no longer exist on the data source. You can use the field property IsMissing to detect whether values for a field were returned at run-time. For more information, see [Dataset Fields Collection References &#40;Report Builder and SSRS&#41;](../report-design/built-in-collections-dataset-fields-collection-references-report-builder.md).  
  

  
## See Also  
 [Dataset Properties Dialog Box, Fields &#40;Report Builder&#41;](../dataset-properties-dialog-box-fields-report-builder.md)   
 [Report Parts and Datasets in Report Builder](report-parts-and-datasets-in-report-builder.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
