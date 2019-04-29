---
title: "Multiple Flat Files Connection Manager Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.multifile.general.f1"
helpviewer_keywords: 
  - "Multiple Flat Files Connection Manager Editor"
ms.assetid: 00129d43-2772-413b-bdf8-ac5de81cf4a5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Multiple Flat Files Connection Manager Editor (General Page)
  Use the **General** page of the **Multiple Flat Files Connection Manager Editor** dialog box to select a group of files that have the same data format, and to specify their data format. A multiple flat files connection enables a package to connect to a group of text files that have the same format.  
  
 To learn more about the Multiple Flat Files connection manager, see [Multiple Flat Files Connection Manager](connection-manager/multiple-flat-files-connection-manager.md).  
  
## Options  
 **Connection manager name**  
 Provide a unique name for the Multiple Flat Files connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **File names**  
 Type the path and file names to use in the Multiple Flat Files connection. You can specify multiple files by using wildcard characters, as in the example "C:\\*.txt", or by using the vertical pipe character (|) to separate multiple file names. All files must have the same data format.  
  
 **Browse**  
 Browse the file names to use in the Multiple Flat Files connection. You can select multiple files. All files must have the same data format.  
  
 **Locale**  
 Specify the location to provide information for ordering and for date and time conversion.  
  
 **Unicode**  
 Indicate whether to use Unicode. Using Unicode precludes specifying a code page.  
  
 **Code page**  
 Specify the code page for non-Unicode text.  
  
 **Format**  
 Indicate whether to use delimited, fixed width, or ragged right formatting. All files must have the same data format.  
  
|Value|Description|  
|-----------|-----------------|  
|Delimited|Columns are separated by delimiters, specified on the **Columns** page.|  
|Fixed width|Columns have a fixed width, specified by dragging marker lines on the **Columns** page.|  
|Ragged right|Ragged right files are files in which every column has a fixed width, except for the last column, which is delimited by the row delimiter, specified on the **Columns** page.|  
  
 **Text qualifier**  
 Specify the text qualifier to use. For example, you can specify to enclose text with quotation marks.  
  
 **Header row delimiter**  
 Select from the list of delimiters for header rows, or enter the delimiter text.  
  
|Value|Description|  
|-----------|-----------------|  
|**{CR}{LF}**|The header row is delimited by a carriage return-line feed combination.|  
|**{CR}**|The header row is delimited by a carriage return.|  
|**{LF}**|The header row is delimited by a line feed.|  
|**Semicolon {;}**|The header row is delimited by a semicolon.|  
|**Colon {:}**|The header row is delimited by a colon.|  
|**Comma {,}**|The header row is delimited by a comma.|  
|**Tab {t}**|The header row is delimited by a tab.|  
|**Vertical bar {&#124;}**|The header row is delimited by a vertical bar.|  
  
 **Header rows to skip**  
 Specify the number of header rows to skip, if any.  
  
 **Column names in the first data row**  
 Indicate whether to expect or provide column names in the first data row.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-columns-page.md)   
 [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-advanced-page.md)   
 [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-preview-page.md)  
  
  
