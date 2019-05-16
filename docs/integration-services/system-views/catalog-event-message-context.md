---
title: "catalog.event_message_context | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 273a54f8-b107-4f36-9461-2b475644760d
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.event_message_context 

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays information about the conditions that are associated with execution event messages, for executions on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Context_id|bigint|Unique ID for the error context.|  
|Event_message_id|bigint|Unique ID for the message that the context relates to.|  
|Context_depth|int|As the depth increases, the context is further from the error. When an error occurs, the context depth starts at 1. The value of 0 indicates the state of the package before execution starts.|  
|Package_path|Nvarchar(max)|The package path for the context source.|  
|Context_type|smallint|The type of object that is the source for the context. See the **Remarks** section for a list of context types.|  
|Context_source_name|Nvarchar(4000)|The name of the object that is the source for the context.|  
|Context_source_id|Nvarchar(38)|The unique ID of the object that is the source for the context.|  
|Property_name|Nvarchar(4000)|The name of the property associated with the source of the context.|  
|Property_value|Sql_variant|The property value associated with the source of the context.|  
  
## Remarks  
 The following table lists the context types.  
  
||||  
|-|-|-|  
|Context type value|Type Name|Description|  
|10|Task|State of a task when an error occurred.|  
|20|Pipeline|Error from a pipeline component: source, destination, or transformation component.|  
|30|Sequence|State of a sequence.|  
|40|For Loop|State of a For Loop.|  
|50|Foreach Loop|State of a Foreach Loop|  
|60|Package|State of the package when an error occurred.|  
|70|Variable|Variable value|  
|80|Connection manager|Properties of a connection manager.|  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the operation  
  
-   Membership to the **ssis_admin** database role.  
  
-   Membership to the **sysadmin** server role.  
  
  
