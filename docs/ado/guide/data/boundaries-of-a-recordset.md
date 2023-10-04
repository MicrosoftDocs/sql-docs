---
title: "Boundaries of a Recordset"
description: "Boundaries of a Recordset"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "EOF property [ADO], boundaries of a Recordset"
  - "Recordset object [ADO], boundaries of a Recordset"
  - "BOF property [ADO], boundaries of a Recordset"
---
# Boundaries of a Recordset
**Recordset** supports the **BOF** and **EOF** properties to delineate the beginning and end, respectively, of the dataset. You can think of **BOF** and **EOF** as "phantom" records that are positioned at the beginning and end of the **Recordset**. Counting **BOF** and **EOF**, our sample **Recordset** would now look like this:  
  
|ProductID|ProductName|UnitPrice|  
|---------------|-----------------|---------------|  
|BOF|||  
|7|Uncle Bob's Organic Dried Pears|30.0000|  
|14|Tofu|23.2500|  
|28|Rssle Sauerkraut|45.6000|  
|51|Manjimup Dried Apples|53.0000|  
|74|Longlife Tofu|10.0000|  
|EOF|||  
  
 When a cursor moves past the last record, **EOF** is set to **True**; otherwise, its value is **False**. Similarly, when the cursor moves before the first record, **BOF** is set to **True**; otherwise, its value is **False**. These properties are commonly used to enumerate records in the dataset, as illustrated in the following JScript code fragment.  
  
```  
while (objRecordset.EOF != true)   
{  
   // Work on the current record.  
   ...  
   // Advance the cursor forward to the next record.  
   objRecordset.MoveNext();  
}  
or  
while (objRecordset.BOF != true)   
{  
   // Work on the current record.  
   ...  
   // Move the cursor to the previous record.  
   objRecordset.MovePrevious();  
}  
```  
  
 If both **BOF** and **EOF** are **True**, the **Recordset** object is empty. Both properties will be **False** for a newly opened, non-empty **Recordset** object. You can use the **BOF** and **EOF** properties together to determine if a **Recordset** object is empty or not, as shown in the following JScript code fragment.  
  
```  
if (objRecordset.EOF == true && objRecordset.BOF == true)  
{  
   WScript.Echo("we got an empty dataset.");  
}  
else  
{  
   WScript.Echo("we got a full dataset.");  
}  
```  
  
 This scheme works for all types of cursor and is independent of the underlying providers. If you attempt to determine the emptiness of a **Recordset** object by checking if its **RecordCount** property value is zero (0) or not, you must take precautions to use an appropriate cursor and provider that support returning of the number of records in the result.  
  
 If you delete the last remaining record in the **Recordset** object, the cursor is left in an indeterminate state. The **BOF** and **EOF** properties may remain **False** until you attempt to reposition the current record, depending upon the provider. For more information, see [Deleting Records Using the Delete Method](./deleting-records-using-the-delete-method.md).