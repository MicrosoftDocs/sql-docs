---
title: "Excel Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: bdcc72b8-8950-47bd-88bf-5db6d48cc6bf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Excel Custom Properties
  **Source Custom Properties**  
  
 The Excel source has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Excel source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer|The mode used to access the database. The possible values are **Open Rowset**, **Open Rowset from Variable**, **SQL Command**, and **SQL Command from Variable**. The default value is **Open Rowset**.|  
|CommandTimeout|Integer|The number of seconds before a command times out.  A value of 0 indicates an infinite time-out.<br /><br /> **Note** This property is not available in the **Excel Source Editor**, but can be set by using the **Advanced Editor**.|  
|OpenRowset|String|The name of the database object that is used to open a rowset.|  
|OpenRowsetVariable|String|The variable that contains the name of the database object that is used to open a rowset.|  
|ParameterMapping|String|The mapping from parameters in the SQL command to variables.|  
|SqlCommand|String|The SQL command to be executed.|  
|SqlCommandVariable|String|The variable that contains the SQL command to be executed.|  
  
 The output and the output columns of the Excel source have no custom properties.  
  
 For more information, see [Excel Source](../../integration-services/data-flow/excel-source.md).  
  
 **Destination Custom Properties**  
  
 The Excel destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Excel destination. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer (enumeration)|A value that specifies how the destination accesses its destination database.<br /><br /> This property can have one of the following values:<br /><br /> **OpenRowset** (0)-You provide the name of a table or view.<br /><br /> **OpenRowset from Variable** (1)-You provide the name of a variable that contains the name of a table or view.<br /><br /> **OpenRowset Using Fastload** (3)-You provide the name of a table or view.<br /><br /> **OpenRowset Using Fastload from Variable** (4)-You provide the name of a variable that contains the name of a table or view.<br /><br /> **SQL Command** (2)-You provide a SQL statement.|  
|CommandTimeout|Integer|The maximum number of seconds that the SQL command can run before timing out. A value of **0** indicates an infinite time. The default value of this property is **0**.<br /><br /> Note: This property is not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**.|  
|FastLoadKeepIdentity|Boolean|A value that specifies whether to copy identity values when data is loaded. This property is available only when using one of the fast load options. The default value of this property is **False**.|  
|FastLoadKeepNulls|Boolean|A value that specifies whether to copy Null values when data is loaded. This property is available only with one of the fast load options. The default value of this property is **False**.|  
|FastLoadMaxInsertCommitSize|Integer|A value that specifies the batch size that the Excel destination tries to commit during fast load operations. The default value is **2147483647**. A value of **0** indicates a single commit operation after all rows are processed.|  
|FastLoadOptions|String|A collection of fast load options. The fast load options include the locking of tables and the checking of constraints. You can specify one, both, or neither.<br /><br /> Note: Some options for this property are not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**.|  
|OpenRowset|String|When AccessMode is **OpenRowset**, the name of the table or view that the Excel destination accesses.|  
|OpenRowsetVariable|String|When AccessMode is **OpenRowset from Variable**, the name of the variable that contains the name of the table or view that the Excel destination accesses.|  
|SqlCommand|String|When AccessMode is **SQL Command**, the Transact-SQL statement that the Excel destination uses to specify the destination columns for the data.|  
  
 The input and the input columns of the Excel destination have no custom properties.  
  
 For more information, see [Excel Destination](../../integration-services/data-flow/excel-destination.md).  
  
## See Also  
 [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
 [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)
  
  
