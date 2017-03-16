---
title: "Flat File Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.flatfiledestadapter.connection.f1"
helpviewer_keywords: 
  - "Flat File Destination Editor"
ms.assetid: b01571fa-bc19-4742-8eed-ac163172a919
caps.latest.revision: 30
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Flat File Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Flat File Destination Editor** dialog box to select the flat file connection for the destination, and to specify whether to overwrite or append to the existing destination file. The flat file destination writes data to a text file. This text file can be in delimited, fixed width, fixed width with row delimiter, or ragged right format.  
  
 To learn more about the Flat File destination, see [Flat File Destination](../../integration-services/data-flow/flat-file-destination.md).  
  
## Options  
 **Flat File connection manager**  
 Select an existing connection manager by using the list box, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Flat File Format** and **Flat File Connection Manager Editor** dialog boxes.  
  
 In addition to the standard flat file formats of delimited, fixed width, and ragged right, the **Flat File Format** dialog box has a fourth option, **Fixed width with row delimiters**. This option represents a special case of the ragged-right format in which [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] adds a dummy column as the final column of data. This dummy column ensures that the final column has a fixed width.  
  
 The **Fixed width with row delimiters** option is not available in the **Flat File Connection Manager Editor**. If necessary, you can emulate this option in the editor. To emulate this option, on the **General** page of the **Flat File Connection Manager Editor**, for **Format**, select **Ragged right**. Then on the **Advanced** page of the editor, add a new dummy column as the final column of data.  
  
 **Overwrite data in the file**  
 Indicate whether to overwrite an existing file, or to append data to it.  
  
 **Header**  
 Type a block of text to insert into the file before any data is written. Use this option to include additional information, such as column headings.  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. Preview can display up to 200 rows.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Flat File Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/flat-file-destination-editor-mappings-page.md)  
  
  