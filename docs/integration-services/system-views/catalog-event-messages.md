---
description: "catalog.event_messages"
title: "catalog.event_messages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: a31a654f-31e9-4da1-aabf-182b07848e36
author: chugugrace
ms.author: chugu
---
# catalog.event_messages 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  Displays information about messages that were logged during operations.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Event_message_ID|bigint|The unique ID of the event message.|  
|Operation_id|bigint|The type of operation.<br /><br /> For a list of operation types, see [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md).|  
|Message_time|datetimeoffset(7)|The time the message was created.|  
|Message_type|smallint|The type of message displayed. For more information about message types, see [catalog.operation_messages &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operation-messages-ssisdb-database.md).|  
|Message_source_type|smallint|The source of the message.|  
|message|nvarchar(max)|The text of the message.|  
|Extended_info_id|bigint|The ID of additional information that relates to the operation message, found in the [catalog.extended_operation_info &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-extended-operation-info-ssisdb-database.md) view.|  
|Package_name|nvarchar(260)|The name of the package file.|  
|Event_name|nvarchar(1024)|The run-time event associated with the message.|  
|Message_source_name|nvarchar(4000)|The package component that is the source of the message.|  
|Message_source_id|nvarchar(38)|The unique ID of the source of the message.|  
|Subcomponent_name|nvarchar(4000)|The data flow component that is the source of the message.<br /><br /> When messages are returned by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] engine, SSIS.Pipeline appears in this column.|  
|Package_path|nvarchar(max)|The unique path of the component within the package.|  
|Execution_path|nvarchar(max)|The full path from the parent package to the point in which the component is executed.<br /><br /> This path also captures iterations of a component.|  
|threadID|int|ID for the thread that is executing when the message is logged.|  
|Message_code|int|The code associated with the message.|  
  
## Remarks  
 This view displays the following message source types.  
  
|**message_source_type**|Description|  
|-------------------------------|-----------------|  
|10|Entry APIs, such as T-SQL and CLR Stored procedures|  
|20|External process used to run package (ISServerExec.exe)|  
|30|Package-level objects|  
|40|Control Flow tasks|  
|50|Control Flow containers|  
|60|Data Flow task|  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the operation  
  
-   Membership to the **ssis_admin** database role.  
  
-   Membership to the **sysadmin** server role.  
  
## See Also  
 [catalog.event_message_context](../../integration-services/system-views/catalog-event-message-context.md)  
  
  
