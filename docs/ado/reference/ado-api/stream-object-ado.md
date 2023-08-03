---
title: "Stream Object (ADO)"
description: "Stream Object (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Stream"
helpviewer_keywords:
  - "Stream object [ADO]"
apitype: "COM"
---
# Stream Object (ADO)
Represents a stream of binary data or text.  
  
 In tree-structured hierarchies such as a file system or an e-mail system, a [Record](./record-object-ado.md) may have a default binary stream of bits associated with it that contains the contents of the file or the e-mail. A **Stream** object can be used to manipulate fields or records containing these streams of data. A **Stream** object can be obtained in these ways:  
  
-   From a URL pointing to an object (typically a file) containing binary or text data. This object can be a simple document, a **Record** object representing a structured document, or a folder.  
  
-   By opening the default **Stream** object associated with a **Record** object. You can obtain the default stream associated with a **Record** object when the **Record** is opened, to eliminate a round-trip just to open the stream.  
  
-   By instantiating a **Stream** object. These **Stream** objects can be used to store data for the purposes of your application. Unlike a **Stream** associated with a URL, or the default **Stream** of a **Record**, an instantiated **Stream** has no association with an underlying source by default.  
  
 With the methods and properties of a **Stream** object, you can do the following:  
  
-   Open a **Stream** object from a **Record** or URL with the [Open](./open-method-ado-stream.md) method.  
  
-   Close a **Stream** with the [Close](./close-method-ado.md) method.  
  
-   Input bytes or text to a **Stream** with the [Write](./write-method.md) and [WriteText](./writetext-method.md) methods.  
  
-   Read bytes from the **Stream** with the [Read](./read-method.md) and [ReadText](./readtext-method.md) methods.  
  
-   Write any **Stream** data still in the ADO buffer to the underlying object with the [Flush](./flush-method-ado.md) method.  
  
-   Copy the contents of a **Stream** to another **Stream** with the [CopyTo](./copyto-method-ado.md) method.  
  
-   Control how lines are read from the source file with the [SkipLine](./skipline-method.md)method and the [LineSeparator](./lineseparator-property-ado.md) property.  
  
-   Determine the end of stream position with the [EOS](./eos-property.md)property and [SetEOS](./seteos-method.md) method.  
  
-   Save and restore data in files with the [SaveToFile](./savetofile-method.md)and [LoadFromFile](./loadfromfile-method-ado.md) methods.  
  
-   Specify the character set used for storing the **Stream** with the [Charset](./charset-property-ado.md) property.  
  
-   Halt an asynchronous **Stream** operation with the [Cancel](./cancel-method-ado.md) method.  
  
-   Determine the number of bytes in a **Stream** with the [Size](./size-property-ado-stream.md) property.  
  
-   Control the current position within a **Stream** with the [Position](./position-property-ado.md) property.  
  
-   Determine the type of data in a **Stream** with the [Type](./type-property-ado-stream.md) property.  
  
-   Determine the current state of the **Stream** (closed, open, or executing) with the [State](./state-property-ado.md) property.  
  
-   Specify the access mode for the **Stream** with the [Mode](./mode-property-ado.md) property.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../guide/data/absolute-and-relative-urls.md).  
  
 The **Stream** object is safe for scripting.  
  
 This section contains the following topics.  
  
-   [Stream Object Properties, Methods, and Events](./stream-object-properties-methods-and-events.md)  
  
## See Also  
 [Records and Streams](../../guide/data/records-and-streams.md)