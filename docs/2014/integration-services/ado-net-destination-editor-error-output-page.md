---
title: "ADO NET Destination Editor (Error Output Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.adonetdest.erroroutput.f1"
ms.assetid: 1a56c3cf-fb6a-416d-a62c-bb19fe441ae5
author: douglaslms
ms.author: douglasl
manager: craigg
---
# ADO NET Destination Editor (Error Output Page)
  Use the **Error Output** page of the **ADO NET Destination Editor** dialog box to specify error handling options.  
  
 To learn more about the ADO NET destination, see [ADO NET Destination](data-flow/ado-net-destination.md).  
  
 **To open the Error Output page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Error Output**.  
  
## Options  
 **Input or Output**  
 View the name of the input.  
  
 **Column**  
 Not used.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Not used.  
  
 **Description**  
 View the description of the operation.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [ADO NET Destination Editor &#40;Connection Manager Page&#41;](../../2014/integration-services/ado-net-destination-editor-connection-manager-page.md)   
 [ADO NET Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/ado-net-destination-editor-mappings-page.md)  
  
  
