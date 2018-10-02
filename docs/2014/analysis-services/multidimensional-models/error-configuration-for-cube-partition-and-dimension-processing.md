---
title: "Error Configuration for Cube, Partition, and Dimension Processing (SSAS - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.cubeproperties.errorconfiguration.f1"
  - "sql12.asvs.sqlserverstudio.dimensionproperties.errorconfiguration.f1"
  - "sql12.asvs.sqlserverstudio.partitionproperties.errorconfiguration.f1"
ms.assetid: 3f442645-790d-4dc8-b60a-709c98022aae
author: minewiskan
ms.author: owend
manager: craigg
---
# Error Configuration for Cube, Partition, and Dimension Processing (SSAS - Multidimensional)
  Error configuration properties on cube, partition, or dimension objects determine how the server responds when data integrity errors occur during processing. Duplicate keys, missing keys, and null values in a key column typically trigger such errors, and while the record causing the error will not be added to the database, you can set properties that determine what happens next. By default, processing stops. However, during cube development, you might want processing to continue when errors occur so that you can test cube behaviors with imported data, even if it is incomplete.  
  
 This topic includes the following sections:  
  
-   [Execution order](#bkmk_exec)  
  
-   [Default behaviors](#bkmk_default)  
  
-   [Error Configuration Properties](#bkmk_props)  
  
-   [Where to set Error Configuration properties](#bkmk_tools)  
  
-   [Missing keys (KeyNotFound)](#bkmk_missing)  
  
-   [Null foreign keys in a fact table (KeyNotFound)](#bkmk_nullfact)  
  
-   [Null keys in a dimension](#bkmk_nulldim)  
  
-   [Duplicate keys resulting inconsistent relationships (KeyDuplicate)](#bkmk_dupe)  
  
-   [Change the error limit or error limit action](#bkmk_limit)  
  
-   [Set the error log path](#bkmk_log)  
  
-   [Next step](#bkmk_next)  
  
##  <a name="bkmk_exec"></a> Execution order  
 The server always executes `NullProcessing` rules before `ErrorConfiguration` rules for each record. This is important to understand because null processing properties that convert nulls to zeroes can subsequently introduce duplicate key errors when two or more error records have zero in a key column.  
  
##  <a name="bkmk_default"></a> Default behaviors  
 By default, processing stops at the first error implicating a key column. This behavior is controlled by an error limit that specifies zero as the number of allowed errors and the Stop Processing directive that tells the server to stop processing when the error limit is reached.  
  
 Records triggering an error, due to null or missing or duplicate values, are either converted to the unknown member or discarded. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will not import data that violates data integrity constraints.  
  
-   Conversion to unknown member occurs by default, due to the `ConvertToUnknown` setting for `KeyErrorAction`. Records allocated to unknown member are quarantined in the database as evidence of a problem that you might want to investigate after processing is finished.  
  
     Unknown members are excluded from query workloads, but they will be visible in some client applications if the `UnknownMember` is set to **Visible**.  
  
     If you want to track how many nulls were converted to the unknown member, you can modify the `NullKeyConvertedToUnknown` property to report these errors to the log or in the Processing window.  
  
-   Discard occurs when you manually set the `KeyErrorAction` property to `DiscardRecord`.  
  
 Through error configuration properties, you can determine how the server responds when an error occurs. Options include stop processing immediately, continue processing but stop logging, or continue both processing and logging of errors. Defaults vary based on the severity of the error.  
  
 The error count keeps track of how many errors occur. When you set an upper limit, server response changes when that limit is reached. By default, the server stops processing after the limit is reached. The default limit is 0, causing processing to stop on the first error that is counted.  
  
 High impact errors, such as a missing key or null value in a key field, should be addressed quickly. By default, these errors adhere to `ReportAndContinue` server behaviors, where the server catches the error, adds it to the error count, and then continues processing (except the error limit is zero, so processing stops immediately).  
  
 Other errors are generated but not counted or logged by default (this is the `IgnoreError` setting) because the error does not necessarily pose a data integrity problem.  
  
 Error counts are affected by null processing settings. For dimension attributes, null processing options determine how the server responds when null values are encountered. By default, nulls in a numeric column are converted to zeroes, while nulls in a string column are processed as blank strings. You can override `NullProcessing` properties to catch null values before they turn into `KeyNotFound` or `KeyDuplicate` errors. See [Null keys in a dimension](#bkmk_nulldim) for details.  
  
 Errors are logged in the Process dialog box but not saved. You can specify a key error log file name to collect errors in a text file.  
  
##  <a name="bkmk_props"></a> Error Configuration Properties  
 There are nine error configuration properties. Five are used to determine server response when a specific error occurs. The other four are scoped to error configuration workloads, such as how many errors to allow, what to do when that limit is reached, whether to collect errors in a log file.  
  
 **Server response to specific errors**  
  
|Property|Default|Other Values|  
|--------------|-------------|------------------|  
|`CalculationError`<br /><br /> Occurs when initializing error configuration.|`IgnoreError` neither logs nor counts the error; processing continues as long as the error count is under the maximum limit.|`ReportAndContinue` logs and counts the error.<br /><br /> `ReportAndStop` reports the error and stops processing immediately, regardless of the error limit.|  
|`KeyNotFound`<br /><br /> Occurs when a foreign key in a fact table does not have a matching primary key in a related dimension table (for example, a Sales fact table has a record with a product ID that doesn't exist in the Product dimension table). This error can occur during partition processing, or dimension processing of snowflaked dimensions.|`ReportAndContinue` logs and counts the error.|`ReportAndStop` reports the error and stops processing immediately, regardless of the error limit.<br /><br /> `IgnoreError` neither logs nor counts the error; processing continues as long as the error count is under the maximum limit. Records that trigger this error are converted to the unknown member by default, but you can change the `KeyErrorAction` property to discard them instead.|  
|`KeyDuplicate`<br /><br /> Occurs when duplicate attribute keys are found in a dimension. In most cases, it is acceptable to have duplicate attribute keys, but this error informs you of the duplicates so that you can check the dimension for design flaws that might lead to inconsistent relationships between attributes.|`IgnoreError` neither logs nor counts the error; processing continues as long as the error count is under the maximum limit.|`ReportAndContinue` logs and counts the error.<br /><br /> `ReportAndStop` reports the error and stops processing immediately, regardless of the error limit.|  
|`NullKeyNotAllowed`<br /><br /> Occurs when `NullProcessing` = `Error` is set on a dimension attribute or when null values exists in an attribute key column used to uniquely identify a member.|`ReportAndContinue` logs and counts the error.|`ReportAndStop` reports the error and stops processing immediately, regardless of the error limit.<br /><br /> `IgnoreError` neither logs nor counts the error; processing continues as long as the error count is under the maximum limit. Records that trigger this error are converted to the unknown member by default, but you can set the `KeyErrorAction` property to discard them instead.|  
|`NullKeyConvertedToUnknown`<br /><br /> Occurs when null values are subsequently converted to the unknown member. Setting `NullProcessing` = `ConvertToUnknown` on a dimension attribute will trigger this error.|`IgnoreError` neither logs nor counts the error; processing continues as long as the error count is under the maximum limit.|If you consider this error to be informational, keep the default. Otherwise, you can choose `ReportAndContinue` to report the error to the Processing window and count the error towards the error limit.<br /><br /> `ReportAndStop` reports the error and stops processing immediately, regardless of the error limit.|  
  
 **General Properties**  
  
|**Property**|**Values**|  
|------------------|----------------|  
|`KeyErrorAction`|This is the action taken by the server when a `KeyNotFound` error occurs. Valid responses to this error include `ConvertToUnknown` or `DiscardRecord`.|  
|`KeyErrorLogFile`|This is a user-defined filename that must have a .log file extension, located in a folder on which the service account has read-write permissions. This log file will only contain errors generated during processing. Use the Flight Recorder if you require more detailed information.|  
|`KeyErrorLimit`|This is the maximum number of data integrity errors that the server will allow before failing the processing. A value of -1 indicates that there is no limit. The default is 0, which means processing stops after the first error occurs. You can also set it to a whole number.|  
|`KeyErrorLimitAction`|This is the action taken by the server when the number of key errors has reached the upper limit. With **Stop Processing**, processing terminates immediately. With **Stop Logging**, processing continues but errors are no longer reported or counted.|  
  
##  <a name="bkmk_tools"></a> Where to set Error Configuration properties  
 Use the property pages in either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] after the database is deployed, or in the model project in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. The same properties are found in both tools. You can also set error configuration properties in the msmdrsrv.ini file to change server defaults for error configuration, and in `Batch` and `Process` commands if processing runs as a scripted operation.  
  
 You can set error configuration on any object that can be processed as a standalone operation.  
  
#### SQL Server Management Studio  
  
1.  In Object Explorer, rick-click **Properties** one of these objects: dimension, cube, or partition.  
  
2.  In Properties, click **Error Configuration**.  
  
#### SQL Server Data Tools  
  
1.  In Solution Explorer, double-click a dimension or cube. `ErrorConfiguration` appears in Properties in the pane below.  
  
2.  Alternatively, for a single dimension only, right-click the dimension in Solution Explorer, choose `Process`, and then choose **Change Settings** in the Process Dimension dialog box. Error configuration options appear on the Dimension Key Errors tab.  
  
##  <a name="bkmk_missing"></a> Missing keys (KeyNotFound)  
 Records with a missing key value cannot be added to the database, not even when errors are ignored or the error limit is unlimited.  
  
 The server produces the `KeyNotFound` error during partition processing, when a table in fact record contains a foreign key value, but the foreign key has no corresponding record in a related dimension table. This error also occurs when processing related or snowflaked dimension tables, where a record in one dimension specifies a foreign key that doesn't exist in the related dimension.  
  
 When a `KeyNotFound` error occurs, the offending record is allocated to the unknown member. This behavior is controlled through the **Key Action**, set to `ConvertToUnknown`, so that you can view the allocated records for further investigation.  
  
##  <a name="bkmk_nullfact"></a> Null foreign keys in a fact table (KeyNotFound)  
 By default, a null value in a foreign key column of a fact table is converted to zero. Assuming zero is not a valid foreign key value, the `KeyNotFound` error will be logged and counted towards the error limit that is zero by default.  
  
 To allow processing to continue, you can handle the null before it is converted and checked for errors. To do this, set `NullProcessing` to `Error`.  
  
#### Set NullProcessing property on a measure  
  
1.  In SQL Server Data Tools, in Solution Explorer, double-click the cube to open it in Cube Designer.  
  
2.  Right-click a measure in the Measures pane and choose **Properties**.  
  
3.  In Properties, expand **Source** to view `NullProcessing` property. It is set to **Automatic** by default, which for OLAP items, converts nulls to zeroes for fields containing numeric data.  
  
4.  Change the value to `Error` to exclude any records having a null value, avoiding the null-to-numeric (zero) conversion. This modification lets you avoid duplicate key errors related to multiple records having zero in the key column, and also avoid `KeyNotFound` errors when a zero-valued foreign key has no primary key equivalent in a related dimension table.  
  
##  <a name="bkmk_nulldim"></a> Null keys in a dimension  
 To continue processing when null values are found in foreign keys in a snowflaked dimension, handle the null values first by setting `NullProcessing` on the `KeyColumn` of the dimension attribute. This discards or converts the record, before the `KeyNotFound` error can occur.  
  
 You have two options for handling nulls on dimension attribute:  
  
-   Set `NullProcessing`=`UnknownMember` to allocate records with null values to the unknown member. This produces the `NullKeyConvertedToUnknown` error, which is ignored by default.  
  
-   Set `NullProcessing`=`Error` to exclude records with null values. This produces the `NullKeyNotAllowed` error, which is logged and counts toward the key error limit. You can set error configuration property on **Null Key Not Allowed** to `IgnoreError` to allow processing to continue.  
  
 Nulls can be problem for non-key fields, in that MDX queries return different results depending on whether null is interpreted as zero or empty. For this reason, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides null processing options that let you predefine the conversion behavior you want. See [Defining the Unknown Member and Null Processing Properties](../lesson-4-7-defining-the-unknown-member-and-null-processing-properties.md) and <xref:Microsoft.AnalysisServices.NullProcessing> for details.  
  
#### Set NullProcessing property on a dimension attribute  
  
1.  In SQL Server Data Tools, in Solution Explorer, double-click the dimension to open it in Dimension Designer.  
  
2.  Right-click an attribute in the Attributes pane and choose **Properties**.  
  
3.  In Properties, expand **KeyColumns** to view `NullProcessing` property. It is set to **Automatic** by default, which converts nulls to zeroes for fields containing numeric data. Change the value to either `Error` or `UnknownMember`.  
  
     This modification removes the underlying conditions that trigger `KeyNotFound` by either discarding or converting the record before it is checked for errors.  
  
     Depending on error configuration, either of these actions can result in an error that is reported and counted. You might need to adjust additional properties, such as setting `KeyNotFound` to `ReportAndContinue` or `KeyErrorLimit` to a non-zero value, to allow processing to continue when these errors are reported and counted.  
  
##  <a name="bkmk_dupe"></a> Duplicate keys resulting inconsistent relationships (KeyDuplicate)  
 By default, the presence of a duplicate key does not stop processing, but the error is ignored and the duplicate record is excluded from the database.  
  
 To change this behavior, set `KeyDuplicate` to `ReportAndContinue` or `ReportAndStop` to report the error. You can then examine the error to determine potential flaws in dimension design.  
  
##  <a name="bkmk_limit"></a> Change the error limit or error limit action  
 You can raise the error limit to allow more errors through during processing. There is no guidance for raising the error limit; the appropriate value will vary depending on your scenario. Error limits are specified as `KeyErrorLimit` in `ErrorConfiguration` properties in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], or as **Number of Errors** in the Error Configuration tab for properties of dimensions, cubes, or measure groups in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Once the error limit is reached, you can specify that processing stops or that logging stops. For example, suppose you set the action to `StopLogging` on an error limit of 100. On the 101st error, processing continues, but errors are no longer logged or counted. Error limit actions are specified as `KeyErrorLimitAction` in `ErrorConfiguration` properties in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], or as **On error action** in the Error Configuration tab for properties of dimensions, cubes, or measure groups in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
##  <a name="bkmk_log"></a> Set the error log path  
 You can specify a file to store key-related error messages that are reported during processing. By default, errors are visible during interactive processing in the Process window and then discarded when you close the window or session. The log will only contain error information related to keys, identical to the errors you see reported in the processing dialog boxes.  
  
 Errors will be logged to a text file and must have .log file extension. The file will be empty unless errors occur. By default, a file will be created in the DATA folder. You can specify another folder as long as the Analysis Services service account can write to that location.  
  
##  <a name="bkmk_next"></a> Next step  
 Decide whether errors will stop processing or be ignored. Remember that only the error is ignored. The record that caused the error is not ignored; it is either discarded or converted to unknown member. Records that violate data integrity rules are never added to the database. By default, processing stops when the first error occurs, but you can change this by raising the error limit. In cube development, it can be useful to relax error configuration rules, allowing processing to continue, so that there is data to test with.  
  
 Decide whether to change default null processing behaviors. By default, nulls in a string column are processed as empty values, while nulls in a numeric column are processed as zero. See [Defining the Unknown Member and Null Processing Properties](../lesson-4-7-defining-the-unknown-member-and-null-processing-properties.md) for instructions on setting null processing on an attribute.  
  
## See Also  
 [Log Properties](../server-properties/log-properties.md)   
 [Defining the Unknown Member and Null Processing Properties](../lesson-4-7-defining-the-unknown-member-and-null-processing-properties.md)  
  
  
