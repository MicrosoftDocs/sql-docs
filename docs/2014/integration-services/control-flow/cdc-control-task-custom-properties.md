---
title: "CDC Control Task Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 2a073699-79a2-4ea1-a68e-fc17a80b74ba
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# CDC Control Task Custom Properties
  The following table describes the custom properties of the CDC Control task. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|Connection|ADO.NET Connection|An ADO.NET connection to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CDC database for access to the change tables and to the CDC State if stored in the same database.<br /><br /> The connection must be to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that is enabled for CDC and where the selected change table is located.|  
|TaskOperation|Integer (enumeration)|The selected operation for the CDC control task. The possible values are **Mark Initial Load Start**, **Mark Initial Load End**, **Mark CDC Start**, **Get Processing Range**, **Mark Processed Range**, and **Reset CDC State**.<br /><br /> If you select **MarkCdcStart**, **MarkInitialLoadStart**, or **MarkInitialLoadEnd** when working on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC (that is, not Oracle) the user specified in the connection manager must be either  **db_owner** or **sysadmin**.<br /><br /> For more information about these operations, see [CDC Control Task Editor](../cdc-control-task-editor.md) and [CDC Control Task](cdc-control-task.md).|  
|OperationParameter|String|Currently used with the **MarkCdcStart** operation. This parameter allows additional input required for the specific operation. For example, the LSN number required for the **MarkCdcStart** operation|  
|StateVariable|String|An SSIS package variable that stores the CDC state of the current CDC context. The CDC Control task reads and writes the state to the **StateVariable** and does not load it or store it to a persistent storage unless **AutomaticStatePersistence** is selected. See [Define a State Variable](../data-flow/define-a-state-variable.md).|  
|AutomaticStatePersistence|Boolean|The CDC Control task reads the CDC State from the CDC State package variable. Following an operation, the CDC Control task updates the value of the CDC State package variable. The **AutomaticStatePersistence** property tells the CDC Control task who is responsible for persisting the CDC State value between runs of the SSIS package.<br /><br /> When this property is **true**, the CDC Control task automatically loads the value of the CDC State variable from a state table. When the CDC Control task updates the value of the CDC State variable it also updates its value in the same state **table.stores**, the state in a special table and updates the State Variable. The developer can control which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database contains that state table and its name. The structure of this state table is predefined.<br /><br /> When **false**, the CDC Control task does not deal with persisting its value. When true, the CDC Control task stores the state in a special table and updates the StateVariable.<br /><br /> The default value is **true**, indicating that state persistence is updated automatically.|  
|StateConnection|ADO.NET Connection|An ADO.NET connection to the database where the state table resides when using **AutomaticStatePersistence**. The default value is the same value for **Connection**.|  
|StateName|String|The name associated with the persistent state. The full load and CDC packages that work with the same CDC context specify a common CDC context name. This name is used for looking up the state row in the state table.<br /><br /> This property is applicable only when **AutomaticStatePersistence** is set to **true**.|  
|StateTable|String|Specifies the name of the table where the CDC context state is stored. This table must be accessible using the connection configured for this component. This table must include varchar columns called **name** and **state**. (The **state** column must have at least 256 characters).<br /><br /> This property is applicable only when **AutomaticStatePersistence** is set to **true**.|  
|CommandTimeout|integer|This value indicates the timeout (in seconds) to use when communicating with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. This value is used where the response time from the database is very slow and the default value (30 seconds) is not enough.|  
  
## See Also  
 [CDC Control Task](cdc-control-task.md)   
 [CDC Control Task Editor](../cdc-control-task-editor.md)  
  
  
