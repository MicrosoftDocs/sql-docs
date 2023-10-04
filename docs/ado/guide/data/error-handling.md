---
title: "Error Handling"
description: "Error Handling in ADO"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "reporting errors [ADO]"
  - "errors [ADO]"
  - "ADO, error handling"
---
# Error Handling in ADO
ADO uses several different methods to notify an application of errors that occur. This section discusses the types of errors that can occur when you are using ADO and how your application is notified. It concludes by making suggestions about how to handle those errors.  
  
## How Does ADO Report Errors?  
 ADO notifies you about errors in several ways:  
  
-   ADO errors generate a run-time error. Handle an ADO error the same way you would any other run-time error, such as using an **On Error** statement in Visual Basic.  
  
-   Your program can receive errors from OLE DB. An OLE DB error generates a run-time error as well.  
  
-   If the error is specific to your data provider, one or more **Error** objects are placed in the **Errors** collection of the **Connection** object that was used to access the data store when the error occurred.  
  
-   If the process that raised an event also produced an error, error information is placed in an **Error** object and passed as a parameter to the event. See [Handling ADO Events](./handling-ado-events.md) for more information about events.  
  
-   Problems that occur when processing batch updates or other bulk operations involving a **Recordset** can be indicated by the **Status** property of the **Recordset**. For example, schema constraint violations or insufficient permissions can be specified by **RecordStatusEnum** values.  
  
-   Problems that occur involving a particular **Field** in the current record are also indicated by the **Status** property of each **Field** in the **Fields** collection of the **Record** or **Recordset**. For example, updates that could not be completed or incompatible data types can be specified by **FieldStatusEnum** values.  
  
 This section contains the following topics.  
  
-   [ADO Errors](./ado-errors.md)  
  
-   [Provider Errors](./provider-errors.md)  
  
-   [Field-Related Error Information](./field-related-error-information.md)  
  
-   [Recordset-Related Error Information](./recordset-related-error-information.md)  
  
-   [Handling Errors In Other Languages](./handling-errors-in-other-languages.md)  
  
-   [Anticipating Errors](./anticipating-errors.md)