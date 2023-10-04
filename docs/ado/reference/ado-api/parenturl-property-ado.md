---
title: "ParentURL Property (ADO)"
description: "ParentURL Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Record::ParentURL"
helpviewer_keywords:
  - "ParentURL property [ADO]"
apitype: "COM"
---
# ParentURL Property (ADO)
Indicates an absolute URL string that points to the parent [Record](./record-object-ado.md) of the current **Record** object.  
  
## Return Value  
 Returns a **String** value that indicates the URL of the parent **Record**.  
  
## Remarks  
 The **ParentURL** property depends on the source used to open the **Record** object. For example, the **Record** can be opened with a source that contains a relative path name of a directory referenced by the [ActiveConnection](./activeconnection-property-ado.md) property.  
  
 Suppose "second" is a folder contained under "first". Open the **Record** object by using the following syntax:  
  
```  
record.ActiveConnection = "https://first"  
record.Open "second"  
```  
  
 Now, the value of `the` **ParentURL** property is `"https://first"`, the same as **ActiveConnection**.  
  
 The source can also be an absolute URL such as, `"https://first/second"`. The **ParentURL** property is then `"https://first"`, the level above `"second"`.  
  
 This property may be a null value if:  
  
-   There is no parent for the current object; for example, if the **Record** object represents the root of a directory.  
  
-   The **Record** object represents an entity that cannot be specified with a URL.  
  
 This property is read-only.  
  
> [!NOTE]
>  This property is only supported by document source providers, such as the [Microsoft OLE DB Provider for Internet Publishing](../../guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Records and Provider-Supplied Fields](../../guide/data/records-and-provider-supplied-fields.md).  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../guide/data/absolute-and-relative-urls.md).  
  
> [!NOTE]
>  If the current record contains a data record from an ADO **Recordset**, accessing the **ParentURL** property causes a run-time error, indicating that no URL is possible.  
  
## Applies To  
 [Record Object (ADO)](./record-object-ado.md)