---
title: "Anticipating Errors"
description: "Anticipating Errors"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "anticipating errors [ADO]"
  - "errors [ADO], preventing"
  - "preventing errors [ADO]"
---
# Anticipating Errors
Error prevention is at least as important as error handling. This final section contains a short list of precautions your application can take to help make errors less likely to occur.  
  
 Check the state of objects by checking the value in the **State** property before trying to perform an operation using those objects. For example, if your application uses a global **Connection**, check its **State** property to see if it is already open before calling the **Open** method.  
  
-   Any program that accepts data from a user must include code to validate that data before sending it to the data store. You cannot rely on the data store, the provider, ADO, or even your programming language to notify you of problems. You must check every byte entered by your users, making sure that data is the correct type for its field and that required fields are not empty.  
  
 Check the data before you try to write any data to the data store. The easiest way to do so is to handle the **WillMove** event or the **WillUpdateRecordset** event. For a more complete discussion of handling ADO events, see [Handling ADO Events](./handling-ado-events.md).  
  
 Make sure that **Recordset** objects are not beyond the boundaries of the **Recordset** before attempting to move the record pointer. If you try to **MoveNext** when **EOF** is True or **MovePrev** when **BOF** is True, an error will occur. If you perform any of the **Move** methods when both **EOF** and **BOF** are True, an error will be generated.  
  
 Errors also will occur if you try to perform operations such as **Seek** and **Find** on an empty **Recordset**.