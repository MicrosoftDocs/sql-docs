---
title: "OLE DB Command Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.oledbcommandtrans.f1"
helpviewer_keywords: 
  - "statements [Integration Services]"
  - "OLE DB Command transformation"
ms.assetid: baa6735c-5acf-4759-b077-1216aca16c6c
author: janinezhang
ms.author: janinez
manager: craigg
---
# OLE DB Command Transformation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The OLE DB Command transformation runs an SQL statement for each row in a data flow. For example, you can run an SQL statement that inserts, updates, or deletes rows in a database table.  
  
 You can configure the OLE DB Command Transformation in the following ways:  
  
-   Provide the SQL statement that the transformation runs for each row.  
  
-   Specify the number of seconds before the SQL statement times out.  
  
-   Specify the default code page.  
  
 Typically, the SQL statement includes parameters. The parameter values are stored in external columns in the transformation input, and mapping an input column to an external column maps an input column to a parameter. For example, to locate rows in the **DimProduct** table by the value in their **ProductKey** column and then delete them, you can map the external column named **Param_0** to the input column named **ProductKey,** and then run the SQL statement `DELETE FROM DimProduct WHERE ProductKey = ?`.. The OLE DB Command transformation provides the parameter names and you cannot modify them. The parameter names are **Param_0**, **Param_1**, and so on.  
  
 If you configure the OLE DB Command transformation by using the **Advanced Editor** dialog box, the parameters in the SQL statement may be mapped automatically to external columns in the transformation input, and the characteristics of each parameter defined, by clicking the **Refresh** button. However, if the OLE DB provider that the OLE DB Command transformation uses does not support deriving parameter information from the parameter, you must configure the external columns manually. This means that you must add a column for each parameter to the external input to the transformation, update the column names to use names like **Param_0**, specify the value of the DBParamInfoFlags property, and map the input columns that contain parameter values to the external columns.  
  
 The value of DBParamInfoFlags represents the characteristics of the parameter. For example, the value **1** specifies that the parameter is an input parameter, and the value **65** specifies that the parameter is an input parameter and may contain a null value. The values must match the values in the OLE DB DBPARAMFLAGSENUM enumeration. For more information, see the OLE DB reference documentation.  
  
 The OLE DB Command transformation includes the **SQLCommand** custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 This transformation has one input, one regular output, and one error output.  
  
## Logging  
 You can log the calls that the OLE DB Command transformation makes to external data providers. You can use this logging capability to troubleshoot the connections and commands to external data sources that the OLE DB Command transformation performs. To log the calls that the OLE DB Command transformation makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Related Tasks  
 You can configure the transformation by either using the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or the object model. See the Developer Guide for details about programmatically configuring this transformation.  
  
## Configure the OLE DB Command Transformation
  To add and configure an OLE DB Command transformation, the package must already include at least one Data Flow task and a source such as a Flat File source or an OLE DB source. This transformation is typically used for running parameterized queries.  
  
#### To configure the OLE DB Command transformation  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the OLE DB Command transformation to the design surface.  
  
4.  Connect the OLE DB Command transformation to the data flow by dragging a connector-the green or red arrow-from a data source or a previous transformation to the OLE DB Command transformation.  
  
5.  Right-click the component and select Edit or Show **Advanced Editor**.  
  
6.  On the **Connection Managers** tab, select an OLE DB connection manager in the **Connection Manager** list. For more information, see [OLE DB Connection Manager](../../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
7.  Click the **Component Properties** tab and click the ellipsis button **(...)** in the **SqlCommand** box.  
  
8.  In the **String Value Editor**, type the parameterized SQL statement using a question mark (?) as the parameter marker for each parameter.  
  
9. Click **Refresh**. When you click **Refresh**, the transformation creates a column for each parameter in the External Columns collection and sets the DBParamInfoFlags property.  
  
10. Click the **Input and Output Properties** tab.  
  
11. Expand **OLE DB Command Input**, and then expand **External Columns**.  
  
12. Verify that **External Columns** lists a column for each parameter in the SQL statement. The column names are **Param_0**, **Param_1**, and so on.  
  
     You should not change the column names. If you change the column names, [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] generates a validation error for the OLE DB Command transformation.  
  
     Also, you should not change the data type. The DataType property of each column is set to the correct data type.  
  
13. If **External Columns** lists no columns you must add them manually.  
  
    -   Click **Add Column** one time for each parameter in the SQL statement.  
  
    -   Update the column names to **Param_0**, **Param_1**, and so on.  
  
    -   Specify a value in the DBParamInfoFlags property. The value must match a value in the OLE DB DBPARAMFLAGSENUM enumeration. For more information, see the OLE DB reference documentation.  
  
    -   Specify the data type of the column and, depending on the data type, specify the code page, length, precision, and scale of the column.  
  
    -   To delete an unused parameter, select the parameter in **External Columns**, and then click **Remove Column**.  
  
    -   Click **Column Mappings** and map columns in the **Available Input Columns** list to parameters in the **Available Destination Columns** list.  
  
14. Click **OK**.  
  
15. To save the updated package, click **Save** on the **File** menu.  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
