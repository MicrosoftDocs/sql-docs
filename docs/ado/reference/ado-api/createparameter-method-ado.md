---
title: "CreateParameter Method (ADO)"
description: "CreateParameter Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command15::raw_CreateParameter"
  - "Command15::CreateParameter"
helpviewer_keywords:
  - "CreateParameter method [RDS]"
apitype: "COM"
---
# CreateParameter Method (ADO)
Creates a new [Parameter](./parameter-object.md) object with the specified properties.  
  
## Syntax  
  
```  
  
Set parameter = command.CreateParameter (Name, Type, Direction, Size, Value)  
```  
  
## Return Value  
 Returns a **Parameter** object.  
  
#### Parameters  
 *Name*  
 Optional. A **String** value that contains the name of the **Parameter** object.  
  
 *Type*  
 Optional. A [DataTypeEnum](./datatypeenum.md) value that specifies the data type of the **Parameter** object.  
  
 *Direction*  
 Optional. A [ParameterDirectionEnum](./parameterdirectionenum.md) value that specifies the type of **Parameter** object.  
  
 *Size*  
 Optional. A **Long** value that specifies the maximum length for the parameter value in characters or bytes.  
  
 *Value*  
 Optional. A **Variant** that specifies the value for the **Parameter** object.  
  
## Remarks  
 Use the **CreateParameter** method to create a new **Parameter** object with a specified name, type, direction, size, and value. Any values you pass in the arguments are written to the corresponding **Parameter** properties.  
  
 This method does not automatically append the **Parameter** object to the **Parameters** collection of a [Command](./command-object-ado.md) object. This lets you set additional properties whose values ADO will validate when you append the **Parameter** object to the collection.  
  
 If you specify a variable-length data type in the *Type* argument, you must either pass a *Size* argument or set the [Size](./size-property-ado-parameter.md) property of the **Parameter** object before appending it to the **Parameters** collection; otherwise, an error occurs.  
  
 If you specify a numeric data type (**adNumeric** or **adDecimal**) in the *Type* argument, then you must also set the [NumericScale](./numericscale-property-ado.md) and [Precision](./precision-property-ado.md) properties.  
  
## Applies To  
 [Command Object (ADO)](./command-object-ado.md)  
  
## See Also  
 [Append and CreateParameter Methods Example (VB)](./append-and-createparameter-methods-example-vb.md)   
 [Append and CreateParameter Methods Example (VC++)](./append-and-createparameter-methods-example-vc.md)   
 [Append Method (ADO)](./append-method-ado.md)   
 [Parameter Object](./parameter-object.md)   
 [Parameters Collection (ADO)](./parameters-collection-ado.md)