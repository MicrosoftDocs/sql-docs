---
title: "ADO NET Destination Editor (Error Output Page) | Microsoft Docs"
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
  - "sql13.dts.designer.adonetdest.erroroutput.f1"
ms.assetid: 1a56c3cf-fb6a-416d-a62c-bb19fe441ae5
caps.latest.revision: 17
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# ADO NET Destination Editor (Error Output Page)
  Use the **Error Output** page of the **ADO NET Destination Editor** dialog box to specify error handling options.  
  
 To learn more about the ADO NET destination, see [ADO NET Destination](../../integration-services/data-flow/ado-net-destination.md).  
  
 **To open the Error Output page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Error Output**.  
  
## Options  
 **Input or Output**  
 View the name of the input.  
  
 **Column**  
 Not used.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Not used.  
  
 **Description**  
 View the description of the operation.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [ADO NET Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-connection-manager-page.md)   
 [ADO NET Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-mappings-page.md)  
  
  