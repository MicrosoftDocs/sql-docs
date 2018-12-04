---
title: "Raw File Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.rawfiledest.f1"
  - "sql13.dts.designer.rawfiledestinationconnectionmanager.f1"
  - "sql13.dts.designer.rawfiledestinationcolumns.f1"
helpviewer_keywords: 
  - "append options [Integration Services]"
  - "destinations [Integration Services], Raw File"
  - "raw data [Integration Services]"
  - "writing raw data"
  - "Raw File destination"
ms.assetid: d311b458-aefc-4b4d-b1a1-4c0ebbb34214
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Raw File Destination
  The Raw File destination writes raw data to a file. Because the format of the data is native to the destination, the data requires no translation and little parsing. This means that the Raw File destination can write data more quickly than other destinations such as the Flat File and the OLE DB destinations.  
  
 In addition to writing raw data to a file, you can also use the Raw File destination to generate an empty raw file that contains only the columns (metadata-only file), without having to run the package. You use the Raw File source to retrieve raw data that was previously written by the destination. You can also point the Raw File source to the metadata-only file.  
  
 The raw file format contains sort information. The Raw File Destination saves all the sort information including the comparison flags for string columns. The Raw File source reads and honors the sort information. You do have the option of configuring the Raw File Source to ignore the sort flags in the file, using the Advanced Editor. For more information about comparison flags, see [Comparing String Data](../../integration-services/data-flow/comparing-string-data.md).  
  
 You can configure the Raw File destination in the following ways:  
  
-   Specify an access mode which is either the name of the file or a variable that contains the name of the file to which the Raw File destination writes.  
  
-   Indicate whether the Raw File destination appends data to an existing file that has the same name or creates a new file.  
  
 The Raw File destination is frequently used to write intermediary results of partly processed data between package executions. Storing raw data means that the data can be read quickly by a Raw File source and then further transformed before it is loaded into its final destination. For example, a package might run several times, and each time write raw data to files. Later, a different package can use the Raw File source to read from each file, use a Union All transformation to merge the data into one data set, and then apply additional transformations that summarize the data before loading the data into its final destination such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
> [!NOTE]  
>  The Raw File destination supports null data but not binary large object (BLOB) data.  
  
> [!NOTE]  
>  The Raw File destination does not use a connection manager.  
  
 This source has one regular input. It does not support an error output.  
  
## Append and New File Options  
 The WriteOption property includes options to append data to an existing file or create a new file.  
  
 The following table describes the available options for the WriteOption property.  
  
|Option|Description|  
|------------|-----------------|  
|Append|Appends data to an existing file. The metadata of the appended data must match the file format.|  
|Create always|Always creates a new file.|  
|Create once|Creates a new file. If the file exists, the component fails.|  
|Truncate and append|Truncates an existing file and then writes the data to the file. The metadata of the appended data must match the file format.|  
  
 The following are important items about appending data:  
  
-   Appending data to an existing raw file does not re-sort the data.  
  
     You need to make certain that the sorted keys remain in the correct order.  
  
-   Appending data to an existing raw file does not change the file metadata (sort information).  
  
 For example, a package reads data sorted on the ProductKey (PK). The package data flow appends the data to an existing raw file. The first time the package runs, three rows are received (PK 1000, 1100, 1200). The raw file now contains the following data.  
  
-   1000, productA  
  
-   1100, productB  
  
-   1200, productC  
  
 The second time the package runs, two new rows are received (PK 1001, 1300). The raw file now contains the following data.  
  
-   1000, productA  
  
-   1100, productB  
  
-   1200, productC  
  
-   1001, productD  
  
-   1300, productE  
  
 The new data is appended to the end of the raw file, and the sorted keys (PK) are out of order. In addition, the append operation didn't change the file metadata (sort information). If you read the file by using the Raw File source, the component indicates that the file is still sorted on PK even though the data in the file is no longer in the correct order.  
  
 To keep the sorted keys in the correct order while appending data, you can design the package data flow as follows:  
  
1.  Retrieve new rows by using Source A.  
  
2.  Retrieve existing rows from RawFile1 using Source B.  
  
3.  Combine the inputs from Source A and Source B by using the Union All transformation.  
  
4.  Sort on PK.  
  
5.  Write to RawFile2 by using the Raw File destination.  
  
     RawFile1 is locked because it's being read from, in the data flow.  
  
6.  Replace RawFile1 with RawFile2.  
  
### Using the Raw File Destination in a Loop  
 If the data flow that uses the Raw File destination is in a loop, you may want to create the file once and then append data to the file when the loop repeats. To append data to the file, the data that is appended must match the format of the existing file.  
  
 To create the file in the first iteration of the loop, and then append rows in the subsequent iterations of the loop, you need to do the following at design time:  
  
1.  Set the WriteOption property to **CreateOnce** or **CreateAlways**and run one iteration of the loop. The file is created. This ensures that the metadata of appended data and the file matches.  
  
2.  Reset the WriteOption property to **Append** and set the ValidateExternalMetadata property to **False**.  
  
 If you use the **TruncateAppend** option instead of the **Append** option, it will truncate rows that were added in any previous iteration, and then append new rows. Using the **TruncateAppend** option also requires that the data matches the file format.  
  
## Configuration of the Raw File Destination  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Raw File Custom Properties](../../integration-services/data-flow/raw-file-custom-properties.md)  
  
## Related Tasks  
 For information about how to set properties of the component, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Related Content  
 Blog entry, [Raw Files Are Awesome](https://www.sqlservercentral.com/blogs/stratesql/archive/2011/1/1/31-days-of-ssis-_1320_-raw-files-are-awesome-_2800_1_2F00_31_2900_.aspx), on sqlservercentral.com.  
  
## Raw File Destination Editor (Connection Manager Page)
  Use the Raw File Destination Editor to configure the Raw File destination to write raw data to a file.  
  
 **What do you want to do?**  
  
-   [Open the Raw File Destination Editor](#open)  
  
-   [Set options on the Connection Manager tab](#connection)  
  
-   [Set options on the Columns tab](#mapping)  
  
###  <a name="open"></a> Open the Raw File Destination Editor  
  
1.  Add the Raw File destination to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the component and then click **Edit**.  
  
###  <a name="connection"></a> Set options on the Connection Manager tab  
 **Access mode**  
 Select how the file name is specified. Select **File name** to enter the file name and path directly, of **File name from variable** to specify a variable that contains the file name.  
  
 **File name** or **Variable name**  
 Enter the name and path of the raw file, or select the variable that contains the file name.  
  
 **Write option**  
 Select the method used to create and write to the file.  
  
 **Generate initial raw file**  
 Click the button to generate an empty raw file that contains only the columns (metadata-only file), without having to run the package. The file contains the columns that you selected on the **Columns** page of the **Raw File Destination Editor**. You can point the Raw File source to this metadata-only file.  
  
 When you click **Generate initial raw file**, a message box appears. Click **OK** to proceed with creating the file. Click **Cancel** to select a different list of columns on the **Columns** page.  
  
###  <a name="mapping"></a> Set options on the Columns tab  
 **Available Input Columns**  
 Select one or more input columns to write to the raw file.  
  
 **Input Column**  
 An input column is automatically added to this table when you select it under **Available Input Columns**, or you can select the input column directly in this table.  
  
 **Output Alias**  
 Specify an alternate name to use for the output column.  
  
## Raw File Destination Editor (Columns Page)
  Use the Raw File Destination Editor to configure the Raw File destination to write raw data to a file.  
  
 **What do you want to do?**  
  
-   [Open the Raw File Destination Editor](#open)  
  
-   [Set options on the Connection Manager tab](#connection)  
  
-   [Set options on the Columns tab](#mapping)  
  
###  <a name="open"></a> Open the Raw File Destination Editor  
  
1.  Add the Raw File destination to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the component and then click **Edit**.  
  
###  <a name="connection"></a> Set options on the Connection Manager tab  
 **Access mode**  
 Select how the file name is specified. Select **File name** to enter the file name and path directly, of **File name from variable** to specify a variable that contains the file name.  
  
 **File name** or **Variable name**  
 Enter the name and path of the raw file, or select the variable that contains the file name.  
  
 **Write option**  
 Select the method used to create and write to the file.  
  
 **Generate initial raw file**  
 Click the button to generate an empty raw file that contains only the columns (metadata-only file), without having to run the package. You can point the Raw File source to the metadata-only file.  
  
 When you click the button, a list of the columns appears. You can click cancel and modify the columns or click OK to proceed with creating the file.  
  
###  <a name="mapping"></a> Set options on the Columns tab  
 **Available Input Columns**  
 Select one or more input columns to write to the raw file.  
  
 **Input Column**  
 An input column is automatically added to this table when you select it under **Available Input Columns**, or you can select the input column directly in this table.  
  
 **Output Alias**  
 Specify an alternate name to use for the output column.  
  
## See Also  
 [Raw File Source](../../integration-services/data-flow/raw-file-source.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
