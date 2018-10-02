---
title: "Fuzzy Grouping Transformation Editor (Connection Manager Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzygroupingtransformation.connection.f1"
helpviewer_keywords: 
  - "Fuzzy Grouping Transformation Editor"
ms.assetid: 47b1446d-5331-473c-9cb5-a98b1f55bf5f
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Fuzzy Grouping Transformation Editor (Connection Manager Tab)
  Use the **Connection Manager** tab of the **Fuzzy Grouping Transformation Editor** dialog box to select an existing connection or create a new one.  
  
> [!NOTE]  
>  The server specified by the connection must be running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The Fuzzy Grouping transformation creates temporary data objects in tempdb that may be as large as the full input to the transformation. While the transformation executes, it issues server queries against these temporary objects. This can affect overall server performance.  
  
 To learn more about the Fuzzy Grouping transformation, see [Fuzzy Grouping Transformation](data-flow/transformations/fuzzy-grouping-transformation.md).  
  
## Options  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager by using the list box, or create a new connection by using the **New** button.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](data-flow/transformations/identify-similar-data-rows-by-using-the-fuzzy-grouping-transformation.md)  
  
  
