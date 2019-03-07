---
title: "Convert Data to a Different Data Type by Using the Data Conversion Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "converting data types [Integration Services]"
  - "Data Conversion transformation"
  - "data types [Integration Services], converting"
ms.assetid: 4aabbe4f-7666-4672-865a-9627bd25fbfd
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Convert Data to a Different Data Type by Using the Data Conversion Transformation
  To add and configure a Data Conversion transformation, the package must already include at least one Data Flow task and one source.  
  
### To convert data to a different data type  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the Data Conversion transformation to the design surface.  
  
4.  Connect the Data Conversion transformation to the data flow by dragging a connector from the source or the previous transformation to the Data Conversion transformation.  
  
5.  Double-click the Data Conversion transformation.  
  
6.  In the **Data ConversionTransformation Editor** dialog box, in the **Available Input Columns** table, select the check box next to the columns whose data type you want to convert.  
  
    > [!NOTE]  
    >  You can apply multiple data conversions to an input column.  
  
7.  Optionally, modify the default values in the **Output Alias** column.  
  
8.  In the **Data Type** list, select the new data type for the column. The default data type is the data type of the input column.  
  
9. Optionally, depending on the selected data type, update the values in the **Length**, the **Precision**, the **Scale**, and the **Code Page** columns.  
  
10. To configure the error output, click **Configure Error Output**. For more information, see [Configure an Error Output in a Data Flow Component](../../configure-an-error-output-in-a-data-flow-component.md).  
  
11. Click **OK**.  
  
12. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Data Conversion Transformation](data-conversion-transformation.md)   
 [Integration Services Transformations](integration-services-transformations.md)   
 [Integration Services Paths](../integration-services-paths.md)   
 [Integration Services Data Types](../integration-services-data-types.md)   
 [Data Flow Task](../../control-flow/data-flow-task.md)  
  
  
