---
title: "Flat File Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "connection managers [Integration Services], Flat File"
  - "connections [Integration Services], flat files"
  - "files [Integration Services], connections"
  - "Flat File connection manager"
  - "flat files"
  - "flat file connections [Integration Services]"
ms.assetid: 7830f80d-af32-4e8f-a6fc-f03af6bc1946
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flat File Connection Manager
  A Flat File connection manager enables a package to access data in a flat file. For example, the Flat File source and destination can use Flat File connection managers to extract and load data.  
  
 The Flat File connection manager can access only one file. To reference multiple files, use a Multiple Flat Files connection manager instead of a Flat File connection manager. For more information, see [Multiple Flat Files Connection Manager](multiple-flat-files-connection-manager.md).  
  
## Column Length  
 By default, the Flat File connection manager sets the length of string columns to 50 characters. In the **Flat File Connection Manager Editor** dialog box, you can evaluate sample data and automatically resize the length of these columns to prevent truncation of data or excess column width. Also, unless you subsequently resize the column length in a Flat File source or a transformation, the column length of string column remains the same throughout the data flow. If these string columns map to destination columns that are narrower, warnings appear in the user interface. Moreover, at run time, errors may occur due to data truncation. To avoid errors or truncation, you can resize the columns to be compatible with the destination columns in the Flat File connection manager, the Flat File source, or a transformation. To modify the length of output columns, you set the `Length` property of the output column on the **Input and Output Properties** tab in the **Advanced Editor** dialog box.  
  
 If you update column lengths in the Flat File connection manager after you have added and configured the Flat File source that uses the connection manager, you do not have to manually resize the output columns in the Flat File source. When you open the **Flat File Source** dialog box, the Flat File source provides an option to synchronize the column metadata.  
  
## Configuration of the Flat File Connection Manager  
 When you add a Flat File connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to a Flat File connection at run time, sets the Flat File connection properties, and adds the Flat File connection manager to the `Connections` collection of the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `FLATFILE`.  
  
 By default, the Flat File connection manager always checks for a row delimiter in unquoted data, and starts a new row when a row delimiter is found. This enables the connection manager to correctly parse files with rows that are missing column fields.  
  
 In some cases, disabling this feature may improve package performance. You can disable this feature by setting the Flat File connection manager property, **AlwaysCheckForRowDelimiters**, to `False`.  
  
 You can configure the Flat File connection manager in the following ways:  
  
-   Specify the file, locale, and code page to use. The locale is used to interpret locale-sensitive data such as dates, and the code page is used to convert string data to Unicode.  
  
-   Specify the file format. You can use a delimited, fixed width, or ragged right format.  
  
-   Specify a header row, data row, and column delimiters. Column delimiters can be set at the file level and overwritten at the column level.  
  
-   Indicate whether the first row in the file contains column names.  
  
-   Specify a text qualifier character. Each column can be configured to recognize a text qualifier.  
  
     The use of a qualifier character to embed a qualifier character into a qualified string is now supported. The double instance of a text qualifier is interpreted as a literal, single instance of that string. For example, if the text qualifier is a single quote and the input data is 'abc', 'def', 'g'hi', the output data is abc, def, g'hi.  
  
-   Set properties such as the name, data type, and maximum width on individual columns.  
  
 You can set the ConnectionString property for the Flat File connection manager by specifying an expression in the Properties window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. To avoid a validation error, do the following.  
  
-   When you use an expression to specify the file, add a file path in the **File name** box in the **Flat File Connection Manager Editor**.  
  
-   Set the **DelayValidation** property on the Flat File connection manager to **True**.  
  
 You can use an expression to create a file name at runtime by using the Flat File connection manager with the Flat File destination.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Flat File Connection Manager Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Flat File Connection Manager Editor &#40;Columns Page&#41;](../flat-file-connection-manager-editor-columns-page.md)  
  
-   [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../flat-file-connection-manager-editor-advanced-page.md)  
  
-   [Flat File Connection Manager Editor &#40;Preview Page&#41;](../flat-file-connection-manager-editor-preview-page.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
  
