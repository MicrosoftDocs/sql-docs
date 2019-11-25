---
title: "Set the Properties of a Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "components [Integration Services], properties"
ms.assetid: 73000ef6-52a2-4dec-8320-0e79acf0c2c5
author: janinezhang
ms.author: janinez
manager: craigg
---
# Set the Properties of a Data Flow Component
  To set the properties of data flow components, which include sources, destinations, and transformations, use one of the following features:  
  
-   The component editors that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides. These editors include only the custom properties of each data flow component.  
  
-   The **Properties** window lists the component-level custom properties of each element, as well as the properties common to all data flow elements.  
  
-   The **Advanced Editor** dialog box provides access to custom properties for each component. The **Advanced Editor** dialog box also provides access to the properties common to all data flow components-the properties of inputs, outputs, error outputs, columns, and external columns.  
  
### To set the properties of a data flow component by using a component editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the data flow with the component whose properties you want to view and modify.  
  
4.  Double-click the data flow component.  
  
5.  In the component editor, view or modify the property values, and then close the editor.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
### To set the properties of a data flow component by using the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the component whose properties you want to view and modify.  
  
4.  Right-click the data flow component, and then click **Properties**.  
  
5.  View or modify the property values, and then close the **Properties** window.  
  
    > [!NOTE]  
    >  Many properties are read-only, and cannot be modified.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
### To set the properties of a data flow component by using the Advanced Editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, and then double-click the Data Flow task that contains the component you want to view or modify.  
  
4.  In the data flow designer, right-click the data flow component, and then click **Show Advanced Editor**.  
  
    > [!NOTE]  
    >  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], data flow components that support multiple inputs cannot use the **Advanced Editor**.  
  
5.  In the **Advanced Editor** dialog box, do any of the following steps:  
  
    -   To view and specify the connection that the component uses, click the **Connection Managers** tab.  
  
        > [!NOTE]  
        >  The **Connection Managers** tab is available only to data flow components that use connection managers to connect to data sources such as files and databases  
  
    -   To view and modify component-level properties, click the **Component Properties** tab.  
  
    -   To view and modify mappings between external columns and the available output, click the **Column Mappings** tab.  
  
        > [!NOTE]  
        >  The **Column Mappings** tab is available only when viewing or editing sources or destinations.  
  
    -   To view a list of the available input columns and to update the names of output columns, click the **Input Columns** tab.  
  
        > [!NOTE]  
        >  The Input Columns tab is available only when working with transformations or destinations. For more information, see [Integration Services Transformations](transformations/integration-services-transformations.md).  
  
    -   To view and modify the properties of inputs, outputs, and error outputs, and the properties of the columns they contain, click the **Input and Output Properties** tab.  
  
        > [!NOTE]  
        >  Sources have no inputs. Destinations have no outputs, except for an optional error output.  
  
6.  View or modify the property values.  
  
7.  Click **OK**.  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## See Also  
 [Integration Services Transformations](transformations/integration-services-transformations.md)  
  
  
