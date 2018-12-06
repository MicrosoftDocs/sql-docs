---
title: "OLE DB Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 13a82d41-dd7a-4708-bc84-4407a536c877
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# OLE DB Custom Properties
  **Source Custom Properties**  
  
 The OLE DB source has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the OLE DB source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer|The mode used to access the database. The possible values are **Open Rowset**, **Open Rowset from Variable**, **SQL Command**, and **SQL Command from Variable**. The default value is **Open Rowset**.|  
|AlwaysUseDefaultCodePage|Boolean|A value that indicates whether to use the value of the **DefaultCodePage** property for each column, or to try to derive the codepage from each column's locale. The default value of this property is **False**.|  
|CommandTimeout|Integer|The number of seconds before a command times out. A value of 0 indicates an infinite time-out.<br /><br /> Note: This property is not available in the **OLE DB Source Editor**, but can be set by using the **Advanced Editor**.|  
|DefaultCodePage|Integer|The code page to use when code page information is unavailable from the data source.|  
|OpenRowset|String|The name of the database object that is used to open a rowset.|  
|OpenRowsetVariable|String|The variable that contains the name of the database object that is used to open a rowset.|  
|ParameterMapping|String|The mapping from parameters in the SQL command to variables.|  
|SqlCommand|String|The SQL command to be executed.|  
|SqlCommandVariable|String|The variable that contains the SQL command to be executed.|  
  
 The output and the output columns of the OLE DB source have no custom properties.  
  
 For more information, see [OLE DB Source](../../integration-services/data-flow/ole-db-source.md).  
  
 **Destination Custom Properties**  
  
 The OLE DB destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the OLE DB destination. All properties are read/write.  
  
> [!NOTE]  
>  The FastLoad options listed here (FastLoadKeepIdentity, FastLoadKeepNulls, and FastLoadOptions) correspond to the similarly named properties exposed by the **IRowsetFastLoad** interface implemented by the Microsoft OLE DB Provider for SQL Server (SQLOLEDB). For more information, search for IRowsetFastLoad in the MSDN Library.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer (enumeration)|A value that specifies how the destination access its destination database.<br /><br /> This property can have one of the following values:<br /><br /> <br /><br /> **OpenRowset** (0)-You provide the name of a table or view.<br /><br /> **OpenRowset from Variable** (1)-You provide the name of a variable that contains the name of a table or view.<br /><br /> **OpenRowset Using Fastload** (3)-You provide the name of a table or view.<br /><br /> **OpenRowset Using Fastload from Variable** (4)-You provide the name of a variable that contains the name of a table or view.<br /><br /> **SQL Command** (2)-You provide a SQL statement.|  
|AlwaysUseDefaultCodePage|Boolean|A value that indicates whether to use the value of the **DefaultCodePage** property for each column, or to try to derive the codepage from each column's locale. The default value of this property is **False**.|  
|CommandTimeout|Integer|The maximum number of seconds that the SQL command can run before timing out. A value of 0 indicates an infinite time. The default value of this property is 0.<br /><br /> Note: This property is not available in the **OLE DB Destination Editor**, but can be set by using the **Advanced Editor**.|  
|DefaultCodePage|Integer|The default codepage associated with the OLE DB destination.|  
|FastLoadKeepIdentity|Boolean|A value that specifies whether to copy identity values when data is loaded. This property is available only with one of the fast load options. The default value of this property is **False**. This property corresponds to the OLE DB [IRowsetFastLoad &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/irowsetfastload-ole-db.md) property **SSPROP_FASTLOADKEEPIDENTITY**.|  
|FastLoadKeepNulls|Boolean|A value that specifies whether to copy Null values when data is loaded. This property is available only with one of the fast load options. The default value of this property is **False**. This property corresponds to the OLE DB [IRowsetFastLoad &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/irowsetfastload-ole-db.md) property **SSPROP_FASTLOADKEEPNULLS**.|  
|FastLoadMaxInsertCommitSize|Integer|A value that specifies the batch size that the OLE DB destination tries to commit during fast load operations. The default value, **0**, indicates a single commit operation after all rows are processed.|  
|FastLoadOptions|String|A collection of fast load options. The fast load options include the locking of tables and the checking of constraints. You can specify one, both, or neither. This property corresponds to the OLE DB IRowsetFastLoad property **SSPROP_FASTLOADOPTIONS** and accepts string options such as **CHECK_CONSTRAINTS** and **TABLOCK**.<br /><br /> Note: Some options for this property are not available in the **Excel Destination Editor**, but can be set by using the **Advanced Editor**.|  
|OpenRowset|String|When AccessMode is **OpenRowset**, the name of the table or view that the OLE DB destination accesses.|  
|OpenRowsetVariable|String|When AccessMode is **OpenRowset from Variable**, the name of the variable that contains the name of the table or view that the OLE DB destination accesses.|  
|SqlCommand|String|When AccessMode is **SQL Command**, the Transact-SQL statement that the OLE DB destination uses to specify the destination columns for the data.|  
  
 The input and the input columns of the OLE DB destination have no custom properties.  
  
 For more information, see [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md).  
  
## See Also  
 [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
  
