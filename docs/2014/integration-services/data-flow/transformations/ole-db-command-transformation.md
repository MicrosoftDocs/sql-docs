---
title: "OLE DB Command Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.oledbcommandtrans.f1"
helpviewer_keywords: 
  - "statements [Integration Services]"
  - "OLE DB Command transformation"
ms.assetid: baa6735c-5acf-4759-b077-1216aca16c6c
author: janinezhang
ms.author: janinez
manager: craigg
---
# OLE DB Command Transformation
  The OLE DB Command transformation runs an SQL statement for each row in a data flow. For example, you can run an SQL statement that inserts, updates, or deletes rows in a database table.  
  
 You can configure the OLE DB Command Transformation in the following ways:  
  
-   Provide the SQL statement that the transformation runs for each row.  
  
-   Specify the number of seconds before the SQL statement times out.  
  
-   Specify the default code page.  
  
 Typically, the SQL statement includes parameters. The parameter values are stored in external columns in the transformation input, and mapping an input column to an external column maps an input column to a parameter. For example, to locate rows in the **DimProduct** table by the value in their **ProductKey** column and then delete them, you can map the external column named **Param_0** to the input column named **ProductKey,** and then run the SQL statement `DELETE FROM DimProduct WHERE ProductKey = ?`.. The OLE DB Command transformation provides the parameter names and you cannot modify them. The parameter names are **Param_0**, **Param_1**, and so on.  
  
 If you configure the OLE DB Command transformation by using the **Advanced Editor** dialog box, the parameters in the SQL statement may be mapped automatically to external columns in the transformation input, and the characteristics of each parameter defined, by clicking the **Refresh** button. However, if the OLE DB provider that the OLE DB Command transformation uses does not support deriving parameter information from the parameter, you must configure the external columns manually. This means that you must add a column for each parameter to the external input to the transformation, update the column names to use names like **Param_0**, specify the value of the DBParamInfoFlags property, and map the input columns that contain parameter values to the external columns.  
  
 The value of DBParamInfoFlags represents the characteristics of the parameter. For example, the value **1** specifies that the parameter is an input parameter, and the value **65** specifies that the parameter is an input parameter and may contain a null value. The values must match the values in the OLE DB DBPARAMFLAGSENUM enumeration. For more information, see the OLE DB reference documentation.  
  
 The OLE DB Command transformation includes the `SQLCommand` custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](transformation-custom-properties.md).  
  
 This transformation has one input, one regular output, and one error output.  
  
## Logging  
 You can log the calls that the OLE DB Command transformation makes to external data providers. You can use this logging capability to troubleshoot the connections and commands to external data sources that the OLE DB Command transformation performs. To log the calls that the OLE DB Command transformation makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Related Tasks  
 You can configure the transformation by either using the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or the object model. For details about configuring the transformation using the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] designer, see  [Configure the OLE DB Command Transformation](../../configure-the-ole-db-command-transformation.md). See the Developer Guide for details about programmatically configuring this transformation.  
  
## See Also  
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
