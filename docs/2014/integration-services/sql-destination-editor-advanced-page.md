---
title: "SQL Destination Editor (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.sqlserverdestadapter.advanced.f1"
helpviewer_keywords: 
  - "SQL Server Destination Editor"
ms.assetid: 9b46bcf8-ddaf-4d7d-90a6-80bc19517e9b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# SQL Destination Editor (Advanced Page)
  Use the **Advanced** page of the **SQL Destination Editor** dialog box to specify advanced bulk insert options.  
  
 To learn more about the SQL Server destination, see [SQL Server Destination](data-flow/sql-server-destination.md).  
  
## Options  
 **Keep identity**  
 Specify whether the task should insert values into identity columns. The default value of this property is `False`.  
  
 **Keep nulls**  
 Specify whether the task should keep null values. The default value of this property is `False`.  
  
 **Table lock**  
 Specify whether the table is locked when the data is loaded. The default value of this property is `True`.  
  
 **Check constraints**  
 Specify whether the task should check constraints. The default value of this property is `True`.  
  
 **Fire triggers**  
 Specify whether the bulk insert should fire triggers on tables. The default value of this property is `False`.  
  
 **First Row**  
 Specify the first row to insert. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Last Row**  
 Specify the last row to insert. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Maximum number of errors**  
 Specify the number of errors that can occur before the bulk insert stops. The default value of this property is **-1**, indicating that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **SQL Destination Editor** to indicate that you do not want to assign a value for this property. Use -1 in the **Properties** window, the **Advanced Editor**, and the object model.  
  
 **Timeout**  
 Specify the number of seconds to wait before the bulk insert stops because of a time-out.  
  
 **Order columns**  
 Type the names of the sort columns. Each column can be sorted in ascending or descending order. If you use multiple sort columns, delimit the list with commas.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [SQL Destination Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/sql-destination-editor-connection-manager-page.md)   
 [SQL Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/sql-destination-editor-mappings-page.md)   
 [Bulk Load Data by Using the SQL Server Destination](data-flow/bulk-load-data-by-using-the-sql-server-destination.md)  
  
  
