---
title: "Slowly Changing Dimension Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.slowlychangingdimtrans.f1"
helpviewer_keywords: 
  - "Slowly Changing Dimension transformation"
  - "slowly changing dimensions"
  - "SCD transformation"
  - "updating slowly changing dimensions"
ms.assetid: f8849151-c171-4725-bd25-f2c33a40f4fe
author: janinezhang
ms.author: janinez
manager: craigg
---
# Slowly Changing Dimension Transformation
  The Slowly Changing Dimension transformation coordinates the updating and inserting of records in data warehouse dimension tables. For example, you can use this transformation to configure the transformation outputs that insert and update records in the DimProduct table of the [!INCLUDE[ssSampleDBDWobject](../../../includes/sssampledbdwobject-md.md)] database with data from the Production.Products table in the AdventureWorks OLTP database.  
  
> [!IMPORTANT]  
>  The Slowly Changing Dimension Wizard only supports connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 The Slowly Changing Dimension transformation provides the following functionality for managing slowly changing dimensions:  
  
-   Matching incoming rows with rows in the lookup table to identify new and existing rows.  
  
-   Identifying incoming rows that contain changes when changes are not permitted.  
  
-   Identifying inferred member records that require updating.  
  
-   Identifying incoming rows that contain historical changes that require insertion of new records and the updating of expired records.  
  
-   Detecting incoming rows that contain changes that require the updating of existing records, including expired ones.  
  
 The Slowly Changing Dimension transformation supports four types of changes: changing attribute, historical attribute, fixed attribute, and inferred member.  
  
-   Changing attribute changes overwrite existing records. This kind of change is equivalent to a Type 1 change. The Slowly Changing Dimension transformation directs these rows to an output named **Changing Attributes Updates Output**.  
  
-   Historical attribute changes create new records instead of updating existing ones. The only change that is permitted in an existing record is an update to a column that indicates whether the record is current or expired. This kind of change is equivalent to a Type 2 change. The Slowly Changing Dimension transformation directs these rows to two outputs: **Historical Attribute Inserts Output** and **New Output**.  
  
-   Fixed attribute changes indicate the column value must not change. The Slowly Changing Dimension transformation detects changes and can direct the rows with changes to an output named **Fixed Attribute Output**.  
  
-   Inferred member indicates that the row is an inferred member record in the dimension table. An inferred member exists when a fact table references a dimension member that is not yet loaded. A minimal inferred-member record is created in anticipation of relevant dimension data, which is provided in a subsequent loading of the dimension data. The Slowly Changing Dimension transformation directs these rows to an output named **Inferred Member Updates**. When data for the inferred member is loaded, you can update the existing record rather than create a new one.  
  
> [!NOTE]  
>  The Slowly Changing Dimension transformation does not support Type 3 changes, which require changes to the dimension table. By identifying columns with the fixed attribute update type, you can capture the data values that are candidates for Type 3 changes.  
  
 At run time, the Slowly Changing Dimension transformation first tries to match the incoming row to a record in the lookup table. If no match is found, the incoming row is a new record; therefore, the Slowly Changing Dimension transformation performs no additional work, and directs the row to **New Output**.  
  
 If a match is found, the Slowly Changing Dimension transformation detects whether the row contains changes. If the row contains changes, the Slowly Changing Dimension transformation identifies the update type for each column and directs the row to the **Changing Attributes Updates Output**, **Fixed Attribute Output**, **Historical Attributes Inserts Output**, or **Inferred Member Updates Output**. If the row is unchanged, the Slowly Changing Dimension transformation directs the row to the **Unchanged Output**.  
  
## Slowly Changing Dimension Transformation Outputs  
 The Slowly Changing Dimension transformation has one input and up to six outputs. An output directs a row to the subset of the data flow that corresponds to the update and the insert requirements of the row. This transformation does not support an error output.  
  
 The following table describes the transformation outputs and the requirements of their subsequent data flows. The requirements describe the data flow that the Slowly Changing Dimension Wizard creates.  
  
|Output|Description|Data flow requirements|  
|------------|-----------------|----------------------------|  
|**Changing Attributes Updates Output**|The record in the lookup table is updated. This output is used for changing attribute rows.|An OLE DB Command transformation updates the record using an UPDATE statement.|  
|**Fixed Attribute Output**|The values in rows that must not change do not match values in the lookup table. This output is used for fixed attribute rows.|No default data flow is created. If the transformation is configured to continue after it encounters changes to fixed attribute columns, you should create a data flow that captures these rows.|  
|**Historical Attributes Inserts Output**|The lookup table contains at least one matching row. The row marked as "current" must now be marked as "expired". This output is used for historical attribute rows.|Derived Column transformations create columns for the expired row and the current row indicators. An OLE DB Command transformation updates the record that must now be marked as "expired". The row with the new column values is directed to the New Output, where the row is inserted and marked as "current".|  
|**Inferred Member Updates Output**|Rows for inferred dimension members are inserted. This output is used for inferred member rows.|An OLE DB Command transformation updates the record using an SQL UPDATE statement.|  
|**New Output**|The lookup table contains no matching rows. The row is added to the dimension table. This output is used for new rows and changes to historical attributes rows.|A Derived Column transformation sets the current row indicator, and an OLE DB destination inserts the row.|  
|**Unchanged Output**|The values in the lookup table match the row values. This output is used for unchanged rows.|No default data flow is created because the Slowly Changing Dimension transformation performs no work. If you want to capture these rows, you should create a data flow for this output.|  
  
## Business Keys  
 The Slowly Changing Dimension transformation requires at least one business key column.  
  
 The Slowly Changing Dimension transformation does not support null business keys. If the data include rows in which the business key column is null, those rows should be removed from the data flow. You can use the Conditional Split transformation to filter rows whose business key columns contain null values. For more information, see [Conditional Split Transformation](conditional-split-transformation.md).  
  
## Optimizing the Performance of the Slowly Changing Dimension Transformation  
 For suggestions on how to improve the performance of the Slowly Changing Dimension Transformation, see [Data Flow Performance Features](../data-flow-performance-features.md).  
  
## Troubleshooting the Slowly Changing Dimension Transformation  
 You can log the calls that the Slowly Changing Dimension transformation makes to external data providers. You can use this logging capability to troubleshoot the connections, commands, and queries to external data sources that the Slowly Changing Dimension transformation performs. To log the calls that the Slowly Changing Dimension transformation makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the Slowly Changing Dimension Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md).  
  
## Configuring the Slowly Changing Dimension Transformation Outputs  
 Coordinating the update and insertion of records in dimension tables can be a complex task, especially if both Type 1 and Type 2 changes are used. [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer provides two ways to configure support for slowly changing dimensions:  
  
-   The **Advanced Editor** dialog box, in which you to select a connection, set common and custom component properties, choose input columns, and set column properties on the six outputs. To complete the task of configuring support for a slowly changing dimension, you must manually create the data flow for the outputs that the Slowly Changing Dimension transformation uses. For more information, see [Data Flow](../data-flow.md).  
  
-   The Load Dimension Wizard, which guides you though the steps to configure the Slowly Changing Dimension transformation and build the data flow for transformation outputs. To change the configuration for slowly change dimensions, rerun the Load Dimension Wizard. For more information, see [Configure Outputs Using the Slowly Changing Dimension Wizard](configure-outputs-using-the-slowly-changing-dimension-wizard.md).  
  
## Related Tasks  
 [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
## Related Content  
  
-   Blog entry, [Optimizing the Slowly Changing Dimension Wizard](https://go.microsoft.com/fwlink/?LinkId=199481), on blogs.msdn.com.  
  
  
