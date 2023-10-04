---
title: "Parameters Collection (ADO)"
description: "Parameters Collection (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command15::get_Parameters"
  - "Command15::Parameters"
  - "Command15::GetParameters"
helpviewer_keywords:
  - "Parameters collection [ADO]"
apitype: "COM"
---
# Parameters Collection (ADO)
Contains all the [Parameter](./parameter-object.md) objects of a [Command](./command-object-ado.md) object.  
  
## Remarks  
 A **Command** object has a **Parameters** collection made up of **Parameter** objects.  
  
 Using the [Refresh](./refresh-method-ado.md) method on a **Command** object's **Parameters** collection retrieves provider parameter information for the stored procedure or parameterized query specified in the **Command** object. Some providers do not support stored procedure calls or parameterized queries; calling the **Refresh** method on the **Parameters** collection when using such a provider will return an error.  
  
 If you have not defined your own **Parameter** objects and you access the **Parameters** collection before calling the **Refresh** method, ADO will automatically call the method and populate the collection for you.  
  
 You can minimize calls to the provider to improve performance if you know the properties of the parameters associated with the stored procedure or parameterized query you wish to call. Use the [CreateParameter](./createparameter-method-ado.md) method to create **Parameter** objects with the appropriate property settings and use the [Append](./append-method-ado.md) method to add them to the **Parameters** collection. This lets you set and return parameter values without having to call the provider for the parameter information. If you are writing to a provider that does not supply parameter information, you must manually populate the **Parameters** collection using this method to be able to use parameters at all. Use the [Delete](./delete-method-ado-parameters-collection.md) method to remove **Parameter** objects from the **Parameters** collection if necessary.  
  
 The objects in the **Parameters** collection of a **Recordset** go out of scope (therefore becoming unavailable) when the **Recordset** is closed.  
  
 When calling a stored procedure with **Command**, the return value/output parameter of a stored procedure is retrieved as follows:  
  
1.  When calling a stored procedure that has no parameters, the **Refresh** method on the **Parameters** collection should be called before calling the **Execute** method on the **Command** object.  
  
2.  When calling a stored procedure with parameters and explicitly appending a parameter to the **Parameters** collection with **Append**, the return value/output parameter should be appended to the **Parameters** collection. The return value must first be appended to the **Parameters** collection. Use **Append** to add the other parameters into the **Parameters** collection in the order of definition. For example, the stored procedure SPWithParam has two parameters. The first parameter, *InParam*, is an input parameter defined as adVarChar (20), and the second parameter, *OutParam*, is an output parameter defined as adVarChar (20). You can retrieve the return value/output parameter with the following code.  
  
    ```vb
    ' Open Connection Conn  
    set ccmd = CreateObject("ADODB.Command")  
    ccmd.Activeconnection= Conn  
  
    ccmd.CommandText="SPWithParam"  
    ccmd.commandType = 4 'adCmdStoredProc  
  
    ccmd.parameters.Append ccmd.CreateParameter(, adInteger, adParamReturnValue, , NULL)   ' return value  
    ccmd.parameters.Append ccmd.CreateParameter("InParam", adVarChar, adParamInput, 20, "hello world")   ' input parameter  
    ccmd.parameters.Append ccmd.CreateParameter("OutParam", adVarChar, adParamOutput, 20, NULL)   ' output parameter  
  
    ccmd.execute()  
  
    ' Access ccmd.parameters(0) as return value of this stored procedure  
    ' Access ccmd.parameters("OutParam") as the output parameter of this stored procedure.  
  
    ```  
  
3.  When calling a stored procedure with parameters and configuring the parameters by calling the **Item** method on the **Parameters** collection, the return value/output parameter of the stored procedure can be retrieved from the **Parameters** collection. For example, the stored procedure SPWithParam has two parameters. The first parameter, *InParam*, is an input parameter defined as adVarChar (20), and the second parameter, *OutParam*, is an output parameter defined as adVarChar (20). You can retrieve the return value/output parameter with the following code.  
  
    ```vb
    ' Open Connection Conn  
    set ccmd = CreateObject("ADODB.Command")  
    ccmd.Activeconnection= Conn  
  
    ccmd.CommandText="SPWithParam"  
    ccmd.commandType = 4 'adCmdStoredProc  
  
    ccmd.parameters.Item("InParam").value = "hello world" ' input parameter  
    ccmd.execute()  
  
    ' Access ccmd.parameters(0) as return value of stored procedure  
    ' Access ccmd.parameters(2) or ccmd.parameters("OutParam") as the output parameter.  
    ```  
  
 This section contains the following topic.  
  
-   [Parameters Collection Properties, Methods, and Events](./parameters-collection-properties-methods-and-events.md)  
  
## See Also  
 [Append Method (ADO)](./append-method-ado.md)   
 [CreateParameter Method (ADO)](./createparameter-method-ado.md)   
 [Parameter Object](./parameter-object.md)