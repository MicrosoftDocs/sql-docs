---
title: "Extract Data by Using the XML Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "extracting data [Integration Services]"
  - "sources [Integration Services], XML"
  - "XML source [Integration Services]"
ms.assetid: 5d5be54c-2b7e-4957-9193-c5ea5c5d6d15
author: janinezhang
ms.author: janinez
manager: craigg
---
# Extract Data by Using the XML Source
  To add and configure an XML source, the package must already include at least one Data Flow task.  
  
### To extract data using an XML source  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the XML source to the design surface.  
  
4.  Double-click the XML source.  
  
5.  In the **XML Source Editor**, on the **Connection Manager** page, select a data access mode:  
  
    -   For the **XML file location** access mode, click **Browse** and locate the folder that contains the XML file.  
  
    -   For the **XML file from variable** access mode, select the user-defined variable that contains the path of the XML file.  
  
    -   For the **XML data from variable** access mode, select the user-defined variable that contains the XML data.  
  
    > [!NOTE]  
    >  The variables must be defined in the scope of the Data Flow task that contains the XML source, or in the scope of the package; additionally, the variable must have a string data type.  
  
6.  Optionally, select **Use inline schema** to indicate that the XML document includes schema information.  
  
7.  To specify an external XML Schema definition language (XSD) schema for the XML file, do one of the following:  
  
    -   Click **Browse** to locate an existing XSD file.  
  
    -   Click **Generate XSD** to create an XSD from the XML file.  
  
8.  To update the names of output columns, click **Columns** and edit the values in the **Output Column** list.  
  
9. To configure the error output, click **Error Output**. For more information, see [Debugging Data Flow](../../integration-services/troubleshooting/debugging-data-flow.md).  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [XML Source](../../integration-services/data-flow/xml-source.md)   
 [Integration Services Transformations](../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../integration-services/control-flow/data-flow-task.md)  
  
  
