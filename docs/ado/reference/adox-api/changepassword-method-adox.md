---
title: "ChangePassword Method (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_User25::raw_ChangePassword"
  - "_User25::ChangePassword"
helpviewer_keywords: 
  - "ChangePassword method [ADOX]"
ms.assetid: d187fbc6-5fac-4abb-803d-bf344dcf0302
author: MightyPen
ms.author: genemi
manager: craigg
---
# ChangePassword Method (ADOX)
Changes the password for a [user](../../../ado/reference/adox-api/user-object-adox.md) account.  
  
## Syntax  
  
```  
  
User.ChangePassword OldPassword, NewPassword  
```  
  
#### Parameters  
 *OldPassword*  
 A **String** value that specifies the user's existing password. If the user doesn't currently have a password, use an empty string ("") for *OldPassword*.  
  
 *NewPassword*  
 A **String** value that specifies the new password.  
  
## Remarks  
 For security reasons, the old password must be specified in addition to the new password.  
  
 An error will occur if the provider does not support the administration of trustee properties.  
  
## Applies To  
 [User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](../../../ado/reference/adox-api/groups-and-users-append-changepassword-methods-example-vb.md)
