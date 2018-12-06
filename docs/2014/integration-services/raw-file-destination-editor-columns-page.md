---
title: "Raw File Destination Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.rawfiledestinationcolumns.f1"
ms.assetid: 37f61d0b-1269-42ee-94ab-011cbaac63e9
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Raw File Destination Editor (Columns Page)
  Use the Raw File Destination Editor to configure the Raw File destination to write raw data to a file.  
  
 **What do you want to do?**  
  
-   [Open the Raw File Destination Editor](#open)  
  
-   [Set options on the Connection Manager tab](#connection)  
  
-   [Set options on the Columns tab](#mapping)  
  
##  <a name="open"></a> Open the Raw File Destination Editor  
  
1.  Add the Raw File destination to an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the component and then click **Edit**.  
  
##  <a name="connection"></a> Set options on the Connection Manager tab  
 **Access mode**  
 Select how the file name is specified. Select **File name** to enter the file name and path directly, of **File name from variable** to specify a variable that contains the file name.  
  
 **File name** or **Variable name**  
 Enter the name and path of the raw file, or select the variable that contains the file name.  
  
 **Write option**  
 Select the method used to create and write to the file.  
  
 **Generate initial raw file**  
 Click the button to generate an empty raw file that contains only the columns (metadata-only file), without having to run the package. You can point the Raw File source to the metadata-only file.  
  
 When you click the button, a list of the columns appears. You can click cancel and modify the columns or click OK to proceed with creating the file.  
  
##  <a name="mapping"></a> Set options on the Columns tab  
 **Available Input Columns**  
 Select one or more input columns to write to the raw file.  
  
 **Input Column**  
 An input column is automatically added to this table when you select it under **Available Input Columns**, or you can select the input column directly in this table.  
  
 **Output Alias**  
 Specify an alternate name to use for the output column.  
  
## See Also  
 [Raw File Destination](data-flow/raw-file-destination.md)  
  
  
