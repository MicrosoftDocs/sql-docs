---
title: "Open Method (ADO Stream)"
description: "Open Method (ADO Stream)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_Open"
  - "_Stream::Open"
helpviewer_keywords:
  - "Open method [ADO]"
apitype: "COM"
---
# Open Method (ADO Stream)
Opens a [Stream](./stream-object-ado.md) object to manipulate streams of binary or text data.  
  
## Syntax  
  
```  
  
Stream.Open Source, Mode , OpenOptions, UserName, Password  
```  
  
#### Parameters  
 *Source*  
 Optional. A **Variant** value that specifies the source of data for the **Stream**. *Source* may contain an absolute URL string that points to an existing node in a well-known tree structure, such as an e-mail or file system. A URL should be specified by using the URL keyword ("URL=*scheme*://*server*/*folder*"). Alternatively, *Source* may contain a reference to an already open [Record](./record-object-ado.md) object, which opens the default stream associated with the **Record**. If *Source* is not specified, a **Stream** is instantiated and opened, associated with no underlying source by default. For more information about URL schemes and their associated providers, see [Absolute and Relative URLs](../../guide/data/absolute-and-relative-urls.md).  
  
 *Mode*  
 Optional. A [ConnectModeEnum](./connectmodeenum.md) value that specifies the access mode for the resultant **Stream** (for example, read/write or read-only). Default value is **adModeUnknown**. See the [Mode](./mode-property-ado.md) property for more information about access modes. If *Mode* is not specified, it is inherited by the source object. For example, if the source **Record** is opened in read-only mode, the **Stream** will also be opened in read-only mode by default.  
  
 *OpenOptions*  
 Optional. A [StreamOpenOptionsEnum](./streamopenoptionsenum.md) value. Default value is **adOpenStreamUnspecified**.  
  
 *UserName*  
 Optional. A **String** value that contains the user identification that, if it is needed, accesses the **Stream** object.  
  
 *Password*  
 Optional. A **String** value that contains the password that, if it is needed, accesses the **Stream** object.  
  
## Remarks  
 When a **Record** object is passed in as the source parameter, the *UserID* and *Password* parameters are not used because access to the **Record** object is already available. Similarly, the [Mode](./mode-property-ado.md) of the **Record** object is transferred to the **Stream** object. When *Source* is not specified, the **Stream** opened contains no data and has a [Size](./size-property-ado-stream.md) of zero (0). To avoid losing any data that is written to this **Stream** when the **Stream** is closed, save the **Stream** with the [CopyTo](./copyto-method-ado.md) or [SaveToFile](./savetofile-method.md) methods, or save it to another memory location.  
  
 An *OpenOptions* value of **adOpenStreamFromRecord** identifies the contents of the *Source* parameter to be an already open **Record** object. The default behavior is to treat *Source* as a URL that points directly to a node in a tree structure, such as a file. The default stream associated with that node is opened.  
  
 While the **Stream** is not open, it is possible to read all the read-only properties of the **Stream**. If a **Stream** is opened asynchronously, all subsequent operations (other than checking the [State](./state-property-ado.md) and other read-only properties) are blocked until the **Open** operation is completed.  
  
 In addition to the options that were discussed earlier, by not specifying *Source*, you can create an instance of a **Stream** object in memory without associating it with an underlying source. You can dynamically add data to the stream by writing binary or text data to the **Stream** with [Write](./write-method.md) or [WriteText](./writetext-method.md), or by loading data from a file with [LoadFromFile](./loadfromfile-method-ado.md).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [Open Method (ADO Connection)](./open-method-ado-connection.md)   
 [Open Method (ADO Record)](./open-method-ado-record.md)   
 [Open Method (ADO Recordset)](./open-method-ado-recordset.md)   
 [OpenSchema Method](./openschema-method.md)   
 [SaveToFile Method](./savetofile-method.md)