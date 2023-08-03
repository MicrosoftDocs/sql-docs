---
title: "Error Object"
description: "Error Object"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Error"
helpviewer_keywords:
  - "error object [ADO]"
apitype: "COM"
---
# Error Object
Contains details about data access errors that pertain to a single operation involving the provider.  
  
## Remarks  
 Any operation involving ADO objects can generate one or more provider errors. As each error occurs, one or more **Error** objects are placed in the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. When another ADO operation generates an error, the **Errors** collection is cleared, and the new set of **Error** objects is placed in the **Errors** collection.  
  
> [!NOTE]
>  Each **Error** object represents a specific provider error, not an ADO error. ADO errors are exposed to the run-time exception-handling mechanism. For example, in Microsoft Visual Basic, the occurrence of an ADO-specific error will trigger an **On Error** event and appear in the **Error** object. For a complete list of ADO errors, see the [ErrorValueEnum](../../../ado/reference/ado-api/errorvalueenum.md) topic.  
  
 You can read an **Error** object's properties to obtain specific details about each error, including the following:  
  
-   The [Description](../../../ado/reference/ado-api/description-property.md) property, which contains the text of the error. This is the default property.  
  
-   The [Number](../../../ado/reference/ado-api/number-property-ado.md) property, which contains the **Long** integer value of the error constant.  
  
-   The [Source](../../../ado/reference/ado-api/source-property-ado-error.md) property, which identifies the object that raised the error. This is particularly useful when you have several **Error** objects in the **Errors** collection following a request to a data source.  
  
-   The [SQLState](../../../ado/reference/ado-api/sqlstate-property.md) and [NativeError](../../../ado/reference/ado-api/nativeerror-property-ado.md) properties, which provide information from SQL data sources.  
  
 When a provider error occurs, it is placed in the **Errors** collection of the **Connection** object. ADO supports the return of multiple errors by a single ADO operation to allow for error information specific to the provider. To obtain this rich error information in an error handler, use the appropriate error-trapping features of the language or environment you are working with, then use nested loops to enumerate the properties of each **Error** object in the **Errors** collection.  
  
> [!NOTE]
>  **Microsoft Visual Basic and VBScript Users** If there is no valid **Connection** object, you will need to retrieve error information from the **Error** object.  
  
 Just as providers do, ADO clears the **OLE Error Info** object before making a call that could potentially generate a new provider error. However, the **Errors** collection on the **Connection** object is cleared and populated only when the provider generates a new error, or when the [Clear](../../../ado/reference/ado-api/clear-method-ado.md) method is called.  
  
 Some properties and methods return warnings that appear as **Error** objects in the **Errors** collection but do not halt a program's execution. Before you call the [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) methods on a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object; the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method on a **Connection** object; or set the [Filter](../../../ado/reference/ado-api/filter-property.md) property on a **Recordset** object, call the **Clear** method on the **Errors** collection. That way, you can read the [Count](../../../ado/reference/ado-api/count-property-ado.md) property of the **Errors** collection to test for returned warnings.  
  
 The **Error** object is not safe for scripting.  
  
 This section contains the following topic.  
  
-   [Error Object Properties, Methods, and Events](../../../ado/reference/ado-api/error-object-properties-methods-and-events.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
