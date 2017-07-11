---
title: "ADO NET Source Editor (Error Output Page) | Microsoft Docs"
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
  - "sql13.dts.designer.adonetsource.erroroutput.f1"
ms.assetid: 4dd9d129-a95c-4d3a-bbbf-e84a39089950
caps.latest.revision: 14
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# ADO NET Source Editor (Error Output Page)
  Use the **Error Output** page of the **ADO NET Source Editor** dialog box to select error handling options and to set properties on error output columns.  
  
 To learn more about the ADO NET source, see [ADO NET Source](../../integration-services/data-flow/ado-net-source.md).  
  
 **To open the Error Output page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the ADO NET source.  
  
2.  On the **Data Flow** tab, double-click the ADO NET source.  
  
3.  In the **ADO NET Source Editor**, click **Error Output**.  
  
## Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **ADO NET Source Editor** dialog box.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 View the description of the error.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [ADO NET Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/ado-net-source-editor-connection-manager-page.md)   
 [ADO NET Source Editor &#40;Columns Page&#41;](../../integration-services/data-flow/ado-net-source-editor-columns-page.md)   
 [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)  
  
  