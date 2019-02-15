---
title: "Configure the OLE DB Command Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "parameters [Integration Services]"
  - "OLE DB Command transformation"
ms.assetid: c800f167-3d2e-4c10-8ba3-a02f1872ccea
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Configure the OLE DB Command Transformation
  To add and configure an OLE DB Command transformation, the package must already include at least one Data Flow task and a source such as a Flat File source or an OLE DB source. This transformation is typically used for running parameterized queries.  
  
### To configure the OLE DB Command transformation  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the OLE DB Command transformation to the design surface.  
  
4.  Connect the OLE DB Command transformation to the data flow by dragging a connector-the green or red arrow-from a data source or a previous transformation to the OLE DB Command transformation.  
  
5.  Right-click the component and select Edit or Show **Advanced Editor**.  
  
6.  On the **Connection Managers** tab, select an OLE DB connection manager in the **Connection Manager** list. For more information, see [OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md).  
  
7.  Click the **Component Properties** tab and click the ellipsis button **(...)** in the **SqlCommand** box.  
  
8.  In the **String Value Editor**, type the parameterized SQL statement using a question mark (?) as the parameter marker for each parameter.  
  
9. Click **Refresh**. When you click **Refresh**, the transformation creates a column for each parameter in the External Columns collection and sets the DBParamInfoFlags property.  
  
10. Click the **Input and Output Properties** tab.  
  
11. Expand **OLE DB Command Input**, and then expand **External Columns**.  
  
12. Verify that **External Columns** lists a column for each parameter in the SQL statement. The column names are **Param_0**, **Param_1**, and so on.  
  
     You should not change the column names. If you change the column names, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] generates a validation error for the OLE DB Command transformation.  
  
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
 [OLE DB Command Transformation](data-flow/transformations/ole-db-command-transformation.md)   
 [Integration Services Transformations](data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](data-flow/integration-services-paths.md)   
 [Data Flow Task](control-flow/data-flow-task.md)  
  
  
