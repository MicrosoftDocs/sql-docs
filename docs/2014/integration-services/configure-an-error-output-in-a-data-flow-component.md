---
title: "Configure an Error Output in a Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "errors [Integration Services], data flow components"
  - "components [Integration Services], data flow"
  - "error outputs [Integration Services]"
ms.assetid: 53d7eeea-927d-4b45-8ea9-084e65ad5390
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Configure an Error Output in a Data Flow Component
  Many data flow components support error outputs, and depending on the component, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer provides different ways to configure an error output. In addition to configuring an error output, you can also configure the columns of an error output. This includes configuring the **ErrorCode** and **ErrorColumn** columns that are added by the component.  
  
## Configuring an Error Output  
 To configure an error output, you have two options:  
  
-   Use the **Configure Error Output** dialog box. You can use this dialog box to configure an error output on any data flow component that supports an error output.  
  
-   Use the editor dialog box for the component. Some components let you configure error outputs directly from their editor dialog box. However, you cannot configure error outputs from the editor dialog box for the ADO NET source, the Import Column transformation, the OLE DB Command transformation, or the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact destination.  
  
 The following procedures describe how to use these dialog boxes to configure error outputs.  
  
#### To configure an error output using the Configure Error Output dialog box  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Drag the error output, represented by the red arrow, from the component that is the source of the errors to another component in the data flow.  
  
5.  In the **Configure Error Output** dialog box, select an action in the **Error** and **Truncation** columns for each column in the component input.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
#### To add an error output using the editor dialog box for the component  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Double-click the data flow components in which you want to configure an error output and, depending on the component, do one of the following steps:  
  
    -   Click **Configure Error Output**.  
  
    -   Click **Error Output**.  
  
5.  Set the **Error** option for each column.  
  
6.  Set the **Truncation** option for each column.  
  
7.  Click **OK**.  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## Configuring Error Output Columns  
 To configure the error output columns, you have to use the **Input and Output Properties** tab of the **Advanced Editor** dialog box.  
  
#### To configure error output columns  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Right-click the component whose error output columns you want to configure and click **Show Advanced Editor**.  
  
5.  Click the **Input and Output Properties** tab and expand **\<component name> Error Output** and then expand **Output Columns**.  
  
6.  Click a column and update its properties.  
  
    > [!NOTE]  
    >  The list of columns includes the columns in the component input, the **ErrorCode** and **ErrorColumn** columns added by previous error outputs, and the **ErrorCode** and **ErrorColumn** columns added by this component.  
  
7.  Click **OK.**  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
## See Also  
 [Error Handling in Data](data-flow/error-handling-in-data.md)  
  
  
