---
title: "Fuzzy Grouping Transformation Editor (Connection Manager Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.fuzzygroupingtransformation.connection.f1"
helpviewer_keywords: 
  - "Fuzzy Grouping Transformation Editor"
ms.assetid: 47b1446d-5331-473c-9cb5-a98b1f55bf5f
caps.latest.revision: 27
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Fuzzy Grouping Transformation Editor (Connection Manager Tab)
  Use the **Connection Manager** tab of the **Fuzzy Grouping Transformation Editor** dialog box to select an existing connection or create a new one.  
  
> [!NOTE]  
>  The server specified by the connection must be running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The Fuzzy Grouping transformation creates temporary data objects in tempdb that may be as large as the full input to the transformation. While the transformation executes, it issues server queries against these temporary objects. This can affect overall server performance.  
  
 To learn more about the Fuzzy Grouping transformation, see [Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md).  
  
## Options  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager by using the list box, or create a new connection by using the **New** button.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/identify-similar-data-rows-by-using-the-fuzzy-grouping-transformation.md)  
  
  