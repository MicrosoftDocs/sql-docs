---
title: "Provider Errors"
description: "Provider Errors"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "errors collection [ADO]"
  - "provider errors [ADO]"
  - "event-related errors [ADO]"
  - "errors [ADO], provider"
  - "Error object [ADO], provider errors"
---
# Provider Errors
When a provider error occurs, a run-time error of -2147467259 is returned. When you receive this error, check the **Errors** collection of the active **Connection** object, which will contain one or more errors describing what occurred.  
  
## The ADO Errors Collection  
 Because a particular ADO operation can produce multiple provider errors, ADO exposes a collection of error objects through the **Connection** object. This collection contains no objects if an operation concludes successfully and contains one or more **Error** objects if something went wrong and the provider raised one or more errors. Examine each error object to determine the exact cause of the error.  
  
 As soon as you have finished handling any errors that have occurred, you can clear the collection by calling the **Clear** method. It is especially important to explicitly clear the **Errors** collection before you call the **Resync**, **UpdateBatch**, or **CancelBatch** method on a **Recordset** object, the **Open** method on a **Connection** object, or set the **Filter** property on a **Recordset** object. By clearing the collection explicitly, you can be certain that any **Error** objects in the collection are not left over from a previous operation.  
  
 Some operations can generate warnings in addition to errors. Warnings are also represented by **Error** objects in the **Errors** collection. When a provider adds a warning to the collection, it does not generate a run-time error. Check the **Count** property of the **Errors** collection to determine whether a warning was produced by a particular operation. If the count is one or greater, an **Error** object has been added to the collection. As soon as you have determined that the **Errors** collection contains errors or warnings, you can iterate through the collection and retrieve information about each **Error** object that it contains. The following short Visual Basic example demonstrates this:  
  
```  
' BeginErrorHandlingVB02  
Private Function DeleteCustomer(ByVal CompanyName As String) As Long  
    On Error GoTo DeleteCustomerError  
  
    rst.Find "CompanyName='" & CompanyName & "'"  
DeleteCustomerError:  
Dim objError As ADODB.Error  
Dim strError As String  
  
    If cnn.Errors.Count > 0 Then  
        For Each objError In cnn.Errors  
            strError = strError & "Error #" & objError.Number & _  
                " " & objError.Description & vbCrLf & _  
                "NativeError: " & objError.NativeError & vbCrLf & _  
                "SQLState: " & objError.SQLState & vbCrLf & _  
                "Reported by: " & objError.Source & vbCrLf & _  
                "Help file: " & objError.HelpFile & vbCrLf & _  
                "Help Context ID: " & objError.HelpContext  
        Next  
        MsgBox strError  
    End If  
End Function  
' EndErrorHandlingVB02  
```  
  
 The error-handling routine includes a **For Each** loop that examines each object in the **Errors** collection. In this example, it accumulates a message for display. In a working program, you would write code to perform an appropriate task for each error, such as closing all open files and shutting down the program in an orderly manner.  
  
## The Error Object  
 By examining an **Error** object you can determine what error occurred, and more important, what application or what object caused the error. The **Error** object has the following properties:  
  
|Property name|Description|  
|-------------------|-----------------|  
|**Description**|A text description of the error that occurred.|  
|**HelpContext, HelpFile**|Refers to the Help topic and Help file that contain a description of the error that occurred.|  
|**NativeError**|The provider-specific error number.|  
|**Number**|A Long Integer that represents the number (listed in the **ErrorValueEnum**) of the error that occurred.|  
|**Source**|Indicates the name of the object or application that generated an error.|  
|**SQLState**|A five-character error code that the provider returns during the process of an SQL statement.|  
  
 The ADO **Error** object is very similar to the standard Visual Basic **Err** object. Its properties describe the error that occurred. In addition to the number of the error, you also receive two related pieces of information. The **NativeError** property contains an error number specific to the provider you are using. In the previous example, the provider is the Microsoft OLE DB Provider for SQL Server, so **NativeError** will contain errors specific to SQL Server. The **SQLState** property has a five-letter code that describes an error in an SQL statement.  
  
## Event-Related Errors  
 The **Error** object is also used when event-related errors occur. You can determine whether an error occurred in the process that raised an ADO event by checking the **Error** object passed as an event parameter.  
  
 If the operation that causes an event is concluded successfully, the *adStatus* parameter of the event handler will be set to *adStatusOK*. On the other hand, if the operation that raised the event was unsuccessful, the *adStatus* parameter is set to *adStatusErrorsOccurred*. In that case, the *pError* parameter will contain an **Error** object that describes the error.
