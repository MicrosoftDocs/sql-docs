---
title: "Charset Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::Charset"
helpviewer_keywords: 
  - "Charset property [ADO]"
ms.assetid: e42507cb-9b46-4ce4-8191-2948eaf14ca2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Charset Property (ADO)
Indicates the character set into which the contents of a text [Stream](../../../ado/reference/ado-api/stream-object-ado.md) should be translated for storage in the internal buffer of the **Stream** object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that specifies the character set into which the contents of the **Stream** will be translated. The default value is **Unicode**. Allowed values are typical strings passed over the interface as Internet character set names (for example, "iso-8859-1", "Windows-1252", and so on). For a list of the character set names that are known by a system, see the subkeys of HKEY_CLASSES_ROOT\MIME\Database\Charset in the Windows Registry.  
  
## Remarks  
 In a text **Stream** object, text data is stored in the character set specified by the **Charset** property. The default is Unicode. The **Charset** property is used for converting data going into the **Stream** or coming out of the **Stream**. For example, if the **Stream** contains ISO-8859-1 data and that data is copied to a BSTR, the **Stream** object will convert the data to Unicode. The reverse is also true.  
  
 For an open **Stream**, the current [Position](../../../ado/reference/ado-api/position-property-ado.md) must be at the beginning of the **Stream** (0) to be able to set **Charset**.  
  
 **Charset** is used only with text **Stream** objects ([Type](../../../ado/reference/ado-api/type-property-ado-stream.md) is **adTypeText**). This property is ignored if **Type** is **adTypeBinary**.  
  
 For a code sample, see [Step 4: Populate the Details Text Box](../../../ado/guide/data/step-4-populate-the-details-text-box.md).  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
