---
title: "ADO Java Class Wrappers | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "class wrappers [ADO]"
ms.assetid: 1fc09dc1-9e32-412e-9f43-b8eb8bb483ca
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Java Class Wrappers
This code declares an instance of the ADO [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) class wrapper and initializes it, all on the same line of code. Further, it declares variables for each of the arguments in the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method, especially for [LockType](../../../ado/reference/ado-api/locktype-property-ado.md) and [CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md) (because Java doesn't support enumerated types). It opens and closes the **Recordset** object. Setting Rs1 to NULL merely schedules that variable to be released when Java performs its systematic and intermittent release of unused objects.  
  
```java
public static void main( String args[])  
{  
   msado15._Recordset   Rs1 = new msado15.Recordset();  
   Variant Source     = new Variant( "SELECT * FROM Authors" );  
   Variant Connect    = new Variant( "DSN=AdoDemo;UID=admin;PWD=;" );  
   int     LockType   = msado15.CursorTypeEnum.adOpenForwardOnly;  
   int     CursorType = msado15.LockTypeEnum.adLockReadOnly;  
   int     Options    = -1;  
  
   Rs1.Open( Source, Connect, LockType,  CursorType, Options );  
   Rs1.Close();  
   Rs1 = null;  
  
   System.out.println( "Success!\n" );  
}  
```  
  
## See Also  
 [Using the Microsoft SDK for Java](../../../ado/guide/appendixes/using-the-microsoft-sdk-for-java.md)
