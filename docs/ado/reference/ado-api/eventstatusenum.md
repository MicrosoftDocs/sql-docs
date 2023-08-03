---
title: "EventStatusEnum"
description: "EventStatusEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "EventStatusEnum"
helpviewer_keywords:
  - "EventStatusEnum enumeration [ADO]"
apitype: "COM"
---
# EventStatusEnum
Specifies the current status of the execution of an event.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adStatusCancel**|4|Requests cancellation of the operation that caused the event to occur.|  
|**adStatusCantDeny**|3|Indicates that the operation cannot request cancellation of the pending operation.|  
|**adStatusErrorsOccurred**|2|Indicates that the operation that caused the event failed due to an error or errors.|  
|**adStatusOK**|1|Indicates that the operation that caused the event was successful.|  
|**adStatusUnwantedEvent**|5|Prevents subsequent notifications before the event method has finished executing.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.EventStatus.CANCEL|  
|AdoEnums.EventStatus.CANTDENY|  
|AdoEnums.EventStatus.ERRORSOCCURRED|  
|AdoEnums.EventStatus.OK|  
|AdoEnums.EventStatus.UNWANTEDEVENT|  
  
## Applies To  

:::row:::
    :::column:::
        [BeginTransComplete, CommitTransComplete, and RollbackTransComplete Events (ADO)](../../../ado/reference/ado-api/begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)  

        [ConnectComplete and Disconnect Events (ADO)](../../../ado/reference/ado-api/connectcomplete-and-disconnect-events-ado.md)  

        [EndOfRecordset Event (ADO)](../../../ado/reference/ado-api/endofrecordset-event-ado.md)  

        [ExecuteComplete Event (ADO)](../../../ado/reference/ado-api/executecomplete-event-ado.md)  
    :::column-end:::
    :::column:::
        [FetchComplete Event (ADO)](../../../ado/reference/ado-api/fetchcomplete-event-ado.md)  

        [InfoMessage Event (ADO)](../../../ado/reference/ado-api/infomessage-event-ado.md)  

        [WillChangeField and FieldChangeComplete Events (ADO)](../../../ado/reference/ado-api/willchangefield-and-fieldchangecomplete-events-ado.md)  

        [WillChangeRecord and RecordChangeComplete Events (ADO)](../../../ado/reference/ado-api/willchangerecord-and-recordchangecomplete-events-ado.md)  
    :::column-end:::
    :::column:::
        [WillChangeRecordset and RecordsetChangeComplete Events (ADO)](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md)  

        [WillConnect Event (ADO)](../../../ado/reference/ado-api/willconnect-event-ado.md)  

        [WillExecute Event (ADO)](../../../ado/reference/ado-api/willexecute-event-ado.md)  

        [WillMove and MoveComplete Events (ADO)](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md)  
    :::column-end:::
:::row-end:::
