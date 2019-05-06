---
title: "Flat File Connection Manager Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.ffileconnection.general.f1"
helpviewer_keywords: 
  - "Flat File Connection Manager Editor"
ms.assetid: 77296024-5c1a-4f6a-9665-0b50d45d744c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flat File Connection Manager Editor (General Page)
  Use the **General** page of the **Flat File Connection Manager Editor** dialog box to select a file and data format. A flat file connection enables a package to connect to a text file.  
  
 To learn more about the Flat File connection manager, see [Flat File Connection Manager](connection-manager/file-connection-manager.md).  
  
## Options  
 **Connection manager name**  
 Provide a unique name for the flat file connection in the workflow. The name provided will be displayed within [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the connection. As a best practice, describe the connection in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **File name**  
 Type the path and file name to use in the flat file connection.  
  
 **Browse**  
 Locate the file name to use in the flat file connection.  
  
 **Locale**  
 Specify the locale to provide language-specific information for ordering and for date and time formats.  
  
 **Unicode**  
 Indicate whether to use Unicode. If you use Unicode, you cannot specify a code page.  
  
 **Code page**  
 Specify the code page for non-Unicode text.  
  
 **Format**  
 Indicate whether the file uses delimited, fixed width, or ragged right formatting.  
  
|Value|Description|  
|-----------|-----------------|  
|Delimited|Columns are separated by delimiters, specified on the **Columns** page.|  
|Fixed width|Columns have a fixed width.|  
|Ragged right|Ragged right files are files in which every column has a fixed width, except for the last column. It is delimited by the row delimiter.|  
  
 **Text qualifier**  
 Specify the text qualifier to use. For example, you can specify that text fields are enclosed in quotation marks.  
  
> [!NOTE]  
>  After you select a text qualifier, you cannot re-select the **None** option. Type **None** to de-select the text qualifier.  
  
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
 Specify the number of header rows or initial data rows to skip, if any.  
  
 **Column names in the first data row**  
 Indicate whether to expect or provide column names in the first data row.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-columns-page.md)   
 [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-advanced-page.md)   
 [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-preview-page.md)  
  
  
