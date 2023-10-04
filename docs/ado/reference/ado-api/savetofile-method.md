---
title: "SaveToFile Method"
description: "SaveToFile Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_SaveToFile"
  - "_Stream::SaveToFile"
helpviewer_keywords:
  - "SaveToFile method [ADO]"
apitype: "COM"
---
# SaveToFile Method
Saves the binary contents of a [Stream](./stream-object-ado.md) to a file.  
  
## Syntax  
  
```  
  
Stream.SaveToFile FileName, SaveOptions  
```  
  
#### Parameters  
 *FileName*  
 A **String** value that contains the fully-qualified name of the file to which the contents of the **Stream** will be saved. You can save to any valid local location, or any location you have access to via a UNC value.  
  
 *SaveOptions*  
 A [SaveOptionsEnum](./saveoptionsenum.md) value that specifies whether a new file should be created by **SaveToFile**, if it does not already exist. Default value is **adSaveCreateNotExists**. With these options you can specify that an error occurs if the specified file does not exist. You can also specify that **SaveToFile** overwrites the current contents of an existing file.  
  
> [!NOTE]
>  If you overwrite an existing file (when **adSaveCreateOverwrite** is set), **SaveToFile** truncates any bytes from the original existing file that follow the new [EOS](./eos-property.md).  
  
## Remarks  
 **SaveToFile** may be used to copy the contents of a **Stream** object to a local file. There is no change in the contents or properties of the **Stream** object. The **Stream** object must be open before calling **SaveToFile**.  
  
 This method does not change the association of the **Stream** object to its underlying source. The **Stream** object will still be associated with the original URL or **Record** that was its source when opened.  
  
 After a **SaveToFile** operation, the current position ([Position](./position-property-ado.md)) in the stream is set to the beginning of the stream (0).  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)  
  
## See Also  
 [Open Method (ADO Stream)](./open-method-ado-stream.md)   
 [Save Method](./save-method.md)