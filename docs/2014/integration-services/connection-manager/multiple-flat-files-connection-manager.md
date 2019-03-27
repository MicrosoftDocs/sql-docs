---
title: "Multiple Flat Files Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Multiple Flat Files connection manager"
  - "connections [Integration Services], flat files"
  - "flat files"
  - "flat file connections [Integration Services]"
  - "connection managers [Integration Services], Multiple Flat Files"
  - "multiple flat file connections"
ms.assetid: 31fc3f7a-d323-44f5-a907-1fa3de66631a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Multiple Flat Files Connection Manager
  A Multiple Flat Files connection manager enables a package to access data in multiple flat files. For example, a Flat File source can use a Multiple Flat Files connection manager when the Data Flow task is inside a loop container, such as the For Loop container. On each loop of the container, the Flat File source loads data from the next file name that the Multiple Flat Files connection manager provides.  
  
 When you add a Multiple Flat Files connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a Multiple Flat Files connection at run time, sets the properties on the Multiple Flat Files connection manager, and adds the Multiple Flat Files connection manager to the `Connections` collection of the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `MULTIFLATFILE`.  
  
 You can configure the Multiple Flat Files connection manager in the following ways:  
  
-   Specify the files, locale, and code page to use. The locale is used to interpret locale-sensitive data such as dates, and the code page is used to convert string data to Unicode.  
  
-   Specify the file format. You can use a delimited, fixed width, or ragged right format.  
  
-   Specify a header row, data row, and column delimiters. Column delimiters can be set at the file level and overwritten at the column level.  
  
-   Indicate whether the first row in the files contains column names.  
  
-   Specify a text qualifier character. Each column can be configured to recognize a text qualifier.  
  
-   Set properties such as the name, data type, and maximum width on individual columns.  
  
 When the Multiple Flat Files connection manager references multiple files, the paths of the files are separated by the pipe (|) character. The `ConnectionString` property of the connection manager has the following format:  
  
 \<*path*>|\<*path*>  
  
 You can also specify multiple files by using wildcard characters. For example, to reference all the text files on the C drive, the value of the `ConnectionString` property can be set to C:\\*.txt.  
  
 If a Multiple Flat Files connection manager references multiple files, all the files must have the same format.  
  
 By default, the Multiple Flat Files connection manager sets the length of string columns to 50 characters. In the **Multiple Flat Files Connection Manager Editor** dialog box, you can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. Unless you resize the column length in a Flat File source or a transformation, the column length remains the same throughout the data flow. If these columns map to destination columns that are narrower, warnings appear in the user interface, and at run time, errors may occur due to data truncation. You can resize the columns to be compatible with the destination columns in the Flat File connection manager, the Flat File source, or a transformation. To modify the length of output columns, you set the `Length` property of the output column on the **Input and Output Properties** tab in the **Advanced Editor** dialog box.  
  
 If you update column lengths in the Multiple Flat Files connection manager after you have added and configured the Flat File source that uses the connection manager, you do not have to manually resize the output columns in the Flat File source. When you open the **Flat File Source** dialog box, the Flat File source provides an option to synchronize the column metadata.  
  
## Configuration of the Multiple Flat Files Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../multiple-flat-files-connection-manager-editor-columns-page.md)  
  
-   [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../multiple-flat-files-connection-manager-editor-advanced-page.md)  
  
-   [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../multiple-flat-files-connection-manager-editor-preview-page.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
## See Also  
 [Flat File Source](../data-flow/flat-file-source.md)   
 [Flat File Destination](../data-flow/flat-file-destination.md)   
 [Integration Services &#40;SSIS&#41; Connections](integration-services-ssis-connections.md)  
  
  
